namespace ProtocolVN.Framework.Win
{
    partial class frmFWLockApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFWLockApplication));
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.labError = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnControl = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.TenCty = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SanPham = new DevExpress.XtraEditors.LabelControl();
            this.PhienBan = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(171, 187);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(165, 21);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mật khẩu";
            // 
            // btnControl
            // 
            this.btnControl.Location = new System.Drawing.Point(188, 229);
            this.btnControl.Name = "btnControl";
            this.btnControl.Size = new System.Drawing.Size(70, 23);
            this.btnControl.TabIndex = 3;
            this.btnControl.Text = "Mở khóa";
            this.btnControl.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên đăng nhập";
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.Location = new System.Drawing.Point(168, 154);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(103, 16);
            this.username.TabIndex = 1;
            this.username.Text = "Tên đăng nhập";
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
            this.TenCty.Location = new System.Drawing.Point(12, 95);
            this.TenCty.Name = "TenCty";
            this.TenCty.Size = new System.Drawing.Size(425, 28);
            this.TenCty.TabIndex = 48;
            this.TenCty.Text = "Chương trình tạm thời bị khóa. Để mở khóa vui lòng nhập vào mật khẩu tương ứng vớ" +
                "i tên đăng nhập bên dưới.";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.SanPham);
            this.panel1.Controls.Add(this.PhienBan);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 77);
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
            this.SanPham.Size = new System.Drawing.Size(292, 33);
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
            this.PhienBan.Size = new System.Drawing.Size(127, 13);
            this.PhienBan.TabIndex = 22;
            this.PhienBan.Text = "Phiên bản v";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(319, 4);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(77, 69);
            this.pictureEdit1.TabIndex = 32;
            // 
            // frmLockApplication
            // 
            this.AcceptButton = this.btnControl;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 275);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TenCty);
            this.Controls.Add(this.btnControl);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labError);
            this.Controls.Add(this.txtPassword);
            this.DoubleBuffered = true;
            this.LookAndFeel.SkinName = "Lilian";
            this.MaximizeBox = false;
            this.Name = "frmLockApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khoá chương trình";
            this.Load += new System.EventHandler(this.frmLockApplication_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLockApplication_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label labError;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label username;
        public DevExpress.XtraEditors.LabelControl TenCty;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl SanPham;
        private DevExpress.XtraEditors.LabelControl PhienBan;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
    }
}