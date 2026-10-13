namespace QuanLyDesktop
{
    partial class FormDonHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
    private Label lblSubtitle;
    private Label lblDetailSection;
    private DataGridView dgvChiTiet;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lblSubtitle = new Label();
            lblDetailSection = new Label();
            dgvDonHang = new DataGridView();
            dgvChiTiet = new DataGridView();
            btnDuyetDon = new Button();
            btnLamMoi = new Button();
            btnXemChiTiet = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(13, 71, 161);
            label1.Location = new Point(28, 18);
            label1.Name = "label1";
            label1.Size = new Size(600, 38);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ ĐƠN HÀNG";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitle.ForeColor = Color.FromArgb(90, 102, 117);
            lblSubtitle.Location = new Point(31, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(316, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Theo dõi đơn đặt hàng từ website và mặt hàng trong đơn";
            // 
            // dgvDonHang
            // 
            dgvDonHang.AllowUserToAddRows = false;
            dgvDonHang.AllowUserToDeleteRows = false;
            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHang.BackgroundColor = Color.White;
            dgvDonHang.BorderStyle = BorderStyle.None;
            dgvDonHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDonHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDonHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
            dgvDonHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDonHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
            dgvDonHang.ColumnHeadersHeight = 38;
            dgvDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDonHang.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvDonHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
            dgvDonHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
            dgvDonHang.EnableHeadersVisualStyles = false;
            dgvDonHang.GridColor = Color.FromArgb(231, 235, 240);
            dgvDonHang.Location = new Point(30, 119);
            dgvDonHang.MultiSelect = false;
            dgvDonHang.Name = "dgvDonHang";
            dgvDonHang.ReadOnly = true;
            dgvDonHang.RowHeadersVisible = false;
            dgvDonHang.RowTemplate.Height = 32;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.Size = new Size(1120, 286);
            dgvDonHang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvDonHang.TabIndex = 2;
            // 
            // lblDetailSection
            // 
            lblDetailSection.AutoSize = true;
            lblDetailSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetailSection.ForeColor = Color.FromArgb(13, 71, 161);
            lblDetailSection.Location = new Point(30, 493);
            lblDetailSection.Name = "lblDetailSection";
            lblDetailSection.Size = new Size(432, 23);
            lblDetailSection.TabIndex = 3;
            lblDetailSection.Text = "CHI TIẾT ĐƠN HÀNG - CHỌN ĐƠN RỒI BẤM XEM CHI TIẾT";
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.BackgroundColor = Color.White;
            dgvChiTiet.BorderStyle = BorderStyle.None;
            dgvChiTiet.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvChiTiet.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 248);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 52, 73);
            dgvChiTiet.ColumnHeadersHeight = 36;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvChiTiet.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 232, 250);
            dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 45, 70);
            dgvChiTiet.EnableHeadersVisualStyles = false;
            dgvChiTiet.GridColor = Color.FromArgb(231, 235, 240);
            dgvChiTiet.Location = new Point(30, 526);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.RowTemplate.Height = 30;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(1120, 180);
            dgvChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvChiTiet.TabIndex = 4;
            // 
            // btnDuyetDon
            // 
            btnDuyetDon.BackColor = Color.FromArgb(13, 71, 161);
            btnDuyetDon.Cursor = Cursors.Hand;
            btnDuyetDon.FlatAppearance.BorderSize = 0;
            btnDuyetDon.FlatStyle = FlatStyle.Flat;
            btnDuyetDon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDuyetDon.ForeColor = Color.White;
            btnDuyetDon.Location = new Point(30, 426);
            btnDuyetDon.Name = "btnDuyetDon";
            btnDuyetDon.Size = new Size(132, 40);
            btnDuyetDon.TabIndex = 5;
            btnDuyetDon.Text = "Duyệt đơn";
            btnDuyetDon.UseVisualStyleBackColor = false;
            btnDuyetDon.Click += btnDuyetDon_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.White;
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderColor = Color.FromArgb(205, 212, 221);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.FromArgb(48, 60, 75);
            btnLamMoi.Location = new Point(340, 426);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(118, 40);
            btnLamMoi.TabIndex = 7;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnXemChiTiet
            // 
            btnXemChiTiet.BackColor = Color.FromArgb(230, 239, 250);
            btnXemChiTiet.Cursor = Cursors.Hand;
            btnXemChiTiet.FlatAppearance.BorderColor = Color.FromArgb(190, 211, 235);
            btnXemChiTiet.FlatStyle = FlatStyle.Flat;
            btnXemChiTiet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXemChiTiet.ForeColor = Color.FromArgb(13, 71, 161);
            btnXemChiTiet.Location = new Point(175, 426);
            btnXemChiTiet.Name = "btnXemChiTiet";
            btnXemChiTiet.Size = new Size(145, 40);
            btnXemChiTiet.TabIndex = 6;
            btnXemChiTiet.Text = "Xem chi tiết";
            btnXemChiTiet.UseVisualStyleBackColor = false;
            btnXemChiTiet.Click += btnXemChiTiet_Click;
            // 
            // FormDonHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1180, 760);
            MinimumSize = new Size(980, 650);
            StartPosition = FormStartPosition.CenterParent;
            Controls.Add(dgvChiTiet);
            Controls.Add(lblDetailSection);
            Controls.Add(btnXemChiTiet);
            Controls.Add(btnLamMoi);
            Controls.Add(btnDuyetDon);
            Controls.Add(dgvDonHang);
            Controls.Add(lblSubtitle);
            Controls.Add(label1);
            Name = "FormDonHang";
            Text = "Quản lý đơn hàng";
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvDonHang;
        private Button btnDuyetDon;
        private Button btnLamMoi;
        private Button btnXemChiTiet;
    }
}