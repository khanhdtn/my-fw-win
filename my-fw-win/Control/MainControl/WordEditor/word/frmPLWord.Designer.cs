namespace ProtocolVN.Framework.Win
{
    partial class frmPLWord
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
            this.plWord1 = new ProtocolVN.Framework.Win.PLWord();
            this.SuspendLayout();
            // 
            // plWord1
            // 
            this.plWord1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plWord1.Location = new System.Drawing.Point(0, 0);
            this.plWord1.Name = "plWord1";
            this.plWord1.Size = new System.Drawing.Size(902, 509);
            this.plWord1.TabIndex = 0;
            // 
            // frmPLWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 509);
            this.Controls.Add(this.plWord1);
            this.Name = "frmPLWord";
            this.Text = "Chỉnh sửa tài liệu Word";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPLWord_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private PLWord plWord1;
    }
}