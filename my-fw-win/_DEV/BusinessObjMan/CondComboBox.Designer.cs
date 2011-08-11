namespace ProtocolVN.Framework.Win
{
    partial class CondComboBox
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
            this.cbID = new ProtocolVN.Framework.Win.PLCombobox();
            this.SuspendLayout();
            // 
            // cbID
            // 
            this.cbID.DataSource = null;
            this.cbID.DisplayField = null;
            this.cbID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbID.Location = new System.Drawing.Point(0, 0);
            this.cbID.Name = "cbID";
            this.cbID.Size = new System.Drawing.Size(175, 20);
            this.cbID.TabIndex = 48;
            this.cbID.ValueField = null;
            // 
            // CondComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbID);
            this.Name = "CondComboBox";
            this.Size = new System.Drawing.Size(175, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private PLCombobox cbID;
    }
}
