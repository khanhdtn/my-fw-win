using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using ProtocolVN.Framework.Core;
using System.Timers;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Xử lý chương trình 
    /// </summary>
    public class HelpExeExt : HelpExe
    {
        public enum ProcessName
        {
            WINDOWS_MEDIA_PLAYER,
            EXCEL,
            WINWORD,
            MS_VISUAL_STUDIO,
            FIREFOX
        }
        private static string GetExeName(ProcessName _TenChuongTrinh)
        {
            string str = String.Empty;
            switch (_TenChuongTrinh)
            {
                case ProcessName.EXCEL: return "excel";
                case ProcessName.WINDOWS_MEDIA_PLAYER: return str = "wmplayer";
                case ProcessName.WINWORD: return str = "winword";
                case ProcessName.MS_VISUAL_STUDIO: return str = "devenv";
                case ProcessName.FIREFOX: return str = "firefox";
            }           
            return "";
        }
        /// <summary>
        /// Đóng chương trình
        /// </summary>
        /// <param name="_TenChuongtrinh">TenChuongTrinh: Tên chương trình cần đóng</param>
        /// <returns></returns>
        public static bool ExitProgram(ProcessName _TenChuongtrinh)
        {
            try
            {
                Process[] pArry = Process.GetProcesses();
                foreach (Process p in pArry)
                {
                    string s = p.ProcessName;
                    s = s.ToLower();
                    if (s.CompareTo(GetExeName(_TenChuongtrinh)) == 0)
                    {
                        if (System.Windows.Forms.DialogResult.Yes == PLMessageBox.ShowConfirmMessage("Bạn có đồng ý đóng chương trình: " + _TenChuongtrinh.ToString()))
                        {
                            p.Kill();
                            return true;
                        }
                        else return false;
                    }
                }
            }
            catch (Exception)
            {
                HelpMsgBox.ShowNotificationMessage("");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Mở một tập tin
        /// </summary>
        /// <param name="_PathFile">Đường dẫn đến File</param>
        /// <returns></returns>
        public static bool OpenFile(ProcessName programe,string _PathFile)
        {
            try
            {            
                Process.Start(GetExeName(programe)+".exe","\""+System.IO.Path.GetFileName(_PathFile)+"\"");
                return true;
            }
            catch (Exception) { HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công");}
            return false;
        }
        public static bool OpenFile( string _PathFile)
        {
            try
            {
                Process.Start(_PathFile);
                return true;
            }
            catch (Exception) { HelpMsgBox.ShowNotificationMessage("Mở tập tin không thành công"); }
            return false;
        }
    }    
}
