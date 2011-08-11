using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using ProtocolVN.Framework.Win;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    //PHUOCNT NOTE Cập nhật xong chưa kiểm tra
    [UserRepositoryItem("Register")]
    public class RepositoryItemSelectDMGridTemplate : RepositoryItemPopupContainerEdit
    {
        public enum _FilterType
        {
            START_WITH,
            CONTAINS
        }
        
        private PopupContainerControl popupControl;
        private string DislayField;
        private long selectId = -1;
        private bool isActive = false;
        private PopupContainerEdit containerEdit = null;
        private bool isSetValue = false;
        private string FilterField;
        private string CotAo;

        public _FilterType _filterType = _FilterType.START_WITH;
        public DMGrid _DMGridCore;
        public const string RepositoryName = "SelectDMGridTemplate";


        public GridView mainGridView;
        public String IDField;
        public String columnField;
        
        //
        public RepositoryItemSelectDMGridTemplate(GroupElementType type, GridView gridView, string TableName, string columnField, string IDField, string DislayField, string getField, string[] NameFields,
            string[] Subjects, string FilterField, ProtocolVN.Framework.Win.DMBasicGrid.InitGridColumns InitGridCol, ProtocolVN.Framework.Win.DMBasicGrid.GetRule Rule, DelegationLib.DefinePermission permission, 
            DataSet dataTable0, params string[] readOnlyField)
        {
            CotAo = columnField + getField + "_PLV";
            this.ShowPopupShadow = true;
            _DMGridCore = new DMGrid();
            popupControl = new PopupContainerControl();
            this.CloseOnOuterMouseClick = false;
            this.PopupControl = popupControl;
            popupControl.Controls.Add(_DMGridCore);
            popupControl.Size = _DMGridCore.Size;
            this.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(RepositoryItemSelectDMGridTemplate_EditValueChanging);
            //_DMGridCore._init(type, TableName, IDField, DislayField, NameFields, Subjects, InitGridCol, Rule, permission, readOnlyField);
            _DMGridCore._init(type, dataTable0.Tables[0], IDField, DislayField, NameFields, Subjects, InitGridCol, Rule, permission, null, null, null, readOnlyField);
            _DMGridCore._DMCore._SupportDoubleClick = false;
            this.Popup += new EventHandler(RepositoryItemSelectDMGridTemplate_Popup);
            this.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(popupContainerEdit1_CloseUp);
            this.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(popupContainerEdit1_Closed);
            this.KeyDown += new KeyEventHandler(popupContainerEdit1_KeyDown);
            this.DislayField = DislayField;
            this.FilterField = FilterField;

            gridView.GridControl.DataSourceChanged += delegate(object sender, EventArgs e)
            {
                if (gridView.GridControl.DataSource != null &&
                    !((DataTable)gridView.GridControl.DataSource).Columns.Contains(CotAo))
                {
                    DataTable dt = (DataTable)gridView.GridControl.DataSource;
                    dt.Columns.Add(new DataColumn(CotAo));
                    SetValueTable(ref dt, columnField, CotAo, TableName, IDField, getField);
                }
            };

            long idValue;

            gridView.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                try
                {
                    if (e.Column.FieldName.Equals(CotAo))
                    {
                        idValue = _getSelectedID();
                        if (e.RowHandle < 0)
                        {
                            //gridView.EditingValue = idValue;
                            gridView.SetFocusedRowCellValue(gridView.Columns.ColumnByFieldName(columnField), idValue);
                        }
                        else
                        {
                            DataTable dt = ((DataTable)gridView.GridControl.DataSource);
                            DataRow row = dt.Rows[e.RowHandle];
                            row[columnField] = idValue;
                            row[CotAo] = e.Value;
                            gridView.SetRowCellValue(e.RowHandle, columnField, _getSelectedID());
                        }
                    }
                }
                catch { }
                //gridView.SetRowCellValue(e.RowHandle, columnField, _getSelectedID());

            };
            gridView.InitNewRow += delegate(object sender, InitNewRowEventArgs e)
            {
                gridView.SetRowCellValue(e.RowHandle, columnField, _getSelectedID());
            };

            this.mainGridView = gridView;
            this.IDField = IDField;
            this.columnField = columnField;
        }

        public RepositoryItemSelectDMGridTemplate(GroupElementType type, GridView gridView, string TableName, string columnField, string IDField, string DislayField, string getField, string[] NameFields,
          string[] Subjects, string FilterField, ProtocolVN.Framework.Win.DMBasicGrid.InitGridColumns InitGridCol, ProtocolVN.Framework.Win.DMBasicGrid.GetRule Rule, DelegationLib.DefinePermission permission, params string[] readOnlyField)
        {
            CotAo = columnField + getField + "_PLV";
            this.ShowPopupShadow = true;
            _DMGridCore = new DMGrid();
            popupControl = new PopupContainerControl();
            this.CloseOnOuterMouseClick = false;
            this.PopupControl = popupControl;
            popupControl.Controls.Add(_DMGridCore);
            popupControl.Size = _DMGridCore.Size;
            this.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(RepositoryItemSelectDMGridTemplate_EditValueChanging);
            _DMGridCore._init(type, TableName, IDField, DislayField, NameFields, Subjects, InitGridCol, Rule, permission, readOnlyField);
            _DMGridCore._DMCore._SupportDoubleClick = false;
            this.Popup += new EventHandler(RepositoryItemSelectDMGridTemplate_Popup);
            this.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(popupContainerEdit1_CloseUp);
            this.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(popupContainerEdit1_Closed);
            this.KeyDown += new KeyEventHandler(popupContainerEdit1_KeyDown);
            this.DislayField = DislayField;
            this.FilterField = FilterField;

            gridView.GridControl.DataSourceChanged += delegate(object sender, EventArgs e)
            {
                if (gridView.GridControl.DataSource != null &&
                    !((DataTable)gridView.GridControl.DataSource).Columns.Contains(CotAo))
                {
                    DataTable dt = (DataTable)gridView.GridControl.DataSource;
                    dt.Columns.Add(new DataColumn(CotAo));
                    SetValueTable(ref dt, columnField, CotAo, TableName, IDField, getField);
                }
            };

            long idValue;

            gridView.CellValueChanging += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                try
                {
                    if (e.Column.FieldName.Equals(CotAo))
                    {
                        idValue = _getSelectedID();
                        if (e.RowHandle < 0)
                        {
                            //gridView.EditingValue = idValue;
                            gridView.SetFocusedRowCellValue(gridView.Columns.ColumnByFieldName(columnField), idValue);
                        }
                        else
                        {
                            DataTable dt = ((DataTable)gridView.GridControl.DataSource);
                            DataRow row = dt.Rows[e.RowHandle];
                            row[columnField] = idValue;
                            row[CotAo] = e.Value;
                            gridView.SetRowCellValue(e.RowHandle, columnField, _getSelectedID());
                        }
                    }
                }
                catch { }
                //gridView.SetRowCellValue(e.RowHandle, columnField, _getSelectedID());

            };
            gridView.InitNewRow += delegate(object sender, InitNewRowEventArgs e)
            {
                gridView.SetRowCellValue(e.RowHandle, columnField, _getSelectedID());
            };

            this.mainGridView = gridView;
            this.IDField = IDField;
            this.columnField = columnField;
        }

       

        private void SetValueTable(ref DataTable dt, string valueField, string fieldName, string tableName, string idField, string getField)
        {
            DataSet ds = DABase.getDatabase().LoadTable(tableName);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[valueField] != null && dr[valueField].ToString() != "")
                {
                    DataRow[] row = ds.Tables[0].Select(idField + " = " + dr[valueField].ToString());
                    dr[fieldName] = row[0][getField];
                }
            }
        }
        private void SetValueTable(DataView view, string valueField, string fieldName, string tableName, string idField, string getField)
        {
            DataSet ds = DABase.getDatabase().LoadTable(tableName);
            for (int i = 0; i < view.Count; i++)
            {
                DataRowView drView = view[i];
                if (drView != null && drView[valueField].ToString() != "")
                {
                    DataRow[] row = ds.Tables[0].Select(idField + " = " + drView[valueField].ToString());
                    drView[fieldName] = row[0][getField];
                }
            }
        }
        void RepositoryItemSelectDMGridTemplate_Popup(object sender, EventArgs e)
        {
            containerEdit = (PopupContainerEdit)sender;
            _DMGridCore.setPopupControl(containerEdit);           
            try {
                DataRow row = null;
                if(this.mainGridView.GetFocusedRow()!=null)
                    row = ((DataRowView)this.mainGridView.GetFocusedRow()).Row;
                
                if (row != null)
                {
                    long ret = HelpNumber.ParseInt64(row[columnField]);
                    if (ret < 0) ret = -1;
                    selectId = ret;
                    _DMGridCore.setSelectedID(selectId);
                }
                else
                {
                    selectId = -1;
                    _DMGridCore.setSelectedID(selectId);
                }
                
                _DMGridCore._DMCore.Grid.BestFitColumns();
            }
            catch {
                selectId = -1;
                _DMGridCore.setSelectedID(selectId);
            }
        }

        void RepositoryItemSelectDMGridTemplate_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!isSetValue)
            {
                containerEdit = (PopupContainerEdit)sender;
                if (FilterField != null)
                {
                    switch (_filterType)
                    {
                        case _FilterType.START_WITH:
                            _DMGridCore.Grid.ActiveFilterString = "[" + FilterField + "]" + " Like " + "'" + containerEdit.Text + "%'";
                            break;
                        case _FilterType.CONTAINS:
                            _DMGridCore.Grid.ActiveFilterString = "[" + FilterField + "]" + " Like " + "'%" + containerEdit.Text + "%'";
                            break;
                    }
                }
                containerEdit.ShowPopup();
                containerEdit.Focus();
                isActive = true;
            }
        }

        void popupContainerEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                if (isActive)
                {
                    _DMGridCore.Grid.Focus();
                    try
                    {
                        int index = (_DMGridCore.Grid.GetSelectedRows())[0];
                        _DMGridCore.Grid.FocusedRowHandle = (e.KeyCode == Keys.Down) ? index + 1 : index - 1;
                    }
                    catch { }

                }
            }
        }
        #region GET & SET
        public long _getSelectedID()
        {
            return _DMGridCore.getSelectedID();
        }
        public void _setSelectedID(long id)
        {
            int flag = _DMGridCore.setSelectedID(id);
            if (id != -1)
            {
                if (flag != -1)
                {
                    SetValue(_DMGridCore.getDislayText());
                    return;
                }
            }
            SetValue("");
        }

        private void SetValue(string value)
        {
            isSetValue = true;
            containerEdit.Text = "";
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
            if (_DMGridCore.Grid.OptionsBehavior.Editable == true)
            {
                containerEdit.ShowPopup();
                // containerEdit.Focus();
            }
        }

        private void popupContainerEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            selectId = _DMGridCore.getSelectedID();
            //Trường hợp chưa thao tác chọn trên lưới
            if (selectId == -1 && _DMGridCore.flagNoSelectClick == true)
                _setSelectedID(selectId);            
            
            //Đã có thao tác chọn trên lưới
            if (selectId != -1)
                _setSelectedID(selectId);           

            _DMGridCore.Grid.ActiveFilterString = "";
            isActive = false;
        }

        #endregion
        public RepositoryItemSelectDMGridTemplate(GroupElementType type, GridView gridView, string TableName, string columnField, string IDField, string DislayField, string getField, string[] NameFields, string[] Subjects, string FilterField, ProtocolVN.Framework.Win.DMBasicGrid.InitGridColumns InitGridCol, ProtocolVN.Framework.Win.DMBasicGrid.GetRule Rule, params string[] readOnlyField)
            : this(type, gridView, TableName, columnField, IDField, DislayField, getField, NameFields, Subjects, FilterField, InitGridCol, Rule, null, readOnlyField) { }

        public RepositoryItemSelectDMGridTemplate(GroupElementType type, GridView gridView, string TableName, string columnField, string IDField, string DislayField, string getField, string NameField, string Subject, string FilterField, ProtocolVN.Framework.Win.DMBasicGrid.InitGridColumns InitGridCol, ProtocolVN.Framework.Win.DMBasicGrid.GetRule Rule, params string[] readOnlyField)
            : this(type, gridView, TableName, columnField, IDField, DislayField, getField, new string[] { NameField }, new string[] { Subject }, FilterField, InitGridCol, Rule, null, readOnlyField) { }

        public RepositoryItemSelectDMGridTemplate(GroupElementType type, GridView gridView, string TableName, string columnField, params string[] readOnlyField)
            : this(type, gridView, TableName, columnField, "ID", "NAME", "ID", new string[] { "NAME" }, new string[] { "Tên" }, "NAME", null, null, null, readOnlyField) { }

        static RepositoryItemSelectDMGridTemplate()
        {
            Register();
        }

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(RepositoryName, typeof(PopupContainerEdit), typeof(RepositoryItemSelectDMGridTemplate), typeof(DevExpress.XtraEditors.ViewInfo.PopupContainerEditViewInfo),
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
