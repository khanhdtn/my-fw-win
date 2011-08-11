using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Printing;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Lớp liệt kê danh sách các màn hình "hệ thống" đang có trong FW   
    /// </summary>
    public class FWFormName
    {
        public static String frmUserManExt = typeof(frmUserManExt).FullName;
        public static String frmTreeUserManExt = typeof(frmTreeUserManExt).FullName;
        public static String frmBackupRestore = typeof(frmBackupRestore).FullName;
        public static String frmLogin = typeof(frmLogin).FullName;
        public static String frmChangePwd = typeof(frmChangePwd).FullName;
        public static String frmConfigDB = typeof(frmConfigDB).FullName;
        public static String frmLicence = typeof(frmLicence).FullName;
        public static String frmPLAbout = typeof(frmPLAbout).FullName;
        public static String frmXPOption = typeof(frmXPOption).FullName;
        public static String frmLockApplication = typeof(frmFWLockApplication).FullName;
        public static String frmBaseReport = typeof(frmBaseReport).FullName;
        public static String PLBlankReport = typeof(PLBlankReport).FullName;
        public static String frmCategory = typeof(frmCategory).FullName;
        public static String frmIntro = typeof(frmIntro).FullName;
        public static String frmLiveUpdate = typeof(frmFWLiveUpdate).FullName;
        public static String frmPermissionFail = typeof(frmPermissionFail).FullName;
        public static String frmGridInfo = typeof(frmGridInfo).FullName;
        public static String noFRMQuickAccessMethodExec = typeof(QuickAccessMethodExec).FullName;
        public static String frmConfigServer = typeof(frmConfigServer).FullName;

        #region PREDICATE
        public static String frmOption = typeof(frmOption).FullName;
        public static String frmUserMan = typeof(frmUserMan).FullName;
        public static String frmUserManReadEdit = typeof(frmUserManReadEdit).FullName;
        public static String frmUserManReadEditCommit = typeof(frmUserManReadEditCommit).FullName;        
        #endregion

        #region Nam trong he thong khac
        public static String frmUserLog = typeof(frmUserLog).FullName;
        #endregion
    }
}
