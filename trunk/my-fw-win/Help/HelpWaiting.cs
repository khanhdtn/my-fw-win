using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Threading;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Xử lý việc thực hiện các màn hình tốn nhiều thời gian
    /// cần xuất hiện màn hình chờ. 
    /// Phát sinh: Trong xử lý backup, xử lý lưu chậm.
    /// </summary>
    public class HelpWaiting
    {
        

        #region Chờ khi xử lý file
        /// <summary>
        /// Hiển thị màn hình có ProgressBar tự xây dựng.
        /// </summary>
        public static void longProcess(XtraForm mainForm, ThreadStart process, long estimateTime)
        {
            if (estimateTime == -1)
                estimateTime = 1;
            mainForm.Cursor = Cursors.WaitCursor;
            TrialWaitingBox frm = new TrialWaitingBox(process);
            frm.estimateTime = estimateTime;
            frm.ShowDialog(mainForm);
            mainForm.Cursor = Cursors.Default;
        }

        public static void showProcessFile(XtraForm parent, DelegationLib.CallFunction_MulIn_NoOut action, List<object> input)
        {
            new frmProcessFileWaitFormHelp(parent).Doing(action, input);
        }

        public static void showProcessFile(XtraForm parent, DelegationLib.CallFunction_MulIn_NoOut action)
        {
            new frmProcessFileWaitFormHelp(parent).Doing(action, new List<object>());
        }
        #endregion

        #region Progress Form để chờ
        public static void showProgressForm(XtraForm parent, DelegationLib.CallFunction_MulIn_NoOut action, List<object> input)
        {
            new frmProgressWaitHelp(parent).Doing(action, input);
        }

        public static void showProgressForm(XtraForm parent, DelegationLib.CallFunction_MulIn_NoOut action)
        {
            new frmProgressWaitHelp(parent).Doing(action, new List<object>());
        }
        #endregion



        #region Message Chờ
        /// <summary>Thực hiện hành động trong action và hiện thông điệp chờ.
        /// Chú ý: Khi dùng các hành động trong action ko có phần MessageBox.
        /// </summary>
        public static void showMsgForm(XtraForm parent, DelegationLib.CallFunction_MulIn_NoOut action, List<object> input)
        {
            WaitingMsg msg = new WaitingMsg(parent);
            try
            {
                action(input);
            }
            catch { }
            finally
            {
                msg.Finish();
            }
        }

        /// <summary>Thực hiện hành động trong action và hiện thông điệp chờ.
        /// Chú ý: Khi dùng các hành động trong action ko có phần MessageBox.
        /// </summary>
        public static void showMsgForm(XtraForm parent, DelegationLib.CallFunction_MulIn_NoOut action)
        {
            WaitingMsg msg = new WaitingMsg(parent);
            try
            {
                action(new List<object>());
            }
            catch { }
            finally
            {
                msg.Finish();
            }
        }

        /// <summary>Thực hiện hành động trong action và hiện thông điệp chờ.
        /// Chú ý: Khi dùng các hành động trong action ko có phần MessageBox.
        /// </summary>
        public static void showMsgForm(XtraForm parent, DelegationLib.CallFunction_NoIn_NoOut action)
        {
            WaitingMsg.LongProcess(action);
        }
        #endregion
    }
}
