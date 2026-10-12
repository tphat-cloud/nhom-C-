namespace QuanLyDesktop
{
    partial class FormDonHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            dgvDonHang = new DataGridView();
            btnDuyetDon = new Button();
            btnLamMoi = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(19, 18);
            label1.Name = "label1";
            label1.Size = new Size(473, 38);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ ĐƠN HÀNG TỪ WEBSITE";
            // 
            // dgvDonHang
            // 
            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDonHang.Location = new Point(107, 59);
            dgvDonHang.Name = "dgvDonHang";
            dgvDonHang.RowHeadersWidth = 62;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.Size = new Size(589, 285);
            dgvDonHang.TabIndex = 1;
            // 
            // btnDuyetDon
            // 
            btnDuyetDon.BackColor = SystemColors.ActiveCaption;
            btnDuyetDon.ForeColor = SystemColors.ActiveCaptionText;
            btnDuyetDon.Location = new Point(151, 371);
            btnDuyetDon.Name = "btnDuyetDon";
            btnDuyetDon.Size = new Size(112, 34);
            btnDuyetDon.TabIndex = 2;
            btnDuyetDon.Text = "Duyệt Đơn";
            btnDuyetDon.UseVisualStyleBackColor = false;
            btnDuyetDon.Click += btnDuyetDon_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(485, 371);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(112, 34);
            btnLamMoi.TabIndex = 3;
            btnLamMoi.Text = "Làm Mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // FormDonHang
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLamMoi);
            Controls.Add(btnDuyetDon);
            Controls.Add(dgvDonHang);
            Controls.Add(label1);
            Name = "FormDonHang";
            Text = "FormDonHang";
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvDonHang;
        private Button btnDuyetDon;
        private Button btnLamMoi;
    }
}