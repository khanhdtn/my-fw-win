namespace ProtocolVN.Framework.Win
{
    partial class frmFWLiveUpdate
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
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnCapNhat = new DevExpress.XtraEditors.SimpleButton();
            this.txtURL = new DevExpress.XtraEditors.TextEdit();
            this.btnEditFileDuLieu = new DevExpress.XtraEditors.ButtonEdit();
            this.rdoURL = new System.Windows.Forms.RadioButton();
            this.rdoFile = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtURL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFileDuLieu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(361, 188);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(70, 23);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Đóng";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Enabled = false;
            this.btnCapNhat.Location = new System.Drawing.Point(285, 188);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(70, 23);
            this.btnCapNhat.TabIndex = 9;
            this.btnCapNhat.Text = "Thực hiện";
            this.btnCapNhat.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // txtURL
            // 
            this.txtURL.EditValue = "www.protocolvn.com";
            this.txtURL.Location = new System.Drawing.Point(125, 132);
            this.txtURL.Name = "txtURL";
            this.txtURL.Properties.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(306, 20);
            this.txtURL.TabIndex = 6;
            this.txtURL.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            // 
            // btnEditFileDuLieu
            // 
            this.btnEditFileDuLieu.Enabled = false;
            this.btnEditFileDuLieu.Location = new System.Drawing.Point(125, 158);
            this.btnEditFileDuLieu.Name = "btnEditFileDuLieu";
            this.btnEditFileDuLieu.Properties.AllowFocused = false;
            this.btnEditFileDuLieu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditFileDuLieu.Size = new System.Drawing.Size(306, 20);
            this.btnEditFileDuLieu.TabIndex = 8;
            this.btnEditFileDuLieu.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditFileDuLieu_ButtonClick);
            this.btnEditFileDuLieu.EditValueChanged += new System.EventHandler(this.btnEditFileDuLieu_EditValueChanged);
            // 
            // rdoURL
            // 
            this.rdoURL.AutoSize = true;
            this.rdoURL.Checked = true;
            this.rdoURL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoURL.ForeColor = System.Drawing.Color.Black;
            this.rdoURL.Location = new System.Drawing.Point(12, 132);
            this.rdoURL.Name = "rdoURL";
            this.rdoURL.Size = new System.Drawing.Size(107, 18);
            this.rdoURL.TabIndex = 5;
            this.rdoURL.TabStop = true;
            this.rdoURL.Text = "Từ PROTOCOL";
            this.rdoURL.UseVisualStyleBackColor = true;
            this.rdoURL.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rdoFile
            // 
            this.rdoFile.AutoSize = true;
            this.rdoFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoFile.ForeColor = System.Drawing.Color.Black;
            this.rdoFile.Location = new System.Drawing.Point(12, 158);
            this.rdoFile.Name = "rdoFile";
            this.rdoFile.Size = new System.Drawing.Size(81, 18);
            this.rdoFile.TabIndex = 7;
            this.rdoFile.Text = "Từ tập tin";
            this.rdoFile.UseVisualStyleBackColor = true;
            this.rdoFile.CheckedChanged += new System.EventHandler(this.rdoFile_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 101);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(301, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Chọn cách cập nhật phiên bản mới vào máy chủ nội bộ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl2.Location = new System.Drawing.Point(29, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(194, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "- Đóng các chương trình không sử dụng.";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl3.Location = new System.Drawing.Point(29, 32);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(201, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "- Đóng tất cả các màn hình đang làm việc.";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl4.Location = new System.Drawing.Point(29, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(163, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "- Sao lưu phiên bản đang sử dụng";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(12, 12);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(388, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Bạn nên thực hiện các công việc sau trước khi cập nhật phiên bản mới";
            // 
            // frmFWLiveUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 223);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.rdoURL);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.rdoFile);
            this.Controls.Add(this.btnEditFileDuLieu);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnCapNhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.LookAndFeel.SkinName = "Blue";
            this.MaximizeBox = false;
            this.Name = "frmFWLiveUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật vào máy chủ nội bộ";
            this.Load += new System.EventHandler(this.frmLiveUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtURL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFileDuLieu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnCapNhat;
        private DevExpress.XtraEditors.TextEdit txtURL;
        private DevExpress.XtraEditors.ButtonEdit btnEditFileDuLieu;
        private System.Windows.Forms.RadioButton rdoURL;
        private System.Windows.Forms.RadioButton rdoFile;
        protected DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.LabelControl labelControl2;
        protected DevExpress.XtraEditors.LabelControl labelControl3;
        protected DevExpress.XtraEditors.LabelControl labelControl4;
        protected DevExpress.XtraEditors.LabelControl labelControl5;
    }
}

