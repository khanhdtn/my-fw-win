using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using System.Threading;
using ProtocolVN.Framework.Core;
using System.Collections.Generic;

namespace ProtocolVN.Framework.Win {
	public partial class frmProgressWait : DevExpress.XtraEditors.XtraForm {
		public frmProgressWait(Form parent) {
			InitializeComponent();
		}

		public void SetProgressValue(int position) {
			progressBarControl1.Position = position;
			this.Update();
		}
	}



    public class frmProgressWaitHelp
    {
        private XtraForm parent;

        public frmProgressWaitHelp(XtraForm parent)
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
            frmProgressWait progressForm = new frmProgressWait(parent);
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
