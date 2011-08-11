using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ProtocolVN.Framework.Win;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    [UserRepositoryItem("Register")]
    public class RepositoryItemSelectDMTreeGroup:RepositoryItemPopupContainerEdit
    {
        public const string RepositoryName = "SelectDMTreeGroup";
        private PopupContainerControl popupControl;
        private DMTreeGroup plGroupCatNew1;
        private string ValueField;
        private long selectId = -1;
        private bool isActive = false;
        private PopupContainerEdit containerEdit = null;
        private string FilterField;
        private bool isSetValue = false;

        public RepositoryItemSelectDMTreeGroup()
        {
            plGroupCatNew1 = new DMTreeGroup();
            popupControl = new PopupContainerControl();
            this.PopupControl = popupControl;
            popupControl.Controls.Add(plGroupCatNew1);
            popupControl.Size = plGroupCatNew1.Size;
            this.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(RepositoryItemSelectDMGridTemplate_EditValueChanging);
            this.Popup += new EventHandler(RepositoryItemSelectDMGridTemplate_Popup);
            this.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(popupContainerEdit1_CloseUp);
            this.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(popupContainerEdit1_Closed);
            this.KeyDown += new KeyEventHandler(popupContainerEdit1_KeyDown);
        }
        public void InitReadOnly(GridView gridView,String GroupTableName, int[] RootID, String IDField, String ParentIDField,
                                    string[] VisibleFields, string[] Captions,string columnField,string getField,string FilterField )
        {
            //popupContainerEdit1.Text = popupContainerEdit1.Properties.NullText;
            plGroupCatNew1.Init(GroupElementType.ONLY_CHOICE, GroupTableName, RootID, IDField, 
                                ParentIDField, VisibleFields, Captions, HelpGen.G_FW_DM_ID, null, null);
            this.ValueField = getField;
            this.FilterField = FilterField;
            AddCondition();
            gridView.GridControl.DataSourceChanged += delegate(object sender, EventArgs e)
            {
                if (gridView.GridControl.DataSource != null &&
                    !((DataTable)gridView.GridControl.DataSource).Columns.Contains(columnField + getField))
                {

                    DataTable dt = (DataTable)gridView.GridControl.DataSource;
                    dt.Columns.Add(new DataColumn(columnField + getField));
                    SetValueTable(ref dt, columnField, columnField + getField, GroupTableName, "ID", getField);
                }
            };

            long idValue;

            gridView.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    idValue = _getSelectedID();
                    if (e.RowHandle > 0)
                    {
                        DataTable dt = ((DataTable)gridView.GridControl.DataSource);
                        DataRow row = dt.Rows[e.RowHandle];
                        row[columnField] = idValue;
                        row[columnField + getField] = e.Value;
                    }
                }
            };
            gridView.RowUpdated += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
            {
                DataRow row = ((DataRowView)e.Row).Row;
                if (row.RowState == DataRowState.Added)
                    row[columnField] = _getSelectedID();
            };
            
        }
       
        public void InitReadOnly(TreeList treeList, String GroupTableName,DataTable dtSource, int[] RootID, String IDField, String ParentIDField,
                                   string[] VisibleFields, string[] Captions, string columnField, string getField, string FilterField)
        {
            plGroupCatNew1.Init(GroupElementType.ONLY_CHOICE, GroupTableName, RootID, IDField,
                                ParentIDField, VisibleFields, Captions, HelpGen.G_FW_DM_ID, null, null);
            this.ValueField = getField;
            this.FilterField = FilterField;
            AddCondition();

            if (!dtSource.Columns.Contains(columnField + getField))
            {
                dtSource.Columns.Add(new DataColumn(columnField + getField));
                SetValueTable(ref dtSource, columnField, columnField + getField, GroupTableName, "ID", getField);
                treeList.DataSource = dtSource;
            }
            long idValue;
            treeList.CellValueChanging += delegate(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    idValue = _getSelectedID();
                    DataRow row = ((DataRowView)treeList.GetDataRecordByNode(e.Node)).Row;
                    row[columnField] = idValue;
                    row[columnField + getField] = e.Value;
                }
            };
        }


        public void InitUpdate(GridView gridView, String GroupTableName, int[] RootID, String IDField, String ParentIDField,
                                string[] VisibleFields, string[] Captions, string GenName,
                                object[] InputColumnType, FieldNameCheck[] Rules,string columnField,string getField, string FilterField)
        {
            plGroupCatNew1.Init(GroupElementType.CHOICE_N_ADD, GroupTableName, RootID, IDField, 
                                ParentIDField, VisibleFields, Captions, GenName, InputColumnType, Rules);
            this.ValueField = getField;
            this.FilterField = FilterField;
            AddCondition();
            gridView.GridControl.DataSourceChanged += delegate(object sender, EventArgs e)
            {
                if (gridView.GridControl.DataSource != null &&
                    !((DataTable)gridView.GridControl.DataSource).Columns.Contains(columnField + getField))
                {

                    DataTable dt = (DataTable)gridView.GridControl.DataSource;
                    dt.Columns.Add(new DataColumn(columnField + getField));
                    SetValueTable(ref dt, columnField, columnField + getField, GroupTableName, "ID", getField);
                }
            };

            long idValue;

            gridView.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    idValue = _getSelectedID();
                    if (e.RowHandle > 0)
                    {
                        DataTable dt = ((DataTable)gridView.GridControl.DataSource);
                        DataRow row = dt.Rows[e.RowHandle];
                        row[columnField] = idValue;
                        row[columnField + getField] = e.Value;
                    }
                }
            };
            gridView.RowUpdated += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
            {
                DataRow row = ((DataRowView)e.Row).Row;
                if (row.RowState == DataRowState.Added)
                    row[columnField] = _getSelectedID();
            };
        }
        public void InitUpdate(TreeList treeList, String GroupTableName,DataTable dtSource, int[] RootID, String IDField, String ParentIDField,
                               string[] VisibleFields, string[] Captions, string GenName,
                               object[] InputColumnType, FieldNameCheck[] Rules, string columnField, string getField, string FilterField)
        {
            plGroupCatNew1.Init(GroupElementType.CHOICE_N_ADD, GroupTableName, RootID, IDField,
                                ParentIDField, VisibleFields, Captions, GenName, InputColumnType, Rules);
            this.ValueField = getField;
            this.FilterField = FilterField;
            AddCondition();

            if (!dtSource.Columns.Contains(columnField + getField))
            {
                dtSource.Columns.Add(new DataColumn(columnField + getField));
                SetValueTable(ref dtSource, columnField, columnField + getField, GroupTableName, "ID", getField);
                treeList.DataSource = dtSource;
            }
            long idValue;
            treeList.CellValueChanging += delegate(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    idValue = _getSelectedID();
                    DataRow row = ((DataRowView)treeList.GetDataRecordByNode(e.Node)).Row;
                    row[columnField] = idValue;
                    row[columnField + getField] = e.Value;
                }
            };
        
        }
        
        void RepositoryItemSelectDMGridTemplate_Popup(object sender, EventArgs e)
        {
            containerEdit = (PopupContainerEdit)sender;
            plGroupCatNew1.setPopupControl(containerEdit);
            plGroupCatNew1.setSelectedID(selectId);
        }

        private void SetValueTable(ref DataTable dt, string valueField, string fieldName, string tableName, string idField, string getField)
        {
            DataSet ds = DABase.getDatabase().LoadTable(tableName);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow[] row = ds.Tables[0].Select(idField + " = " + dr[valueField].ToString());
                    dr[fieldName] = row[0][getField];
                }
            }
            catch { }
        }

        void RepositoryItemSelectDMGridTemplate_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!isSetValue)
            {
                containerEdit = (PopupContainerEdit)sender;
                object prevalue = null;

                plGroupCatNew1.TreeList_1.OptionsBehavior.EnableFiltering = true;
                plGroupCatNew1.TreeList_1.OptionsBehavior.AutoSelectAllInEditor = false;

                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Value1 = prevalue;
                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Visible = true;
                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Value1 = containerEdit.Text;
                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Visible = false;

                prevalue = containerEdit.Text;
                containerEdit.ShowPopup();
                containerEdit.Focus();
                isActive = true;
            }
        }

        void popupContainerEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== System.Windows.Forms.Keys.Down || e.KeyCode == System.Windows.Forms.Keys.Up)
            {
                if (isActive)
                {
                    plGroupCatNew1.TreeList_1.Focus();
                    try
                    {
                        TreeListNode node = plGroupCatNew1.TreeList_1.Selection[0];
                        plGroupCatNew1.TreeList_1.FocusedNode = (e.KeyCode == System.Windows.Forms.Keys.Down) ? node.NextVisibleNode : node.PrevVisibleNode;
                    }
                    catch { }
                }
            }
        }
        #region GET & SET
        public long _getSelectedID()
        {
            return plGroupCatNew1.getSelectedID();
        }
        public void _setSelectedID(long id)
        {
            plGroupCatNew1.setSelectedID(id);
            if (id != -1)
            {
                if (plGroupCatNew1.SelectedNode != null)
                {
                    SetTextValue(plGroupCatNew1.SelectedNode[ValueField].ToString());
                    return;
                }
            }
            SetTextValue(containerEdit.Properties.NullText);
        }

        private void SetTextValue(string value)
        {
            isSetValue = true;
            containerEdit.Text = value;
            isSetValue = false;
        }
        #region Kiểm tra dữ liệu
        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorName)
        {
            errorProvider.SetError(containerEdit, errorName);
        }
        #endregion
        #endregion

        #region Sự kiện trên Popup
        private void popupContainerEdit1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (plGroupCatNew1.TreeList_1.OptionsBehavior.Editable == true)
            {
                TreeListNode node = plGroupCatNew1.TreeList_1.FocusedNode;
                //HUYNC : Thêm điều kiện kiểm tra có sử dụng filter hay không?
                if (node.Id != 0)
                    containerEdit.ShowPopup();
            }
        }

        private void popupContainerEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            TreeListNode selectNode = plGroupCatNew1.SelectedNode;
            //if (selectNode == null)
            //{
            //    //Thông báo không cho chọn nút trên cùng
            //    SetTextValue(containerEdit.Properties.NullText);
            //    return;
            //}
            if (selectNode != null)
            {
                //Khong cho chon nut tren cung
                if (selectNode.GetValue(ValueField) != DBNull.Value && selectNode.GetValue(ValueField) != null)
                    SetTextValue(selectNode.GetValue(ValueField).ToString());
                else
                    SetTextValue(containerEdit.Properties.NullText);

                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Value1 = null;
                plGroupCatNew1.TreeList_1.FilterConditions[plGroupCatNew1.TreeList_1.FilterConditions.Count - 1].Visible = true;

                selectId = plGroupCatNew1.getSelectedID();
            }
        }
      
        #endregion
        
        private void AddCondition()
        {
            plGroupCatNew1.TreeList_1.FilterConditions.Add(new FilterCondition(FilterConditionEnum.NotContains,plGroupCatNew1.TreeList_1.Columns.ColumnByFieldName(FilterField),null,null,true));
        }
        static RepositoryItemSelectDMTreeGroup()
        {
            Register();
        }

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(RepositoryName, typeof(PopupContainerEdit), typeof(RepositoryItemSelectDMTreeGroup), typeof(DevExpress.XtraEditors.ViewInfo.PopupContainerEditViewInfo), 
                new DevExpress.XtraEditors.Drawing.ButtonEditPainter(), true, null));
        }
        public override string EditorTypeName
        {
     		get
    		{
          		return RepositoryName;
     		}
		}
      
    }
}
