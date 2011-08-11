using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Data;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class _ConfigPrinter
    {
        #region fields
        string printerName;
        string pageSize;
        int marginLeft;
        int marginRight;
        int marginTop;
        int marginBottom;
        #endregion

        #region properties
        public string PrinterName
        {
            get { return printerName; }
            set { printerName = value; }
        }


        public string PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int MarginLeft
        {
            get { return marginLeft; }
            set { marginLeft = value; }
        }

        public int MarginRight
        {
            get { return marginRight; }
            set { marginRight = value; }
        }

        public int MarginTop
        {
            get { return marginTop; }
            set { marginTop = value; }
        }

        public int MarginBottom
        {
            get { return marginBottom; }
            set { marginBottom = value; }
        }
        #endregion
    }

    public class _Print
    {
        #region  fields
        XtraForm mainForm;
        string reportNameFile;
        Dictionary<string, object> parametres;
        DataSet mainDataset;
        DataSet[] subDataset;       //Danh sách subDataSet
        string[] subReportFileNames;//Danh sách report name tương ứng SubDataSet
        _ConfigPrinter thietDatIn;
        #endregion

        #region methods
        public void execDirectlyPrint()
        {
            if (HelpCrystalReport.HasPrinter() == true)
            {
                if (thietDatIn != null)//có cấu hình thông số in
                    HelpReport.Print(reportNameFile, parametres, mainDataset, subDataset, thietDatIn.PrinterName, thietDatIn.PageSize, thietDatIn.MarginLeft, thietDatIn.MarginRight, thietDatIn.MarginTop, thietDatIn.MarginBottom);
                else//in mặc định
                {
                    ReportHelp pl = new ReportHelp(reportNameFile, parametres, mainDataset, subDataset, subReportFileNames);
                    if (pl.print() == false)
                        HelpMsgBox.ShowNotificationMessage("Lỗi máy in.");
                    //phương thức này in không được và cả phương thức override
                    //HelpReport.Print(reportFullPathFile, parametres, mainDataset, subDataset );
                }
            }
            else
                HelpMsgBox.ShowNotificationMessage("Chưa cài đặt máy in.");
        }

        public void execPreviewWith()
        {
            HelpReport.Preview(mainForm, reportNameFile, parametres, mainDataset, subDataset, subReportFileNames);
        }

        public void execPintShowDialog()
        {
            HelpReport.PrintShowDialog(reportNameFile, parametres, mainDataset, subDataset, subReportFileNames);
        }

        #endregion

        #region properties
        public XtraForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        public string ReportNameFile
        {
            get { return reportNameFile; }
            set { reportNameFile = RadParams.RUNTIME_PATH + @"\report\" + value; }
        }
        public Dictionary<string, object> Parametres
        {
            get { return parametres; }
            set { parametres = value; }
        }
        public DataSet MainDataset
        {
            get { return mainDataset; }
            set { mainDataset = value; }
        }
        public DataSet[] SubDataset
        {
            get { return subDataset; }
            set { subDataset = value; }
        }

        public string[] SubReportFileName
        {
            get { return subReportFileNames; }
            set { subReportFileNames = value; }
        }
        public _ConfigPrinter ThietDatIn
        {
            get { return thietDatIn; }
            set { thietDatIn = value; }
        }
        #endregion;
    };
}
