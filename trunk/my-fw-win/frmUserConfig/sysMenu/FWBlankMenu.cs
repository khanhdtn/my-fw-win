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
    class FWBlankMenu : IFWMenu
    {
        #region IFWMenu Members
        public string CreateHelpMenu()
        {
            StringBuilder str = new StringBuilder();
            
            return str.ToString();
        }


        public string CreateRibbonItem()
        {
            StringBuilder str = new StringBuilder();
            str.Append(MenuBuilder.CreateRootItem("CHANGEPASS", "Thay đổi mật khẩu", typeof(frmChangePwd).FullName, "2", ""));
            str.Append(MenuBuilder.CreateRootItem("CLOSEALL", "Đóng tất cả", "3", ""));
            MenuBuilder.CreateItem(str, "LOCK", "Khóa chương trình", "1", true, typeof(frmFWLockApplication).FullName, false, false, "4", false, "", "", true);
            str.Append(MenuBuilder.CreateRootItem("LOGOUT", "Đăng xuất", "5", ""));
            return str.ToString();
        }

        public string CreateQuickAccess()
        {            
            StringBuilder str = new StringBuilder();
            MenuBuilder.CreateItem(str, "FW7", "Khóa chương trình", "1", true, typeof(frmFWLockApplication).FullName, false, false, "35", false, "", "", true);
            MenuBuilder.CreateItem(str, "APPLICATION", "Ứng dụng thường dùng", "1", true, "", false, false, "42", false, "", "", false);
            MenuBuilder.CreateItem(str, "NOTEPAD", "Notepad", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunNotePad", false, false, "37", false, "", "", false);
            MenuBuilder.CreateItem(str, "WORDPAD", "Wordpad", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunWordPad", false, false, "36", false, "", "", false);
            MenuBuilder.CreateItem(str, "WINWORD", "Winword", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunWord", false, false, "41", false, "", "", false);
            MenuBuilder.CreateItem(str, "EXCEL", "Excel", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunExcel", false, false, "40", false, "", "", false);
            MenuBuilder.CreateItem(str, "CALC", "Calc", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunCalc", false, false, "38", false, "", "", false);
            MenuBuilder.CreateItem(str, "EXPLORER", "Window Explorer", "APPLICATION", true, "ProtocolVN.Framework.Win.QuickAccessMethodExec?param=RunExplorer", false, false, "39", false, "", "", false);
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
            if (FrameworkParams.HomePageMenu != null) str.Append(FrameworkParams.HomePageMenu);
            return str.ToString();
        }

        #endregion

        #region IFWMenu Members

        public string CreateSysMenu()
        {
            StringBuilder str = new StringBuilder();
            string PLSYSTEM = GlobalConst.MENU_SYSTEM_MENU_ITEM_ID;
            str.Append(MenuBuilder.CreateRootItem(PLSYSTEM, "Hệ thống", ""));
            MenuBuilder.CreateItem(str, "PLSYSTEM000", "Khai báo danh mục", PLSYSTEM, true, FWFormName.frmCategory, true, false, "21", false, "", "", true);
            str.Append(MenuBuilder.CreateItem("PLSYSTEM001", "Tham số hệ thống", PLSYSTEM, true, HelpFURL.FURL(FrameworkParams.defineAppParamExec, "ShowAppParamForm"), false, false, "22", false, "", "", true));
            MenuBuilder.CreateItem(str, "PLSYSTEM002", "Tham số báo cáo", PLSYSTEM, true, typeof(frmAppReportParams).FullName, false, false, "7", false, "", "", true);
            MenuBuilder.CreateItem(str, "PLSYSTEM0021", "Cấu hình máy chủ", PLSYSTEM, true, typeof(frmConfigServer).FullName, false, false, "7", false, "", "", true);
            MenuBuilder.CreateItem(str, "PLSYSTEM003", "Quản lý người dùng", PLSYSTEM, true, typeof(frmTreeUserManExt).FullName, true, false, "8", false, "", "", true);
            str.Append(MenuBuilder.CreateItem("PLTT_TOCHUC", "Thông tin tổ chức", PLSYSTEM, true, "", true, false, "9", false, "", ""));
            MenuBuilder.CreateItem(str, "PLTT_TOCHUC001", "Hồ sơ công ty", "PLTT_TOCHUC", true, typeof(frmCompanyInfo).FullName, false, false, "44", false, "", "", true);
            str.Append(MenuBuilder.CreateItem("PLTT_TOCHUC002", "Sơ đồ tổ chức", "PLTT_TOCHUC", true, HelpFURL.FURL(typeof(FWMethodExec).FullName, "ShowSystemCategory"), false, false, "43", false, "", "", true));
            MenuBuilder.CreateItem(str, "PLSYSTEM004", "Nâng cấp chương trình", PLSYSTEM, true, typeof(frmFWLiveUpdate).FullName, false, false, "0", false, "", "Nâng cấp chương trình là tải phiên bản mới nhất về máy chủ, sau đó máy con sẽ cập nhật về để có chương trình mới nhất.", true);
            MenuBuilder.CreateItem(str, "PLSYSTEM005", "Nhật ký sử dụng", PLSYSTEM, true, typeof(frmUserLog).FullName, true, false, "0", false, "", "", true);
            MenuBuilder.CreateItem(str, "PLSYSTEM006", "Sao lưu phục hồi", PLSYSTEM, true, typeof(frmBackupRestore).FullName, false, false, "0", false, "", "", true);
            str.Append(FrameworkParams.appendPLSYSTEMMenu);

            return str.ToString();
        }

        public string CreateToolMenu()
        {
            StringBuilder str = new StringBuilder();
            string PLTOOL = GlobalConst.MENU_TOOL_MENU_ITEM_ID;
            str.Append(MenuBuilder.CreateRootItem(PLTOOL, "Tiện ích", ""));
            MenuBuilder.CreateItem(str, "PLTOOL000", "Truy cập nhanh", PLTOOL, true, typeof(frmFWFURLBrowser).FullName, false, false, "21", false, "", "Trình duyệt giúp cho đến được thể hiện cần thao tác.", false);            
            //MenuBuilder.CreateItem(str, "PLTOOL001", "PL-Clipboard", PLTOOL, true, FWFormName.frmCategory, true, false, "21", false, "", "Cho phép sao chép dữ liệu giữa các màn hình.", true);
            //MenuBuilder.CreateItem(str, "PLTOOL002", "PL-Query", PLTOOL, true, FWFormName.frmCategory, true, false, "21", false, "", "Cho phép sao chép dữ liệu giữa các màn hình.", true);
            //MenuBuilder.CreateItem(str, "PLTOOL003", "Trên nhân viên", "PLTOOL002", true, FWFormName.frmCategory, true, false, "21", false, "", "Cho phép sao chép dữ liệu giữa các màn hình.", true);
            MenuBuilder.CreateItem(str, "PLTOOL004", "Sổ địa chỉ", PLTOOL, true, typeof(frmAddressBook).FullName, true, false, "21", false, "", "Sổ địa chỉ dùng để chứa các địa chỉ cá nhân của người dùng.", false);
            MenuBuilder.CreateItem(str, "PLTOOL005", "Sổ ghi chú", PLTOOL, true, typeof(frmStickiesMain).FullName + "?param=RunStickies", false, false, "21", false, "", "Sổ ghi chú dùng để chứa các ghi chú cá nhân của người dùng.", false);
            //MenuBuilder.CreateItem(str, "PLTOOL006", "Thảo luận nội bộ", PLTOOL, true, typeof(ChatMethodExec).FullName + "?param=RunMessenger", true, false, "21", false, "", "", true);

            MenuBuilder.CreateItem(str, "PLTOOL007", "Hệ thống báo cáo", PLTOOL,
                true, typeof(frmBaseReport).FullName, true, true, "", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL008", "Báo cáo dạng Pivot", PLTOOL,
            //    true, typeof(DevExpress.XtraPivotGrid.Demos.frmOLAPMain).FullName, false, false, "", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL009", "Báo cáo dạng Chart", PLTOOL,
            //    true, typeof(DevExpress.XtraCharts.Demos.frmOLAPChartMain).FullName, false, false, "", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL010", "Quản lý công việc", PLTOOL,
            //    true, typeof(DevExpress.XtraScheduler.Demos.frmSchedulerMain).FullName, false, false, "", false, "", "", true);


            MenuBuilder.CreateItem(str, "PLTOOL011", "Khai thác dữ liệu", PLTOOL,
                true, typeof(PLFilter).FullName, true, false, "", false, "", "", true);

            MenuBuilder.CreateItem(str, "PLTOOL012", "Nhập dữ liệu từ Excel", PLTOOL,
                            true, typeof(frmImport).FullName, true, false, "", false, "", "", true);

            str.Append(FrameworkParams.appendPLTOOLMenu);

            return str.ToString();   
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
