namespace QuanLyDesktop;

partial class FormMain
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblXinChao;
    private GroupBox grpThongTin;
    private Label lblMaSP;
    private TextBox txtMaSP;
    private Label lblTenSP;
    private TextBox txtTenSP;
    private Label lblDanhMuc;
    private ComboBox cboDanhMuc;
    private Label lblGiaBan;
    private TextBox txtGiaBan;
    private Label lblMoTa;
    private TextBox txtMoTa;
    private Button btnThem;
    private Button btnSua;
    private Button btnXoa;
    private Button btnLamMoi;
    private GroupBox grpDanhSach;
    private TextBox txtTimKiem;
    private Button btnTimKiem;
    private Button btnHienTatCa;
    private DataGridView dgvSanPham;
    private Button btnDangXuat;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitle = new Label();
        lblXinChao = new Label();
        grpThongTin = new GroupBox();
        btnLamMoi = new Button();
        btnXoa = new Button();
        btnSua = new Button();
        btnThem = new Button();
        txtMoTa = new TextBox();
        lblMoTa = new Label();
        txtGiaBan = new TextBox();
        lblGiaBan = new Label();
        cboDanhMuc = new ComboBox();
        lblDanhMuc = new Label();
        txtTenSP = new TextBox();
        lblTenSP = new Label();
        txtMaSP = new TextBox();
        lblMaSP = new Label();
        grpDanhSach = new GroupBox();
        dgvSanPham = new DataGridView();
        btnHienTatCa = new Button();
        btnTimKiem = new Button();
        txtTimKiem = new TextBox();
        btnDangXuat = new Button();
        button1 = new Button();
        grpThongTin.SuspendLayout();
        grpDanhSach.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(13, 71, 161);
        lblTitle.Location = new Point(25, 19);
        lblTitle.Margin = new Padding(4, 0, 4, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(723, 45);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "HỆ THỐNG QUẢN LÝ CỬA HÀNG THỜI TRANG";
        // 
        // lblXinChao
        // 
        lblXinChao.AutoSize = true;
        lblXinChao.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
        lblXinChao.ForeColor = Color.DarkSlateGray;
        lblXinChao.Location = new Point(28, 69);
        lblXinChao.Margin = new Padding(4, 0, 4, 0);
        lblXinChao.Name = "lblXinChao";
        lblXinChao.Size = new Size(339, 28);
        lblXinChao.TabIndex = 1;
        lblXinChao.Text = "Người dùng: Admin | Quyền: Quản trị";
        // 
        // grpThongTin
        // 
        grpThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpThongTin.BackColor = Color.White;
        grpThongTin.Controls.Add(btnLamMoi);
        grpThongTin.Controls.Add(btnXoa);
        grpThongTin.Controls.Add(btnSua);
        grpThongTin.Controls.Add(btnThem);
        grpThongTin.Controls.Add(txtMoTa);
        grpThongTin.Controls.Add(lblMoTa);
        grpThongTin.Controls.Add(txtGiaBan);
        grpThongTin.Controls.Add(lblGiaBan);
        grpThongTin.Controls.Add(cboDanhMuc);
        grpThongTin.Controls.Add(lblDanhMuc);
        grpThongTin.Controls.Add(txtTenSP);
        grpThongTin.Controls.Add(lblTenSP);
        grpThongTin.Controls.Add(txtMaSP);
        grpThongTin.Controls.Add(lblMaSP);
        grpThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpThongTin.ForeColor = Color.FromArgb(13, 71, 161);
        grpThongTin.Location = new Point(25, 112);
        grpThongTin.Margin = new Padding(4, 4, 4, 4);
        grpThongTin.Name = "grpThongTin";
        grpThongTin.Padding = new Padding(4, 4, 4, 4);
        grpThongTin.Size = new Size(1212, 238);
        grpThongTin.TabIndex = 2;
        grpThongTin.TabStop = false;
        grpThongTin.Text = "THÔNG TIN QUẦN ÁO";
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.SteelBlue;
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(731, 162);
        btnLamMoi.Margin = new Padding(4, 4, 4, 4);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(162, 50);
        btnLamMoi.TabIndex = 13;
        btnLamMoi.Text = "🔄 Làm Mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.Crimson;
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnXoa.ForeColor = Color.White;
        btnXoa.Location = new Point(544, 162);
        btnXoa.Margin = new Padding(4, 4, 4, 4);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(162, 50);
        btnXoa.TabIndex = 12;
        btnXoa.Text = "🗑️ Xóa";
        btnXoa.UseVisualStyleBackColor = false;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.Orange;
        btnSua.Cursor = Cursors.Hand;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(356, 162);
        btnSua.Margin = new Padding(4, 4, 4, 4);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(162, 50);
        btnSua.TabIndex = 11;
        btnSua.Text = "✏️ Cập Nhật";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.SeaGreen;
        btnThem.Cursor = Cursors.Hand;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(169, 162);
        btnThem.Margin = new Padding(4, 4, 4, 4);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(162, 50);
        btnThem.TabIndex = 10;
        btnThem.Text = "➕ Thêm Mới";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // txtMoTa
        // 
        txtMoTa.Font = new Font("Segoe UI", 9.5F);
        txtMoTa.Location = new Point(569, 96);
        txtMoTa.Margin = new Padding(4, 4, 4, 4);
        txtMoTa.Name = "txtMoTa";
        txtMoTa.PlaceholderText = "Chất liệu cotton, thoáng mát...";
        txtMoTa.Size = new Size(618, 33);
        txtMoTa.TabIndex = 9;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.Font = new Font("Segoe UI", 9.5F);
        lblMoTa.ForeColor = Color.Black;
        lblMoTa.Location = new Point(425, 100);
        lblMoTa.Margin = new Padding(4, 0, 4, 0);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(65, 25);
        lblMoTa.TabIndex = 8;
        lblMoTa.Text = "Mô tả:";
        // 
        // txtGiaBan
        // 
        txtGiaBan.Font = new Font("Segoe UI", 9.5F);
        txtGiaBan.Location = new Point(169, 96);
        txtGiaBan.Margin = new Padding(4, 4, 4, 4);
        txtGiaBan.Name = "txtGiaBan";
        txtGiaBan.PlaceholderText = "VD: 250000";
        txtGiaBan.Size = new Size(224, 33);
        txtGiaBan.TabIndex = 7;
        // 
        // lblGiaBan
        // 
        lblGiaBan.AutoSize = true;
        lblGiaBan.Font = new Font("Segoe UI", 9.5F);
        lblGiaBan.ForeColor = Color.Black;
        lblGiaBan.Location = new Point(25, 100);
        lblGiaBan.Margin = new Padding(4, 0, 4, 0);
        lblGiaBan.Name = "lblGiaBan";
        lblGiaBan.Size = new Size(137, 25);
        lblGiaBan.TabIndex = 6;
        lblGiaBan.Text = "Giá bán (VNĐ):";
        // 
        // cboDanhMuc
        // 
        cboDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDanhMuc.Font = new Font("Segoe UI", 9.5F);
        cboDanhMuc.FormattingEnabled = true;
        cboDanhMuc.Location = new Point(994, 40);
        cboDanhMuc.Margin = new Padding(4, 4, 4, 4);
        cboDanhMuc.Name = "cboDanhMuc";
        cboDanhMuc.Size = new Size(193, 33);
        cboDanhMuc.TabIndex = 5;
        // 
        // lblDanhMuc
        // 
        lblDanhMuc.AutoSize = true;
        lblDanhMuc.Font = new Font("Segoe UI", 9.5F);
        lblDanhMuc.ForeColor = Color.Black;
        lblDanhMuc.Location = new Point(881, 44);
        lblDanhMuc.Margin = new Padding(4, 0, 4, 0);
        lblDanhMuc.Name = "lblDanhMuc";
        lblDanhMuc.Size = new Size(102, 25);
        lblDanhMuc.TabIndex = 4;
        lblDanhMuc.Text = "Danh mục:";
        // 
        // txtTenSP
        // 
        txtTenSP.Font = new Font("Segoe UI", 9.5F);
        txtTenSP.Location = new Point(569, 40);
        txtTenSP.Margin = new Padding(4, 4, 4, 4);
        txtTenSP.Name = "txtTenSP";
        txtTenSP.PlaceholderText = "VD: Áo Thun Polo Nam";
        txtTenSP.Size = new Size(286, 33);
        txtTenSP.TabIndex = 3;
        // 
        // lblTenSP
        // 
        lblTenSP.AutoSize = true;
        lblTenSP.Font = new Font("Segoe UI", 9.5F);
        lblTenSP.ForeColor = Color.Black;
        lblTenSP.Location = new Point(425, 44);
        lblTenSP.Margin = new Padding(4, 0, 4, 0);
        lblTenSP.Name = "lblTenSP";
        lblTenSP.Size = new Size(132, 25);
        lblTenSP.TabIndex = 2;
        lblTenSP.Text = "Tên sản phẩm:";
        // 
        // txtMaSP
        // 
        txtMaSP.Font = new Font("Segoe UI", 9.5F);
        txtMaSP.Location = new Point(169, 40);
        txtMaSP.Margin = new Padding(4, 4, 4, 4);
        txtMaSP.Name = "txtMaSP";
        txtMaSP.PlaceholderText = "VD: AT-010";
        txtMaSP.Size = new Size(224, 33);
        txtMaSP.TabIndex = 1;
        // 
        // lblMaSP
        // 
        lblMaSP.AutoSize = true;
        lblMaSP.Font = new Font("Segoe UI", 9.5F);
        lblMaSP.ForeColor = Color.Black;
        lblMaSP.Location = new Point(25, 44);
        lblMaSP.Margin = new Padding(4, 0, 4, 0);
        lblMaSP.Name = "lblMaSP";
        lblMaSP.Size = new Size(130, 25);
        lblMaSP.TabIndex = 0;
        lblMaSP.Text = "Mã sản phẩm:";
        // 
        // grpDanhSach
        // 
        grpDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpDanhSach.BackColor = Color.White;
        grpDanhSach.Controls.Add(dgvSanPham);
        grpDanhSach.Controls.Add(btnHienTatCa);
        grpDanhSach.Controls.Add(btnTimKiem);
        grpDanhSach.Controls.Add(txtTimKiem);
        grpDanhSach.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpDanhSach.ForeColor = Color.FromArgb(13, 71, 161);
        grpDanhSach.Location = new Point(25, 369);
        grpDanhSach.Margin = new Padding(4, 4, 4, 4);
        grpDanhSach.Name = "grpDanhSach";
        grpDanhSach.Padding = new Padding(4, 4, 4, 4);
        grpDanhSach.Size = new Size(1212, 419);
        grpDanhSach.TabIndex = 3;
        grpDanhSach.TabStop = false;
        grpDanhSach.Text = "DANH SÁCH QUẦN ÁO TRONG CƠ SỞ DỮ LIỆU";
        // 
        // dgvSanPham
        // 
        dgvSanPham.AllowUserToAddRows = false;
        dgvSanPham.AllowUserToDeleteRows = false;
        dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSanPham.BackgroundColor = Color.WhiteSmoke;
        dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSanPham.Location = new Point(25, 94);
        dgvSanPham.Margin = new Padding(4, 4, 4, 4);
        dgvSanPham.MultiSelect = false;
        dgvSanPham.Name = "dgvSanPham";
        dgvSanPham.ReadOnly = true;
        dgvSanPham.RowHeadersWidth = 51;
        dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSanPham.Size = new Size(1162, 306);
        dgvSanPham.TabIndex = 3;
        dgvSanPham.CellClick += dgvSanPham_CellClick;
        // 
        // btnHienTatCa
        // 
        btnHienTatCa.BackColor = Color.Gray;
        btnHienTatCa.Cursor = Cursors.Hand;
        btnHienTatCa.FlatStyle = FlatStyle.Flat;
        btnHienTatCa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnHienTatCa.ForeColor = Color.White;
        btnHienTatCa.Location = new Point(625, 36);
        btnHienTatCa.Margin = new Padding(4, 4, 4, 4);
        btnHienTatCa.Name = "btnHienTatCa";
        btnHienTatCa.Size = new Size(138, 42);
        btnHienTatCa.TabIndex = 2;
        btnHienTatCa.Text = "Tất Cả";
        btnHienTatCa.UseVisualStyleBackColor = false;
        btnHienTatCa.Click += btnHienTatCa_Click;
        // 
        // btnTimKiem
        // 
        btnTimKiem.BackColor = Color.FromArgb(13, 71, 161);
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(475, 36);
        btnTimKiem.Margin = new Padding(4, 4, 4, 4);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(138, 42);
        btnTimKiem.TabIndex = 1;
        btnTimKiem.Text = "🔍 Tìm Kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Font = new Font("Segoe UI", 9.5F);
        txtTimKiem.Location = new Point(25, 40);
        txtTimKiem.Margin = new Padding(4, 4, 4, 4);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập tên hoặc mã sản phẩm cần tìm...";
        txtTimKiem.Size = new Size(436, 33);
        txtTimKiem.TabIndex = 0;
        // 
        // btnDangXuat
        // 
        btnDangXuat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDangXuat.BackColor = Color.IndianRed;
        btnDangXuat.Cursor = Cursors.Hand;
        btnDangXuat.FlatStyle = FlatStyle.Flat;
        btnDangXuat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDangXuat.ForeColor = Color.White;
        btnDangXuat.Location = new Point(1100, 25);
        btnDangXuat.Margin = new Padding(4, 4, 4, 4);
        btnDangXuat.Name = "btnDangXuat";
        btnDangXuat.Size = new Size(138, 44);
        btnDangXuat.TabIndex = 6;
        btnDangXuat.Text = "Đăng Xuất";
        btnDangXuat.UseVisualStyleBackColor = false;
        btnDangXuat.Click += btnDangXuat_Click;
        // 
        // button1
        // 
        button1.Location = new Point(967, 35);
        button1.Name = "button1";
        button1.Size = new Size(112, 34);
        button1.TabIndex = 7;
        button1.Text = "Đơn Hàng";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.WhiteSmoke;
        ClientSize = new Size(1262, 812);
        Controls.Add(button1);
        Controls.Add(btnDangXuat);
        Controls.Add(grpDanhSach);
        Controls.Add(grpThongTin);
        Controls.Add(lblXinChao);
        Controls.Add(lblTitle);
        Margin = new Padding(4, 4, 4, 4);
        MinimumSize = new Size(1182, 736);
        Name = "FormMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản Lý Shop Thời Trang";
        Load += FormMain_Load;
        grpThongTin.ResumeLayout(false);
        grpThongTin.PerformLayout();
        grpDanhSach.ResumeLayout(false);
        grpDanhSach.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Button button1;
}
