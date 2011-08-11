using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using DevExpress.XtraEditors.Controls;

/*
Yêu cầu
1. Tổ chức một Address book gồm nhưng thông tin contact cá nhân
2. Cấu hình hình thị các đối tượng quan tâm
    + Nhân viên
    + Khách hàng
    + Nhà cung cấp
    + Đối tác
    + ...
3. Hỗ trợ chọn 1 hoặc nhiều mục tin từ chọn lựa này
	+ Ví dụ trong hệ thống gửi mail có thể gọi form chọn lựa này
	để chọn ra được các địa chỉ mail hoặc trong hệ thống gửi thư
	cho khách hàng có thể dùng cái này để chọn khách hàng cần gửi
	
-----------------------------------------------------------------	
Review 24/12/2008
1. OK Nhưng form contact nên cho phép số liệu ít và số liệu nhiều.
2. Chưa xây dựng
3. Chưa kiểm tra được
*/
namespace ProtocolVN.Framework.Win
{
    public partial class frmAddressBook : DevExpress.XtraEditors.XtraForm, IPublicForm
    {
        private string tableNameContact;
        private string tableNameGroup;
        private string displayFieldGroup;
        private string valueFieldGroup;
        private string genGroup;
        private string genContact;
        private string idFieldGroupContact;
        public static List<object> ListNumber;
        public static List<object> ListEmail;
        private long userId;
        private string userIdField;
        private DataSet dsGroup;
        
        public frmAddressBook()
        {
            try
            {
                InitializeComponent();
                userId = FrameworkParams.currentUser.id;
                tableNameContact = "FW_AB_CONTACT";
                idFieldGroupContact = "GROUP_ID";
                tableNameGroup = "FW_AB_GROUP";
                displayFieldGroup = "NAME";
                valueFieldGroup = "ID";
                userIdField = "USERID";
                //genGroup = "G_AB_GROUP";
                //genContact = "G_AB_CONTACT";
                genGroup = "G_FW_ID";
                genContact = "G_FW_ID";
                ListNumber = null;
                ListEmail = null;
            }
            catch (Exception ex)
            {
                HelpMsgBox.ShowNotificationMessage(ex.StackTrace);
            }
        }

        #region Event

        private void frmAddressBookcs_Load(object sender, EventArgs e)
        {
            initContact();
            initGroup();
            try
            {
                if (listBoxControl1.Items.Count < 1)
                {
                    EnableGroup(false);
                    barThemContact.Enabled = false;
                }
                if (gridView1.DataRowCount < 1) EnableContact(false);
            }
            catch { }
            //
        }

        void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn.FieldName == "GHI_CHU")
                gridView1.OptionsBehavior.Editable = true;
            else
                gridView1.OptionsBehavior.Editable = false; 
        }

        void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowChangeContact();
        }

        private void EnableGroup(bool value)
        {
            barXoaNhom.Enabled = value;
            barDoiTenNhom.Enabled = value;
        }

        private void EnableContact(bool value)
        {
            barSuaContact.Enabled = value;
            barXoaContact.Enabled = value;
        }
        void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = dsGroup.Tables[0].Rows[listBoxControl1.SelectedIndex];
                int idGroup = HelpNumber.ParseInt32(dr[valueFieldGroup].ToString());
                gridControl1.DataSource = GetDataSetContact(idGroup.ToString()).Tables[0];
                //HUYNC
                EnableGroup(true);
            }
            catch { EnableGroup(false); EnableContact(false); barThemContact.Enabled = false; gridControl1.DataSource = null; }
        }

        private void barThemNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGroup group = new frmGroup(DataOperation.Add);
            ProtocolForm.ShowModalDialog(this, group);
            if(frmGroup.TenNhom !=null)
            {
                if (listBoxControl1.FindString(frmGroup.TenNhom) < 0)
                    AddNewGroup();
                barThemContact.Enabled = true;
            }
        }

        private void barXoaNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteGroup();
            if (listBoxControl1.Items.Count > 0)
                barThemContact.Enabled = true;
            else
                barThemContact.Enabled = false;
        }

        private void barDoiTenNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGroup group = new frmGroup(DataOperation.Edit);
            ProtocolForm.ShowModalDialog(this, group);
            if (frmGroup.TenNhom != null)
            {
                if (listBoxControl1.FindString(frmGroup.TenNhom) < 0)
                    ChangeGroupName();
            }
        }

        private void barThemContact_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow dr = dsGroup.Tables[0].Rows[listBoxControl1.SelectedIndex];
            frmContact contact = new frmContact(dr[valueFieldGroup], genContact, tableNameContact);
            ProtocolForm.ShowModalDialog(this, contact);
            gridControl1.DataSource = GetDataSetContact(dr[valueFieldGroup].ToString()).Tables[0];
        }

        private void barXoaContact_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn xóa không?");
            if (result == DialogResult.Yes)
            {
                gridView1.DeleteSelectedRows();
                UpdateContact();
            }
        }

        private void barSuaContact_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShowChangeContact();
            }
            catch { }
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barChon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetMobileNumber();
            GetEmail();
            this.Close();
        }


        #endregion

        #region Group
        private void initGroup()
        {
            try
            {
                DbCommand dbCommand = DABase.getDatabase().GetSQLStringCommand("SELECT * FROM " + tableNameGroup+ " WHERE " + userIdField+ "=" +userId);
                dsGroup = DABase.getDatabase().LoadDataSet(dbCommand);
                //listBoxControl1.DataSource = ds.Tables[0];
                //listBoxControl1.DisplayMember = displayFieldGroup;
                //listBoxControl1.ValueMember = valueFieldGroup;
                AddListBox(dsGroup.Tables[0]);
                listBoxControl1.HighlightedItemStyle = HighlightStyle.Skinned;
                listBoxControl1.HotTrackItems = false;
                listBoxControl1.SelectedIndexChanged += new EventHandler(listBoxControl1_SelectedIndexChanged);
                listBoxControl1.SelectedIndex = 0;
                DataRow dr = dsGroup.Tables[0].Rows[listBoxControl1.SelectedIndex];
                gridControl1.DataSource = GetDataSetContact(dr[valueFieldGroup].ToString()).Tables[0];
            }
            catch { }
        }

        private void AddListBox(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
                listBoxControl1.Items.Add(dr[displayFieldGroup].ToString(), 9);
        }

        private void AddNewGroup()
        {
            DataTable dt = dsGroup.Tables[0];
            DataRow dr = dt.NewRow();
            dr[displayFieldGroup] = frmGroup.TenNhom;
            dr[valueFieldGroup] = DABase.getDatabase().GetID(genGroup);
            dr[userIdField] = userId;
            dt.Rows.Add(dr);

            if (UpdateGroup(DataOperation.Add, dr[valueFieldGroup], dr[displayFieldGroup]) != -1)
                listBoxControl1.Items.Add(frmGroup.TenNhom, 9);
        }

        private int UpdateGroup(DataOperation operation,object value, object display)
        {
            string command;
            if (operation == DataOperation.Add)
                command = "INSERT INTO " + tableNameGroup + "(" + valueFieldGroup + "," + displayFieldGroup + "," + userIdField +  ")" + " VALUES(" + value.ToString() + ",'" + display.ToString() + "'," + userId + ")";
            else if (operation == DataOperation.Edit)
                command = "UPDATE " + tableNameGroup + " SET " + displayFieldGroup + "='" + display.ToString() + "' WHERE " + valueFieldGroup + " = " + value.ToString();
            else
                command = "DELETE FROM " + tableNameGroup + " WHERE " + valueFieldGroup + "=" + value; 

            DbCommand dbCommand = DABase.getDatabase().GetSQLStringCommand(command);
            return DABase.getDatabase().ExecuteNonQuery(dbCommand);
            
        }

        private void DeleteGroup()
        {
            DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn xóa không?");
            if (result == DialogResult.Yes)
            {
                DataTable dt = dsGroup.Tables[0];
                DataRow dr = dt.Rows[listBoxControl1.SelectedIndex];
                try
                {
                    if (UpdateGroup(DataOperation.Delete, dr[valueFieldGroup], dr[displayFieldGroup]) > -1)
                    {
                        dr.Delete();
                        dt.AcceptChanges();
                        listBoxControl1.Items.RemoveAt(listBoxControl1.SelectedIndex);
                    }
                }
                catch { }

            }
        }

        private void ChangeGroupName()
        {
            int indexChange = listBoxControl1.SelectedIndex;
            DataTable dt = dsGroup.Tables[0];
            DataRow dr = dt.Rows[indexChange];
            if (UpdateGroup(DataOperation.Edit, dr[valueFieldGroup], frmGroup.TenNhom) > -1)
            {
                dr[displayFieldGroup] = frmGroup.TenNhom;
                ImageListBoxItem item = new ImageListBoxItem(frmGroup.TenNhom, 9);
                listBoxControl1.Items.Insert(indexChange, item);
                listBoxControl1.Items.RemoveAt(indexChange + 1);
                listBoxControl1.SelectedIndex = indexChange;
            }

        }
        #endregion

        #region Contact
        private void initContact()
        {
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.OptionsBehavior.Editable = false;
            gridColumn7.OptionsColumn.AllowEdit = true;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridView1.DoubleClick += new EventHandler(gridView1_DoubleClick);
            gridView1.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(gridView1_FocusedColumnChanged);
            gridView1.RowCountChanged += new EventHandler(gridView1_RowCountChanged);
        }

        void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0)
            {
                EnableContact(true);
                barThemContact.Enabled = true;
            }
            else
                EnableContact(false);
        }

        private int UpdateContact()
        {
            DataSet ds = ((DataTable)gridControl1.DataSource).DataSet;
            return DABase.getDatabase().UpdateTable(ds, tableNameContact);
        }

        private void ShowChangeContact()
        {
            DataRow dr = dsGroup.Tables[0].Rows[listBoxControl1.SelectedIndex];
            frmContact contact = new frmContact(dr[valueFieldGroup], genContact, tableNameContact, gridView1.GetDataRow(gridView1.FocusedRowHandle));
            ProtocolForm.ShowModalDialog(this, contact);
            gridControl1.DataSource = GetDataSetContact(dr[valueFieldGroup].ToString()).Tables[0];
        }

        private DataSet GetDataSetContact(string valueCondition)
        {
            DbCommand dbCommand = DABase.getDatabase().GetSQLStringCommand("SELECT * FROM " + tableNameContact + " WHERE " + idFieldGroupContact + "=" + valueCondition);
            return DABase.getDatabase().LoadDataSet(dbCommand, tableNameContact);
        }
        #endregion

        private void GetMobileNumber()
        {
            ListNumber = new List<object>();
            int[] index = gridView1.GetSelectedRows();
            foreach(int i in index)
            {
                DataRow dr = gridView1.GetDataRow(i);
                string phone = dr["DIEN_THOAI"].ToString();
                phone = phone.Replace(".","");
                ListNumber.Add(phone);
            }
        }

        private void GetEmail()
        {
            ListEmail = new List<object>();
            int[] index = gridView1.GetSelectedRows();
            foreach (int i in index)
            {
                DataRow dr = gridView1.GetDataRow(i);
                ListEmail.Add(dr["EMAIL"].ToString());
            }
        }

    
    }
}
