using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using ProtocolVN.Framework.Core;
using DevExpress.Utils;

namespace ProtocolVN.Framework.Win
{
    public abstract class ProtocolMenu
    {
        public static bool isBarCode = false;

        /// <summary>Gắn các item vào trong menu hệ thống
        /// </summary>
        public abstract String CreateMenu();

        /// <summary>Gắn các item vào Application Menu
        /// </summary>
        public virtual String CreateRibbonAppMenu()
        {
            return "";
        }

        /// <summary>Gắn các item mặc định vào trong Trang 'Thường dùng'
        /// </summary>
        public virtual String CreateHomePageMenu()
        {
            return "";
        }

        /// <summary>Gắn các item mặc định vào trong QuickAccessMenu
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual String CreateQuickAccessMenu()
        {
            return "";
        }

        /// <summary>Gắn các item vào trang 'Hệ thống'
        /// </summary>
        public virtual String CreateSystemPageMenu()
        {
            return "";
        }
        
        /// <summary>Gắn các item vào trang 'Công cụ'
        /// </summary>
        public virtual String CreateToolPageMenu()
        {
            return "";
        }

        /// <summary>Gắn các item vào trang 'Giúp đỡ'
        /// </summary>
        public virtual String CreateHelpPageMenu()
        {
            return "";
        }

        /// <summary>Gắn các item vào trang 'Đang phát triển'
        /// Trang này chỉ này chỉ nhìn thấy khi sản phẩm ở dạng đang phát triển
        /// nó phù hợp cho gắn thử nghiệm các chức năng mới vào hệ thống.
        /// </summary>
        public virtual String CreateDevelopPageMenu()
        {
            return "";
        }
        
        public static void init(ProtocolMenu custom)
        {
            //Application menu
            FrameworkParams.RibbonMenu = custom.CreateRibbonAppMenu();
            //Quick Access
            FrameworkParams.QuickAccessMenu = custom.CreateQuickAccessMenu();
            //Ribbon Page            
            FrameworkParams.HomePageMenu = custom.CreateHomePageMenu();
            FrameworkParams.Menu = custom.CreateMenu();
            FrameworkParams.appendPLHELPMenu = custom.CreateHelpPageMenu();
            FrameworkParams.appendPLSYSTEMMenu = custom.CreateSystemPageMenu();
            FrameworkParams.appendPLTOOLMenu = custom.CreateToolPageMenu();
            FrameworkParams.appendPLDEVMenuItems = custom.CreateDevelopPageMenu();
        }
    }
}