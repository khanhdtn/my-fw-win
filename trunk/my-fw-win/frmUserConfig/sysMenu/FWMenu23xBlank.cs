using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using ProtocolVN.Framework.Core;
using DevExpress.Utils;
using ProtocolVN.DanhMuc;
using ProtocolVN.Plugin.NoteBook;
using ProtocolVN.Plugin.ImpExp;

namespace ProtocolVN.Framework.Win
{
    sealed class FWMenu23xBlank : IFWMenu
    {
        #region IFWMenu Members

        public string CreateRibbonItem()
        {
            StringBuilder str = new StringBuilder();
            #region Danh sách các item bắt buộc
            str.Append(MenuBuilder.CreateRootItem("CHANGEPASS", "Thay đổi mật khẩu", typeof(frmChangePwd).FullName, "2", ""));
            //str.Append(MenuBuilder.CreateRootItem("CLOSEALL", "Đóng tất cả màn hình", "3", ""));
            //MenuBuilder.CreateItem(str, "LOCK", "Khóa chương trình", "1", true, typeof(frmLockApplication).FullName, false, false, "4", false, "", "", true);
            str.Append(MenuBuilder.CreateRootItem("LOGOUT", "Đăng xuất", "5", ""));
            #endregion
            #region Danh sách item tùy chọn
            //...
            #endregion 
            return str.ToString();
        }

        public string CreateQuickAccess()
        {            
            StringBuilder str = new StringBuilder();
            #region Danh sách các item bắt buộc
            MenuBuilder.CreateItem(str, "LOCK", "Khóa chương trình", "1", true, typeof(frmFWLockApplication).FullName, false, false, "35", false, "", "", true);
            str.Append(MenuBuilder.CreateRootItem("CLOSEALL", "Đóng tất cả màn hình", "62", ""));
            MenuBuilder.CreateItem(str, "CALC", "Máy tính bỏ túi", "1", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunCalc", false, false, "64", false, "", "", false);
            MenuBuilder.CreateItem(str, "NOTE", "Ghi chú", "1", true, typeof(frmFWNote).FullName, false, false, "27", false, "", "", false);

            MenuBuilder.CreateItem(str, "APPLICATION", "Ứng dụng thường dùng", "1", true, "", false, false, "63", false, "", "", false);
            MenuBuilder.CreateItem(str, "NOTEPAD", "Notepad", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunNotePad", false, false, "37", false, "", "", false);
            MenuBuilder.CreateItem(str, "WORDPAD", "Wordpad", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunWordPad", false, false, "36", false, "", "", false);
            MenuBuilder.CreateItem(str, "WINWORD", "Winword", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunWord", false, false, "41", false, "", "", false);
            MenuBuilder.CreateItem(str, "EXCEL", "Excel", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunExcel", false, false, "40", false, "", "", false);            
            MenuBuilder.CreateItem(str, "EXPLORER", "Window Explorer", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunExplorer", false, false, "39", false, "", "", false);
            #endregion
            #region Danh sách item tùy biến.
            //...
            #endregion
            return str.ToString();
        }

        
        #region Menu Item Ribbon Menu
        public string CreateHomePage()
        {
            StringBuilder str = new StringBuilder();
            return str.ToString();
        }

        public string CreateHelpMenu()
        {
            StringBuilder str = new StringBuilder();

            #region Danh sách các item bắt buộc của Help Page
            string PLHELP = GlobalConst.MENU_HELP_MENU_ITEM_ID;
            str.Append(MenuBuilder.CreateRootItem(PLHELP, "Giúp đỡ", ""));
            if (RadParams.HELP_FILE != null) MenuBuilder.CreateItem(str, SPECIAL_MENU_ITEM.GUIDE.ToString(), "Hướng dẫn sử dụng", PLHELP, true, "", true, false, "mnbHDanSuDung.png", false, "", "", true);


            if (FrameworkParams.IsUpdateVersionAtLocalServer == false)
            {
                MenuBuilder.CreateItem(str, "CAP_NHAT_PHIEN_BAN_MOI", "Cập nhật phiên bản mới", PLHELP, true, typeof(frmFWLiveUpdateDirect).FullName, false, false, "0", false, "", "", true);
            }
            else
            {
                MenuBuilder.CreateItem(str, "CAP_NHAT_PHIEN_BAN_MOI", "Cập nhật phiên bản mới", PLHELP, true, "", false, false, "0", false,
                    "", "", true);
                MenuBuilder.CreateItem(str, "CAP_NHAT_PHIEN_BAN_MOI_2", "Cập nhật phiên bản mới vào máy chủ nội bộ",
                    "CAP_NHAT_PHIEN_BAN_MOI", true, typeof(frmFWLiveUpdate).FullName, false, false, "0", false, "", "", true);
                MenuBuilder.CreateItem(str, SPECIAL_MENU_ITEM.UPDATEPROGRAM.ToString(), "Cập nhật phiên bản mới từ máy chủ nội bộ",
                    "CAP_NHAT_PHIEN_BAN_MOI", true, "", false, false, "0", false, "", "", true);

            }

            MenuBuilder.CreateItem(str, "PLHELP.3", "Thông tin phần mềm", PLHELP, true, typeof(frmPLAbout).FullName, false, false, "1", false, "", "", true);
            #endregion

            #region Danh sách các item tùy biến
            if(FrameworkParams.appendPLHELPMenu != String.Empty)
                str.Append(FrameworkParams.appendPLHELPMenu);
            #endregion
            return str.ToString();
        }

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
                    <ID>" + GlobalConst.MENU_PLUGIN_MENU_ITEM_ID + @"</ID>
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

        public string CreateSysMenu()
        {
            StringBuilder str = new StringBuilder();
            return str.ToString();
        }

        public string CreateToolMenu()
        {
            StringBuilder str = new StringBuilder();
            return str.ToString();
        }

        public string CreateDevPage()
        {
            StringBuilder str = new StringBuilder();
            string PLDevPage = GlobalConst.MENU_DEVELOP_MENU_ITEM_ID;
            str.Append(MenuBuilder.CreateRootItem(PLDevPage, "Đang phát triển", ""));
            #region Danh sách các Item bắt buộc
            #endregion

            #region Danh sách các Item tùy biến
            if (FrameworkParams.appendPLDEVMenuItems != String.Empty)
            {
                str.Append(FrameworkParams.appendPLDEVMenuItems);
            }
            #endregion
            return str.ToString();
        }

        #endregion
        #endregion
    }
}
