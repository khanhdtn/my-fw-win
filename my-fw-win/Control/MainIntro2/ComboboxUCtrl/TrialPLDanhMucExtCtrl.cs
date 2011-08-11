using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;


using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

using ProtocolVN.Framework.Core;


namespace ProtocolVN.Framework.Win
{
    //public interface IDanhMucAdv
    //{
    //    int _getId(object value);
    //    void _focusRow();
    //    void _selectValue();
    //    void _nonSelectValue();
    //}

    public partial class TrialPLDanhMucExtCtrl : DevExpress.XtraEditors.XtraUserControl,IDanhMucAdv
    {
        private XtraForm danhMucForm;
        private PopupContainerEdit popupContainerEdit;
        private GridView gridView=null;
        private TreeList treeList=null;
        public static object valueFocus;
        private string getField;
        private string  tableName;
        private string valueField;
        private string columnField;
        public delegate void GetValue(int idSelect);
        GetValue formValue;

        public PopupContainerEdit PopupContainerEdit
        {
            set { popupContainerEdit = value; }
        }
        public GridView GridView
        {
            set { gridView = value; }
        }
        public GridView GridViewEditor
        {
            set { gridView1 = value; }
            get { return gridView1; }
        }
        public TreeList TreeList
        {
            set { treeList = value; }
        }
        public TrialPLDanhMucExtCtrl()
        {
            InitializeComponent();
        }
        public void _focusRow()
        {
            if (valueFocus != null)
            {
                DataView view = gridView1.DataSource as DataView;
                int i = _getRowHandle(view , valueFocus);
                gridView1.FocusedRowHandle = i;
            }
        }
        int _getRowHandle(DataView view,object value)
        {
            try
            {
                if (view != null)
                {
                    for (int i = 0 ; i < view.Table.Rows.Count ; i++)
                    {
                        object valueCompare = view.Table.Rows[i][getField];
                        if (value.Equals(valueCompare)) return i;
                    }
                }
            }
            catch { }
            return -1;
        }
        private void _initGridView(string fieldID, string[] visibleField, string[] caption)
        {
            GridColumn colID = new GridColumn();
            colID.FieldName = fieldID;
            gridView1.Columns.Add(colID);
            for(int i=0 ;i<visibleField.Length;i++)
            {
                GridColumn col = new GridColumn();
                col.Name = visibleField[i];
                col.FieldName = visibleField[i];
                col.Caption = caption[i];
                col.Visible = true;
                col.VisibleIndex = i;
                gridView1.Columns.Add(col);
            }
        }

        #region Su dung tren gridView, treeList
        public void _init(XtraForm frmDanhMuc, string btnCaption, GetValue function,string columnField,string tableName,string valueField, string[] visibleField, string[] caption,string getField)
        {
            this.tableName = tableName;
            this.danhMucForm = frmDanhMuc;
            this.getField = getField;
            this.valueField = valueField;
            this.columnField = columnField;
            this.btnExtend.Text = btnCaption;
            this.btnExtend.Width = btnExtend.CalcBestSize().Width;
            this.btnExtend.Visible = true;
            this.formValue = function;
            _initGridView(valueField , visibleField , caption);
            this.gridControl1.DataSource = _loadData();
            if(popupContainerEdit!=null)//Su dung tren form
                popupContainerEdit.Properties.NullText = GlobalConst.NULL_TEXT;
        }
        public void _init(XtraForm frmDanhMuc, string columnField, string tableName, string valueField, string[] visibleField, string[] caption, string getField)
        {
            this.tableName = tableName;
            this.danhMucForm = frmDanhMuc;
            this.getField = getField;
            this.valueField = valueField;
            this.columnField = columnField;
            this.btnExtend.Visible = false;
            _initGridView(valueField, visibleField, caption);
            this.gridControl1.DataSource = _loadData();
            if (popupContainerEdit != null)//Su dung tren form
                popupContainerEdit.Properties.NullText = GlobalConst.NULL_TEXT;
        }
        #endregion

        #region Su dung tren form
        public void _init(XtraForm frmDanhMuc , string tableName , string valueField , string[] visibleField , string[] caption , string getField)
        {
            this.tableName = tableName;
            this.danhMucForm = frmDanhMuc;
            this.getField = getField;
            this.valueField = valueField;
            _initGridView(valueField , visibleField , caption);
            this.gridControl1.DataSource = _loadData();
            if (popupContainerEdit != null)//Su dung tren form
                popupContainerEdit.Properties.NullText = GlobalConst.NULL_TEXT;
        }
        public void _init(XtraForm frmDanhMuc,string btnCaption, GetValue function, string tableName, string valueField, string[] visibleField, string[] caption, string getField)
        {
            this.tableName = tableName;
            this.danhMucForm = frmDanhMuc;
            this.getField = getField;
            this.valueField = valueField;
            this.btnExtend.Text = btnCaption;
            this.btnExtend.Width = this.btnExtend.CalcBestSize().Width;
            this.btnExtend.Visible = true;
            this.formValue = function;
            _initGridView(valueField, visibleField, caption);
            this.gridControl1.DataSource = _loadData();
            if (popupContainerEdit != null)//Su dung tren form
                popupContainerEdit.Properties.NullText = GlobalConst.NULL_TEXT;
        }
        #endregion

        private void btnDanhMuc_Click(object sender , EventArgs e)
        {
            TrialPLDanhMucExt.isClosePopup = false;
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm,danhMucForm);
            this.gridControl1.DataSource = _loadData();
        }

        private DataTable _loadData()
        {
            DataSet ds = DABase.getDatabase().LoadTable(tableName);
            return ds.Tables[0];
        }

        private void btnChon_Click(object sender , EventArgs e)
        {
            _selectValue();
        }

        private void btnBoChon_Click(object sender , EventArgs e)
        {
           _nonSelectValue();
        }

        public int _getId(object value)
        {
            try
            {
                DataView view = gridView1.DataSource as DataView;
                DataRow dr = gridView1.GetDataRow(_getRowHandle(view , value));
                return int.Parse(dr[0].ToString());
            }
            catch { }
            return -1;
        }

        public void _selectValue()
        {
            DataRow dr = this.gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (popupContainerEdit != null)//Su dung tren form
            {
                popupContainerEdit.EditValue = dr[getField].ToString();
                popupContainerEdit.ClosePopup();
            }
            else // Su dung tren grid
            {
                TrialPLDanhMucExt.isClosePopup = true;
                if (gridView != null)
                    gridView.SetRowCellValue(gridView.FocusedRowHandle , gridView.FocusedColumn , dr[getField]);
                else if (treeList != null)
                {
                    TreeListNode node = treeList.FocusedNode;
                    DataRow row = ((DataRowView)treeList.GetDataRecordByNode(node)).Row;
                    row[columnField + getField] = dr[getField].ToString();
                    row[columnField] = dr[valueField].ToString();
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(columnField);
                }
            }
        }

        public void _nonSelectValue()
        {
            if (popupContainerEdit != null)//Su dung tren form
            {
                popupContainerEdit.EditValue =  popupContainerEdit.Properties.NullText;
                popupContainerEdit.ClosePopup();
            }
            else // Su dung tren grid
            {
                TrialPLDanhMucExt.isClosePopup = true;
                if(gridView!=null)
                    gridView.SetRowCellValue(gridView.FocusedRowHandle , gridView.FocusedColumn , "");
                else if(treeList!=null)
                {
                    TreeListNode node = treeList.FocusedNode;
                    DataRow row = ((DataRowView)treeList.GetDataRecordByNode(node)).Row;
                    row[columnField+getField] = "";
                    row[columnField] = -1;
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(columnField);
                }
            }
        }

        private void gridView1_DoubleClick(object sender , EventArgs e)
        {
            _selectValue();
        }

        private void gridView1_KeyDown(object sender , KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                _selectValue();
        }

        private void btnExtend_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            formValue(HelpNumber.ParseInt32(dr[valueField].ToString()));
        }
    }
}
