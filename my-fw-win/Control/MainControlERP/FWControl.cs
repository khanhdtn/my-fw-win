using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Printing;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public class FWControlData
    {
        //ACT :Action Có ảnh hưởng
        //VAL :Value Ko ảnh hưởng
        public static DataTable _VAL_VAT = null;
        public static DataTable VAL_VAT()
        {
            if (_VAL_VAT != null)
                return _VAL_VAT;
            else
                return HelpDataSetExt.CreateDataTable(new string[] { "VAT" }, new string[] { "" },
                      new string[] { "0", "5", "10" });
        }
    }

    public class FWControl
    {
        public static void _initVAT(PLCombobox Input)
        {
            DataSet ds = new DataSet();
            DataTable dt = FWControlData.VAL_VAT();
            ds.Tables.Add(dt);
            Input.DataSource = ds.Tables[0]; ;
            Input.DisplayField = "VAT";
            Input.ValueField = "VAT";
            Input._init();
        }

        public static void _initPrinters(ComboBoxEdit Input)
        {
            String pkInstalledPrinters;

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                Input.Properties.Items.Add(pkInstalledPrinters);
            }
        }
    }
}
