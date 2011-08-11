namespace ProtocolVN.Framework.Win
{
    partial class PLChonNhanVien
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
            this.GridControl_1 = new DevExpress.XtraGrid.GridControl();
            this.gridView_1 = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.toolStrip_1 = new System.Windows.Forms.ToolStrip();
            this.ChonSep = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnNoSelect = new System.Windows.Forms.ToolStripButton();
            this.DongSep = new System.Windows.Forms.ToolStripSeparator();
            this.Close = new System.Windows.Forms.ToolStripButton();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.TreeList_1 = new ProtocolVN.Framework.Win.PLDataTree();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.repositoryItemTextEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_1)).BeginInit();
            this.toolStrip_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeList_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).BeginInit();
            this.SuspendLayout();
            // 
            // GridControl_1
            // 
            this.GridControl_1.AllowDrop = true;
            this.GridControl_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl_1.EmbeddedNavigator.Enabled = false;
            this.GridControl_1.EmbeddedNavigator.Name = "";
            this.GridControl_1.FormsUseDefaultLookAndFeel = false;
            this.GridControl_1.Location = new System.Drawing.Point(160, 25);
            this.GridControl_1.MainView = this.gridView_1;
            this.GridControl_1.Name = "GridControl_1";
            this.GridControl_1.Size = new System.Drawing.Size(578, 296);
            this.GridControl_1.TabIndex = 34;
            this.GridControl_1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_1});
            // 
            // gridView_1
            // 
            this.gridView_1.BestFitMaxRowCount = 50;
            this.gridView_1.FixedLineWidth = 1;
            this.gridView_1.GridControl = this.GridControl_1;
            this.gridView_1.GroupPanelText = "Danh sách nhân viên";
            this.gridView_1.HorzScrollStep = 1;
            this.gridView_1.Name = "gridView_1";
            this.gridView_1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView_1.OptionsCustomization.AllowGroup = false;
            this.gridView_1.OptionsCustomization.AllowRowSizing = true;
            this.gridView_1.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView_1.OptionsView.AutoCalcPreviewLineCount = true;
            this.gridView_1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView_1.DoubleClick += new System.EventHandler(this.gridView_1_DoubleClick);
            // 
            // toolStrip_1
            // 
            this.toolStrip_1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChonSep,
            this.btnSelect,
            this.btnNoSelect,
            this.DongSep,
            this.Close});
            this.toolStrip_1.Location = new System.Drawing.Point(160, 0);
            this.toolStrip_1.Name = "toolStrip_1";
            this.toolStrip_1.Size = new System.Drawing.Size(578, 25);
            this.toolStrip_1.TabIndex = 33;
            this.toolStrip_1.Text = "toolStrip1";
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
            // DongSep
            // 
            this.DongSep.Name = "DongSep";
            this.DongSep.Size = new System.Drawing.Size(6, 25);
            // 
            // Close
            // 
            this.Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(37, 22);
            this.Close.Text = "Đón&g";
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("d544f49b-23b6-4b21-af45-80b23d315b74");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.Size = new System.Drawing.Size(160, 321);
            this.dockPanel1.Text = "Phòng ban";
            
            this.dockPanel1.Options.AllowDockLeft = false;
            this.dockPanel1.Options.AllowDockRight = false;
            this.dockPanel1.Options.AllowDockTop = false;
            this.dockPanel1.Options.AllowDockBottom = false;
            this.dockPanel1.Options.AllowDockFill = false;
            this.dockPanel1.Options.AllowFloating = false;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.TreeList_1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(154, 293);
            this.dockPanel1_Container.TabIndex = 0;
            this.dockPanel1_Container.Dock = System.Windows.Forms.DockStyle.Left;
            
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
            this.TreeList_1.Appearance.Row.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.Appearance.Row.ForeColor = System.Drawing.Color.Azure;
            this.TreeList_1.Appearance.SelectedRow.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
            this.TreeList_1.Appearance.SelectedRow.BorderColor = System.Drawing.Color.White;
            this.TreeList_1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.TreeList_1.ColumnsImageList = this.imageList1;
            this.TreeList_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeList_1.HorzScrollStep = 1;
            this.TreeList_1.Location = new System.Drawing.Point(0, 0);
            this.TreeList_1.Name = "TreeList_1";
            this.TreeList_1.BeginUnboundLoad();
            this.TreeList_1.AppendNode(new object[0], -1, 0, 1, -1);
            this.TreeList_1.AppendNode(new object[0], 0, 0, 1, -1);
            this.TreeList_1.EndUnboundLoad();
            this.TreeList_1.OptionsBehavior.AutoChangeParent = false;
            this.TreeList_1.OptionsBehavior.AutoNodeHeight = false;
            this.TreeList_1.OptionsBehavior.CanCloneNodesOnDrop = true;
            this.TreeList_1.OptionsBehavior.CloseEditorOnLostFocus = false;
            this.TreeList_1.OptionsBehavior.Editable = false;
            this.TreeList_1.OptionsBehavior.KeepSelectedOnClick = false;
            this.TreeList_1.OptionsBehavior.ShowEditorOnMouseUp = true;
            this.TreeList_1.OptionsBehavior.SmartMouseHover = false;
            this.TreeList_1.OptionsMenu.EnableColumnMenu = false;
            this.TreeList_1.OptionsMenu.EnableFooterMenu = false;
            this.TreeList_1.OptionsView.EnableAppearanceEvenRow = true;
            this.TreeList_1.OptionsView.EnableAppearanceOddRow = true;
            this.TreeList_1.OptionsView.ShowColumns = false;
            this.TreeList_1.OptionsView.ShowHorzLines = false;
            this.TreeList_1.OptionsView.ShowIndicator = false;
            this.TreeList_1.OptionsView.ShowVertLines = false;
            this.TreeList_1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit5,
            this.repositoryItemTextEdit7});
            this.TreeList_1.SelectImageList = this.imageList1;
            this.TreeList_1.Size = new System.Drawing.Size(154, 293);
            this.TreeList_1.TabIndex = 33;
            this.TreeList_1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.TreeList_1_FocusedNodeChanged);
            // 
            // repositoryItemTextEdit5
            // 
            this.repositoryItemTextEdit5.Mask.EditMask = "P";
            this.repositoryItemTextEdit5.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit5.Name = "repositoryItemTextEdit5";
            // 
            // repositoryItemTextEdit7
            // 
            this.repositoryItemTextEdit7.AutoHeight = false;
            this.repositoryItemTextEdit7.Name = "repositoryItemTextEdit7";
            // 
            // PLChonNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl_1);
            this.Controls.Add(this.toolStrip_1);
            this.Controls.Add(this.dockPanel1);
            this.Name = "PLChonNhanVien";
            this.Size = new System.Drawing.Size(738, 321);
            this.Load += new System.EventHandler(this.PLChonNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_1)).EndInit();
            this.toolStrip_1.ResumeLayout(false);
            this.toolStrip_1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TreeList_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl GridControl_1;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridView_1;
        private System.Windows.Forms.ToolStrip toolStrip_1;
        private System.Windows.Forms.ToolStripSeparator ChonSep;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        public PLDataTree TreeList_1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit7;
        private System.Windows.Forms.ToolStripButton btnNoSelect;
        public System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton Close;
        private System.Windows.Forms.ToolStripSeparator DongSep;
    }
}
