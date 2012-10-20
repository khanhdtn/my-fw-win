namespace ProtocolVN.Framework.Win
{
    partial class DMGrid
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
            this.btnBar = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.ChonSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnNoSelect = new System.Windows.Forms.ToolStripButton();
            this.ChiTietSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnInfo = new System.Windows.Forms.ToolStripButton();
            this.LuuSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnNoSave = new System.Windows.Forms.ToolStripButton();
            this.DongSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_1
            // 
            this.btnBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.btnBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.btnBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDel,
            this.btnUpdate,
            this.ChonSep,
            this.btnSelect,
            this.btnNoSelect,
            this.ChiTietSep,
            this.btnInfo,
            this.LuuSep,
            this.btnSave,
            this.btnNoSave,
            this.DongSep,
            this.btnClose});
            this.btnBar.Location = new System.Drawing.Point(0, 0);
            this.btnBar.Name = "toolStrip_1";
            this.btnBar.Size = new System.Drawing.Size(554, 25);
            this.btnBar.TabIndex = 34;
            this.btnBar.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAdd.Size = new System.Drawing.Size(47, 22);
            this.btnAdd.Text = "&Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(29, 22);
            this.btnDel.Text = "&Xóa";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(30, 22);
            this.btnUpdate.Text = "&Sửa";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // ChonSep
            // 
            this.ChonSep.Name = "ChonSep";
            this.ChonSep.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSelect
            // 
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(36, 22);
            this.btnSelect.Text = "&Chọn";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnNoSelect
            // 
            this.btnNoSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNoSelect.Name = "btnNoSelect";
            this.btnNoSelect.Size = new System.Drawing.Size(49, 22);
            this.btnNoSelect.Text = "&Bỏ chọn";
            this.btnNoSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoSelect.Click += new System.EventHandler(this.btnNoSelect_Click);
            // 
            // ChiTietSep
            // 
            this.ChiTietSep.Name = "ChiTietSep";
            this.ChiTietSep.Size = new System.Drawing.Size(6, 25);
            this.ChiTietSep.Visible = false;
            // 
            // btnInfo
            // 
            this.btnInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(45, 22);
            this.btnInfo.Text = "Chi &tiết";
            this.btnInfo.Visible = false;
            // 
            // LuuSep
            // 
            this.LuuSep.Name = "LuuSep";
            this.LuuSep.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(29, 22);
            this.btnSave.Text = "&Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNoSave
            // 
            this.btnNoSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNoSave.Name = "btnNoSave";
            this.btnNoSave.Size = new System.Drawing.Size(59, 22);
            this.btnNoSave.Text = "&Không lưu";
            this.btnNoSave.Click += new System.EventHandler(this.btnNoSave_Click);
            // 
            // DongSep
            // 
            this.DongSep.Name = "DongSep";
            this.DongSep.Size = new System.Drawing.Size(6, 25);
            // 
            // Close
            // 
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "Close";
            this.btnClose.Size = new System.Drawing.Size(37, 22);
            this.btnClose.Text = "Đón&g";
            this.btnClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // DMGridTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.Controls.Add(this.toolStrip_1);
            this.Name = "DMGridTemplate";
            this.Size = new System.Drawing.Size(554, 446);
            this.btnBar.ResumeLayout(false);
            this.btnBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip btnBar;
        public System.Windows.Forms.ToolStripButton btnAdd;
        public System.Windows.Forms.ToolStripButton btnDel;
        public System.Windows.Forms.ToolStripButton btnUpdate;
        public System.Windows.Forms.ToolStripSeparator ChonSep;
        public System.Windows.Forms.ToolStripButton btnSelect;
        public System.Windows.Forms.ToolStripButton btnNoSelect;
        public System.Windows.Forms.ToolStripSeparator ChiTietSep;
        public System.Windows.Forms.ToolStripButton btnInfo;
        public System.Windows.Forms.ToolStripSeparator LuuSep;
        public System.Windows.Forms.ToolStripButton btnSave;
        public System.Windows.Forms.ToolStripButton btnNoSave;
        public System.Windows.Forms.ToolStripSeparator DongSep;
        public System.Windows.Forms.ToolStripButton btnClose;

    }
}
