namespace ProtocolVN.Framework.Win
{
    partial class frmTram
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
            this.Tuyen = new ProtocolVN.Framework.Win.PLCombobox();
            this.label2 = new System.Windows.Forms.Label();
            this.Tram = new ProtocolVN.Framework.Win.PLCombobox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Noidung = new DevExpress.XtraEditors.MemoEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnPlay = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Noidung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tuyen
            // 
            this.Tuyen.DataSource = null;
            this.Tuyen.DisplayField = null;
            this.Tuyen.Location = new System.Drawing.Point(67, 6);
            this.Tuyen.Name = "Tuyen";
            this.Tuyen.Size = new System.Drawing.Size(290, 20);
            this.Tuyen.TabIndex = 12;
            this.Tuyen.ValueField = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tuyến";
            // 
            // Tram
            // 
            this.Tram.DataSource = null;
            this.Tram.DisplayField = null;
            this.Tram.Location = new System.Drawing.Point(67, 32);
            this.Tram.Name = "Tram";
            this.Tram.Size = new System.Drawing.Size(290, 20);
            this.Tram.TabIndex = 14;
            this.Tram.ValueField = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Trạm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Nội dung";
            // 
            // Noidung
            // 
            this.Noidung.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Noidung.Location = new System.Drawing.Point(67, 58);
            this.Noidung.Name = "Noidung";
            this.Noidung.Size = new System.Drawing.Size(330, 118);
            this.Noidung.TabIndex = 16;
            this.Noidung.ToolTip = "Mô tả thêm cho trạm thuộc tuyến này";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Âm thanh";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.panelControl1.Controls.Add(this.btnPlay);
            this.panelControl1.Controls.Add(this.btnStop);
            this.panelControl1.Controls.Add(this.btnStart);
            this.panelControl1.Location = new System.Drawing.Point(67, 182);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(330, 39);
            this.panelControl1.TabIndex = 18;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(241, 228);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Location = new System.Drawing.Point(322, 228);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 20;
            this.btnThoat.Text = "Thoát";
            // 
            // btnStop
            // 
            this.btnStop.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnStop.Image = global::ProtocolVN.Framework.Win.Dev.Properties.Resources.stop;
            this.btnStop.Location = new System.Drawing.Point(40, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(29, 29);
            this.btnStop.TabIndex = 19;
            this.btnStop.ToolTip = "Dừng ghi âm";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnStart.Image = global::ProtocolVN.Framework.Win.Dev.Properties.Resources.record;
            this.btnStart.Location = new System.Drawing.Point(5, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(29, 29);
            this.btnStart.TabIndex = 19;
            this.btnStart.ToolTip = "Bắt đầu ghi âm";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnPlay.Image = global::ProtocolVN.Framework.Win.Dev.Properties.Resources.play;
            this.btnPlay.Location = new System.Drawing.Point(75, 5);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(29, 29);
            this.btnPlay.TabIndex = 20;
            this.btnPlay.ToolTip = "Phát thử";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // frmTram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 258);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Noidung);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Tram);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tuyen);
            this.Controls.Add(this.label2);
            this.Name = "frmTram";
            this.Text = "Trạm";
            ((System.ComponentModel.ISupportInitialize)(this.Noidung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PLCombobox Tuyen;
        private System.Windows.Forms.Label label2;
        private PLCombobox Tram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.MemoEdit Noidung;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnPlay;
    }
}