using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using ProtocolVN.Framework.Core;
using DevExpress.Utils;

namespace ProtocolVN.Framework.Win
{
    sealed class PLMenu : IFWMenu
    {
        private string CreateCommonApp()
        {
            StringBuilder str = new StringBuilder();
            string PLHELP = GlobalConst.MENU_HELP_MENU_ITEM_ID;
            MenuBuilder.CreateItem(str, "APPLICATION", "Ứng dụng thường dùng", PLHELP, true, "", false, true, "", false, "", "", false);
            MenuBuilder.CreateItem(str, "NOTEPAD", "Notepad", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunNotePad", false, false, "mnsNotePad.png", false, "", "", false);
            MenuBuilder.CreateItem(str, "WORDPAD", "Wordpad", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunWordPad", false, false, "mnsNotePad.png", false, "", "", false);
            MenuBuilder.CreateItem(str, "WINWORD", "Winword", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunWord", false, false, "mnsWord.png", false, "", "", false);
            MenuBuilder.CreateItem(str, "EXCEL", "Excel", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunExcel", false, false, "mnsExcel.png", false, "", "", false);
            MenuBuilder.CreateItem(str, "CALC", "Calc", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunCalc", false, false, "mnsCalc.png", false, "", "", false);
            MenuBuilder.CreateItem(str, "EXPLORER", "Window Explorer", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunExplorer", false, false, "mnsExplorer.png", false, "", "", false);

            return str.ToString();
        }
        
        /// <summary>
        /// MenuPage Giúp đỡ
        /// </summary>
        /// <returns></returns>
        public string CreateHelpMenu()
        {
            StringBuilder str = new StringBuilder();
            string PLHELP = GlobalConst.MENU_HELP_MENU_ITEM_ID;
            str.Append(MenuBuilder.CreateRootItem(PLHELP, "Giúp đỡ", ""));
            if (RadParams.HELP_FILE != null)
                MenuBuilder.CreateItem(str, SPECIAL_MENU_ITEM.GUIDE.ToString(), "Hướng dẫn sử dụng", PLHELP, true, "", true, false, "mnbHDanSuDung.png", false, "", "", true);
            MenuBuilder.CreateItem(str, "PLHELP.1", "Cập nhật phiên bản mới", PLHELP, true, typeof(frmFWLiveUpdateDirect).FullName, false, true, "0", false, "", "", true);
            //MenuBuilder.CreateItem(str, "PLHELP.1", "Tải chương trình mới", PLHELP, true, "ProtocolVN.Framework.Win.frmLiveUpdate", false, false, "mnbTChuongTrinhMoi.png", false, "", "", true);
            //MenuBuilder.CreateItem(str, SPECIAL_MENU_ITEM.UPDATEPROGRAM.ToString(), "Cập nhật chương trình", PLHELP, true, "", true, false, "mnbCNhatChuongTrinh.png", false, "", "", true);
            MenuBuilder.CreateItem(str, "PLHELP.3", "Về tác giả", PLHELP, true, "ProtocolVN.Framework.Win.frmPLAbout", false, true, "mnbVeTacGia.png", false, "", "", true);
            //MenuBuilder.CreateItem(str, "PLHELP.4", "Quản lý bổ trợ", PLHELP, true, "ProtocolVN.Framework.Win.frmPluginManager", false, false, "mnbBoTro.png", false, "", "", true);
            str.Append(CreateCommonApp());
            str.Append(FrameworkParams.appendPLHELPMenu);
            return str.ToString();
        }

        /// <summary>
        /// Menu nằm trên application menu
        /// </summary>
        /// <returns></returns>
        public string CreateRibbonItem()
        {
            StringBuilder str = new StringBuilder();
            str.Append(MenuBuilder.CreateRootItem("CHANGEPASS", "Thay đổi mật khẩu", typeof(frmChangePwd).FullName, "mnbTDoiMatKhau.png", ""));
            str.Append(MenuBuilder.CreateRootItem("CLOSEALL", "Đóng tất cả", "mnbDongTatCa.png", ""));
            MenuBuilder.CreateItem(str, "LOCK", "Khóa chương trình", "1", true, typeof(frmFWLockApplication).FullName, false, false, "mnbKhoaChuongTrinh.png", false, "", "", true);
            str.Append(MenuBuilder.CreateRootItem("LOGOUT", "Đăng xuất", "mnbDangXuat.png", ""));
            return str.ToString();
        }

        //Menu nằm trên Quick Access
        public string CreateQuickAccess()
        {
            StringBuilder str = new StringBuilder();
            MenuBuilder.CreateItem(str, "FW7", "Khóa chương trình", "1", true, typeof(frmFWLockApplication).FullName, false, false, "mnbKhoaChuongTrinh.png", false, "", "", true);
            return str.ToString();
        }

        

        /// <summary>
        /// Menu dùng để chứa các PLUGIN
        /// </summary>
        /// <returns></returns>
        public string CreatePluginMenu()
        {
            string ret = ProtocolVN.Framework.Win.HelpPlugin.CreateMenuPlugin();
            if (ret == null || ret == "")
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(
                @"<Item>
                    <ID>"+GlobalConst.MENU_PLUGIN_MENU_ITEM_ID+@"</ID>
                    <Name>Bổ trợ</Name>
                    <Parents>1</Parents>
                    <Enable>Y</Enable>
                    <Form></Form>
                    <MDI>N</MDI>
                    <Sep></Sep>
                    <ImageName></ImageName>
                    <Waiting></Waiting>
                    <HelpPage></HelpPage>
                    <ToolTip></ToolTip>
                 </Item>
            ");

            builder.Append(ret);

            return builder.ToString();          
        }

        public string CreateHomePage()
        {
            StringBuilder str = new StringBuilder();
            str.Append(MenuBuilder.CreateRootItem(GlobalConst.MENU_HOMEPAGE_MENU_ITEM_ID, "Thường dùng", ""));
            if(FrameworkParams.HomePageMenu!=null) str.Append(FrameworkParams.HomePageMenu);
            return str.ToString();
        }

        #region IFWMenu Members

        public string CreateSysMenu()
        {
            return "";
        }

        public string CreateToolMenu()
        {
            return "";
        }

        #endregion



        #region IFWMenu Members


        public string CreateDevPage()
        {
            return "";
        }

        #endregion
    }
}
