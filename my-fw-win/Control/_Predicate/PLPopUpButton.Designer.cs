namespace ProtocolVN.Framework.Win
{
    partial class PLPopUpButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLPopUpButton));
            this.but = new DevExpress.XtraEditors.SimpleButton();
            this.popup = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // but
            // 
            this.but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.but.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.but.Appearance.Options.UseBackColor = true;
            this.but.Location = new System.Drawing.Point(0, 0);
            this.but.Name = "but";
            this.but.Size = new System.Drawing.Size(118, 23);
            this.but.TabIndex = 1;
            this.but.Click += new System.EventHandler(this.but_Click);
            // 
            // popup
            // 
            this.popup.AllowFocus = false;
            this.popup.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.popup.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.popup.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.popup.Appearance.Options.UseBackColor = true;
            this.popup.Appearance.Options.UseFont = true;
            this.popup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.popup.Image = ((System.Drawing.Image)(resources.GetObject("popup.Image")));
            this.popup.Location = new System.Drawing.Point(117, 0);
            this.popup.Name = "popup";
            this.popup.ShowToolTips = false;
            this.popup.Size = new System.Drawing.Size(23, 23);
            this.popup.TabIndex = 2;
            this.popup.MouseLeave += new System.EventHandler(this.popup_MouseLeave);
            this.popup.Click += new System.EventHandler(this.popup_Click);
            // 
            // PLPopUpButton
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popup);
            this.Controls.Add(this.but);
            this.Name = "PLPopUpButton";
            this.Size = new System.Drawing.Size(140, 23);
            this.Leave += new System.EventHandler(this.popup_Leave);
            this.MouseLeave += new System.EventHandler(this.popup_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton but;
        private DevExpress.XtraEditors.SimpleButton popup;
        
    }
}
