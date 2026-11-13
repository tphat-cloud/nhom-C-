using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

public partial class FormDoiMatKhau : Form
{
    private const int MinPasswordLength = 8;

    private string connectionString => DatabaseConfig.ConnectionString;
    private readonly int _userId;

    public FormDoiMatKhau(int userId)
    {
        _userId = userId;
        InitializeComponent();
    }

    /// <summary>
    /// Hiển thị tên đăng nhập của tài khoản đang đăng nhập để người dùng đối chiếu.
    /// </summary>
    private void FormDoiMatKhau_Load(object sender, EventArgs e)
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            const string sql = @"
                SELECT Username
                FROM Users
                WHERE UserId = @id
                  AND IsActive = 1";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = _userId;

            object? username = cmd.ExecuteScalar();
            if (username is null || username == DBNull.Value)
            {
                MessageBox.Show(
                    "Không tìm thấy tài khoản đang đăng nhập hoặc tài khoản đã bị khoá.",
                    "Lỗi tài khoản",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
                return;
            }

            lblTenDangNhap.Text = username.ToString();
            txtMatKhauCu.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể kết nối đến Cơ sở dữ liệu: " + ex.Message,
                "Lỗi kết nối CSDL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Close();
        }
    }

    /// <summary>
    /// Kiểm tra dữ liệu nhập, xác thực mật khẩu cũ rồi ghi hash mới vào CSDL.
    /// </summary>
    private void btnLuu_Click(object sender, EventArgs e)
    {
        // Form1 trim mật khẩu khi đăng nhập, nên mật khẩu cũ cũng phải trim
        // để việc xác thực ở đây giống hệt lúc đăng nhập.
        string oldPassword = txtMatKhauCu.Text.Trim();
        string newPassword = txtMatKhauMoi.Text;
        string confirmPassword = txtXacNhan.Text;

        if (string.IsNullOrWhiteSpace(oldPassword)
            || string.IsNullOrWhiteSpace(newPassword)
            || string.IsNullOrWhiteSpace(confirmPassword))
        {
            MessageBox.Show(
                "Vui lòng nhập đầy đủ mật khẩu hiện tại, mật khẩu mới và xác nhận.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (newPassword.Length < MinPasswordLength)
        {
            MessageBox.Show(
                $"Mật khẩu mới phải có ít nhất {MinPasswordLength} ký tự.",
                "Mật khẩu quá ngắn",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtMatKhauMoi.Focus();
            return;
        }

        // Không cho đặt mật khẩu có dấu cách ở hai đầu: lúc đăng nhập Form1 trim
        // mật khẩu, nên mật khẩu như vậy sẽ không bao giờ đăng nhập được.
        if (!string.Equals(newPassword, newPassword.Trim(), StringComparison.Ordinal))
        {
            MessageBox.Show(
                "Mật khẩu mới không được bắt đầu hoặc kết thúc bằng dấu cách.",
                "Mật khẩu không hợp lệ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtMatKhauMoi.Focus();
            return;
        }

        if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
        {
            MessageBox.Show(
                "Mật khẩu mới và phần xác nhận không khớp.",
                "Xác nhận sai",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtXacNhan.Clear();
            txtXacNhan.Focus();
            return;
        }

        if (string.Equals(newPassword, oldPassword, StringComparison.Ordinal))
        {
            MessageBox.Show(
                "Mật khẩu mới phải khác mật khẩu hiện tại.",
                "Mật khẩu không đổi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtMatKhauMoi.Focus();
            return;
        }

        try
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1. Đọc hash hiện tại của chính tài khoản đang đăng nhập.
            const string selectSql = @"
                SELECT PasswordHash
                FROM Users
                WHERE UserId = @id
                  AND IsActive = 1";

            string? currentHash;
            using (SqlCommand cmd = new SqlCommand(selectSql, conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = _userId;

                object? value = cmd.ExecuteScalar();
                currentHash = value is null || value == DBNull.Value ? null : value.ToString();
            }

            // 2. Mật khẩu chỉ được kiểm tra bằng PasswordHasher ở tầng ứng dụng.
            if (!PasswordHasher.Verify(oldPassword, currentHash))
            {
                MessageBox.Show(
                    "Mật khẩu hiện tại không đúng.",
                    "Không thể đổi mật khẩu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtMatKhauCu.Clear();
                txtMatKhauCu.Focus();
                return;
            }

            // 3. Ghi hash mới (salt mới), không lưu mật khẩu gốc.
            const string updateSql = @"
                UPDATE Users
                SET PasswordHash = @hash
                WHERE UserId = @id";

            using (SqlCommand cmd = new SqlCommand(updateSql, conn))
            {
                cmd.Parameters.Add("@hash", SqlDbType.NVarChar, 255)
                    .Value = PasswordHasher.Hash(newPassword);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = _userId;

                if (cmd.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show(
                        "Không cập nhật được mật khẩu. Vui lòng thử lại.",
                        "Lỗi cập nhật",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show(
                "Đổi mật khẩu thành công. Hãy dùng mật khẩu mới ở lần đăng nhập sau.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể đổi mật khẩu: " + ex.Message,
                "Lỗi CSDL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnHuy_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
