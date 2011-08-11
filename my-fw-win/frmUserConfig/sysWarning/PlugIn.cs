using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using System.Data;
using System.Data.Common;

namespace ProtocolVN.Plugin.WarningSystem
{
    /*
    CREATE TABLE DM_WARNING (
        ID    A_ID NOT NULL ,
        NAME  A_STR_SHORT 
    );*/

    public class PlugIn :ProtocolVN.Plugin.IPlugin
    {
        public static string PLUGIN_NAME = "He thong canh bao";
        #region Đoạn code bắt buộc không thay đổi
        private System.Reflection.Assembly getAssemblyHelp()
        {
            System.Reflection.Assembly ret = System.Reflection.Assembly.GetCallingAssembly();
            return ret;
        }

        public System.Reflection.Assembly getAssembly()
        {
            return getAssemblyHelp();
        }
        #endregion

        private string m_strName;
        private string m_strDes;
        private WarningSystemPluginEx warSystem = null;

        public PlugIn()
        {
            m_strName = PLUGIN_NAME;
            m_strDes = "Canh bao he thong";
        }

        public string Name
        {
            get { return m_strName; }
            set { m_strName = value; }
        }

        public string Description
        {
            get { return m_strDes; }
            set { m_strDes = value; }
        }

        public string BuildMenu()
        {
            if (FrameworkParams.currentUser.username == "admin")
                return @"<Item>
                    <ID>MAIN_WAR_SYS_PLUGIN</ID>
                    <Name>Quản lý cảnh báo</Name>
                    <Parents>PLUGIN</Parents>
                    <Enable>Y</Enable>
                    <Form>ProtocolVN.Plugin.WarningSystem.frmAssignUser</Form>
                    <MDI>N</MDI>
                    <Sep></Sep>
                    <ImageName></ImageName>
                    <Waiting></Waiting>
                    <HelpPage></HelpPage>
                    <ToolTip>Cấu hình cảnh báo cho nhieu người dùng.</ToolTip>
                  </Item>
                   <Item>
                    <ID>MAIN_ASS_WAR_PLUGIN</ID>
                    <Name>Cấu hình cảnh báo</Name>
                    <Parents>PLUGIN</Parents>
                    <Enable>Y</Enable>
                    <Form>ProtocolVN.Plugin.WarningSystem.frmWarningManager</Form>
                    <MDI>N</MDI>
                    <Sep></Sep>
                    <ImageName></ImageName>
                    <Waiting></Waiting>
                    <HelpPage></HelpPage>
                    <ToolTip>Cấu hình cảnh báo cho người dùng</ToolTip>
                  </Item>"
                    ;
            else
                return @"<Item>
                    <ID>MAIN_WAR_SYS_PLUGIN</ID>
                    <Name>Cấu hình cảnh báo</Name>
                    <Parents>PLUGIN</Parents>
                    <Enable>Y</Enable>
                    <Form>ProtocolVN.Plugin.WarningSystem.frmWarningManager</Form>
                    <MDI>N</MDI>
                    <Sep></Sep>
                    <ImageName></ImageName>
                    <Waiting></Waiting>
                    <HelpPage></HelpPage>
                    <ToolTip>Cấu hình cảnh báo cho người dùng</ToolTip>
                  </Item>";

        }

        public bool Install()
        {
            frmInstallDB installDb = new frmInstallDB(new InstallDB());
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, installDb);
            if (CheckOK())
                return true;
            else
                return false;
        }

        public bool UnInstall()
        {
            frmInstallDB installDb = new frmInstallDB(new InstallDB());
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, installDb);
            return true;
        }

        public bool CheckOK()
        {
            DataSet dsDmWar = DABase.getDatabase().LoadTable("FW_DM_WARNING");
            DataSet dsWarInfo = DABase.getDatabase().LoadTable("FW_WARNINGINFO");
            DataSet dsParam = DABase.getDatabase().LoadTable("FW_WARNING_PARAM");
            if (dsDmWar.Tables.Count < 1 || dsWarInfo.Tables.Count < 1 || dsParam.Tables.Count < 1)
                return false;
            return true;
        }

        public bool InitPlugin()
        {
            try
            {
                WarningSupport.StartWarning();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DisposePlugin()
        {
            warSystem.StopAll();
            return true;
        }

        public bool HookShowForm(DevExpress.XtraEditors.XtraForm frm)
        {
            //Lấy tên của các warning của form đưa vào biến names.
            string frmName = frm.GetType().Name;
            //warSystem = WarningSupport.StartWarning(frmName);
            return true;
        }

        #region IPlugin Members


        public IFormat GetFormat()
        {
            return null;
        }

        public IPermission GetPermission()
        {
            return null;
        }

        public bool HookHideForm(DevExpress.XtraEditors.XtraForm frm)
        {
            return true;
        }

        #endregion
    }
}
