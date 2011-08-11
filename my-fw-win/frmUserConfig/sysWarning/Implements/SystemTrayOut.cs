using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public class SystemTrayOut : PLOut
    {
        private static SystemTrayOut tray;
        public static SystemTrayOut Instance()
        {
            if (tray == null)
            {
                tray = new SystemTrayOut("PROTOCOL Software Co.Ltd");
            }
            return tray;
        }
        public static void Dispose()
        {
            if (tray != null) tray.close(null);
        }


        private NotifyIcon trayIcon;
        private String title;
        
        private SystemTrayOut(string title)
        {
            this.title = title;
        }

        #region PLOutput Members

        public object open(object param)
        {
            this.trayIcon = new NotifyIcon();
            if (RadParams.ApplicationIcon != null)
                this.trayIcon.Icon = RadParams.ApplicationIcon;
            this.trayIcon.Text = title;
            this.trayIcon.Visible = false;
            
            return "NOTHING";
        }

        public object write(string title, string text)
        {
            if (this.trayIcon == null) 
                open(null);
            if (!this.trayIcon.Visible) 
                this.trayIcon.Visible = true;
            if (text != null) 
                this.trayIcon.ShowBalloonTip(0, title, text, ToolTipIcon.Info);
            this.trayIcon.Text = text;
            
            return "NOTHING";
        }

        public object close(object param)
        {
            //if (trayIcon != null)
            //{
            //    trayIcon.Visible = false;
            //    trayIcon.Dispose();
            //    trayIcon = null;
            //}

            return "NOTHING";
        }

        #endregion
    }
}
