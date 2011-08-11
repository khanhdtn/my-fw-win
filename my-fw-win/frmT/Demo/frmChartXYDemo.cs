using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using System.Data;

namespace ProtocolVN.Framework.Win.Demo
{
    public class ChartXYDemo : IChartXY
    {
        #region IChartXY Members

        public string GetCaptionX()
        {
            return "Tháng";
        }

        public string GetCaptionY()
        {
            return "Doanh số bán";
        }

        public string GetSeriesName()
        {
            return "Tháng";
        }

        public string GetTitle()
        {
            return "Doanh số bán hàng trong năm";
        }

        public string GetXFN()
        {
            return "THANG";
        }

        public string GetYFN()
        {
            return "DOANHSO";
        }

        public string[] MasterCaption()
        {
            return new string[]{"Tháng", "Họ tên nhân viên", "Doanh số bán"};
        }

        public System.Data.DataSet MasterData()
        {
            DataTable dt = new DataTable();            
            DataColumn cT = new DataColumn("THANG");
            DataColumn cNV = new DataColumn("TENNV");
            DataColumn cDS = new DataColumn("DOANHSO", 1.GetType());
            dt.Columns.Add(cT);
            dt.Columns.Add(cNV);
            dt.Columns.Add(cDS);
            dt.Rows.Add("T1", "NVA", 100);
            dt.Rows.Add("T2", "NVA", 80);
            dt.Rows.Add("T3", "NVA", 100);
            dt.Rows.Add("T4", "NVA", 80);
            dt.Rows.Add("T5", "NVA", 100);
            dt.Rows.Add("T6", "NVA", 80);
            dt.Rows.Add("T7", "NVA", 100);
            dt.Rows.Add("T8", "NVA", 80);
            dt.Rows.Add("T9", "NVA", 100);
            dt.Rows.Add("T10", "NVA", 80);
            dt.Rows.Add("T11", "NVA", 80);
            dt.Rows.Add("T12", "NVA", 80);            
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].AcceptChanges();
            return ds;
        }

        public string[] DetailCaption()
        {
            return new string[] { "Thang", "Phieu ban hang", "Thanh tien"};
        }

        public System.Data.DataSet DetailData(string IDKey)
        {
            DataTable dt = new DataTable();
            DataColumn tenNV = new DataColumn("THANG");
            DataColumn pbh = new DataColumn("PBH");
            DataColumn tTien = new DataColumn("THANHTIEN", 1.GetType());
            dt.Columns.Add(tenNV);
            dt.Columns.Add(pbh);
            dt.Columns.Add(tTien);
            if (IDKey == "T1")
            {
                dt.Rows.Add("T1", "PBH1", 100);
                dt.Rows.Add("T1", "PBH2", 80);
            }
            else if (IDKey == "T2") 
            {
                dt.Rows.Add("T2", "PBH3", 100);
                dt.Rows.Add("T2", "PBH3", 100);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].AcceptChanges();
            return ds;
        }

        #endregion
    }
}
