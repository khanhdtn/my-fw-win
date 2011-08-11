using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace ProtocolVN.Framework.Win
{
    class BackupRestoreInfo 
    {
        public long UserId;        
        public string FilePath;
        public string Note;
        public char IsBackup;

        public BackupRestoreInfo() { }

        public BackupRestoreInfo(long UserId, string FilePath, string Note, char IsBackup)
        {
            this.UserId = UserId;
            this.FilePath = FilePath;
            this.Note = Note;
            this.IsBackup = IsBackup;
        }

        public bool backup()
        {
            return DABackup.Instance.Backup(UserId, FilePath, Note);
        }

        public DataSet GetHistory()
        {
            return DABackup.Instance.LoadBackupRestoreLog("BACK_REST_LOG");                        
        }


        public bool restore()
        {
            if (System.IO.File.Exists(this.FilePath))
                return DABackup.Instance.Restore(UserId, FilePath, Note);
            else
                return false;
        }
    }
}
