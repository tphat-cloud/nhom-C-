using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

public partial class Form1 : Form
{
    private string connectionString => DatabaseConfig.ConnectionString;

    public Form1()
    {
        InitializeComponent();
    }

    // Sự kiện khi bấm nút "Đăng Nhập"
    private void btnDangNhap_Click(object sender, EventArgs e)
    {
        string username = txtTaiKhoan.Text.Trim();
        string password = txtMatKhau.Text.Trim();

        // 1. Kiểm tra không được để trống
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            // 2. Kết nối tới SQL Server để kiểm tra tài khoản
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Câu truy vấn kiểm tra tài khoản trong bảng Users
                string query = @"
    SELECT u.FullName, u.RoleId, r.RoleName
    FROM Users AS u
    INNER JOIN Roles AS r ON r.RoleId = u.RoleId
    WHERE u.Username = @u
      AND u.PasswordHash = @p
      AND u.IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Đăng nhập thành công!
                            string fullName = reader["FullName"].ToString() ?? "Người dùng";
                            string roleName = reader["RoleName"].ToString() ?? "";

                            MessageBox.Show($"Đăng nhập thành công!\nXin chào: {fullName}\nVai trò: {roleName}",
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Mở Form quản lý chính (FormMain) và ẩn Form đăng nhập
                            FormMain fMain = new FormMain(fullName, roleName);
                            this.Hide();
                            fMain.ShowDialog();

                            // Giữ Form đăng nhập để người dùng có thể đăng nhập lại sau khi đăng xuất.
                            txtMatKhau.Clear();
                            txtTaiKhoan.Focus();
                            this.Show();
                        }
                        else
                        {
                            // Sai tài khoản hoặc mật khẩu
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!",
                                            "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtMatKhau.Clear();
                            txtMatKhau.Focus();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không thể kết nối đến Cơ sở dữ liệu: " + ex.Message, "Lỗi kết nối CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Sự kiện khi bấm nút "Thoát" và hỏi lại để xác nhận "Thoát"
    private void btnThoat_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            Application.Exit();
        }
    }

    private void txtTaiKhoan_TextChanged(object sender, EventArgs e)
    {

    }
}
