using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    public partial class frmFWLockApplication : XtraFormPL, IPublicForm
    {
        public frmFWLockApplication()
        {
            //FrameworkParams.MainForm.Visible = false;
            InitializeComponent();
            this.username.Text = FrameworkParams.currentUser.username;
            this.SanPham.Text = FrameworkParams.ProductName;
            this.PhienBan.Text += HelpApplication.getVersion();
        }

        private bool isClose = false;
        private void simpleButton2_Click(object sender , EventArgs e)
        {
            if (DAUser.Instance.checkPassword(FrameworkParams.currentUser.username,
                            txtPassword.Text.Trim()))
            {
                //FrameworkParams.MainForm.Visible = true;
                isClose = true;
                this.Close();
                FrameworkParams.MainForm.Show();
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage("Xin vui lòng nhập lại mật khẩu.");
            }
        }

        private void frmLockApplication_FormClosing(object sender , FormClosingEventArgs e)
        {
            if(isClose == false)
                e.Cancel = true;
        }

        private void frmLockApplication_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);            
            this.ShowInTaskbar = true;
            FrameworkParams.MainForm.Hide();
        }
    }
}