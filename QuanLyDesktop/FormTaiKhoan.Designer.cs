#nullable enable

namespace QuanLyDesktop;

partial class FormTaiKhoan
{
    private System.ComponentModel.IContainer? components;
    private TableLayoutPanel layoutRoot = null!;
    private Panel panelHeader = null!;
    private Label lblTitle = null!;
    private Label lblSubtitle = null!;
    private TableLayoutPanel layoutMain = null!;
    private GroupBox grpUsers = null!;
    private TableLayoutPanel layoutUsers = null!;
    private FlowLayoutPanel flowUsersHeader = null!;
    private Label lblUsersTitle = null!;
    private Button btnRefresh = null!;
    private DataGridView dgvUsers = null!;
    private GroupBox grpEditor = null!;
    private TableLayoutPanel layoutEditor = null!;
    private Label lblUsername = null!;
    private TextBox txtUsername = null!;
    private Label lblFullName = null!;
    private TextBox txtFullName = null!;
    private Label lblRole = null!;
    private ComboBox cboRole = null!;
    private Label lblPassword = null!;
    private TextBox txtPassword = null!;
    private Label lblConfirmPassword = null!;
    private TextBox txtConfirmPassword = null!;
    private CheckBox chkIsActive = null!;
    private Label lblPasswordHint = null!;
    private FlowLayoutPanel flowButtons = null!;
    private Button btnNew = null!;
    private Button btnSave = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        layoutRoot = new TableLayoutPanel();
        panelHeader = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        layoutMain = new TableLayoutPanel();
        grpUsers = new GroupBox();
        layoutUsers = new TableLayoutPanel();
        flowUsersHeader = new FlowLayoutPanel();
        lblUsersTitle = new Label();
        btnRefresh = new Button();
        dgvUsers = new DataGridView();
        grpEditor = new GroupBox();
        layoutEditor = new TableLayoutPanel();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblRole = new Label();
        cboRole = new ComboBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        lblConfirmPassword = new Label();
        txtConfirmPassword = new TextBox();
        chkIsActive = new CheckBox();
        lblPasswordHint = new Label();
        flowButtons = new FlowLayoutPanel();
        btnNew = new Button();
        btnSave = new Button();
        layoutRoot.SuspendLayout();
        panelHeader.SuspendLayout();
        layoutMain.SuspendLayout();
        grpUsers.SuspendLayout();
        layoutUsers.SuspendLayout();
        flowUsersHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
        grpEditor.SuspendLayout();
        layoutEditor.SuspendLayout();
        flowButtons.SuspendLayout();
        SuspendLayout();
        //
        // layoutRoot
        //
        layoutRoot.BackColor = Color.FromArgb(245, 247, 250);
        layoutRoot.ColumnCount = 1;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRoot.Controls.Add(panelHeader, 0, 0);
        layoutRoot.Controls.Add(layoutMain, 0, 1);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Padding = new Padding(20);
        layoutRoot.RowCount = 2;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRoot.TabIndex = 0;
        //
        // panelHeader
        //
        panelHeader.BackColor = Color.White;
        panelHeader.Controls.Add(lblSubtitle);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Dock = DockStyle.Fill;
        panelHeader.Margin = new Padding(0, 0, 0, 14);
        panelHeader.Padding = new Padding(18, 8, 18, 6);
        panelHeader.TabIndex = 0;
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(27, 54, 93);
        lblTitle.Location = new Point(18, 8);
        lblTitle.Text = "Quản lý tài khoản";
        //
        // lblSubtitle
        //
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F);
        lblSubtitle.ForeColor = Color.FromArgb(90, 102, 117);
        lblSubtitle.Location = new Point(21, 51);
        lblSubtitle.Text = "Tạo tài khoản, phân quyền và quản lý trạng thái hoạt động.";
        //
        // layoutMain
        //
        layoutMain.ColumnCount = 2;
        layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
        layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        layoutMain.Controls.Add(grpUsers, 0, 0);
        layoutMain.Controls.Add(grpEditor, 1, 0);
        layoutMain.Dock = DockStyle.Fill;
        layoutMain.Margin = new Padding(0);
        layoutMain.RowCount = 1;
        layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutMain.TabIndex = 1;
        //
        // grpUsers
        //
        grpUsers.BackColor = Color.White;
        grpUsers.Controls.Add(layoutUsers);
        grpUsers.Dock = DockStyle.Fill;
        grpUsers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpUsers.ForeColor = Color.FromArgb(27, 54, 93);
        grpUsers.Margin = new Padding(0, 0, 10, 0);
        grpUsers.Padding = new Padding(12);
        grpUsers.Text = "Danh sách tài khoản";
        //
        // layoutUsers
        //
        layoutUsers.ColumnCount = 1;
        layoutUsers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutUsers.Controls.Add(flowUsersHeader, 0, 0);
        layoutUsers.Controls.Add(dgvUsers, 0, 1);
        layoutUsers.Dock = DockStyle.Fill;
        layoutUsers.Margin = new Padding(0);
        layoutUsers.RowCount = 2;
        layoutUsers.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        layoutUsers.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        //
        // flowUsersHeader
        //
        flowUsersHeader.Controls.Add(lblUsersTitle);
        flowUsersHeader.Controls.Add(btnRefresh);
        flowUsersHeader.Dock = DockStyle.Fill;
        flowUsersHeader.FlowDirection = FlowDirection.RightToLeft;
        flowUsersHeader.WrapContents = false;
        //
        // btnRefresh
        //
        btnRefresh.BackColor = Color.White;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        btnRefresh.ForeColor = Color.FromArgb(27, 54, 93);
        btnRefresh.Margin = new Padding(0, 0, 0, 5);
        btnRefresh.Size = new Size(92, 32);
        btnRefresh.Text = "Tải lại";
        btnRefresh.UseVisualStyleBackColor = false;
        //
        // lblUsersTitle
        //
        lblUsersTitle.AutoSize = true;
        lblUsersTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        lblUsersTitle.ForeColor = Color.FromArgb(90, 102, 117);
        lblUsersTitle.Margin = new Padding(0, 7, 0, 0);
        lblUsersTitle.Text = "Chọn một tài khoản để chỉnh sửa";
        //
        // dgvUsers
        //
        dgvUsers.AllowUserToAddRows = false;
        dgvUsers.AllowUserToDeleteRows = false;
        dgvUsers.AllowUserToResizeRows = false;
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvUsers.BackgroundColor = Color.White;
        dgvUsers.BorderStyle = BorderStyle.None;
        dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvUsers.ColumnHeadersHeight = 38;
        dgvUsers.Dock = DockStyle.Fill;
        dgvUsers.EnableHeadersVisualStyles = false;
        dgvUsers.MultiSelect = false;
        dgvUsers.ReadOnly = true;
        dgvUsers.RowHeadersVisible = false;
        dgvUsers.RowTemplate.Height = 36;
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
        dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(60, 72, 88);
        dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        dgvUsers.DefaultCellStyle.ForeColor = Color.FromArgb(28, 43, 61);
        dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
        dgvUsers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(28, 43, 61);
        //
        // grpEditor
        //
        grpEditor.BackColor = Color.White;
        grpEditor.Controls.Add(layoutEditor);
        grpEditor.Dock = DockStyle.Fill;
        grpEditor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpEditor.ForeColor = Color.FromArgb(27, 54, 93);
        grpEditor.Margin = new Padding(10, 0, 0, 0);
        grpEditor.Padding = new Padding(14);
        grpEditor.Text = "Thông tin tài khoản";
        //
        // layoutEditor
        //
        layoutEditor.ColumnCount = 2;
        layoutEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 126F));
        layoutEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutEditor.Controls.Add(lblUsername, 0, 0);
        layoutEditor.Controls.Add(txtUsername, 1, 0);
        layoutEditor.Controls.Add(lblFullName, 0, 1);
        layoutEditor.Controls.Add(txtFullName, 1, 1);
        layoutEditor.Controls.Add(lblRole, 0, 2);
        layoutEditor.Controls.Add(cboRole, 1, 2);
        layoutEditor.Controls.Add(lblPassword, 0, 3);
        layoutEditor.Controls.Add(txtPassword, 1, 3);
        layoutEditor.Controls.Add(lblConfirmPassword, 0, 4);
        layoutEditor.Controls.Add(txtConfirmPassword, 1, 4);
        layoutEditor.Controls.Add(chkIsActive, 0, 5);
        layoutEditor.Controls.Add(lblPasswordHint, 0, 6);
        layoutEditor.Controls.Add(flowButtons, 0, 7);
        layoutEditor.SetColumnSpan(chkIsActive, 2);
        layoutEditor.SetColumnSpan(lblPasswordHint, 2);
        layoutEditor.SetColumnSpan(flowButtons, 2);
        layoutEditor.Dock = DockStyle.Fill;
        layoutEditor.Margin = new Padding(0);
        layoutEditor.RowCount = 8;
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        //
        // lblUsername
        //
        lblUsername.Anchor = AnchorStyles.Left;
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9.5F);
        lblUsername.ForeColor = Color.FromArgb(51, 65, 85);
        lblUsername.Text = "Tên đăng nhập";
        //
        // txtUsername
        //
        txtUsername.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtUsername.Font = new Font("Segoe UI", 9.5F);
        txtUsername.MaxLength = 50;
        txtUsername.Size = new Size(260, 28);
        //
        // lblFullName
        //
        lblFullName.Anchor = AnchorStyles.Left;
        lblFullName.AutoSize = true;
        lblFullName.Font = new Font("Segoe UI", 9.5F);
        lblFullName.ForeColor = Color.FromArgb(51, 65, 85);
        lblFullName.Text = "Họ và tên";
        //
        // txtFullName
        //
        txtFullName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtFullName.Font = new Font("Segoe UI", 9.5F);
        txtFullName.MaxLength = 100;
        txtFullName.Size = new Size(260, 28);
        //
        // lblRole
        //
        lblRole.Anchor = AnchorStyles.Left;
        lblRole.AutoSize = true;
        lblRole.Font = new Font("Segoe UI", 9.5F);
        lblRole.ForeColor = Color.FromArgb(51, 65, 85);
        lblRole.Text = "Vai trò";
        //
        // cboRole
        //
        cboRole.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRole.Font = new Font("Segoe UI", 9.5F);
        cboRole.Size = new Size(260, 29);
        //
        // lblPassword
        //
        lblPassword.Anchor = AnchorStyles.Left;
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9.5F);
        lblPassword.ForeColor = Color.FromArgb(51, 65, 85);
        lblPassword.Text = "Mật khẩu mới";
        //
        // txtPassword
        //
        txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtPassword.Font = new Font("Segoe UI", 9.5F);
        txtPassword.MaxLength = 128;
        txtPassword.Size = new Size(260, 28);
        txtPassword.UseSystemPasswordChar = true;
        //
        // lblConfirmPassword
        //
        lblConfirmPassword.Anchor = AnchorStyles.Left;
        lblConfirmPassword.AutoSize = true;
        lblConfirmPassword.Font = new Font("Segoe UI", 9F);
        lblConfirmPassword.ForeColor = Color.FromArgb(51, 65, 85);
        lblConfirmPassword.Text = "Xác nhận mật khẩu";
        //
        // txtConfirmPassword
        //
        txtConfirmPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtConfirmPassword.Font = new Font("Segoe UI", 9.5F);
        txtConfirmPassword.MaxLength = 128;
        txtConfirmPassword.Size = new Size(260, 28);
        txtConfirmPassword.UseSystemPasswordChar = true;
        //
        // chkIsActive
        //
        chkIsActive.Anchor = AnchorStyles.Left;
        chkIsActive.AutoSize = true;
        chkIsActive.Font = new Font("Segoe UI", 9.5F);
        chkIsActive.ForeColor = Color.FromArgb(51, 65, 85);
        chkIsActive.Text = "Tài khoản đang hoạt động";
        //
        // lblPasswordHint
        //
        lblPasswordHint.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblPasswordHint.AutoSize = true;
        lblPasswordHint.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblPasswordHint.ForeColor = Color.FromArgb(90, 102, 117);
        lblPasswordHint.Text = "Mật khẩu được băm PBKDF2 trước khi lưu.";
        //
        // flowButtons
        //
        flowButtons.Controls.Add(btnNew);
        flowButtons.Controls.Add(btnSave);
        flowButtons.Dock = DockStyle.Fill;
        flowButtons.FlowDirection = FlowDirection.RightToLeft;
        flowButtons.WrapContents = false;
        //
        // btnSave
        //
        btnSave.BackColor = Color.FromArgb(27, 54, 93);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Margin = new Padding(8, 3, 0, 3);
        btnSave.Size = new Size(142, 38);
        btnSave.Text = "Tạo tài khoản";
        btnSave.UseVisualStyleBackColor = false;
        //
        // btnNew
        //
        btnNew.BackColor = Color.White;
        btnNew.FlatStyle = FlatStyle.Flat;
        btnNew.Font = new Font("Segoe UI", 9F);
        btnNew.ForeColor = Color.FromArgb(27, 54, 93);
        btnNew.Margin = new Padding(8, 3, 0, 3);
        btnNew.Size = new Size(110, 38);
        btnNew.Text = "Làm mới";
        btnNew.UseVisualStyleBackColor = false;
        //
        // FormTaiKhoan
        //
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1180, 720);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(1000, 640);
        Name = "FormTaiKhoan";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý tài khoản";
        layoutRoot.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        layoutMain.ResumeLayout(false);
        grpUsers.ResumeLayout(false);
        layoutUsers.ResumeLayout(false);
        flowUsersHeader.ResumeLayout(false);
        flowUsersHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
        grpEditor.ResumeLayout(false);
        layoutEditor.ResumeLayout(false);
        layoutEditor.PerformLayout();
        flowButtons.ResumeLayout(false);
        ResumeLayout(false);
    }
}
