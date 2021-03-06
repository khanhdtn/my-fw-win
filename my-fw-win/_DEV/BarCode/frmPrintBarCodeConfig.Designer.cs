namespace ProtocolVN.Framework.Win
{
    partial class frmPrintBarCodeConfig
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
            this.gBPrinter_PaperSize = new System.Windows.Forms.GroupBox();
            this.cboPrinter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbChoosePaperSize = new DevExpress.XtraEditors.LabelControl();
            this.cboPaperSize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbChoosePrinter = new DevExpress.XtraEditors.LabelControl();
            this.btnAdPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdPrintToPrinter = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TemNgang = new DevExpress.XtraEditors.SpinEdit();
            this.ChieuRong = new DevExpress.XtraEditors.SpinEdit();
            this.TemDoc = new DevExpress.XtraEditors.SpinEdit();
            this.ChieuCao = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.gBPrinter_PaperSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPrinter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPaperSize.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TemNgang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChieuRong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TemDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChieuCao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gBPrinter_PaperSize
            // 
            this.gBPrinter_PaperSize.Controls.Add(this.cboPrinter);
            this.gBPrinter_PaperSize.Controls.Add(this.ChieuRong);
            this.gBPrinter_PaperSize.Controls.Add(this.lbChoosePaperSize);
            this.gBPrinter_PaperSize.Controls.Add(this.cboPaperSize);
            this.gBPrinter_PaperSize.Controls.Add(this.labelControl6);
            this.gBPrinter_PaperSize.Controls.Add(this.labelControl2);
            this.gBPrinter_PaperSize.Controls.Add(this.labelControl5);
            this.gBPrinter_PaperSize.Controls.Add(this.ChieuCao);
            this.gBPrinter_PaperSize.Controls.Add(this.lbChoosePrinter);
            this.gBPrinter_PaperSize.Controls.Add(this.labelControl4);
            this.gBPrinter_PaperSize.Location = new System.Drawing.Point(12, 12);
            this.gBPrinter_PaperSize.Name = "gBPrinter_PaperSize";
            this.gBPrinter_PaperSize.Size = new System.Drawing.Size(322, 107);
            this.gBPrinter_PaperSize.TabIndex = 5;
            this.gBPrinter_PaperSize.TabStop = false;
            this.gBPrinter_PaperSize.Text = "Máy in -  Khổ giấy";
            // 
            // cboPrinter
            // 
            this.cboPrinter.Location = new System.Drawing.Point(85, 24);
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPrinter.Size = new System.Drawing.Size(227, 20);
            this.cboPrinter.TabIndex = 5;
            this.cboPrinter.SelectedIndexChanged += new System.EventHandler(this.cboPrinter_SelectedIndexChanged);
            // 
            // lbChoosePaperSize
            // 
            this.lbChoosePaperSize.Location = new System.Drawing.Point(10, 53);
            this.lbChoosePaperSize.Name = "lbChoosePaperSize";
            this.lbChoosePaperSize.Size = new System.Drawing.Size(68, 13);
            this.lbChoosePaperSize.TabIndex = 2;
            this.lbChoosePaperSize.Text = "Chọn khổ giấy";
            // 
            // cboPaperSize
            // 
            this.cboPaperSize.Location = new System.Drawing.Point(85, 50);
            this.cboPaperSize.Name = "cboPaperSize";
            this.cboPaperSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPaperSize.Size = new System.Drawing.Size(227, 20);
            this.cboPaperSize.TabIndex = 6;
            this.cboPaperSize.SelectedIndexChanged += new System.EventHandler(this.cboPaperSize_SelectedIndexChanged);
            // 
            // lbChoosePrinter
            // 
            this.lbChoosePrinter.Location = new System.Drawing.Point(10, 27);
            this.lbChoosePrinter.Name = "lbChoosePrinter";
            this.lbChoosePrinter.Size = new System.Drawing.Size(59, 13);
            this.lbChoosePrinter.TabIndex = 0;
            this.lbChoosePrinter.Text = "Chọn máy in";
            // 
            // btnAdPreview
            // 
            this.btnAdPreview.Location = new System.Drawing.Point(186, 202);
            this.btnAdPreview.Name = "btnAdPreview";
            this.btnAdPreview.Size = new System.Drawing.Size(91, 23);
            this.btnAdPreview.TabIndex = 13;
            this.btnAdPreview.Text = "Xem trước khi in";
            this.btnAdPreview.Click += new System.EventHandler(this.btnAdPreview_Click);
            // 
            // btnAdPrintToPrinter
            // 
            this.btnAdPrintToPrinter.Location = new System.Drawing.Point(283, 202);
            this.btnAdPrintToPrinter.Name = "btnAdPrintToPrinter";
            this.btnAdPrintToPrinter.Size = new System.Drawing.Size(50, 23);
            this.btnAdPrintToPrinter.TabIndex = 14;
            this.btnAdPrintToPrinter.Text = "In";
            this.btnAdPrintToPrinter.Click += new System.EventHandler(this.btnAdPrintToPrinter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TemNgang);
            this.groupBox1.Controls.Add(this.TemDoc);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 71);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mẫu in";
            // 
            // TemNgang
            // 
            this.TemNgang.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TemNgang.Location = new System.Drawing.Point(121, 43);
            this.TemNgang.Name = "TemNgang";
            this.TemNgang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TemNgang.Size = new System.Drawing.Size(43, 20);
            this.TemNgang.TabIndex = 10;
            // 
            // ChieuRong
            // 
            this.ChieuRong.EditValue = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            this.ChieuRong.Enabled = false;
            this.ChieuRong.Location = new System.Drawing.Point(85, 76);
            this.ChieuRong.Name = "ChieuRong";
            this.ChieuRong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ChieuRong.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ChieuRong.Size = new System.Drawing.Size(58, 20);
            this.ChieuRong.TabIndex = 9;
            // 
            // TemDoc
            // 
            this.TemDoc.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TemDoc.Location = new System.Drawing.Point(121, 17);
            this.TemDoc.Name = "TemDoc";
            this.TemDoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TemDoc.Size = new System.Drawing.Size(43, 20);
            this.TemDoc.TabIndex = 8;
            // 
            // ChieuCao
            // 
            this.ChieuCao.EditValue = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            this.ChieuCao.Enabled = false;
            this.ChieuCao.Location = new System.Drawing.Point(233, 76);
            this.ChieuCao.Name = "ChieuCao";
            this.ChieuCao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ChieuCao.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ChieuCao.Size = new System.Drawing.Size(58, 20);
            this.ChieuCao.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(94, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Số tem chiều ngang";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Số tem chiều dọc";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 79);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Chiều dài";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(293, 79);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(19, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "inch";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(145, 79);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(19, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "inch";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(178, 79);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Chiều rộng";
            // 
            // frmPrintBarCodeConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 234);
            this.Controls.Add(this.btnAdPreview);
            this.Controls.Add(this.btnAdPrintToPrinter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gBPrinter_PaperSize);
            this.Name = "frmPrintBarCodeConfig";
            this.Text = "Thông số in";
            this.Load += new System.EventHandler(this.frmTestPrintBarCodeConfig_Load);
            this.gBPrinter_PaperSize.ResumeLayout(false);
            this.gBPrinter_PaperSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPrinter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPaperSize.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TemNgang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChieuRong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TemDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChieuCao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBPrinter_PaperSize;
        private DevExpress.XtraEditors.ComboBoxEdit cboPaperSize;
        private DevExpress.XtraEditors.LabelControl lbChoosePrinter;
        private DevExpress.XtraEditors.LabelControl lbChoosePaperSize;
        private DevExpress.XtraEditors.ComboBoxEdit cboPrinter;
        private DevExpress.XtraEditors.SimpleButton btnAdPreview;
        private DevExpress.XtraEditors.SimpleButton btnAdPrintToPrinter;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SpinEdit TemDoc;
        private DevExpress.XtraEditors.SpinEdit ChieuCao;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit ChieuRong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit TemNgang;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}