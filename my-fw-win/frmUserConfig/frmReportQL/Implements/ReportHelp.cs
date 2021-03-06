using System;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using ProtocolVN.Framework.Win;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Import;
using ProtocolVN.Framework.Core;
using System.Collections;

namespace ProtocolVN.Framework.Win
{
    class ReportHelp
    {
        String mReportFile;         
        DataSet mMainDataSet;

        string mPrinterName="";    //Tên máy in dùng để in
        string mPaperSize="";      //Cở giấy A4, A3, A2, ...
        int mMarginLeft;        //Lề trái
        int mMarginRight;       //Lề phải
        int mMarginTop;         //Lề trên
        int mMarginBottom;      //Lề dưới

        //Options
        Dictionary<string, object> mParameters; // Tham số của report
        DataSet[] mSubReportDataSets;           // Danh sách các dataset cho subreport
        string[] subReportFileNames;
        /// <summary>Dựng lên đối tượng report hỗ trợ in ấn.
        /// </summary>
        /// <param name="_reportFile">Tên của report file có đường dẫn từ thư mục thực thi</param>
        /// <param name="_parameter">Danh sách các tham số nếu có</param>
        /// <param name="_mainDataSet">Data Set cho MainReport</param>
        /// <param name="_subreportDataSet">Data Set cho SubReport</param>
        public ReportHelp(String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet, string[] subReportFileNames)
        {
            this.mReportFile = _reportFile;
            this.mParameters = _parameter;
            this.mMainDataSet = _mainDataSet;
            this.mSubReportDataSets = _subreportDataSet;
            this.subReportFileNames = subReportFileNames;
            try { this.mPrinterName = FrameworkParams.option.printerName; }
            catch { };
        }

        public ReportHelp(String _reportFile, Dictionary<string, object> _parameter, DataSet _mainDataSet, DataSet[] _subreportDataSet)
            : this( _reportFile, _parameter,  _mainDataSet, _subreportDataSet, null)
        {
        }

        ///// <summary>Hàm kiểm tra xem máy đã cài máy in chưa
        ///// </summary>
        ///// <returns></returns>
        //public static bool HasPrinter()
        //{
        //    if (PrinterSettings.InstalledPrinters.Count == 0)
        //        return false;
        //    return true;
        //}

        private List<DataSet> FillDataForSubReport(ReportDocument report)
        {
            List<DataSet> DSList = new List<DataSet>();

            if (mSubReportDataSets != null)
            {
                if (subReportFileNames == null)
                {
                    for (int i = 0; i < mSubReportDataSets.Length; i++)
                    {
                        report.Subreports[i].SetDataSource(mSubReportDataSets[i]);
                        DSList.Add(mSubReportDataSets[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < subReportFileNames.Length; i++)
                    {
                        for (int j = 0; j < report.Subreports.Count; j++)
                        {
                            if (subReportFileNames[i].Equals(report.Subreports[j].Name))
                            {
                                report.Subreports[j].SetDataSource(mSubReportDataSets[i]);
                                DSList.Add(mSubReportDataSets[i]);
                                break;
                            }
                        }
                    }
                }
            }

            return DSList;
        }
        private void FillParams(ReportDocument report)
        {
            if (mParameters != null)
            {
                foreach (string varKey in mParameters.Keys)
                {
                    object tempValue;
                    mParameters.TryGetValue(varKey, out tempValue);
                    ParameterField paraField = report.ParameterFields[varKey];
                    paraField.CurrentValues.Clear();
                    ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = tempValue;
                    paraField.CurrentValues.Add(paramDiscreteValue);
                }
            }
        }
        private void FillParams(PLCrystalReportViewer view)
        {
            if (mParameters != null)
            {
                CrystalDecisions.Shared.ParameterFields pars = new CrystalDecisions.Shared.ParameterFields();
                foreach (string varKey in mParameters.Keys)
                {
                    object tempValue;
                    mParameters.TryGetValue(varKey, out tempValue);
                    CrystalDecisions.Shared.ParameterField paramField = new CrystalDecisions.Shared.ParameterField();
                    paramField.Name = varKey;
                    CrystalDecisions.Shared.ParameterDiscreteValue paramDiscreteValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    paramDiscreteValue.Value = tempValue;
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    pars.Add(paramField);
                }
                //view._I.ParameterFieldInfo = pars;//phien bản 12
                view.ParameterFieldInfo = pars;//phien bản 10
            }
        }
        private ReportDocument GetRepObj()
        {
            ReportDocument report = new ReportDocument();
            if (mReportFile.IndexOf("\\EMB") >= 0) 
                report = (ReportDocument)GenerateClass.initObject(mReportFile.Substring(mReportFile.IndexOf("\\EMB") + 4));
            else 
                report.Load(mReportFile);

            return report;
        }
        #region Xem trước khi in
        /// <summary>Xem trước khi in
        /// </summary>
        /// <returns></returns>
        public XtraForm preview()
        {
            try
            {
                PLBlankReport frm = new PLBlankReport();
                PLCrystalReportViewer view = new PLCrystalReportViewer();
                ReportDocument report = GetRepObj();                
                report.SetDataSource(mMainDataSet);
                view._DSList = FillDataForSubReport(report);
                view._DSList.Insert(0, mMainDataSet);

                FillParams(view);
                //view._I.ReportSource = report;//phien bản 12
                view.ReportSource = report;//phien bản 10
                frm.WindowState = FormWindowState.Maximized;
                view.Dock = DockStyle.Fill;
                frm.Controls.Add(view);

                return frm;
            }
            catch (Exception ex)
            {
                PLMessageBox.ShowErrorMessage("Không kết nối được với máy in.\nVui lòng kiểm tra lại kết nối với máy in.");
                PLException.AddException(ex);
                return null;
            }
            
        }
        #endregion

        #region In Trực Tiếp ra máy In
        /// <summary>Đặt các thông số riêng cho bản in
        /// </summary>
        /// <param name="_printerName">Tên máy in sẽ in</param>
        /// <param name="_paperSize">Cỡ giấy</param>
        /// <param name="_marginLeft">Lề trái</param>
        /// <param name="_marginRight">Lề phải</param>
        /// <param name="_marginTop">Lề trên</param>
        /// <param name="_marginBottom">Lề Dưới</param>
        public void paperSetup(string _printerName, string _paperSize, int _marginLeft, int _marginRight, int _marginTop, int _marginBottom)
        {
            mPrinterName = _printerName;
            mPaperSize = _paperSize;
            mMarginLeft = _marginLeft;
            mMarginRight = _marginRight;
            mMarginTop = _marginTop;
            mMarginBottom = _marginBottom;
        }

        private void applyNewPaperSetup(ReportDocument report)
        {
            //Chọn máy in
            if (mPrinterName != ""){
                report.PrintOptions.PrinterName = mPrinterName;
            }
            //Chọn khổ giấy ( nếu không chọn có lâý đúng bản thiết kế không ) PHUOC TODO
            if (mPaperSize != "")
                report.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)System.Enum.Parse(typeof(CrystalDecisions.Shared.PaperSize), "Paper" + mPaperSize);
            
            //Canh lề 
            if (mMarginLeft > 0 && mMarginRight > 0 && mMarginTop > 0 && mMarginBottom > 0)
            {
                PageMargins pageMargin = new PageMargins(mMarginLeft, mMarginTop, mMarginRight, mMarginBottom);
                report.PrintOptions.ApplyPageMargins(pageMargin);
            }
        }
        /// <summary>In trực tiếp ra máy in
        /// </summary>
        public bool print()
        {
            try
            {
                ReportDocument report = GetRepObj();
                report.SetDataSource(mMainDataSet);
                FillDataForSubReport(report);
                FillParams(report);
                
                applyNewPaperSetup(report);
                report.PrintToPrinter(1, false, 0, 0);
                return true;
            }
            catch(Exception ex)
            {
                //HelpMsgBox.ShowNotificationMessage("Không kết nối được với máy in. Xin vui lòng kiểm tra lại kết nối.");
                PLException.AddException(ex);
                return false;
            }
        }        
        #endregion

        #region In Trực Tiếp Cho Chọn máy in và số bản in
        public void printSelectedPrinter()
        {
            try
            {
                ReportDocument report = GetRepObj();
                report.SetDataSource(mMainDataSet);//gan nguon cho main report
                List<DataSet> DSList = FillDataForSubReport(report);
                DSList.Insert(0, mMainDataSet);

                FillParams(report);
                //#region Phiên bản 12
                //PLCrystalReportViewer view = new PLCrystalReportViewer();
                //view._I.ReportSource = report;
                //view._DSList = DSList;
                //view._I.PrintReport();
                //#endregion
                #region Phiên bản 10
                PLCrystalReportViewer view = new PLCrystalReportViewer();
                view.ReportSource = report;
                view._DSList = DSList;
                view.PrintReport();
                #endregion
            }
            catch (Exception ex)
            {
                HelpMsgBox.ShowNotificationMessage("Lỗi máy in");
                PLException.AddException(ex);
            }
        }
        #endregion
    }
}
