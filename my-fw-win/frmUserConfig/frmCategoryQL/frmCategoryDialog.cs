using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Danh mục ko gắn vào bất cứ trung tâm điều khiển nào.
    /// </summary>
    public partial class frmCategoryDialog : XtraFormPL
    {
        private IPluginCategory cat;

        public frmCategoryDialog(XtraUserControl control, string Caption, Size? size)
        {
            InitializeComponent();
            if (Caption != null) this.Text = Caption;
            if (size != null) this.Size = size.Value;
            this.panelControl1.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            this.cat = (IPluginCategory)control;
            if (control is DMTreeGroup) this.ThemCon.Visible = true;
            if (control is IActionCategory) ((IActionCategory)this.cat).GetMenuAction().Visible = false;
            this.ThemCon.Image = FWImageDic.ADD_CHILD_IMAGE16;
            this.btnThem.Image = FWImageDic.ADD_IMAGE16;
            this.btnXoa.Image = FWImageDic.DELETE_IMAGE16;
            this.btnSua.Image = FWImageDic.EDIT_IMAGE16;
            this.btnLuu.Image = FWImageDic.SAVE_IMAGE16;
            this.btnKhongLuu.Image = FWImageDic.NO_SAVE_IMAGE16;
            this.btnClose.Image = FWImageDic.CLOSE_IMAGE16;
        }

        public frmCategoryDialog(XtraUserControl control)
            : this(control, null, null)
        {
                
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            this.cat.AddAction(null);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.cat.DeleteAction(null);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            this.cat.EditAction(null);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.cat.SaveAction(null);
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            this.cat.NoSaveAction(null);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThemCon_Click(object sender, EventArgs e)
        {
            this.cat.AddChildAction(null);
        }
    }
}