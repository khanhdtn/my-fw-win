namespace ProtocolVN.Framework.Win
{
    partial class CondDate
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
            this.NgayTu = new DevExpress.XtraEditors.DateEdit();
            this.NgayDen = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayTu.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayDen.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayDen.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // NgayTu
            // 
            this.NgayTu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NgayTu.EditValue = null;
            this.NgayTu.Location = new System.Drawing.Point(0, 0);
            this.NgayTu.Name = "NgayTu";
            this.NgayTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.NgayTu.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.NgayTu.Size = new System.Drawing.Size(105, 20);
            this.NgayTu.TabIndex = 5;
            // 
            // NgayDen
            // 
            this.NgayDen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NgayDen.EditValue = null;
            this.NgayDen.Location = new System.Drawing.Point(105, 0);
            this.NgayDen.Name = "NgayDen";
            this.NgayDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.NgayDen.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.NgayDen.Size = new System.Drawing.Size(101, 20);
            this.NgayDen.TabIndex = 6;
            // 
            // CondDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NgayDen);
            this.Controls.Add(this.NgayTu);
            this.Name = "CondDate";
            this.Size = new System.Drawing.Size(207, 20);
            ((System.ComponentModel.ISupportInitialize)(this.NgayTu.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayDen.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayDen.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit NgayTu;
        private DevExpress.XtraEditors.DateEdit NgayDen;
    }
}
