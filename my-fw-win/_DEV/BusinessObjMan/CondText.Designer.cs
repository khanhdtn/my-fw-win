namespace ProtocolVN.Framework.Win
{
    partial class CondText
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
            this.giatri = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.giatri.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // giatri
            // 
            this.giatri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.giatri.Location = new System.Drawing.Point(0, 0);
            this.giatri.Name = "giatri";
            this.giatri.Size = new System.Drawing.Size(175, 20);
            this.giatri.TabIndex = 0;
            // 
            // ctr1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.giatri);
            this.Name = "ctr1";
            this.Size = new System.Drawing.Size(175, 20);
            ((System.ComponentModel.ISupportInitialize)(this.giatri.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit giatri;
    }
}
