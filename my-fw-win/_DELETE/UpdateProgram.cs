using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    public class UpdateProgram
    {
        static string UPDATE_VERSION_FILE = RadParams.RUNTIME_PATH + @"\update\currentversion.txt";
        static string UPDATE_EXE_FILE = RadParams.RUNTIME_PATH + @"\update\LiveUpdate.exe";

        public static void ShowNewVersionInfo(){
            if (_checkNewVersion())
            {
                if(PLMessageBox.ShowConfirmMessage("Chương trình đã có phiên bản mới. Bạn có muốn cập nhật không ?") == DialogResult.Yes){
                    UpdateProgram._updateNewVersion();
                }
            }
        }

        //Ham kiem tra version hien tai da la moi nhat chua
        public static bool _checkNewVersion()
        {
            int verDb = _getCurVersion();
            int verFile = _getVerFromFile();

            //Kiem tra table da duoc tao chua hay la da co version moi tren database chua
            if (verDb == -1 || verDb == 0)
                return false;

            if (verDb > verFile)
                return true;
            return false;
        }

        //Lay version hien tai
        public static int _getCurVersion()
        {
            //int version = 0;
            //DatabaseFB db = DABase.getDatabase();
            //DbCommand command = DABase.getDatabase().GetSQLStringCommand("SELECT VERSION FROM LIVEUPDATE WHERE ID=1");
            //DataSet ds = new DataSet();
            //db.LoadDataSet(command , ds , "UPDATE");
            //if(ds.Tables.Count ==0)
            //    return -1;
            //try
            //{
            //    version = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            //}
            //catch { return 0; }
            //return version;
            int version = 0;
            try
            {
                DataSet ds = DABase.getDatabase().LoadDataSet("SELECT VERSION FROM LIVEUPDATE WHERE ID=1");
                if (ds.Tables.Count == 0)
                    return -1;
                version = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch { return -1; }
            return version;
        }

        //Lay version tu file
        private static int _getVerFromFile()
        {
            string verPath = UpdateProgram.UPDATE_VERSION_FILE;
            if (!File.Exists(verPath))
                _initFileVersion();
            int ver=-1;
            using (StreamReader writer = new StreamReader(verPath))
            {
                try
                {
                    ver = int.Parse(writer.ReadLine());
                }
                catch { }
            }
            return ver;
        }

        private static void _initFileVersion()
        {
            //Tao file version va gan gia tri khoi tao bang ko
            try
            {
                string verPath = UpdateProgram.UPDATE_VERSION_FILE;
                using (StreamWriter writer = new StreamWriter(verPath))
                {
                    writer.Write(0);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        //Tien hanh update version moi
        public static void _updateNewVersion()
        {
            string dbInfo = RadParams.server + ";" + RadParams.database + ";" + RadParams.port + ";" + RadParams.username + ";" + RadParams.password;
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_NO_THANKS);
            Process p = new Process();
            p.StartInfo.FileName = UpdateProgram.UPDATE_EXE_FILE;
            p.StartInfo.Arguments = FrameworkParams.ExecuteFileName + ";" + dbInfo;

            try
            {
                p.Start();
            }
            catch { }
        }

        public static void _createTable()
        {
            
                DatabaseFB db = DABase.getDatabase();
                DbCommand createCmd = db.GetSQLStringCommand("CREATE TABLE LIVEUPDATE(ID INTEGER NOT NULL, FILECONTENT BLOB sub_type 0 segment size 3000, VERSION INTEGER NOT NULL)"); 
                db.ExecuteNonQuery(createCmd);

                _initValue();
        }

        //Khoi tao gia tri ban dau cho table
        private static void _initValue()
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand insCmd = db.GetSQLStringCommand("INSERT INTO LIVEUPDATE VALUES(1 , NULL , 0)");
            db.ExecuteNonQuery(insCmd);
        }

        public static void _startUpdateNewVersion()
        {
            if (UpdateProgram._checkNewVersion())
            {
                DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn cập nhật chương trình mới không?");
                if (result == DialogResult.Yes)
                    UpdateProgram._updateNewVersion();
            }
            else
            {
                if (PLMessageBox.ShowConfirmMessage("Chương trình hiện tại bạn đang sử dụng là phiên bản mới nhất. Bạn có muốn cập nhật phiên bản mới không?") == DialogResult.Yes)
                {
                    UpdateProgram._updateNewVersion();
                }
            }
        }        
    }
}
