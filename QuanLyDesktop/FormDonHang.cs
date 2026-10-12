using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop
{
    public partial class FormDonHang : Form
    {
        private string connectionString = "Server=.;Database=FashionShopDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public FormDonHang()
        {
            InitializeComponent();
            LoadDonHang();
        }

        private void LoadDonHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            OrderId,
                            OrderCode AS [Mã Đơn],
                            CustomerName AS [Tên Khách Hàng],
                            CustomerPhone AS [Số Điện Thoại],
                            ShippingAddress AS [Địa Chỉ Nhận],
                            TotalAmount AS [Tổng Tiền (VNĐ)],
                            Status AS [Trạng Thái],
                            OrderDate AS [Ngày Đặt]
                        FROM Orders
                        ORDER BY OrderId DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvDonHang.DataSource = dt;

                        // Ẩn cột OrderId ngầm đi
                        if (dgvDonHang.Columns["OrderId"] != null)
                            dgvDonHang.Columns["OrderId"].Visible = false;

                        // Định dạng tiền tệ
                        if (dgvDonHang.Columns["Tổng Tiền (VNĐ)"] != null)
                            dgvDonHang.Columns["Tổng Tiền (VNĐ)"].DefaultCellStyle.Format = "N0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDuyetDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng trên bảng để duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvDonHang.SelectedRows[0].Cells["OrderId"].Value);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE Orders SET Status = N'Đã duyệt' WHERE OrderId = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", orderId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã duyệt đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDonHang();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi duyệt đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDonHang();
        }
    }
}
