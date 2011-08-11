namespace ProtocolVN.Framework.Win
{
    partial class PLDateTime
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ThoiGian = new DevExpress.XtraEditors.TimeEdit();
            this.Ngay = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ThoiGian.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ThoiGian
            // 
            this.ThoiGian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ThoiGian.EditValue = new System.DateTime(2009, 1, 14, 0, 0, 0, 0);
            this.ThoiGian.Location = new System.Drawing.Point(121, 0);
            this.ThoiGian.Name = "ThoiGian";
            this.ThoiGian.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ThoiGian.Size = new System.Drawing.Size(68, 20);
            this.ThoiGian.TabIndex = 27;
            // 
            // Ngay
            // 
            this.Ngay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Ngay.EditValue = null;
            this.Ngay.Location = new System.Drawing.Point(3, 0);
            this.Ngay.Name = "Ngay";
            this.Ngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Ngay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Ngay.Size = new System.Drawing.Size(116, 20);
            this.Ngay.TabIndex = 28;
            // 
            // PLDateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Ngay);
            this.Controls.Add(this.ThoiGian);
            this.Name = "PLDateTime";
            this.Size = new System.Drawing.Size(189, 20);
            ((System.ComponentModel.ISupportInitialize)(this.ThoiGian.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TimeEdit ThoiGian;
        private DevExpress.XtraEditors.DateEdit Ngay;
    }
}
