namespace ProtocolVN.Framework.Win
{
    partial class PLMoneyType
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
            this.PLNgoaiTe = new ProtocolVN.Framework.Win.PLCombobox();
            this.btnUpdateTi_Gia = new DevExpress.XtraEditors.SimpleButton();
            this.TiGia = new DevExpress.XtraEditors.CalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.TiGia.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // PLNgoaiTe
            // 
            this.PLNgoaiTe.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PLNgoaiTe.DataSource = null;
            this.PLNgoaiTe.DisplayField = null;
            this.PLNgoaiTe.Location = new System.Drawing.Point(0, 0);
            this.PLNgoaiTe.Name = "PLNgoaiTe";
            this.PLNgoaiTe.Size = new System.Drawing.Size(92, 20);
            this.PLNgoaiTe.TabIndex = 18;
            this.PLNgoaiTe.ValueField = null;
            this.PLNgoaiTe.SelectedIndexChanged += new System.EventHandler(this.PLNgoaiTe_SelectedIndexChanged);
            // 
            // btnUpdateTi_Gia
            // 
            this.btnUpdateTi_Gia.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUpdateTi_Gia.Appearance.Options.UseTextOptions = true;
            this.btnUpdateTi_Gia.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnUpdateTi_Gia.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnUpdateTi_Gia.Location = new System.Drawing.Point(188, 1);
            this.btnUpdateTi_Gia.Name = "btnUpdateTi_Gia";
            this.btnUpdateTi_Gia.Size = new System.Drawing.Size(21, 19);
            this.btnUpdateTi_Gia.TabIndex = 22;
            this.btnUpdateTi_Gia.Text = "...";
            this.btnUpdateTi_Gia.Click += new System.EventHandler(this.btnUpdateTi_Gia_Click);
            // 
            // TiGia
            // 
            this.TiGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TiGia.Location = new System.Drawing.Point(93, 0);
            this.TiGia.Name = "TiGia";
            this.TiGia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TiGia.Properties.ReadOnly = true;
            this.TiGia.Size = new System.Drawing.Size(95, 20);
            this.TiGia.TabIndex = 21;
            this.TiGia.EditValueChanged += new System.EventHandler(this.TiGia_EditValueChanged);
            // 
            // PLMoneyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdateTi_Gia);
            this.Controls.Add(this.TiGia);
            this.Controls.Add(this.PLNgoaiTe);
            this.Name = "PLMoneyType";
            this.Size = new System.Drawing.Size(209, 20);
            ((System.ComponentModel.ISupportInitialize)(this.TiGia.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ProtocolVN.Framework.Win.PLCombobox PLNgoaiTe;
        public DevExpress.XtraEditors.SimpleButton btnUpdateTi_Gia;
        private DevExpress.XtraEditors.CalcEdit TiGia;
    }
}
