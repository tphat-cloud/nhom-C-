using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDesktop
{
    public partial class FormDonHang : Form
    {
        private string connectionString => DatabaseConfig.ConnectionString;

        public FormDonHang()
        {
            InitializeComponent();
            cboLocTrangThai.Items.AddRange(new object[]
            {
                "Tất cả trạng thái",
                "Chờ xử lý",
                "Đã duyệt",
                "Đang giao",
                "Hoàn thành",
                "Đã hủy"
            });
            cboLocTrangThai.SelectedIndex = 0;
            cboTrangThaiMoi.Enabled = false;
            LoadDonHang();
        }

        /// <summary>
        /// Tải danh sách đơn hàng theo từ khóa/trạng thái và xóa chi tiết của lựa chọn trước.
        /// </summary>
        private void LoadDonHang(string keyword = "", string status = "")
        {
            dgvChiTiet.DataSource = null;
            lblDetailSection.Text = "CHI TIẾT ĐƠN HÀNG - CHỌN ĐƠN RỒI BẤM XEM CHI TIẾT";
            cboTrangThaiMoi.Items.Clear();
            cboTrangThaiMoi.Enabled = false;

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
                        WHERE (@keyword = N''
                               OR OrderCode LIKE N'%' + @keyword + N'%'
                               OR CustomerName LIKE N'%' + @keyword + N'%'
                               OR CustomerPhone LIKE N'%' + @keyword + N'%')
                          AND (@status = N'' OR Status = @status)
                        ORDER BY OrderId DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        da.SelectCommand.Parameters.Add("@keyword", SqlDbType.NVarChar, 100)
                            .Value = keyword;
                        da.SelectCommand.Parameters.Add("@status", SqlDbType.NVarChar, 50)
                            .Value = status;

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

                        dgvDonHang.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái đơn theo các bước nghiệp vụ được phép.
        /// </summary>
        private void btnDuyetDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng trên bảng để duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvDonHang.SelectedRows[0].Cells["OrderId"].Value);
            string currentStatus = dgvDonHang.SelectedRows[0].Cells["Trạng Thái"].Value?.ToString() ?? "";
            string nextStatus = cboTrangThaiMoi.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(nextStatus))
            {
                MessageBox.Show(
                    "Đơn hàng này không còn trạng thái tiếp theo để chuyển.",
                    "Không thể cập nhật",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string[] allowedStatuses = GetAllowedNextStatuses(currentStatus);
            if (Array.IndexOf(allowedStatuses, nextStatus) < 0)
            {
                MessageBox.Show(
                    "Trạng thái được chọn không hợp lệ với trạng thái hiện tại của đơn.",
                    "Chuyển trạng thái không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                $"Chuyển đơn hàng sang trạng thái '{nextStatus}'?",
                "Xác nhận cập nhật",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE Orders
                        SET Status = @nextStatus
                        WHERE OrderId = @id AND Status = @currentStatus";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@nextStatus", SqlDbType.NVarChar, 50).Value = nextStatus;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = orderId;
                        cmd.Parameters.Add("@currentStatus", SqlDbType.NVarChar, 50).Value = currentStatus;

                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            MessageBox.Show(
                                "Đơn hàng đã được thay đổi ở nơi khác. Danh sách sẽ được tải lại.",
                                "Không có thay đổi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(
                                $"Đã chuyển đơn sang trạng thái '{nextStatus}'.",
                                "Hoàn tất",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }

                        LoadDonHang(txtTimKiem.Text.Trim(), GetSelectedFilterStatus());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi duyệt đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Trả về các trạng thái tiếp theo hợp lệ cho một đơn hàng.
        /// </summary>
        private string[] GetAllowedNextStatuses(string currentStatus)
        {
            return currentStatus switch
            {
                "Chờ xử lý" => new[] { "Đã duyệt", "Đã hủy" },
                "Đã duyệt" => new[] { "Đang giao", "Đã hủy" },
                "Đang giao" => new[] { "Hoàn thành" },
                _ => Array.Empty<string>()
            };
        }

        /// <summary>
        /// Đưa các trạng thái hợp lệ vào danh sách khi người dùng chọn một đơn.
        /// </summary>
        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string currentStatus =
                dgvDonHang.Rows[e.RowIndex].Cells["Trạng Thái"].Value?.ToString() ?? "";
            string[] allowedStatuses = GetAllowedNextStatuses(currentStatus);

            cboTrangThaiMoi.Items.Clear();
            cboTrangThaiMoi.Items.AddRange(allowedStatuses);
            cboTrangThaiMoi.Enabled = allowedStatuses.Length > 0;
            cboTrangThaiMoi.SelectedIndex = allowedStatuses.Length > 0 ? 0 : -1;
        }

        /// <summary>
        /// Trả về trạng thái lọc; mục "Tất cả" được đổi thành chuỗi rỗng cho SQL.
        /// </summary>
        private string GetSelectedFilterStatus()
        {
            return cboLocTrangThai.SelectedIndex <= 0
                ? ""
                : cboLocTrangThai.SelectedItem?.ToString() ?? "";
        }

        /// <summary>
        /// Tìm đơn theo mã, tên khách/số điện thoại và trạng thái đã chọn.
        /// </summary>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDonHang(txtTimKiem.Text.Trim(), GetSelectedFilterStatus());
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
            txtTimKiem.Clear();
            cboLocTrangThai.SelectedIndex = 0;
            LoadDonHang();
        }
    }
}
