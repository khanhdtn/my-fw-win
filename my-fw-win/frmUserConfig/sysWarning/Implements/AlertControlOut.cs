using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Alerter;

namespace ProtocolVN.Framework.Win
{
    public class AlertControlOut : PLOut
    {
        AlertControl alert = null;
        #region PLOut Members

        public object open(object param)
        {
            alert = new AlertControl();
            return "NOOP";
        }

        public object write(string title, string text)
        {
            alert.Show(FrameworkParams.MainForm, title, text);
            return "NOOP";
        }

        public object close(object param)
        {
            alert.Dispose();
            alert = null;
            return "NOOP";
        }

        #endregion
    }
}
