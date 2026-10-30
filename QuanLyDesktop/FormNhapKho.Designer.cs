namespace QuanLyDesktop;

partial class FormNhapKho
{
    private System.ComponentModel.IContainer components = null!;
    private Panel panelHeader;
    private Label lblTitle;
    private Label lblSubtitle;
    private Label lblListTitle;
    private TextBox txtTimKiem;
    private Button btnTimKiem;
    private Button btnLamMoi;
    private Button btnHuyPhieu;
    private DataGridView dgvPhieu;
    private Label lblDetailTitle;
    private DataGridView dgvChiTiet;
    private GroupBox grpPhieuMoi;
    private Label lblNhaCungCap;
    private TextBox txtNhaCungCap;
    private Label lblDienThoai;
    private TextBox txtDienThoai;
    private Label lblGhiChu;
    private TextBox txtGhiChu;
    private Label lblChonHang;
    private Label lblSanPham;
    private ComboBox cboSanPham;
    private Label lblBienThe;
    private ComboBox cboBienThe;
    private Label lblSoLuong;
    private NumericUpDown nudSoLuong;
    private Label lblGiaNhap;
    private TextBox txtGiaNhap;
    private Button btnThemDong;
    private Button btnXoaDong;
    private Label lblDongNhap;
    private DataGridView dgvDongNhap;
    private Label lblTongKet;
    private Button btnLuuPhieu;
    private Label lblLuuY;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelHeader = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        lblListTitle = new Label();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        btnLamMoi = new Button();
        btnHuyPhieu = new Button();
        dgvPhieu = new DataGridView();
        lblDetailTitle = new Label();
        dgvChiTiet = new DataGridView();
        grpPhieuMoi = new GroupBox();
        lblNhaCungCap = new Label();
        txtNhaCungCap = new TextBox();
        lblDienThoai = new Label();
        txtDienThoai = new TextBox();
        lblGhiChu = new Label();
        txtGhiChu = new TextBox();
        lblChonHang = new Label();
        lblSanPham = new Label();
        cboSanPham = new ComboBox();
        lblBienThe = new Label();
        cboBienThe = new ComboBox();
        lblSoLuong = new Label();
        nudSoLuong = new NumericUpDown();
        lblGiaNhap = new Label();
        txtGiaNhap = new TextBox();
        btnThemDong = new Button();
        btnXoaDong = new Button();
        lblDongNhap = new Label();
        dgvDongNhap = new DataGridView();
        lblTongKet = new Label();
        btnLuuPhieu = new Button();
        lblLuuY = new Label();
        panelHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPhieu).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
        grpPhieuMoi.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvDongNhap).BeginInit();
        SuspendLayout();
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(17, 43, 72);
        panelHeader.Controls.Add(lblSubtitle);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(0, 0);
        panelHeader.Name = "panelHeader";
        panelHeader.Size = new Size(1240, 112);
        panelHeader.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 19F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(28, 19);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(300, 36);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "PHIẾU NHẬP KHO";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblSubtitle.ForeColor = Color.FromArgb(209, 222, 238);
        lblSubtitle.Location = new Point(31, 67);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(200, 19);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Mỗi lần nhận hàng lập một phiếu";
        // 
        // lblListTitle
        // 
        lblListTitle.AutoSize = true;
        lblListTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblListTitle.ForeColor = Color.FromArgb(13, 71, 161);
        lblListTitle.Location = new Point(30, 128);
        lblListTitle.Name = "lblListTitle";
        lblListTitle.Size = new Size(180, 19);
        lblListTitle.TabIndex = 2;
        lblListTitle.Text = "DANH SÁCH PHIẾU NHẬP";
        // 
        // txtTimKiem
        // 
        txtTimKiem.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtTimKiem.Location = new Point(30, 152);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tìm theo mã phiếu hoặc nhà cung cấp";
        txtTimKiem.Size = new Size(270, 25);
        txtTimKiem.TabIndex = 3;
        // 
        // btnTimKiem
        // 
        btnTimKiem.BackColor = Color.FromArgb(230, 239, 250);
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.FlatAppearance.BorderColor = Color.FromArgb(190, 211, 235);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.FromArgb(13, 71, 161);
        btnTimKiem.Location = new Point(310, 148);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(115, 34);
        btnTimKiem.TabIndex = 4;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.White;
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderColor = Color.FromArgb(205, 212, 221);
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.FromArgb(48, 60, 75);
        btnLamMoi.Location = new Point(435, 148);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(105, 34);
        btnLamMoi.TabIndex = 5;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // btnHuyPhieu
        // 
        btnHuyPhieu.BackColor = Color.IndianRed;
        btnHuyPhieu.Cursor = Cursors.Hand;
        btnHuyPhieu.FlatStyle = FlatStyle.Flat;
        btnHuyPhieu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnHuyPhieu.ForeColor = Color.White;
        btnHuyPhieu.Location = new Point(550, 148);
        btnHuyPhieu.Name = "btnHuyPhieu";
        btnHuyPhieu.Size = new Size(140, 34);
        btnHuyPhieu.TabIndex = 6;
        btnHuyPhieu.Text = "Hủy phiếu";
        btnHuyPhieu.UseVisualStyleBackColor = false;
        btnHuyPhieu.Click += btnHuyPhieu_Click;
        // 
        // dgvPhieu
        // 
        dgvPhieu.AllowUserToAddRows = false;
        dgvPhieu.AllowUserToDeleteRows = false;
        dgvPhieu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvPhieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPhieu.BackgroundColor = Color.White;
        dgvPhieu.BorderStyle = BorderStyle.None;
        dgvPhieu.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvPhieu.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgvPhieu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
        dgvPhieu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvPhieu.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
        dgvPhieu.ColumnHeadersHeight = 38;
        dgvPhieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvPhieu.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        dgvPhieu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
        dgvPhieu.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
        dgvPhieu.EnableHeadersVisualStyles = false;
        dgvPhieu.GridColor = Color.FromArgb(231, 235, 240);
        dgvPhieu.Location = new Point(30, 196);
        dgvPhieu.MultiSelect = false;
        dgvPhieu.Name = "dgvPhieu";
        dgvPhieu.ReadOnly = true;
        dgvPhieu.RowHeadersVisible = false;
        dgvPhieu.RowTemplate.Height = 32;
        dgvPhieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPhieu.Size = new Size(660, 226);
        dgvPhieu.TabIndex = 7;
        dgvPhieu.CellClick += dgvPhieu_CellClick;
        // 
        // lblDetailTitle
        // 
        lblDetailTitle.AutoSize = true;
        lblDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblDetailTitle.ForeColor = Color.FromArgb(13, 71, 161);
        lblDetailTitle.Location = new Point(30, 438);
        lblDetailTitle.Name = "lblDetailTitle";
        lblDetailTitle.Size = new Size(210, 19);
        lblDetailTitle.TabIndex = 8;
        lblDetailTitle.Text = "CHI TIẾT PHIẾU ĐANG CHỌN";
        // 
        // dgvChiTiet
        // 
        dgvChiTiet.AllowUserToAddRows = false;
        dgvChiTiet.AllowUserToDeleteRows = false;
        dgvChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvChiTiet.BackgroundColor = Color.White;
        dgvChiTiet.BorderStyle = BorderStyle.None;
        dgvChiTiet.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvChiTiet.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
        dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
        dgvChiTiet.ColumnHeadersHeight = 38;
        dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvChiTiet.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
        dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
        dgvChiTiet.EnableHeadersVisualStyles = false;
        dgvChiTiet.GridColor = Color.FromArgb(231, 235, 240);
        dgvChiTiet.Location = new Point(30, 462);
        dgvChiTiet.MultiSelect = false;
        dgvChiTiet.Name = "dgvChiTiet";
        dgvChiTiet.ReadOnly = true;
        dgvChiTiet.RowHeadersVisible = false;
        dgvChiTiet.RowTemplate.Height = 32;
        dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvChiTiet.Size = new Size(660, 350);
        dgvChiTiet.TabIndex = 9;
        // 
        // grpPhieuMoi
        // 
        grpPhieuMoi.Controls.Add(lblLuuY);
        grpPhieuMoi.Controls.Add(btnLuuPhieu);
        grpPhieuMoi.Controls.Add(lblTongKet);
        grpPhieuMoi.Controls.Add(dgvDongNhap);
        grpPhieuMoi.Controls.Add(lblDongNhap);
        grpPhieuMoi.Controls.Add(btnXoaDong);
        grpPhieuMoi.Controls.Add(btnThemDong);
        grpPhieuMoi.Controls.Add(txtGiaNhap);
        grpPhieuMoi.Controls.Add(lblGiaNhap);
        grpPhieuMoi.Controls.Add(nudSoLuong);
        grpPhieuMoi.Controls.Add(lblSoLuong);
        grpPhieuMoi.Controls.Add(cboBienThe);
        grpPhieuMoi.Controls.Add(lblBienThe);
        grpPhieuMoi.Controls.Add(cboSanPham);
        grpPhieuMoi.Controls.Add(lblSanPham);
        grpPhieuMoi.Controls.Add(lblChonHang);
        grpPhieuMoi.Controls.Add(txtGhiChu);
        grpPhieuMoi.Controls.Add(lblGhiChu);
        grpPhieuMoi.Controls.Add(txtDienThoai);
        grpPhieuMoi.Controls.Add(lblDienThoai);
        grpPhieuMoi.Controls.Add(txtNhaCungCap);
        grpPhieuMoi.Controls.Add(lblNhaCungCap);
        grpPhieuMoi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        grpPhieuMoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        grpPhieuMoi.ForeColor = Color.FromArgb(35, 52, 73);
        grpPhieuMoi.Location = new Point(712, 128);
        grpPhieuMoi.Name = "grpPhieuMoi";
        grpPhieuMoi.Size = new Size(498, 684);
        grpPhieuMoi.TabIndex = 10;
        grpPhieuMoi.TabStop = false;
        grpPhieuMoi.Text = "LẬP PHIẾU NHẬP MỚI";
        // 
        // lblNhaCungCap
        // 
        lblNhaCungCap.AutoSize = true;
        lblNhaCungCap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNhaCungCap.ForeColor = Color.FromArgb(48, 60, 75);
        lblNhaCungCap.Location = new Point(14, 26);
        lblNhaCungCap.Name = "lblNhaCungCap";
        lblNhaCungCap.Size = new Size(90, 15);
        lblNhaCungCap.TabIndex = 0;
        lblNhaCungCap.Text = "Nhà cung cấp";
        // 
        // txtNhaCungCap
        // 
        txtNhaCungCap.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtNhaCungCap.Location = new Point(14, 48);
        txtNhaCungCap.Name = "txtNhaCungCap";
        txtNhaCungCap.PlaceholderText = "Ví dụ: Công ty May mặc Sài Gòn";
        txtNhaCungCap.Size = new Size(300, 25);
        txtNhaCungCap.TabIndex = 0;
        // 
        // lblDienThoai
        // 
        lblDienThoai.AutoSize = true;
        lblDienThoai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDienThoai.ForeColor = Color.FromArgb(48, 60, 75);
        lblDienThoai.Location = new Point(326, 26);
        lblDienThoai.Name = "lblDienThoai";
        lblDienThoai.Size = new Size(64, 15);
        lblDienThoai.TabIndex = 2;
        lblDienThoai.Text = "Điện thoại";
        // 
        // txtDienThoai
        // 
        txtDienThoai.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtDienThoai.Location = new Point(326, 48);
        txtDienThoai.Name = "txtDienThoai";
        txtDienThoai.PlaceholderText = "028...";
        txtDienThoai.Size = new Size(156, 25);
        txtDienThoai.TabIndex = 1;
        // 
        // lblGhiChu
        // 
        lblGhiChu.AutoSize = true;
        lblGhiChu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGhiChu.ForeColor = Color.FromArgb(48, 60, 75);
        lblGhiChu.Location = new Point(14, 84);
        lblGhiChu.Name = "lblGhiChu";
        lblGhiChu.Size = new Size(47, 15);
        lblGhiChu.TabIndex = 4;
        lblGhiChu.Text = "Ghi chú";
        // 
        // txtGhiChu
        // 
        txtGhiChu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtGhiChu.Location = new Point(14, 106);
        txtGhiChu.Name = "txtGhiChu";
        txtGhiChu.PlaceholderText = "Số hóa đơn giấy, đợt nhập...";
        txtGhiChu.Size = new Size(468, 25);
        txtGhiChu.TabIndex = 2;
        // 
        // lblChonHang
        // 
        lblChonHang.AutoSize = true;
        lblChonHang.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblChonHang.ForeColor = Color.FromArgb(13, 71, 161);
        lblChonHang.Location = new Point(14, 144);
        lblChonHang.Name = "lblChonHang";
        lblChonHang.Size = new Size(190, 17);
        lblChonHang.TabIndex = 6;
        lblChonHang.Text = "CHỌN BIẾN THỂ CẦN NHẬP";
        // 
        // lblSanPham
        // 
        lblSanPham.AutoSize = true;
        lblSanPham.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSanPham.ForeColor = Color.FromArgb(48, 60, 75);
        lblSanPham.Location = new Point(14, 170);
        lblSanPham.Name = "lblSanPham";
        lblSanPham.Size = new Size(61, 15);
        lblSanPham.TabIndex = 8;
        lblSanPham.Text = "Sản phẩm";
        // 
        // cboSanPham
        // 
        cboSanPham.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSanPham.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        cboSanPham.Location = new Point(14, 192);
        cboSanPham.Name = "cboSanPham";
        cboSanPham.Size = new Size(240, 25);
        cboSanPham.TabIndex = 3;
        cboSanPham.SelectedIndexChanged += cboSanPham_SelectedIndexChanged;
        // 
        // lblBienThe
        // 
        lblBienThe.AutoSize = true;
        lblBienThe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblBienThe.ForeColor = Color.FromArgb(48, 60, 75);
        lblBienThe.Location = new Point(264, 170);
        lblBienThe.Name = "lblBienThe";
        lblBienThe.Size = new Size(120, 15);
        lblBienThe.TabIndex = 10;
        lblBienThe.Text = "Biến thể (size/màu)";
        // 
        // cboBienThe
        // 
        cboBienThe.DropDownStyle = ComboBoxStyle.DropDownList;
        cboBienThe.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        cboBienThe.Location = new Point(264, 192);
        cboBienThe.Name = "cboBienThe";
        cboBienThe.Size = new Size(218, 25);
        cboBienThe.TabIndex = 4;
        // 
        // lblSoLuong
        // 
        lblSoLuong.AutoSize = true;
        lblSoLuong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSoLuong.ForeColor = Color.FromArgb(48, 60, 75);
        lblSoLuong.Location = new Point(14, 230);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.Size = new Size(58, 15);
        lblSoLuong.TabIndex = 12;
        lblSoLuong.Text = "Số lượng";
        // 
        // nudSoLuong
        // 
        nudSoLuong.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        nudSoLuong.Location = new Point(14, 252);
        nudSoLuong.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoLuong.Name = "nudSoLuong";
        nudSoLuong.Size = new Size(90, 25);
        nudSoLuong.TabIndex = 5;
        nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblGiaNhap
        // 
        lblGiaNhap.AutoSize = true;
        lblGiaNhap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblGiaNhap.ForeColor = Color.FromArgb(48, 60, 75);
        lblGiaNhap.Location = new Point(114, 230);
        lblGiaNhap.Name = "lblGiaNhap";
        lblGiaNhap.Size = new Size(130, 15);
        lblGiaNhap.TabIndex = 14;
        lblGiaNhap.Text = "Giá nhập / 1 đơn vị";
        // 
        // txtGiaNhap
        // 
        txtGiaNhap.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtGiaNhap.Location = new Point(114, 252);
        txtGiaNhap.Name = "txtGiaNhap";
        txtGiaNhap.PlaceholderText = "113000";
        txtGiaNhap.Size = new Size(130, 25);
        txtGiaNhap.TabIndex = 6;
        // 
        // btnThemDong
        // 
        btnThemDong.BackColor = Color.FromArgb(13, 71, 161);
        btnThemDong.Cursor = Cursors.Hand;
        btnThemDong.FlatAppearance.BorderSize = 0;
        btnThemDong.FlatStyle = FlatStyle.Flat;
        btnThemDong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThemDong.ForeColor = Color.White;
        btnThemDong.Location = new Point(254, 248);
        btnThemDong.Name = "btnThemDong";
        btnThemDong.Size = new Size(110, 34);
        btnThemDong.TabIndex = 7;
        btnThemDong.Text = "Thêm dòng";
        btnThemDong.UseVisualStyleBackColor = false;
        btnThemDong.Click += btnThemDong_Click;
        // 
        // btnXoaDong
        // 
        btnXoaDong.BackColor = Color.White;
        btnXoaDong.Cursor = Cursors.Hand;
        btnXoaDong.FlatAppearance.BorderColor = Color.FromArgb(205, 212, 221);
        btnXoaDong.FlatStyle = FlatStyle.Flat;
        btnXoaDong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnXoaDong.ForeColor = Color.FromArgb(48, 60, 75);
        btnXoaDong.Location = new Point(372, 248);
        btnXoaDong.Name = "btnXoaDong";
        btnXoaDong.Size = new Size(110, 34);
        btnXoaDong.TabIndex = 8;
        btnXoaDong.Text = "Xóa dòng";
        btnXoaDong.UseVisualStyleBackColor = false;
        btnXoaDong.Click += btnXoaDong_Click;
        // 
        // lblDongNhap
        // 
        lblDongNhap.AutoSize = true;
        lblDongNhap.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDongNhap.ForeColor = Color.FromArgb(13, 71, 161);
        lblDongNhap.Location = new Point(14, 294);
        lblDongNhap.Name = "lblDongNhap";
        lblDongNhap.Size = new Size(150, 17);
        lblDongNhap.TabIndex = 17;
        lblDongNhap.Text = "CÁC DÒNG ĐÃ CHỌN";
        // 
        // dgvDongNhap
        // 
        dgvDongNhap.AllowUserToAddRows = false;
        dgvDongNhap.AllowUserToDeleteRows = false;
        dgvDongNhap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvDongNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDongNhap.BackgroundColor = Color.White;
        dgvDongNhap.BorderStyle = BorderStyle.None;
        dgvDongNhap.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvDongNhap.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgvDongNhap.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
        dgvDongNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvDongNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
        dgvDongNhap.ColumnHeadersHeight = 36;
        dgvDongNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvDongNhap.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        dgvDongNhap.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
        dgvDongNhap.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
        dgvDongNhap.EnableHeadersVisualStyles = false;
        dgvDongNhap.GridColor = Color.FromArgb(231, 235, 240);
        dgvDongNhap.Location = new Point(14, 316);
        dgvDongNhap.MultiSelect = false;
        dgvDongNhap.Name = "dgvDongNhap";
        dgvDongNhap.ReadOnly = true;
        dgvDongNhap.RowHeadersVisible = false;
        dgvDongNhap.RowTemplate.Height = 30;
        dgvDongNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDongNhap.Size = new Size(468, 186);
        dgvDongNhap.TabIndex = 9;
        // 
        // lblTongKet
        // 
        lblTongKet.AutoSize = true;
        lblTongKet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTongKet.ForeColor = Color.FromArgb(35, 52, 73);
        lblTongKet.Location = new Point(14, 514);
        lblTongKet.Name = "lblTongKet";
        lblTongKet.Size = new Size(220, 19);
        lblTongKet.TabIndex = 19;
        lblTongKet.Text = "Tổng cộng: 0 món — 0 đ";
        // 
        // btnLuuPhieu
        // 
        btnLuuPhieu.BackColor = Color.FromArgb(21, 128, 61);
        btnLuuPhieu.Cursor = Cursors.Hand;
        btnLuuPhieu.FlatAppearance.BorderSize = 0;
        btnLuuPhieu.FlatStyle = FlatStyle.Flat;
        btnLuuPhieu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLuuPhieu.ForeColor = Color.White;
        btnLuuPhieu.Location = new Point(14, 542);
        btnLuuPhieu.Name = "btnLuuPhieu";
        btnLuuPhieu.Size = new Size(468, 44);
        btnLuuPhieu.TabIndex = 10;
        btnLuuPhieu.Text = "LƯU PHIẾU NHẬP KHO";
        btnLuuPhieu.UseVisualStyleBackColor = false;
        btnLuuPhieu.Click += btnLuuPhieu_Click;
        // 
        // lblLuuY
        // 
        lblLuuY.AutoSize = true;
        lblLuuY.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        lblLuuY.ForeColor = Color.FromArgb(110, 122, 138);
        lblLuuY.Location = new Point(14, 596);
        lblLuuY.Name = "lblLuuY";
        lblLuuY.Size = new Size(300, 15);
        lblLuuY.TabIndex = 21;
        lblLuuY.Text = "Tồn kho được cộng ngay khi lưu; hủy phiếu sẽ trừ lại.";
        // 
        // FormNhapKho
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 247, 251);
        ClientSize = new Size(1240, 844);
        Controls.Add(grpPhieuMoi);
        Controls.Add(dgvChiTiet);
        Controls.Add(lblDetailTitle);
        Controls.Add(dgvPhieu);
        Controls.Add(btnHuyPhieu);
        Controls.Add(btnLamMoi);
        Controls.Add(btnTimKiem);
        Controls.Add(txtTimKiem);
        Controls.Add(lblListTitle);
        Controls.Add(panelHeader);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(1180, 780);
        Name = "FormNhapKho";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Phiếu nhập kho";
        Load += FormNhapKho_Load;
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPhieu).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
        grpPhieuMoi.ResumeLayout(false);
        grpPhieuMoi.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvDongNhap).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
