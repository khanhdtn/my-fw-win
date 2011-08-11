using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    //PHUOCNC: Sau này xem xét chuyển thành OPTION
    public class GlobalConst
    {
        #region Sử dụng cho Menu
        public const string ID_ROOT = "ID_ROOT"; //Field được dùng để gom nhóm trong dữ liệu dạng cây
        public static string NULL_TEXT = "Chọn giá trị";
        #endregion


        #region Sử dụng cho MENU
        //ID dành cho item của "trang đầu"
        public const string MENU_HOMEPAGE_MENU_ITEM_ID = "HOMEPAGE"; 

        //ID dành cho item của "trang bổ trợ"
        public const string MENU_PLUGIN_MENU_ITEM_ID = "PLUGIN"; 

        //ID dành cho item của "trang giúp đỡ"
        public const string MENU_HELP_MENU_ITEM_ID = "PLHELP"; 

        //ID dành cho item của "trang hệ thống"
        public const string MENU_SYSTEM_MENU_ITEM_ID = "PLSYSTEM";

        //ID dành cho item của "trang tiện ích"
        public const string MENU_TOOL_MENU_ITEM_ID = "PLTOOL";

        //ID dành cho item của "trang hệ thống"
        public const string MENU_DEVELOP_MENU_ITEM_ID = "FWDEVELOP";
        #endregion


        public static string VISIBLE_TITLE = "Đang dùng";
    }
}
