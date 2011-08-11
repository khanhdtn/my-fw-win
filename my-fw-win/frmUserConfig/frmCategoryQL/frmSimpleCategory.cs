using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.XPath;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using ProtocolVN.Framework.Core;
using System.Drawing;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Trung tâm điều khiển các danh mục. Nó là nơi tập trung tất cả danh mục.
    /// </summary>
    public partial class frmSimpleCategory : XtraFormPL, IParamForm, IFormCategory
    {
        private string table;
        private string type;
        private string captionVN;

        public frmSimpleCategory(string table, string type, string captionVN)
        {
            InitializeComponent();
            this.addButton.Image = FWImageDic.ADD_IMAGE20;
            this.editButton.Image = FWImageDic.EDIT_IMAGE20;
            this.removeButton.Image = FWImageDic.DELETE_IMAGE20;
            this.closeButton.Image = FWImageDic.CLOSE_IMAGE20;
            this.btnSave.Image = FWImageDic.SAVE_IMAGE20;
            this.btnNoSave.Image = FWImageDic.NO_SAVE_IMAGE20;
            this.table = table;
            this.type = type;
            this.captionVN = captionVN;
        }

        #region Sự kiện trên form
        private void frmSimpleCategory_Load(object sender, EventArgs e)
        {
            if (control != null)
            {
                this.panel1.Controls.Remove(control);
                this.control.Visible = false;
            }

            lblCat.Text = "Danh sách " + captionVN.ToLower();

            string param = "";
            string formName = table;

            if (formName.Contains("?") && formName.Contains("="))
            {
                param = formName.Substring(formName.IndexOf('=') + 1).Trim();
                formName = formName.Substring(0, formName.IndexOf('?')).Trim();
            }

            this.control = (XtraUserControl)GenerateClass.initMethod(formName, param, false);

            if (control == null) return;

            if (HelpPermission.CheckCtrl(control) == false)
            {
                ProtocolForm.ShowModalDialog(this, new frmPermissionFail());
                return;
            }

            ICategory cat = (ICategory)this.control;
            cat.SetOwner(this);
            cat.UpdateGUI();
            this.control.Disposed += delegate
            {
                this.Close();
            };

            this.control.Dock = DockStyle.Fill;
            this.control.Name = "control";
            //this.control.Visible = true;
            this.panel1.Controls.Add(control);


            if (type.Equals("action"))
            {
                //Dùng hoàn toàn User Control của mình
            }
            else if (type.Equals("plugin"))
            {
                //Dùng navigation của form frmCategory
                //this.closeButton.Visible = false;
                this.control.Controls.Add(this.toolStrip1);
                
                if (cat.GetGridView() != null)
                {
                    HelpGrid.addXuatRaFileItem(this.toolStrip1, cat.GetGridView());
                }

                if (cat.GetGridView() != null)
                {
                    HelpGrid.addInLuoiItem(this.toolStrip1, cat.GetGridView());
                }

            }
            this.control.Focus();
        }

        /// <summary>Đóng form
        /// </summary>
        private void closeButton_Click1(object sender, EventArgs e){
            this.Close();
        }

        /// <summary>Chọn Xóa mục tin
        /// </summary>
        private void removeButton_Click1(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.DeleteAction(null);
        }

        /// <summary>Chọn Sửa mục tin
        /// </summary>
        private void editButton_Click1(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.EditAction(null);
        }

        /// <summary>Chọn Thêm mục tin
        /// </summary>
        private void addButton_Click1(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.AddAction(null);
        }

        /// <summary>Chọn Lưu mục tin
        /// </summary>
        private void btnSave_Click1(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.SaveAction(null);
        }

        /// <summary>Chọn Không Lưu mục tin
        /// </summary>
        private void btnNoSave_Click1(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.NoSaveAction(null);
        }
        
        #endregion

        #region IParamForm & ILangable Members

        void IParamForm.Activate()
        {
            //FocusParam(TagPropertyMan.Get(this.Tag, "FORM_PARAM").ToString());
        }

        public List<Control> GetLangableControls()
        {
            return null;
        }

        #endregion        
    
        #region IFormCategory Members

        public ToolStripButton GetRemoveBtn()
        {
            return this.removeButton;
        }

        public ToolStripButton GetAddBtn()
        {
            return this.addButton;
        }

        public ToolStripButton GetEditBtn()
        {
            return this.editButton;
        }

        public ToolStripButton GetSaveBtn()
        {
            return this.btnSave;
        }

        public ToolStripButton GetNoSaveBtn()
        {
            return this.btnNoSave;
        }

        #endregion
    }    
}