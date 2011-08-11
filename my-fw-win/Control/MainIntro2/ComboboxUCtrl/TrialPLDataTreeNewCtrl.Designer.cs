namespace ProtocolVN.Framework.Win
{
    partial class UserControlDataTree
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
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.btnChon = new DevExpress.XtraEditors.SimpleButton();
            this.btnDanhMuc = new DevExpress.XtraEditors.SimpleButton();
            this.btnBoChon = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))) , ((int)(((byte)(128)))) , ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))) , ((int)(((byte)(128)))) , ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))) , ((int)(((byte)(128)))) , ((int)(((byte)(255)))));
            this.treeList1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeList1.Location = new System.Drawing.Point(3 , 3);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(337 , 200);
            this.treeList1.TabIndex = 0;
            this.treeList1.DoubleClick += new System.EventHandler(this.treeList1_DoubleClick);
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(178 , 210);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(75 , 23);
            this.btnChon.TabIndex = 1;
            this.btnChon.Text = "Chọn";
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.Location = new System.Drawing.Point(3 , 210);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(75 , 23);
            this.btnDanhMuc.TabIndex = 2;
            this.btnDanhMuc.Text = "Danh Mục";
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            // 
            // btnBoChon
            // 
            this.btnBoChon.Location = new System.Drawing.Point(265 , 210);
            this.btnBoChon.Name = "btnBoChon";
            this.btnBoChon.Size = new System.Drawing.Size(75 , 23);
            this.btnBoChon.TabIndex = 3;
            this.btnBoChon.Text = "Bỏ chọn";
            this.btnBoChon.Click += new System.EventHandler(this.btnBoChon_Click);
            // 
            // UserControlDataTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F , 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBoChon);
            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.treeList1);
            this.Name = "UserControlDataTree";
            this.Size = new System.Drawing.Size(346 , 239);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.SimpleButton btnChon;
        private DevExpress.XtraEditors.SimpleButton btnDanhMuc;
        private DevExpress.XtraEditors.SimpleButton btnBoChon;
    }
}
