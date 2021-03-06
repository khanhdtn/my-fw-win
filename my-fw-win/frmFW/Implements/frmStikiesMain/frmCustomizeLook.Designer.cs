namespace ProtocolVN.Plugin.NoteBook
{
    partial class frmCustomizeLook
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlTitleColor = new System.Windows.Forms.Panel();
            this.pnlBgColor = new System.Windows.Forms.Panel();
            this.lblFont = new System.Windows.Forms.Label();
            this.btnFont = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblOpacity = new System.Windows.Forms.Label();
            this.btnTitleFont = new System.Windows.Forms.Button();
            this.lblTitleFont = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Màu nền tiêu đề";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Màu nền ghi chú";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Kiểu chữ ghi chú";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(208, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(127, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlTitleColor
            // 
            this.pnlTitleColor.Location = new System.Drawing.Point(122, 9);
            this.pnlTitleColor.Name = "pnlTitleColor";
            this.pnlTitleColor.Size = new System.Drawing.Size(161, 15);
            this.pnlTitleColor.TabIndex = 6;
            this.pnlTitleColor.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // pnlBgColor
            // 
            this.pnlBgColor.Location = new System.Drawing.Point(122, 67);
            this.pnlBgColor.Name = "pnlBgColor";
            this.pnlBgColor.Size = new System.Drawing.Size(161, 15);
            this.pnlBgColor.TabIndex = 8;
            this.pnlBgColor.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(119, 98);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(98, 13);
            this.lblFont.TabIndex = 9;
            this.lblFont.Text = "Đây là ghi chú mẫu";
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(260, 93);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(23, 22);
            this.btnFont.TabIndex = 10;
            this.btnFont.Text = "...";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Opacity";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(114, 122);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(136, 45);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // lblOpacity
            // 
            this.lblOpacity.AutoSize = true;
            this.lblOpacity.Location = new System.Drawing.Point(244, 127);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(0, 13);
            this.lblOpacity.TabIndex = 13;
            // 
            // btnTitleFont
            // 
            this.btnTitleFont.Location = new System.Drawing.Point(259, 34);
            this.btnTitleFont.Name = "btnTitleFont";
            this.btnTitleFont.Size = new System.Drawing.Size(23, 22);
            this.btnTitleFont.TabIndex = 16;
            this.btnTitleFont.Text = "...";
            this.btnTitleFont.UseVisualStyleBackColor = true;
            this.btnTitleFont.Click += new System.EventHandler(this.btnTitleFont_Click);
            // 
            // lblTitleFont
            // 
            this.lblTitleFont.AutoSize = true;
            this.lblTitleFont.Location = new System.Drawing.Point(122, 39);
            this.lblTitleFont.Name = "lblTitleFont";
            this.lblTitleFont.Size = new System.Drawing.Size(97, 13);
            this.lblTitleFont.TabIndex = 15;
            this.lblTitleFont.Text = "Đây là tiêu đề mẫu";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Kiểu chữ tiêu đề";
            // 
            // frmCustomizeLook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 201);
            this.Controls.Add(this.btnTitleFont);
            this.Controls.Add(this.lblTitleFont);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOpacity);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.pnlBgColor);
            this.Controls.Add(this.pnlTitleColor);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCustomizeLook";
            this.Text = "Tùy biến mẫu ghi chú";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlTitleColor;
        private System.Windows.Forms.Panel pnlBgColor;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblOpacity;
        private System.Windows.Forms.Button btnTitleFont;
        private System.Windows.Forms.Label lblTitleFont;
        private System.Windows.Forms.Label label6;
    }
}