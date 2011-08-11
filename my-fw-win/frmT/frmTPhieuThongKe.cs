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
    public abstract class frmTPhieuThongKeChange : XtraFormPL
    {
        //public PopupMenu popupMenu1;
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
        #endregion

        public QueryBuilder filter = null;
        protected frmTPhieuThongKeChange() { }
        
        public abstract void InitFieldMaster();
        public abstract QueryBuilder PLBuildQueryFilter();
        public abstract void PLLoadFilterPart();
        public virtual bool CheckFilter()
        {
            return true;
        }
    }

    public class TPhieuThongKeFix : DevExpress.DXperience.Demos.LookAndFeelMenu
    {        
        // Fields
        private bool filter = false;
        private frmTPhieuThongKeChange that;
        private String _tenBaoCao;
        private bool _clickXem = false;

        #region INIT FORM
        public TPhieuThongKeFix(frmTPhieuThongKeChange phieuTK, bool hasFilterPart)
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
            this.that.barCheckItemFilter.Glyph = FWImageDic.FILTER_IMAGE20;
            this.that.barButtonItemTuyChonXemDoThi.Glyph = FWImageDic.CHART_IMAGE20;
            this.that.barButtonItemTuyChonXemDoThi.Enabled = false;
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
        void barButtonItemTuyChonXemDoThi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (this._clickXem == false)
            //{
            //    this.DoSearch();
            //    this._clickXem = true;
            //}

            try
            {
                string chartTitle = GetTenBaoCao();
                PopupChartOption popup = new PopupChartOption(
                    this.that.pivotGridMaster, chartTitle != ""
                    ? "Biểu đồ " + chartTitle : "Biểu đồ thống kê");
                HelpProtocolForm.ShowModalDialog(this.that, popup, true);
            }
            catch (Exception exception)
            {
                PLException.AddException(exception);
                PLMessageBox.ShowErrorMessage("Không thể xem chi tiết biểu đồ.");
            }
            finally
            {
            }
        }
        #endregion

        #region Xem thống kê PIVOT
        void barButtonItemSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.that.CheckFilter())
            {
                HelpWaiting.showMsgForm(this.that, DoSearch);
            }
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
                }
                this.pivotGridMaster_FocusedCellChanged(null, null);
                this._clickXem = true;
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
                this.that.barButtonItemTuyChonXemDoThi.Enabled = this.that.barButtonItemExport.Enabled;
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
                PrintableComponentLink link = FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this.GetTenBaoCao(),
                    "Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));
                link.ShowPreviewDialog();
            }
            else
            {
                if (this.that.pivotGridMaster == null) return;
                this.that.pivotGridMaster.ShowPrintPreview();
            }
        }
        void  barItemIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (FrameworkParams.headerLetter != null)
            {
                FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this.GetTenBaoCao(),
                    "Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat)).PrintDlg();
            }
            else
            {
                if (this.that.pivotGridMaster == null) return;
                this.that.pivotGridMaster.Print();
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
                link = FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this.GetTenBaoCao(),
                    "Ngày báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));

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

        #region XÓA
        //void PLDaChonCell(bool status)
        //{
        //    if (status)
        //    {
        //        this._setEnableMenu(true);
        //    }
        //    else
        //    {
        //        this._setEnableMenu(false);
        //    }
        //}
        //void createBusinessMenu(string[] captions, string fieldName, string[] ImageNames,
        //    DelegationLib.CallFunction_MulIn_NoOut[] delegates, PermissionItem[] pers)
        //{
        //    PhieuThongKeHelp.CreateBusinessMenu(this.that.pivotGridMaster,
        //        this.that.barSubItem1, fieldName, captions, ImageNames, delegates, pers);
        //}
        //void PrintPreview_OnClick(object sender, ItemClickEventArgs e)
        //{
        //    if (FrameworkParams.headerLetter != null)
        //    {
        //        PrintableComponentLink link = FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this._tenBaoCao,
        //            "Ngày xuất báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat));                
        //        link.ShowPreviewDialog();

        //    }
        //    else
        //    {
        //        if (this.that.pivotGridMaster == null) return;
        //        this.that.pivotGridMaster.ShowPrintPreview();
        //    }                                  
        //}
        //void Print_OnClick(object sender, ItemClickEventArgs e)
        //{
        //    if (FrameworkParams.headerLetter != null)
        //    {
        //        FrameworkParams.headerLetter.Draw(that.pivotGridMaster, this._tenBaoCao,
        //            "Ngày xuất báo cáo: " + DateTime.Today.ToString(FrameworkParams.option.dateFormat)).PrintDlg();
        //    }
        //    else
        //    {
        //        if (this.that.pivotGridMaster == null) return;
        //        this.that.pivotGridMaster.Print();
        //    }
        //}
        //void chartControlMaster_DoubleClick(object sender, EventArgs e)
        //{
        //    _showTuyChonXemDoThi();
        //}
        //void _setBusinessMenu()
        //{
        //    try
        //    {
        //        _MenuItem businessMenuList = this.that.GetBusinessMenuList();
        //        if (businessMenuList != null)
        //        {
        //            this.createBusinessMenu(businessMenuList.CaptionNames,
        //                businessMenuList.FieldName, businessMenuList.ImageNames,
        //                businessMenuList.Funcs, businessMenuList.Permissions);
        //        }
        //        else
        //        {
        //            this.that.barSubItem1.Visibility = BarItemVisibility.Never;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        PLException.AddException(exception);
        //    }
        //}

        //void _setContextMenuOnGrid()
        //{
        //    PhieuThongKeHelp.SetContextMenuOnGrid(this.that.pivotGridMaster,
        //        this.that.GetMenuAppendGridList(), this.that.GetBusinessMenuList(),
        //        this.that.IncludeNghiepVu);
        //}

        //void splitContainerControl1_Resize(object sender, EventArgs e)
        //{
        //    this.that.splitContainerControl1.SplitterPosition =
        //        (3 * this.that.splitContainerControl1.Height) / 4;
        //}
        //Giải pháp đơn giản PHUOCNT
        //void ExportTo(string title, string filter, string exportFormat)
        //{
        //    try
        //    {
        //        if (this.that.pivotGridMaster == null) return;
        //        string fileName = ShowSaveFileDialog(title, filter);
        //        if (fileName != "")
        //        {
        //            this.that.Refresh();
        //            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
        //            FrameworkParams.wait = new WaitingMsg();
        //            switch (exportFormat)
        //            {
        //                case "HTML": this.that.pivotGridMaster.ExportToHtml(fileName);
        //                    break;
        //                case "MHT": this.that.pivotGridMaster.ExportToMht(fileName);
        //                    break;
        //                case "PDF": this.that.pivotGridMaster.ExportToPdf(fileName);
        //                    break;
        //                case "XLS": this.that.pivotGridMaster.ExportToXls(fileName);
        //                    break;
        //                case "XLSX": this.that.pivotGridMaster.ExportToXlsx(fileName);
        //                    break;
        //                case "RTF": this.that.pivotGridMaster.ExportToRtf(fileName);
        //                    break;
        //                case "TXT": this.that.pivotGridMaster.ExportToText(fileName);
        //                    break;
        //            }
        //            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();

        //            if (PLMessageBox.ShowConfirmMessage("Bạn có muốn mở file này không?") == DialogResult.Yes)
        //            {
        //                HelpFile.OpenFile(fileName);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
        //    }

        //}

        //#region Cách Export của Duy
        //System.Threading.Thread thread;
        //bool stop;
        //void StartExport()
        //{
        //    Thread.Sleep(400);
        //    if (stop)
        //        return;
        //    ExportForm progressForm = new ExportForm(this.that);
        //    //ProgressForm progressForm = new ProgressForm(this.that);
        //    progressForm.Show();
        //    try
        //    {
        //        while (!stop)
        //        {
        //            Application.DoEvents();
        //            Thread.Sleep(100);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    progressForm.Dispose();
        //}

        //void EndExport()
        //{
        //    stop = true;
        //    thread.Join();
        //}

        //void ExportTo(string title, string filter, string exportFormat)
        //{
        //    if (this.that.pivotGridMaster == null) return;
        //    string fileName = ShowSaveFileDialog(title, filter);
        //    if (fileName != "")
        //    {
        //        this.that.Refresh();
        //        stop = false;
        //        thread = new Thread(new ThreadStart(StartExport));
        //        thread.Start();
        //        Cursor currentCursor = Cursor.Current;
        //        Cursor.Current = Cursors.WaitCursor;

        //        switch (exportFormat)
        //        {
        //            case "HTML": this.that.pivotGridMaster.ExportToHtml(fileName);
        //                break;
        //            case "MHT": this.that.pivotGridMaster.ExportToMht(fileName);
        //                break;
        //            case "PDF": this.that.pivotGridMaster.ExportToPdf(fileName);
        //                break;
        //            case "XLS": this.that.pivotGridMaster.ExportToXls(fileName);
        //                break;
        //            case "XLSX": this.that.pivotGridMaster.ExportToXlsx(fileName);
        //                break;
        //            case "RTF": this.that.pivotGridMaster.ExportToRtf(fileName);
        //                break;
        //            case "TXT": this.that.pivotGridMaster.ExportToText(fileName);
        //                break;
        //        }
        //        EndExport();
        //        Cursor.Current = currentCursor;
        //        //OpenFile(fileName);
        //        if (PLMessageBox.ShowConfirmMessage("Bạn có muốn mở file này không?") == DialogResult.Yes)
        //        {
        //            HelpFile.OpenFile(fileName);
        //        }
        //    }
        //}
        //#endregion
        #endregion
    }
}