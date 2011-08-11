using System;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    public enum DataOperation
    {
        Add = 0,
        Edit = 1,
        Delete = 2
    }
    public partial class frmGroup : DevExpress.XtraEditors.XtraForm, IPublicForm
    {

        public static string TenNhom = null;
        public frmGroup(DataOperation operation)
        {
            InitializeComponent();
            if (operation == DataOperation.Add)
                this.Text = "Thêm nhóm mới";
            else
                this.Text = "Sửa nhóm";
        }

        private bool isValidName()
        {
            return ((txtTenNhom.Text == "") ? false : true);
        }
        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (isValidName())
            {
                TenNhom = txtTenNhom.Text;
                this.Close();
            }
            else
            {
                PLMessageBox.ShowErrorMessage("Bạn chưa nhập tên nhóm");
                txtTenNhom.Focus();
            }
        }

        private void btnĐong_Click(object sender, EventArgs e)
        {
            TenNhom = null;
            this.Close();
        }

        private void frmGroup_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }

    }
}