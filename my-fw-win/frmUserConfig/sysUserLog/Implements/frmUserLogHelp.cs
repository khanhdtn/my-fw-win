using System;
using System.Data;
using System.Data.Common;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    public class DAUserLog
    {
        public static DAUserLog Instance = new DAUserLog();

        private DAUserLog() 
        {
        }

        public DataSet loadDataSetOperationLog(string tableName)        
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT op.USERID, op.CDATE," +
              "EXTRACT(HOUR FROM CDATE) || ':' || EXTRACT(MINUTE FROM CDATE) AS HOUR_LOG, " +
              //" op.CDATE as HOUR_LOG, " +
              " op.LOGCONTENT, uc.USERNAME, op.LOG_LEVEL FROM FW_USER_LOG_REL" + 
              " op, USER_CAT uc where op.USERID=uc.USERID");
            DataSet ds = new DataSet();
            db.LoadDataSet(dbSelect, ds, tableName);
            return ds;
        }

        public bool insert(long userID, string content)
        {
            string[] msgs = content.Split(';');
            string msg = msgs[0];
            string level = "";
            if (msgs.Length == 2) level = msgs[1];
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = null;
            try
            {
                dbTrans = db.BeginTransaction(db.OpenConnection());
                DbCommand insert = db.GetSQLStringCommand("INSERT INTO FW_USER_LOG_REL" +
                            " (USERID, CDATE, LOGCONTENT, LOG_LEVEL) VALUES(@userId ,'NOW',@content, @log_level)");
                db.AddInParameter(insert, "@userId", DbType.Int64, userID);
                db.AddInParameter(insert, "@content", DbType.String, msg.Trim());
                db.AddInParameter(insert, "@log_level", DbType.String, level.Trim());            

                db.ExecuteNonQuery(insert, dbTrans);
                //db.CommitTransaction(dbTrans);
                dbTrans.Commit();
                return true;
            }
            catch
            {
                //db.RollbackTransaction(dbTrans);
                dbTrans.Rollback();
                return false;
            }            
        }
        
        public static bool delete(long userid, DateTime date)
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbTrans = null;
            try
            {
                dbTrans = db.BeginTransaction(db.OpenConnection());
                DbCommand dbDelete = db.GetSQLStringCommand("DELETE FROM FW_USER_LOG_REL WHERE userid=@userid and CDATE=@date");
                db.AddInParameter(dbDelete, "@userid", DbType.Int64, userid);
                db.AddInParameter(dbDelete, "@date", DbType.DateTime, date);
                db.ExecuteNonQuery(dbDelete, dbTrans);
                db.CommitTransaction(dbTrans);
            }
            catch
            {
                db.RollbackTransaction(dbTrans);
                return false;
            }
            return true;          
        }        
    }

    public class UserLog
    {
        public long userID;
        public string username;
        public DateTime date;
        public string Content;

        public UserLog(long idUser, string name, DateTime date, string Content)
        {
            this.userID = idUser;
            this.username = name;
            this.date = date;
            this.Content = Content;
        }

        public static DataView LoadAllOperationLog()
        {
            DataSet ds = DAUserLog.Instance.loadDataSetOperationLog("USER_LOG");
            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables["USER_LOG"]);
            return dv;
        }

        public static void Log(long userid, string content)
        {
            DAUserLog.Instance.insert(userid, content);
        }

        public static void Delete(long userid, DateTime date)
        {
            DAUserLog.delete(userid, date);
        }

        public static bool Log(string msg)
        {
            try
            {
                UserLog.Log(FrameworkParams.currentUser.id, msg);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
