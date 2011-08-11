using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using ProtocolVN.Framework.Win;
using System.Data;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmClipboardMan : XtraForm, IParamForm, IProtocolForm
    {
        public frmClipboardMan()
        {
            InitializeComponent();

            this.addButton.Image = FWImageDic.ADD_IMAGE20;
            this.removeButton.Image = FWImageDic.DELETE_IMAGE20;
            this.closeButton.Image = FWImageDic.CLOSE_IMAGE20;
            this.closeButton.Visible = false;
            groupClipboard.GroupStyle = NavBarGroupStyle.LargeIconsText;
        }
        string entitySelected = "";
        private void Init()
        {
            
            string[] titles = ClipboardMan.Instance.GetTitiles();
            string[] entitys = ClipboardMan.Instance.GetEntitys();
            for (int i = 0; i < titles.Length; i++)
            {
                NavBarItem nav = new NavBarItem(titles[i]);
                nav.Name = entitys[i]; //Gán tên của Item là tên của đối tượng tương ứng
                nav.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                nav.AppearanceHotTracked.Options.UseFont = true;
                nav.LinkClicked += new NavBarLinkEventHandler(Item_LinkClicked);
                nav.LargeImage = ResourceMan.getImage48(ClipboardMan.Instance.GetImange(entitys[i]));
                DevExpress.XtraNavBar.NavBarItemLink navItemLink = new DevExpress.XtraNavBar.NavBarItemLink(nav);
                groupClipboard.ItemLinks.Add(navItemLink);
                this.navBarControl1.Items.Add(nav);
            }

            //CHAUTV : Nếu có một clipboardItem có dữ liệu thì hiển thị lên
            bool TonTai = false;
            for (int j = 0; j < titles.Length; j++)
            {
                if (ClipboardMan.Instance.CheckDataSet(entitys[j]))
                {
                    dgc_details.DataSource = ClipboardMan.Instance.clipboard[entitys[j]].Data.Tables[0];
                    entitySelected = entitys[j];
                    ShowColumns(entitySelected);
                    TonTai = true;
                    break;
                }
            }
            if (TonTai == false&&ClipboardMan.Instance.clipboard.Count>0) //Neu khong co dư liệu nào thì hiện cấu trúc dataset của clipboardItem đầu tiên
            {
                dgc_details.DataSource = ClipboardMan.Instance.clipboard[entitys[0]].Data.Tables[0];
                ShowColumns(entitys[0]);
            }
            if (dgv_details.RowCount > 0)
            {
                this.addButton.Enabled = true;
                this.removeButton.Enabled = true;
            }
            else
            {
                this.addButton.Enabled = false;
                this.removeButton.Enabled = false;
            }
        }

        void Item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            entitySelected = ((NavBarItem)sender).Name; //Chọn đối tượng 

            DataSet ds = ClipboardMan.Instance.clipboard[entitySelected].Data;
            dgc_details.DataSource = ds.Tables[0];
            ShowColumns(entitySelected);
           
            
        }
        public void ShowColumns(string entity)
        {
            //Duyệt ẩn hiện các cột
            string[] captions = ClipboardMan.Instance.clipboard[entity].Captions;
            string[] keys = ClipboardMan.Instance.clipboard[entity].Keys;
            DataSet ds = ClipboardMan.Instance.clipboard[entity].Data;
            for (int i = 0; i < captions.Length; i++)
            {
                string fieldname = ds.Tables[0].Columns[i].ColumnName;
                if (TonTaiField(fieldname, dgv_details))
                {
                    dgv_details.Columns[fieldname].Caption = captions[i];

                    if (captions[i].Trim() == "" && dgv_details.Columns[fieldname].Visible == true)
                    {

                        dgv_details.Columns[fieldname].Visible = false;
                    }
                }

            }
        }
                
        private bool TonTaiField(string field,DevExpress.XtraGrid.Views.Grid.GridView gridview)
        {
            for (int i = 0; i < gridview.Columns.Count; i++)
            {
                if (gridview.Columns[i].FieldName == field)
                    return true;
            }
            return false;
        }
               
        private void frmClipboardMan_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            ClipboardMan.Instance.DisData(entitySelected);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int[] rowsSelected = this.dgv_details.GetSelectedRows();
            ClipboardMan.Instance.ClearRows(entitySelected, rowsSelected);
            //Tiến hành xóa lưới
            dgv_details.DeleteSelectedRows();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_details_RowCountChanged(object sender, EventArgs e)
        {
            if (dgv_details.RowCount > 0)
            {
                this.addButton.Enabled = true;
                this.removeButton.Enabled = true;
            }
            else
            {
                this.addButton.Enabled = false;
                this.removeButton.Enabled = false;
            }
        }

        #region IParamForm Members

        void IParamForm.Activate()
        {

        }

        #endregion

        #region IPermisionable Members

        public List<object> GetObjectItems()
        {
            return null;
        }

        public List<Control> GetPermisionableControls()
        {
            return null;
        }

        #endregion

        #region IFormatable Members

        public List<Control> GetFormatControls()
        {
            return null;
        }

        #endregion

       
    }
}