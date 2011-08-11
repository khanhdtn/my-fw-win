namespace ProtocolVN.Framework.Win
{
    partial class frmFWUserError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFWUserError));
            this.labError = new System.Windows.Forms.Label();
            this.TenCty = new DevExpress.XtraEditors.LabelControl();
            this.btnRestart = new DevExpress.XtraEditors.SimpleButton();
            this.btnSendError = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SanPham = new DevExpress.XtraEditors.LabelControl();
            this.PhienBan = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labError
            // 
            this.labError.AutoSize = true;
            this.labError.ForeColor = System.Drawing.Color.Red;
            this.labError.Location = new System.Drawing.Point(12, 110);
            this.labError.Name = "labError";
            this.labError.Size = new System.Drawing.Size(0, 13);
            this.labError.TabIndex = 5;
            // 
            // TenCty
            // 
            this.TenCty.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenCty.Appearance.ForeColor = System.Drawing.Color.Black;
            this.TenCty.Appearance.Options.UseFont = true;
            this.TenCty.Appearance.Options.UseForeColor = true;
            this.TenCty.Appearance.Options.UseTextOptions = true;
            this.TenCty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TenCty.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TenCty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.TenCty.Location = new System.Drawing.Point(23, 82);
            this.TenCty.Name = "TenCty";
            this.TenCty.Size = new System.Drawing.Size(336, 42);
            this.TenCty.TabIndex = 48;
            this.TenCty.Text = "Xin lỗi bạn về sự cố này. Vui lòng khởi động lại chương trình. Nếu sự cố này vẫn " +
                "còn xuất hiện vui lòng gửi sự cố cho công ty PROTOCOL.";
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(180, 177);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(62, 23);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "Khởi động";
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnSendError
            // 
            this.btnSendError.Location = new System.Drawing.Point(12, 177);
            this.btnSendError.Name = "btnSendError";
            this.btnSendError.Size = new System.Drawing.Size(75, 23);
            this.btnSendError.TabIndex = 0;
            this.btnSendError.Text = "Gửi sự cố";
            this.btnSendError.Click += new System.EventHandler(this.btnSendError_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(316, 177);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(62, 23);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(248, 177);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(62, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Tiếp tục";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.SanPham);
            this.panel1.Controls.Add(this.PhienBan);
            this.panel1.Controls.Add(this.pictureEdit2);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 77);
            this.panel1.TabIndex = 49;
            // 
            // SanPham
            // 
            this.SanPham.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SanPham.Appearance.ForeColor = System.Drawing.Color.Red;
            this.SanPham.Appearance.Options.UseFont = true;
            this.SanPham.Appearance.Options.UseForeColor = true;
            this.SanPham.Appearance.Options.UseTextOptions = true;
            this.SanPham.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.SanPham.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.SanPham.Location = new System.Drawing.Point(21, 13);
            this.SanPham.Name = "SanPham";
            this.SanPham.Size = new System.Drawing.Size(258, 33);
            this.SanPham.TabIndex = 22;
            this.SanPham.Text = "PL-PRODUCTS";
            // 
            // PhienBan
            // 
            this.PhienBan.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhienBan.Appearance.Options.UseFont = true;
            this.PhienBan.Appearance.Options.UseTextOptions = true;
            this.PhienBan.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.PhienBan.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.PhienBan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.PhienBan.Location = new System.Drawing.Point(24, 51);
            this.PhienBan.Name = "PhienBan";
            this.PhienBan.Size = new System.Drawing.Size(145, 13);
            this.PhienBan.TabIndex = 22;
            this.PhienBan.Text = "Phiên bản v";
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(300, 3);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.AllowFocused = false;
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ReadOnly = true;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit2.Size = new System.Drawing.Size(77, 69);
            this.pictureEdit2.TabIndex = 32;
            // 
            // frmUserError
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 214);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnSendError);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.TenCty);
            this.Controls.Add(this.labError);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "frmUserError";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = HelpApplication.getTitleForm("Thông báo");
            this.Load += new System.EventHandler(this.frmLockApplication_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labError;
        public DevExpress.XtraEditors.LabelControl TenCty;
        private DevExpress.XtraEditors.SimpleButton btnRestart;
        private DevExpress.XtraEditors.SimpleButton btnSendError;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl SanPham;
        private DevExpress.XtraEditors.LabelControl PhienBan;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
    }
}