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

        /// <summary>
        /// Tải danh sách đơn hàng mới nhất và xóa chi tiết cũ để tránh nhầm với đơn khác.
        /// </summary>
        private void LoadDonHang()
        {
            dgvChiTiet.DataSource = null;
            lblDetailSection.Text = "CHI TIẾT ĐƠN HÀNG - CHỌN ĐƠN RỒI BẤM XEM CHI TIẾT";

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

                        if (dgvDonHang.Columns["Ngày Đặt"] != null)
                            dgvDonHang.Columns["Ngày Đặt"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Duyệt đơn hàng đang được chọn và tải lại danh sách để cập nhật trạng thái.
        /// </summary>
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
        /// <summary>
        /// Hiển thị các mặt hàng thuộc đơn hàng đang chọn trong bảng chi tiết phía dưới.
        /// Nhân viên dùng thông tin này để kiểm tra đơn trước khi duyệt.
        /// </summary>
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            // Chưa chọn đơn thì không biết cần tải chi tiết của đơn nào.
            if (dgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một đơn hàng trước.",
                    "Chưa chọn đơn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedOrder = dgvDonHang.SelectedRows[0];
            int orderId = Convert.ToInt32(selectedOrder.Cells["OrderId"].Value);
            string maDon = selectedOrder.Cells["Mã Đơn"].Value?.ToString() ?? "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Chỉ lấy các dòng chi tiết thuộc đơn hàng đã chọn.
                    string sql = @"
                        SELECT
                            ProductName AS [Sản phẩm],
                            ISNULL(Size, N'') AS [Size],
                            ISNULL(Color, N'') AS [Màu],
                            Quantity AS [Số lượng],
                            UnitPrice AS [Đơn giá],
                            TotalPrice AS [Thành tiền]
                        FROM OrderDetails
                        WHERE OrderId = @id";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@id", orderId);

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvChiTiet.DataSource = dt;
                        lblDetailSection.Text = $"CHI TIẾT ĐƠN HÀNG {maDon} - {dt.Rows.Count} MẶT HÀNG";

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show(
                                "Đơn hàng này chưa có thông tin sản phẩm.",
                                "Không có chi tiết",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            return;
                        }

                        // Hiển thị tiền ở định dạng dễ đọc thay vì số thập phân dài.
                        dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                        dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải chi tiết đơn hàng: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Tải lại dữ liệu từ cơ sở dữ liệu để hiển thị các thay đổi mới nhất.
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDonHang();
        }
    }
}
