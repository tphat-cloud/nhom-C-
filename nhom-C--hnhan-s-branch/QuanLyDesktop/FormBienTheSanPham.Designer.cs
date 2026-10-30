namespace QuanLyDesktop;

partial class FormBienTheSanPham
{
    private System.ComponentModel.IContainer components = null!;
    private Panel panelHeader;
    private Label lblTitle;
    private Label lblProductName;
    private Label lblSize;
    private ComboBox cboSize;
    private Label lblColor;
    private TextBox txtColor;
    private Label lblStock;
    private NumericUpDown nudStock;
    private Button btnThem;
    private Button btnSua;
    private Button btnLamMoi;
    private Label lblListTitle;
    private DataGridView dgvVariants;

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
        lblProductName = new Label();
        lblSize = new Label();
        cboSize = new ComboBox();
        lblColor = new Label();
        txtColor = new TextBox();
        lblStock = new Label();
        nudStock = new NumericUpDown();
        btnThem = new Button();
        btnSua = new Button();
        btnLamMoi = new Button();
        lblListTitle = new Label();
        dgvVariants = new DataGridView();
        panelHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvVariants).BeginInit();
        SuspendLayout();
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(17, 43, 72);
        panelHeader.Controls.Add(lblProductName);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(0, 0);
        panelHeader.Name = "panelHeader";
        panelHeader.Size = new Size(1040, 112);
        panelHeader.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 19F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(28, 19);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(415, 36);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "QUẢN LÝ SIZE, MÀU VÀ TỒN KHO";
        // 
        // lblProductName
        // 
        lblProductName.AutoSize = true;
        lblProductName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblProductName.ForeColor = Color.FromArgb(209, 222, 238);
        lblProductName.Location = new Point(31, 67);
        lblProductName.Name = "lblProductName";
        lblProductName.Size = new Size(104, 19);
        lblProductName.TabIndex = 1;
        lblProductName.Text = "Sản phẩm: ...";
        // 
        // lblSize
        // 
        lblSize.AutoSize = true;
        lblSize.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblSize.ForeColor = Color.FromArgb(48, 60, 75);
        lblSize.Location = new Point(30, 140);
        lblSize.Name = "lblSize";
        lblSize.Size = new Size(34, 17);
        lblSize.TabIndex = 1;
        lblSize.Text = "Size";
        // 
        // cboSize
        // 
        cboSize.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSize.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        cboSize.Location = new Point(30, 164);
        cboSize.Name = "cboSize";
        cboSize.Size = new Size(150, 25);
        cboSize.TabIndex = 0;
        // 
        // lblColor
        // 
        lblColor.AutoSize = true;
        lblColor.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblColor.ForeColor = Color.FromArgb(48, 60, 75);
        lblColor.Location = new Point(205, 140);
        lblColor.Name = "lblColor";
        lblColor.Size = new Size(54, 17);
        lblColor.TabIndex = 2;
        lblColor.Text = "Màu sắc";
        // 
        // txtColor
        // 
        txtColor.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtColor.Location = new Point(205, 164);
        txtColor.Name = "txtColor";
        txtColor.PlaceholderText = "Ví dụ: Đen";
        txtColor.Size = new Size(210, 25);
        txtColor.TabIndex = 1;
        // 
        // lblStock
        // 
        lblStock.AutoSize = true;
        lblStock.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblStock.ForeColor = Color.FromArgb(48, 60, 75);
        lblStock.Location = new Point(440, 140);
        lblStock.Name = "lblStock";
        lblStock.Size = new Size(65, 17);
        lblStock.TabIndex = 3;
        lblStock.Text = "Tồn kho";
        // 
        // nudStock
        // 
        nudStock.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        nudStock.Location = new Point(440, 164);
        nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        nudStock.Name = "nudStock";
        nudStock.Size = new Size(150, 25);
        nudStock.TabIndex = 2;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(13, 71, 161);
        btnThem.Cursor = Cursors.Hand;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(620, 157);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(125, 38);
        btnThem.TabIndex = 3;
        btnThem.Text = "Thêm biến thể";
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
        btnSua.Location = new Point(755, 157);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(115, 38);
        btnSua.TabIndex = 4;
        btnSua.Text = "Cập nhật";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.White;
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.FlatAppearance.BorderColor = Color.FromArgb(205, 212, 221);
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.FromArgb(48, 60, 75);
        btnLamMoi.Location = new Point(880, 157);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(125, 38);
        btnLamMoi.TabIndex = 5;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // lblListTitle
        // 
        lblListTitle.AutoSize = true;
        lblListTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblListTitle.ForeColor = Color.FromArgb(13, 71, 161);
        lblListTitle.Location = new Point(30, 220);
        lblListTitle.Name = "lblListTitle";
        lblListTitle.Size = new Size(155, 19);
        lblListTitle.TabIndex = 4;
        lblListTitle.Text = "BIẾN THỂ CỦA SẢN PHẨM";
        // 
        // dgvVariants
        // 
        dgvVariants.AllowUserToAddRows = false;
        dgvVariants.AllowUserToDeleteRows = false;
        dgvVariants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvVariants.BackgroundColor = Color.White;
        dgvVariants.BorderStyle = BorderStyle.None;
        dgvVariants.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvVariants.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgvVariants.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
        dgvVariants.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvVariants.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
        dgvVariants.ColumnHeadersHeight = 38;
        dgvVariants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvVariants.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        dgvVariants.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
        dgvVariants.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
        dgvVariants.Dock = DockStyle.Fill;
        dgvVariants.EnableHeadersVisualStyles = false;
        dgvVariants.GridColor = Color.FromArgb(231, 235, 240);
        dgvVariants.Location = new Point(30, 250);
        dgvVariants.MultiSelect = false;
        dgvVariants.Name = "dgvVariants";
        dgvVariants.ReadOnly = true;
        dgvVariants.RowHeadersVisible = false;
        dgvVariants.RowTemplate.Height = 34;
        dgvVariants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvVariants.Size = new Size(975, 380);
        dgvVariants.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvVariants.TabIndex = 6;
        dgvVariants.CellClick += dgvVariants_CellClick;
        // 
        // FormBienTheSanPham
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 247, 251);
        ClientSize = new Size(1040, 680);
        Controls.Add(dgvVariants);
        Controls.Add(lblListTitle);
        Controls.Add(btnLamMoi);
        Controls.Add(btnSua);
        Controls.Add(btnThem);
        Controls.Add(nudStock);
        Controls.Add(lblStock);
        Controls.Add(txtColor);
        Controls.Add(lblColor);
        Controls.Add(cboSize);
        Controls.Add(lblSize);
        Controls.Add(panelHeader);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(980, 620);
        Name = "FormBienTheSanPham";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý biến thể sản phẩm";
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvVariants).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
