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
    private Button btnBienThe;
    private GroupBox grpDanhSach;
    private TextBox txtTimKiem;
    private Button btnTimKiem;
    private Button btnHienTatCa;
    private DataGridView dgvSanPham;
    private Button btnDangXuat;
    private Button btnDoiMatKhau;
    private TableLayoutPanel layoutRoot;
    private Panel panelSidebar;
    private Label lblBrand;
    private Label lblBrandSubtitle;
    private Button btnTrangChu;
    private Button btnSanPham;
    private TableLayoutPanel layoutWorkspace;
    private Panel panelHeader;
    private Panel panelContent;
    private Panel panelDashboard;
    private TableLayoutPanel layoutDashboard;
    private Panel panelWelcome;
    private Label lblDashboardGreeting;
    private Label lblDashboardDescription;
    private TableLayoutPanel layoutStats;
    private DashboardStatCard panelStatProducts;
    private DashboardStatCard panelStatOrders;
    private DashboardStatCard panelStatRevenue;
    private Label lblStatProductsTitle;
    private Label lblStatProductsValue;
    private Label lblStatOrdersTitle;
    private Label lblStatOrdersValue;
    private Label lblStatRevenueTitle;
    private Label lblStatRevenueValue;
    private Panel panelOverview;
    private Label lblOverviewTitle;
    private Label lblOverviewBody;
    private Label lblDashboardStatus;
    private FlowLayoutPanel layoutQuickActions;
    private Button btnQuickProducts;
    private Button btnQuickOrders;
    private Button btnQuickInventory;
    private Button btnQuickCategories;

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
        panelSidebar = new Panel();
        lblBrand = new Label();
        lblBrandSubtitle = new Label();
        btnTrangChu = new Button();
        btnSanPham = new Button();
        layoutWorkspace = new TableLayoutPanel();
        panelHeader = new Panel();
        panelContent = new Panel();
        panelDashboard = new Panel();
        layoutDashboard = new TableLayoutPanel();
        panelWelcome = new Panel();
        lblDashboardGreeting = new Label();
        lblDashboardDescription = new Label();
        layoutStats = new TableLayoutPanel();
        panelStatProducts = new DashboardStatCard();
        panelStatOrders = new DashboardStatCard();
        panelStatRevenue = new DashboardStatCard();
        lblStatProductsTitle = new Label();
        lblStatProductsValue = new Label();
        lblStatOrdersTitle = new Label();
        lblStatOrdersValue = new Label();
        lblStatRevenueTitle = new Label();
        lblStatRevenueValue = new Label();
        panelOverview = new Panel();
        lblOverviewTitle = new Label();
        lblOverviewBody = new Label();
        lblDashboardStatus = new Label();
        layoutQuickActions = new FlowLayoutPanel();
        btnQuickProducts = new Button();
        btnQuickOrders = new Button();
        btnQuickInventory = new Button();
        btnQuickCategories = new Button();
        lblTitle = new Label();
        lblXinChao = new Label();
        grpThongTin = new GroupBox();
        btnLamMoi = new Button();
        btnBienThe = new Button();
        btnXoa = new Button();
        btnSua = new Button();
        btnThem = new Button();
        txtMoTa = new TextBox();
        lblMoTa = new Label();
        txtGiaBan = new TextBox();
        lblGiaBan = new Label();
        txtGiaGoc = new TextBox();
        lblGiaGoc = new Label();
        txtImageUrl = new TextBox();
        lblImageUrl = new Label();
        chkIsFeatured = new CheckBox();
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
        txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        btnDangXuat = new Button();
        btnDoiMatKhau = new Button();
        button1 = new Button();
        btnDanhMuc = new Button();
        btnNhapKho = new Button();
        grpThongTin.SuspendLayout();
        grpDanhSach.SuspendLayout();
        layoutRoot.SuspendLayout();
        panelSidebar.SuspendLayout();
        layoutWorkspace.SuspendLayout();
        panelHeader.SuspendLayout();
        panelContent.SuspendLayout();
        panelDashboard.SuspendLayout();
        layoutDashboard.SuspendLayout();
        panelWelcome.SuspendLayout();
        layoutStats.SuspendLayout();
        panelStatProducts.SuspendLayout();
        panelStatOrders.SuspendLayout();
        panelStatRevenue.SuspendLayout();
        panelOverview.SuspendLayout();
        layoutQuickActions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).BeginInit();
        SuspendLayout();
        // 
        // layoutRoot
        // 
        layoutRoot.BackColor = Color.FromArgb(244, 247, 251);
        layoutRoot.ColumnCount = 2;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 226F));
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRoot.Controls.Add(panelSidebar, 0, 0);
        layoutRoot.Controls.Add(layoutWorkspace, 1, 0);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Margin = new Padding(0);
        layoutRoot.RowCount = 1;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRoot.TabIndex = 0;
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.FromArgb(17, 43, 72);
        panelSidebar.Controls.Add(btnDanhMuc);
        panelSidebar.Controls.Add(btnNhapKho);
        panelSidebar.Controls.Add(button1);
        panelSidebar.Controls.Add(btnSanPham);
        panelSidebar.Controls.Add(btnTrangChu);
        panelSidebar.Controls.Add(lblBrandSubtitle);
        panelSidebar.Controls.Add(lblBrand);
        panelSidebar.Dock = DockStyle.Fill;
        panelSidebar.TabIndex = 0;
        // 
        // lblBrand
        // 
        lblBrand.AutoSize = false;
        lblBrand.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblBrand.ForeColor = Color.White;
        lblBrand.Location = new Point(20, 28);
        lblBrand.Name = "lblBrand";
        lblBrand.Size = new Size(194, 28);
        lblBrand.TabIndex = 0;
        lblBrand.Text = "FASHION STORE";
        // 
        // lblBrandSubtitle
        // 
        lblBrandSubtitle.AutoSize = false;
        lblBrandSubtitle.Font = new Font("Segoe UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblBrandSubtitle.ForeColor = Color.FromArgb(171, 193, 218);
        lblBrandSubtitle.Location = new Point(20, 59);
        lblBrandSubtitle.Name = "lblBrandSubtitle";
        lblBrandSubtitle.Size = new Size(194, 34);
        lblBrandSubtitle.TabIndex = 1;
        lblBrandSubtitle.Text = "QUẢN TRỊ CỬA HÀNG\r\nTHỜI TRANG";
        // 
        // btnTrangChu
        // 
        btnTrangChu.BackColor = Color.FromArgb(35, 91, 150);
        btnTrangChu.Cursor = Cursors.Hand;
        btnTrangChu.FlatAppearance.BorderSize = 0;
        btnTrangChu.FlatStyle = FlatStyle.Flat;
        btnTrangChu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnTrangChu.ForeColor = Color.White;
        btnTrangChu.Location = new Point(12, 126);
        btnTrangChu.Name = "btnTrangChu";
        btnTrangChu.Size = new Size(202, 44);
        btnTrangChu.TabIndex = 0;
        btnTrangChu.Text = "   Trang chủ";
        btnTrangChu.TextAlign = ContentAlignment.MiddleLeft;
        btnTrangChu.UseVisualStyleBackColor = false;
        btnTrangChu.Click += btnTrangChu_Click;
        // 
        // btnSanPham
        // 
        btnSanPham.BackColor = Color.FromArgb(17, 43, 72);
        btnSanPham.Cursor = Cursors.Hand;
        btnSanPham.FlatAppearance.BorderSize = 0;
        btnSanPham.FlatStyle = FlatStyle.Flat;
        btnSanPham.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        btnSanPham.ForeColor = Color.FromArgb(223, 232, 243);
        btnSanPham.Location = new Point(12, 178);
        btnSanPham.Name = "btnSanPham";
        btnSanPham.Size = new Size(202, 44);
        btnSanPham.TabIndex = 1;
        btnSanPham.Text = "   Quản lý sản phẩm";
        btnSanPham.TextAlign = ContentAlignment.MiddleLeft;
        btnSanPham.UseVisualStyleBackColor = false;
        btnSanPham.Click += btnSanPham_Click;
        // 
        // layoutWorkspace
        // 
        layoutWorkspace.ColumnCount = 1;
        layoutWorkspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutWorkspace.Controls.Add(panelHeader, 0, 0);
        layoutWorkspace.Controls.Add(panelContent, 0, 1);
        layoutWorkspace.Dock = DockStyle.Fill;
        layoutWorkspace.Margin = new Padding(0);
        layoutWorkspace.RowCount = 2;
        layoutWorkspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
        layoutWorkspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutWorkspace.TabIndex = 1;
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.White;
        panelHeader.Controls.Add(btnDoiMatKhau);
        panelHeader.Controls.Add(btnDangXuat);
        panelHeader.Controls.Add(lblXinChao);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Dock = DockStyle.Fill;
        panelHeader.Padding = new Padding(28, 12, 24, 10);
        panelHeader.TabIndex = 0;
        // 
        // panelContent
        // 
        panelContent.BackColor = Color.FromArgb(244, 247, 251);
        panelContent.Controls.Add(panelDashboard);
        panelContent.Controls.Add(grpDanhSach);
        panelContent.Controls.Add(grpThongTin);
        panelContent.Dock = DockStyle.Fill;
        panelContent.Padding = new Padding(24);
        panelContent.TabIndex = 1;
        // 
        // panelDashboard
        // 
        panelDashboard.BackColor = Color.FromArgb(244, 247, 251);
        panelDashboard.Controls.Add(layoutDashboard);
        panelDashboard.Dock = DockStyle.Fill;
        panelDashboard.TabIndex = 0;
        // 
        // layoutDashboard
        // 
        layoutDashboard.ColumnCount = 1;
        layoutDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutDashboard.Controls.Add(panelWelcome, 0, 0);
        layoutDashboard.Controls.Add(layoutStats, 0, 1);
        layoutDashboard.Controls.Add(panelOverview, 0, 2);
        layoutDashboard.Dock = DockStyle.Fill;
        layoutDashboard.Padding = new Padding(0, 0, 0, 4);
        layoutDashboard.RowCount = 3;
        layoutDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
        layoutDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 158F));
        layoutDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutDashboard.TabIndex = 0;
        // 
        // panelWelcome
        // 
        panelWelcome.BackColor = Color.White;
        panelWelcome.Controls.Add(lblDashboardDescription);
        panelWelcome.Controls.Add(lblDashboardGreeting);
        panelWelcome.Dock = DockStyle.Fill;
        panelWelcome.Margin = new Padding(0, 0, 0, 16);
        panelWelcome.Padding = new Padding(22, 14, 20, 12);
        panelWelcome.TabIndex = 0;
        // 
        // lblDashboardGreeting
        // 
        lblDashboardGreeting.AutoSize = true;
        lblDashboardGreeting.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblDashboardGreeting.ForeColor = Color.FromArgb(28, 43, 61);
        lblDashboardGreeting.Location = new Point(22, 16);
        lblDashboardGreeting.Name = "lblDashboardGreeting";
        lblDashboardGreeting.Size = new Size(315, 37);
        lblDashboardGreeting.TabIndex = 0;
        lblDashboardGreeting.Text = "Chào mừng trở lại";
        // 
        // lblDashboardDescription
        // 
        lblDashboardDescription.AutoSize = true;
        lblDashboardDescription.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblDashboardDescription.ForeColor = Color.FromArgb(99, 112, 129);
        lblDashboardDescription.Location = new Point(26, 64);
        lblDashboardDescription.Name = "lblDashboardDescription";
        lblDashboardDescription.Size = new Size(393, 19);
        lblDashboardDescription.TabIndex = 1;
        lblDashboardDescription.Text = "Tổng quan hoạt động cửa hàng, cập nhật trực tiếp từ dữ liệu.";
        // 
        // layoutStats
        // 
        layoutStats.ColumnCount = 3;
        layoutStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
        layoutStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
        layoutStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
        layoutStats.Controls.Add(panelStatProducts, 0, 0);
        layoutStats.Controls.Add(panelStatOrders, 1, 0);
        layoutStats.Controls.Add(panelStatRevenue, 2, 0);
        layoutStats.Dock = DockStyle.Fill;
        layoutStats.Margin = new Padding(0, 0, 0, 16);
        layoutStats.RowCount = 1;
        layoutStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutStats.TabIndex = 1;
        // 
        // panelStatProducts
        // 
        panelStatProducts.BackColor = Color.White;
        panelStatProducts.AccentColor = Color.FromArgb(24, 105, 213);
        panelStatProducts.Controls.Add(lblStatProductsValue);
        panelStatProducts.Controls.Add(lblStatProductsTitle);
        panelStatProducts.Dock = DockStyle.Fill;
        panelStatProducts.Margin = new Padding(0, 0, 10, 0);
        panelStatProducts.Padding = new Padding(20);
        panelStatProducts.TabIndex = 0;
        // 
        // lblStatProductsTitle
        // 
        lblStatProductsTitle.AutoSize = true;
        lblStatProductsTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStatProductsTitle.ForeColor = Color.FromArgb(99, 112, 129);
        lblStatProductsTitle.Location = new Point(43, 20);
        lblStatProductsTitle.Name = "lblStatProductsTitle";
        lblStatProductsTitle.Size = new Size(245, 17);
        lblStatProductsTitle.TabIndex = 0;
        lblStatProductsTitle.Text = "SẢN PHẨM ĐANG KINH DOANH";
        // 
        // lblStatProductsValue
        // 
        lblStatProductsValue.AutoSize = true;
        lblStatProductsValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStatProductsValue.ForeColor = Color.FromArgb(24, 105, 213);
        lblStatProductsValue.Location = new Point(42, 55);
        lblStatProductsValue.Name = "lblStatProductsValue";
        lblStatProductsValue.Size = new Size(47, 48);
        lblStatProductsValue.TabIndex = 1;
        lblStatProductsValue.Text = "0";
        // 
        // panelStatOrders
        // 
        panelStatOrders.BackColor = Color.White;
        panelStatOrders.AccentColor = Color.FromArgb(20, 151, 116);
        panelStatOrders.Controls.Add(lblStatOrdersValue);
        panelStatOrders.Controls.Add(lblStatOrdersTitle);
        panelStatOrders.Dock = DockStyle.Fill;
        panelStatOrders.Margin = new Padding(5, 0, 5, 0);
        panelStatOrders.Padding = new Padding(20);
        panelStatOrders.TabIndex = 1;
        // 
        // lblStatOrdersTitle
        // 
        lblStatOrdersTitle.AutoSize = true;
        lblStatOrdersTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStatOrdersTitle.ForeColor = Color.FromArgb(99, 112, 129);
        lblStatOrdersTitle.Location = new Point(43, 20);
        lblStatOrdersTitle.Name = "lblStatOrdersTitle";
        lblStatOrdersTitle.Size = new Size(230, 17);
        lblStatOrdersTitle.TabIndex = 0;
        lblStatOrdersTitle.Text = "ĐƠN HÀNG HÔM NAY";
        // 
        // lblStatOrdersValue
        // 
        lblStatOrdersValue.AutoSize = true;
        lblStatOrdersValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStatOrdersValue.ForeColor = Color.FromArgb(20, 151, 116);
        lblStatOrdersValue.Location = new Point(42, 55);
        lblStatOrdersValue.Name = "lblStatOrdersValue";
        lblStatOrdersValue.Size = new Size(47, 48);
        lblStatOrdersValue.TabIndex = 1;
        lblStatOrdersValue.Text = "0";
        // 
        // panelStatRevenue
        // 
        panelStatRevenue.BackColor = Color.White;
        panelStatRevenue.AccentColor = Color.FromArgb(192, 117, 29);
        panelStatRevenue.Controls.Add(lblStatRevenueValue);
        panelStatRevenue.Controls.Add(lblStatRevenueTitle);
        panelStatRevenue.Dock = DockStyle.Fill;
        panelStatRevenue.Margin = new Padding(10, 0, 0, 0);
        panelStatRevenue.Padding = new Padding(20);
        panelStatRevenue.TabIndex = 2;
        // 
        // lblStatRevenueTitle
        // 
        lblStatRevenueTitle.AutoSize = true;
        lblStatRevenueTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStatRevenueTitle.ForeColor = Color.FromArgb(99, 112, 129);
        lblStatRevenueTitle.Location = new Point(43, 20);
        lblStatRevenueTitle.Name = "lblStatRevenueTitle";
        lblStatRevenueTitle.Size = new Size(235, 17);
        lblStatRevenueTitle.TabIndex = 0;
        lblStatRevenueTitle.Text = "DOANH THU HÔM NAY";
        // 
        // lblStatRevenueValue
        // 
        lblStatRevenueValue.AutoSize = true;
        lblStatRevenueValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStatRevenueValue.ForeColor = Color.FromArgb(192, 117, 29);
        lblStatRevenueValue.Location = new Point(42, 55);
        lblStatRevenueValue.Name = "lblStatRevenueValue";
        lblStatRevenueValue.Size = new Size(42, 42);
        lblStatRevenueValue.TabIndex = 1;
        lblStatRevenueValue.Text = "0";
        // 
        // panelOverview
        // 
        panelOverview.BackColor = Color.White;
        panelOverview.Controls.Add(lblDashboardStatus);
        panelOverview.Controls.Add(layoutQuickActions);
        panelOverview.Controls.Add(lblOverviewBody);
        panelOverview.Controls.Add(lblOverviewTitle);
        panelOverview.Dock = DockStyle.Top;
        panelOverview.Height = 204;
        panelOverview.Padding = new Padding(22, 16, 22, 16);
        panelOverview.TabIndex = 2;
        //
        // lblOverviewTitle
        // 
        lblOverviewTitle.AutoSize = true;
        lblOverviewTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblOverviewTitle.ForeColor = Color.FromArgb(28, 43, 61);
        lblOverviewTitle.Location = new Point(22, 20);
        lblOverviewTitle.Name = "lblOverviewTitle";
        lblOverviewTitle.Size = new Size(144, 21);
        lblOverviewTitle.TabIndex = 0;
        lblOverviewTitle.Text = "Thao tác nhanh";
        //
        // lblOverviewBody
        // 
        lblOverviewBody.AutoSize = true;
        lblOverviewBody.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblOverviewBody.ForeColor = Color.FromArgb(99, 112, 129);
        lblOverviewBody.Location = new Point(22, 49);
        lblOverviewBody.Name = "lblOverviewBody";
        lblOverviewBody.Size = new Size(642, 19);
        lblOverviewBody.TabIndex = 1;
        lblOverviewBody.Text = "Mở nhanh khu vực quản lý cần dùng.";
        //
        // lblDashboardStatus
        //
        lblDashboardStatus.AutoSize = false;
        lblDashboardStatus.Dock = DockStyle.Bottom;
        lblDashboardStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblDashboardStatus.ForeColor = Color.FromArgb(99, 112, 129);
        lblDashboardStatus.Height = 24;
        lblDashboardStatus.Name = "lblDashboardStatus";
        lblDashboardStatus.TabIndex = 2;
        lblDashboardStatus.TextAlign = ContentAlignment.MiddleLeft;
        lblDashboardStatus.Text = "Số liệu được lấy từ cơ sở dữ liệu.";
        //
        // layoutQuickActions
        //
        layoutQuickActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        layoutQuickActions.Controls.Add(btnQuickProducts);
        layoutQuickActions.Controls.Add(btnQuickOrders);
        layoutQuickActions.Controls.Add(btnQuickInventory);
        layoutQuickActions.Controls.Add(btnQuickCategories);
        layoutQuickActions.Location = new Point(22, 78);
        layoutQuickActions.Name = "layoutQuickActions";
        layoutQuickActions.Size = new Size(930, 48);
        layoutQuickActions.TabIndex = 3;
        layoutQuickActions.WrapContents = false;
        //
        // btnQuickProducts
        //
        btnQuickProducts.BackColor = Color.FromArgb(13, 71, 161);
        btnQuickProducts.Cursor = Cursors.Hand;
        btnQuickProducts.FlatAppearance.BorderColor = Color.FromArgb(13, 71, 161);
        btnQuickProducts.FlatStyle = FlatStyle.Flat;
        btnQuickProducts.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnQuickProducts.ForeColor = Color.White;
        btnQuickProducts.Margin = new Padding(0, 0, 12, 0);
        btnQuickProducts.Name = "btnQuickProducts";
        btnQuickProducts.Size = new Size(210, 44);
        btnQuickProducts.TabIndex = 0;
        btnQuickProducts.Text = "Sản phẩm";
        btnQuickProducts.UseVisualStyleBackColor = false;
        btnQuickProducts.Click += btnSanPham_Click;
        //
        // btnQuickOrders
        //
        btnQuickOrders.BackColor = Color.FromArgb(244, 247, 251);
        btnQuickOrders.Cursor = Cursors.Hand;
        btnQuickOrders.FlatAppearance.BorderColor = Color.FromArgb(227, 232, 240);
        btnQuickOrders.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 239, 250);
        btnQuickOrders.FlatStyle = FlatStyle.Flat;
        btnQuickOrders.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnQuickOrders.ForeColor = Color.FromArgb(13, 71, 161);
        btnQuickOrders.Margin = new Padding(0, 0, 12, 0);
        btnQuickOrders.Name = "btnQuickOrders";
        btnQuickOrders.Size = new Size(210, 44);
        btnQuickOrders.TabIndex = 1;
        btnQuickOrders.Text = "Đơn hàng";
        btnQuickOrders.UseVisualStyleBackColor = false;
        btnQuickOrders.Click += button1_Click;
        //
        // btnQuickInventory
        //
        btnQuickInventory.BackColor = Color.FromArgb(244, 247, 251);
        btnQuickInventory.Cursor = Cursors.Hand;
        btnQuickInventory.FlatAppearance.BorderColor = Color.FromArgb(227, 232, 240);
        btnQuickInventory.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 239, 250);
        btnQuickInventory.FlatStyle = FlatStyle.Flat;
        btnQuickInventory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnQuickInventory.ForeColor = Color.FromArgb(13, 71, 161);
        btnQuickInventory.Margin = new Padding(0, 0, 12, 0);
        btnQuickInventory.Name = "btnQuickInventory";
        btnQuickInventory.Size = new Size(210, 44);
        btnQuickInventory.TabIndex = 2;
        btnQuickInventory.Text = "Nhập kho";
        btnQuickInventory.UseVisualStyleBackColor = false;
        btnQuickInventory.Click += btnNhapKho_Click;
        //
        // btnQuickCategories
        //
        btnQuickCategories.BackColor = Color.FromArgb(244, 247, 251);
        btnQuickCategories.Cursor = Cursors.Hand;
        btnQuickCategories.FlatAppearance.BorderColor = Color.FromArgb(227, 232, 240);
        btnQuickCategories.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 239, 250);
        btnQuickCategories.FlatStyle = FlatStyle.Flat;
        btnQuickCategories.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        btnQuickCategories.ForeColor = Color.FromArgb(13, 71, 161);
        btnQuickCategories.Name = "btnQuickCategories";
        btnQuickCategories.Size = new Size(210, 44);
        btnQuickCategories.TabIndex = 3;
        btnQuickCategories.Text = "Danh mục";
        btnQuickCategories.UseVisualStyleBackColor = false;
        btnQuickCategories.Click += button2_Click;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(28, 43, 61);
        lblTitle.Location = new Point(28, 13);
        lblTitle.Margin = new Padding(4, 0, 4, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(600, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "TỔNG QUAN CỬA HÀNG";
        // 
        // lblXinChao
        // 
        lblXinChao.AutoSize = true;
        lblXinChao.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        lblXinChao.ForeColor = Color.FromArgb(99, 112, 129);
        lblXinChao.Location = new Point(30, 49);
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
        grpThongTin.Controls.Add(btnBienThe);
        grpThongTin.Controls.Add(btnXoa);
        grpThongTin.Controls.Add(btnSua);
        grpThongTin.Controls.Add(btnThem);
        grpThongTin.Controls.Add(txtMoTa);
        grpThongTin.Controls.Add(lblMoTa);
        grpThongTin.Controls.Add(txtGiaBan);
        grpThongTin.Controls.Add(lblGiaBan);
        grpThongTin.Controls.Add(txtGiaGoc);
        grpThongTin.Controls.Add(lblGiaGoc);
        grpThongTin.Controls.Add(txtImageUrl);
        grpThongTin.Controls.Add(lblImageUrl);
        grpThongTin.Controls.Add(chkIsFeatured);
        grpThongTin.Controls.Add(cboDanhMuc);
        grpThongTin.Controls.Add(lblDanhMuc);
        grpThongTin.Controls.Add(txtTenSP);
        grpThongTin.Controls.Add(lblTenSP);
        grpThongTin.Controls.Add(txtMaSP);
        grpThongTin.Controls.Add(lblMaSP);
        grpThongTin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpThongTin.ForeColor = Color.FromArgb(13, 71, 161);
        grpThongTin.Location = new Point(24, 24);
        grpThongTin.Margin = new Padding(4);
        grpThongTin.Name = "grpThongTin";
        grpThongTin.Padding = new Padding(4);
        // Cao hơn để chứa thêm hàng Giá gốc / Ảnh / Nổi bật, không đè danh sách SP.
        grpThongTin.Size = new Size(1000, 300);
        grpThongTin.TabIndex = 2;
        grpThongTin.TabStop = false;
        grpThongTin.Text = "THÔNG TIN SẢN PHẨM";
        grpThongTin.Visible = false;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.SteelBlue;
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(720, 220);
        btnLamMoi.Margin = new Padding(4);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(145, 42);
        btnLamMoi.TabIndex = 14;
        btnLamMoi.Text = "🔄 Làm Mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // btnBienThe
        // 
        btnBienThe.BackColor = Color.FromArgb(230, 239, 250);
        btnBienThe.Cursor = Cursors.Hand;
        btnBienThe.FlatAppearance.BorderColor = Color.FromArgb(190, 211, 235);
        btnBienThe.FlatStyle = FlatStyle.Flat;
        btnBienThe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnBienThe.ForeColor = Color.FromArgb(13, 71, 161);
        btnBienThe.Location = new Point(875, 220);
        btnBienThe.Name = "btnBienThe";
        btnBienThe.Size = new Size(105, 42);
        btnBienThe.TabIndex = 15;
        btnBienThe.Text = "Size / tồn kho";
        btnBienThe.UseVisualStyleBackColor = false;
        btnBienThe.Click += btnBienThe_Click;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.Crimson;
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnXoa.ForeColor = Color.White;
        btnXoa.Location = new Point(555, 220);
        btnXoa.Margin = new Padding(4);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(145, 42);
        btnXoa.TabIndex = 13;
        btnXoa.Text = "Ngừng bán";
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
        btnSua.Location = new Point(390, 220);
        btnSua.Margin = new Padding(4);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(145, 42);
        btnSua.TabIndex = 12;
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
        btnThem.Location = new Point(225, 220);
        btnThem.Margin = new Padding(4);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(145, 42);
        btnThem.TabIndex = 11;
        btnThem.Text = "➕ Thêm Mới";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // txtMoTa — hàng 3, bên phải (không chồng ImageUrl)
        // 
        txtMoTa.Font = new Font("Segoe UI", 9.5F);
        txtMoTa.Location = new Point(470, 152);
        txtMoTa.Margin = new Padding(4);
        txtMoTa.Name = "txtMoTa";
        txtMoTa.PlaceholderText = "Chất liệu cotton, thoáng mát...";
        txtMoTa.Size = new Size(490, 33);
        txtMoTa.TabIndex = 10;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.Font = new Font("Segoe UI", 9.5F);
        lblMoTa.ForeColor = Color.Black;
        lblMoTa.Location = new Point(340, 156);
        lblMoTa.Margin = new Padding(4, 0, 4, 0);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(65, 25);
        lblMoTa.TabIndex = 10;
        lblMoTa.Text = "Mô tả:";
        // 
        // txtGiaBan — hàng 2 trái
        // 
        txtGiaBan.Font = new Font("Segoe UI", 9.5F);
        txtGiaBan.Location = new Point(130, 96);
        txtGiaBan.Margin = new Padding(4);
        txtGiaBan.Name = "txtGiaBan";
        txtGiaBan.PlaceholderText = "VD: 250000";
        txtGiaBan.Size = new Size(190, 33);
        txtGiaBan.TabIndex = 6;
        // 
        // lblGiaBan
        // 
        lblGiaBan.AutoSize = true;
        lblGiaBan.Font = new Font("Segoe UI", 9.5F);
        lblGiaBan.ForeColor = Color.Black;
        lblGiaBan.Location = new Point(24, 100);
        lblGiaBan.Margin = new Padding(4, 0, 4, 0);
        lblGiaBan.Name = "lblGiaBan";
        lblGiaBan.Size = new Size(137, 25);
        lblGiaBan.TabIndex = 5;
        lblGiaBan.Text = "Giá bán (VNĐ):";
        // 
        // txtGiaGoc — hàng 2 giữa
        // 
        txtGiaGoc.Font = new Font("Segoe UI", 9.5F);
        txtGiaGoc.Location = new Point(475, 96);
        txtGiaGoc.Margin = new Padding(4);
        txtGiaGoc.Name = "txtGiaGoc";
        txtGiaGoc.PlaceholderText = "VD: 300000";
        txtGiaGoc.Size = new Size(190, 33);
        txtGiaGoc.TabIndex = 7;
        // 
        // lblGiaGoc
        // 
        lblGiaGoc.AutoSize = true;
        lblGiaGoc.Font = new Font("Segoe UI", 9.5F);
        lblGiaGoc.ForeColor = Color.Black;
        lblGiaGoc.Location = new Point(340, 100);
        lblGiaGoc.Name = "lblGiaGoc";
        lblGiaGoc.Text = "Giá gốc (VNĐ):";
        // 
        // txtImageUrl — hàng 3 trái
        // 
        txtImageUrl.Font = new Font("Segoe UI", 9.5F);
        txtImageUrl.Location = new Point(130, 152);
        txtImageUrl.Margin = new Padding(4);
        txtImageUrl.Name = "txtImageUrl";
        txtImageUrl.PlaceholderText = "/images/ao-thun.jpg hoặc URL";
        txtImageUrl.Size = new Size(190, 33);
        txtImageUrl.TabIndex = 9;
        // 
        // lblImageUrl
        // 
        lblImageUrl.AutoSize = true;
        lblImageUrl.Font = new Font("Segoe UI", 9.5F);
        lblImageUrl.ForeColor = Color.Black;
        lblImageUrl.Location = new Point(24, 156);
        lblImageUrl.Name = "lblImageUrl";
        lblImageUrl.Text = "URL ảnh:";
        // 
        // chkIsFeatured — hàng 2 phải
        // 
        chkIsFeatured.AutoSize = true;
        chkIsFeatured.Font = new Font("Segoe UI", 9.5F);
        chkIsFeatured.Location = new Point(710, 100);
        chkIsFeatured.Name = "chkIsFeatured";
        chkIsFeatured.TabIndex = 8;
        chkIsFeatured.Text = "Sản phẩm nổi bật";
        chkIsFeatured.UseVisualStyleBackColor = true;
        // 
        // cboDanhMuc
        // 
        cboDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDanhMuc.Font = new Font("Segoe UI", 9.5F);
        cboDanhMuc.FormattingEnabled = true;
        cboDanhMuc.Location = new Point(818, 40);
        cboDanhMuc.Margin = new Padding(4);
        cboDanhMuc.Name = "cboDanhMuc";
        cboDanhMuc.Size = new Size(160, 33);
        cboDanhMuc.TabIndex = 5;
        // 
        // lblDanhMuc
        // 
        lblDanhMuc.AutoSize = true;
        lblDanhMuc.Font = new Font("Segoe UI", 9.5F);
        lblDanhMuc.ForeColor = Color.Black;
        lblDanhMuc.Location = new Point(710, 44);
        lblDanhMuc.Margin = new Padding(4, 0, 4, 0);
        lblDanhMuc.Name = "lblDanhMuc";
        lblDanhMuc.Size = new Size(102, 25);
        lblDanhMuc.TabIndex = 4;
        lblDanhMuc.Text = "Danh mục:";
        // 
        // txtTenSP
        // 
        txtTenSP.Font = new Font("Segoe UI", 9.5F);
        txtTenSP.Location = new Point(475, 40);
        txtTenSP.Margin = new Padding(4);
        txtTenSP.Name = "txtTenSP";
        txtTenSP.PlaceholderText = "VD: Áo Thun Polo Nam";
        txtTenSP.Size = new Size(220, 33);
        txtTenSP.TabIndex = 3;
        // 
        // lblTenSP
        // 
        lblTenSP.AutoSize = true;
        lblTenSP.Font = new Font("Segoe UI", 9.5F);
        lblTenSP.ForeColor = Color.Black;
        lblTenSP.Location = new Point(340, 44);
        lblTenSP.Margin = new Padding(4, 0, 4, 0);
        lblTenSP.Name = "lblTenSP";
        lblTenSP.Size = new Size(132, 25);
        lblTenSP.TabIndex = 2;
        lblTenSP.Text = "Tên sản phẩm:";
        // 
        // txtMaSP
        // 
        txtMaSP.Font = new Font("Segoe UI", 9.5F);
        txtMaSP.Location = new Point(130, 40);
        txtMaSP.Margin = new Padding(4);
        txtMaSP.Name = "txtMaSP";
        txtMaSP.PlaceholderText = "VD: AT-010";
        txtMaSP.Size = new Size(190, 33);
        txtMaSP.TabIndex = 1;
        // 
        // lblMaSP
        // 
        lblMaSP.AutoSize = true;
        lblMaSP.Font = new Font("Segoe UI", 9.5F);
        lblMaSP.ForeColor = Color.Black;
        lblMaSP.Location = new Point(24, 44);
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
        // Đẩy xuống dưới grpThongTin (Y=24, cao 300) để không chồng vùng nhập liệu.
        grpDanhSach.Location = new Point(24, 340);
        grpDanhSach.Margin = new Padding(4);
        grpDanhSach.Name = "grpDanhSach";
        grpDanhSach.Padding = new Padding(4);
        // Giữ chừa 24px đáy của panelContent; anchor Bottom sẽ tự co giãn theo cửa sổ.
        grpDanhSach.Size = new Size(1000, 450);
        grpDanhSach.TabIndex = 3;
        grpDanhSach.TabStop = false;
        grpDanhSach.Text = "DANH SÁCH SẢN PHẨM";
        grpDanhSach.Visible = false;
        // 
        // dgvSanPham
        // 
        dgvSanPham.AllowUserToAddRows = false;
        dgvSanPham.AllowUserToDeleteRows = false;
        dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSanPham.BackgroundColor = Color.WhiteSmoke;
        dgvSanPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSanPham.Location = new Point(20, 94);
        dgvSanPham.Margin = new Padding(4);
        dgvSanPham.MultiSelect = false;
        dgvSanPham.Name = "dgvSanPham";
        dgvSanPham.ReadOnly = true;
        dgvSanPham.RowHeadersWidth = 51;
        dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSanPham.Size = new Size(950, 320);
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
        btnHienTatCa.Location = new Point(490, 36);
        btnHienTatCa.Margin = new Padding(4);
        btnHienTatCa.Name = "btnHienTatCa";
        btnHienTatCa.Size = new Size(118, 38);
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
        btnTimKiem.Location = new Point(360, 36);
        btnTimKiem.Margin = new Padding(4);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(118, 38);
        btnTimKiem.TabIndex = 1;
        btnTimKiem.Text = "🔍 Tìm Kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Font = new Font("Segoe UI", 9.5F);
        txtTimKiem.Location = new Point(25, 40);
        txtTimKiem.Margin = new Padding(4);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Nhập tên hoặc mã sản phẩm cần tìm...";
        txtTimKiem.Size = new Size(330, 33);
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
        btnDangXuat.Location = new Point(1035, 20);
        btnDangXuat.Margin = new Padding(4);
        btnDangXuat.Name = "btnDangXuat";
        btnDangXuat.Size = new Size(138, 42);
        btnDangXuat.TabIndex = 6;
        btnDangXuat.Text = "Đăng Xuất";
        btnDangXuat.UseVisualStyleBackColor = false;
        btnDangXuat.Click += btnDangXuat_Click;
        // 
        // btnDoiMatKhau
        // 
        btnDoiMatKhau.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDoiMatKhau.BackColor = Color.FromArgb(27, 54, 93);
        btnDoiMatKhau.Cursor = Cursors.Hand;
        btnDoiMatKhau.FlatStyle = FlatStyle.Flat;
        btnDoiMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDoiMatKhau.ForeColor = Color.White;
        btnDoiMatKhau.Location = new Point(884, 20);
        btnDoiMatKhau.Margin = new Padding(4);
        btnDoiMatKhau.Name = "btnDoiMatKhau";
        btnDoiMatKhau.Size = new Size(140, 42);
        btnDoiMatKhau.TabIndex = 7;
        btnDoiMatKhau.Text = "Đổi Mật Khẩu";
        btnDoiMatKhau.UseVisualStyleBackColor = false;
        btnDoiMatKhau.Click += btnDoiMatKhau_Click;
        // 
        // button1
        // 
        button1.BackColor = Color.FromArgb(17, 43, 72);
        button1.Cursor = Cursors.Hand;
        button1.FlatAppearance.BorderSize = 0;
        button1.FlatStyle = FlatStyle.Flat;
        button1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        button1.ForeColor = Color.FromArgb(223, 232, 243);
        button1.Location = new Point(12, 230);
        button1.Name = "button1";
        button1.Size = new Size(202, 44);
        button1.TabIndex = 3;
        button1.Text = "   Quản lý đơn hàng";
        button1.TextAlign = ContentAlignment.MiddleLeft;
        button1.UseVisualStyleBackColor = false;
        button1.Click += button1_Click;
        // 
        // btnDanhMuc
        // 
        btnDanhMuc.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        btnDanhMuc.BackColor = Color.FromArgb(17, 43, 72);
        btnDanhMuc.Cursor = Cursors.Hand;
        btnDanhMuc.FlatAppearance.BorderSize = 0;
        btnDanhMuc.FlatStyle = FlatStyle.Flat;
        btnDanhMuc.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        btnDanhMuc.ForeColor = Color.FromArgb(223, 232, 243);
        btnDanhMuc.Location = new Point(12, 282);
        btnDanhMuc.Name = "btnDanhMuc";
        btnDanhMuc.Size = new Size(202, 44);
        btnDanhMuc.TabIndex = 2;
        btnDanhMuc.Text = "   Quản lý danh mục";
        btnDanhMuc.TextAlign = ContentAlignment.MiddleLeft;
        btnDanhMuc.UseVisualStyleBackColor = false;
        btnDanhMuc.Click += button2_Click;
        // 
        // btnNhapKho
        // 
        btnNhapKho.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        btnNhapKho.BackColor = Color.FromArgb(17, 43, 72);
        btnNhapKho.Cursor = Cursors.Hand;
        btnNhapKho.FlatAppearance.BorderSize = 0;
        btnNhapKho.FlatStyle = FlatStyle.Flat;
        btnNhapKho.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        btnNhapKho.ForeColor = Color.FromArgb(223, 232, 243);
        btnNhapKho.Location = new Point(12, 334);
        btnNhapKho.Name = "btnNhapKho";
        btnNhapKho.Size = new Size(202, 44);
        btnNhapKho.TabIndex = 4;
        btnNhapKho.Text = "   Phiếu nhập kho";
        btnNhapKho.TextAlign = ContentAlignment.MiddleLeft;
        btnNhapKho.UseVisualStyleBackColor = false;
        btnNhapKho.Click += btnNhapKho_Click;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.WhiteSmoke;
        ClientSize = new Size(1440, 900);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        Margin = new Padding(4);
        MinimumSize = new Size(1182, 736);
        Name = "FormMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Fashion Store | Quản trị";
        WindowState = FormWindowState.Maximized;
        Load += FormMain_Load;
        grpThongTin.ResumeLayout(false);
        grpThongTin.PerformLayout();
        grpDanhSach.ResumeLayout(false);
        grpDanhSach.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSanPham).EndInit();
        panelStatProducts.ResumeLayout(false);
        panelStatProducts.PerformLayout();
        panelStatOrders.ResumeLayout(false);
        panelStatOrders.PerformLayout();
        panelStatRevenue.ResumeLayout(false);
        panelStatRevenue.PerformLayout();
        panelOverview.ResumeLayout(false);
        panelOverview.PerformLayout();
        layoutQuickActions.ResumeLayout(false);
        layoutStats.ResumeLayout(false);
        panelWelcome.ResumeLayout(false);
        panelWelcome.PerformLayout();
        layoutDashboard.ResumeLayout(false);
        panelDashboard.ResumeLayout(false);
        panelContent.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        layoutWorkspace.ResumeLayout(false);
        panelSidebar.ResumeLayout(false);
        panelSidebar.PerformLayout();
        layoutRoot.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private Button button1;
    private Button btnDanhMuc;
    private Button btnNhapKho;
    private TextBox txtGiaGoc;
    private Label lblGiaGoc;

    private TextBox txtImageUrl;
    private Label lblImageUrl;

    private CheckBox chkIsFeatured;
}
