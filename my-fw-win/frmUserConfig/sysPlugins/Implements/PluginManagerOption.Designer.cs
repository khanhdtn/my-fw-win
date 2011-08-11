namespace ProtocolVN.Framework.Win
{
    partial class PluginManagerOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManagerOption));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barNapCauKien = new DevExpress.XtraBars.BarButtonItem();
            this.GoBo = new DevExpress.XtraBars.BarButtonItem();
            this.barChon = new DevExpress.XtraBars.BarButtonItem();
            this.barGoBo = new DevExpress.XtraBars.BarButtonItem();
            this.barPluginNotInstall = new DevExpress.XtraBars.BarButtonItem();
            this.barDSPluginInstall = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(596, 492);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Danh sách các bổ trợ";
            this.gridView1.Images = this.imageCollection1;
            this.gridView1.Name = "gridView1";
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // gridColumn3
            // 
            this.gridColumn3.FieldName = "CHOICE";
            this.gridColumn3.ImageAlignment = System.Drawing.StringAlignment.Center;
            this.gridColumn3.ImageIndex = 5;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 35;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên bổ trợ";
            this.gridColumn1.FieldName = "NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 257;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mô tả";
            this.gridColumn2.FieldName = "DESCRIPTION";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 346;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barChon,
            this.barGoBo,
            this.barPluginNotInstall,
            this.barDSPluginInstall,
            this.barNapCauKien,
            this.GoBo});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 7;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barNapCauKien),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.GoBo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChon, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barGoBo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPluginNotInstall, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDSPluginInstall)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barNapCauKien
            // 
            this.barNapCauKien.Caption = "Cài đặt";
            this.barNapCauKien.Id = 5;
            this.barNapCauKien.ImageIndex = 6;
            this.barNapCauKien.Name = "barNapCauKien";
            this.barNapCauKien.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barNapCauKien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNapCauKien_ItemClick);
            // 
            // GoBo
            // 
            this.GoBo.Caption = "Gỡ bỏ";
            this.GoBo.Id = 6;
            this.GoBo.ImageIndex = 4;
            this.GoBo.Name = "GoBo";
            this.GoBo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.GoBo_ItemClick);
            // 
            // barChon
            // 
            this.barChon.Caption = "Kích hoạt";
            this.barChon.Id = 0;
            this.barChon.ImageIndex = 1;
            this.barChon.Name = "barChon";
            this.barChon.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barChon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barChon_ItemClick);
            // 
            // barGoBo
            // 
            this.barGoBo.Caption = "Tạm ngừng";
            this.barGoBo.Id = 1;
            this.barGoBo.ImageIndex = 4;
            this.barGoBo.Name = "barGoBo";
            this.barGoBo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barGoBo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barGoBo_ItemClick);
            // 
            // barPluginNotInstall
            // 
            this.barPluginNotInstall.Caption = "Bổ trợ";
            this.barPluginNotInstall.Id = 3;
            this.barPluginNotInstall.ImageIndex = 3;
            this.barPluginNotInstall.Name = "barPluginNotInstall";
            this.barPluginNotInstall.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPluginNotInstall.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPluginNotInstall_ItemClick);
            // 
            // barDSPluginInstall
            // 
            this.barDSPluginInstall.Caption = "Bổ trợ đang dùng";
            this.barDSPluginInstall.Id = 4;
            this.barDSPluginInstall.ImageIndex = 2;
            this.barDSPluginInstall.Name = "barDSPluginInstall";
            this.barDSPluginInstall.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDSPluginInstall.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDSPluginInstall_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(596, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 518);
            this.barDockControlBottom.Size = new System.Drawing.Size(596, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 492);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(596, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 492);
            // 
            // PluginManagerOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PluginManagerOption";
            this.Size = new System.Drawing.Size(596, 518);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barChon;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barGoBo;
        private DevExpress.XtraBars.BarButtonItem barPluginNotInstall;
        private DevExpress.XtraBars.BarButtonItem barDSPluginInstall;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem barNapCauKien;
        private DevExpress.XtraBars.BarButtonItem GoBo;
    }
}
