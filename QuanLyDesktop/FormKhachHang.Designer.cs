namespace QuanLyDesktop;

partial class FormKhachHang
{
    private TableLayoutPanel layoutRoot;
    private Panel panelSearch;
    private Label lblSearch;
    private TextBox txtSearch;
    private Button btnTimKiem;
    private Button btnTaiLai;
    private TableLayoutPanel layoutBody;
    private GroupBox grpCustomers;
    private DataGridView dgvCustomers;
    private TableLayoutPanel layoutRight;
    private GroupBox grpContactInfo;
    private TableLayoutPanel layoutContact;
    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblPhoneNumber;
    private TextBox txtPhoneNumber;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblAddress;
    private TextBox txtAddress;
    private FlowLayoutPanel flowActions;
    private Button btnSave;
    private Button btnBoChon;
    private GroupBox grpOrderHistory;
    private DataGridView dgvOrderHistory;

    private void InitializeComponent()
    {
        layoutRoot = new TableLayoutPanel();
        panelSearch = new Panel();
        lblSearch = new Label();
        txtSearch = new TextBox();
        btnTimKiem = new Button();
        btnTaiLai = new Button();
        layoutBody = new TableLayoutPanel();
        grpCustomers = new GroupBox();
        dgvCustomers = new DataGridView();
        layoutRight = new TableLayoutPanel();
        grpContactInfo = new GroupBox();
        layoutContact = new TableLayoutPanel();
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblPhoneNumber = new Label();
        txtPhoneNumber = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblAddress = new Label();
        txtAddress = new TextBox();
        flowActions = new FlowLayoutPanel();
        btnSave = new Button();
        btnBoChon = new Button();
        grpOrderHistory = new GroupBox();
        dgvOrderHistory = new DataGridView();
        layoutRoot.SuspendLayout();
        panelSearch.SuspendLayout();
        layoutBody.SuspendLayout();
        grpCustomers.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
        layoutRight.SuspendLayout();
        grpContactInfo.SuspendLayout();
        layoutContact.SuspendLayout();
        flowActions.SuspendLayout();
        grpOrderHistory.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOrderHistory).BeginInit();
        SuspendLayout();
        //
        // layoutRoot
        //
        layoutRoot.BackColor = Color.FromArgb(245, 247, 250);
        layoutRoot.ColumnCount = 1;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRoot.Controls.Add(panelSearch, 0, 0);
        layoutRoot.Controls.Add(layoutBody, 0, 1);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Padding = new Padding(18, 14, 18, 18);
        layoutRoot.RowCount = 2;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRoot.TabIndex = 0;
        //
        // panelSearch
        //
        panelSearch.BackColor = Color.White;
        panelSearch.Controls.Add(btnTaiLai);
        panelSearch.Controls.Add(btnTimKiem);
        panelSearch.Controls.Add(txtSearch);
        panelSearch.Controls.Add(lblSearch);
        panelSearch.Dock = DockStyle.Fill;
        panelSearch.Margin = new Padding(0, 0, 0, 12);
        panelSearch.Padding = new Padding(12, 8, 12, 8);
        panelSearch.TabIndex = 0;
        //
        // lblSearch
        //
        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblSearch.ForeColor = Color.FromArgb(28, 43, 61);
        lblSearch.Location = new Point(14, 17);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(91, 17);
        lblSearch.TabIndex = 0;
        lblSearch.Text = "Tìm khách:";
        //
        // txtSearch
        //
        txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.Location = new Point(112, 11);
        txtSearch.MaxLength = 100;
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Nhập tên hoặc số điện thoại";
        txtSearch.Size = new Size(760, 24);
        txtSearch.TabIndex = 0;
        //
        // btnTimKiem
        //
        btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTimKiem.BackColor = Color.FromArgb(13, 71, 161);
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(884, 8);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(100, 32);
        btnTimKiem.TabIndex = 1;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        //
        // btnTaiLai
        //
        btnTaiLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTaiLai.BackColor = Color.FromArgb(230, 239, 250);
        btnTaiLai.FlatAppearance.BorderColor = Color.FromArgb(190, 211, 235);
        btnTaiLai.FlatStyle = FlatStyle.Flat;
        btnTaiLai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTaiLai.ForeColor = Color.FromArgb(13, 71, 161);
        btnTaiLai.Location = new Point(994, 8);
        btnTaiLai.Name = "btnTaiLai";
        btnTaiLai.Size = new Size(112, 32);
        btnTaiLai.TabIndex = 2;
        btnTaiLai.Text = "Tất cả";
        btnTaiLai.UseVisualStyleBackColor = false;
        btnTaiLai.Click += btnTaiLai_Click;
        //
        // layoutBody
        //
        layoutBody.ColumnCount = 2;
        layoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        layoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
        layoutBody.Controls.Add(grpCustomers, 0, 0);
        layoutBody.Controls.Add(layoutRight, 1, 0);
        layoutBody.Dock = DockStyle.Fill;
        layoutBody.Margin = new Padding(0);
        layoutBody.RowCount = 1;
        layoutBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutBody.TabIndex = 1;
        //
        // grpCustomers
        //
        grpCustomers.BackColor = Color.White;
        grpCustomers.Controls.Add(dgvCustomers);
        grpCustomers.Dock = DockStyle.Fill;
        grpCustomers.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpCustomers.ForeColor = Color.FromArgb(13, 71, 161);
        grpCustomers.Margin = new Padding(0, 0, 12, 0);
        grpCustomers.Padding = new Padding(10);
        grpCustomers.TabIndex = 0;
        grpCustomers.TabStop = false;
        grpCustomers.Text = "KHÁCH HÀNG";
        //
        // dgvCustomers
        //
        dgvCustomers.AllowUserToAddRows = false;
        dgvCustomers.AllowUserToDeleteRows = false;
        dgvCustomers.AllowUserToResizeRows = false;
        dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCustomers.BackgroundColor = Color.White;
        dgvCustomers.Dock = DockStyle.Fill;
        dgvCustomers.MultiSelect = false;
        dgvCustomers.Name = "dgvCustomers";
        dgvCustomers.ReadOnly = true;
        dgvCustomers.RowHeadersVisible = false;
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCustomers.TabIndex = 0;
        dgvCustomers.CellClick += dgvCustomers_CellClick;
        //
        // layoutRight
        //
        layoutRight.ColumnCount = 1;
        layoutRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRight.Controls.Add(grpContactInfo, 0, 0);
        layoutRight.Controls.Add(grpOrderHistory, 0, 1);
        layoutRight.Dock = DockStyle.Fill;
        layoutRight.Margin = new Padding(0);
        layoutRight.RowCount = 2;
        layoutRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 228F));
        layoutRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRight.TabIndex = 1;
        //
        // grpContactInfo
        //
        grpContactInfo.BackColor = Color.White;
        grpContactInfo.Controls.Add(layoutContact);
        grpContactInfo.Dock = DockStyle.Fill;
        grpContactInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpContactInfo.ForeColor = Color.FromArgb(13, 71, 161);
        grpContactInfo.Margin = new Padding(0, 0, 0, 12);
        grpContactInfo.Padding = new Padding(10);
        grpContactInfo.TabIndex = 0;
        grpContactInfo.TabStop = false;
        grpContactInfo.Text = "THÔNG TIN LIÊN HỆ";
        //
        // layoutContact
        //
        layoutContact.ColumnCount = 4;
        layoutContact.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        layoutContact.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
        layoutContact.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 78F));
        layoutContact.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
        layoutContact.Controls.Add(lblFullName, 0, 0);
        layoutContact.Controls.Add(txtFullName, 1, 0);
        layoutContact.Controls.Add(lblPhoneNumber, 0, 1);
        layoutContact.Controls.Add(txtPhoneNumber, 1, 1);
        layoutContact.Controls.Add(lblEmail, 2, 1);
        layoutContact.Controls.Add(txtEmail, 3, 1);
        layoutContact.Controls.Add(lblAddress, 0, 2);
        layoutContact.Controls.Add(txtAddress, 1, 2);
        layoutContact.Controls.Add(flowActions, 0, 3);
        layoutContact.Dock = DockStyle.Fill;
        layoutContact.Margin = new Padding(0);
        layoutContact.Padding = new Padding(6, 8, 6, 0);
        layoutContact.RowCount = 4;
        layoutContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        layoutContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        layoutContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        layoutContact.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutContact.SetColumnSpan(txtFullName, 3);
        layoutContact.SetColumnSpan(txtAddress, 3);
        layoutContact.SetColumnSpan(flowActions, 4);
        layoutContact.TabIndex = 0;
        //
        // lblFullName
        //
        lblFullName.Dock = DockStyle.Fill;
        lblFullName.Font = new Font("Segoe UI", 9F);
        lblFullName.ForeColor = Color.FromArgb(45, 55, 72);
        lblFullName.Text = "Họ và tên:";
        lblFullName.TextAlign = ContentAlignment.MiddleLeft;
        //
        // txtFullName
        //
        txtFullName.Dock = DockStyle.Fill;
        txtFullName.Font = new Font("Segoe UI", 9.5F);
        txtFullName.MaxLength = 100;
        txtFullName.Margin = new Padding(3, 5, 10, 5);
        //
        // lblPhoneNumber
        //
        lblPhoneNumber.Dock = DockStyle.Fill;
        lblPhoneNumber.Font = new Font("Segoe UI", 9F);
        lblPhoneNumber.ForeColor = Color.FromArgb(45, 55, 72);
        lblPhoneNumber.Text = "Điện thoại:";
        lblPhoneNumber.TextAlign = ContentAlignment.MiddleLeft;
        //
        // txtPhoneNumber
        //
        txtPhoneNumber.Dock = DockStyle.Fill;
        txtPhoneNumber.Font = new Font("Segoe UI", 9.5F);
        txtPhoneNumber.MaxLength = 20;
        txtPhoneNumber.Margin = new Padding(3, 5, 10, 5);
        //
        // lblEmail
        //
        lblEmail.Dock = DockStyle.Fill;
        lblEmail.Font = new Font("Segoe UI", 9F);
        lblEmail.ForeColor = Color.FromArgb(45, 55, 72);
        lblEmail.Text = "Email:";
        lblEmail.TextAlign = ContentAlignment.MiddleLeft;
        //
        // txtEmail
        //
        txtEmail.Dock = DockStyle.Fill;
        txtEmail.Font = new Font("Segoe UI", 9.5F);
        txtEmail.MaxLength = 100;
        txtEmail.Margin = new Padding(3, 5, 0, 5);
        //
        // lblAddress
        //
        lblAddress.Dock = DockStyle.Fill;
        lblAddress.Font = new Font("Segoe UI", 9F);
        lblAddress.ForeColor = Color.FromArgb(45, 55, 72);
        lblAddress.Text = "Địa chỉ:";
        lblAddress.TextAlign = ContentAlignment.MiddleLeft;
        //
        // txtAddress
        //
        txtAddress.Dock = DockStyle.Fill;
        txtAddress.Font = new Font("Segoe UI", 9.5F);
        txtAddress.MaxLength = 255;
        txtAddress.Margin = new Padding(3, 5, 0, 5);
        //
        // flowActions
        //
        flowActions.Controls.Add(btnSave);
        flowActions.Controls.Add(btnBoChon);
        flowActions.Dock = DockStyle.Fill;
        flowActions.FlowDirection = FlowDirection.RightToLeft;
        flowActions.Margin = new Padding(0, 0, 0, 0);
        flowActions.Padding = new Padding(0, 2, 0, 0);
        flowActions.WrapContents = false;
        flowActions.TabIndex = 1;
        //
        // btnSave
        //
        btnSave.BackColor = Color.FromArgb(13, 71, 161);
        btnSave.Enabled = false;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Margin = new Padding(8, 0, 0, 0);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(138, 34);
        btnSave.TabIndex = 0;
        btnSave.Text = "Lưu thay đổi";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += btnSave_Click;
        //
        // btnBoChon
        //
        btnBoChon.BackColor = Color.FromArgb(230, 239, 250);
        btnBoChon.FlatAppearance.BorderColor = Color.FromArgb(190, 211, 235);
        btnBoChon.FlatStyle = FlatStyle.Flat;
        btnBoChon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnBoChon.ForeColor = Color.FromArgb(13, 71, 161);
        btnBoChon.Margin = new Padding(8, 0, 0, 0);
        btnBoChon.Name = "btnBoChon";
        btnBoChon.Size = new Size(110, 34);
        btnBoChon.TabIndex = 1;
        btnBoChon.Text = "Bỏ chọn";
        btnBoChon.UseVisualStyleBackColor = false;
        btnBoChon.Click += btnBoChon_Click;
        //
        // grpOrderHistory
        //
        grpOrderHistory.BackColor = Color.White;
        grpOrderHistory.Controls.Add(dgvOrderHistory);
        grpOrderHistory.Dock = DockStyle.Fill;
        grpOrderHistory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpOrderHistory.ForeColor = Color.FromArgb(13, 71, 161);
        grpOrderHistory.Margin = new Padding(0);
        grpOrderHistory.Padding = new Padding(10);
        grpOrderHistory.TabIndex = 1;
        grpOrderHistory.TabStop = false;
        grpOrderHistory.Text = "LỊCH SỬ ĐƠN HÀNG";
        //
        // dgvOrderHistory
        //
        dgvOrderHistory.AllowUserToAddRows = false;
        dgvOrderHistory.AllowUserToDeleteRows = false;
        dgvOrderHistory.AllowUserToResizeRows = false;
        dgvOrderHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrderHistory.BackgroundColor = Color.White;
        dgvOrderHistory.Dock = DockStyle.Fill;
        dgvOrderHistory.MultiSelect = false;
        dgvOrderHistory.Name = "dgvOrderHistory";
        dgvOrderHistory.ReadOnly = true;
        dgvOrderHistory.RowHeadersVisible = false;
        dgvOrderHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrderHistory.TabIndex = 0;
        //
        // FormKhachHang
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1180, 760);
        Controls.Add(layoutRoot);
        MinimumSize = new Size(900, 600);
        Name = "FormKhachHang";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý khách hàng";
        layoutRoot.ResumeLayout(false);
        panelSearch.ResumeLayout(false);
        panelSearch.PerformLayout();
        layoutBody.ResumeLayout(false);
        grpCustomers.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
        layoutRight.ResumeLayout(false);
        grpContactInfo.ResumeLayout(false);
        layoutContact.ResumeLayout(false);
        layoutContact.PerformLayout();
        flowActions.ResumeLayout(false);
        grpOrderHistory.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvOrderHistory).EndInit();
        ResumeLayout(false);
    }
}
