namespace ProtocolVN.Framework.Win
{
    partial class PLCheckedComboBox
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
            this.cCB_Edit = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cCB_Edit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cCB_Edit
            // 
            this.cCB_Edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cCB_Edit.Location = new System.Drawing.Point(0, 0);
            this.cCB_Edit.Name = "cCB_Edit";
            this.cCB_Edit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cCB_Edit.Size = new System.Drawing.Size(199, 20);
            this.cCB_Edit.TabIndex = 5;
            // 
            // PLCheckedComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cCB_Edit);
            this.Name = "PLCheckedComboBox";
            this.Size = new System.Drawing.Size(199, 20);
            ((System.ComponentModel.ISupportInitialize)(this.cCB_Edit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckedComboBoxEdit cCB_Edit;
    }
}
