using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.DXperience.Demos;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{    
    public abstract class frmTBieuDoChange : XtraFormPL
    {
        #region UI Fields.
        public IContainer components;
        public BarButtonItem barButtonItem3;
        public BarSubItem barButtonItemExport;
        public BarButtonItem barButtonItemSearch;
        public BarButtonItem barButtonItemTuyChonXemDoThi;
        public BarCheckItem barCheckItemFilter;
        public BarDockControl barDockControlBottom;
        public BarDockControl barDockControlLeft;
        public BarDockControl barDockControlRight;
        public BarDockControl barDockControlTop;
        public BarManager barManager1;
        public PivotGridControl pivotGridMaster;                
        public Bar MainBar;
        public PopupControlContainer popupControlContainerFilter;        
        public PopupMenu popupMenuFilter;
        public SplitContainerControl splitContainerControl1;
        public DevExpress.XtraBars.BarButtonItem barItemIn;
        public DevExpress.XtraBars.PopupMenu popupIn;
        public DevExpress.XtraBars.BarButtonItem barItemXemTruoc;
        public DevExpress.XtraEditors.ComboBoxEdit comboChartType;
        public DevExpress.XtraEditors.CheckEdit ceChartDataRow;
        public DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.CheckEdit ceChartDataVertical;
        public DevExpress.XtraEditors.CheckEdit checkShowPointLabels;
        public DevExpress.XtraCharts.ChartControl chartControlMaster;
        #endregion

        public QueryBuilder filter = null;
        protected frmTBieuDoChange() { }
        
        public abstract void InitFieldMaster();
        public abstract QueryBuilder PLBuildQueryFilter();
        public abstract void PLLoadFilterPart();
        public virtual bool CheckFilter()
        {
            return true;
        }
    }

    public class TBieuDoFix : DevExpress.DXperience.Demos.LookAndFeelMenu
    {        
        // Fields
        private bool filter = false;
        private frmTBieuDoChange that;
        private String _tenBaoCao;
        private bool _clickXem = false;

        #region INIT FORM
        public TBieuDoFix(frmTBieuDoChange phieuTK, bool hasFilterPart)
            : base(null, null, "")
        {
            this.that = phieuTK;
            this.filter = hasFilterPart;
            this.HamDung();
            this._tenBaoCao = this.that.Text.ToUpper();
        }
        void HamDung()
        {
            this.that.MainBar.Text = "Chức năng";
            this.that.MainBar.Manager.AllowCustomization = false;
            this.that.MainBar.OptionsBar.AllowDelete = false;
            this.that.MainBar.OptionsBar.DisableClose = true;
            this.that.MainBar.OptionsBar.DisableCustomization = true;
            this.that.barButtonItemExport.Glyph = FWImageDic.EXPORT_TO_FILE_IMAGE20;
            this.that.barButtonItemExport.Enabled = false;
            
            this.that.barButtonItemSearch.Glyph = FWImageDic.FIND_IMAGE20;
            this.that.barButtonItemSearch.Enabled = false;
            
            this.that.barCheckItemFilter.Glyph = FWImageDic.FILTER_IMAGE20;
            
            this.that.barButtonItemTuyChonXemDoThi.Glyph = FWImageDic.CHART_IMAGE20;
            this.that.barButtonItemTuyChonXemDoThi.Enabled = true;
            this.that.barItemIn.Glyph = FWImageDic.PRINT_IMAGE20;
            this.that.barItemIn.Enabled = false;
            try
            {
                this.that.InitFieldMaster();
            }
            catch (Exception exception)
            {
                PLException.AddException(exception);
                HelpDevError.ShowMessage(this, exception.Message);
            }
            this._initMenuExport();

            this.that.barButtonItemSearch.ItemClick += new ItemClickEventHandler(
                this.barButtonItemSearch_ItemClick);
            this.that.barCheckItemFilter.CheckedChanged += new ItemClickEventHandler(
                this.barCheckItemFilter_CheckedChanged);
            this.that.barButtonItemTuyChonXemDoThi.ItemClick += new ItemClickEventHandler(
                this.barButtonItemTuyChonXemDoThi_ItemClick);
            this.that.pivotGridMaster.FocusedCellChanged += new EventHandler(
                this.pivotGridMaster_FocusedCellChanged);
            this.that.Load += new EventHandler(this.PhieuTK_Load);
            this.that.barItemIn.ItemClick += new ItemClickEventHandler(barItemIn_ItemClick);
            this.that.barItemXemTruoc.ItemClick += new ItemClickEventHandler(barItemXemTruoc_ItemClick);

            HelpPivotGrid.VietHoaMenuGridPivot(this.that.pivotGridMaster);

            #region Phát sinh của biểu đồ
            this._initLoaiBieuDo();
            this.that.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            this.that.checkShowPointLabels.CheckedChanged += new System.EventHandler(this.checkShowPointLabels_CheckedChanged);
            this.that.comboChartType.SelectedIndexChanged += new System.EventHandler(this.comboChartType_SelectedIndexChanged);
            this.that.ceChartDataVertical.CheckedChanged += new System.EventHandler(this.ceChartDataVertical_CheckedChanged);
            #endregion

            that.chartControlMaster.BorderOptions.Visible = false;
        }
        protected void PhieuTK_Load(object sender, EventArgs e)
        {
            try
            {
                this.PLCustomView();
                this.that.PLLoadFilterPart();
            }
            catch (Exception exception)
            {
                PLException.AddException(exception);
                if (FrameworkParams.wait != null)
                {
                    FrameworkParams.wait.Finish();
                }
            }
        }
        void PLCustomView()
        {
            if (this.filter)
            {
                this.that.barCheckItemFilter.Checked = true;
                this.that.popupControlContainerFilter.Show();
            }
            else
                this.that.barButtonItemSearch.Enabled = false;

            this.that.pivotGridMaster.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.that.pivotGridMaster.OptionsCustomization.AllowEdit = false;
        }
        #endregion


        private string GetTenBaoCao()
        {
            return (_tenBaoCao != null ? _tenBaoCao : ((XtraFormPL)this.that).Text).ToUpper();
        }
        #region Xem biểu đồ.
        private void _initLoaiBieuDo()
        {
            ViewType[] restrictedTypes = new ViewType[] { 
                ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt,
				ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt,
                ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick,
				ViewType.Bubble
			};

            foreach (ViewType type in Enum.GetValues(typeof(ViewType)))
            {
                if (Array.IndexOf<ViewType>(restrictedTypes, type) >= 0) continue;
                this.that.comboChartType.Properties.Items.Add(type);
            }

            //comboChartType.SelectedItem = ViewType.Line;
            this.that.comboChartType.SelectedItem = ViewType.Bar3D;
        }
        private void _initChart()
        {
            this.that.checkShowPointLabels.Checked = true;           
            this.that.chartControlMaster.SeriesTemplate.Label.Visible = true;

            if (this.that.pivotGridMaster.Cells.Selection.X == 0 &&
                this.that.pivotGridMaster.Cells.Selection.Y == 0 &&
                this.that.pivotGridMaster.Cells.Selection.Width == 0 &&
                this.that.pivotGridMaster.Cells.Selection.Height == 0)
            {
                this.that.pivotGridMaster.OptionsChartDataSource.SelectionOnly = false;
            }
            else
            {
                this.that.pivotGridMaster.OptionsChartDataSource.SelectionOnly = true;
            }

            this.that.ceChartDataVertical.Checked = true;
            this.that.pivotGridMaster.OptionsChartDataSource.ChartDataVertical = this.that.ceChartDataVertical.Checked;


            if (this.that.chartControlMaster.Titles.Count == 0)
            {
                ChartTitle title = new ChartTitle();
                title.Text = this.GetTenBaoCao();
                this.that.chartControlMaster.Titles.Add(title);
            }
            this.that.chartControlMaster.SeriesDataMember = "Series";
            this.that.chartControlMaster.SeriesTemplate.ArgumentDataMember = "Arguments";
            this.that.chartControlMaster.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Values" });

            this.that.chartControlMaster.SeriesTemplate.LegendPointOptions.PointView = PointView.ArgumentAndValues;

            this.that.chartControlMaster.DataSource = this.that.pivotGridMaster;
            this.that.chartControlMaster.SeriesTemplate.ChangeView((ViewType)this.that.comboChartType.SelectedItem);

            PopupChartOption._setScrollAndZoom(this.that.chartControlMaster);
        }


        void barButtonItemTuyChonXemDoThi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.that.CheckFilter())
            {
                HelpWaiting.showMsgForm(this.that, DoVeBieuDo);
            }
        }
        #endregion
        void DoVeBieuDo()
        {
            this.DoSearch();
            this._initChart();
            this.that.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
        }
        #region Xem thống kê PIVOT
        void barButtonItemSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.that.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
        }
        void DoSearch()
        {
            try
            {
                this.that.filter = this.that.PLBuildQueryFilter();

                if (this.that.filter != null)
                {
                    if (this.that.filter.isEmpty() && this.filter)
                    {
                        ((DataTable)this.that.pivotGridMaster.DataSource).Clear();
                    }
                    else
                    {
                        this.PLLoadMasterPart(this.that.pivotGridMaster);
                    }

                    this.pivotGridMaster_FocusedCellChanged(null, null);
                    this._clickXem = true;
                }                
            }
            catch (Exception exception)
            {
                PLException.AddException(exception);
                this._clickXem = false;
            }            
        }

        void PLLoadMasterPart(PivotGridControl MasterGrid)
        {
            try
            {
                DataSet set = DABase.getDatabase().LoadReadOnlyDataSet(this.that.filter);
                this.that.barButtonItemExport.Enabled =
                    ((set != null && set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0) ? true : false);
                this.that.barItemIn.Enabled = this.that.barButtonItemExport.Enabled;
                this.that.barButtonItemSearch.Enabled = this.that.barButtonItemExport.Enabled;
                MasterGrid.DataSource = set.Tables[0];
            }
            catch (Exception exception)
            {
                PLException.AddException(exception);
            }
        }
        #endregion

        #region Ẩn hiện điều kiện lọc
        void barCheckItemFilter_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (this.that.barCheckItemFilter.Checked)
            {
                this.that.popupControlContainerFilter.Show();
            }
            else
            {
                this.that.popupControlContainerFilter.Hide();
            }
        }
        #endregion      
        
        #region Phần IN
        void  barItemXemTruoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (FrameworkParams.headerLetter != null)
            {
                //if (this.that.splitContainerControl1.PanelVisibility== SplitPanelVisibility.Panel2)
                {
                    PrintableComponentLink link = FrameworkParams.headerLetter.Draw(that.chartControlMaster, null, null);
                        //"Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));
                    
                    that.chartControlMaster.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;
                    link.PrintingSystem.PageSettings.Landscape = true;
                    link.PrintingSystem.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;

                    link.ShowPreviewDialog();
                }
                //else if (this.that.splitContainerControl1.PanelVisibility ==  SplitPanelVisibility.Panel1)
                //{
                //    PrintableComponentLink link = FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this.GetTenBaoCao(),
                //        "Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));
                //    that.chartControlMaster.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;
                //    link.PrintingSystem.PageSettings.Landscape = true;
                //    link.ShowPreviewDialog();
                //}
            }
            else
            {
                //if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel2)
                {
                    if (this.that.chartControlMaster == null) 
                        return;
                    else
                        this.that.chartControlMaster.ShowPrintPreview();
                }
                //else if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel1)
                //{
                //    if (this.that.pivotGridMaster == null) return;
                //    this.that.pivotGridMaster.ShowPrintPreview();
                //}
            }
            
        }
        void  barItemIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (FrameworkParams.headerLetter != null)
            {               
                //if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel2)
                {
                    //PrintableComponentLink link = FrameworkParams.headerLetter.Draw(that.chartControlMaster, this.GetTenBaoCao(),
                    //    "Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));
                    PrintableComponentLink link = FrameworkParams.headerLetter.Draw(that.chartControlMaster, null, null);
                    that.chartControlMaster.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;
                    link.PrintingSystem.PageSettings.Landscape = true;
                    link.PrintingSystem.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    link.Print(FrameworkParams.option.printerName);
                }
                //else if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel1)
                //{
                //    FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this.GetTenBaoCao(),
                //        "Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat)).PrintDlg();                   
                //}
            }
            else
            {
                //if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel2)
                {
                    if (this.that.chartControlMaster == null) 
                        return;
                    else
                        this.that.chartControlMaster.Print();
                }
                //else if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel1)
                //{
                //    if (this.that.pivotGridMaster == null) return;
                //    this.that.pivotGridMaster.Print();
                //}
            }            
        }
        #region "Hỗ trợ In và Xuất ra tập tin"
        void _initMenuExport()
        {
            this.that.barButtonItemExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                new DevExpress.XtraBars.LinkPersistInfo(
                    new ButtonBarItem(this.that.barManager1, "Xuất ra file Excel 2007", new ItemClickEventHandler(ExportXLSX_Click))),
                new DevExpress.XtraBars.LinkPersistInfo(
                    new ButtonBarItem(this.that.barManager1, "Xuất ra file Excel 97 - 2003", new ItemClickEventHandler(ExportXLS_Click))),
                new DevExpress.XtraBars.LinkPersistInfo(
                    new ButtonBarItem(this.that.barManager1, "Xuất ra file PDF", new ItemClickEventHandler(ExportPDF_Click))), 
                new DevExpress.XtraBars.LinkPersistInfo(
                    new ButtonBarItem(this.that.barManager1, "Xuất ra file HTML", new ItemClickEventHandler(ExportHTML_Click))),
                //new DevExpress.XtraBars.LinkPersistInfo(
                //    new ButtonBarItem(this.that.barManager1, "Xuất ra MHT", new ItemClickEventHandler(ExportMHT_Click))),
                //new DevExpress.XtraBars.LinkPersistInfo(
                //    new ButtonBarItem(this.that.barManager1, "Xuất ra Text", new ItemClickEventHandler(ExportTXT_Click))),
                new DevExpress.XtraBars.LinkPersistInfo(
                    new ButtonBarItem(this.that.barManager1, "Xuất ra file Word", new ItemClickEventHandler(ExportRTF_Click)))                                               
            });
        }
        void ExportTo(string title, string filter, string exportFormat)
        {
            if (this.that.pivotGridMaster == null) return;
            string fileName = ShowSaveFileDialog(title, filter);
            if (fileName != "")
            {
                //Export
                List<object> input = new List<object>();
                input.Add(exportFormat);
                input.Add(fileName);
                HelpWaiting.showProcessFile(this.that, export, input);
                //OpenFile(fileName);
                if (PLMessageBox.ShowConfirmMessage("Bạn có muốn mở file này không?") == DialogResult.Yes)
                {
                    HelpFile.OpenFile(fileName);
                }
            }
        }
        private void export(List<object> input)
        {
            string exportFormat = input[0].ToString();
            string fileName = input[1].ToString();
            PrintableComponentLink link = null;
            if (FrameworkParams.headerLetter != null)
            {                
                //if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel2)
                {
                   //link = FrameworkParams.headerLetter.Draw(that.chartControlMaster, this.GetTenBaoCao(),
                   //     "Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));
                   link = FrameworkParams.headerLetter.Draw(that.chartControlMaster, null, null);                   
                }
                //else if (this.that.splitContainerControl1.PanelVisibility == SplitPanelVisibility.Panel1)
                //{
                //   link = FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this.GetTenBaoCao(),
                //        "Ngày xuất báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));                    
                //}
            }
            switch (exportFormat)
            {
                case "HTML":
                    if (link != null)
                        link.PrintingSystem.ExportToHtml(fileName);
                    else
                        this.that.pivotGridMaster.ExportToHtml(fileName);
                    break;
                case "MHT":
                    if (link != null)
                        link.PrintingSystem.ExportToMht(fileName);
                    else
                        this.that.pivotGridMaster.ExportToMht(fileName);
                    break;
                case "PDF":
                    if (link != null)
                        link.PrintingSystem.ExportToPdf(fileName);
                    else
                        this.that.pivotGridMaster.ExportToPdf(fileName);
                    break;
                case "XLS":
                    if (link != null)
                        link.PrintingSystem.ExportToXls(fileName);
                    else
                        this.that.pivotGridMaster.ExportToXls(fileName);
                    break;
                case "XLSX":
                    if (link != null)
                        link.PrintingSystem.ExportToXlsx(fileName);
                    else
                        this.that.pivotGridMaster.ExportToXlsx(fileName);
                    break;
                case "RTF":
                    if (link != null)
                        link.PrintingSystem.ExportToRtf(fileName);
                    else
                        this.that.pivotGridMaster.ExportToRtf(fileName);
                    break;
                case "TXT":
                    if (link != null)
                        link.PrintingSystem.ExportToText(fileName);
                    else
                        this.that.pivotGridMaster.ExportToText(fileName);
                    break;
            }
        }
        void ExportHTML_Click(object sender, ItemClickEventArgs e)
        {
            ExportTo("HTML Document", "HTML Documents|*.html", "HTML");
        }
        void ExportMHT_Click(object sender, ItemClickEventArgs e)
        {
            ExportTo("MHT Document", "MHT Documents|*.mht", "MHT");
        }
        void ExportPDF_Click(object sender, ItemClickEventArgs e)
        {
            ExportTo("PDF Document", "PDF Documents|*.pdf", "PDF");
        }
        void ExportXLS_Click(object sender, ItemClickEventArgs e)
        {
            ExportTo("Microsoft Excel Document", "Microsoft Excel|*.xls", "XLS");
        }
        void ExportXLSX_Click(object sender, ItemClickEventArgs e)
        {
            ExportTo("Microsoft Excel 2007 Document", "Microsoft Excel 2007|*.xlsx", "XLSX");
        }
        void ExportRTF_Click(object sender, ItemClickEventArgs e)
        {
            ExportTo("RTF Document", "RTF Documents|*.rtf", "RTF");
        }
        void ExportTXT_Click(object sender, ItemClickEventArgs e)
        {
            ExportTo("Text Document", "Text Files|*.txt", "TXT");
        }
        #endregion
        #endregion

        public void pivotGridMaster_FocusedCellChanged(object sender, EventArgs e)
        {
            //...
        }

        private void comboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HelpWaiting.showMsgForm(this.that, drawGraph);            
            //this.that.chartControlMaster.SeriesTemplate.ChangeView((ViewType)this.that.comboChartType.SelectedItem);
            //if (this.that.chartControlMaster.Diagram is Diagram3D)
            //{
            //    Diagram3D diagram = (Diagram3D)this.that.chartControlMaster.Diagram;
            //    diagram.RuntimeRotation = true;
            //    diagram.RuntimeZooming = true;
            //    diagram.RuntimeScrolling = true;
            //}
        }

        private void drawGraph()
        {
            this.that.chartControlMaster.SeriesTemplate.ChangeView((ViewType)this.that.comboChartType.SelectedItem);
            PopupChartOption._setScrollAndZoom(this.that.chartControlMaster);
        }

        private void ceChartDataVertical_CheckedChanged(object sender, EventArgs e)
        {
            this.that.pivotGridMaster.OptionsChartDataSource.ChartDataVertical = this.that.ceChartDataVertical.Checked;
        }

        private void checkShowPointLabels_CheckedChanged(object sender, EventArgs e)
        {           
            this.that.chartControlMaster.SeriesTemplate.Label.Visible = this.that.checkShowPointLabels.Checked;
        }
    }
}