using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

public partial class FormMain : Form
{
    private string connectionString => DatabaseConfig.ConnectionString;
    private string _fullName;
    private string _roleName;
    private int _userId;
    private int _selectedProductId = -1; // Lưu mã sản phẩm đang được chọn để Sửa hoặc Xóa

    /// <summary>
    /// Kiểm tra người đang đăng nhập có quyền Admin hay không.
    /// Nếu không phải Admin, hiển thị cảnh báo và trả về false để dừng thao tác.
    /// </summary>
    private bool KiemTraQuyenAdmin()
    {
        // So sánh không phân biệt chữ hoa/chữ thường để tránh sai khác như "admin" và "Admin".
        if (string.Equals(_roleName, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        MessageBox.Show(
            "Bạn không có quyền thực hiện chức năng này.",
            "Không đủ quyền",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return false;
    }

    public FormMain(int userId, string fullName, string roleName)
    {
        _userId = userId;
        _fullName = fullName;
        _roleName = roleName;
        InitializeComponent();
    }

    // Sự kiện khi mở Form lên
    private void FormMain_Load(object sender, EventArgs e)
    {
        this.Text = $"Quản Lý Shop Quần Áo - Người dùng: {_fullName} ({_roleName})";
        lblXinChao.Text = $"Người dùng: {_fullName} | Quyền: {_roleName}";
        lblDashboardGreeting.Text = $"Chào mừng trở lại, {_fullName}";

        // Tải danh mục vào ComboBox và tải danh sách quần áo lên bảng DataGridView
        LoadDanhMuc();
        LoadSanPham();
        LoadDashboardSummary();
        ShowDashboard();
    }

    /// <summary>
    /// Nạp các số liệu tổng quan thật từ SQL Server vào ba thẻ trên trang chủ.
    /// </summary>
    private void LoadDashboardSummary()
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"
                DECLARE @today DATE = CONVERT(DATE, GETDATE());

                SELECT
                    (SELECT COUNT(1)
                     FROM Products
                     WHERE IsActive = 1) AS ActiveProductCount,
                    (SELECT COUNT(1)
                     FROM Orders
                     WHERE OrderDate >= @today
                       AND OrderDate < DATEADD(DAY, 1, @today)) AS OrdersToday,
                    (SELECT COALESCE(SUM(TotalAmount), 0)
                     FROM Orders
                     WHERE (Status = N'Hoàn thành'
                            OR (IsPaid = 1 AND Status <> N'Đã hủy'))
                       AND OrderDate >= @today
                       AND OrderDate < DATEADD(DAY, 1, @today)) AS RevenueToday";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                lblStatProductsValue.Text = Convert.ToInt32(reader["ActiveProductCount"]).ToString("N0");
                lblStatOrdersValue.Text = Convert.ToInt32(reader["OrdersToday"]).ToString("N0");
                lblStatRevenueValue.Text = Convert.ToDecimal(reader["RevenueToday"]).ToString("N0") + " đ";
            }
        }
        catch
        {
            // Các form nạp danh sách đã báo lỗi kết nối; dashboard chỉ đánh dấu số liệu chưa tải được.
            lblStatProductsValue.Text = "--";
            lblStatOrdersValue.Text = "--";
            lblStatRevenueValue.Text = "--";
            lblDashboardStatus.Text = "Không thể tải số liệu tổng quan từ cơ sở dữ liệu.";
        }
    }

    /// <summary>
    /// Hiển thị trang tổng quan và đánh dấu mục Trang chủ trên menu.
    /// </summary>
    private void ShowDashboard()
    {
        panelDashboard.Visible = true;
        grpThongTin.Visible = false;
        grpDanhSach.Visible = false;
        lblTitle.Text = "TỔNG QUAN CỬA HÀNG";

        btnTrangChu.BackColor = Color.FromArgb(35, 91, 150);
        btnTrangChu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnSanPham.BackColor = Color.FromArgb(17, 43, 72);
        btnSanPham.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
    }

    /// <summary>
    /// Hiển thị khu vực quản lý sản phẩm mà không đóng khung điều hướng chính.
    /// </summary>
    private void ShowProducts()
    {
        panelDashboard.Visible = false;
        grpThongTin.Visible = true;
        grpDanhSach.Visible = true;
        lblTitle.Text = "QUẢN LÝ SẢN PHẨM";

        btnTrangChu.BackColor = Color.FromArgb(17, 43, 72);
        btnTrangChu.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        btnSanPham.BackColor = Color.FromArgb(35, 91, 150);
        btnSanPham.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
    }

    private void btnTrangChu_Click(object sender, EventArgs e)
    {
        LoadDashboardSummary();
        ShowDashboard();
    }

    private void btnSanPham_Click(object sender, EventArgs e)
    {
        ShowProducts();
    }

    // 1. Tải danh mục (Áo thun, Sơ mi, Quần Jean...) vào ComboBox
    private void LoadDanhMuc()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT CategoryId, Name FROM Categories WHERE IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cboDanhMuc.DataSource = dt;
                        cboDanhMuc.DisplayMember = "Name";       // Tên hiển thị cho người xem
                        cboDanhMuc.ValueMember = "CategoryId";   // Giá trị mã danh mục ngầm
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // 2. Tải danh sách quần áo lên DataGridView
    private void LoadSanPham(string keyword = "")
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        p.ProductId,
                        p.ProductCode AS [Mã SP],
                        p.Name AS [Tên Sản Phẩm],
                        c.Name AS [Danh Mục],
                        p.Price AS [Giá Bán (VNĐ)],
                        p.Description AS [Mô Tả],
                        p.CategoryId
                    FROM Products p
                    INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                                        WHERE p.IsActive = 1
                                            AND (@kw = '' OR p.Name LIKE @kw OR p.ProductCode LIKE @kw)
                    ORDER BY p.ProductId DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", string.IsNullOrEmpty(keyword) ? "" : "%" + keyword + "%");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvSanPham.DataSource = dt;

                        // Ẩn cột ProductId và CategoryId ngầm
                        if (dgvSanPham.Columns["ProductId"] != null)
                            dgvSanPham.Columns["ProductId"].Visible = false;
                        if (dgvSanPham.Columns["CategoryId"] != null)
                            dgvSanPham.Columns["CategoryId"].Visible = false;

                        // Định dạng hiển thị tiền tệ cho cột Giá
                        if (dgvSanPham.Columns["Giá Bán (VNĐ)"] != null)
                            dgvSanPham.Columns["Giá Bán (VNĐ)"].DefaultCellStyle.Format = "N0";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // 3. Khi click chuột vào 1 dòng trên bảng -> Tự động điền dữ liệu lên các ô TextBox
    private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
            _selectedProductId = Convert.ToInt32(row.Cells["ProductId"].Value);

            txtMaSP.Text = row.Cells["Mã SP"].Value?.ToString();
            txtTenSP.Text = row.Cells["Tên Sản Phẩm"].Value?.ToString();
            txtGiaBan.Text = Convert.ToDecimal(row.Cells["Giá Bán (VNĐ)"].Value).ToString("0");
            txtMoTa.Text = row.Cells["Mô Tả"].Value?.ToString();

            if (row.Cells["CategoryId"].Value != null)
            {
                cboDanhMuc.SelectedValue = Convert.ToInt32(row.Cells["CategoryId"].Value);
            }
        }
    }

    /// <summary>
    /// Thêm sản phẩm mới vào cơ sở dữ liệu sau khi xác nhận người dùng là Admin
    /// và các thông tin nhập vào hợp lệ.
    /// </summary>
    private void btnThem_Click(object sender, EventArgs e)
    {
        // Kiểm tra quyền tại thời điểm xử lý, không chỉ dựa vào việc nút đang bật hay tắt.
        if (!KiemTraQuyenAdmin())
        {
            return;
        }

        string maSP = txtMaSP.Text.Trim();
        string tenSP = txtTenSP.Text.Trim();
        string moTa = txtMoTa.Text.Trim();

        if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(txtGiaBan.Text.Trim()))
        {
            MessageBox.Show("Vui lòng điền đầy đủ Mã sản phẩm, Tên sản phẩm và Giá bán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtGiaBan.Text.Trim(), out decimal giaBan) || giaBan <= 0)
        {
            MessageBox.Show("Giá bán phải là số hợp lệ lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int maDanhMuc = Convert.ToInt32(cboDanhMuc.SelectedValue);

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    INSERT INTO Products (ProductCode, Name, CategoryId, Price, Description, IsActive, IsFeatured, CreatedAt)
                    VALUES (@code, @name, @catId, @price, @desc, 1, 0, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@code", maSP);
                    cmd.Parameters.AddWithValue("@name", tenSP);
                    cmd.Parameters.AddWithValue("@catId", maDanhMuc);
                    cmd.Parameters.AddWithValue("@price", giaBan);
                    cmd.Parameters.AddWithValue("@desc", moTa);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearInputs();
                    LoadSanPham();
                }
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2601 || ex.Number == 2627) // Lỗi trùng mã sản phẩm (Unique Key)
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại! Vui lòng đặt mã khác.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Cập nhật sản phẩm đang được chọn sau khi xác nhận quyền Admin
    /// và kiểm tra dữ liệu người dùng nhập vào.
    /// </summary>
    private void btnSua_Click(object sender, EventArgs e)
    {
        // Dừng ngay nếu người đăng nhập không có quyền sửa sản phẩm.
        if (!KiemTraQuyenAdmin())
        {
            return;
        }

        if (_selectedProductId <= 0)
        {
            MessageBox.Show("Vui lòng click chọn 1 sản phẩm trên bảng để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string maSP = txtMaSP.Text.Trim();
        string tenSP = txtTenSP.Text.Trim();
        string moTa = txtMoTa.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSP) ||
            string.IsNullOrWhiteSpace(tenSP) ||
            !decimal.TryParse(txtGiaBan.Text.Trim(), out decimal giaBan) ||
            giaBan <= 0)
        {
            MessageBox.Show("Vui lòng nhập mã, tên và giá bán lớn hơn 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int maDanhMuc = Convert.ToInt32(cboDanhMuc.SelectedValue);

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    UPDATE Products 
                    SET ProductCode = @code, Name = @name, CategoryId = @catId, Price = @price, Description = @desc
                    WHERE ProductId = @id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@code", maSP);
                    cmd.Parameters.AddWithValue("@name", tenSP);
                    cmd.Parameters.AddWithValue("@catId", maDanhMuc);
                    cmd.Parameters.AddWithValue("@price", giaBan);
                    cmd.Parameters.AddWithValue("@desc", moTa);
                    cmd.Parameters.AddWithValue("@id", _selectedProductId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearInputs();
                    LoadSanPham();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Ngừng kinh doanh sản phẩm đang được chọn sau khi xác nhận quyền Admin.
    /// Dữ liệu vẫn được giữ để bảo toàn liên kết với các đơn hàng cũ.
    /// </summary>
    private void btnXoa_Click(object sender, EventArgs e)
    {
        // Dừng ngay nếu người đăng nhập không có quyền xóa sản phẩm.
        if (!KiemTraQuyenAdmin())
        {
            return;
        }

        if (_selectedProductId <= 0)
        {
            MessageBox.Show("Vui lòng chọn một sản phẩm trước.", "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult result = MessageBox.Show(
            "Bạn có chắc muốn ngừng kinh doanh sản phẩm này?",
            "Xác nhận ngừng bán",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Chuyển trạng thái thay vì xóa dòng để không làm mất lịch sử đơn hàng.
                    string sql = @"
                        UPDATE Products
                        SET IsActive = 0
                        WHERE ProductId = @id AND IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", _selectedProductId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show(
                                "Sản phẩm không còn tồn tại hoặc đã ngừng bán.",
                                "Không có thay đổi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            LoadSanPham();
                            return;
                        }

                        MessageBox.Show("Đã ngừng kinh doanh sản phẩm.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadSanPham();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa sản phẩm này (có thể đã có đơn hàng liên quan): " + ex.Message, "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // 7. Nút "Làm Mới"
    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        ClearInputs();
        LoadSanPham();
    }

    // 8. Nút "Tìm Kiếm"
    private void btnTimKiem_Click(object sender, EventArgs e)
    {
        string keyword = txtTimKiem.Text.Trim();
        LoadSanPham(keyword);
    }

    // 9. Nút "Hiển Thị Tất Cả"
    private void btnHienTatCa_Click(object sender, EventArgs e)
    {
        txtTimKiem.Clear();
        LoadSanPham();
    }

    /// <summary>
    /// Mở màn hình quản lý size, màu và tồn kho cho sản phẩm đang chọn.
    /// </summary>
    private void btnBienThe_Click(object sender, EventArgs e)
    {
        if (!KiemTraQuyenAdmin())
        {
            return;
        }

        if (_selectedProductId <= 0)
        {
            MessageBox.Show(
                "Vui lòng chọn một sản phẩm trước khi quản lý size và tồn kho.",
                "Chưa chọn sản phẩm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        using FormBienTheSanPham formBienThe =
            new FormBienTheSanPham(_selectedProductId, txtTenSP.Text.Trim());
        formBienThe.ShowDialog(this);
    }

    // 10. Nút "Đăng Xuất"
    private void btnDangXuat_Click(object sender, EventArgs e)
    {
        this.Close(); // Đóng FormMain để quay lại
    }

    // Hàm xóa trắng các ô nhập liệu
    private void ClearInputs()
    {
        _selectedProductId = -1;
        txtMaSP.Clear();
        txtTenSP.Clear();
        txtGiaBan.Clear();
        txtMoTa.Clear();
        if (cboDanhMuc.Items.Count > 0)
            cboDanhMuc.SelectedIndex = 0;
        txtMaSP.Focus();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        using FormDonHang f = new FormDonHang();
        f.ShowDialog(); // Mở cửa sổ đơn hàng lên

        // Đơn hàng có thể vừa được đổi trạng thái hoặc đánh dấu thanh toán,
        // nên doanh thu và số đơn phải được tính lại ngay khi quay về dashboard.
        LoadDashboardSummary();
    }

    /// <summary>
    /// Mở màn hình phiếu nhập kho. Nhân viên được lập phiếu,
    /// chỉ Admin được hủy phiếu (kiểm tra bên trong FormNhapKho).
    /// </summary>
    private void btnNhapKho_Click(object sender, EventArgs e)
    {
        using FormNhapKho formNhapKho = new FormNhapKho(_userId, _roleName);
        formNhapKho.ShowDialog(this);

        // Nhập kho làm thay đổi tồn kho nên số liệu tổng quan cần được tính lại.
        LoadDashboardSummary();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        // Chỉ Admin được mở màn hình quản lý danh mục.
        if (!KiemTraQuyenAdmin())
        {
            return;
        }

        // Mở form danh mục như cửa sổ con và chờ đóng form rồi mới quay lại.
        using FormDanhMuc formDanhMuc = new FormDanhMuc();
        formDanhMuc.ShowDialog(this);
    }
}
