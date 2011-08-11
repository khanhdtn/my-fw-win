namespace ProtocolVN.Framework.Win
{
    partial class PatternSelect
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
            this.txtNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtDate = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPattern = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPattern.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(175, 3);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Properties.Mask.EditMask = "[#]+";
            this.txtNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.txtNumber.Size = new System.Drawing.Size(44, 20);
            this.txtNumber.TabIndex = 17;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(78, 3);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDate.Properties.Items.AddRange(new object[] {
            "",
            "DDMMYYYY",
            "MMYYYY",
            "YYYY",
            "YYYYMM",
            "DDMMYY"});
            this.txtDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtDate.Size = new System.Drawing.Size(91, 20);
            this.txtDate.TabIndex = 16;
            // 
            // txtPattern
            // 
            this.txtPattern.Location = new System.Drawing.Point(3, 3);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(69, 20);
            this.txtPattern.TabIndex = 17;
            // 
            // PatternSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtPattern);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtDate);
            this.Name = "PatternSelect";
            this.Size = new System.Drawing.Size(223, 27);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPattern.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtNumber;
        private DevExpress.XtraEditors.ComboBoxEdit txtDate;
        private DevExpress.XtraEditors.TextEdit txtPattern;
    }
}
