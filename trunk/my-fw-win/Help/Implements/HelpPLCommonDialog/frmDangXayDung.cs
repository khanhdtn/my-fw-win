using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class frmDangXayDung : XtraForm, IPublicForm, INonLog
    {
        public frmDangXayDung()
        {
            InitializeComponent();
        }

        public static void Show(XtraForm form)
        {
            frmDangXayDung frm = new frmDangXayDung();
            ProtocolForm.ShowModalDialog(form, frm);
        }
    }
}