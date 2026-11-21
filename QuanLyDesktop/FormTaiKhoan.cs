using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

public partial class FormTaiKhoan : Form
{
    private const int MinimumPasswordLength = 8;
    private const int MaximumPasswordLength = 128;

    private readonly int _currentUserId;
    private int? _selectedUserId;

    public FormTaiKhoan(int currentUserId)
    {
        _currentUserId = currentUserId;
        InitializeComponent();

        Load += FormTaiKhoan_Load;
        dgvUsers.CellClick += dgvUsers_CellClick;
        btnNew.Click += btnNew_Click;
        btnSave.Click += btnSave_Click;
        btnRefresh.Click += btnRefresh_Click;
    }

    private void FormTaiKhoan_Load(object? sender, EventArgs e)
    {
        if (!ReloadData())
        {
            Close();
            return;
        }

        ResetEditor();
    }

    private bool ReloadData()
    {
        try
        {
            using SqlConnection connection = new(DatabaseConfig.ConnectionString);
            connection.Open();

            if (!IsCurrentUserAdmin(connection))
            {
                MessageBox.Show(
                    "Chỉ tài khoản Admin đang hoạt động mới được quản lý tài khoản.",
                    "Không đủ quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            LoadRoles(connection);
            LoadUsers(connection);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải danh sách tài khoản: " + ex.Message,
                "Lỗi CSDL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }
    }

    private bool IsCurrentUserAdmin(SqlConnection connection)
    {
        const string sql = """
            SELECT COUNT(1)
            FROM Users u
            INNER JOIN Roles r ON r.RoleId = u.RoleId
            WHERE u.UserId = @userId
              AND u.IsActive = 1
              AND r.RoleName = N'Admin'
            """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@userId", SqlDbType.Int).Value = _currentUserId;
        return Convert.ToInt32(command.ExecuteScalar()) == 1;
    }

    private void LoadRoles(SqlConnection connection)
    {
        const string sql = """
            SELECT RoleId, RoleName
            FROM Roles
            ORDER BY RoleName
            """;

        var roles = new List<RoleOption>();
        using (SqlCommand command = new(sql, connection))
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                roles.Add(new RoleOption(
                    reader.GetInt32(reader.GetOrdinal("RoleId")),
                    reader.GetString(reader.GetOrdinal("RoleName"))));
            }
        }

        if (roles.Count == 0)
        {
            throw new InvalidOperationException("Cơ sở dữ liệu chưa có vai trò để gán cho tài khoản.");
        }

        cboRole.DataSource = roles;
        cboRole.DisplayMember = nameof(RoleOption.RoleName);
        cboRole.ValueMember = nameof(RoleOption.RoleId);
    }

    private void LoadUsers(SqlConnection connection)
    {
        const string sql = """
            SELECT u.UserId, u.Username, u.FullName, u.RoleId, r.RoleName, u.IsActive
            FROM Users u
            INNER JOIN Roles r ON r.RoleId = u.RoleId
            ORDER BY u.Username, u.UserId
            """;

        var table = new DataTable();
        using (SqlCommand command = new(sql, connection))
        using (SqlDataReader reader = command.ExecuteReader())
        {
            table.Load(reader);
        }

        dgvUsers.DataSource = table;
        dgvUsers.ClearSelection();

        dgvUsers.Columns[nameof(UserRow.UserId)]!.Visible = false;
        dgvUsers.Columns[nameof(UserRow.RoleId)]!.Visible = false;
        dgvUsers.Columns[nameof(UserRow.Username)]!.HeaderText = "Tên đăng nhập";
        dgvUsers.Columns[nameof(UserRow.FullName)]!.HeaderText = "Họ tên";
        dgvUsers.Columns[nameof(UserRow.RoleName)]!.HeaderText = "Vai trò";
        dgvUsers.Columns[nameof(UserRow.IsActive)]!.HeaderText = "Đang hoạt động";
    }

    private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvUsers.Rows[e.RowIndex].DataBoundItem is not DataRowView row)
        {
            return;
        }

        _selectedUserId = Convert.ToInt32(row[nameof(UserRow.UserId)]);
        txtUsername.Text = Convert.ToString(row[nameof(UserRow.Username)]) ?? string.Empty;
        txtFullName.Text = Convert.ToString(row[nameof(UserRow.FullName)]) ?? string.Empty;
        cboRole.SelectedValue = Convert.ToInt32(row[nameof(UserRow.RoleId)]);
        chkIsActive.Checked = Convert.ToBoolean(row[nameof(UserRow.IsActive)]);
        txtPassword.Clear();
        txtConfirmPassword.Clear();

        bool isCurrentUser = _selectedUserId == _currentUserId;
        cboRole.Enabled = !isCurrentUser;
        chkIsActive.Enabled = !isCurrentUser;
        btnSave.Text = "Lưu thay đổi";
        lblPasswordHint.Text = "Để trống cả hai ô mật khẩu nếu không muốn đổi.";
    }

    private void btnNew_Click(object? sender, EventArgs e) => ResetEditor();

    private void btnRefresh_Click(object? sender, EventArgs e)
    {
        if (ReloadData())
        {
            ResetEditor();
        }
    }

    private void ResetEditor()
    {
        _selectedUserId = null;
        txtUsername.Clear();
        txtFullName.Clear();
        txtPassword.Clear();
        txtConfirmPassword.Clear();
        chkIsActive.Checked = true;
        cboRole.Enabled = true;
        chkIsActive.Enabled = true;

        if (cboRole.Items.Count > 0)
        {
            int staffIndex = -1;
            for (int i = 0; i < cboRole.Items.Count; i++)
            {
                if (cboRole.Items[i] is RoleOption role
                    && string.Equals(role.RoleName, "Staff", StringComparison.OrdinalIgnoreCase))
                {
                    staffIndex = i;
                    break;
                }
            }

            cboRole.SelectedIndex = staffIndex >= 0 ? staffIndex : 0;
        }

        btnSave.Text = "Tạo tài khoản";
        lblPasswordHint.Text = $"Tài khoản mới cần mật khẩu từ {MinimumPasswordLength} ký tự.";
        dgvUsers.ClearSelection();
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string fullName = txtFullName.Text.Trim();
        string password = txtPassword.Text;
        string confirmation = txtConfirmPassword.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(fullName))
        {
            ShowValidationError("Vui lòng nhập tên đăng nhập và họ tên.");
            return;
        }

        if (username.Length > 50 || fullName.Length > 100)
        {
            ShowValidationError("Tên đăng nhập tối đa 50 ký tự và họ tên tối đa 100 ký tự.");
            return;
        }

        if (cboRole.SelectedItem is not RoleOption selectedRole)
        {
            ShowValidationError("Vui lòng chọn vai trò cho tài khoản.");
            return;
        }

        bool isNewUser = !_selectedUserId.HasValue;
        bool passwordProvided = password.Length > 0 || confirmation.Length > 0;

        if (isNewUser && (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmation)))
        {
            ShowValidationError("Vui lòng nhập mật khẩu và xác nhận mật khẩu cho tài khoản mới.");
            txtPassword.Focus();
            return;
        }

        if (passwordProvided)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmation))
            {
                ShowValidationError("Vui lòng nhập cả mật khẩu mới và phần xác nhận.");
                return;
            }

            if (password.Length < MinimumPasswordLength || password.Length > MaximumPasswordLength)
            {
                ShowValidationError(
                    $"Mật khẩu phải có từ {MinimumPasswordLength} đến {MaximumPasswordLength} ký tự.");
                txtPassword.Focus();
                return;
            }

            if (!string.Equals(password, password.Trim(), StringComparison.Ordinal))
            {
                ShowValidationError("Mật khẩu không được bắt đầu hoặc kết thúc bằng dấu cách.");
                txtPassword.Focus();
                return;
            }

            if (!string.Equals(password, confirmation, StringComparison.Ordinal))
            {
                ShowValidationError("Mật khẩu và phần xác nhận không khớp.");
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }
        }

        bool isActive = chkIsActive.Checked;
        if (_selectedUserId == _currentUserId &&
            (!isActive || !string.Equals(selectedRole.RoleName, "Admin", StringComparison.OrdinalIgnoreCase)))
        {
            ShowValidationError("Không thể vô hiệu hóa hoặc đổi vai trò của tài khoản Admin đang đăng nhập.");
            return;
        }

        try
        {
            using (SqlConnection connection = new(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                if (!IsCurrentUserAdmin(connection))
                {
                    MessageBox.Show(
                        "Quyền Admin của bạn không còn hiệu lực. Vui lòng đăng nhập lại.",
                        "Không đủ quyền",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                if (UsernameExists(connection, username, _selectedUserId ?? 0))
                {
                    ShowValidationError("Tên đăng nhập đã được sử dụng.");
                    txtUsername.Focus();
                    return;
                }

                if (isNewUser)
                {
                    CreateUser(connection, username, fullName, selectedRole.RoleId, isActive, password);
                }
                else
                {
                    UpdateUser(connection, _selectedUserId!.Value, username, fullName,
                        selectedRole.RoleId, isActive, passwordProvided ? password : null);
                }
            }

            MessageBox.Show(
                isNewUser ? "Đã tạo tài khoản mới." : "Đã cập nhật tài khoản.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            if (ReloadData())
            {
                ResetEditor();
            }
        }
        catch (SqlException ex) when (ex.Number is 2601 or 2627)
        {
            ShowValidationError("Tên đăng nhập đã được sử dụng.");
            txtUsername.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể lưu tài khoản: " + ex.Message,
                "Lỗi CSDL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private static bool UsernameExists(SqlConnection connection, string username, int excludedUserId)
    {
        const string sql = """
            SELECT COUNT(1)
            FROM Users
            WHERE Username = @username
              AND UserId <> @excludedUserId
            """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
        command.Parameters.Add("@excludedUserId", SqlDbType.Int).Value = excludedUserId;
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    private static void CreateUser(
        SqlConnection connection,
        string username,
        string fullName,
        int roleId,
        bool isActive,
        string password)
    {
        const string sql = """
            INSERT INTO Users (Username, PasswordHash, FullName, RoleId, IsActive)
            VALUES (@username, @passwordHash, @fullName, @roleId, @isActive)
            """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
        command.Parameters.Add("@passwordHash", SqlDbType.NVarChar, 255).Value = PasswordHasher.Hash(password);
        command.Parameters.Add("@fullName", SqlDbType.NVarChar, 100).Value = fullName;
        command.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;
        command.Parameters.Add("@isActive", SqlDbType.Bit).Value = isActive;

        if (command.ExecuteNonQuery() != 1)
        {
            throw new InvalidOperationException("Không tạo được tài khoản.");
        }
    }

    private static void UpdateUser(
        SqlConnection connection,
        int userId,
        string username,
        string fullName,
        int roleId,
        bool isActive,
        string? password)
    {
        const string sql = """
            UPDATE Users
            SET Username = @username,
                FullName = @fullName,
                RoleId = @roleId,
                IsActive = @isActive,
                PasswordHash = CASE
                    WHEN @passwordHash IS NULL THEN PasswordHash
                    ELSE @passwordHash
                END
            WHERE UserId = @userId
            """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
        command.Parameters.Add("@fullName", SqlDbType.NVarChar, 100).Value = fullName;
        command.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;
        command.Parameters.Add("@isActive", SqlDbType.Bit).Value = isActive;
        command.Parameters.Add("@passwordHash", SqlDbType.NVarChar, 255).Value =
            password is null ? DBNull.Value : PasswordHasher.Hash(password);
        command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

        if (command.ExecuteNonQuery() != 1)
        {
            throw new InvalidOperationException("Tài khoản không còn tồn tại hoặc không thể cập nhật.");
        }
    }

    private static void ShowValidationError(string message)
    {
        MessageBox.Show(message, "Thông tin chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private sealed record RoleOption(int RoleId, string RoleName);

    private sealed class UserRow
    {
        public int UserId { get; init; }
        public string Username { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public int RoleId { get; init; }
        public string RoleName { get; init; } = string.Empty;
        public bool IsActive { get; init; }
    }
}
