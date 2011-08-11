using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public class HelpInPhieu
    {
        public delegate void FuncProcess(_Print p);
        public static _Print DefinePrintObj(string reportName, string StoreNameMain,
                string[] ParamNames, DbType[] types, long[] values, string[] StoreNameSub, FuncProcess func)
        {
            _Print print = new _Print();
            print.ReportNameFile = reportName;
            print.MainDataset = GetReportMain(StoreNameMain, ParamNames, types, values);
            print.SubDataset = GetSubReport(StoreNameSub);
            print.Parametres = new Dictionary<string, object>();
            if (func != null) func(print);
            return print;
        }

        private static DataSet GetReportMain(string StoreName, string[] ParamNames, DbType[] types, long[] values)
        {
            DataSet ds = new DataSet();
            DatabaseFB db = DABase.getDatabase();
            DbCommand cmd = db.GetStoredProcCommand(StoreName);
            for (int i = 0; i < ParamNames.Length; i++)
            {
                string param = ParamNames[i];
                db.AddInParameter(cmd, param, types[i], values[i]);
            }
            db.LoadDataSet(cmd, ds, StoreName);
            return ds;
        }

        private static DataSet[] GetSubReport(string[] StoreNames)
        {
            DataSet[] dataset = new DataSet[StoreNames.Length];
            for (int i = 0; i < StoreNames.Length; i++)
            {
                string StoreName = StoreNames[i];
                DataSet ds = new DataSet();
                DatabaseFB db = DABase.getDatabase();
                DbCommand sp = db.GetStoredProcCommand(StoreName);
                db.LoadDataSet(sp, ds, StoreName);
                dataset[i] = ds;
            }
            return dataset;
        }


        public delegate _Print GetPrintObj(XtraForm mainForm, PhieuType LoaiPhieu, long[] IDs);
        public static void PrintPhieu(XtraForm mainForm, PrintType CachIn, PhieuType LoaiPhieu, long[] IDs, GetPrintObj Print)
        {
            try
            {
                _Print _print = Print(mainForm, LoaiPhieu, IDs);
                _print.MainForm = mainForm;
                if (_print != null)
                {
                    if (CachIn == PrintType.PREVIEW)
                        HelpReport.Preview(_print);
                    else if (CachIn == PrintType.DIRECT)
                        HelpReport.Print(_print);
                }
            }
            catch
            { }
        }

        public static List<Object> InitInPhieu(ContextMenuStrip mnuIn, PLCPhieu Phieu, IDDOPhieu DOData)
        {
            List<object> items = new List<object>();

            System.Windows.Forms.ToolStripMenuItem item;
            item = new System.Windows.Forms.ToolStripMenuItem();
            item.Name = "itemXemTruocKhiIn";
            item.Text = "Xem trước khi in";
            item.Click += delegate(object sender, EventArgs e)
            {
                Phieu.GetPrintObj((XtraForm)mnuIn.FindForm(), new long[] { DOData.GetID() }).execPreviewWith();
            };
            mnuIn.Items.Add(item);
            ApplyPermissionAction.ApplyPermissionObject(items, item, Phieu.GetPhieuType().AllowIn);
            //Perm.Add(Phieu.GetPhieuType().AllowIn);

            item = new System.Windows.Forms.ToolStripMenuItem();
            item.Name = "itemIn";
            item.Text = "In";
            item.Click += delegate(object sender, EventArgs e)
            {
                Phieu.GetPrintObj((XtraForm)mnuIn.FindForm(), new long[] { DOData.GetID() }).execDirectlyPrint();
            };

            mnuIn.Items.Add(item);
            ApplyPermissionAction.ApplyPermissionObject(items, item, Phieu.GetPhieuType().AllowIn);
            //Perm.Add(Phieu.GetPhieuType().AllowIn);

            return items;
        }
    }
    

    public enum PrintType
    {
        PREVIEW,//Xem trước khi in
        DIRECT,//In trực tiếp
        DIALOG//Chọn máy in khi in
    }
}
