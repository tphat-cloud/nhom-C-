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
        lblHeader = new Label();
        lblTaiKhoan = new Label();
        txtTaiKhoan = new TextBox();
        lblMatKhau = new Label();
        txtMatKhau = new TextBox();
        btnDangNhap = new Button();
        btnThoat = new Button();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeader.ForeColor = Color.FromArgb(13, 71, 161);
        lblHeader.Location = new Point(20, 25);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(400, 40);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "ĐĂNG NHẬP HỆ THỐNG SHOP";
        lblHeader.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblTaiKhoan
        // 
        lblTaiKhoan.AutoSize = true;
        lblTaiKhoan.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        lblTaiKhoan.Location = new Point(45, 90);
        lblTaiKhoan.Name = "lblTaiKhoan";
        lblTaiKhoan.Size = new Size(128, 23);
        lblTaiKhoan.TabIndex = 1;
        lblTaiKhoan.Text = "Tên đăng nhập:";
        // 
        // txtTaiKhoan
        // 
        txtTaiKhoan.Font = new Font("Segoe UI", 10.5F);
        txtTaiKhoan.Location = new Point(45, 120);
        txtTaiKhoan.Name = "txtTaiKhoan";
        txtTaiKhoan.PlaceholderText = "Nhập tài khoản (admin)";
        txtTaiKhoan.Size = new Size(350, 31);
        txtTaiKhoan.TabIndex = 2;
        // 
        // lblMatKhau
        // 
        lblMatKhau.AutoSize = true;
        lblMatKhau.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        lblMatKhau.Location = new Point(45, 170);
        lblMatKhau.Name = "lblMatKhau";
        lblMatKhau.Size = new Size(86, 23);
        lblMatKhau.TabIndex = 3;
        lblMatKhau.Text = "Mật khẩu:";
        // 
        // txtMatKhau
        // 
        txtMatKhau.Font = new Font("Segoe UI", 10.5F);
        txtMatKhau.Location = new Point(45, 200);
        txtMatKhau.Name = "txtMatKhau";
        txtMatKhau.PasswordChar = '●';
        txtMatKhau.PlaceholderText = "Nhập mật khẩu (123456)";
        txtMatKhau.Size = new Size(350, 31);
        txtMatKhau.TabIndex = 4;
        // 
        // btnDangNhap
        // 
        btnDangNhap.BackColor = Color.FromArgb(13, 71, 161);
        btnDangNhap.Cursor = Cursors.Hand;
        btnDangNhap.FlatStyle = FlatStyle.Flat;
        btnDangNhap.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        btnDangNhap.ForeColor = Color.White;
        btnDangNhap.Location = new Point(45, 260);
        btnDangNhap.Name = "btnDangNhap";
        btnDangNhap.Size = new Size(165, 42);
        btnDangNhap.TabIndex = 5;
        btnDangNhap.Text = "Đăng Nhập";
        btnDangNhap.UseVisualStyleBackColor = false;
        btnDangNhap.Click += btnDangNhap_Click;
        // 
        // btnThoat
        // 
        btnThoat.BackColor = Color.LightGray;
        btnThoat.Cursor = Cursors.Hand;
        btnThoat.FlatStyle = FlatStyle.Flat;
        btnThoat.Font = new Font("Segoe UI", 10.5F);
        btnThoat.ForeColor = Color.Black;
        btnThoat.Location = new Point(230, 260);
        btnThoat.Name = "btnThoat";
        btnThoat.Size = new Size(165, 42);
        btnThoat.TabIndex = 6;
        btnThoat.Text = "Thoát";
        btnThoat.UseVisualStyleBackColor = false;
        btnThoat.Click += btnThoat_Click;
        // 
        // Form1
        // 
        AcceptButton = btnDangNhap;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(440, 340);
        Controls.Add(btnThoat);
        Controls.Add(btnDangNhap);
        Controls.Add(txtMatKhau);
        Controls.Add(lblMatKhau);
        Controls.Add(txtTaiKhoan);
        Controls.Add(lblTaiKhoan);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Đăng Nhập - Quản Lý Shop Thời Trang";
        ResumeLayout(false);
        PerformLayout();
    }
}
