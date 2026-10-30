namespace QuanLyDesktop;

partial class FormDanhMuc
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel layoutRoot;
    private Panel panelHeader;
    private Label lblTitle;
    private Label lblSubtitle;
    private TableLayoutPanel layoutBody;
    private Panel panelEditor;
    private TableLayoutPanel layoutEditor;
    private Label lblEditorTitle;
    private Label lblTenDanhMuc;
    private TextBox txtTenDanhMuc;
    private Label lblMoTa;
    private TextBox txtMoTa;
    private FlowLayoutPanel flowActions;
    private Button btnThem;
    private Button btnSua;
    private Button btnNgungHoatDong;
    private Panel panelList;
    private TableLayoutPanel layoutList;
    private Label lblListTitle;
    private TableLayoutPanel layoutSearch;
    private TextBox txtTimKiem;
    private Button btnTimKiem;
    private Button btnLamMoi;
    private DataGridView dgvDanhMuc;

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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        layoutRoot = new TableLayoutPanel();
        panelHeader = new Panel();
        lblSubtitle = new Label();
        lblTitle = new Label();
        layoutBody = new TableLayoutPanel();
        panelEditor = new Panel();
        layoutEditor = new TableLayoutPanel();
        lblEditorTitle = new Label();
        lblTenDanhMuc = new Label();
        txtTenDanhMuc = new TextBox();
        lblMoTa = new Label();
        txtMoTa = new TextBox();
        flowActions = new FlowLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnNgungHoatDong = new Button();
        panelList = new Panel();
        layoutList = new TableLayoutPanel();
        lblListTitle = new Label();
        layoutSearch = new TableLayoutPanel();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        btnLamMoi = new Button();
        dgvDanhMuc = new DataGridView();
        layoutRoot.SuspendLayout();
        panelHeader.SuspendLayout();
        layoutBody.SuspendLayout();
        panelEditor.SuspendLayout();
        layoutEditor.SuspendLayout();
        flowActions.SuspendLayout();
        panelList.SuspendLayout();
        layoutList.SuspendLayout();
        layoutSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDanhMuc).BeginInit();
        SuspendLayout();
        // 
        // layoutRoot
        // 
        layoutRoot.BackColor = Color.FromArgb(245, 247, 250);
        layoutRoot.ColumnCount = 1;
        layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutRoot.Controls.Add(panelHeader, 0, 0);
        layoutRoot.Controls.Add(layoutBody, 0, 1);
        layoutRoot.Dock = DockStyle.Fill;
        layoutRoot.Location = new Point(0, 0);
        layoutRoot.Name = "layoutRoot";
        layoutRoot.Padding = new Padding(24, 18, 24, 22);
        layoutRoot.RowCount = 2;
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
        layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutRoot.Size = new Size(1180, 760);
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
        panelHeader.Size = new Size(1132, 74);
        panelHeader.TabIndex = 0;
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblSubtitle.ForeColor = Color.FromArgb(90, 102, 117);
        lblSubtitle.Location = new Point(21, 49);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(479, 25);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Tạo, cập nhật và theo dõi các nhóm sản phẩm của shop";
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTitle.ForeColor = Color.FromArgb(13, 71, 161);
        lblTitle.Location = new Point(18, 8);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(585, 48);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "QUẢN LÝ DANH MỤC SẢN PHẨM";
        // 
        // layoutBody
        // 
        layoutBody.ColumnCount = 2;
        layoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
        layoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutBody.Controls.Add(panelEditor, 0, 0);
        layoutBody.Controls.Add(panelList, 1, 0);
        layoutBody.Dock = DockStyle.Fill;
        layoutBody.Location = new Point(24, 106);
        layoutBody.Margin = new Padding(0);
        layoutBody.Name = "layoutBody";
        layoutBody.RowCount = 1;
        layoutBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutBody.Size = new Size(1132, 632);
        layoutBody.TabIndex = 1;
        // 
        // panelEditor
        // 
        panelEditor.BackColor = Color.White;
        panelEditor.Controls.Add(layoutEditor);
        panelEditor.Dock = DockStyle.Fill;
        panelEditor.Location = new Point(0, 0);
        panelEditor.Margin = new Padding(0, 0, 10, 0);
        panelEditor.Name = "panelEditor";
        panelEditor.Size = new Size(340, 632);
        panelEditor.TabIndex = 0;
        // 
        // layoutEditor
        // 
        layoutEditor.ColumnCount = 1;
        layoutEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutEditor.Controls.Add(lblEditorTitle, 0, 0);
        layoutEditor.Controls.Add(lblTenDanhMuc, 0, 1);
        layoutEditor.Controls.Add(txtTenDanhMuc, 0, 2);
        layoutEditor.Controls.Add(lblMoTa, 0, 3);
        layoutEditor.Controls.Add(txtMoTa, 0, 4);
        layoutEditor.Controls.Add(flowActions, 0, 5);
        layoutEditor.Dock = DockStyle.Fill;
        layoutEditor.Location = new Point(0, 0);
        layoutEditor.Name = "layoutEditor";
        layoutEditor.Padding = new Padding(18, 16, 18, 14);
        layoutEditor.RowCount = 6;
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutEditor.RowStyles.Add(new RowStyle(SizeType.Absolute, 108F));
        layoutEditor.Size = new Size(340, 632);
        layoutEditor.TabIndex = 0;
        // 
        // lblEditorTitle
        // 
        lblEditorTitle.Dock = DockStyle.Fill;
        lblEditorTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblEditorTitle.ForeColor = Color.FromArgb(13, 71, 161);
        lblEditorTitle.Location = new Point(21, 16);
        lblEditorTitle.Name = "lblEditorTitle";
        lblEditorTitle.Size = new Size(298, 38);
        lblEditorTitle.TabIndex = 0;
        lblEditorTitle.Text = "THÔNG TIN DANH MỤC";
        lblEditorTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTenDanhMuc
        // 
        lblTenDanhMuc.Dock = DockStyle.Fill;
        lblTenDanhMuc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTenDanhMuc.ForeColor = Color.FromArgb(48, 60, 75);
        lblTenDanhMuc.Location = new Point(21, 54);
        lblTenDanhMuc.Name = "lblTenDanhMuc";
        lblTenDanhMuc.Size = new Size(298, 30);
        lblTenDanhMuc.TabIndex = 1;
        lblTenDanhMuc.Text = "Tên danh mục *";
        lblTenDanhMuc.TextAlign = ContentAlignment.BottomLeft;
        // 
        // txtTenDanhMuc
        // 
        txtTenDanhMuc.Dock = DockStyle.Fill;
        txtTenDanhMuc.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtTenDanhMuc.Location = new Point(21, 87);
        txtTenDanhMuc.Name = "txtTenDanhMuc";
        txtTenDanhMuc.PlaceholderText = "Ví dụ: Áo thun nam";
        txtTenDanhMuc.Size = new Size(298, 34);
        txtTenDanhMuc.TabIndex = 0;
        // 
        // lblMoTa
        // 
        lblMoTa.Dock = DockStyle.Fill;
        lblMoTa.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblMoTa.ForeColor = Color.FromArgb(48, 60, 75);
        lblMoTa.Location = new Point(21, 122);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(298, 34);
        lblMoTa.TabIndex = 2;
        lblMoTa.Text = "Mô tả";
        lblMoTa.TextAlign = ContentAlignment.BottomLeft;
        // 
        // txtMoTa
        // 
        txtMoTa.AcceptsReturn = true;
        txtMoTa.Dock = DockStyle.Fill;
        txtMoTa.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtMoTa.Location = new Point(21, 159);
        txtMoTa.Multiline = true;
        txtMoTa.Name = "txtMoTa";
        txtMoTa.PlaceholderText = "Mô tả ngắn về danh mục...";
        txtMoTa.ScrollBars = ScrollBars.Vertical;
        txtMoTa.Size = new Size(298, 348);
        txtMoTa.TabIndex = 1;
        // 
        // flowActions
        // 
        flowActions.Controls.Add(btnThem);
        flowActions.Controls.Add(btnSua);
        flowActions.Controls.Add(btnNgungHoatDong);
        flowActions.Dock = DockStyle.Fill;
        flowActions.Location = new Point(18, 510);
        flowActions.Margin = new Padding(0);
        flowActions.Name = "flowActions";
        flowActions.Padding = new Padding(0, 12, 0, 0);
        flowActions.Size = new Size(304, 108);
        flowActions.TabIndex = 3;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(13, 71, 161);
        btnThem.Cursor = Cursors.Hand;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(0, 12);
        btnThem.Margin = new Padding(0, 0, 8, 8);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(145, 40);
        btnThem.TabIndex = 2;
        btnThem.Text = "Thêm danh mục";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.FromArgb(230, 239, 250);
        btnSua.Cursor = Cursors.Hand;
        btnSua.FlatAppearance.BorderColor = Color.FromArgb(190, 211, 235);
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSua.ForeColor = Color.FromArgb(13, 71, 161);
        btnSua.Location = new Point(153, 12);
        btnSua.Margin = new Padding(0, 0, 0, 8);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(145, 40);
        btnSua.TabIndex = 3;
        btnSua.Text = "Cập nhật";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnNgungHoatDong
        // 
        btnNgungHoatDong.BackColor = Color.White;
        btnNgungHoatDong.Cursor = Cursors.Hand;
        btnNgungHoatDong.FlatAppearance.BorderColor = Color.FromArgb(205, 212, 221);
        btnNgungHoatDong.FlatStyle = FlatStyle.Flat;
        btnNgungHoatDong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNgungHoatDong.ForeColor = Color.FromArgb(150, 55, 55);
        btnNgungHoatDong.Location = new Point(0, 60);
        btnNgungHoatDong.Margin = new Padding(0);
        btnNgungHoatDong.Name = "btnNgungHoatDong";
        btnNgungHoatDong.Size = new Size(298, 40);
        btnNgungHoatDong.TabIndex = 4;
        btnNgungHoatDong.Text = "Ngừng hoạt động";
        btnNgungHoatDong.UseVisualStyleBackColor = false;
        btnNgungHoatDong.Click += btnNgungHoatDong_Click;
        // 
        // panelList
        // 
        panelList.BackColor = Color.White;
        panelList.Controls.Add(layoutList);
        panelList.Dock = DockStyle.Fill;
        panelList.Location = new Point(360, 0);
        panelList.Margin = new Padding(10, 0, 0, 0);
        panelList.Name = "panelList";
        panelList.Size = new Size(772, 632);
        panelList.TabIndex = 1;
        // 
        // layoutList
        // 
        layoutList.ColumnCount = 1;
        layoutList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutList.Controls.Add(lblListTitle, 0, 0);
        layoutList.Controls.Add(layoutSearch, 0, 1);
        layoutList.Controls.Add(dgvDanhMuc, 0, 2);
        layoutList.Dock = DockStyle.Fill;
        layoutList.Location = new Point(0, 0);
        layoutList.Name = "layoutList";
        layoutList.Padding = new Padding(18, 16, 18, 16);
        layoutList.RowCount = 3;
        layoutList.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        layoutList.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layoutList.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutList.Size = new Size(772, 632);
        layoutList.TabIndex = 0;
        // 
        // lblListTitle
        // 
        lblListTitle.Dock = DockStyle.Fill;
        lblListTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblListTitle.ForeColor = Color.FromArgb(13, 71, 161);
        lblListTitle.Location = new Point(21, 16);
        lblListTitle.Name = "lblListTitle";
        lblListTitle.Size = new Size(730, 38);
        lblListTitle.TabIndex = 0;
        lblListTitle.Text = "DANH SÁCH DANH MỤC";
        lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // layoutSearch
        // 
        layoutSearch.ColumnCount = 3;
        layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        layoutSearch.Controls.Add(txtTimKiem, 0, 0);
        layoutSearch.Controls.Add(btnTimKiem, 1, 0);
        layoutSearch.Controls.Add(btnLamMoi, 2, 0);
        layoutSearch.Dock = DockStyle.Fill;
        layoutSearch.Location = new Point(18, 56);
        layoutSearch.Margin = new Padding(0, 2, 0, 6);
        layoutSearch.Name = "layoutSearch";
        layoutSearch.RowCount = 1;
        layoutSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layoutSearch.Size = new Size(736, 40);
        layoutSearch.TabIndex = 1;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Dock = DockStyle.Fill;
        txtTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtTimKiem.Location = new Point(3, 3);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tìm theo tên danh mục...";
        txtTimKiem.Size = new Size(506, 33);
        txtTimKiem.TabIndex = 5;
        // 
        // btnTimKiem
        // 
        btnTimKiem.BackColor = Color.FromArgb(13, 71, 161);
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(520, 0);
        btnTimKiem.Margin = new Padding(8, 0, 4, 0);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(100, 38);
        btnTimKiem.TabIndex = 6;
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
        btnLamMoi.Location = new Point(628, 0);
        btnLamMoi.Margin = new Padding(4, 0, 0, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(100, 38);
        btnLamMoi.TabIndex = 7;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // dgvDanhMuc
        // 
        dgvDanhMuc.AllowUserToAddRows = false;
        dgvDanhMuc.AllowUserToDeleteRows = false;
        dgvDanhMuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDanhMuc.BackgroundColor = Color.White;
        dgvDanhMuc.BorderStyle = BorderStyle.None;
        dgvDanhMuc.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvDanhMuc.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(232, 239, 248);
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(35, 52, 73);
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvDanhMuc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvDanhMuc.ColumnHeadersHeight = 38;
        dgvDanhMuc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = SystemColors.Window;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(215, 232, 250);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(25, 45, 70);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvDanhMuc.DefaultCellStyle = dataGridViewCellStyle2;
        dgvDanhMuc.Dock = DockStyle.Fill;
        dgvDanhMuc.EnableHeadersVisualStyles = false;
        dgvDanhMuc.GridColor = Color.FromArgb(231, 235, 240);
        dgvDanhMuc.Location = new Point(21, 105);
        dgvDanhMuc.MultiSelect = false;
        dgvDanhMuc.Name = "dgvDanhMuc";
        dgvDanhMuc.ReadOnly = true;
        dgvDanhMuc.RowHeadersVisible = false;
        dgvDanhMuc.RowHeadersWidth = 62;
        dgvDanhMuc.RowTemplate.Height = 34;
        dgvDanhMuc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDanhMuc.Size = new Size(730, 508);
        dgvDanhMuc.TabIndex = 8;
        dgvDanhMuc.CellClick += dgvDanhMuc_CellClick;
        // 
        // FormDanhMuc
        // 
        AcceptButton = btnTimKiem;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1180, 760);
        Controls.Add(layoutRoot);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(1000, 680);
        Name = "FormDanhMuc";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý danh mục sản phẩm";
        Load += FormDanhMuc_Load;
        layoutRoot.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        layoutBody.ResumeLayout(false);
        panelEditor.ResumeLayout(false);
        layoutEditor.ResumeLayout(false);
        layoutEditor.PerformLayout();
        flowActions.ResumeLayout(false);
        panelList.ResumeLayout(false);
        layoutList.ResumeLayout(false);
        layoutSearch.ResumeLayout(false);
        layoutSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDanhMuc).EndInit();
        ResumeLayout(false);
    }
}
