using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Timers;
using System.Threading;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Form se tu dong sao 1 khoan thoi gian hien thi
    /// Su dung qua HelpXtraForm
    /// </summary>
    
    class TimerForm
    {
        private System.Timers.Timer clock = null;
        private XtraForm form;

        public TimerForm(XtraForm form)
        {
            this.form = form;
        }

        public void setTimer(int timeToClose)
        {
            this.clock = new System.Timers.Timer();
            this.clock.Elapsed += new ElapsedEventHandler(CloseDialog);
            this.clock.Interval = timeToClose;
            this.clock.Enabled = true;
        }
        private void CloseDialog(object source, ElapsedEventArgs e)
        {
            this.form.Close();
        }

        public void ShowDialog()
        {
            this.form.ShowDialog();
        }
    }
}
