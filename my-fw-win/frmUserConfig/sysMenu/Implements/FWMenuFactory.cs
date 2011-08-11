using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    interface IFWMenu {
        string CreateSysMenu();
        string CreateToolMenu();
        string CreateHelpMenu();
        string CreateRibbonItem();
        string CreateQuickAccess();
        string CreatePluginMenu();
        string CreateHomePage();
        string CreateDevPage();
    }
    public enum FWMenu
    {
        FW21x,//Không dùng
        FW22x,//Không dùng
        FW23x,
        FWBlankMenu,
        FW23xBlank,
        FW23xDID
    }

    class FWMenuFactory 
    {
        private static IFWMenu getIFWMenu()
        {
            IFWMenu menu = null;
            switch (FrameworkParams.fwMenu)
            {
                case FWMenu.FW21x:
                    menu = new PLMenu();
                    break;
                case FWMenu.FW22x:
                    menu = new FWMenu22x();
                    break;
                case FWMenu.FW23x:
                    menu = new FWMenu23x();
                    break;
                case FWMenu.FW23xDID:
                    menu = new FWMenu23xDID();
                    break;
                case FWMenu.FW23xBlank:
                    menu = new FWMenu23xBlank();
                    break;
                case FWMenu.FWBlankMenu:
                    menu = new FWBlankMenu();
                    break;
            }
            return menu;
        }

        

        public static string CreateRibbonItem()
        {
            return getIFWMenu().CreateRibbonItem();
        }

        public static string CreateQuickAccess()
        {
            return getIFWMenu().CreateQuickAccess();
        }




        #region Ribbon Menu

        internal static string CreateHomePage()
        {
            return getIFWMenu().CreateHomePage();
        }

        internal static string CreateSystemPage()
        {
            return getIFWMenu().CreateSysMenu();
        }

        internal static string CreateToolPage()
        {
            return getIFWMenu().CreateToolMenu();
        }

        internal static string CreatePluginPage()
        {
            return getIFWMenu().CreatePluginMenu();
        }

        internal static String CreateHelpPage()
        {
            return getIFWMenu().CreateHelpMenu();
        }

        internal static string CreateDevPage()
        {
            return getIFWMenu().CreateDevPage();
        }

        #endregion
    }
}
