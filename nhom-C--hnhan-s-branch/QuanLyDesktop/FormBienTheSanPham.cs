using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

public partial class FormBienTheSanPham : Form
{
    private readonly int _productId;
    private readonly string _productName;
    private string connectionString => DatabaseConfig.ConnectionString;
    private int _selectedVariantId = -1;

    public FormBienTheSanPham(int productId, string productName)
    {
        _productId = productId;
        _productName = productName;
        InitializeComponent();

        lblProductName.Text = $"Sản phẩm: {_productName}";
        cboSize.Items.AddRange(new object[] { "XS", "S", "M", "L", "XL", "XXL", "Free size" });
        cboSize.SelectedIndex = -1;

        LoadVariants();
    }

    /// <summary>
    /// Tải các biến thể size/màu của sản phẩm hiện tại và ẩn khóa chính nội bộ.
    /// </summary>
    private void LoadVariants()
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            const string sql = @"
                SELECT ProductVariantId, Size, Color, StockQuantity
                FROM ProductVariants
                WHERE ProductId = @productId
                ORDER BY Size, Color";

            using SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.Add("@productId", SqlDbType.Int).Value = _productId;

            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvVariants.DataSource = table;

            if (dgvVariants.Columns["ProductVariantId"] != null)
            {
                dgvVariants.Columns["ProductVariantId"].Visible = false;
            }

            if (dgvVariants.Columns["Size"] != null)
            {
                dgvVariants.Columns["Size"].HeaderText = "Size";
            }

            if (dgvVariants.Columns["Color"] != null)
            {
                dgvVariants.Columns["Color"].HeaderText = "Màu sắc";
            }

            if (dgvVariants.Columns["StockQuantity"] != null)
            {
                dgvVariants.Columns["StockQuantity"].HeaderText = "Tồn kho";
            }

            dgvVariants.ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải biến thể sản phẩm: " + ex.Message,
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Chọn một dòng để nạp size, màu và số lượng tồn lên các ô chỉnh sửa.
    /// </summary>
    private void dgvVariants_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        DataGridViewRow row = dgvVariants.Rows[e.RowIndex];
        if (row.Cells["ProductVariantId"].Value is null)
        {
            return;
        }

        _selectedVariantId = Convert.ToInt32(row.Cells["ProductVariantId"].Value);
        string size = row.Cells["Size"].Value?.ToString() ?? "";
        cboSize.SelectedItem = size;
        txtColor.Text = row.Cells["Color"].Value?.ToString() ?? "";
        nudStock.Value = Convert.ToInt32(row.Cells["StockQuantity"].Value);
    }

    /// <summary>
    /// Kiểm tra size/màu chưa tồn tại trong cùng sản phẩm để tránh biến thể trùng.
    /// </summary>
    private bool VariantAlreadyExists(SqlConnection conn, string size, string color, int excludedVariantId = 0)
    {
        const string sql = @"
            SELECT COUNT(1)
            FROM ProductVariants
            WHERE ProductId = @productId
              AND Size = @size
              AND Color = @color
              AND ProductVariantId <> @excludedVariantId";

        using SqlCommand command = new SqlCommand(sql, conn);
        command.Parameters.Add("@productId", SqlDbType.Int).Value = _productId;
        command.Parameters.Add("@size", SqlDbType.NVarChar, 20).Value = size;
        command.Parameters.Add("@color", SqlDbType.NVarChar, 50).Value = color;
        command.Parameters.Add("@excludedVariantId", SqlDbType.Int).Value = excludedVariantId;

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    /// <summary>
    /// Kiểm tra trường nhập và trả về size/màu đã chuẩn hóa.
    /// </summary>
    private bool TryReadVariantInput(out string size, out string color, out int stockQuantity)
    {
        size = cboSize.SelectedItem?.ToString() ?? "";
        color = txtColor.Text.Trim();
        stockQuantity = Convert.ToInt32(nudStock.Value);

        if (string.IsNullOrWhiteSpace(size) || string.IsNullOrWhiteSpace(color))
        {
            MessageBox.Show(
                "Vui lòng chọn size và nhập màu sắc.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        if (color.Length > 50)
        {
            MessageBox.Show(
                "Tên màu tối đa 50 ký tự.",
                "Thông tin quá dài",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Thêm một biến thể mới cho sản phẩm hiện tại.
    /// </summary>
    private void btnThem_Click(object sender, EventArgs e)
    {
        if (!TryReadVariantInput(out string size, out string color, out int stockQuantity))
        {
            return;
        }

        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            if (VariantAlreadyExists(conn, size, color))
            {
                MessageBox.Show(
                    "Size và màu này đã có trong sản phẩm.",
                    "Biến thể bị trùng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            const string sql = @"
                INSERT INTO ProductVariants (ProductId, Size, Color, StockQuantity)
                VALUES (@productId, @size, @color, @stockQuantity)";

            using SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@productId", SqlDbType.Int).Value = _productId;
            command.Parameters.Add("@size", SqlDbType.NVarChar, 20).Value = size;
            command.Parameters.Add("@color", SqlDbType.NVarChar, 50).Value = color;
            command.Parameters.Add("@stockQuantity", SqlDbType.Int).Value = stockQuantity;
            command.ExecuteNonQuery();

            MessageBox.Show(
                "Đã thêm biến thể sản phẩm.",
                "Hoàn tất",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ClearInputs();
            LoadVariants();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể thêm biến thể: " + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Cập nhật size, màu hoặc số lượng tồn của biến thể đang chọn.
    /// </summary>
    private void btnSua_Click(object sender, EventArgs e)
    {
        if (_selectedVariantId <= 0)
        {
            MessageBox.Show(
                "Vui lòng chọn một biến thể trong bảng trước.",
                "Chưa chọn biến thể",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (!TryReadVariantInput(out string size, out string color, out int stockQuantity))
        {
            return;
        }

        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            if (VariantAlreadyExists(conn, size, color, _selectedVariantId))
            {
                MessageBox.Show(
                    "Size và màu này đã có trong một biến thể khác của sản phẩm.",
                    "Biến thể bị trùng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            const string sql = @"
                UPDATE ProductVariants
                SET Size = @size, Color = @color, StockQuantity = @stockQuantity
                WHERE ProductVariantId = @variantId AND ProductId = @productId";

            using SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@size", SqlDbType.NVarChar, 20).Value = size;
            command.Parameters.Add("@color", SqlDbType.NVarChar, 50).Value = color;
            command.Parameters.Add("@stockQuantity", SqlDbType.Int).Value = stockQuantity;
            command.Parameters.Add("@variantId", SqlDbType.Int).Value = _selectedVariantId;
            command.Parameters.Add("@productId", SqlDbType.Int).Value = _productId;

            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show(
                    "Không tìm thấy biến thể cần cập nhật.",
                    "Không có thay đổi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                LoadVariants();
                return;
            }

            MessageBox.Show(
                "Đã cập nhật biến thể sản phẩm.",
                "Hoàn tất",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ClearInputs();
            LoadVariants();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể cập nhật biến thể: " + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Xóa lựa chọn hiện tại và tải lại danh sách biến thể.
    /// </summary>
    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        ClearInputs();
        LoadVariants();
    }

    private void ClearInputs()
    {
        _selectedVariantId = -1;
        cboSize.SelectedIndex = -1;
        txtColor.Clear();
        nudStock.Value = 0;
        dgvVariants.ClearSelection();
    }
}
