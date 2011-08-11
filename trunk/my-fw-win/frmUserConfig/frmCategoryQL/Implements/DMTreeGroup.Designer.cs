
using ProtocolVN.Framework.Win;
namespace ProtocolVN.Framework.Win
{
    partial class DMTreeGroup
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip_1 = new System.Windows.Forms.ToolStrip();
            this.addSameLevel = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.ChonSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnNoSelect = new System.Windows.Forms.ToolStripButton();
            this.LuuSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnLuu = new System.Windows.Forms.ToolStripButton();
            this.btnKhongLuu = new System.Windows.Forms.ToolStripButton();
            this.DongSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.TreeList_1 = new ProtocolVN.Framework.Win.PLDataTree();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeList_1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip_1
            // 
            this.toolStrip_1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSameLevel,
            this.btnAdd,
            this.btnDel,
            this.btnUpdate,
            this.ChonSep,
            this.btnSelect,
            this.btnNoSelect,
            this.LuuSep,
            this.btnLuu,
            this.btnKhongLuu,
            this.DongSep,
            this.btnClose});
            this.toolStrip_1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_1.Name = "toolStrip_1";
            this.toolStrip_1.Size = new System.Drawing.Size(508, 25);
            this.toolStrip_1.TabIndex = 33;
            this.toolStrip_1.Text = "toolStrip1";
            // 
            // addSameLevel
            // 
            this.addSameLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSameLevel.Name = "addSameLevel";
            this.addSameLevel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.addSameLevel.Size = new System.Drawing.Size(47, 22);
            this.addSameLevel.Text = "&Thêm";
            this.addSameLevel.Click += new System.EventHandler(this.addSameLevel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 22);
            this.btnAdd.Text = "Thêm &con";
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
            // LuuSep
            // 
            this.LuuSep.Name = "LuuSep";
            this.LuuSep.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLuu
            // 
            this.btnLuu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(29, 22);
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnKhongLuu
            // 
            this.btnKhongLuu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKhongLuu.Name = "btnKhongLuu";
            this.btnKhongLuu.Size = new System.Drawing.Size(59, 22);
            this.btnKhongLuu.Text = "&Không lưu";
            this.btnKhongLuu.Click += new System.EventHandler(this.btnKhongLuu_Click);
            // 
            // DongSep
            // 
            this.DongSep.Name = "DongSep";
            this.DongSep.Size = new System.Drawing.Size(6, 25);
            // 
            // btnClose
            // 
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(37, 22);
            this.btnClose.Text = "Đón&g";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // TreeList_1
            // 
            this.TreeList_1.AllowDrop = true;
            this.TreeList_1.Appearance.Empty.BackColor = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.Empty.BackColor2 = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.Empty.ForeColor = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.EvenRow.BackColor = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.EvenRow.BackColor2 = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.FocusedCell.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.TreeList_1.Appearance.FocusedCell.BorderColor = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Blue;
            this.TreeList_1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.TreeList_1.Appearance.FocusedRow.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this.TreeList_1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.White;
            this.TreeList_1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.TreeList_1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.TreeList_1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.TreeList_1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TreeList_1.Appearance.Row.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.Appearance.Row.ForeColor = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.SelectedRow.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
            this.TreeList_1.Appearance.SelectedRow.BorderColor = System.Drawing.Color.White;
            this.TreeList_1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TreeList_1.ColumnsImageList = this.imageList1;
            this.TreeList_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeList_1.HorzScrollStep = 1;
            this.TreeList_1.Location = new System.Drawing.Point(0, 25);
            this.TreeList_1.Name = "TreeList_1";
            this.TreeList_1.OptionsBehavior.AutoFocusNewNode = true;
            this.TreeList_1.OptionsBehavior.DragNodes = true;
            this.TreeList_1.OptionsBehavior.Editable = false;
            this.TreeList_1.OptionsBehavior.EnterMovesNextColumn = true;
            this.TreeList_1.OptionsBehavior.SmartMouseHover = false;
            this.TreeList_1.OptionsBehavior.UseTabKey = true;
            //this.TreeList_1.OptionsMenu.EnableColumnMenu = false;
            //this.TreeList_1.OptionsMenu.EnableFooterMenu = false;
            this.TreeList_1.OptionsView.EnableAppearanceEvenRow = true;
            this.TreeList_1.OptionsView.EnableAppearanceOddRow = true;
            this.TreeList_1.SelectImageList = this.imageList1;
            this.TreeList_1.Size = new System.Drawing.Size(508, 276);
            this.TreeList_1.TabIndex = 33;
            this.TreeList_1.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.TreeList_1_AfterDragNode);
            this.TreeList_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeList_1_KeyDown);
            this.TreeList_1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.TreeList_1_FocusedNodeChanged);
            this.TreeList_1.ValidateNode += new DevExpress.XtraTreeList.ValidateNodeEventHandler(this.TreeList_1_ValidateNode_1);
            this.TreeList_1.DoubleClick += new System.EventHandler(this.TreeList_1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // DMTreeGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TreeList_1);
            this.Controls.Add(this.toolStrip_1);
            this.Name = "DMTreeGroup";
            this.Size = new System.Drawing.Size(508, 301);
            this.Load += new System.EventHandler(this.DMTreeGroup_Load);
            this.toolStrip_1.ResumeLayout(false);
            this.toolStrip_1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeList_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.ToolStrip toolStrip_1;
        public System.Windows.Forms.ToolStripButton btnAdd;
        public System.Windows.Forms.ToolStripButton btnUpdate;
        public System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.ToolStripSeparator ChonSep;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        public PLDataTree TreeList_1;
        private System.Windows.Forms.ToolStripButton btnNoSelect;
        private System.Windows.Forms.ToolStripSeparator LuuSep;
        public System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.ToolStripButton addSameLevel;
        #endregion
        public System.Windows.Forms.ToolStripButton btnLuu;
        public System.Windows.Forms.ToolStripButton btnKhongLuu;
        private System.Windows.Forms.ToolStripSeparator DongSep;


    }
}
