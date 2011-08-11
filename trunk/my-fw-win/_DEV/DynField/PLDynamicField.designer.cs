namespace ProtocolVN.Framework.Win
{
    partial class PLDynamicField
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
            this.customizeField_vgc = new DevExpress.XtraVerticalGrid.VGridControl();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.customizeField_vgc)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customizeField_vgc
            // 
            this.customizeField_vgc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customizeField_vgc.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            this.customizeField_vgc.Location = new System.Drawing.Point(0, 0);
            this.customizeField_vgc.Name = "customizeField_vgc";
            this.customizeField_vgc.Size = new System.Drawing.Size(312, 385);
            this.customizeField_vgc.TabIndex = 2;
            this.customizeField_vgc.Leave += new System.EventHandler(this.customizeField_vgc_Leave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customizeField_vgc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 385);
            this.panel1.TabIndex = 4;
            // 
            // PLDynamicField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PLDynamicField";
            this.Size = new System.Drawing.Size(312, 385);
            ((System.ComponentModel.ISupportInitialize)(this.customizeField_vgc)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraVerticalGrid.VGridControl customizeField_vgc;
        private System.Windows.Forms.Panel panel1;
    }
}
