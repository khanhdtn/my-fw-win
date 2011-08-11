using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Data.Common;
using ICSharpCode.SharpZipLib.Zip;

namespace ProtocolVN.Framework.Win
{
    public class LiveUpdateHelper
    {
        static string UPDATE_VERSION_FILE = RadParams.RUNTIME_PATH + @"\pl-version.ver";
        static string UPDATE_EXE_FILE = RadParams.RUNTIME_PATH + @"\update\LiveUpdate.exe";
        public static string UPDATE_DOWNLOAD_FOLDER = RadParams.RUNTIME_PATH + @"\update\download";
        public static string UPDATE_DOWNLOAD_VERSION_ZIP_FILE = RadParams.RUNTIME_PATH + @"\update\download\update_version.zip";

        public static void updateNewVersionHelper(bool IsLocalServerUpdate, string newVersion)
        {
            string isFromFile = "true";
            if( IsLocalServerUpdate == true) isFromFile = "false";
            string dbInfo = RadParams.server + ";" + RadParams.database + ";" + RadParams.port + ";" + 
                RadParams.username + ";" + RadParams.password + ";" + newVersion + ";" + isFromFile;
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_NO_THANKS);
            Process p = new Process();
            p.StartInfo.FileName = LiveUpdateHelper.UPDATE_EXE_FILE;
            p.StartInfo.Arguments = FrameworkParams.ExecuteFileName + ";" + dbInfo;
            try { p.Start(); }
            catch { }
        }

        /// <summary>
        /// Chỉ kiểm tra và cập nhật version mới khi máy version đặt tại LocalServer
        /// không hỗ trợ khi version đặt tại Protocol.
        /// </summary>
        public static void updateVersionFromLocalServer()
        {
            int localVersion = getLocalhostVersion();
            int serverVersion = Int32.MinValue;
            if (FrameworkParams.IsUpdateVersionAtLocalServer == true)
            {
                serverVersion = getVersionFromCustomerServer();
            }

            if (localVersion < serverVersion)
            {
                if (PLMessageBox.ShowConfirmMessage("Có phiên bản phần mềm mới nhất từ PROTOCOL Software. \n Bạn có muốn cập nhật không ?") != DialogResult.Yes)
                {
                    return;
                }

                //Dữ liệu cập nhật đã có trong DB rồi

                //Nâng cấp
                LiveUpdateHelper.updateNewVersionHelper(FrameworkParams.IsUpdateVersionAtLocalServer, serverVersion + "");
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage("Phiên bản phần mềm đang sử dụng là mới nhất.");
                return;
            }
        }
        /// <summary>
        /// Lấy thông tin phiên bản từ tập tin ZIP
        /// </summary>
        public static int getVersionFromZipFile(String zipFilePath)
        {
            ZipInputStream zipStream = null;
            int newVersion = -1;

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(zipFilePath));
                FileStream streamWriter = null;
                ZipEntry entry;
                string directoryName = "";
                string directory = "";
                string filePath = "";

                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    //directoryName = Path.GetDirectoryName(entry.Name);
                    //directory = "";
                    //if (directoryName.Length > 0 && directoryName != "update")
                    //{
                    //    directory = desPath + @"\" + directoryName;
                    //    //Directory.CreateDirectory(directory);
                    //}
                    if (Path.GetFileName(entry.Name).EndsWith("pl-version.ver"))
                    {
                        int size;
                        byte[] data = new byte[(int)zipStream.Length];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                newVersion = HelpNumber.ParseInt32(System.Text.Encoding.GetEncoding("utf-8").GetString(data));
                                if (newVersion == Int32.MinValue)
                                    newVersion = -1;                                    
                            }
                            else break;
                        }
                        break;
                    }
                }
                zipStream.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                try { if(zipStream!=null) zipStream.Close(); }
                catch { }
            }
            return newVersion;
        }
        

        public static int getVersionFromFile(String verFilePath)
        {
            int ver = -1;
            using (StreamReader writer = new StreamReader(verFilePath))
            {
                try
                {
                    String line = writer.ReadLine();
                    String[] results = line.Split(';');
                    ver = int.Parse(results[0]);
                }
                catch { }
            }
            return ver;
        }

        public static int getLocalhostVersion()
        {
            if (!File.Exists(LiveUpdateHelper.UPDATE_VERSION_FILE))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(LiveUpdateHelper.UPDATE_VERSION_FILE))
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

            return getVersionFromFile(LiveUpdateHelper.UPDATE_VERSION_FILE);
        }

        public static int getVersionFromProtocolServer()
        {
            String serverLicenceFileURL = FrameworkParams.UpdateURL;                       
            try
            {
                System.Net.WebClient webClient = new System.Net.WebClient();
                String versionStr = webClient.DownloadString(serverLicenceFileURL);
                String[] arrays = versionStr.Split(';');
                int versionNum = HelpNumber.ParseInt32(arrays[0]);
                if (versionNum == Int32.MinValue)
                    return -1;
                else
                    return versionNum;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return -1;
            }
        }

        public static string getVersionURLFromProtocolServer()
        {
            String serverLicenceFileURL = FrameworkParams.UpdateURL;
            try
            {
                System.Net.WebClient webClient = new System.Net.WebClient();
                String versionStr = webClient.DownloadString(serverLicenceFileURL);
                String[] arrays = versionStr.Split(';');
                return FrameworkParams.UpdateURL.Substring(0, FrameworkParams.UpdateURL.LastIndexOf('/')) + "/" + arrays[1];                 
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return "";
            }
        }

        public static int getVersionFromCustomerServer()
        {
            int version = -1;
            try
            {
                DataSet ds = DABase.getDatabase().LoadDataSet("SELECT VERSION FROM FW_LIVE_UPDATE WHERE ID=1");
                if (ds.Tables.Count == 0) return -1;
                version = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch { return -1; }
            return version;
        }

        public static void initDataStructure()
        {
            DatabaseFB db = DABase.getDatabase();
            DbCommand createCmd = db.GetSQLStringCommand("CREATE TABLE FW_LIVE_UPDATE(ID INTEGER NOT NULL, FILECONTENT BLOB sub_type 0 segment size 3000, VERSION INTEGER NOT NULL)");
            db.ExecuteNonQuery(createCmd);

            db = DABase.getDatabase();
            DbCommand insCmd = db.GetSQLStringCommand("INSERT INTO FW_LIVE_UPDATE VALUES(1 , NULL , 0)");
            db.ExecuteNonQuery(insCmd);
        }
    }
}
