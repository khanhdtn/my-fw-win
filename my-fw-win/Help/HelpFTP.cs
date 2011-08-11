using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Net;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp hỗ trợ làm việc với FTP Server
    /// </summary>
    public class HelpFTP
    {
        public static string FTP_FILE = RadParams.RUNTIME_PATH + @"\conf\ftp.cpl";
        private static string key = "FTP-TEAMBEAN_107EFACD9002";
        private static readonly HelpFTP instance = new HelpFTP();
        private HelpFTP()
        {
            loadFtp();
        }

        public static HelpFTP Instance
        {
            get
            {
                return instance;
            }
        }

        private Chilkat.Ftp2 SetInfomation(string host, int port, string username, string password)
        {
            Chilkat.Ftp2 ftp = new Chilkat.Ftp2();
            ftp.UnlockComponent(key);
            ftp.Hostname = host;
            ftp.Port = port;
            ftp.Username = username;
            ftp.Password = password;
            ftp.SetTypeAscii();
            return ftp;
        }

        public bool upload(String localFile , String ftpFile)
        {
            Chilkat.Ftp2 client = SetInfomation(MyFtp.Ip, HelpNumber.ParseInt32(MyFtp.Port), MyFtp.Username, MyFtp.Password);

            string dir = (ftpFile.Split(new char[]{'/'}))[0];

            try
            {
                if (client.Connect())
                {
                    client.CreateRemoteDir(dir);
                    if (client.PutFile(localFile, ftpFile))
                    {
                        client.Disconnect();
                        return true;
                    }
                    client.Disconnect();
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool checkConnect(string ip,string port, string username, string password)
        {
          
            Chilkat.Ftp2 client = SetInfomation(ip, HelpNumber.ParseInt32(port) , username, password);
            try
            {
                if (client.Connect())
                {
                    client.Disconnect();
                    return true;
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        public bool checkConnect()
        {
            Chilkat.Ftp2 client = SetInfomation(MyFtp.Ip , HelpNumber.ParseInt32(MyFtp.Port) , MyFtp.Username , MyFtp.Password);
            try
            {
                if (client.Connect())
                {
                    client.Disconnect();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool download(String ftpFile , String localFile)
        {
            Chilkat.Ftp2 client = SetInfomation(MyFtp.Ip , HelpNumber.ParseInt32(MyFtp.Port) , MyFtp.Username , MyFtp.Password);
            try
            {
                if (client.Connect())
                {
                    if (File.Exists(localFile))
                        File.Delete(localFile);
                    if (client.GetFile(ftpFile, localFile))
                    {
                        client.Disconnect();
                        return true;
                    }
                    client.Disconnect();
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public long getFileSize(string filePath)
        {
            long fileSize;
            Chilkat.Ftp2 client = SetInfomation(MyFtp.Ip, HelpNumber.ParseInt32(MyFtp.Port), MyFtp.Username, MyFtp.Password);
            try
            {
                if (client.Connect())
                {
                    fileSize = (long)client.GetSizeByName(filePath);
                    client.Disconnect();
                    return fileSize;
                }
                return -1;
            }
            catch
            {
                return -1;
            }
        }

        public bool delete(String ftpFile)
        {
            Chilkat.Ftp2 client = SetInfomation(MyFtp.Ip , HelpNumber.ParseInt32(MyFtp.Port) , MyFtp.Username , MyFtp.Password);
            try
            {
                if (client.Connect())
                {
                    if (client.DeleteRemoteFile(ftpFile))
                    {
                        client.Disconnect();
                        return true;
                    }
                    client.Disconnect();
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        public bool createDir(String ftpDir , String dirName)
        {
            Chilkat.Ftp2 client = SetInfomation(MyFtp.Ip , HelpNumber.ParseInt32(MyFtp.Port) , MyFtp.Username , MyFtp.Password);
            try
            {
                if (client.Connect())
                {
                    if (client.CreateRemoteDir(ftpDir + "/" + dirName))
                    {
                        client.Disconnect();
                        return true;
                    }
                    client.Disconnect();
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        public bool removeDir(string ftpDir)
        {
            Chilkat.Ftp2 client = SetInfomation(MyFtp.Ip, HelpNumber.ParseInt32(MyFtp.Port), MyFtp.Username, MyFtp.Password);
            try
            {
                if (client.Connect())
                {
                    client.DeleteTree();
                    if (client.RemoveRemoteDir(ftpDir))
                    {
                        client.Disconnect();
                        return true;
                    }
                    client.Disconnect();
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool renameDir(string oldName, string newName)
        {
            try
            {
                FtpWebRequest reqFTP;
                string uri = "ftp://" + MyFtp.Ip + ":" + MyFtp.Port + "/" + oldName;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(MyFtp.Username, MyFtp.Password);
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo =  newName;

                WebResponse response = reqFTP.GetResponse();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool isExist(string remotePath)
        {
            try
            {
                string[] lstDir = getFolderFTP();
                foreach (string dir in lstDir)
                    if (dir.Equals(remotePath)) return true;
                return false;
            }
            catch
            {
                return false;
            }

        }

        public string[] getFolderFTP()
        {
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            string[] lstDir = null;
            Chilkat.Ftp2 ftp = new Chilkat.Ftp2();
            try
            {
                string uri = "ftp://" + MyFtp.Ip + "/";

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(MyFtp.Username, MyFtp.Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response
                                                .GetResponseStream());

                string line = reader.ReadLine();

                if (line == null)
                {
                    result.Append(line);
                    result.Append("\n");
                }

                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }


                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                lstDir = result.ToString().Split('\n');
                if (lstDir[0] == "") lstDir = null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return lstDir;
        }

        public string[] getFilesFTP(string folderName)
        {
            string[] lstFiles = null;
            if (folderName != "")
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest reqFTP;
                try
                {
                    string uri = "ftp://" + MyFtp.Ip + "/" + folderName;

                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(MyFtp.Username , MyFtp.Password);
                    reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                    WebResponse response = reqFTP.GetResponse();
                    StreamReader reader = new StreamReader(response
                                                    .GetResponseStream());

                    string line = reader.ReadLine();

                    if (line == null)
                    {
                        result.Append(line);
                        result.Append("\n");
                    }

                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = reader.ReadLine();
                    }


                    result.Remove(result.ToString().LastIndexOf('\n') , 1);
                    reader.Close();
                    response.Close();
                    lstFiles = result.ToString().Split('\n');
                    if (lstFiles[0] == "") lstFiles = null;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            return lstFiles;
        }

        public static void loadFtp()
        {
            try
            {
                DataSet ds = new DataSet();
                if (!ConfigFile.ReadXML(HelpFTP.FTP_FILE, ds))
                {
                    string data = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
                                <NewDataSet>
                                  <ftp>
                                    <ip>protocolvn.info</ip>
                                    <username>visitor</username>
                                    <password>visitor</password>
                                    <port>21</port>
                                  </ftp>
                                </NewDataSet>";
                    ConfigFile.Save(FTP_FILE, data);
                    ConfigFile.ReadXML(HelpFTP.FTP_FILE, ds);
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable myDataTable = ds.Tables["ftp"];
                    DataRow row = myDataTable.Rows[0];
                    MyFtp.Ip = (string)row["ip"];
                    MyFtp.Port = (string)row["port"];
                    MyFtp.Username = (string)row["username"];
                    MyFtp.Password = (string)row["password"];
                }
            }
            catch { }
        }
    }

    public class MyFtp
    {
        private static string ip = "";
        private static string port = "";
        private static string username = "";
        private static string password = "";

        public MyFtp() { }

        public static string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public static string Port
        {
            get { return port; }
            set { port = value; }
        }

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        public static string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}