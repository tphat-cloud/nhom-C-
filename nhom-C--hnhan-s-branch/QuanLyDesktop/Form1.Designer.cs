namespace QuanLyDesktop;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Label lblHeader;
    private Label lblTaiKhoan;
    private TextBox txtTaiKhoan;
    private Label lblMatKhau;
    private TextBox txtMatKhau;
    private Button btnDangNhap;
    private Button btnThoat;
    private TableLayoutPanel layoutRoot;
    private Panel panelBrand;
    private Panel panelAccent;
    private Label lblBrandEyebrow;
    private Label lblBrandName;
    private Label lblBrandCopy;
    private Label lblBrandFooter;
    private Panel panelLogin;
    private TableLayoutPanel layoutLogin;
    private Label lblSubtitle;
    private Label lblAccessNote;

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
        layoutRoot = new TableLayoutPanel();
        panelBrand = new Panel();
        lblBrandFooter = new Label();
        lblBrandCopy = new Label();
        lblBrandName = new Label();
        lblBrandEyebrow = new Label();
        panelAccent = new Panel();
        panelLogin = new Panel();
        layoutLogin = new TableLayoutPanel();
        lblHeader = new Label();
        lblSubtitle = new Label();
        lblTaiKhoan = new Label();
        txtTaiKhoan = new TextBox();
        lblMatKhau = new Label();
        txtMatKhau = new TextBox();
        btnDangNhap = new Button();
        btnThoat = new Button();
        lblAccessNote = new Label();
        layoutRoot.SuspendLayout();
        panelBrand.SuspendLayout();
        panelLogin.SuspendLayout();
        layoutLogin.SuspendLayout();
        SuspendLayout();
        // 
        // layoutRoot
        // 
        layoutRoot.ColumnCount = 2;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRoot.Controls.Add(panelBrand, 0, 0);
        layoutRoot.Controls.Add(panelLogin, 1, 0);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Location = new Point(0, 0);
        layoutRoot.Margin = new Padding(0);
        layoutRoot.Name = "layoutRoot";
        layoutRoot.RowCount = 1;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRoot.Size = new Size(1040, 640);
        layoutRoot.TabIndex = 0;
        // 
        // panelBrand
        // 
        panelBrand.BackColor = Color.FromArgb(17, 43, 72);
        panelBrand.Controls.Add(lblBrandFooter);
        panelBrand.Controls.Add(lblBrandCopy);
        panelBrand.Controls.Add(lblBrandName);
        panelBrand.Controls.Add(lblBrandEyebrow);
        panelBrand.Controls.Add(panelAccent);
        panelBrand.Dock = DockStyle.Fill;
        panelBrand.Location = new Point(3, 3);
        panelBrand.Name = "panelBrand";
        panelBrand.Size = new Size(354, 634);
        panelBrand.TabIndex = 0;
        // 
        // lblBrandFooter
        // 
        lblBrandFooter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblBrandFooter.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblBrandFooter.ForeColor = Color.FromArgb(168, 198, 231);
        lblBrandFooter.Location = new Point(42, 1072);
        lblBrandFooter.Name = "lblBrandFooter";
        lblBrandFooter.Size = new Size(270, 44);
        lblBrandFooter.TabIndex = 4;
        lblBrandFooter.Text = "KHÔNG GIAN QUẢN TRỊ NỘI BỘ\nQuản lý sản phẩm và đơn hàng";
        // 
        // lblBrandCopy
        // 
        lblBrandCopy.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblBrandCopy.ForeColor = Color.FromArgb(209, 222, 238);
        lblBrandCopy.Location = new Point(42, 258);
        lblBrandCopy.Name = "lblBrandCopy";
        lblBrandCopy.Size = new Size(270, 66);
        lblBrandCopy.TabIndex = 3;
        lblBrandCopy.Text = "Hệ thống quản lý\ncửa hàng thời trang";
        // 
        // lblBrandName
        // 
        lblBrandName.Font = new Font("Segoe UI", 29F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblBrandName.ForeColor = Color.White;
        lblBrandName.Location = new Point(37, 125);
        lblBrandName.Name = "lblBrandName";
        lblBrandName.Size = new Size(285, 112);
        lblBrandName.TabIndex = 2;
        lblBrandName.Text = "BULUBU";
        lblBrandName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblBrandEyebrow
        // 
        lblBrandEyebrow.AutoSize = true;
        lblBrandEyebrow.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblBrandEyebrow.ForeColor = Color.FromArgb(168, 198, 231);
        lblBrandEyebrow.Location = new Point(39, 70);
        lblBrandEyebrow.Name = "lblBrandEyebrow";
        lblBrandEyebrow.Size = new Size(243, 25);
        lblBrandEyebrow.TabIndex = 1;
        lblBrandEyebrow.Text = "FASHION STORE  /  ADMIN";
        // 
        // panelAccent
        // 
        panelAccent.BackColor = Color.FromArgb(72, 155, 245);
        panelAccent.Location = new Point(42, 105);
        panelAccent.Name = "panelAccent";
        panelAccent.Size = new Size(58, 4);
        panelAccent.TabIndex = 0;
        // 
        // panelLogin
        // 
        panelLogin.BackColor = Color.White;
        panelLogin.Controls.Add(layoutLogin);
        panelLogin.Dock = DockStyle.Fill;
        panelLogin.Location = new Point(363, 3);
        panelLogin.Name = "panelLogin";
        panelLogin.Padding = new Padding(12, 8, 12, 8);
        panelLogin.Size = new Size(674, 634);
        panelLogin.TabIndex = 1;
        // 
        // layoutLogin
        // 
        layoutLogin.ColumnCount = 3;
        layoutLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
        layoutLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        layoutLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
        layoutLogin.Controls.Add(lblHeader, 1, 1);
        layoutLogin.Controls.Add(lblSubtitle, 1, 2);
        layoutLogin.Controls.Add(lblTaiKhoan, 1, 4);
        layoutLogin.Controls.Add(txtTaiKhoan, 1, 5);
        layoutLogin.Controls.Add(lblMatKhau, 1, 7);
        layoutLogin.Controls.Add(txtMatKhau, 1, 8);
        layoutLogin.Controls.Add(btnDangNhap, 1, 10);
        layoutLogin.Controls.Add(btnThoat, 1, 11);
        layoutLogin.Controls.Add(lblAccessNote, 1, 12);
        layoutLogin.Dock = DockStyle.Fill;
        layoutLogin.Location = new Point(12, 8);
        layoutLogin.Margin = new Padding(0);
        layoutLogin.Name = "layoutLogin";
        layoutLogin.RowCount = 14;
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));
        layoutLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
        layoutLogin.Size = new Size(650, 618);
        layoutLogin.TabIndex = 0;
        // 
        // lblHeader
        // 
        lblHeader.Dock = DockStyle.Fill;
        lblHeader.Font = new Font("Segoe UI", 23F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblHeader.ForeColor = Color.FromArgb(28, 43, 61);
        lblHeader.Location = new Point(100, 74);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(449, 61);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Chào mừng trở lại";
        lblHeader.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSubtitle
        // 
        lblSubtitle.Dock = DockStyle.Fill;
        lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblSubtitle.ForeColor = Color.FromArgb(105, 117, 132);
        lblSubtitle.Location = new Point(100, 135);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(449, 43);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Đăng nhập để tiếp tục vào hệ thống quản lý.";
        lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTaiKhoan
        // 
        lblTaiKhoan.Dock = DockStyle.Fill;
        lblTaiKhoan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTaiKhoan.ForeColor = Color.FromArgb(48, 60, 75);
        lblTaiKhoan.Location = new Point(100, 196);
        lblTaiKhoan.Name = "lblTaiKhoan";
        lblTaiKhoan.Size = new Size(449, 43);
        lblTaiKhoan.TabIndex = 2;
        lblTaiKhoan.Text = "Tên đăng nhập";
        lblTaiKhoan.TextAlign = ContentAlignment.BottomLeft;
        // 
        // txtTaiKhoan
        // 
        txtTaiKhoan.BorderStyle = BorderStyle.FixedSingle;
        txtTaiKhoan.Dock = DockStyle.Fill;
        txtTaiKhoan.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtTaiKhoan.Location = new Point(97, 244);
        txtTaiKhoan.Margin = new Padding(0, 5, 0, 5);
        txtTaiKhoan.Name = "txtTaiKhoan";
        txtTaiKhoan.PlaceholderText = "Nhập tên đăng nhập";
        txtTaiKhoan.Size = new Size(455, 37);
        txtTaiKhoan.TabIndex = 3;
        txtTaiKhoan.TextChanged += txtTaiKhoan_TextChanged;
        // 
        // lblMatKhau
        // 
        lblMatKhau.Dock = DockStyle.Fill;
        lblMatKhau.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblMatKhau.ForeColor = Color.FromArgb(48, 60, 75);
        lblMatKhau.Location = new Point(100, 312);
        lblMatKhau.Name = "lblMatKhau";
        lblMatKhau.Size = new Size(449, 43);
        lblMatKhau.TabIndex = 4;
        lblMatKhau.Text = "Mật khẩu";
        lblMatKhau.TextAlign = ContentAlignment.BottomLeft;
        // 
        // txtMatKhau
        // 
        txtMatKhau.BorderStyle = BorderStyle.FixedSingle;
        txtMatKhau.Dock = DockStyle.Fill;
        txtMatKhau.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtMatKhau.Location = new Point(97, 360);
        txtMatKhau.Margin = new Padding(0, 5, 0, 5);
        txtMatKhau.Name = "txtMatKhau";
        txtMatKhau.PasswordChar = '●';
        txtMatKhau.PlaceholderText = "Nhập mật khẩu";
        txtMatKhau.Size = new Size(455, 37);
        txtMatKhau.TabIndex = 5;
        // 
        // btnDangNhap
        // 
        btnDangNhap.BackColor = Color.FromArgb(24, 105, 213);
        btnDangNhap.Cursor = Cursors.Hand;
        btnDangNhap.Dock = DockStyle.Fill;
        btnDangNhap.FlatAppearance.BorderSize = 0;
        btnDangNhap.FlatStyle = FlatStyle.Flat;
        btnDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnDangNhap.ForeColor = Color.White;
        btnDangNhap.Location = new Point(97, 440);
        btnDangNhap.Margin = new Padding(0, 6, 0, 5);
        btnDangNhap.Name = "btnDangNhap";
        btnDangNhap.Size = new Size(455, 50);
        btnDangNhap.TabIndex = 6;
        btnDangNhap.Text = "Đăng nhập";
        btnDangNhap.UseVisualStyleBackColor = false;
        btnDangNhap.Click += btnDangNhap_Click;
        // 
        // btnThoat
        // 
        btnThoat.BackColor = Color.White;
        btnThoat.Cursor = Cursors.Hand;
        btnThoat.Dock = DockStyle.Fill;
        btnThoat.FlatAppearance.BorderColor = Color.FromArgb(211, 218, 228);
        btnThoat.FlatStyle = FlatStyle.Flat;
        btnThoat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnThoat.ForeColor = Color.FromArgb(64, 76, 91);
        btnThoat.Location = new Point(97, 499);
        btnThoat.Margin = new Padding(0, 4, 0, 4);
        btnThoat.Name = "btnThoat";
        btnThoat.Size = new Size(455, 41);
        btnThoat.TabIndex = 7;
        btnThoat.Text = "Thoát";
        btnThoat.UseVisualStyleBackColor = false;
        btnThoat.Click += btnThoat_Click;
        // 
        // lblAccessNote
        // 
        lblAccessNote.Dock = DockStyle.Fill;
        lblAccessNote.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblAccessNote.ForeColor = Color.FromArgb(117, 128, 142);
        lblAccessNote.Location = new Point(100, 544);
        lblAccessNote.Name = "lblAccessNote";
        lblAccessNote.Size = new Size(449, 43);
        lblAccessNote.TabIndex = 8;
        lblAccessNote.Text = "Chỉ dành cho tài khoản được cấp quyền truy cập.";
        lblAccessNote.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Form1
        // 
        AcceptButton = btnDangNhap;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(1040, 640);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimumSize = new Size(920, 600);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Đăng Nhập - Quản Lý Shop Thời Trang";
        layoutRoot.ResumeLayout(false);
        panelBrand.ResumeLayout(false);
        panelBrand.PerformLayout();
        panelLogin.ResumeLayout(false);
        layoutLogin.ResumeLayout(false);
        layoutLogin.PerformLayout();
        ResumeLayout(false);
    }
}
