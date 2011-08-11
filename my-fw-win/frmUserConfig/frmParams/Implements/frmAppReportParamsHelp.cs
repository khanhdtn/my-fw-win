using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    public class frmAppReportParamsHelp
    {
        public static string KEY_FIELD_NAME = "ID";

        public static List<ParamField> getFields()
        {
            QueryBuilder query = new QueryBuilder(@"
                    SELECT
                        rf.rdb$field_name, rf.rdb$description, 
                        f.rdb$field_type, f.rdb$field_length
                    FROM rdb$relation_fields rf
                        join rdb$relations r
                        on rf.rdb$relation_name = r.rdb$relation_name
                        join rdb$fields f
                        on f.rdb$field_name = rf.rdb$field_source
                        left join rdb$types t
                        on t.rdb$type = f.rdb$field_type
                        and t.rdb$field_name = 'RDB$FIELD_TYPE'
                    WHERE
                        r.rdb$view_blr is null and
                        (r.rdb$system_flag is null or r.rdb$system_flag=0) and
                        rf.rdb$relation_name = 'FW_REPORT_PARAMS' 
                        and rf.rdb$field_name <> '" + KEY_FIELD_NAME + "' and 1=1");
            DataSet ds = DABase.getDatabase().LoadDataSet(query);
            List<ParamField> list = new List<ParamField>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ParamField field = new ParamField();
                field.FIELD_NAME = dr["RDB$FIELD_NAME"].ToString().Trim();
                field.DESCRIPTION = dr["RDB$DESCRIPTION"].ToString();
                field.FIELD_TYPE = HelpNumber.ParseInt32(dr["RDB$FIELD_TYPE"]);
                field.FIELD_LENGTH = HelpNumber.ParseInt64(dr["RDB$FIELD_LENGTH"]);
                list.Add(field);
            }
            return list;
        }

        public static DataSet LoadDataSource()
        {
            return DABase.getDatabase().LoadDataSet(
                "select * from FW_REPORT_PARAMS where 1=1", "FW_REPORT_PARAMS");
        }

        public static bool Update(DataTable Source)
        {
            bool flag = false;
            if (Source != null)
            {
                DataSet MainDS = DABase.getDatabase().LoadDataSet(
                    "select * from FW_REPORT_PARAMS where 1=1", "FW_REPORT_PARAMS");
                if (MainDS.Tables[0].Rows.Count > 0)
                {
                    HelpDataSet.MergeTable(new string[] { "ID" }, MainDS.Tables[0], Source, false, true);
                    flag = DatabaseFB.Update2DataSet(HelpGen.G_FW_ID, MainDS, null, false);
                }
            }
            return flag;
        }        

        public static Object GetThamSo(string TenThamSo)
        {
            string sql = "select " + TenThamSo + " from FW_REPORT_PARAMS"; 
            DbCommand select = DABase.getDatabase().GetSQLStringCommand(sql);
            return DABase.getDatabase().ExecuteScalar(select) != null ? DABase.getDatabase().ExecuteScalar(select) : null;        
        }

        /// <summary>
        /// Kiểm tra xem Tên Tham Số đó có tồn tại không
        /// </summary>
        /// <param name="TenThamSo"></param>
        /// <returns></returns>
        public static bool TonTaiThamSo(string TenThamSo)
        {
            if (GetThamSo(TenThamSo) == null) return false;
            else return true;
        }

        /// <summary>
        /// Đặt giá trị vào tên tham số tương ứng
        /// Nếu TenThamSo đó tồn tại sẽ overwrite dữ liệu hiện tại
        /// </summary>
        /// <param name="TenThamSo">Tên tham số cần lấy giá trị</param>
        /// <param name="GiaTri">Giá trị</param>
        public static bool SetThamSo(string TenThamSo, object GiaTri)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                DbCommand dbUpdate = null;
                if (TonTaiThamSo(TenThamSo))
                {
                    dbUpdate = db.GetSQLStringCommand("update FW_REPORT_PARAMS set " + TenThamSo + "=@giatri where " + TenThamSo + "=@thamso");
                    db.AddInParameter(dbUpdate, "@thamso", DbType.String, TenThamSo);
                    db.AddInParameter(dbUpdate, "@giatri", DbType.String, GiaTri);
                }
                else
                    return false;

                db.ExecuteNonQuery(dbUpdate);
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return false;
            }            
        }        
    }

    public class ParamField
    {
        private string _FIELD_NAME;

        public string FIELD_NAME
        {
            get { return _FIELD_NAME; }
            set { _FIELD_NAME = value; }
        }
        private string _DESCRIPTION;

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        private int _FIELD_TYPE;

        public int FIELD_TYPE
        {
            get { return _FIELD_TYPE; }
            set { _FIELD_TYPE = value; }
        }
        private long _FIELD_LENGTH;

        public long FIELD_LENGTH
        {
            get { return _FIELD_LENGTH; }
            set { _FIELD_LENGTH = value; }
        }
    }

    public enum FB_DATA_TYPE
    {
        SMALLINT = 7,
        INTEGER = 8,
        INT64 = 16,
        FLOAT = 10,
        D_FLOAT = 11,
        BOOLEAN = 17,
        DOUBLE = 27,
        DATE = 12,
        TIME = 13,
        TIMESTAMP = 35,
        VARCHAR = 37,
        CHAR = 14,
        CSTRING = 40,
        BLOB_ID = 45,
        BLOB = 261
    }
}