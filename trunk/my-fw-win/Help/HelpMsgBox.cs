using System;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ làm việc với MessageBox
    /// </summary>
    public class HelpMsgBox : PLMessageBox
    {
        #region Override from var
        /// <summary>
        /// Hộp thoại thông báo không xác nhận
        /// </summary>
        public static DialogResult ShowNotificationMessage(string vnMsg)
        {
            var box = GetNotificationMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.ShowDialog();
            return DialogResult.OK;
        }

        /// <summary>
        /// Hộp thoại thông báo không xác nhận
        /// </summary>
        public static DialogResult ShowNotificationMessage(string vnMsg, Control control)
        {
            var box = GetNotificationMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.ShowDialog(control);
            return DialogResult.OK;
        }

        /// <summary>
        /// Hộp thoại xác nhận
        /// </summary>
        public new static DialogResult ShowConfirmMessage(string vnMsg)
        {
            var box = GetConfirmMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.TopLevel = true;            
            box.ShowDialog();
            if (box.Kq == 1)
                return DialogResult.Yes;
            else
                return DialogResult.No;
        }

        /// <summary>
        /// Hộp thoại xác nhận
        /// </summary>
        public static DialogResult ShowConfirmMessage(string vnMsg, Control control)
        {
            var box = GetConfirmMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.ShowDialog(control);
            if (box.Kq == 1)
                return DialogResult.Yes;
            else
                return DialogResult.No;
        }
        public  static DialogResult ShowConfirmMessage(string vnMsg, Control control, bool showCancel)
        {
            if (!showCancel) return ShowConfirmMessage(vnMsg, control);
            return XtraMessageBox.Show(control, vnMsg, "Xác nhận", MessageBoxButtons.YesNoCancel,
                                       MessageBoxIcon.Question);

        }

        public static DialogResult ShowConfirmMessage(string vnMsg, bool showCancel)
        {
            if (!showCancel) return ShowConfirmMessage(vnMsg);
            return XtraMessageBox.Show(vnMsg, "Xác nhận", MessageBoxButtons.YesNoCancel,
                                       MessageBoxIcon.Question);

        }

        /// <summary>
        /// Hộp thoại thông báo lổi
        /// </summary>
        public new static DialogResult ShowErrorMessage(string vnMsg)
        {
            var box = GetErrorMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.ShowDialog();
            return DialogResult.OK;
        }

        /// <summary>
        /// Hộp thoại thông báo lổi
        /// </summary>
        public static DialogResult ShowErrorMessage(string vnMsg, Control control)
        {
            var box = GetErrorMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.ShowDialog(control);
            return DialogResult.OK;
        }

        /// <summary>
        /// Hiển thị hộp thoại thông báo lỗi nghiêm trọng. Nên thoát khỏi chương trình khi có thông báo này
        /// </summary>
        public static DialogResult ShowSystemErrorMessage(string vnMsg)
        {
            var box = GetSystemErrorMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.TopMost = true;
            box.ShowDialog();
            return DialogResult.OK;
        }

        /// <summary>
        /// Hiển thị hộp thoại thông báo lỗi nghiêm trọng. Nên thoát khỏi chương trình khi có thông báo này
        /// </summary>
        public static DialogResult ShowSystemErrorMessage(string vnMsg, Control control)
        {
            var box = GetSystemErrorMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();            
            box.ShowDialog(control);
            return DialogResult.OK;
        }

        /// <summary>
        /// Hiển thị hộp thoại thông báo lỗi nghiêm trọng. Nên thoát khỏi chương trình khi có thông báo này
        /// </summary>
        public static DialogResult ShowSystemErrorMessage(string vnMsg, XtraForm mainForm)
        {
            var box = GetSystemErrorMessage(vnMsg);
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            box.TopMost = true;
            box.ShowDialog(mainForm);
            return DialogResult.OK;
        }
        #endregion


        #region Message Box hỗ trợ Restart ứng dụng
        public static DialogResult _showNotificationMessage(string vnMsg, bool ShowRestart)
        {
            return  PLMessageBoxExt.ShowNotificationMessage(vnMsg, ShowRestart);
        }

        public static DialogResult _showErrorMessage(string vnMsg, bool ShowRestart)
        {
            return PLMessageBoxExt.ShowErrorMessage(vnMsg, ShowRestart);
        }

        public static DialogResult _showSystemErrorMessage(string vnMsg, bool ShowRestart)
        {
            return PLMessageBoxExt.ShowSystemErrorMessage(vnMsg, ShowRestart);
        }

        public static DialogResult _showDBErrorMessage(string vnMsg, bool ShowRestart)
        {
            return PLMessageBoxExt.ShowDBErrorMessage(vnMsg, ShowRestart);
        }

        #endregion

        #region MsgBox hỗ trợ nhập 1 dòng dữ liệu
        public static InputBoxResult _showMsgInput(string Prompt)
        {
            return PLInputBox.Show(Prompt);
        }

        public static InputBoxResult _showMsgInput(string Prompt, string Title)
        {
            return PLInputBox.Show(Prompt, Title);
        }

        public static InputBoxResult _showMsgInput(string Prompt, string Title, string Default)
        {
            return PLInputBox.Show(Prompt, Title, Default);
        }

        public static InputBoxResult _showMsgInput(string Prompt, string Title, string Default, int XPos, int YPos)
        {
            return PLInputBox.Show(Prompt, Title, Default, XPos, YPos);
        }
        #endregion

        /// <summary>Cho phép hiện thị một thông báo và thêm nhiều nút xử lý khác.
        /// </summary>
        public static IDialogAction ShowMessage(string title, string msg, Image img, IDialogAction[] actions)
        {
            return XMessageBox.ShowMessage( title,  msg,  img,  actions);
        }


        [Obsolete("Sử dụng lớp HelpWaiting")]
        public static void _showWaitingMsg(DelegationLib.CallFunction_NoIn_NoOut func)
        {
            WaitingMsg.LongProcess(func);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HelpMsgBox
            // 
            this.ClientSize = new System.Drawing.Size(349, 111);
            this.Name = "HelpMsgBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
      
    }
}
