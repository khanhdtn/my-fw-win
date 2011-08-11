namespace ProtocolVN.Framework.Win
{
    partial class frmFWUserFormMan
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
            this.gridControlForm = new DevExpress.XtraGrid.GridControl();
            this.gridViewForm = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CotTenForm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotMoTa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CotHienThi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_Luu = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Dong = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForm)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlForm
            // 
            this.gridControlForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlForm.Location = new System.Drawing.Point(0, 27);
            this.gridControlForm.MainView = this.gridViewForm;
            this.gridControlForm.Name = "gridControlForm";
            this.gridControlForm.Size = new System.Drawing.Size(492, 231);
            this.gridControlForm.TabIndex = 0;
            this.gridControlForm.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewForm});
            // 
            // gridViewForm
            // 
            this.gridViewForm.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CotTenForm,
            this.CotMoTa,
            this.CotHienThi});
            this.gridViewForm.GridControl = this.gridControlForm;
            this.gridViewForm.Name = "gridViewForm";
            // 
            // CotTenForm
            // 
            this.CotTenForm.Caption = "Tên Form";
            this.CotTenForm.Name = "CotTenForm";
            this.CotTenForm.OptionsColumn.ReadOnly = true;
            // 
            // CotMoTa
            // 
            this.CotMoTa.Caption = "Mô tả";
            this.CotMoTa.Name = "CotMoTa";
            this.CotMoTa.OptionsColumn.ReadOnly = true;
            this.CotMoTa.Visible = true;
            this.CotMoTa.VisibleIndex = 0;
            // 
            // CotHienThi
            // 
            this.CotHienThi.Caption = "Hiện";
            this.CotHienThi.Name = "CotHienThi";
            this.CotHienThi.Visible = true;
            this.CotHienThi.VisibleIndex = 1;
            // 
            // btn_Luu
            // 
            this.btn_Luu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Luu.Location = new System.Drawing.Point(385, 264);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Size = new System.Drawing.Size(47, 23);
            this.btn_Luu.TabIndex = 1;
            this.btn_Luu.Text = "&Lưu";
            this.btn_Luu.Click += new System.EventHandler(this.btn_Luu_Click);
            // 
            // btn_Dong
            // 
            this.btn_Dong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Dong.Location = new System.Drawing.Point(438, 264);
            this.btn_Dong.Name = "btn_Dong";
            this.btn_Dong.Size = new System.Drawing.Size(47, 23);
            this.btn_Dong.TabIndex = 2;
            this.btn_Dong.Text = "Đóng";
            this.btn_Dong.Click += new System.EventHandler(this.btn_Dong_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(302, 264);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(76, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "&Mở màn hình";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Danh sách màn hình ";
            // 
            // frmFWUserFormMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 292);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btn_Dong);
            this.Controls.Add(this.btn_Luu);
            this.Controls.Add(this.gridControlForm);
            this.Name = "frmFWUserFormMan";
            this.Text = "Ẩn/Hiện các màn hình";
            this.Load += new System.EventHandler(this.Manage_Show_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlForm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewForm;
        private DevExpress.XtraEditors.SimpleButton btn_Luu;
        private DevExpress.XtraEditors.SimpleButton btn_Dong;
        private DevExpress.XtraGrid.Columns.GridColumn CotTenForm;
        private DevExpress.XtraGrid.Columns.GridColumn CotMoTa;
        private DevExpress.XtraGrid.Columns.GridColumn CotHienThi;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}