namespace ProtocolVN.Framework.Win.Demo
{
    partial class frmTPhieuThongKeTemplate
    {
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
        protected void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.MainBar = new DevExpress.XtraBars.Bar();
            this.barButtonItemSearch = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuFilter = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barCheckItemFilter = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItemTuyChonXemDoThi = new DevExpress.XtraBars.BarButtonItem();
            this.barItemIn = new DevExpress.XtraBars.BarButtonItem();
            this.popupIn = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barItemXemTruoc = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExport = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.pivotGridMaster = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.fieldHangHoa = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldThang = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldSoLuongNhap = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldSoLuongXuat = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldNam = new DevExpress.XtraPivotGrid.PivotGridField();
            this.popupControlContainerFilter = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.thoiGian = new ProtocolVN.Framework.Win.Trial.PLDateSelection();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainerFilter)).BeginInit();
            this.popupControlContainerFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.MainBar});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemExport,
            this.barButtonItemSearch,
            this.barButtonItemTuyChonXemDoThi,
            this.barCheckItemFilter,
            this.barButtonItem3,
            this.barItemXemTruoc,
            this.barItemIn});
            this.barManager1.MaxItemId = 36;
            // 
            // MainBar
            // 
            this.MainBar.BarName = "MainBar";
            this.MainBar.DockCol = 0;
            this.MainBar.DockRow = 0;
            this.MainBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.MainBar.FloatLocation = new System.Drawing.Point(39, 133);
            this.MainBar.FloatSize = new System.Drawing.Size(72, 73);
            this.MainBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.barButtonItemSearch, "&Thống kê"),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemTuyChonXemDoThi, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemIn, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemExport, true)});
            this.MainBar.OptionsBar.AllowQuickCustomization = false;
            this.MainBar.OptionsBar.DrawDragBorder = false;
            this.MainBar.OptionsBar.RotateWhenVertical = false;
            this.MainBar.OptionsBar.UseWholeRow = true;
            this.MainBar.Text = "Custom 1";
            // 
            // barButtonItemSearch
            // 
            this.barButtonItemSearch.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItemSearch.Caption = "&Thống kê";
            this.barButtonItemSearch.DropDownControl = this.popupMenuFilter;
            this.barButtonItemSearch.Id = 27;
            this.barButtonItemSearch.Name = "barButtonItemSearch";
            this.barButtonItemSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // popupMenuFilter
            // 
            this.popupMenuFilter.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemFilter)});
            this.popupMenuFilter.Manager = this.barManager1;
            this.popupMenuFilter.Name = "popupMenuFilter";
            // 
            // barCheckItemFilter
            // 
            this.barCheckItemFilter.Caption = "Hiện điều kiện thống kê";
            this.barCheckItemFilter.Checked = true;
            this.barCheckItemFilter.Id = 29;
            this.barCheckItemFilter.Name = "barCheckItemFilter";
            this.barCheckItemFilter.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItemTuyChonXemDoThi
            // 
            this.barButtonItemTuyChonXemDoThi.Caption = "Xem &biểu đồ";
            this.barButtonItemTuyChonXemDoThi.Id = 4;
            this.barButtonItemTuyChonXemDoThi.Name = "barButtonItemTuyChonXemDoThi";
            this.barButtonItemTuyChonXemDoThi.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemIn
            // 
            this.barItemIn.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barItemIn.Caption = "&In";
            this.barItemIn.DropDownControl = this.popupIn;
            this.barItemIn.Id = 35;
            this.barItemIn.Name = "barItemIn";
            this.barItemIn.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // popupIn
            // 
            this.popupIn.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemXemTruoc)});
            this.popupIn.Manager = this.barManager1;
            this.popupIn.Name = "popupIn";
            // 
            // barItemXemTruoc
            // 
            this.barItemXemTruoc.Caption = "Xem t&rước";
            this.barItemXemTruoc.Id = 34;
            this.barItemXemTruoc.Name = "barItemXemTruoc";
            // 
            // barButtonItemExport
            // 
            this.barButtonItemExport.Caption = "Xuất ra &file";
            this.barButtonItemExport.Id = 3;
            this.barButtonItemExport.Name = "barButtonItemExport";
            this.barButtonItemExport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(925, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 497);
            this.barDockControlBottom.Size = new System.Drawing.Size(925, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 473);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(925, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 473);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem3.Caption = "In";
            this.barButtonItem3.Id = 30;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 58);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.pivotGridMaster);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.CaptionLocation = DevExpress.Utils.Locations.Left;
            this.splitContainerControl1.Panel2.ShowCaption = true;
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl1.Size = new System.Drawing.Size(925, 439);
            this.splitContainerControl1.SplitterPosition = 405;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // pivotGridMaster
            // 
            this.pivotGridMaster.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pivotGridMaster.Cursor = System.Windows.Forms.Cursors.Default;
            this.pivotGridMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridMaster.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldHangHoa,
            this.fieldThang,
            this.fieldSoLuongNhap,
            this.fieldSoLuongXuat,
            this.fieldNam});
            this.pivotGridMaster.Location = new System.Drawing.Point(0, 0);
            this.pivotGridMaster.Name = "pivotGridMaster";
            this.pivotGridMaster.OptionsView.ShowColumnTotals = false;
            this.pivotGridMaster.OptionsView.ShowFilterHeaders = false;
            this.pivotGridMaster.OptionsView.ShowRowGrandTotals = false;
            this.pivotGridMaster.OptionsView.ShowRowTotals = false;
            this.pivotGridMaster.Size = new System.Drawing.Size(925, 439);
            this.pivotGridMaster.TabIndex = 2;
            // 
            // fieldHangHoa
            // 
            this.fieldHangHoa.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldHangHoa.AreaIndex = 0;
            this.fieldHangHoa.Name = "fieldHangHoa";
            this.fieldHangHoa.Options.ShowCustomTotals = false;
            this.fieldHangHoa.Options.ShowTotals = false;
            // 
            // fieldThang
            // 
            this.fieldThang.Appearance.Header.Options.UseTextOptions = true;
            this.fieldThang.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldThang.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.fieldThang.Appearance.Value.Options.UseFont = true;
            this.fieldThang.Appearance.Value.Options.UseTextOptions = true;
            this.fieldThang.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldThang.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldThang.AreaIndex = 1;
            this.fieldThang.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
            this.fieldThang.Name = "fieldThang";
            this.fieldThang.Options.ShowGrandTotal = false;
            this.fieldThang.Options.ShowTotals = false;
            this.fieldThang.UnboundFieldName = "fieldThang";
            // 
            // fieldSoLuongNhap
            // 
            this.fieldSoLuongNhap.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldSoLuongNhap.AreaIndex = 0;
            this.fieldSoLuongNhap.Name = "fieldSoLuongNhap";
            this.fieldSoLuongNhap.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.fieldSoLuongNhap.Options.AllowEdit = false;
            this.fieldSoLuongNhap.Options.ShowGrandTotal = false;
            this.fieldSoLuongNhap.Options.ShowTotals = false;
            // 
            // fieldSoLuongXuat
            // 
            this.fieldSoLuongXuat.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldSoLuongXuat.AreaIndex = 1;
            this.fieldSoLuongXuat.Name = "fieldSoLuongXuat";
            this.fieldSoLuongXuat.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.fieldSoLuongXuat.Options.AllowEdit = false;
            this.fieldSoLuongXuat.Options.ShowGrandTotal = false;
            this.fieldSoLuongXuat.Options.ShowTotals = false;
            // 
            // fieldNam
            // 
            this.fieldNam.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldNam.AreaIndex = 0;
            this.fieldNam.Name = "fieldNam";
            this.fieldNam.Options.ShowGrandTotal = false;
            this.fieldNam.Options.ShowTotals = false;
            this.fieldNam.UnboundFieldName = "fieldNam";
            // 
            // popupControlContainerFilter
            // 
            this.popupControlContainerFilter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.popupControlContainerFilter.Controls.Add(this.thoiGian);
            this.popupControlContainerFilter.Controls.Add(this.labelControl4);
            this.popupControlContainerFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.popupControlContainerFilter.Location = new System.Drawing.Point(0, 24);
            this.popupControlContainerFilter.Manager = this.barManager1;
            this.popupControlContainerFilter.Name = "popupControlContainerFilter";
            this.popupControlContainerFilter.Size = new System.Drawing.Size(925, 34);
            this.popupControlContainerFilter.TabIndex = 0;
            this.popupControlContainerFilter.Visible = false;
            // 
            // thoiGian
            // 
            this.thoiGian.Caption = "Sáu tháng cuối năm 2010";
            this.thoiGian.Default = ProtocolVN.Framework.Win.Trial.SelectionTypes.None;
            this.thoiGian.FirstFrom = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.thoiGian.FirstTo = new System.DateTime(2010, 12, 31, 0, 0, 0, 0);
            this.thoiGian.FromDate = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.thoiGian.Location = new System.Drawing.Point(61, 6);
            this.thoiGian.Name = "thoiGian";
            this.thoiGian.ReturnType = ProtocolVN.Framework.Win.Trial.TimeType.Date;
            this.thoiGian.SecondFrom = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.thoiGian.SecondTo = new System.DateTime(2010, 12, 31, 0, 0, 0, 0);
            this.thoiGian.Size = new System.Drawing.Size(369, 20);
            this.thoiGian.TabIndex = 16;
            this.thoiGian.ToDate = new System.DateTime(2010, 12, 31, 0, 0, 0, 0);
            this.thoiGian.Types = ((ProtocolVN.Framework.Win.Trial.SelectionTypes)(((ProtocolVN.Framework.Win.Trial.SelectionTypes.SixMonths | ProtocolVN.Framework.Win.Trial.SelectionTypes.FromMonthToMonth)
                        | ProtocolVN.Framework.Win.Trial.SelectionTypes.FromQuarterToQuarter)));
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 7);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(43, 13);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "Thời gian";
            // 
            // frmTPhieuThongKeTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 497);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.popupControlContainerFilter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmTPhieuThongKeTemplate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainerFilter)).EndInit();
            this.popupControlContainerFilter.ResumeLayout(false);
            this.popupControlContainerFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridField fieldHangHoa;
        private DevExpress.XtraPivotGrid.PivotGridField fieldThang;
        private DevExpress.XtraPivotGrid.PivotGridField fieldSoLuongNhap;
        private DevExpress.XtraPivotGrid.PivotGridField fieldSoLuongXuat;
        private DevExpress.XtraPivotGrid.PivotGridField fieldNam;
        private ProtocolVN.Framework.Win.Trial.PLDateSelection thoiGian;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.ComponentModel.IContainer components;
        
    }
}