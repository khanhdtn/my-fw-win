using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    /// PHUOCC
    /// Cho phép sao lưu dữ liệu và phục hồi dữ liệu sao lưu.
    /// CAU LENH GBAK RAT KO CHỊU (VI UNICODE KHI DUNG CO THE BI LOI) TỐT HƠN HẾT LA COPY 
    /// FILE, ZIP LAI rồi bung ZIP
    public partial class frmBackupRestore : DevExpress.XtraEditors.XtraForm
    {
        public long UserId;        
        private bool Finish;
        BackupRestoreInfo BackRes;
        public static bool usingGBACK = false;

        public frmBackupRestore()
        {
            InitializeComponent();
            initData();
        }

        private void frmBackupRestore_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }

        private void bteFileBackup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {            
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Vị trí lưu";
            dlg.Filter = "Tập tin dự phòng (*.fbk)|*.fbk|Tất cả (*.*)|*.*";
            string dir = System.Environment.CurrentDirectory;
            if (usingGBACK)
            {
                if (dir.Contains(" "))
                {
                    HelpMsgBox.ShowNotificationMessage("Xin vui lòng chọn đường dẫn không có khoản trống. Ví dụ D:\\\\");
                    return;
                }
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bteFileBackup.Text = dlg.FileName;
                System.Environment.CurrentDirectory = dir;
            }
        }

        private void bteFileRestore_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Vị trí phục hồi";
            dlg.Filter = "Tập tin dự phòng (*.fbk)|*.fbk|Tất cả (*.*)|*.*";
            string dir = System.Environment.CurrentDirectory;

            //Do không dùng cơ chế phục hồi gbak của Firebird do đó nên không cần kiểm tra cái này nữa.
            if (usingGBACK)
            {
                if (dir.Contains(" "))
                {
                    HelpMsgBox.ShowNotificationMessage("Xin vui lòng chọn đường dẫn không có khoản trống. Ví dụ D:\\\\");
                    return;
                }
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bteFileRestore.Text = dlg.FileName;
                System.Environment.CurrentDirectory = dir;
            }
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            if (validate().Count > 0) return;
            if (xtraTabControl1.SelectedTabPage == xtraTabPageBackup)
            {
                if (HelpNoCategory.IsAtDBServer() == true)
                {
                    BackRes = new BackupRestoreInfo(UserId, bteFileBackup.Text, mmeDesBackup.Text, 'Y');
                    //TrialWaitingBox.LongProcess(this, new ThreadStart(ExecBackup), -1);
                    //WaitingMsg.LongProcess(ExecBackup);

                    HelpWaiting.showProgressForm(this, ExecBackup);

                    if (!Finish) FWMsgBox.showBackupError();
                    else { HelpMsgBox.ShowNotificationMessage("Sao lưu thành công."); this.Close(); }
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Sao lưu không thành công. Việc sao lưu dữ liệu chỉ được thực hiện tại máy chủ dữ liệu");
                }
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPageRestore)
            {
                if (HelpNoCategory.IsAtDBServer() == true)
                {
                    BackRes = new BackupRestoreInfo(UserId, bteFileRestore.Text, mmeDesRestore.Text, 'N');
                    //TrialWaitingBox.LongProcess(this, new ThreadStart(ExecRestore), -1);
                    WaitingMsg.LongProcess(ExecRestore);
                    if (!Finish) FWMsgBox.showRestoreError();
                    else { HelpMsgBox.ShowNotificationMessage("Phục hồi thành công. Dữ liệu lưu tại thư mục chứa db của ứng dụng với phần mở rộng .bak"); this.Close(); }
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Phục hồi không thành công. Việc phục hồi dữ liệu chỉ được thực hiện tại máy chủ dữ liệu");
                }
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPageHistory)
            {
                LoadHistory();
            }        
        }

        private void ExecBackup(List<object> input)
        {
            Finish = BackRes.backup();
        }

        private void ExecBackup()
        {            
            Finish = BackRes.backup();
        }

        private void ExecRestore()
        {
            Finish = BackRes.restore();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();                        
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPageHistory)
                LoadHistory();            
        }

        private void LoadHistory()
        {
            BackupRestoreInfo BackRes = new BackupRestoreInfo();
            DataSet ds = BackRes.GetHistory();
            
            gridControlHistory.DataSource = (DataTable)ds.Tables[0];
        }

        public void initData()
        {
            UserId = FrameworkParams.currentUser.id;
            txtUser.EditValue = FrameworkParams.currentUser.username;
            txtUser2.EditValue = FrameworkParams.currentUser.username;
            txtDate.EditValue = DateTime.Now;
            txtDate2.EditValue = DateTime.Now;
        }

        public void trimAllData()
        {
            mmeDesRestore.Text = mmeDesRestore.Text.Trim();
            mmeDesBackup.Text = mmeDesBackup.Text.Trim();
        }

        public List<string> validate()
        {
            trimAllData();
            List<string> error = new List<string>();
            if (xtraTabControl1.SelectedTabPage == xtraTabPageBackup)
            {
                if(HelpIsCheck.isBlankString(bteFileBackup.Text)){
                    error.Add(ErrorMsgLib.errorRequired("Tập tin dữ liệu"));
                    PLMessageBox.ShowErrorMessage(ErrorMsgLib.errorRequired("Tập tin dữ liệu"));
                }
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPageRestore)
            {
                if(HelpIsCheck.isBlankString(bteFileRestore.Text)){
                    error.Add(ErrorMsgLib.errorRequired("Tập tin dữ liệu"));
                    PLMessageBox.ShowErrorMessage(ErrorMsgLib.errorRequired("Tập tin dữ liệu"));
                }
            }
            return error;
        }

       
    }
}