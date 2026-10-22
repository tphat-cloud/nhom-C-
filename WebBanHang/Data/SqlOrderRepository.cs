using System.Data;
using Microsoft.Data.SqlClient;
using WebBanHang.Models;

namespace WebBanHang.Data;

/// <summary>
/// Đặt hàng bằng ADO.NET thuần với một SqlTransaction duy nhất:
///   1. Tìm hoặc tạo khách hàng theo số điện thoại
///   2. Tạo đơn (trạng thái "Chờ xử lý", tổng tiền tạm 0)
///   3. Với từng món: trừ kho có điều kiện (StockQuantity &gt;= @quantity) rồi ghi chi tiết
///   4. Cập nhật tổng tiền và commit
/// Nếu bất kỳ món nào không đủ hàng, toàn bộ đơn bị rollback (không trừ kho dở dang).
/// </summary>
public sealed class SqlOrderRepository : IOrderRepository
{
    /// <summary>Số lần thử lại khi mã đơn bị trùng (UNIQUE OrderCode) do 2 khách đặt cùng lúc.</summary>
    private const int MaxOrderCodeAttempts = 3;

    private const int DuplicateKeyIndex = 2601;
    private const int DuplicateKeyConstraint = 2627;

    private readonly ISqlConnectionFactory _connectionFactory;

    public SqlOrderRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<OrderResult> CreateOrderAsync(NewOrder order, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(order);

        // Gộp các dòng trùng biến thể và sắp xếp cố định để hạn chế deadlock khi trừ kho
        IReadOnlyList<NewOrderItem> items = order.Items
            .Where(item => item.ProductVariantId > 0 && item.Quantity > 0)
            .GroupBy(item => item.ProductVariantId)
            .Select(group => new NewOrderItem
            {
                ProductVariantId = group.Key,
                Quantity = group.Sum(item => item.Quantity)
            })
            .OrderBy(item => item.ProductVariantId)
            .ToList();

        if (items.Count == 0)
        {
            return OrderResult.Fail("Giỏ hàng đang trống, chưa có sản phẩm nào để đặt.");
        }

        if (!PaymentMethods.IsSupported(order.PaymentMethod))
        {
            return OrderResult.Fail("Phương thức thanh toán không hợp lệ.");
        }

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        for (int attempt = 1; attempt <= MaxOrderCodeAttempts; attempt++)
        {
            await using SqlTransaction transaction =
                (SqlTransaction)await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            try
            {
                OrderResult result = await InsertOrderGraphAsync(connection, transaction, order, items, cancellationToken);

                if (!result.Success)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return result;
                }

                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch (SqlException ex) when (IsDuplicateOrderCode(ex) && attempt < MaxOrderCodeAttempts)
            {
                // Trùng mã đơn: rollback và sinh mã mới rồi thử lại
                await SafeRollbackAsync(transaction);
            }
            catch (SqlException ex) when (IsDuplicateOrderCode(ex))
            {
                await SafeRollbackAsync(transaction);
                return OrderResult.Fail("Hệ thống đang bận, chưa tạo được mã đơn. Vui lòng thử đặt hàng lại.");
            }
            catch
            {
                await SafeRollbackAsync(transaction);
                throw;
            }
        }

        return OrderResult.Fail("Không tạo được đơn hàng, vui lòng thử lại sau.");
    }

    /// <summary>Ghi toàn bộ đơn trong transaction hiện tại; trả về Fail nếu không đủ hàng.</summary>
    private static async Task<OrderResult> InsertOrderGraphAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        NewOrder order,
        IReadOnlyList<NewOrderItem> items,
        CancellationToken cancellationToken)
    {
        int customerId = await GetOrCreateCustomerAsync(connection, transaction, order, cancellationToken);

        (int orderId, string orderCode) = await InsertOrderAsync(
            connection, transaction, order, customerId, cancellationToken);

        decimal totalAmount = 0m;

        foreach (NewOrderItem item in items)
        {
            VariantSnapshot? variant = await ReadVariantAsync(
                connection, transaction, item.ProductVariantId, cancellationToken);

            if (variant is null)
            {
                return OrderResult.Fail(
                    "Có sản phẩm trong giỏ đã ngừng bán. Vui lòng xóa sản phẩm đó khỏi giỏ và đặt lại.");
            }

            int affectedRows = await TryDeductStockAsync(
                connection, transaction, item.ProductVariantId, item.Quantity, cancellationToken);

            // Câu UPDATE chỉ chạy khi còn đủ hàng -> 0 dòng nghĩa là vừa bị người khác mua hết
            if (affectedRows == 0)
            {
                return OrderResult.Fail(
                    $"Sản phẩm \"{variant.ProductName}\" (size {variant.Size}, màu {variant.Color}) " +
                    "không đủ số lượng bạn đặt. Vui lòng giảm số lượng hoặc chọn size/màu khác.");
            }

            decimal lineTotal = variant.Price * item.Quantity;
            await InsertOrderDetailAsync(
                connection, transaction, orderId, item.ProductVariantId, variant,
                item.Quantity, lineTotal, cancellationToken);

            totalAmount += lineTotal;
        }

        await UpdateOrderTotalAsync(connection, transaction, orderId, totalAmount, cancellationToken);

        return OrderResult.Ok(orderCode, totalAmount);
    }

    /// <summary>
    /// Khách quen (trùng số điện thoại) thì dùng lại hồ sơ cũ và cập nhật thông tin mới nhất;
    /// khách mới thì tạo bản ghi trong bảng Customers.
    /// </summary>
    private static async Task<int> GetOrCreateCustomerAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        NewOrder order,
        CancellationToken cancellationToken)
    {
        const string selectSql = """
            SELECT TOP (1) CustomerId
            FROM Customers
            WHERE PhoneNumber = @phone
            ORDER BY CustomerId DESC
            """;

        await using (SqlCommand select = CreateCommand(selectSql, connection, transaction))
        {
            select.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = order.PhoneNumber;

            object? existing = await select.ExecuteScalarAsync(cancellationToken);
            if (existing is not null && existing is not DBNull)
            {
                int customerId = Convert.ToInt32(existing);

                const string updateSql = """
                    UPDATE Customers
                    SET FullName = @fullName,
                        Email    = COALESCE(@email, Email),
                        Address  = COALESCE(@address, Address)
                    WHERE CustomerId = @customerId
                    """;

                await using SqlCommand update = CreateCommand(updateSql, connection, transaction);
                update.Parameters.Add("@fullName", SqlDbType.NVarChar, 100).Value = order.CustomerName;
                update.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = order.Address;
                AddNullableText(update, "@email", 100, order.Email);
                update.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;

                await update.ExecuteNonQueryAsync(cancellationToken);
                return customerId;
            }
        }

        const string insertSql = """
            INSERT INTO Customers (FullName, PhoneNumber, Email, Address)
            OUTPUT INSERTED.CustomerId
            VALUES (@fullName, @phone, @email, @address)
            """;

        await using SqlCommand insert = CreateCommand(insertSql, connection, transaction);
        insert.Parameters.Add("@fullName", SqlDbType.NVarChar, 100).Value = order.CustomerName;
        insert.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = order.PhoneNumber;
        insert.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = order.Address;
        AddNullableText(insert, "@email", 100, order.Email);

        object? inserted = await insert.ExecuteScalarAsync(cancellationToken);
        return inserted is null or DBNull ? 0 : Convert.ToInt32(inserted);
    }

    /// <summary>Tạo dòng Orders và trả về (OrderId, OrderCode).</summary>
    private static async Task<(int OrderId, string OrderCode)> InsertOrderAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        NewOrder order,
        int customerId,
        CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO Orders
                (OrderCode, CustomerId, CustomerName, CustomerPhone, ShippingAddress,
                 Note, TotalAmount, Status, PaymentMethod, IsPaid)
            OUTPUT INSERTED.OrderId
            VALUES
                (@orderCode, @customerId, @customerName, @phone, @address,
                 @note, 0, @status, @paymentMethod, 0)
            """;

        string orderCode = GenerateOrderCode();

        await using SqlCommand command = CreateCommand(sql, connection, transaction);
        command.Parameters.Add("@orderCode", SqlDbType.NVarChar, 30).Value = orderCode;
        command.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;
        command.Parameters.Add("@customerName", SqlDbType.NVarChar, 100).Value = order.CustomerName;
        command.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = order.PhoneNumber;
        command.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = order.Address;
        command.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = OrderStatuses.Pending;
        command.Parameters.Add("@paymentMethod", SqlDbType.NVarChar, 50).Value = order.PaymentMethod;
        AddNullableText(command, "@note", 500, order.Note);

        object? inserted = await command.ExecuteScalarAsync(cancellationToken);
        int orderId = inserted is null or DBNull ? 0 : Convert.ToInt32(inserted);

        return (orderId, orderCode);
    }

    /// <summary>
    /// Mã đơn dạng DH + yyMMddHHmmss + 3 số ngẫu nhiên (tối đa 17 ký tự, cột cho phép 30).
    /// UNIQUE(OrderCode) là chốt chặn cuối; nếu trùng, CreateOrderAsync tự sinh mã khác.
    /// </summary>
    private static string GenerateOrderCode()
        => $"DH{DateTime.Now:yyMMddHHmmss}{Random.Shared.Next(100, 1000)}";

    /// <summary>Đọc tên/giá thật của biến thể; trả về null nếu sản phẩm đã ngừng bán.</summary>
    private static async Task<VariantSnapshot?> ReadVariantAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        int productVariantId,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT v.ProductId, p.Name AS ProductName, v.Size, v.Color, p.Price
            FROM ProductVariants v
            INNER JOIN Products p ON p.ProductId = v.ProductId
            INNER JOIN Categories c ON c.CategoryId = p.CategoryId
            WHERE v.ProductVariantId = @variantId
              AND p.IsActive = 1
              AND c.IsActive = 1
            """;

        await using SqlCommand command = CreateCommand(sql, connection, transaction);
        command.Parameters.Add("@variantId", SqlDbType.Int).Value = productVariantId;

        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new VariantSnapshot(
            ProductId: reader.GetInt32(reader.GetOrdinal("ProductId")),
            ProductName: reader.GetString(reader.GetOrdinal("ProductName")),
            Size: reader.GetString(reader.GetOrdinal("Size")),
            Color: reader.GetString(reader.GetOrdinal("Color")),
            Price: reader.GetDecimal(reader.GetOrdinal("Price")));
    }

    /// <summary>
    /// Trừ kho có điều kiện: câu UPDATE chỉ tác động khi tồn kho còn đủ.
    /// Trả về số dòng bị ảnh hưởng (0 = không đủ hàng hoặc biến thể đã bị xóa).
    /// </summary>
    private static async Task<int> TryDeductStockAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        int productVariantId,
        int quantity,
        CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE ProductVariants
            SET StockQuantity = StockQuantity - @quantity
            WHERE ProductVariantId = @variantId
              AND StockQuantity >= @quantity
            """;

        await using SqlCommand command = CreateCommand(sql, connection, transaction);
        command.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
        command.Parameters.Add("@variantId", SqlDbType.Int).Value = productVariantId;

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task InsertOrderDetailAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        int orderId,
        int productVariantId,
        VariantSnapshot variant,
        int quantity,
        decimal lineTotal,
        CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO OrderDetails
                (OrderId, ProductId, ProductVariantId, ProductName, Size, Color,
                 Quantity, UnitPrice, TotalPrice)
            VALUES
                (@orderId, @productId, @variantId, @productName, @size, @color,
                 @quantity, @unitPrice, @totalPrice)
            """;

        await using SqlCommand command = CreateCommand(sql, connection, transaction);
        command.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;
        command.Parameters.Add("@productId", SqlDbType.Int).Value = variant.ProductId;
        command.Parameters.Add("@variantId", SqlDbType.Int).Value = productVariantId;
        command.Parameters.Add("@productName", SqlDbType.NVarChar, 200).Value = variant.ProductName;
        command.Parameters.Add("@size", SqlDbType.NVarChar, 20).Value = variant.Size;
        command.Parameters.Add("@color", SqlDbType.NVarChar, 50).Value = variant.Color;
        command.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;

        SqlParameter unitPrice = command.Parameters.Add("@unitPrice", SqlDbType.Decimal);
        unitPrice.Precision = 18;
        unitPrice.Scale = 2;
        unitPrice.Value = variant.Price;

        SqlParameter totalPrice = command.Parameters.Add("@totalPrice", SqlDbType.Decimal);
        totalPrice.Precision = 18;
        totalPrice.Scale = 2;
        totalPrice.Value = lineTotal;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task UpdateOrderTotalAsync(
        SqlConnection connection,
        SqlTransaction transaction,
        int orderId,
        decimal totalAmount,
        CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE Orders
            SET TotalAmount = @totalAmount
            WHERE OrderId = @orderId
            """;

        await using SqlCommand command = CreateCommand(sql, connection, transaction);
        SqlParameter total = command.Parameters.Add("@totalAmount", SqlDbType.Decimal);
        total.Precision = 18;
        total.Scale = 2;
        total.Value = totalAmount;
        command.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static SqlCommand CreateCommand(string sql, SqlConnection connection, SqlTransaction transaction)
        => new(sql, connection, transaction);

    private static void AddNullableText(SqlCommand command, string name, int size, string? value)
    {
        SqlParameter parameter = command.Parameters.Add(name, SqlDbType.NVarChar, size);
        parameter.Value = string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();
    }

    /// <summary>Lỗi 2601/2627 = vi phạm UNIQUE (ở đây là trùng OrderCode).</summary>
    private static bool IsDuplicateOrderCode(SqlException exception)
        => exception.Number is DuplicateKeyIndex or DuplicateKeyConstraint;

    private static async Task SafeRollbackAsync(SqlTransaction transaction)
    {
        try
        {
            await transaction.RollbackAsync(CancellationToken.None);
        }
        catch (InvalidOperationException)
        {
            // Transaction đã kết thúc trước đó - không cần rollback nữa
        }
    }

    /// <summary>Ảnh chụp dữ liệu biến thể tại thời điểm đặt hàng (giá lấy từ CSDL).</summary>
    private sealed record VariantSnapshot(
        int ProductId,
        string ProductName,
        string Size,
        string Color,
        decimal Price);
}
