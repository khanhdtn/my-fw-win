using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class SupportDeveloper
    {
        public SupportDeveloper()
        {
            new DeveloperKey();
        }
    } 

    public class DeveloperKey
    {
        public DeveloperKey()
        {
            PLHotKey.AddHotKeyItem(new HotKeyItem(ProtocolVN.Framework.Win.ModifierKeys.Control, Keys.F8, ShowReportSQL));
            PLHotKey.AddHotKeyItem(new HotKeyItem(ProtocolVN.Framework.Win.ModifierKeys.Control, Keys.F9, EndWaiting));
            PLHotKey.AddHotKeyItem(new HotKeyItem(ProtocolVN.Framework.Win.ModifierKeys.Control, Keys.F10, ShowLastestException));
            PLHotKey.AddHotKeyItem(new HotKeyItem(ProtocolVN.Framework.Win.ModifierKeys.Control, Keys.F11, ShowMissingSecurity));
            PLHotKey.AddHotKeyItem(new HotKeyItem(ProtocolVN.Framework.Win.ModifierKeys.Control, Keys.F12, SwitchLDAP));
            PLHotKey.AddHotKeyItem(new HotKeyItem(ProtocolVN.Framework.Win.ModifierKeys.Control, Keys.F7,
                delegate(object input)
                {
                    FrameworkParams.MainForm.Show();
                    return null;
                }));
        }


        /// <summary>
        /// Thông tin ngoại lệ : nhấn Alt - F10
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Object ShowLastestException(Object obj)
        {
            PLDebug.ShowExceptionInfo();
            return null;
        }

        /// <summary>
        /// Xem thông tin Security : nhấn Alt - F11
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Object ShowMissingSecurity(Object obj)
        {
            StringBuilder buider = new StringBuilder("");
            foreach (Permission per in PermissionStore.MissFeatures)
            {
                buider.AppendLine(per.SQL());
            }
            PLDebug.ShowString(buider.ToString());
            return null;
        }

        /// <summary>
        /// Liệt kê danh sách các câu lệnh SQL cần thực hiện để phân quyền REPORT 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Object ShowReportSQL(Object obj)
        {
            PLDebug.ShowString(ReportStore.ToSQL());
            return null;
        }

        private static Object SwitchLDAP(Object obj)
        {
            if (PLMessageBox.ShowConfirmMessage("Bạn có muốn sử dụng LDAP ?") == DialogResult.Yes)
            {
                FrameworkParams.UsingLDAP = true;
                //if (PLMessageBox.ShowConfirmMessage("Switch LDAP Out?") == DialogResult.Yes)
                //{
                //    FrameworkParams.UsingLDAP = true;
                //    LDAPConfig.I.LDAP_PATH = "LDAP://protocolvn.net";
                //}
                //else
                //{
                //    FrameworkParams.UsingLDAP = true;
                //    LDAPConfig.I.LDAP_PATH = "LDAP://protocolvn.com";
                //}
            }
            else
            {
                FrameworkParams.UsingLDAP = false;                
            }
            return null;
        }

        private static Object EndWaiting(Object obj)
        {
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            return null;
        }        
    }
}
