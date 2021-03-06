using System.Threading;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ xây dựng WaitingBox trong Framwork
    /// </summary>
    public class TrialPLProgressBar
    //TODO PHUOC : Phước Kiểm tra lại cách xây dựng PLProgressBar
    {
        ProgressBarControl progressBar;
        ThreadStart ths;
        Thread th;
        int slowRate;
        double step;
        double limit1;
        double limit2;        

        public TrialPLProgressBar(ProgressBarControl progressBar, ThreadStart ths)
        {
            progressBar.Properties.Maximum = 100;
            progressBar.Properties.Minimum = 0;

            this.progressBar = progressBar;
            this.ths = ths;
            this.step = (progressBar.Properties.Maximum - progressBar.Properties.Minimum) / 100;
            this.limit1 = step * 80;
            this.limit2 = step * 95;
        }

        public bool Run(long secondTime)
        {
            long mili = secondTime * 1000;
            this.slowRate = (int)mili / (progressBar.Properties.Maximum - progressBar.Properties.Minimum);
            try
            {
                this.progressBar.Position = 0;
                th = new Thread(ths);
                th.Start();

                while (th.ThreadState != ThreadState.Stopped )
                {
                    progressBar.Increment((int)step);
                    progressBar.Update();

                    Thread.Sleep(slowRate);

                    if (progressBar.Position >= limit2){
                        step = 0;
                    }
                    else if (progressBar.Position >= limit1){
                        slowRate *= 2;
                    }
                }
                this.step = (progressBar.Properties.Maximum - progressBar.Properties.Minimum) / 50;
                slowRate = 5;
                while (HelpNumber.ParseInt32(progressBar.Text) != progressBar.Properties.Maximum)
                {
                    progressBar.Increment((int)step);
                    progressBar.Update();
                    Thread.Sleep(slowRate);
                }

                progressBar.Position = progressBar.Properties.Maximum;
                return true;
            }
            catch
            {
                return false;
            }
        }                                        
    }    
}