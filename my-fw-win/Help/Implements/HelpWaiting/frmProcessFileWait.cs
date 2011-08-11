using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using ProtocolVN.Framework.Core;
using System.Collections.Generic;

namespace ProtocolVN.Framework.Win {
	public partial class frmProcessFileWait : DevExpress.XtraEditors.XtraForm {
		public frmProcessFileWait(Form parent) {
			InitializeComponent();
			this.ClientSize = new Size( 
				pictureBox1.Image.Width + this.DockPadding.All * 2, 
				pictureBox1.Image.Height + this.DockPadding.All * 2);
			if(parent != null) {
				Left = parent.Left + (parent.Width - Width) / 2;
				Top = parent.Top + (parent.Height - Height) / 2;
			}
		}
	}

    public class frmProcessFileWaitFormHelp
    {
        private XtraForm parent;

        public frmProcessFileWaitFormHelp(XtraForm parent)
        {
            this.parent = parent;
        }

        System.Threading.Thread thread;
        bool stop;
        void StartAction()
        {
            Thread.Sleep(400);
            if (stop)
                return;
            frmProcessFileWait progressForm = new frmProcessFileWait(parent);
            progressForm.Show();
            try
            {
                while (!stop)
                {
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
            catch
            {
            }
            progressForm.Dispose();
        }

        void EndAction()
        {
            stop = true;
            thread.Join();
        }

        public void Doing(DelegationLib.CallFunction_MulIn_NoOut func, List<object> input)
        {
            Cursor currentCursor = null;
            try
            {
                parent.Refresh();
                stop = false;
                thread = new Thread(new ThreadStart(StartAction));
                thread.Start();
                currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                func(input);
            }
            catch { }
            finally
            {
                EndAction();
                Cursor.Current = currentCursor;
            }


        }
    }
}
