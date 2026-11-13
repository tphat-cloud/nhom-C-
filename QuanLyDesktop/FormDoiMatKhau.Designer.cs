namespace QuanLyDesktop;

partial class FormDoiMatKhau
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel layoutRoot;
    private Panel panelHeader;
    private Label lblTitle;
    private Label lblSubtitle;
    private Panel panelBody;
    private TableLayoutPanel layoutForm;
    private Label lblTaiKhoan;
    private Label lblTenDangNhap;
    private Label lblMatKhauCu;
    private TextBox txtMatKhauCu;
    private Label lblMatKhauMoi;
    private TextBox txtMatKhauMoi;
    private Label lblXacNhan;
    private TextBox txtXacNhan;
    private Label lblGoiY;
    private FlowLayoutPanel flowButtons;
    private Button btnLuu;
    private Button btnHuy;

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
        layoutRoot = new TableLayoutPanel();
        panelHeader = new Panel();
        lblSubtitle = new Label();
        lblTitle = new Label();
        panelBody = new Panel();
        layoutForm = new TableLayoutPanel();
        lblTaiKhoan = new Label();
        lblTenDangNhap = new Label();
        lblMatKhauCu = new Label();
        txtMatKhauCu = new TextBox();
        lblMatKhauMoi = new Label();
        txtMatKhauMoi = new TextBox();
        lblXacNhan = new Label();
        txtXacNhan = new TextBox();
        lblGoiY = new Label();
        flowButtons = new FlowLayoutPanel();
        btnLuu = new Button();
        btnHuy = new Button();
        layoutRoot.SuspendLayout();
        panelHeader.SuspendLayout();
        panelBody.SuspendLayout();
        layoutForm.SuspendLayout();
        flowButtons.SuspendLayout();
        SuspendLayout();
        // 
        // layoutRoot
        // 
        layoutRoot.BackColor = Color.FromArgb(245, 247, 250);
        layoutRoot.ColumnCount = 1;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRoot.Controls.Add(panelHeader, 0, 0);
        layoutRoot.Controls.Add(panelBody, 0, 1);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Location = new Point(0, 0);
        layoutRoot.Name = "layoutRoot";
        layoutRoot.Padding = new Padding(24, 18, 24, 22);
        layoutRoot.RowCount = 2;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRoot.Size = new Size(600, 470);
        layoutRoot.TabIndex = 0;
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.White;
        panelHeader.Controls.Add(lblSubtitle);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Dock = DockStyle.Fill;
        panelHeader.Location = new Point(24, 18);
        panelHeader.Margin = new Padding(0, 0, 0, 14);
        panelHeader.Name = "panelHeader";
        panelHeader.Padding = new Padding(18, 10, 18, 8);
        panelHeader.Size = new Size(552, 74);
        panelHeader.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTitle.ForeColor = Color.FromArgb(27, 54, 93);
        lblTitle.Location = new Point(18, 10);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(180, 41);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Đổi Mật Khẩu";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblSubtitle.ForeColor = Color.FromArgb(90, 102, 117);
        lblSubtitle.Location = new Point(21, 49);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(330, 25);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Mật khẩu mới được băm PBKDF2 trước khi lưu vào CSDL";
        // 
        // panelBody
        // 
        panelBody.BackColor = Color.White;
        panelBody.Controls.Add(layoutForm);
        panelBody.Dock = DockStyle.Fill;
        panelBody.Location = new Point(24, 106);
        panelBody.Margin = new Padding(0);
        panelBody.Name = "panelBody";
        panelBody.Padding = new Padding(24, 20, 24, 18);
        panelBody.Size = new Size(552, 342);
        panelBody.TabIndex = 1;
        // 
        // layoutForm
        // 
        layoutForm.ColumnCount = 2;
        layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        layoutForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutForm.Controls.Add(lblTaiKhoan, 0, 0);
        layoutForm.Controls.Add(lblTenDangNhap, 1, 0);
        layoutForm.Controls.Add(lblMatKhauCu, 0, 1);
        layoutForm.Controls.Add(txtMatKhauCu, 1, 1);
        layoutForm.Controls.Add(lblMatKhauMoi, 0, 2);
        layoutForm.Controls.Add(txtMatKhauMoi, 1, 2);
        layoutForm.Controls.Add(lblXacNhan, 0, 3);
        layoutForm.Controls.Add(txtXacNhan, 1, 3);
        layoutForm.Controls.Add(lblGoiY, 0, 4);
        layoutForm.Controls.Add(flowButtons, 0, 5);
        layoutForm.Dock = DockStyle.Fill;
        layoutForm.Location = new Point(24, 20);
        layoutForm.Name = "layoutForm";
        layoutForm.RowCount = 6;
        layoutForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        layoutForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        layoutForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        layoutForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        layoutForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        layoutForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutForm.Size = new Size(504, 304);
        layoutForm.TabIndex = 0;
        // 
        // lblTaiKhoan
        // 
        lblTaiKhoan.Anchor = AnchorStyles.Left;
        lblTaiKhoan.AutoSize = true;
        lblTaiKhoan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblTaiKhoan.ForeColor = Color.FromArgb(51, 65, 85);
        lblTaiKhoan.Location = new Point(3, 11);
        lblTaiKhoan.Name = "lblTaiKhoan";
        lblTaiKhoan.Size = new Size(91, 23);
        lblTaiKhoan.TabIndex = 0;
        lblTaiKhoan.Text = "Tài khoản";
        // 
        // lblTenDangNhap
        // 
        lblTenDangNhap.Anchor = AnchorStyles.Left;
        lblTenDangNhap.AutoSize = true;
        lblTenDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTenDangNhap.ForeColor = Color.FromArgb(27, 54, 93);
        lblTenDangNhap.Location = new Point(153, 11);
        lblTenDangNhap.Name = "lblTenDangNhap";
        lblTenDangNhap.Size = new Size(24, 23);
        lblTenDangNhap.TabIndex = 1;
        lblTenDangNhap.Text = "-";
        // 
        // lblMatKhauCu
        // 
        lblMatKhauCu.Anchor = AnchorStyles.Left;
        lblMatKhauCu.AutoSize = true;
        lblMatKhauCu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblMatKhauCu.ForeColor = Color.FromArgb(51, 65, 85);
        lblMatKhauCu.Location = new Point(3, 57);
        lblMatKhauCu.Name = "lblMatKhauCu";
        lblMatKhauCu.Size = new Size(130, 23);
        lblMatKhauCu.TabIndex = 2;
        lblMatKhauCu.Text = "Mật khẩu hiện tại";
        // 
        // txtMatKhauCu
        // 
        txtMatKhauCu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtMatKhauCu.BorderStyle = BorderStyle.FixedSingle;
        txtMatKhauCu.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtMatKhauCu.Location = new Point(153, 50);
        txtMatKhauCu.Name = "txtMatKhauCu";
        txtMatKhauCu.Size = new Size(348, 30);
        txtMatKhauCu.TabIndex = 3;
        txtMatKhauCu.UseSystemPasswordChar = true;
        // 
        // lblMatKhauMoi
        // 
        lblMatKhauMoi.Anchor = AnchorStyles.Left;
        lblMatKhauMoi.AutoSize = true;
        lblMatKhauMoi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblMatKhauMoi.ForeColor = Color.FromArgb(51, 65, 85);
        lblMatKhauMoi.Location = new Point(3, 103);
        lblMatKhauMoi.Name = "lblMatKhauMoi";
        lblMatKhauMoi.Size = new Size(117, 23);
        lblMatKhauMoi.TabIndex = 4;
        lblMatKhauMoi.Text = "Mật khẩu mới";
        // 
        // txtMatKhauMoi
        // 
        txtMatKhauMoi.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtMatKhauMoi.BorderStyle = BorderStyle.FixedSingle;
        txtMatKhauMoi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtMatKhauMoi.Location = new Point(153, 96);
        txtMatKhauMoi.Name = "txtMatKhauMoi";
        txtMatKhauMoi.Size = new Size(348, 30);
        txtMatKhauMoi.TabIndex = 5;
        txtMatKhauMoi.UseSystemPasswordChar = true;
        // 
        // lblXacNhan
        // 
        lblXacNhan.Anchor = AnchorStyles.Left;
        lblXacNhan.AutoSize = true;
        lblXacNhan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblXacNhan.ForeColor = Color.FromArgb(51, 65, 85);
        lblXacNhan.Location = new Point(3, 149);
        lblXacNhan.Name = "lblXacNhan";
        lblXacNhan.Size = new Size(143, 23);
        lblXacNhan.TabIndex = 6;
        lblXacNhan.Text = "Nhập lại mật khẩu";
        // 
        // txtXacNhan
        // 
        txtXacNhan.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtXacNhan.BorderStyle = BorderStyle.FixedSingle;
        txtXacNhan.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtXacNhan.Location = new Point(153, 142);
        txtXacNhan.Name = "txtXacNhan";
        txtXacNhan.Size = new Size(348, 30);
        txtXacNhan.TabIndex = 7;
        txtXacNhan.UseSystemPasswordChar = true;
        // 
        // lblGoiY
        // 
        lblGoiY.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblGoiY.AutoSize = true;
        lblGoiY.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
        lblGoiY.ForeColor = Color.FromArgb(90, 102, 117);
        lblGoiY.Location = new Point(3, 194);
        lblGoiY.Name = "lblGoiY";
        lblGoiY.Size = new Size(493, 20);
        lblGoiY.TabIndex = 8;
        lblGoiY.Text = "Mật khẩu mới tối thiểu 8 ký tự và phải khác mật khẩu hiện tại.";
        // 
        // flowButtons
        // 
        flowButtons.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        flowButtons.AutoSize = true;
        flowButtons.Controls.Add(btnLuu);
        flowButtons.Controls.Add(btnHuy);
        flowButtons.FlowDirection = FlowDirection.RightToLeft;
        flowButtons.Location = new Point(212, 221);
        flowButtons.Name = "flowButtons";
        flowButtons.Size = new Size(289, 44);
        flowButtons.TabIndex = 9;
        flowButtons.WrapContents = false;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(27, 54, 93);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(153, 3);
        btnLuu.Margin = new Padding(6, 3, 0, 3);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(136, 38);
        btnLuu.TabIndex = 0;
        btnLuu.Text = "Đổi mật khẩu";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnHuy
        // 
        btnHuy.BackColor = Color.FromArgb(236, 240, 245);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        btnHuy.ForeColor = Color.FromArgb(51, 65, 85);
        btnHuy.Location = new Point(3, 3);
        btnHuy.Margin = new Padding(6, 3, 0, 3);
        btnHuy.Name = "btnHuy";
        btnHuy.Size = new Size(138, 38);
        btnHuy.TabIndex = 1;
        btnHuy.Text = "Huỷ";
        btnHuy.UseVisualStyleBackColor = false;
        btnHuy.Click += btnHuy_Click;
        // 
        // FormDoiMatKhau
        // 
        AcceptButton = btnLuu;
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(245, 247, 250);
        CancelButton = btnHuy;
        ClientSize = new Size(600, 470);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormDoiMatKhau";
        Padding = new Padding(0);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Đổi Mật Khẩu";
        Load += FormDoiMatKhau_Load;
        layoutRoot.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        panelBody.ResumeLayout(false);
        layoutForm.ResumeLayout(false);
        layoutForm.PerformLayout();
        flowButtons.ResumeLayout(false);
        ResumeLayout(false);
    }
}
