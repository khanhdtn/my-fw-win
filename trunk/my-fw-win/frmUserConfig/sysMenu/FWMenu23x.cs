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
    sealed class FWMenu23x : IFWMenu
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
            #region Danh sách các item bắt buộc
            str.Append(MenuBuilder.CreateRootItem(GlobalConst.MENU_HOMEPAGE_MENU_ITEM_ID, "Thường dùng", ""));
            #endregion
            #region Danh sách các item tùy biến
            if (FrameworkParams.HomePageMenu != String.Empty)
                str.Append(FrameworkParams.HomePageMenu);
            #endregion
            return str.ToString();
        }

        public string CreateHelpMenu()
        {
            StringBuilder str = new StringBuilder();
            //str.Append(CreateSysMenu());
            //str.Append(CreateToolMenu());

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
            //MenuBuilder.CreateItem(str, "PLHELP.4", "Yêu cầu - Góp ý", PLHELP, true, typeof(frmGopY).FullName, false, true, "1", false, "", "", true);
            //MenuBuilder.CreateItem(str, "PLHELP.5", "Hỗ trợ trực tuyến", PLHELP, true, typeof(ChatMethodExec).FullName + "?param=RunMessenger", true, false, "21", false, "", "", true);
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
            #region Danh sách Item bắt buộc
            string PLSYSTEM = GlobalConst.MENU_SYSTEM_MENU_ITEM_ID;
            str.Append(MenuBuilder.CreateRootItem(PLSYSTEM, "Hệ thống", ""));

            //MenuBuilder.CreateItem(str, "PLSYSTEM000", "Danh mục dữ liệu", PLSYSTEM, true, FWFormName.frmCategory, true, false, "21", false, "", "", true);
            str.Append(MenuBuilder.CreateItem("PLSYSTEM007", "Cấu hình nghiệp vụ", PLSYSTEM, true, HelpFURL.FURL(FrameworkParams.defineAppParamExec, "ShowCauHinhNghiepVu"), false, false, "22", false, "", "", true));
            //str.Append(MenuBuilder.CreateItem("PLSYSTEM007", "Cấu hình nghiệp vụ", PLSYSTEM, true, "", false, false, "22", false, "", "", true));
            //str.Append(MenuBuilder.CreateItem("PLSYSTEM001", "Tham số nghiệp vụ", "PLSYSTEM007", true, HelpFURL.FURL(FrameworkParams.defineAppParamExec, "ShowAppParamForm"), false, false, "22", false, "", "", true));
            //MenuBuilder.CreateItem(str, "PLSYSTEM002", "Tham số báo cáo", "PLSYSTEM007", true, typeof(frmAppReportParams).FullName, false, false, "7", false, "", "", true);
            
            //Cấu hình mã phiếu 
            str.Append(MenuBuilder.CreateItem("PLSYSTEM008", "Cấu hình hệ thống", PLSYSTEM, true, typeof(frmConfigServer).FullName, false, false, "22", false, "", "", true));
            //str.Append(MenuBuilder.CreateItem("PLSYSTEM008", "Cấu hình hệ thống", PLSYSTEM, true, "", false, false, "22", false, "", "", true));
            //MenuBuilder.CreateItem(str, "PLTT_TOCHUC001", "Thông tin đơn vị", "PLSYSTEM008", true, typeof(frmCompanyInfo).FullName, false, false, "44", false, "", "", true);
            //MenuBuilder.CreateItem(str, "PLSYSTEM0021", "Tham số hệ thống", "PLSYSTEM008", true, typeof(frmConfigServer).FullName, false, false, "7", false, "", "", true);

            if (ProtocolMenu.isBarCode)
            {
                MenuBuilder.CreateItem(str, "PLFWSYSBARCODE", "Cấu hình mã vạch", PLSYSTEM, true, typeof(frmBarcodeConfig).FullName, false, false, "7", false, "", "", true);
            }

            MenuBuilder.CreateItem(str, "PLSYSTEM003", "Quản lý người dùng", PLSYSTEM, true, typeof(frmTreeUserManExt).FullName, true, true, "8", false, "", "", true);

            //str.Append(MenuBuilder.CreateItem("PLTT_TOCHUC", "Thông tin tổ chức", PLSYSTEM, true, "", true, false, "9", false, "", ""));            
            //str.Append(MenuBuilder.CreateItem("PLTT_TOCHUC002", "Sơ đồ tổ chức", "PLTT_TOCHUC", true, HelpFURL.FURL(typeof(FWMethodExec).FullName, "ShowSystemCategory"), false, false, "43", false, "", "", true));
            //MenuBuilder.CreateItem(str, "PLSYSTEM004", "Nâng cấp chương trình", PLSYSTEM, true, typeof(frmLiveUpdate).FullName, false, false, "0", false, "", "Nâng cấp chương trình là tải phiên bản mới nhất về máy chủ, sau đó máy con sẽ cập nhật về để có chương trình mới nhất.", true);

            MenuBuilder.CreateItem(str, "PLSYSTEM005", "Nhật ký sử dụng", PLSYSTEM, true, typeof(frmUserLog).FullName, true, false, "0", false, "", "", true);

            MenuBuilder.CreateItem(str, "PLSYSTEM006", "Sao lưu phục hồi", PLSYSTEM, true, typeof(frmBackupRestore).FullName, false, true, "0", false, "", "", true);
            MenuBuilder.CreateItem(str, "PLTOOL012", "Nhập dữ liệu từ Excel", PLSYSTEM,
                            true, typeof(frmImport).FullName, true, false, "21", false, "", "", true);
            #endregion

            #region Danh sách các Item tùy biến
            if(FrameworkParams.appendPLSYSTEMMenu != String.Empty)
                str.Append(FrameworkParams.appendPLSYSTEMMenu);
            #endregion
            return str.ToString();
        }

        public string CreateToolMenu()
        {
            StringBuilder str = new StringBuilder();
            #region Danh sách các item bắt buộc
            string PLTOOL = GlobalConst.MENU_TOOL_MENU_ITEM_ID;
            //str.Append(MenuBuilder.CreateRootItem(PLTOOL, "Tiện ích", ""));
            //MenuBuilder.CreateItem(str, "PLTOOL000", "Truy cập nhanh", PLTOOL, true, typeof(frmFURLBrowser).FullName, false, false, "21", false, "", "Trình duyệt giúp cho đến được thể hiện cần thao tác.", false);            
            //MenuBuilder.CreateItem(str, "PLTOOL001", "PL-Clipboard", PLTOOL, true, FWFormName.frmCategory, true, false, "21", false, "", "Cho phép sao chép dữ liệu giữa các màn hình.", true);
            //MenuBuilder.CreateItem(str, "PLTOOL002", "PL-Query", PLTOOL, true, FWFormName.frmCategory, true, false, "21", false, "", "Cho phép sao chép dữ liệu giữa các màn hình.", true);
            //MenuBuilder.CreateItem(str, "PLTOOL003", "Trên nhân viên", "PLTOOL002", true, FWFormName.frmCategory, true, false, "21", false, "", "Cho phép sao chép dữ liệu giữa các màn hình.", true);
            //MenuBuilder.CreateItem(str, "PLTOOL004", "Sổ địa chỉ", PLTOOL, true, typeof(frmAddressBook).FullName, true, false, "21", false, "", "Sổ địa chỉ dùng để chứa các địa chỉ cá nhân của người dùng.", false);
            //MenuBuilder.CreateItem(str, "PLTOOL005", "Ghi chú", PLTOOL, true, typeof(frmStickiesMain).FullName + "?param=RunStickies", false, false, "21", false, "", "Sổ ghi chú dùng để chứa các ghi chú cá nhân của người dùng.", false);
            //MenuBuilder.CreateItem(str, "PLTOOL006", "Thảo luận nội bộ", PLTOOL, true, typeof(ChatMethodExec).FullName + "?param=RunMessenger", true, false, "21", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL007", "Hệ thống báo cáo", PLTOOL, true, typeof(frmBaseReport).FullName, true, true, "21", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL008", "Báo cáo dạng Pivot", PLTOOL,
            //    true, typeof(DevExpress.XtraPivotGrid.Demos.frmOLAPMain).FullName, false, false, "", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL009", "Báo cáo dạng Chart", PLTOOL,
            //    true, typeof(DevExpress.XtraCharts.Demos.frmOLAPChartMain).FullName, false, false, "", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL010", "Quản lý công việc", PLTOOL,
            //    true, typeof(DevExpress.XtraScheduler.Demos.frmSchedulerMain).FullName, false, false, "", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL011", "Khai thác dữ liệu", PLTOOL,
            //    true, typeof(PLFilter).FullName, true, false, "", false, "", "", true);

            //MenuBuilder.CreateItem(str, "PLTOOL012", "Nhập dữ liệu từ Excel", PLTOOL,
            //                true, typeof(frmImport).FullName, true, false, "21", false, "", "", true);
            #endregion

            #region Danh sách các item tùy biến
            if (FrameworkParams.appendPLTOOLMenu != String.Empty)
            {
                str.Append(FrameworkParams.appendPLTOOLMenu);
            }
            #endregion
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
