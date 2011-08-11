using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public enum ProgramName
    {
        PDF,
        SWF,
        FLV,
        NA
    }
    /// <summary>
    /// Dùng để hỗ trợ xử lý trên tập tin
    /// </summary>
    public class HelpFile : ZipFile
    {
        /// <summary>
        /// Mở tập tin dùng chương trình của hệ thống
        /// nếu ko có dùng chương trình có trong chương trình của PROTOCOL
        /// </summary>
        /// <param name="FileName"></param>
        public static void OpenFile(string FileName, ProgramName program)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            try
            {                
                proc.Start();
            }
            catch (Win32Exception e)
            {                
                switch (program)
                {
                    case ProgramName.PDF:
                        string foxitFile = RadParams.RUNTIME_PATH + @"\apps\pdf.exe";
                        if (File.Exists(foxitFile))
                            System.Diagnostics.Process.Start(foxitFile, "\"" + FileName + "\"");
                        return;
                    case ProgramName.SWF:
                        string swfFile = RadParams.RUNTIME_PATH + @"\apps\FlashPlayer.exe";
                        if (File.Exists(swfFile))
                            System.Diagnostics.Process.Start(swfFile, "\"" + FileName + "\"");
                        return;
                    case ProgramName.FLV:
                        string flvFile = RadParams.RUNTIME_PATH + @"\apps\FlashPlayer.exe";
                        if (File.Exists(flvFile))
                            System.Diagnostics.Process.Start(flvFile, "\"" + FileName + "\"");
                        return;
                }
                System.Diagnostics.Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " + FileName);
            }
        }

        /// <summary>
        /// Hệ thống sẽ tự động tạo folder nếu nó ko tồn tại
        /// </summary>        
        public static bool NeedFolder(string folderName)
        {
            try
            {
                if (!(System.IO.Directory.Exists(folderName)))
                {
                    System.IO.Directory.CreateDirectory(folderName);
                }
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem kích thước của tập tin có nhỏ hơn số maxFileSizeMB
        /// Nếu trả về false khi kích thước file lớn hơn maxFileSizeMB
        ///            true khi kích thước nhỏ hơn maxFileSizeMB
        /// PROJECTS: PLOFFICE        
        /// </summary>
        /// <param name="path"></param>
        /// <param name="maxFileSizeMB"></param>
        /// <returns></returns>
        public static bool CheckFileSize(string path, long maxFileSizeMB)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Length / 1048576 > maxFileSizeMB)
                return false;
            return true;
        }

        /// <summary>Chuyển dữ liệu từ file thành mảng bytes
        /// Nếu tập tin lớn sẽ xuất hiện hợp thoại chờ
        /// </summary>
        public static byte[] FileToBytes(string fullPath)
        {
            bool usingWaiting = false;
            FileStream fs = null;
            byte[] bytes = null;
            try
            {
                fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                if (fs.Length / 1048576 > 1)//Kích thước lớn hơn 1MB
                    if (FrameworkParams.wait == null)
                    {
                        FrameworkParams.wait = new WaitingMsg();
                        usingWaiting = true;
                    }

                bytes = new byte[fs.Length];
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);
                    if (n == 0) break;
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                numBytesToRead = bytes.Length;

            }
            catch (IOException e) { return null; }
            catch (OutOfMemoryException oe) { return null; }
            finally {
                if(fs!=null) fs.Close();
                if (usingWaiting && FrameworkParams.wait !=null)
                {
                    FrameworkParams.wait.Finish();
                }
            }
            return bytes;
        }

        /// <summary>
        /// Chuyển dữ liệu dạng mảng bytes sang file
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static bool BytesToFile(byte[] bytes, string fullPath)
        {
            int numBytesToWrite = bytes.Length;
            bool usingWaiting = false;
            if (numBytesToWrite / 1048576 > 1)//Kích thước lớn hơn 1MB
                if (FrameworkParams.wait == null)
                {
                    FrameworkParams.wait = new WaitingMsg();
                    usingWaiting = true;
                }

            FileStream fs = null;
            try
            {
                fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                fs.Write(bytes, 0, numBytesToWrite);
            }
            catch (IOException e)
            {
                e.StackTrace.ToString();
                return false;
            }
            finally
            {
                if(fs!=null) fs.Close();
                if (usingWaiting && FrameworkParams.wait != null)
                {
                    FrameworkParams.wait.Finish();
                }
            }

            return true;
        }
        
        /// <summary>
        /// Mở file dùng chương trình mặc định của hệ thống
        /// không dùng phần mềm riêng trong sản phẩm của PROTOCOL
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static bool OpenFile(string fullPath)
        {
            return HelpExe.RunExe(fullPath);
        }

        
        /// <summary>
        /// Copy file từ src qua đích.
        /// Nếu tập đích đã tồn tại sẽ bị xóa.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="tgt"></param>
        public static bool CopyFiles(string src, string tgt)    
        {
            try
            {
                if (File.Exists(tgt))
                {
                    File.Delete(tgt);
                }
                File.Copy(src, tgt);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Obsolete("Dùng hàm HelpByte.ImageToByteArray để thay thế")]
        public static byte[] ImageToByteArray(Image image)
        {
            //System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            //image.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //byte[] ret = mStream.ToArray();
            //mStream.Close();
            //return ret;

            return ImageToByteArray(image);
        }

        /// <summary>Mở tập tin từ 1 mảng bytes
        /// </summary>
        public static void OpenFileFromBytes(byte[] data, string filePath)
        {
            try
            {
                if (data == null) return;
                string tempFolder = FrameworkParams.TEMP_FOLDER;
                if (!System.IO.Directory.Exists(tempFolder)) tempFolder = RadParams.RUNTIME_PATH;
                filePath = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                System.IO.FileInfo fi = new System.IO.FileInfo(tempFolder + "\\" + filePath);
                if (fi.Exists) fi.Attributes = System.IO.FileAttributes.Normal;

                if (HelpFile.BytesToFile((byte[])data, fi.FullName))
                {
                    //fi.Attributes = System.IO.FileAttributes.Hidden;
                    HelpFile.OpenFile(fi.FullName);
                }
            }
            catch
            {
                HelpMsgBox.ShowNotificationMessage("Tập tin hiện có vấn đề, vui lòng truy cập lại sau!");
            }
        }


        #region Kiểm tra 1 tập tin có đang mở không.


        public static bool? isUsedByOtherProgram(string FullFileName)
        {
            bool? flag = null;
            try
            {
                string temFile = Path.GetTempFileName();
                File.Move(FullFileName, temFile);                
                //Không đang sử dụng
                try {               
                    //Trả lại tập tin đã di chuyển
                    File.Move(temFile, FullFileName);
                    flag = false;
                }
                catch {
                    //Trả lại 1 lần nữa với phần mở rộng là .plagain
                    try { File.Move(temFile, FullFileName + ".plagain"); }
                    catch{}
                    flag = false;
                }
            }
            catch
            {
                //Đang sử dụng
                flag = true;                
            }
            return flag;
        }        
        #endregion
    }


    public class HelpPLConfigFile : ConfigFile{

    }
}
