using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    public class Param
    {
        private long _NHOM_THAM_SO;

        public long NHOM_THAM_SO
        {
            get { return _NHOM_THAM_SO; }
            set { _NHOM_THAM_SO = value; }
        }
        private string _TEN_THAM_SO;

        public string TEN_THAM_SO
        {
            get { return _TEN_THAM_SO; }
            set { _TEN_THAM_SO = value; }
        }
        private string _GIA_TRI;

        public string GIA_TRI
        {
            get { return _GIA_TRI; }
            set { _GIA_TRI = value; }
        }
        private string _MO_TA;

        public string MO_TA
        {
            get { return _MO_TA; }
            set { _MO_TA = value; }
        }

        private string _TEN_THAM_SO_USER;

        public string TEN_THAM_SO_USER
        {
            get { return _TEN_THAM_SO_USER; }
            set { _TEN_THAM_SO_USER = value; }
        }

        /// <summary>
        /// 1: Text
        /// 2: Number
        /// 3: Date
        /// 4: Bool
        /// 5: Time
        /// </summary>
        private string _DATA_TYPE;

        public string DATA_TYPE
        {
            get { return _DATA_TYPE; }
            set { _DATA_TYPE = value; }
        }
    }

    public class ParamGroup
    {
        private long _ID;

        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _NAME;

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
    }

    public class frmAppParamsHelp
    {        
        public static List<Param> _initParams()
        {
            List<Param> list = new List<Param>();
            DataSet ds = DABase.getDatabase().LoadTable("FW_THAM_SO_UNG_DUNG");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Param param = new Param();
                param.NHOM_THAM_SO = (long)dr["NHOM_THAM_SO"];
                param.TEN_THAM_SO = dr["TEN_THAM_SO"].ToString();
                param.DATA_TYPE = dr["DATA_TYPE"].ToString();
                param.GIA_TRI = dr["GIA_TRI"].ToString();
                param.MO_TA = dr["MO_TA"].ToString();
                param.TEN_THAM_SO_USER = dr["TEN_THAM_SO_USER"].ToString();                
                if(dr["VISIBLE_BIT"].ToString().Equals("Y"))
                    list.Add(param);
            }
            return list;
        }

        public static List<ParamGroup> _initParamGroup()
        {
            List<ParamGroup> list = new List<ParamGroup>();
            string sql = "select distinct nhom_tham_so, ten_nhom_tham_so from fw_tham_so_ung_dung";
            DataSet ds = DABase.getDatabase().LoadDataSet(sql);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ParamGroup paramG = new ParamGroup();
                paramG.ID = (long)dr["NHOM_THAM_SO"];
                paramG.NAME = dr["TEN_NHOM_THAM_SO"].ToString();
                list.Add(paramG);
            }
            return list;
        }        

        public static DataTable LoadSchema()
        {
            QueryBuilder query = new QueryBuilder(
                "select * from fw_tham_so_ung_dung where 1=1");
            query.addCondition("1=-1");
            return DABase.getDatabase().LoadReadOnlyDataSet(query).Tables[0];
        }

        public static bool Update(DataTable Source)
        {
            bool flag = false;
            if (Source != null)
            {
                DataSet MainDS = DABase.getDatabase().LoadTable("FW_THAM_SO_UNG_DUNG");
                if (MainDS.Tables[0].Rows.Count > 0)
                {
                    HelpDataSet.MergeTable(new string[] { "NHOM_THAM_SO", "TEN_THAM_SO" }, MainDS.Tables[0], Source, true, true);
                    flag = DatabaseFB.Update2DataSet(HelpGen.G_FW_ID, MainDS, null, false);
                }                
            }
            return flag;
        }

        //PHUOCNC......................................................

        public static Object GetThamSo(string TenThamSo)
        {
            string sql = "select gia_tri, DATA_TYPE from fw_tham_so_ung_dung where " +
                "ten_tham_so='" + TenThamSo + "' and visible_bit='Y'";
            DbCommand select = DABase.getDatabase().GetSQLStringCommand(sql);

            //PHUOCNT TODO
            //Chuyen ve doi tuong dua vao DataType
            IDataReader reader = DABase.getDatabase().ExecuteReader(select);
            if(reader.Read()){
                return HelpMultiDataTypeField.GetObjectFromPLString(reader["gia_tri"].ToString(), 
                    HelpMultiDataTypeField.ToFWDatType(HelpNumber.ParseInt32(reader["DATA_TYPE"])));
            }
            return null;
            
            //return DABase.getDatabase().ExecuteScalar(select) != null ? DABase.getDatabase().ExecuteScalar(select) : null;            
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


        public static bool SetThamSo(string TenThamSo, object GiaTri, FWPLDataType dataType)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                DbCommand dbUpdate = null;
                if (TonTaiThamSo(TenThamSo))
                {
                    dbUpdate = db.GetSQLStringCommand("update fw_tham_so_ung_dung set gia_tri=@giatri where ten_tham_so=@thamso");                    
                    db.AddInParameter(dbUpdate, "@thamso", DbType.String, TenThamSo);
                    db.AddInParameter(dbUpdate, "@giatri", DbType.String, HelpMultiDataTypeField.GetPLStringFromObject(GiaTri, dataType));
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
        /// <summary>
        /// Đặt giá trị vào tên tham số tương ứng
        /// Nếu TenThamSo đó tồn tại sẽ overwrite dữ liệu hiện tại
        /// </summary>
        /// <param name="TenThamSo">Tên tham số cần lấy giá trị</param>
        /// <param name="GiaTri">Giá trị</param>
        public static bool SetThamSo(string TenThamSo, object GiaTri)
        {
            return SetThamSo(TenThamSo, GiaTri, FWPLDataType.TEXT);                
        }
    }
}
