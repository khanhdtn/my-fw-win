using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;

namespace ProtocolVN.Framework.Win
{
    public class FilterCase
    {
        #region Properties
        private long _id;               //Số thứ tự cho các query (Tự động tăng)
        private long _userid;           //ID của người dùng
        private string _datasetid;         //ID tương ứng dataset cần duyệt
        private string[] _captions;     //Danh sách các captions cần hiển thị lên lưới
        private string[] _fields;       //Danh sách các field cần đưa vào lưới để hiển thị
        private string _title;
        //Tille của câu query mà người dùng đặt. 
        //Sau này chỉ hiển thị phần này trên combobox. Còn value của nó chình là Query
        private string _sqlQuery; //Trường này dùng để hiển truy vấn lấy dữ liệu
        
        private System.Data.DataSet _data;  //Cấu trúc dataset cần thực thi filter
        private string _QuerySource;


        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public long USERID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public string DATASETID
        {
            get { return _datasetid; }
            set { _datasetid = value; }
        }
        public string TITLE
        {
            get { return _title; }
            set { _title = value; }
        }
        public string[] CAPTION
        {
            get { return _captions; }
            set { _captions = value; }
        }
        public string[] FILEDS
        {
            get { return _fields; }
            set { _fields = value; }
        }
        public string SQLQUERY
        {
            get { return _sqlQuery; }
            set { _sqlQuery = value; }
        }
        public string QUERYSOURCE
        {
            get { return _QuerySource; }
            set { _QuerySource = value; }
        }

        #endregion


        public FilterCase(long UserID, string DataSetID, string Title, string QuerySource)
        {
            this._userid = UserID;
            this._datasetid = DataSetID;
            this._title = Title;
            this._QuerySource = QuerySource;
        }

        public FilterCase(long UserID, string DataSetID, string Title,
            string[] Fields, string[] Captions, string QuerySource)
        {
            this._userid = UserID;
            this._datasetid = DataSetID;
            this._title = Title;
            this._captions = Captions;
            this._fields = Fields;
            this._QuerySource = QuerySource;
        }

        public bool CheckFieldExist(string fieldname)
        {
            foreach (string item in this.FILEDS)
            {
                if (item.Trim() == fieldname)
                    return true;
            }
            return false;
        }
       
        public DataSet DataSetFilterFromDatabase(string sqlWhere)
        {
            try
            {
                string sql = "";
                if (this._QuerySource.EndsWith("1=1"))
                    sql = this._QuerySource + " and " + sqlWhere;
                else
                    sql = this._QuerySource + " where " + sqlWhere;

                return DABase.getDatabase().LoadDataSet(sql);
            }
            catch (Exception ex) { PLException.AddException(ex); }
            return null;
        }

        #region Xử lý DB đặc thù của Firebird - TODO: Xử lý dọc lập với Firebird

        public String GetSQLCommand(SQLDATA sqlData)
        {
            string sql = this._QuerySource;
            //if (!this._QuerySource.EndsWith("1=1"))
            //    sql = this._QuerySource + " where 1=1";
            string wrapperSQL = "select * from (" + sql + ") SAVE_QUERY where ";
            wrapperSQL += sqlData.Filters;
            wrapperSQL = wrapperSQL.Replace("[", " ");
            wrapperSQL = wrapperSQL.Replace("]", " ");

            return wrapperSQL;
        }

        public DataSet DataSetFilterFromDatabase(SQLDATA sqlData)
        {
            try
            {
                string sql = this._QuerySource;
                //if (!this._QuerySource.EndsWith("1=1"))
                //    sql = this._QuerySource + " where 1=1";
                string wrapperSQL = "select * from (" + sql + ") SAVE_QUERY where ";
                wrapperSQL += sqlData.Filters;
                wrapperSQL = wrapperSQL.Replace("[", " ");
                wrapperSQL = wrapperSQL.Replace("]", " ");

                DatabaseFB fb = DABase.getDatabase();
                DataSet ds = new DataSet();
                DbCommand select = fb.GetSQLStringCommand(wrapperSQL);
                fb.AddParameters(select, HelpDBExt.GenerateDbInputParam(sqlData.ParameterDataTypes));
                fb.LoadDataSet(select, ds, "tableName");

                if (ds.Tables.Count == 0)
                {                    
                    PLException.AddException(new PLException(
                        new Exception(), "", "", wrapperSQL, "Câu truy vấn bị lỗi"));
                    HelpMsgBox.ShowNotificationMessage("Điều kiện tìm không còn hợp lệ. Vui lòng tạo điều kiện tìm mới");
                }
                return ds;
            }
            catch (Exception ex) { PLException.AddException(ex); }
            return null;
        }
        #endregion
    }
}
