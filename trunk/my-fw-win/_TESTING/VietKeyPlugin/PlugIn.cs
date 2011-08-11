using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Plugin.VietInput
{
    public class PlugIn : IPlugin
    {
        #region Đoạn code bắt buộc không thay đổi
        private System.Reflection.Assembly getAssemblyHelp()
        {
            return System.Reflection.Assembly.GetCallingAssembly();
        }

        public System.Reflection.Assembly getAssembly()
        {
            return getAssemblyHelp();
        }
        #endregion

        private string m_strName;
        private string m_strDes;
		
		public PlugIn()
		{
            m_strName = "Bo go tieng viet";
            m_strDes = "Cho phep go tieng Viet ma khong can co Unikey, hay Vietkey";
		}
				
		public string Name
		{
            get{return m_strName;}
            set{m_strName=value;}
        }

        public string Description
        {
            get { return m_strDes; }
            set { m_strDes = value; }
        }

        public string BuildMenu()
        {
            return "";
        }

        public bool Install()
        {
            return true;
        }

        public bool UnInstall()
        {
            return true;
        }

        public bool CheckOK()
        {
            return true;
        }

        public bool InitPlugin()
        {
            return true;
        }

        public bool DisposePlugin()
        {
            return true;
        }

        #region IPlugin Members


        public bool HookShowForm(DevExpress.XtraEditors.XtraForm frm)
        {
            new PLVietKey(frm);
            PLVietKey.KieuGo = VietKeyHandler.InputType.Auto;
            return true;
        }

        #endregion

        #region IPlugin Members


        public bool HookHideForm(DevExpress.XtraEditors.XtraForm frm)
        {
            return true;
        }

        #endregion

        #region IPlugin Members


        public IFormat GetFormat()
        {
            return null;
        }

        public IPermission GetPermission()
        {
            return null;
        }

        #endregion
    }
}
