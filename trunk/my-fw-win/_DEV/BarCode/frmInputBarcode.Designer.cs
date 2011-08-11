namespace ProtocolVN.Framework.Win
{
    partial class frmInputBarcode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInputBarcode));
            this.barCode = new DevExpress.XtraEditors.TextEdit();
            this.Slg = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this._SoLbl = new DevExpress.XtraEditors.LabelControl();
            this._TenSPLbl = new DevExpress.XtraEditors.LabelControl();
            this._TenSP = new DevExpress.XtraEditors.TextEdit();
            this._GiaLbl = new DevExpress.XtraEditors.LabelControl();
            this._Gia = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Slg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._TenSP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barCode
            // 
            this.barCode.EditValue = "";
            this.barCode.Location = new System.Drawing.Point(84, 4);
            this.barCode.Name = "barCode";
            this.barCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barCode.Properties.Appearance.Options.UseFont = true;
            this.barCode.Size = new System.Drawing.Size(228, 39);
            this.barCode.TabIndex = 1;
            // 
            // Slg
            // 
            this.Slg.EditValue = "1";
            this.Slg.Location = new System.Drawing.Point(412, 4);
            this.Slg.Name = "Slg";
            this.Slg.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Slg.Properties.Appearance.Options.UseFont = true;
            this.Slg.Size = new System.Drawing.Size(74, 39);
            this.Slg.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl1.Appearance.Image")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseImage = true;
            this.labelControl1.Location = new System.Drawing.Point(6, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 45);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "      ";
            // 
            // _SoLbl
            // 
            this._SoLbl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._SoLbl.Appearance.Options.UseFont = true;
            this._SoLbl.Appearance.Options.UseTextOptions = true;
            this._SoLbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._SoLbl.Location = new System.Drawing.Point(335, 18);
            this._SoLbl.Name = "_SoLbl";
            this._SoLbl.Size = new System.Drawing.Size(71, 19);
            this._SoLbl.TabIndex = 2;
            this._SoLbl.Text = "Số lượng";
            // 
            // _TenSPLbl
            // 
            this._TenSPLbl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._TenSPLbl.Appearance.Options.UseFont = true;
            this._TenSPLbl.Location = new System.Drawing.Point(6, 49);
            this._TenSPLbl.Name = "_TenSPLbl";
            this._TenSPLbl.Size = new System.Drawing.Size(114, 19);
            this._TenSPLbl.TabIndex = 4;
            this._TenSPLbl.Text = "Tên sản phẩm";
            // 
            // _TenSP
            // 
            this._TenSP.Enabled = false;
            this._TenSP.Location = new System.Drawing.Point(6, 69);
            this._TenSP.Name = "_TenSP";
            this._TenSP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._TenSP.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this._TenSP.Properties.Appearance.Options.UseFont = true;
            this._TenSP.Properties.Appearance.Options.UseForeColor = true;
            this._TenSP.Properties.ReadOnly = true;
            this._TenSP.Size = new System.Drawing.Size(480, 45);
            this._TenSP.TabIndex = 5;
            this._TenSP.TabStop = false;
            // 
            // _GiaLbl
            // 
            this._GiaLbl.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._GiaLbl.Appearance.Options.UseFont = true;
            this._GiaLbl.Location = new System.Drawing.Point(335, 51);
            this._GiaLbl.Name = "_GiaLbl";
            this._GiaLbl.Size = new System.Drawing.Size(18, 16);
            this._GiaLbl.TabIndex = 4;
            this._GiaLbl.Text = "Giá";
            this._GiaLbl.Visible = false;
            // 
            // _Gia
            // 
            this._Gia.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Gia.Appearance.ForeColor = System.Drawing.Color.Blue;
            this._Gia.Appearance.Options.UseFont = true;
            this._Gia.Appearance.Options.UseForeColor = true;
            this._Gia.Location = new System.Drawing.Point(359, 49);
            this._Gia.Name = "_Gia";
            this._Gia.Size = new System.Drawing.Size(115, 19);
            this._Gia.TabIndex = 4;
            this._Gia.Text = "1.000.000.000";
            this._Gia.Visible = false;
            // 
            // frmInputBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 118);
            this.Controls.Add(this._SoLbl);
            this.Controls.Add(this._Gia);
            this.Controls.Add(this._GiaLbl);
            this.Controls.Add(this._TenSPLbl);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.Slg);
            this.Controls.Add(this._TenSP);
            this.Controls.Add(this.barCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmInputBarcode";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmInputBarcode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Slg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._TenSP.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit barCode;
        private DevExpress.XtraEditors.TextEdit Slg;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl _SoLbl;
        private DevExpress.XtraEditors.LabelControl _TenSPLbl;
        private DevExpress.XtraEditors.TextEdit _TenSP;
        private DevExpress.XtraEditors.LabelControl _GiaLbl;
        private DevExpress.XtraEditors.LabelControl _Gia;
    }
}