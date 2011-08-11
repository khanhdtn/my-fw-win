using ProtocolVN.Plugin.ImpExp;
namespace ProtocolVN.Plugin.OldImpExp
{
    partial class frmOldImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOldImport));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridDataSource = new DevExpress.XtraGrid.GridControl();
            this.gridviewDataSource = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnNap = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteAll = new System.Windows.Forms.ToolStripButton();
            this.cbSelSheet = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridMap = new DevExpress.XtraGrid.GridControl();
            this.gridViewMap = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.gridDataTarget = new DevExpress.XtraGrid.GridControl();
            this.gridviewDataTarget = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuMapping = new System.Windows.Forms.ToolStripButton();
            this.menuDelete = new System.Windows.Forms.ToolStripButton();
            this.menuDeleteAll = new System.Windows.Forms.ToolStripButton();
            this.menuInsertData = new System.Windows.Forms.ToolStripButton();
            this.btnInsertAllData = new System.Windows.Forms.ToolStripButton();
            this.comboListTable = new System.Windows.Forms.ToolStripComboBox();
            this.btnSaveData = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDataSource)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDataTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDataTarget)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridDataSource);
            this.splitContainerControl1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(699, 456);
            this.splitContainerControl1.SplitterPosition = 224;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridDataSource
            // 
            this.gridDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDataSource.Location = new System.Drawing.Point(0, 38);
            this.gridDataSource.MainView = this.gridviewDataSource;
            this.gridDataSource.Name = "gridDataSource";
            this.gridDataSource.Size = new System.Drawing.Size(695, 182);
            this.gridDataSource.TabIndex = 1;
            this.gridDataSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridviewDataSource});
            // 
            // gridviewDataSource
            // 
            this.gridviewDataSource.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridviewDataSource.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridviewDataSource.GridControl = this.gridDataSource;
            this.gridviewDataSource.IndicatorWidth = 40;
            this.gridviewDataSource.Name = "gridviewDataSource";
            this.gridviewDataSource.OptionsCustomization.AllowGroup = false;
            this.gridviewDataSource.OptionsLayout.Columns.AddNewColumns = false;
            this.gridviewDataSource.OptionsNavigation.AutoFocusNewRow = true;
            this.gridviewDataSource.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridviewDataSource.OptionsSelection.MultiSelect = true;
            this.gridviewDataSource.OptionsView.EnableAppearanceEvenRow = true;
            this.gridviewDataSource.OptionsView.EnableAppearanceOddRow = true;
            this.gridviewDataSource.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridviewDataSource.OptionsView.ShowGroupedColumns = true;
            this.gridviewDataSource.OptionsView.ShowGroupPanel = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNap,
            this.btnDelete,
            this.btnDeleteAll,
            this.cbSelSheet});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(695, 38);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnNap
            // 
            this.btnNap.Image = ((System.Drawing.Image)(resources.GetObject("btnNap.Image")));
            this.btnNap.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNap.Name = "btnNap";
            this.btnNap.Size = new System.Drawing.Size(72, 35);
            this.btnNap.Text = "Nạp dữ liệu";
            this.btnNap.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNap.ToolTipText = "Import Data";
            this.btnNap.Click += new System.EventHandler(this.btnNap_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(31, 35);
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.ToolTipText = "Xóa dữ liệu";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.Image")));
            this.btnDeleteAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(63, 35);
            this.btnDeleteAll.Text = "Xóa tất cả";
            this.btnDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteAll.ToolTipText = "Xóa hết";
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // cbSelSheet
            // 
            this.cbSelSheet.Name = "cbSelSheet";
            this.cbSelSheet.Size = new System.Drawing.Size(121, 38);
            this.cbSelSheet.Text = "Chọn sheet";
            this.cbSelSheet.Visible = false;
            this.cbSelSheet.SelectedIndexChanged += new System.EventHandler(this.cbSelSheet_SelectedIndexChanged);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 38);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.splitContainerControl2.Panel1.Controls.Add(this.gridMap);
            this.splitContainerControl2.Panel1.Text = "splitContainerControl2_Panel1";
            this.splitContainerControl2.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.splitContainerControl2.Panel2.Controls.Add(this.gridDataTarget);
            this.splitContainerControl2.Panel2.Text = "splitContainerControl2_Panel2";
            this.splitContainerControl2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainerControl2.Size = new System.Drawing.Size(695, 184);
            this.splitContainerControl2.SplitterPosition = 218;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gridMap
            // 
            this.gridMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMap.Location = new System.Drawing.Point(0, 0);
            this.gridMap.MainView = this.gridViewMap;
            this.gridMap.Name = "gridMap";
            this.gridMap.Size = new System.Drawing.Size(218, 184);
            this.gridMap.TabIndex = 0;
            this.gridMap.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMap});
            // 
            // gridViewMap
            // 
            this.gridViewMap.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewMap.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewMap.GridControl = this.gridMap;
            this.gridViewMap.IndicatorWidth = 40;
            this.gridViewMap.Name = "gridViewMap";
            this.gridViewMap.OptionsCustomization.AllowFilter = false;
            this.gridViewMap.OptionsCustomization.AllowGroup = false;
            this.gridViewMap.OptionsCustomization.AllowSort = false;
            this.gridViewMap.OptionsLayout.Columns.AddNewColumns = false;
            this.gridViewMap.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewMap.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewMap.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMap.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewMap.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewMap.OptionsView.ShowGroupedColumns = true;
            this.gridViewMap.OptionsView.ShowGroupPanel = false;
            this.gridViewMap.OptionsView.ShowIndicator = false;
            this.gridViewMap.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewMap_CellValueChanging);
            // 
            // gridDataTarget
            // 
            this.gridDataTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDataTarget.Location = new System.Drawing.Point(0, 0);
            this.gridDataTarget.MainView = this.gridviewDataTarget;
            this.gridDataTarget.Name = "gridDataTarget";
            this.gridDataTarget.Size = new System.Drawing.Size(471, 184);
            this.gridDataTarget.TabIndex = 0;
            this.gridDataTarget.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridviewDataTarget});
            // 
            // gridviewDataTarget
            // 
            this.gridviewDataTarget.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridviewDataTarget.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridviewDataTarget.GridControl = this.gridDataTarget;
            this.gridviewDataTarget.IndicatorWidth = 40;
            this.gridviewDataTarget.Name = "gridviewDataTarget";
            this.gridviewDataTarget.OptionsLayout.Columns.AddNewColumns = false;
            this.gridviewDataTarget.OptionsNavigation.AutoFocusNewRow = true;
            this.gridviewDataTarget.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridviewDataTarget.OptionsSelection.MultiSelect = true;
            this.gridviewDataTarget.OptionsView.EnableAppearanceEvenRow = true;
            this.gridviewDataTarget.OptionsView.EnableAppearanceOddRow = true;
            this.gridviewDataTarget.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridviewDataTarget.OptionsView.ShowGroupedColumns = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMapping,
            this.menuDelete,
            this.menuDeleteAll,
            this.menuInsertData,
            this.btnInsertAllData,
            this.comboListTable,
            this.btnSaveData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(695, 38);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menuMapping
            // 
            this.menuMapping.Checked = true;
            this.menuMapping.CheckOnClick = true;
            this.menuMapping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuMapping.Image = ((System.Drawing.Image)(resources.GetObject("menuMapping.Image")));
            this.menuMapping.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menuMapping.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuMapping.Name = "menuMapping";
            this.menuMapping.Size = new System.Drawing.Size(86, 35);
            this.menuMapping.Text = "Ánh xạ dữ liệu";
            this.menuMapping.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menuMapping.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuMapping.Click += new System.EventHandler(this.menuMapping_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Image = ((System.Drawing.Image)(resources.GetObject("menuDelete.Image")));
            this.menuDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menuDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(31, 35);
            this.menuDelete.Text = "Xóa";
            this.menuDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menuDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
            // 
            // menuDeleteAll
            // 
            this.menuDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("menuDeleteAll.Image")));
            this.menuDeleteAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menuDeleteAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuDeleteAll.Name = "menuDeleteAll";
            this.menuDeleteAll.Size = new System.Drawing.Size(51, 35);
            this.menuDeleteAll.Text = "Xóa hết";
            this.menuDeleteAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menuDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuDeleteAll.Click += new System.EventHandler(this.menuDeleteAll_Click);
            // 
            // menuInsertData
            // 
            this.menuInsertData.Image = ((System.Drawing.Image)(resources.GetObject("menuInsertData.Image")));
            this.menuInsertData.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menuInsertData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuInsertData.Name = "menuInsertData";
            this.menuInsertData.Size = new System.Drawing.Size(33, 35);
            this.menuInsertData.Text = "Nạp";
            this.menuInsertData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menuInsertData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuInsertData.Click += new System.EventHandler(this.menuInsertData_Click);
            // 
            // btnInsertAllData
            // 
            this.btnInsertAllData.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertAllData.Image")));
            this.btnInsertAllData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsertAllData.Name = "btnInsertAllData";
            this.btnInsertAllData.Size = new System.Drawing.Size(65, 35);
            this.btnInsertAllData.Text = "Nạp tất cả";
            this.btnInsertAllData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInsertAllData.Click += new System.EventHandler(this.btnInsertAllData_Click);
            // 
            // comboListTable
            // 
            this.comboListTable.Name = "comboListTable";
            this.comboListTable.Size = new System.Drawing.Size(121, 38);
            this.comboListTable.Text = "Chọn dữ liệu đích";
            this.comboListTable.SelectedIndexChanged += new System.EventHandler(this.comboListTable_SelectedIndexChanged);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveData.Image")));
            this.btnSaveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(31, 35);
            this.btnSaveData.Text = "Lưu";
            this.btnSaveData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveData.ToolTipText = "Lưu";
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // frmOldImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 456);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmOldImport";
            this.Text = "Nhập dữ liệu từ tập tin";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDataSource)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDataTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDataTarget)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraGrid.GridControl gridDataSource;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridviewDataSource;
        private System.Windows.Forms.ToolStripButton btnNap;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnDeleteAll;
        private System.Windows.Forms.ToolStripButton menuMapping;
        private System.Windows.Forms.ToolStripButton menuDelete;
        private System.Windows.Forms.ToolStripButton menuDeleteAll;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraGrid.GridControl gridDataTarget;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridviewDataTarget;
        private DevExpress.XtraGrid.GridControl gridMap;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridViewMap;
        private System.Windows.Forms.ToolStripButton menuInsertData;
        private System.Windows.Forms.ToolStripComboBox comboListTable;
        private System.Windows.Forms.ToolStripButton btnSaveData;
        private System.Windows.Forms.ToolStripButton btnInsertAllData;
        private System.Windows.Forms.ToolStripComboBox cbSelSheet;
    }
}

