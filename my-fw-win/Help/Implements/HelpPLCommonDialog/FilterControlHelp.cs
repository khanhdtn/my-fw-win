using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors.Filtering;

namespace ProtocolVN.Framework.Win
{
    public class FilterControlHelp
    {
        /// <summary>
        /// Lấy danh sách tất cả các câu truy vấn đã lưu
        /// </summary>
        /// <param name="Input"></param>
        public static void InitCombobox(PLCombobox Input, FilterCase filter)
        {
            DataSet ds = new DataSet();
            DatabaseFB db = DABase.getDatabase();
            System.Data.Common.DbCommand cmd = db.GetSQLStringCommand("select * from FW_QUERY_STORE where USERID = @USERID and DATASETID = @DATASETID");
            db.AddInParameter(cmd, "@USERID", DbType.Int64, filter.USERID);
            db.AddInParameter(cmd, "@DATASETID", DbType.String, filter.DATASETID);
            db.LoadDataSet(cmd, ds, "FW_QUERY_STORE");

            if (ds.Tables.Count == 0 || (ds.Tables.Count == 1 && ds.Tables[0] == null)) 
                PLException.AddException(new Exception("Thiếu bảng FW_QUERY_STORE"));

            //Tùy chọn nguồn dữ liệu
            Input.DataSource = ds.Tables[0];
            //Giá trị hiển thị khi chọn
            Input.DisplayField = "TITLE";
            //Giá trị nhận được khi lấy giá trị
            Input.ValueField = "ID";

            Input._init();
        }

        ///// <summary>
        ///// Lấy danh sách tất cả các câu truy vấn đã lưu
        ///// </summary>
        ///// <param name="Input"></param>
        //public static void InitCombobox(ProtocolVN.Framework.Win.PLCombobox Input)
        //{
        //    DataSet ds = DABase.getDatabase().LoadTable("FW_QUERY_STORE");
        //    if (ds == null) PLException.AddException(new Exception("Thiếu bảng FW_QUERY_STORE"));
        //    //Tùy chọn nguồn dữ liệu
        //    Input.DataSource = ds.Tables[0];
        //    //Giá trị hiển thị khi chọn
        //    Input.DisplayField = "TITLE";
        //    //Giá trị nhận được khi lấy giá trị
        //    Input.ValueField = "ID";

        //    Input._init();
        //}

        /// <summary>
        /// Lấy nội dung của câu truy vấn
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetQueryFromID(long ID)
        {
            try
            {
                DataSet ds = DABase.getDatabase().LoadDataSet("select * from FW_QUERY_STORE where ID = " + ID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["QUERY"].ToString();
                }
            }
            catch (Exception ex) { PLException.AddException(ex); }
            return "";
        }

        /// <summary>
        /// Xóa 1 câu truy vấn
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool Delete(long ID)
        {
            try
            {
                string sql = "delete from FW_QUERY_STORE where ID = @ID";
                DatabaseFB db = DABase.getDatabase();
                DbCommand cmd = db.GetSQLStringCommand(sql);
                db.AddInParameter(cmd, "@ID", DbType.Int64, ID);
                if (db.ExecuteNonQuery(cmd) > 0) return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return false;
        }



        #region Hàm này sẽ bỏ vì giải pháp cũ khi sinh ra câu truy vấn của Châu không hợp lý
        public static string ConvertQueryFilterToWhereSql(string QueryFilterStr)
        {
            string Kq = QueryFilterStr;
            Kq = Kq.Replace("[", "");
            Kq = Kq.Replace("]", "");
            Kq = Kq.Replace("#", "'");
            Kq = Kq.Replace("Between(", "Between ");
            Kq = Kq.Replace(",", " and ");
            Kq = Kq.Replace(")", " ");
            return Kq;
        }
        #endregion

        /// <summary>Hàm này kiểm tra sự tồn tại của query tương ứng người dùng, datasetID
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private static bool CheckLap(string title, long userID, String dataSetID)
        {
            try
            {
                DataSet ds = new DataSet();
                DatabaseFB db = DABase.getDatabase();
                System.Data.Common.DbCommand cmd = db.GetSQLStringCommand("select * from FW_QUERY_STORE where USERID = @USERID and TITLE = @TITLE and DATASETID = @DATASETID");
                db.AddInParameter(cmd, "@USERID", DbType.Int64, userID);
                db.AddInParameter(cmd, "@DATASETID", DbType.String, dataSetID);
                db.AddInParameter(cmd, "@TITLE", DbType.String, title);
                db.LoadDataSet(cmd, ds, "DANH_SACH_QUERY");
                if (ds.Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception ex) { PLException.AddException(ex); }
            return false;
        }


        /// <summary>Hàm lưu lại một câu query
        /// </summary>
        /// <param name="queryAdd"></param>
        /// <returns></returns>
        public static bool Save(string queryAdd, long userID, String dataSetID, string title)
        {
            try
            {
                //string _sqlQuery = ConvertQueryFilterToWhereSql(queryAdd);

                string _sqlQuery = queryAdd;

                if (!CheckLap(title, userID, dataSetID))
                {
                    long ID = DABase.getDatabase().GetID(HelpGen.G_FW_ID);

                    DatabaseFB db = DABase.getDatabase();
                    string sql = "insert into FW_QUERY_STORE(ID,USERID,QUERY,TITLE,DATASETID,SQL_QUERY) values(@ID,@USERID,@QUERY,@TITLE,@DATASETID,@SQL_QUERY)";
                    System.Data.Common.DbCommand cmd = db.GetSQLStringCommand(sql);
                    db.AddInParameter(cmd, "@ID", DbType.Int64, ID);
                    db.AddInParameter(cmd, "@DATASETID", DbType.String, dataSetID);
                    db.AddInParameter(cmd, "@USERID", DbType.Int64, userID);
                    db.AddInParameter(cmd, "@TITLE", DbType.String, title);
                    db.AddInParameter(cmd, "@QUERY", DbType.String, queryAdd);
                    db.AddInParameter(cmd, "@SQL_QUERY", DbType.String, _sqlQuery);
                    if (db.ExecuteNonQuery(cmd) > 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return false;
        }
    }
}
