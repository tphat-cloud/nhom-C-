using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

public partial class FormDanhMuc : Form
{
    private readonly string connectionString =
        "Server=.;Database=FashionShopDb;Trusted_Connection=True;TrustServerCertificate=True;";
    private int _selectedCategoryId = -1;
    private bool _selectedCategoryIsActive;

    public FormDanhMuc()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Được gọi khi form vừa mở; tải danh sách danh mục để hiển thị.
    /// </summary>
    private void FormDanhMuc_Load(object sender, EventArgs e)
    {
        LoadDanhMuc();
    }

    /// <summary>
    /// Lấy danh mục từ SQL Server và gán vào bảng trên giao diện.
    /// </summary>
    private void LoadDanhMuc(string keyword = "")
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"
                SELECT CategoryId, Name, Description, IsActive
                FROM Categories
                WHERE @keyword = N'' OR Name LIKE N'%' + @keyword + N'%'
                ORDER BY Name";

            using SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.Add("@keyword", SqlDbType.NVarChar, 255)
                .Value = keyword;

            DataTable table = new DataTable();
            adapter.Fill(table);

            // Bảng tự tạo cột từ các trường trong DataTable.
            dgvDanhMuc.DataSource = table;

            // ID chỉ dùng nội bộ, không cần hiển thị cho người dùng.
            if (dgvDanhMuc.Columns["CategoryId"] != null)
            {
                dgvDanhMuc.Columns["CategoryId"].Visible = false;
            }

            dgvDanhMuc.Columns["Name"].HeaderText = "Tên danh mục";
            dgvDanhMuc.Columns["Description"].HeaderText = "Mô tả";
            dgvDanhMuc.Columns["IsActive"].HeaderText = "Đang hoạt động";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải danh sách danh mục: " + ex.Message,
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Lưu ID của dòng được chọn và đưa tên, mô tả lên các ô nhập liệu.
    /// </summary>
    private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        // Không xử lý khi người dùng bấm vào hàng tiêu đề của bảng.
        if (e.RowIndex < 0)
        {
            return;
        }

        DataGridViewRow selectedRow = dgvDanhMuc.Rows[e.RowIndex];

        if (selectedRow.Cells["CategoryId"].Value is null)
        {
            return;
        }

        // Giữ khóa chính để các thao tác sửa/ngừng hoạt động tác động đúng danh mục.
        _selectedCategoryId = Convert.ToInt32(selectedRow.Cells["CategoryId"].Value);
        _selectedCategoryIsActive = Convert.ToBoolean(selectedRow.Cells["IsActive"].Value);
        txtTenDanhMuc.Text = selectedRow.Cells["Name"].Value?.ToString() ?? "";
        txtMoTa.Text = selectedRow.Cells["Description"].Value?.ToString() ?? "";
        btnNgungHoatDong.Text = _selectedCategoryIsActive
            ? "Ngừng hoạt động"
            : "Kích hoạt lại";
        btnNgungHoatDong.ForeColor = _selectedCategoryIsActive
            ? Color.FromArgb(150, 55, 55)
            : Color.FromArgb(13, 71, 161);
    }

    /// <summary>
    /// Kiểm tra dữ liệu rồi thêm một danh mục mới đang hoạt động vào cơ sở dữ liệu.
    /// </summary>
    private void btnThem_Click(object sender, EventArgs e)
    {
        string tenDanhMuc = txtTenDanhMuc.Text.Trim();
        string moTa = txtMoTa.Text.Trim();

        // Tên danh mục là bắt buộc vì cột Name trong CSDL không cho phép NULL.
        if (string.IsNullOrWhiteSpace(tenDanhMuc))
        {
            MessageBox.Show(
                "Vui lòng nhập tên danh mục.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtTenDanhMuc.Focus();
            return;
        }

        // Giới hạn độ dài khớp với kiểu dữ liệu trong bảng Categories.
        if (tenDanhMuc.Length > 100 || moTa.Length > 255)
        {
            MessageBox.Show(
                "Tên danh mục tối đa 100 ký tự và mô tả tối đa 255 ký tự.",
                "Thông tin quá dài",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // Kiểm tra trước để tránh tạo danh mục trùng tên.
            string duplicateSql = "SELECT COUNT(1) FROM Categories WHERE Name = @name";
            using (SqlCommand duplicateCommand = new SqlCommand(duplicateSql, conn))
            {
                duplicateCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100)
                    .Value = tenDanhMuc;

                if (Convert.ToInt32(duplicateCommand.ExecuteScalar()) > 0)
                {
                    MessageBox.Show(
                        "Tên danh mục này đã tồn tại.",
                        "Danh mục bị trùng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            string insertSql = @"
                INSERT INTO Categories (Name, Description, IsActive)
                VALUES (@name, @description, 1)";

            using (SqlCommand insertCommand = new SqlCommand(insertSql, conn))
            {
                insertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100)
                    .Value = tenDanhMuc;

                object descriptionValue = string.IsNullOrWhiteSpace(moTa)
                    ? DBNull.Value
                    : moTa;

                insertCommand.Parameters.Add("@description", SqlDbType.NVarChar, 255)
                    .Value = descriptionValue;

                insertCommand.ExecuteNonQuery();
            }

            MessageBox.Show(
                "Thêm danh mục thành công.",
                "Hoàn tất",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _selectedCategoryId = -1;
            _selectedCategoryIsActive = false;
            txtTenDanhMuc.Clear();
            txtMoTa.Clear();
            btnNgungHoatDong.Text = "Ngừng hoạt động";
            btnNgungHoatDong.ForeColor = Color.FromArgb(150, 55, 55);
            LoadDanhMuc();
            dgvDanhMuc.ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể thêm danh mục: " + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Cập nhật tên và mô tả cho danh mục đang được chọn.
    /// </summary>
    private void btnSua_Click(object sender, EventArgs e)
    {
        if (_selectedCategoryId <= 0)
        {
            MessageBox.Show(
                "Vui lòng chọn một danh mục trong bảng trước khi cập nhật.",
                "Chưa chọn danh mục",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string tenDanhMuc = txtTenDanhMuc.Text.Trim();
        string moTa = txtMoTa.Text.Trim();

        if (string.IsNullOrWhiteSpace(tenDanhMuc))
        {
            MessageBox.Show(
                "Tên danh mục không được để trống.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtTenDanhMuc.Focus();
            return;
        }

        if (tenDanhMuc.Length > 100 || moTa.Length > 255)
        {
            MessageBox.Show(
                "Tên danh mục tối đa 100 ký tự và mô tả tối đa 255 ký tự.",
                "Thông tin quá dài",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // Bỏ qua chính danh mục đang sửa khi kiểm tra tên trùng.
            string duplicateSql = @"
                SELECT COUNT(1)
                FROM Categories
                WHERE Name = @name AND CategoryId <> @id";

            using (SqlCommand duplicateCommand = new SqlCommand(duplicateSql, conn))
            {
                duplicateCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100)
                    .Value = tenDanhMuc;
                duplicateCommand.Parameters.Add("@id", SqlDbType.Int)
                    .Value = _selectedCategoryId;

                if (Convert.ToInt32(duplicateCommand.ExecuteScalar()) > 0)
                {
                    MessageBox.Show(
                        "Tên danh mục này đã được sử dụng.",
                        "Danh mục bị trùng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            string updateSql = @"
                UPDATE Categories
                SET Name = @name, Description = @description
                WHERE CategoryId = @id";

            using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
            {
                updateCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100)
                    .Value = tenDanhMuc;

                object descriptionValue = string.IsNullOrWhiteSpace(moTa)
                    ? DBNull.Value
                    : moTa;

                updateCommand.Parameters.Add("@description", SqlDbType.NVarChar, 255)
                    .Value = descriptionValue;
                updateCommand.Parameters.Add("@id", SqlDbType.Int)
                    .Value = _selectedCategoryId;

                if (updateCommand.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy danh mục cần cập nhật. Danh sách sẽ được tải lại.",
                        "Danh mục không còn tồn tại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    LoadDanhMuc();
                    return;
                }
            }

            MessageBox.Show(
                "Cập nhật danh mục thành công.",
                "Hoàn tất",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _selectedCategoryId = -1;
            _selectedCategoryIsActive = false;
            txtTenDanhMuc.Clear();
            txtMoTa.Clear();
            btnNgungHoatDong.Text = "Ngừng hoạt động";
            btnNgungHoatDong.ForeColor = Color.FromArgb(150, 55, 55);
            LoadDanhMuc();
            dgvDanhMuc.ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể cập nhật danh mục: " + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Chuyển trạng thái danh mục đang chọn giữa đang hoạt động và đã ngừng.
    /// </summary>
    private void btnNgungHoatDong_Click(object sender, EventArgs e)
    {
        if (_selectedCategoryId <= 0)
        {
            MessageBox.Show(
                "Vui lòng chọn một danh mục trong bảng trước.",
                "Chưa chọn danh mục",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        bool newIsActive = !_selectedCategoryIsActive;
        string actionText = newIsActive ? "kích hoạt lại" : "ngừng hoạt động";

        DialogResult confirmation = MessageBox.Show(
            $"Bạn có chắc muốn {actionText} danh mục '{txtTenDanhMuc.Text.Trim()}'?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmation != DialogResult.Yes)
        {
            return;
        }

        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // Chỉ đổi trạng thái, không xóa dòng dữ liệu hay liên kết sản phẩm.
            string sql = @"
                UPDATE Categories
                SET IsActive = @newStatus
                WHERE CategoryId = @id AND IsActive = @currentStatus";

            using SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add("@newStatus", SqlDbType.Bit).Value = newIsActive;
            command.Parameters.Add("@id", SqlDbType.Int).Value = _selectedCategoryId;
            command.Parameters.Add("@currentStatus", SqlDbType.Bit).Value = _selectedCategoryIsActive;

            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show(
                    "Trạng thái danh mục đã thay đổi hoặc danh mục không còn tồn tại. Danh sách sẽ được tải lại.",
                    "Không thể cập nhật trạng thái",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(
                    $"Đã {actionText} danh mục.",
                    "Hoàn tất",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            _selectedCategoryId = -1;
            _selectedCategoryIsActive = false;
            txtTenDanhMuc.Clear();
            txtMoTa.Clear();
            btnNgungHoatDong.Text = "Ngừng hoạt động";
            btnNgungHoatDong.ForeColor = Color.FromArgb(150, 55, 55);
            LoadDanhMuc(txtTimKiem.Text.Trim());
            dgvDanhMuc.ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể ngừng hoạt động danh mục: " + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Lọc danh sách theo tên danh mục người dùng nhập.
    /// </summary>
    private void btnTimKiem_Click(object sender, EventArgs e)
    {
        _selectedCategoryId = -1;
        _selectedCategoryIsActive = false;
        txtTenDanhMuc.Clear();
        txtMoTa.Clear();
        btnNgungHoatDong.Text = "Ngừng hoạt động";
        btnNgungHoatDong.ForeColor = Color.FromArgb(150, 55, 55);
        LoadDanhMuc(txtTimKiem.Text.Trim());
        dgvDanhMuc.ClearSelection();
    }

    /// <summary>
    /// Xóa từ khóa tìm kiếm và tải lại toàn bộ danh sách danh mục.
    /// </summary>
    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        txtTimKiem.Clear();
        _selectedCategoryId = -1;
        _selectedCategoryIsActive = false;
        txtTenDanhMuc.Clear();
        txtMoTa.Clear();
        btnNgungHoatDong.Text = "Ngừng hoạt động";
        btnNgungHoatDong.ForeColor = Color.FromArgb(150, 55, 55);
        LoadDanhMuc();
        dgvDanhMuc.ClearSelection();
    }
}