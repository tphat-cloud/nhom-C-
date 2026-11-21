using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

public partial class FormKhachHang : Form
{
    private string connectionString => DatabaseConfig.ConnectionString;
    private int? _selectedCustomerId;

    public FormKhachHang()
    {
        InitializeComponent();
        Load += FormKhachHang_Load;
    }

    private void FormKhachHang_Load(object? sender, EventArgs e)
    {
        LoadCustomers();
    }

    private void LoadCustomers(string keyword = "", int? customerIdToSelect = null)
    {
        try
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            const string sql = @"
                SELECT CustomerId, FullName, PhoneNumber, Email, Address
                FROM Customers
                WHERE (@keyword = N''
                       OR FullName COLLATE Vietnamese_CI_AI LIKE N'%' + @keyword + N'%' ESCAPE '\'
                       OR PhoneNumber LIKE N'%' + @keyword + N'%' ESCAPE '\')
                ORDER BY FullName, CustomerId";

            using SqlDataAdapter adapter = new(sql, connection);
            adapter.SelectCommand.Parameters.Add("@keyword", SqlDbType.NVarChar, 100)
                .Value = EscapeLikeKeyword(keyword.Trim());

            DataTable customers = new();
            adapter.Fill(customers);
            dgvCustomers.DataSource = customers;
            dgvCustomers.Columns["CustomerId"].Visible = false;
            dgvCustomers.Columns["FullName"].HeaderText = "Họ và tên";
            dgvCustomers.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
            dgvCustomers.Columns["Email"].HeaderText = "Email";
            dgvCustomers.Columns["Address"].Visible = false;
            dgvCustomers.Columns["FullName"].FillWeight = 42;
            dgvCustomers.Columns["PhoneNumber"].FillWeight = 30;
            dgvCustomers.Columns["Email"].FillWeight = 28;
            dgvCustomers.ClearSelection();

            int? idToSelect = customerIdToSelect ?? _selectedCustomerId;
            if (idToSelect.HasValue)
            {
                foreach (DataGridViewRow row in dgvCustomers.Rows)
                {
                    if (Convert.ToInt32(row.Cells["CustomerId"].Value) == idToSelect.Value)
                    {
                        row.Selected = true;
                        SelectCustomer(row);
                        return;
                    }
                }
            }

            _selectedCustomerId = null;
            ClearCustomerDetails();
        }
        catch (Exception ex)
        {
            dgvCustomers.DataSource = null;
            dgvOrderHistory.DataSource = null;
            _selectedCustomerId = null;
            ClearCustomerDetails();
            MessageBox.Show(
                "Không thể tải danh sách khách hàng: " + ex.Message,
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private static string EscapeLikeKeyword(string keyword) =>
        keyword.Replace("\\", "\\\\")
            .Replace("%", "\\%")
            .Replace("_", "\\_")
            .Replace("[", "\\[");

    private void dgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        SelectCustomer(dgvCustomers.Rows[e.RowIndex]);
    }

    private void SelectCustomer(DataGridViewRow row)
    {
        if (row.Cells["CustomerId"].Value is null
            || row.Cells["CustomerId"].Value == DBNull.Value)
        {
            return;
        }

        _selectedCustomerId = Convert.ToInt32(row.Cells["CustomerId"].Value);
        txtFullName.Text = row.Cells["FullName"].Value?.ToString() ?? "";
        txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? "";
        txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
        txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
        btnSave.Enabled = true;
        LoadOrderHistory(_selectedCustomerId.Value);
    }

    private void LoadOrderHistory(int customerId)
    {
        try
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            const string sql = @"
                SELECT OrderCode, OrderDate, Status, TotalAmount,
                       CASE WHEN IsPaid = 1 AND Status <> N'Đã hủy'
                            THEN N'Đã thanh toán'
                            ELSE N'Chưa thanh toán' END AS PaymentStatus
                FROM Orders
                WHERE CustomerId = @customerId
                ORDER BY OrderDate DESC, OrderId DESC";

            using SqlDataAdapter adapter = new(sql, connection);
            adapter.SelectCommand.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;

            DataTable orders = new();
            adapter.Fill(orders);
            dgvOrderHistory.DataSource = orders;
            dgvOrderHistory.Columns["OrderCode"].HeaderText = "Mã đơn";
            dgvOrderHistory.Columns["OrderDate"].HeaderText = "Ngày đặt";
            dgvOrderHistory.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvOrderHistory.Columns["Status"].HeaderText = "Trạng thái";
            dgvOrderHistory.Columns["TotalAmount"].HeaderText = "Tổng tiền";
            dgvOrderHistory.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
            dgvOrderHistory.Columns["PaymentStatus"].HeaderText = "Thanh toán";
        }
        catch (Exception ex)
        {
            dgvOrderHistory.DataSource = null;
            MessageBox.Show(
                "Không thể tải lịch sử đơn hàng: " + ex.Message,
                "Lỗi tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnTimKiem_Click(object? sender, EventArgs e)
    {
        _selectedCustomerId = null;
        ClearCustomerDetails();
        LoadCustomers(txtSearch.Text);
    }

    private void btnTaiLai_Click(object? sender, EventArgs e)
    {
        txtSearch.Clear();
        _selectedCustomerId = null;
        ClearCustomerDetails();
        LoadCustomers();
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (!_selectedCustomerId.HasValue)
        {
            MessageBox.Show(
                "Vui lòng chọn khách hàng cần cập nhật.",
                "Chưa chọn khách hàng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string fullName = txtFullName.Text.Trim();
        string phoneNumber = txtPhoneNumber.Text.Trim();
        string email = txtEmail.Text.Trim();
        string address = txtAddress.Text.Trim();

        if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phoneNumber))
        {
            MessageBox.Show(
                "Họ tên và số điện thoại không được để trống.",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (fullName.Length > 100 || phoneNumber.Length > 20
            || email.Length > 100 || address.Length > 255)
        {
            MessageBox.Show(
                "Độ dài tối đa: họ tên 100, số điện thoại 20, email 100 và địa chỉ 255 ký tự.",
                "Thông tin quá dài",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            const string sql = @"
                UPDATE Customers
                SET FullName = @fullName,
                    PhoneNumber = @phoneNumber,
                    Email = @email,
                    Address = @address
                WHERE CustomerId = @customerId";

            using SqlCommand command = new(sql, connection);
            command.Parameters.Add("@fullName", SqlDbType.NVarChar, 100).Value = fullName;
            command.Parameters.Add("@phoneNumber", SqlDbType.NVarChar, 20).Value = phoneNumber;
            command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value =
                string.IsNullOrWhiteSpace(email) ? DBNull.Value : email;
            command.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value =
                string.IsNullOrWhiteSpace(address) ? DBNull.Value : address;
            command.Parameters.Add("@customerId", SqlDbType.Int).Value = _selectedCustomerId.Value;

            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show(
                    "Khách hàng không còn tồn tại hoặc không có thay đổi.",
                    "Không cập nhật được",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                LoadCustomers(txtSearch.Text);
                return;
            }

            MessageBox.Show(
                "Đã cập nhật thông tin khách hàng.",
                "Hoàn tất",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            LoadCustomers(txtSearch.Text, _selectedCustomerId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể cập nhật khách hàng: " + ex.Message,
                "Lỗi cập nhật",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnBoChon_Click(object? sender, EventArgs e)
    {
        _selectedCustomerId = null;
        dgvCustomers.ClearSelection();
        ClearCustomerDetails();
    }

    private void ClearCustomerDetails()
    {
        txtFullName.Clear();
        txtPhoneNumber.Clear();
        txtEmail.Clear();
        txtAddress.Clear();
        dgvOrderHistory.DataSource = null;
        btnSave.Enabled = false;
    }
}
