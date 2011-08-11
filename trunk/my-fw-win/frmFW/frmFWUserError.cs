using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Threading;
using System.Text;
using System.Diagnostics;
namespace ProtocolVN.Framework.Win
{
    partial class frmFWUserError : XtraFormPublicPL
    {
        public frmFWUserError(ThreadExceptionEventArgs e)
        {
            InitializeComponent();
            this.ShowInTaskbar = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            this.SanPham.Text = FrameworkParams.ProductName;
            this.PhienBan.Text += HelpApplication.getVersion();
            this.Icon = FrameworkParams.ApplicationIcon;
        }

        private void frmLockApplication_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);            
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            try
            {
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
            }
            finally
            {
                this.Close();
                Application.ExitThread();
            }
        }

        //PHUOCNT TODO.
        private void btnSendError_Click(object sender, EventArgs e)
        {
            if (Debugger.IsAttached == false)
            {
                StringBuilder str = new StringBuilder("");
                int i = 1;
                foreach (Exception ex in PLException.GetLastestExceptions())
                {
                    str.Append("Line " + (i++) + ex.StackTrace);
                    str.AppendLine();
                }
                String[] TO = new String[2];
                TO[0] = "support@protocolvn.com";
                string strTieuDe = "[Su co] San pham: " + FrameworkParams.ProductName + " - Khach hang: " + FrameworkParams.CustomerName;
                if (HelpEmail.sendFromPLEmail(TO, strTieuDe, str.ToString()) == true)
                {
                    HelpMsgBox.ShowNotificationMessage("Sự cố của bạn đã được gửi đến công ty PROTOCOL.\n\r Công ty sẽ giải quyết sự cố này trong thời gian nhanh nhất.\n\r Chân thành cám ơn.");
                    FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Sự cố của bạn chưa được gửi đến công ty PROTOCOL.\n\r Vui lòng kiểm tra kết nối internet.");
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}