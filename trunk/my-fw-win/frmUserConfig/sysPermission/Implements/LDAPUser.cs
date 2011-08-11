using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    public class LDAPConfig
    {
        public static LDAPConfig I = new LDAPConfig();

        public string FULLNAME = "CN";
        public string EMAIL = "mail";
        public string LDAP_PATH = "LDAP://protocolvn.info";

        private LDAPConfig() { 

        }
    }

    public class LDAPUser
    {
        #region Các hàm thao tác trên DB
        private static long? KiemTraUserTrongDBUSerCat(string user)
        {
            long blResult = -1;
            string query = "Select * from USER_CAT where USERNAME ='" + user + "'";
            DataTable dttUSER_CAT = DABase.getDatabase().LoadDataSet(query).Tables[0];
            if (dttUSER_CAT.Rows.Count > 0)
            {
                foreach (DataRow dtr in dttUSER_CAT.Rows)
                {
                    if (dtr["EMPLOYEE_ID"] == null)
                        return -1;
                    else
                        return long.Parse(dtr["EMPLOYEE_ID"].ToString());                    
                }
                return null;
            }
            else {
                return null;
            };            
        }
        private static long ThemUserTrongDBNhanVien(string name, string email)
        {
            long blResult = -1;
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());
            long getIDUser = DABase.getDatabase().GetID(HelpGen.G_FW_ID);
            string strsql = "Insert into DM_NHAN_VIEN(ID,TEN_NV,EMAIL,VISIBLE_BIT, DEPARTMENT_ID) values(@ID,@NAME,@EMAIL,'Y',1)";
            DbCommand cmdLuuKhachHang = db.GetSQLStringCommand(strsql);
            db.AddInParameter(cmdLuuKhachHang, "@ID", DbType.Int64, getIDUser);
            db.AddInParameter(cmdLuuKhachHang, "@NAME", DbType.String, name);
            db.AddInParameter(cmdLuuKhachHang, "@EMAIL", DbType.String, email);
            if (db.ExecuteNonQuery(cmdLuuKhachHang, dbTrans) > 0)
            {
                db.CommitTransaction(dbTrans);
                blResult = getIDUser;
            }
            else { };
            return blResult;
        }
        private static bool ThemUserTrongDBUserCat(string user, string pass, long employee_ID)
        {
            bool blResult = false;
            DateTime lastAccess = DateTime.Now;
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());
            long getIDUser = DABase.getDatabase().GetID(HelpGen.G_FW_ID);
            string strsql = "Insert into USER_CAT(USERID,USERNAME,PWD,LASTACCESS,ISCHANGEPWD_BIT,ISACTIVE_BIT,EMPLOYEE_ID) values(@USERID,@USERNAME,@PWD,@LASTACCESS,@ISCHANGEPWD_BIT,@ISACTIVE_BIT,@EMPLOYEE_ID)";
            DbCommand cmdLuuKhachHang = db.GetSQLStringCommand(strsql);
            db.AddInParameter(cmdLuuKhachHang, "@USERID", DbType.Int64, getIDUser);
            db.AddInParameter(cmdLuuKhachHang, "@USERNAME", DbType.String, user);
            db.AddInParameter(cmdLuuKhachHang, "@PWD", DbType.String, pass);
            db.AddInParameter(cmdLuuKhachHang, "@LASTACCESS", DbType.DateTime, lastAccess);
            db.AddInParameter(cmdLuuKhachHang, "@ISCHANGEPWD_BIT", DbType.String, "Y");
            db.AddInParameter(cmdLuuKhachHang, "@ISACTIVE_BIT", DbType.String, "Y");
            if(employee_ID==-1)
                db.AddInParameter(cmdLuuKhachHang, "@EMPLOYEE_ID", DbType.Int64, DBNull.Value);
            else
                db.AddInParameter(cmdLuuKhachHang, "@EMPLOYEE_ID", DbType.Int64, employee_ID);

            if (db.ExecuteNonQuery(cmdLuuKhachHang, dbTrans) > 0)
            {
                db.CommitTransaction(dbTrans);
                blResult = true;
            }
            else { };
            return blResult;
        }
        // --------Bảng USER_CAT chỉ update Column LASTACCESS
        private static bool UpdateUserTrongDBUserCat(string username)
        {
            bool blReSult = false;
            DateTime dttLastAccess = DateTime.Now;
            string sql = "update USER_CAT set LASTACCESS = @LASTACCESS where USERNAME = @USERNAME";
            DatabaseFB db = DABase.getDatabase();
            DbCommand cmd = db.GetSQLStringCommand(sql);
            db.AddInParameter(cmd, "@LASTACCESS", DbType.DateTime, dttLastAccess);
            db.AddInParameter(cmd, "@USERNAME", DbType.String, username);
            if (db.ExecuteNonQuery(cmd) > 0)
            {
                blReSult = true;
            }
            return blReSult;
        }
        private static bool KiemTraUserTrongBangDBNhanVien(long id)
        {
            bool blResult = false;
            string query = "Select * from DM_NHAN_VIEN where ID =" + id.ToString();
            DataTable dttUSER_CAT = DABase.getDatabase().LoadDataSet(query).Tables[0];
            if (dttUSER_CAT.Rows.Count > 0)
            {
                blResult = true;
            }
            else { };
            return blResult;
        }
        private static bool UpdateUserTrongDBNhanVien(long id, string tenNhanVien, string email)
        {
            bool blReSult = false;
            DateTime dttLastAccess = DateTime.Now;
            string sql = "update DM_NHAN_VIEN set TEN_NV = @TEN_NV, EMAIL = @EMAIL where ID = @ID";
            DatabaseFB db = DABase.getDatabase();
            DbCommand cmd = db.GetSQLStringCommand(sql);
            db.AddInParameter(cmd, "@TEN_NV", DbType.String, tenNhanVien);
            db.AddInParameter(cmd, "@EMAIL", DbType.String, email);
            db.AddInParameter(cmd, "@ID", DbType.Int64, id);
            if (db.ExecuteNonQuery(cmd) > 0)
            {
                blReSult = true;
            }
            return blReSult;
        }
        private static long ThemUserTrongDBNhanVien(long id, string name, string email)
        {
            long blResult = -1;
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = db.BeginTransaction(db.OpenConnection());
            long getIDUser = id;
            string strsql = "Insert into DM_NHAN_VIEN(ID,TEN_NV,EMAIL) values(@ID,@NAME,@EMAIL)";
            DbCommand cmdLuuKhachHang = db.GetSQLStringCommand(strsql);
            db.AddInParameter(cmdLuuKhachHang, "@ID", DbType.Int64, getIDUser);
            db.AddInParameter(cmdLuuKhachHang, "@NAME", DbType.String, name);
            db.AddInParameter(cmdLuuKhachHang, "@EMAIL", DbType.String, email);
            if (db.ExecuteNonQuery(cmdLuuKhachHang, dbTrans) > 0)
            {
                db.CommitTransaction(dbTrans);
                blResult = getIDUser;
            }
            else { };
            return blResult;
        }
        #endregion

        public static bool Login(string username, string pwd)
        {
            bool blResult = false;
            string strUser = "";
            string strPass = "";
            strUser = username;
            strPass = pwd;
            bool IsVeryfy = false;
            string path = LDAPConfig.I.LDAP_PATH;
            if (!(strUser.Equals("") || strPass.Equals("")))
            {
                // Kiểm tra trên LDAP
                IsVeryfy = HelpLDAP.Verify(strUser, strPass, path);
            }
            else
            {
                return false;
            }
            // Nếu có thì Kiểm Tra trên DB
            if (IsVeryfy == true)
            {                
                string fullName = HelpLDAP.GetInfo(strUser, strPass, LDAPConfig.I.LDAP_PATH, LDAPConfig.I.FULLNAME);
                string email = HelpLDAP.GetInfo(strUser, strPass, LDAPConfig.I.LDAP_PATH, LDAPConfig.I.EMAIL);

                long? longKiemTraTrenDB = KiemTraUserTrongDBUSerCat(strUser);
                
                if (longKiemTraTrenDB == null)//Không tồn tại tài khoản
                {
                    long longIDNhanVien = -1;
                    if (PLMessageBox.ShowConfirmMessage("Bạn đăng nhập hệ thống lần đầu. \nBạn có muốn tạo nhân viên có tên là " +
                        fullName + " và cấp tài khoản này cho nhân viên mới tạo?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        // Chèn trong bảng DM_NHAN_VIEN trước 
                        longIDNhanVien = ThemUserTrongDBNhanVien(fullName, email);
                    }
                                        
                    // Nếu thêm thành công thì thêm tiếp bên USER_CAT
                    ThemUserTrongDBUserCat(strUser, "protocol", longIDNhanVien);
                }
                // Nếu có cập nhật bên 2 bảng USER_CAT
                else
                {
                    UpdateUserTrongDBUserCat(strUser);
                    //// Kiểm Tra bên DM_NHAN_VIEN có nhân viên hay không,nếu không có thì thêm, có rồi thì cập nhập
                    //if (KiemTraUserTrongBangDBNhanVien(longKiemTraTrenDB))
                    //{
                    //    UpdateUserTrongDBNhanVien(longKiemTraTrenDB, fullName, email);
                    //}
                    //else
                    //{
                    //    ThemUserTrongDBNhanVien(longKiemTraTrenDB, fullName, email);
                    //}
                }
                blResult = true;
            }
            return blResult;
        }
    }
}
