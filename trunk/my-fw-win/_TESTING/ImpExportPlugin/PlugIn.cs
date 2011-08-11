using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace ProtocolVN.Plugin.ImpExp
{
    public class PlugIn : IPlugin
    {
        public static string PLUGIN_NAME = "Nhap/Xuat du lieu";
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
		
		public PlugIn()
		{
            m_strName = PLUGIN_NAME;
            m_strDes = "Xuat va nhap du lieu tu ben ngoai";
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
            return @"<Item>
                        <ID>MAIN_IMP_EXP_PLUGIN</ID>
                        <Name>Nhập dữ liệu v2</Name>
                        <Parents>PLUGIN</Parents>
                        <Enable>Y</Enable>
                        <Form>ProtocolVN.Plugin.ImpExp.frmImport</Form>
                        <MDI>N</MDI>
                        <Sep></Sep>
                        <ImageName></ImageName>
                        <Waiting></Waiting>
                        <HelpPage></HelpPage>
                        <ToolTip></ToolTip>
                    </Item>
                    <Item>
                        <ID>MAIN_IMP_EXP_PLUGIN_1</ID>
                        <Name>Nhập dữ liệu v1</Name>
                        <Parents>PLUGIN</Parents>
                        <Enable>Y</Enable>
                        <Form>ProtocolVN.Plugin.OldImpExp.frmOldImport</Form>
                        <MDI>N</MDI>
                        <Sep></Sep>
                        <ImageName></ImageName>
                        <Waiting></Waiting>
                        <HelpPage></HelpPage>
                        <ToolTip></ToolTip>
                    </Item>
                    ";
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

        public bool HookShowForm(DevExpress.XtraEditors.XtraForm frm)
        {
            return true;
        }

        #region IPlugin Members


        public bool HookHideForm(DevExpress.XtraEditors.XtraForm frm)
        {
            return true;
        }

        #endregion

        #region IPlugin Members


        public ProtocolVN.Framework.Win.IFormat GetFormat()
        {
            return null;
        }

        public ProtocolVN.Framework.Win.IPermission GetPermission()
        {
            return null;
        }

        #endregion
    }
}
