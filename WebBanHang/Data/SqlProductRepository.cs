using System.Data;
using Microsoft.Data.SqlClient;
using WebBanHang.Models;

namespace WebBanHang.Data;

public sealed class SqlProductRepository : IProductRepository
{
    /// <summary>
    /// Câu SELECT dùng chung: kèm tên danh mục và tổng tồn kho.
    /// Tồn kho tính bằng truy vấn con (không phụ thuộc view) để web vẫn chạy
    /// trên database chưa chạy migration.
    /// </summary>
    private const string ProductSelect = """
        SELECT
            p.ProductId,
            p.ProductCode,
            p.Name,
            p.Description,
            p.Price,
            p.OriginalPrice,
            p.ImageUrl,
            p.CategoryId,
            c.Name AS CategoryName,
            c.ImageUrl AS CategoryImageUrl,
            p.IsFeatured,
            ISNULL((SELECT SUM(v.StockQuantity)
                    FROM ProductVariants v
                    WHERE v.ProductId = p.ProductId), 0) AS TotalStock
        FROM Products p
        INNER JOIN Categories c ON c.CategoryId = p.CategoryId
        """;

    private readonly ISqlConnectionFactory _connectionFactory;

    public SqlProductRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<Category>> GetActiveCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT CategoryId, Name, Description, ImageUrl, IsActive
            FROM Categories
            WHERE IsActive = 1
            ORDER BY Name
            """;

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using SqlCommand command = new(sql, connection);
        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        var categories = new List<Category>();
        while (await reader.ReadAsync(cancellationToken))
        {
            categories.Add(new Category
            {
                CategoryId = ReadInt(reader, "CategoryId"),
                Name = ReadText(reader, "Name"),
                Description = ReadNullableText(reader, "Description"),
                ImageUrl = ReadNullableText(reader, "ImageUrl"),
                IsActive = ReadFlag(reader, "IsActive")
            });
        }

        return categories;
    }

    public async Task<int> CountActiveProductsAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT COUNT(*)
            FROM Products p
            INNER JOIN Categories c ON c.CategoryId = p.CategoryId
            WHERE p.IsActive = 1 AND c.IsActive = 1
            """;

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using SqlCommand command = new(sql, connection);
        object? result = await command.ExecuteScalarAsync(cancellationToken);

        return result is null or DBNull ? 0 : Convert.ToInt32(result);
    }

    public async Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(
        int take = 8,
        CancellationToken cancellationToken = default)
    {
        const string sql = ProductSelect + """

            WHERE p.IsActive = 1 AND c.IsActive = 1 AND p.IsFeatured = 1
            ORDER BY p.CreatedAt DESC, p.ProductId DESC
            OFFSET 0 ROWS FETCH NEXT @take ROWS ONLY
            """;

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@take", SqlDbType.Int).Value = take <= 0 ? 8 : Math.Min(take, 50);

        return await ReadProductsAsync(command, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(
        string? keyword,
        int categoryId,
        CancellationToken cancellationToken = default)
    {
        // COLLATE Vietnamese_CI_AI: khách gõ không dấu ("ao") vẫn tìm được "Áo" —
        // collation mặc định của CSDL phân biệt dấu nên sẽ không khớp.
        const string sql = ProductSelect + """

            WHERE p.IsActive = 1
              AND c.IsActive = 1
              AND (@keyword = N''
                   OR p.Name COLLATE Vietnamese_CI_AI LIKE @keyword ESCAPE '\'
                   OR p.ProductCode COLLATE Vietnamese_CI_AI LIKE @keyword ESCAPE '\')
              AND (@categoryId = 0 OR p.CategoryId = @categoryId)
            ORDER BY p.IsFeatured DESC, p.Name
            """;

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@keyword", SqlDbType.NVarChar, 220).Value = ToLikePattern(keyword);
        command.Parameters.Add("@categoryId", SqlDbType.Int).Value = categoryId;

        return await ReadProductsAsync(command, cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        const string sql = ProductSelect + """

            WHERE p.ProductId = @productId AND p.IsActive = 1
            """;

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@productId", SqlDbType.Int).Value = productId;

        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        return await reader.ReadAsync(cancellationToken) ? MapProduct(reader) : null;
    }

    public async Task<IReadOnlyList<ProductVariant>> GetVariantsAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT ProductVariantId, ProductId, Size, Color, StockQuantity
            FROM ProductVariants
            WHERE ProductId = @productId
            ORDER BY Size, Color
            """;

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@productId", SqlDbType.Int).Value = productId;

        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        var variants = new List<ProductVariant>();
        while (await reader.ReadAsync(cancellationToken))
        {
            variants.Add(new ProductVariant
            {
                ProductVariantId = ReadInt(reader, "ProductVariantId"),
                ProductId = ReadInt(reader, "ProductId"),
                Size = ReadText(reader, "Size"),
                Color = ReadText(reader, "Color"),
                StockQuantity = ReadInt(reader, "StockQuantity")
            });
        }

        return variants;
    }

    public async Task<VariantInfo?> GetVariantInfoAsync(
        int productVariantId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                v.ProductVariantId,
                v.ProductId,
                p.Name AS ProductName,
                v.Size,
                v.Color,
                p.Price,
                p.ImageUrl,
                c.ImageUrl AS CategoryImageUrl,
                v.StockQuantity
            FROM ProductVariants v
            INNER JOIN Products p ON p.ProductId = v.ProductId
            INNER JOIN Categories c ON c.CategoryId = p.CategoryId
            WHERE v.ProductVariantId = @variantId
              AND p.IsActive = 1
              AND c.IsActive = 1
            """;

        await using SqlConnection connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@variantId", SqlDbType.Int).Value = productVariantId;

        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new VariantInfo
        {
            ProductVariantId = ReadInt(reader, "ProductVariantId"),
            ProductId = ReadInt(reader, "ProductId"),
            ProductName = ReadText(reader, "ProductName"),
            Size = ReadText(reader, "Size"),
            Color = ReadText(reader, "Color"),
            Price = ReadMoney(reader, "Price"),
            ImageUrl = ReadNullableText(reader, "ImageUrl"),
            CategoryImageUrl = ReadNullableText(reader, "CategoryImageUrl"),
            StockQuantity = ReadInt(reader, "StockQuantity")
        };
    }

    public async Task<Product?> GetProductWithVariantsAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        Product? product = await GetProductByIdAsync(productId, cancellationToken);
        if (product is null)
        {
            return null;
        }

        product.Variants = await GetVariantsAsync(productId, cancellationToken);
        return product;
    }

    private static async Task<IReadOnlyList<Product>> ReadProductsAsync(
        SqlCommand command,
        CancellationToken cancellationToken)
    {
        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        var products = new List<Product>();
        while (await reader.ReadAsync(cancellationToken))
        {
            products.Add(MapProduct(reader));
        }

        return products;
    }

    private static Product MapProduct(SqlDataReader reader) => new()
    {
        ProductId = ReadInt(reader, "ProductId"),
        ProductCode = ReadText(reader, "ProductCode"),
        Name = ReadText(reader, "Name"),
        Description = ReadNullableText(reader, "Description"),
        Price = ReadMoney(reader, "Price"),
        OriginalPrice = ReadNullableMoney(reader, "OriginalPrice"),
        ImageUrl = ReadNullableText(reader, "ImageUrl"),
        CategoryId = ReadInt(reader, "CategoryId"),
        CategoryName = ReadText(reader, "CategoryName"),
        CategoryImageUrl = ReadNullableText(reader, "CategoryImageUrl"),
        IsFeatured = ReadFlag(reader, "IsFeatured"),
        TotalStock = ReadInt(reader, "TotalStock")
    };

    /// <summary>
    /// Biến từ khóa người dùng thành mẫu LIKE an toàn:
    /// thoát các ký tự đại diện (% _ [ \) để khách gõ "%" không quét cả bảng.
    /// </summary>
    private static string ToLikePattern(string? keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return string.Empty;
        }

        string trimmed = keyword.Trim();
        if (trimmed.Length > 100)
        {
            trimmed = trimmed[..100];
        }

        string escaped = trimmed
            .Replace("\\", "\\\\")
            .Replace("%", "\\%")
            .Replace("_", "\\_")
            .Replace("[", "\\[");

        return $"%{escaped}%";
    }

    private static int ReadInt(SqlDataReader reader, string column)
    {
        int index = reader.GetOrdinal(column);
        return reader.IsDBNull(index) ? 0 : reader.GetInt32(index);
    }

    private static decimal ReadMoney(SqlDataReader reader, string column)
    {
        int index = reader.GetOrdinal(column);
        return reader.IsDBNull(index) ? 0m : reader.GetDecimal(index);
    }

    private static decimal? ReadNullableMoney(SqlDataReader reader, string column)
    {
        int index = reader.GetOrdinal(column);
        return reader.IsDBNull(index) ? null : reader.GetDecimal(index);
    }

    private static string ReadText(SqlDataReader reader, string column)
    {
        int index = reader.GetOrdinal(column);
        return reader.IsDBNull(index) ? string.Empty : reader.GetString(index);
    }

    private static string? ReadNullableText(SqlDataReader reader, string column)
    {
        int index = reader.GetOrdinal(column);
        return reader.IsDBNull(index) ? null : reader.GetString(index);
    }

    private static bool ReadFlag(SqlDataReader reader, string column)
    {
        int index = reader.GetOrdinal(column);
        return !reader.IsDBNull(index) && reader.GetBoolean(index);
    }
}
