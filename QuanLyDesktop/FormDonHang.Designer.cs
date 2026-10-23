namespace QuanLyDesktop
{
    partial class FormDonHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
    private Label lblSubtitle;
    private Label lblDetailSection;
    private DataGridView dgvChiTiet;
    private TextBox txtTimKiem;
    private ComboBox cboLocTrangThai;
    private Button btnTimKiem;
    private Label lblTrangThaiMoi;
    private ComboBox cboTrangThaiMoi;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lblSubtitle = new Label();
            lblDetailSection = new Label();
            dgvDonHang = new DataGridView();
            dgvChiTiet = new DataGridView();
            txtTimKiem = new TextBox();
            cboLocTrangThai = new ComboBox();
            btnTimKiem = new Button();
            lblTrangThaiMoi = new Label();
            cboTrangThaiMoi = new ComboBox();
            btnDuyetDon = new Button();
            btnLamMoi = new Button();
            btnXemChiTiet = new Button();
            btnThanhToan = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(13, 71, 161);
            label1.Location = new Point(28, 18);
            label1.Name = "label1";
            label1.Size = new Size(600, 38);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ ĐƠN HÀNG";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitle.ForeColor = Color.FromArgb(90, 102, 117);
            lblSubtitle.Location = new Point(31, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(316, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Theo dõi đơn đặt hàng từ website và mặt hàng trong đơn";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTimKiem.Location = new Point(30, 96);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Mã đơn, tên khách hoặc số điện thoại...";
            txtTimKiem.Size = new Size(360, 33);
            txtTimKiem.TabIndex = 0;
            // 
            // cboLocTrangThai
            // 
            cboLocTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocTrangThai.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboLocTrangThai.Location = new Point(405, 96);
            cboLocTrangThai.Name = "cboLocTrangThai";
            cboLocTrangThai.Size = new Size(190, 33);
            cboLocTrangThai.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.FromArgb(13, 71, 161);
            btnTimKiem.Cursor = Cursors.Hand;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.Location = new Point(610, 94);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(110, 38);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvDonHang
            // 
            dgvDonHang.AllowUserToAddRows = false;
            dgvDonHang.AllowUserToDeleteRows = false;
            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHang.BackgroundColor = Color.White;
            dgvDonHang.BorderStyle = BorderStyle.None;
            dgvDonHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDonHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDonHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
            dgvDonHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDonHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
            dgvDonHang.ColumnHeadersHeight = 38;
            dgvDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDonHang.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvDonHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
            dgvDonHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
            dgvDonHang.EnableHeadersVisualStyles = false;
            dgvDonHang.GridColor = Color.FromArgb(231, 235, 240);
            dgvDonHang.Location = new Point(30, 142);
            dgvDonHang.MultiSelect = false;
            dgvDonHang.Name = "dgvDonHang";
            dgvDonHang.ReadOnly = true;
            dgvDonHang.RowHeadersVisible = false;
            dgvDonHang.RowTemplate.Height = 32;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.Size = new Size(1120, 258);
            dgvDonHang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvDonHang.TabIndex = 2;
            dgvDonHang.CellClick += dgvDonHang_CellClick;
            // 
            // lblTrangThaiMoi
            // 
            lblTrangThaiMoi.AutoSize = true;
            lblTrangThaiMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTrangThaiMoi.ForeColor = Color.FromArgb(48, 60, 75);
            lblTrangThaiMoi.Location = new Point(30, 412);
            lblTrangThaiMoi.Name = "lblTrangThaiMoi";
            lblTrangThaiMoi.Size = new Size(115, 15);
            lblTrangThaiMoi.TabIndex = 5;
            lblTrangThaiMoi.Text = "Trạng thái tiếp theo";
            // 
            // cboTrangThaiMoi
            // 
            cboTrangThaiMoi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThaiMoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboTrangThaiMoi.Location = new Point(30, 432);
            cboTrangThaiMoi.Name = "cboTrangThaiMoi";
            cboTrangThaiMoi.Size = new Size(190, 33);
            cboTrangThaiMoi.TabIndex = 6;
            // 
            // lblDetailSection
            // 
            lblDetailSection.AutoSize = true;
            lblDetailSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetailSection.ForeColor = Color.FromArgb(13, 71, 161);
            lblDetailSection.Location = new Point(30, 486);
            lblDetailSection.Name = "lblDetailSection";
            lblDetailSection.Size = new Size(432, 23);
            lblDetailSection.TabIndex = 3;
            lblDetailSection.Text = "CHI TIẾT ĐƠN HÀNG - CHỌN ĐƠN RỒI BẤM XEM CHI TIẾT";
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.BackgroundColor = Color.White;
            dgvChiTiet.BorderStyle = BorderStyle.None;
            dgvChiTiet.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvChiTiet.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
            dgvChiTiet.ColumnHeadersHeight = 36;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvChiTiet.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
            dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
            dgvChiTiet.EnableHeadersVisualStyles = false;
            dgvChiTiet.GridColor = Color.FromArgb(231, 235, 240);
            dgvChiTiet.Location = new Point(30, 519);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.RowTemplate.Height = 30;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(1120, 187);
            dgvChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvChiTiet.TabIndex = 4;
            // 
            // btnDuyetDon
            // 
            btnDuyetDon.BackColor = Color.FromArgb(13, 71, 161);
            btnDuyetDon.Cursor = Cursors.Hand;
            btnDuyetDon.FlatAppearance.BorderSize = 0;
            btnDuyetDon.FlatStyle = FlatStyle.Flat;
            btnDuyetDon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDuyetDon.ForeColor = Color.White;
            btnDuyetDon.Location = new Point(235, 430);
            btnDuyetDon.Name = "btnDuyetDon";
            btnDuyetDon.Size = new Size(155, 38);
            btnDuyetDon.TabIndex = 7;
            btnDuyetDon.Text = "Cập nhật trạng thái";
            btnDuyetDon.UseVisualStyleBackColor = false;
            btnDuyetDon.Click += btnDuyetDon_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.White;
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderColor = Color.FromArgb(205, 212, 221);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.FromArgb(48, 60, 75);
            btnLamMoi.Location = new Point(735, 94);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 38);
            btnLamMoi.TabIndex = 4;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnXemChiTiet
            // 
            btnXemChiTiet.BackColor = Color.FromArgb(230, 239, 250);
            btnXemChiTiet.Cursor = Cursors.Hand;
            btnXemChiTiet.FlatAppearance.BorderColor = Color.FromArgb(190, 211, 235);
            btnXemChiTiet.FlatStyle = FlatStyle.Flat;
            btnXemChiTiet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXemChiTiet.ForeColor = Color.FromArgb(13, 71, 161);
            btnXemChiTiet.Location = new Point(405, 430);
            btnXemChiTiet.Name = "btnXemChiTiet";
            btnXemChiTiet.Size = new Size(145, 40);
            btnXemChiTiet.TabIndex = 8;
            btnXemChiTiet.Text = "Xem chi tiết";
            btnXemChiTiet.UseVisualStyleBackColor = false;
            btnXemChiTiet.Click += btnXemChiTiet_Click;
            // 
            // btnThanhToan
            // 
            btnThanhToan.BackColor = Color.FromArgb(46, 125, 50);
            btnThanhToan.Cursor = Cursors.Hand;
            btnThanhToan.FlatAppearance.BorderSize = 0;
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(565, 430);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(160, 40);
            btnThanhToan.TabIndex = 9;
            btnThanhToan.Text = "Xác nhận đã TT";
            btnThanhToan.UseVisualStyleBackColor = false;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // FormDonHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1180, 760);
            MinimumSize = new Size(980, 650);
            StartPosition = FormStartPosition.CenterParent;
            Controls.Add(dgvChiTiet);
            Controls.Add(lblDetailSection);
            Controls.Add(btnThanhToan);
            Controls.Add(btnXemChiTiet);
            Controls.Add(btnLamMoi);
            Controls.Add(btnDuyetDon);
            Controls.Add(cboTrangThaiMoi);
            Controls.Add(lblTrangThaiMoi);
            Controls.Add(dgvDonHang);
            Controls.Add(btnTimKiem);
            Controls.Add(cboLocTrangThai);
            Controls.Add(txtTimKiem);
            Controls.Add(lblSubtitle);
            Controls.Add(label1);
            Name = "FormDonHang";
            Text = "Quản lý đơn hàng";
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvDonHang;
        private Button btnDuyetDon;
        private Button btnLamMoi;
        private Button btnXemChiTiet;
        private Button btnThanhToan;
    }
}