using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

/// <summary>
/// Màn hình lập và theo dõi phiếu nhập kho.
/// Mỗi phiếu ghi nhận: nhập của nhà cung cấp nào, ai lập, từng biến thể size/màu
/// với số lượng và giá nhập bao nhiêu; tồn kho được cộng ngay khi lưu phiếu.
/// </summary>
public partial class FormNhapKho : Form
{
    /// <summary>
    /// Một dòng hàng đang chờ lưu vào phiếu (chưa ghi xuống CSDL).
    /// </summary>
    private sealed class PendingLine
    {
        public int ProductVariantId { get; init; }
        public string ProductName { get; init; } = "";
        public string Size { get; init; } = "";
        public string Color { get; init; } = "";
        public int Quantity { get; init; }
        public decimal ImportPrice { get; init; }
        public decimal TotalPrice => Quantity * ImportPrice;
    }

    /// <summary>Số lần thử lại khi mã phiếu bị trùng (UNIQUE ReceiptCode).</summary>
    private const int MaxReceiptCodeAttempts = 3;

    private readonly int _userId;
    private readonly string _roleName;
    private readonly List<PendingLine> _pendingLines = new();
    private string connectionString => DatabaseConfig.ConnectionString;
    private int _selectedReceiptId = -1;

    private bool IsAdmin =>
        string.Equals(_roleName, "Admin", StringComparison.OrdinalIgnoreCase);

    public FormNhapKho(int userId, string roleName)
    {
        _userId = userId;
        _roleName = roleName;
        InitializeComponent();

        lblSubtitle.Text =
            $"Người lập: {_roleName} — mỗi phiếu nhập cộng tồn kho ngay khi lưu.";
    }

    private void FormNhapKho_Load(object sender, EventArgs e)
    {
        LoadProducts();
        LoadReceipts();
        RenderPendingLines();
    }

    /// <summary>
    /// Nạp danh sách sản phẩm đang bán vào ComboBox chọn hàng nhập.
    /// </summary>
    private void LoadProducts()
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            const string sql = @"
                SELECT ProductId, ProductCode + N' - ' + Name AS DisplayName
                FROM Products
                WHERE IsActive = 1
                ORDER BY Name";

            using SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            adapter.Fill(table);

            cboSanPham.DataSource = table;
            cboSanPham.DisplayMember = "DisplayName";
            cboSanPham.ValueMember = "ProductId";
            cboSanPham.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải danh sách sản phẩm: " + ex.Message,
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Nạp các biến thể size/màu của sản phẩm đang chọn, kèm tồn kho hiện tại
    /// để người lập phiếu biết đang có bao nhiêu trước khi nhập thêm.
    /// </summary>
    private void LoadVariants(int productId)
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            const string sql = @"
                SELECT v.ProductVariantId,
                       p.Name AS ProductName,
                       v.Size,
                       v.Color,
                       v.StockQuantity,
                       v.Size + N' / ' + v.Color + N' (tồn ' +
                           CAST(v.StockQuantity AS NVARCHAR(10)) + N')' AS HienThi
                FROM ProductVariants v
                INNER JOIN Products p ON p.ProductId = v.ProductId
                WHERE v.ProductId = @productId
                ORDER BY v.Size, v.Color";

            using SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.Add("@productId", SqlDbType.Int).Value = productId;

            DataTable table = new DataTable();
            adapter.Fill(table);

            cboBienThe.DataSource = table;
            cboBienThe.DisplayMember = "HienThi";
            cboBienThe.ValueMember = "ProductVariantId";
            cboBienThe.SelectedIndex = table.Rows.Count > 0 ? 0 : -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải biến thể của sản phẩm: " + ex.Message,
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSanPham.SelectedValue is int productId && productId > 0)
        {
            LoadVariants(productId);
            return;
        }

        cboBienThe.DataSource = null;
    }

    /// <summary>
    /// Tải danh sách phiếu nhập, lọc theo mã phiếu hoặc nhà cung cấp.
    /// </summary>
    private void LoadReceipts(string keyword = "")
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            const string sql = @"
                SELECT r.StockReceiptId,
                       r.ReceiptCode AS [Mã phiếu],
                       r.ReceiptDate AS [Ngày nhập],
                       ISNULL(r.SupplierName, N'—') AS [Nhà cung cấp],
                       r.TotalQuantity AS [Số lượng],
                       r.TotalAmount AS [Tổng tiền],
                       r.Status AS [Trạng thái],
                       ISNULL(u.FullName, N'—') AS [Người lập]
                FROM StockReceipts r
                LEFT JOIN Users u ON u.UserId = r.CreatedByUserId
                WHERE (@keyword = N''
                       OR r.ReceiptCode LIKE @keyword
                       OR r.SupplierName LIKE @keyword)
                ORDER BY r.ReceiptDate DESC, r.StockReceiptId DESC";

            using SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.Add("@keyword", SqlDbType.NVarChar, 220).Value =
                string.IsNullOrWhiteSpace(keyword) ? "" : "%" + keyword.Trim() + "%";

            DataTable table = new DataTable();
            adapter.Fill(table);

            dgvPhieu.DataSource = table;

            if (dgvPhieu.Columns["StockReceiptId"] != null)
            {
                dgvPhieu.Columns["StockReceiptId"].Visible = false;
            }

            if (dgvPhieu.Columns["Ngày nhập"] != null)
            {
                dgvPhieu.Columns["Ngày nhập"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            if (dgvPhieu.Columns["Tổng tiền"] != null)
            {
                dgvPhieu.Columns["Tổng tiền"].DefaultCellStyle.Format = "N0";
                dgvPhieu.Columns["Tổng tiền"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvPhieu.Columns["Số lượng"] != null)
            {
                dgvPhieu.Columns["Số lượng"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            dgvPhieu.ClearSelection();
            _selectedReceiptId = -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải danh sách phiếu nhập: " + ex.Message +
                "\n\nNếu bảng chưa tồn tại, hãy chạy migration 03_stock_receipts.sql.",
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Tải các dòng chi tiết của phiếu đang chọn.
    /// </summary>
    private void LoadReceiptDetail(int receiptId)
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            const string sql = @"
                SELECT d.ProductName AS [Sản phẩm],
                       d.Size AS [Size],
                       d.Color AS [Màu],
                       d.Quantity AS [Số lượng],
                       d.ImportPrice AS [Giá nhập],
                       d.TotalPrice AS [Thành tiền]
                FROM StockReceiptDetails d
                WHERE d.StockReceiptId = @receiptId
                ORDER BY d.Size, d.Color";

            using SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.Add("@receiptId", SqlDbType.Int).Value = receiptId;

            DataTable table = new DataTable();
            adapter.Fill(table);

            dgvChiTiet.DataSource = table;

            foreach (string moneyColumn in new[] { "Giá nhập", "Thành tiền" })
            {
                if (dgvChiTiet.Columns[moneyColumn] != null)
                {
                    dgvChiTiet.Columns[moneyColumn].DefaultCellStyle.Format = "N0";
                    dgvChiTiet.Columns[moneyColumn].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dgvChiTiet.Columns["Số lượng"] != null)
            {
                dgvChiTiet.Columns["Số lượng"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải chi tiết phiếu: " + ex.Message,
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void dgvPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        DataGridViewRow row = dgvPhieu.Rows[e.RowIndex];
        if (row.Cells["StockReceiptId"].Value is null)
        {
            return;
        }

        _selectedReceiptId = Convert.ToInt32(row.Cells["StockReceiptId"].Value);
        LoadReceiptDetail(_selectedReceiptId);
    }

    /// <summary>
    /// Vẽ lại lưới các dòng chờ lưu và tính tổng số lượng / tổng tiền.
    /// </summary>
    private void RenderPendingLines()
    {
        DataTable table = new DataTable();
        table.Columns.Add("ProductVariantId", typeof(int));
        table.Columns.Add("Sản phẩm", typeof(string));
        table.Columns.Add("Size", typeof(string));
        table.Columns.Add("Màu", typeof(string));
        table.Columns.Add("Số lượng", typeof(int));
        table.Columns.Add("Giá nhập", typeof(decimal));
        table.Columns.Add("Thành tiền", typeof(decimal));

        foreach (PendingLine line in _pendingLines)
        {
            table.Rows.Add(
                line.ProductVariantId,
                line.ProductName,
                line.Size,
                line.Color,
                line.Quantity,
                line.ImportPrice,
                line.TotalPrice);
        }

        dgvDongNhap.DataSource = table;

        if (dgvDongNhap.Columns["ProductVariantId"] != null)
        {
            dgvDongNhap.Columns["ProductVariantId"].Visible = false;
        }

        foreach (string moneyColumn in new[] { "Giá nhập", "Thành tiền" })
        {
            if (dgvDongNhap.Columns[moneyColumn] != null)
            {
                dgvDongNhap.Columns[moneyColumn].DefaultCellStyle.Format = "N0";
                dgvDongNhap.Columns[moneyColumn].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }

        if (dgvDongNhap.Columns["Số lượng"] != null)
        {
            dgvDongNhap.Columns["Số lượng"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
        }

        dgvDongNhap.ClearSelection();

        int totalQuantity = _pendingLines.Sum(line => line.Quantity);
        decimal totalAmount = _pendingLines.Sum(line => line.TotalPrice);
        lblTongKet.Text = $"Tổng cộng: {totalQuantity:N0} món — {totalAmount:N0} đ";
    }

    /// <summary>
    /// Thêm biến thể đang chọn vào danh sách chờ lưu.
    /// </summary>
    private void btnThemDong_Click(object sender, EventArgs e)
    {
        if (cboBienThe.SelectedItem is not DataRowView row)
        {
            MessageBox.Show(
                "Vui lòng chọn sản phẩm và biến thể size/màu cần nhập.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtGiaNhap.Text.Trim(), out decimal importPrice) || importPrice <= 0)
        {
            MessageBox.Show(
                "Giá nhập phải là số lớn hơn 0 (ví dụ: 113000).",
                "Giá nhập không hợp lệ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        int variantId = Convert.ToInt32(row["ProductVariantId"]);

        if (_pendingLines.Any(line => line.ProductVariantId == variantId))
        {
            MessageBox.Show(
                "Biến thể này đã có trong phiếu. Hãy xóa dòng cũ nếu muốn nhập số lượng khác.",
                "Dòng bị trùng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        _pendingLines.Add(new PendingLine
        {
            ProductVariantId = variantId,
            ProductName = row["ProductName"].ToString() ?? "",
            Size = row["Size"].ToString() ?? "",
            Color = row["Color"].ToString() ?? "",
            Quantity = Convert.ToInt32(nudSoLuong.Value),
            ImportPrice = importPrice
        });

        txtGiaNhap.Clear();
        nudSoLuong.Value = 1;
        RenderPendingLines();
    }

    /// <summary>
    /// Bỏ dòng đang chọn khỏi phiếu nháp.
    /// </summary>
    private void btnXoaDong_Click(object sender, EventArgs e)
    {
        if (dgvDongNhap.CurrentRow is null)
        {
            MessageBox.Show(
                "Vui lòng chọn một dòng trong danh sách để xóa.",
                "Chưa chọn dòng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        int index = dgvDongNhap.CurrentRow.Index;
        if (index < 0 || index >= _pendingLines.Count)
        {
            return;
        }

        _pendingLines.RemoveAt(index);
        RenderPendingLines();
    }

    /// <summary>
    /// Ghi phiếu nhập xuống CSDL trong một transaction:
    /// đầu phiếu, các dòng chi tiết, cộng tồn kho, cập nhật tổng tiền.
    /// </summary>
    private void btnLuuPhieu_Click(object sender, EventArgs e)
    {
        if (_pendingLines.Count == 0)
        {
            MessageBox.Show(
                "Phiếu nhập phải có ít nhất một dòng hàng.",
                "Phiếu rỗng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string supplierName = txtNhaCungCap.Text.Trim();
        string supplierPhone = txtDienThoai.Text.Trim();
        string note = txtGhiChu.Text.Trim();

        if (supplierName.Length > 150 || supplierPhone.Length > 20 || note.Length > 500)
        {
            MessageBox.Show(
                "Nhà cung cấp tối đa 150 ký tự, điện thoại 20 ký tự, ghi chú 500 ký tự.",
                "Thông tin quá dài",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        int totalQuantity = _pendingLines.Sum(line => line.Quantity);

        for (int attempt = 1; attempt <= MaxReceiptCodeAttempts; attempt++)
        {
            try
            {
                int receiptId = SaveReceipt(supplierName, supplierPhone, note);

                MessageBox.Show(
                    $"Đã lưu phiếu nhập kho thành công.\nĐã cộng {totalQuantity:N0} sản phẩm vào tồn kho.",
                    "Hoàn tất",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearDraft();
                LoadReceipts();
                SelectReceipt(receiptId);

                if (cboSanPham.SelectedValue is int productId && productId > 0)
                {
                    LoadVariants(productId);
                }

                return;
            }
            catch (SqlException ex) when (IsDuplicateReceiptCode(ex) && attempt < MaxReceiptCodeAttempts)
            {
                // Trùng mã phiếu (UNIQUE ReceiptCode) → sinh mã khác rồi thử lại.
            }
            catch (SqlException ex) when (IsDuplicateReceiptCode(ex))
            {
                MessageBox.Show(
                    "Không sinh được mã phiếu duy nhất sau nhiều lần thử. Vui lòng lưu lại.",
                    "Trùng mã phiếu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể lưu phiếu nhập, toàn bộ thay đổi đã được hoàn tác.\nChi tiết: " + ex.Message,
                    "Lỗi lưu phiếu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }
    }

    /// <summary>
    /// Ghi một phiếu nhập hoàn chỉnh; lỗi ở bất kỳ bước nào đều rollback toàn bộ.
    /// </summary>
    private int SaveReceipt(string supplierName, string supplierPhone, string note)
    {
        using SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        using SqlTransaction transaction = conn.BeginTransaction();

        try
        {
            int receiptId;

            const string insertReceipt = @"
                INSERT INTO StockReceipts
                    (ReceiptCode, ReceiptDate, SupplierName, SupplierPhone, Note,
                     Status, CreatedByUserId, CreatedAt)
                VALUES
                    (@receiptCode, GETDATE(), @supplierName, @supplierPhone, @note,
                     N'Đã nhập', @userId, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand command = new SqlCommand(insertReceipt, conn, transaction))
            {
                command.Parameters.Add("@receiptCode", SqlDbType.NVarChar, 30).Value = GenerateReceiptCode();
                command.Parameters.Add("@supplierName", SqlDbType.NVarChar, 150).Value = ToDbText(supplierName);
                command.Parameters.Add("@supplierPhone", SqlDbType.NVarChar, 20).Value = ToDbText(supplierPhone);
                command.Parameters.Add("@note", SqlDbType.NVarChar, 500).Value = ToDbText(note);
                command.Parameters.Add("@userId", SqlDbType.Int).Value = _userId;

                receiptId = Convert.ToInt32(command.ExecuteScalar());
            }

            const string insertDetail = @"
                INSERT INTO StockReceiptDetails
                    (StockReceiptId, ProductVariantId, ProductName, Size, Color,
                     Quantity, ImportPrice, TotalPrice)
                VALUES
                    (@receiptId, @variantId, @productName, @size, @color,
                     @quantity, @importPrice, @totalPrice);";

            const string addStock = @"
                UPDATE ProductVariants
                SET StockQuantity = StockQuantity + @quantity
                WHERE ProductVariantId = @variantId;";

            foreach (PendingLine line in _pendingLines)
            {
                using (SqlCommand command = new SqlCommand(insertDetail, conn, transaction))
                {
                    command.Parameters.Add("@receiptId", SqlDbType.Int).Value = receiptId;
                    command.Parameters.Add("@variantId", SqlDbType.Int).Value = line.ProductVariantId;
                    command.Parameters.Add("@productName", SqlDbType.NVarChar, 200).Value = line.ProductName;
                    command.Parameters.Add("@size", SqlDbType.NVarChar, 20).Value = ToDbText(line.Size);
                    command.Parameters.Add("@color", SqlDbType.NVarChar, 50).Value = ToDbText(line.Color);
                    command.Parameters.Add("@quantity", SqlDbType.Int).Value = line.Quantity;
                    command.Parameters.Add("@importPrice", SqlDbType.Decimal).Value = line.ImportPrice;
                    command.Parameters.Add("@totalPrice", SqlDbType.Decimal).Value = line.TotalPrice;

                    // Số lẻ thập phân của tiền tệ: DECIMAL(18,2)
                    command.Parameters["@importPrice"].Precision = 18;
                    command.Parameters["@importPrice"].Scale = 2;
                    command.Parameters["@totalPrice"].Precision = 18;
                    command.Parameters["@totalPrice"].Scale = 2;

                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(addStock, conn, transaction))
                {
                    command.Parameters.Add("@quantity", SqlDbType.Int).Value = line.Quantity;
                    command.Parameters.Add("@variantId", SqlDbType.Int).Value = line.ProductVariantId;

                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException(
                            "Biến thể đã bị xóa khỏi hệ thống nên không thể nhập kho.");
                    }
                }
            }

            const string updateTotals = @"
                UPDATE StockReceipts
                SET TotalQuantity = (SELECT ISNULL(SUM(Quantity), 0)
                                     FROM StockReceiptDetails
                                     WHERE StockReceiptId = @receiptId),
                    TotalAmount   = (SELECT ISNULL(SUM(TotalPrice), 0)
                                     FROM StockReceiptDetails
                                     WHERE StockReceiptId = @receiptId)
                WHERE StockReceiptId = @receiptId;";

            using (SqlCommand command = new SqlCommand(updateTotals, conn, transaction))
            {
                command.Parameters.Add("@receiptId", SqlDbType.Int).Value = receiptId;
                command.ExecuteNonQuery();
            }

            transaction.Commit();
            return receiptId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Hủy phiếu nhập: trừ lại đúng số lượng đã nhập, chỉ khi tồn kho còn đủ.
    /// </summary>
    private void btnHuyPhieu_Click(object sender, EventArgs e)
    {
        if (!IsAdmin)
        {
            MessageBox.Show(
                "Chỉ quản trị viên được hủy phiếu nhập kho.",
                "Không đủ quyền",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (_selectedReceiptId <= 0)
        {
            MessageBox.Show(
                "Vui lòng chọn một phiếu trong danh sách trước khi hủy.",
                "Chưa chọn phiếu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string receiptCode = GetSelectedReceiptCode(out string status);

        if (status == "Đã hủy")
        {
            MessageBox.Show(
                $"Phiếu {receiptCode} đã được hủy trước đó.",
                "Không có thay đổi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        DialogResult confirm = MessageBox.Show(
            $"Hủy phiếu {receiptCode}?\nTồn kho sẽ bị trừ lại đúng số lượng đã nhập trong phiếu này.",
            "Xác nhận hủy phiếu",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
        {
            return;
        }

        try
        {
            CancelReceipt(_selectedReceiptId);

            MessageBox.Show(
                $"Đã hủy phiếu {receiptCode} và trừ lại tồn kho tương ứng.",
                "Hoàn tất",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LoadReceipts();
            if (cboSanPham.SelectedValue is int productId && productId > 0)
            {
                LoadVariants(productId);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể hủy phiếu nhập, toàn bộ thay đổi đã được hoàn tác.\nChi tiết: " + ex.Message,
                "Lỗi hủy phiếu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Trừ lại tồn kho và đánh dấu phiếu đã hủy trong một transaction.
    /// Không cho hủy nếu hàng của phiếu đã bán ra, tránh tồn kho bị âm.
    /// </summary>
    private void CancelReceipt(int receiptId)
    {
        using SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        using SqlTransaction transaction = conn.BeginTransaction();

        try
        {
            const string detailSql = @"
                SELECT d.ProductName, d.Size, d.Color, d.Quantity,
                       v.StockQuantity, d.ProductVariantId
                FROM StockReceiptDetails d
                INNER JOIN ProductVariants v ON v.ProductVariantId = d.ProductVariantId
                WHERE d.StockReceiptId = @receiptId";

            List<(int VariantId, int Quantity, string Description, int Stock)> details = new();

            using (SqlCommand command = new SqlCommand(detailSql, conn, transaction))
            {
                command.Parameters.Add("@receiptId", SqlDbType.Int).Value = receiptId;

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string description =
                        $"{reader["ProductName"]} ({reader["Size"]}/{reader["Color"]})";

                    details.Add((
                        Convert.ToInt32(reader["ProductVariantId"]),
                        Convert.ToInt32(reader["Quantity"]),
                        description,
                        Convert.ToInt32(reader["StockQuantity"])));
                }
            }

            const string deductStock = @"
                UPDATE ProductVariants
                SET StockQuantity = StockQuantity - @quantity
                WHERE ProductVariantId = @variantId
                  AND StockQuantity >= @quantity;";

            foreach ((int variantId, int quantity, string description, int stock) in details)
            {
                if (stock < quantity)
                {
                    throw new InvalidOperationException(
                        $"Không thể hủy: {description} chỉ còn {stock} trong kho " +
                        $"nhưng phiếu đã nhập {quantity} (hàng đã bán ra).");
                }

                using SqlCommand command = new SqlCommand(deductStock, conn, transaction);
                command.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                command.Parameters.Add("@variantId", SqlDbType.Int).Value = variantId;

                if (command.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException(
                        $"Không thể trừ tồn kho của {description} — dữ liệu vừa thay đổi, vui lòng thử lại.");
                }
            }

            const string markCancelled = @"
                UPDATE StockReceipts
                SET Status = N'Đã hủy'
                WHERE StockReceiptId = @receiptId
                  AND Status = N'Đã nhập';";

            using (SqlCommand command = new SqlCommand(markCancelled, conn, transaction))
            {
                command.Parameters.Add("@receiptId", SqlDbType.Int).Value = receiptId;

                if (command.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException("Phiếu không còn ở trạng thái có thể hủy.");
                }
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private string GetSelectedReceiptCode(out string status)
    {
        status = "";

        if (dgvPhieu.CurrentRow is null)
        {
            return "";
        }

        status = dgvPhieu.CurrentRow.Cells["Trạng thái"].Value?.ToString() ?? "";
        return dgvPhieu.CurrentRow.Cells["Mã phiếu"].Value?.ToString() ?? "";
    }

    /// <summary>
    /// Chọn lại đúng dòng phiếu vừa lưu để người dùng thấy ngay chi tiết.
    /// </summary>
    private void SelectReceipt(int receiptId)
    {
        _selectedReceiptId = receiptId;

        foreach (DataGridViewRow row in dgvPhieu.Rows)
        {
            if (row.Cells["StockReceiptId"].Value is null)
            {
                continue;
            }

            if (Convert.ToInt32(row.Cells["StockReceiptId"].Value) == receiptId)
            {
                row.Selected = true;
                if (row.Cells["Mã phiếu"] is DataGridViewCell cell)
                {
                    dgvPhieu.CurrentCell = cell;
                }

                break;
            }
        }

        LoadReceiptDetail(receiptId);
    }

    private void btnTimKiem_Click(object sender, EventArgs e)
    {
        LoadReceipts(txtTimKiem.Text);
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        txtTimKiem.Clear();
        LoadReceipts();
        ClearDraft();

        if (cboSanPham.SelectedValue is int productId && productId > 0)
        {
            LoadVariants(productId);
        }
    }

    private void ClearDraft()
    {
        _pendingLines.Clear();
        txtNhaCungCap.Clear();
        txtDienThoai.Clear();
        txtGhiChu.Clear();
        txtGiaNhap.Clear();
        nudSoLuong.Value = 1;
        dgvChiTiet.DataSource = null;
        RenderPendingLines();
    }

    /// <summary>
    /// Mã phiếu dạng NK + yyMMddHHmmss + 3 số (tối đa 17 ký tự, cột cho phép 30).
    /// UNIQUE(ReceiptCode) là chốt chặn cuối; nếu trùng, SaveReceipt được gọi lại.
    /// </summary>
    private static string GenerateReceiptCode()
        => $"NK{DateTime.Now:yyMMddHHmmss}{Random.Shared.Next(100, 1000)}";

    /// <summary>Lỗi 2601/2627 = vi phạm UNIQUE (ở đây là trùng ReceiptCode).</summary>
    private static bool IsDuplicateReceiptCode(SqlException exception)
        => exception.Number is 2601 or 2627;

    private static object ToDbText(string value)
        => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
}
