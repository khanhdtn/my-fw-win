namespace ProtocolVN.Framework.Win
{
    partial class CondNumber
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
            this.SoTu = new DevExpress.XtraEditors.SpinEdit();
            this.SoDen = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.SoTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoDen.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // SoTu
            // 
            this.SoTu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SoTu.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SoTu.Location = new System.Drawing.Point(0, 0);
            this.SoTu.Name = "SoTu";
            this.SoTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SoTu.Size = new System.Drawing.Size(136, 20);
            this.SoTu.TabIndex = 1;
            // 
            // SoDen
            // 
            this.SoDen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SoDen.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SoDen.Location = new System.Drawing.Point(135, 0);
            this.SoDen.Name = "SoDen";
            this.SoDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SoDen.Size = new System.Drawing.Size(136, 20);
            this.SoDen.TabIndex = 3;
            // 
            // CondNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SoDen);
            this.Controls.Add(this.SoTu);
            this.Name = "CondNumber";
            this.Size = new System.Drawing.Size(270, 20);
            ((System.ComponentModel.ISupportInitialize)(this.SoTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoDen.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit SoTu;
        private DevExpress.XtraEditors.SpinEdit SoDen;
    }
}
