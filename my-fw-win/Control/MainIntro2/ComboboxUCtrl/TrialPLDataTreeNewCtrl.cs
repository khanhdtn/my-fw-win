using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using ProtocolVN.Framework.Win;

using DevExpress.XtraTreeList;

namespace ProtocolVN.Framework.Win
{
    public partial class UserControlDataTree : DevExpress.XtraEditors.XtraUserControl
    {
        private int[] RootID;
        private string order = "";
        private XtraForm danhMucForm;
        private PopupContainerEdit popupContainerEdit;
        private GridView gridView;
        private TreeList treeList;
        public static object valueFocus;
        private string tableName;
        private static string fieldGet;
        private string IDField;
        private string columnField;

        public PopupContainerEdit PopupContainerEdit
        {
            set { popupContainerEdit = value; }
        }

        public GridView GridView
        {
            set { gridView = value; }
        }
        public TreeList TreeList
        {
            set { treeList = value; }
        }

        public TreeList TreeListEditor
        {
            set { treeList1 = value;}
            get{return treeList1;}
        }
        public void _focusRow()
        {
            if (valueFocus != null)
                treeList1.FocusedNode = treeList1.FindNodeByFieldValue(fieldGet , valueFocus);
        }

        public UserControlDataTree()
        {
            InitializeComponent();
            treeList1.OptionsMenu.EnableColumnMenu = false;
            treeList1.OptionsMenu.EnableFooterMenu = false;
            treeList1.KeyDown += new KeyEventHandler(treeList_KeyDown);            
        }

        void treeList_KeyDown(object sender , KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                _selectValue();
        }

        public void _selectValue()
        {
            TreeListNode lstNode = treeList1.FocusedNode;
            object value = lstNode.GetValue(fieldGet);
            if (popupContainerEdit != null)//Su dung tren form
            {
                popupContainerEdit.EditValue =value;
                popupContainerEdit.ClosePopup();
            }
            else // Su dung tren grid
            {
                TrialPLDataTreeNew.isClosePopup = true;
                if(gridView!=null)
                    gridView.SetRowCellValue(gridView.FocusedRowHandle , gridView.FocusedColumn ,value);
                else if (treeList != null)
                {
                    TreeListNode node = treeList.FocusedNode;
                    DataRow row = ((DataRowView)treeList.GetDataRecordByNode(node)).Row;
                    row[columnField+fieldGet] = value;
                    row[columnField] = lstNode.GetValue(IDField);
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(columnField);
                }

            }
        }
        public void _nonSelectValue()
        {
            if (popupContainerEdit != null)//Su dung tren form
            {
                popupContainerEdit.EditValue = popupContainerEdit.Properties.NullText;
                popupContainerEdit.ClosePopup();
            }
            else // Su dung tren grid
            {
                TrialPLDataTreeNew.isClosePopup = true;
                if(gridView!=null)
                    gridView.SetRowCellValue(gridView.FocusedRowHandle , gridView.FocusedColumn , "");
                else if (treeList != null)
                {
                    TreeListNode node = treeList.FocusedNode;
                    DataRow row = ((DataRowView)treeList.GetDataRecordByNode(node)).Row;
                    row[columnField +fieldGet] = "";
                    row[columnField] = -1;
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(columnField);
                }
            }
        }

        public int _getId(object value)
        {
            TreeListNode lstNode = treeList1.FindNodeByFieldValue(fieldGet , value);
            try
            {
                int id = int.Parse(lstNode.GetValue(IDField).ToString());
                return id;
            }
            catch { }
            return -1;
        }
        private void _initTree( string IDField , string IDParentField , string[] VisibleFields , string[] Captions)
        {
            treeList1.Nodes.Clear();
            TreeListColumn colID = treeList1.Columns.Add();
            colID.Caption = IDField;
            colID.FieldName = IDField;
            colID.Name = IDField;
            colID.VisibleIndex = -1;
            colID.OptionsColumn.AllowFocus = false;
            this.order = VisibleFields[0].ToString();
            //Khởi tạo các cột Visible
            for (int i = 0 ; i < VisibleFields.Length ; i++)
            {
                TreeListColumn col = treeList1.Columns.Add();
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.Caption = Captions[i];
                col.FieldName = VisibleFields[i];
                col.Name = i.ToString();
                col.VisibleIndex = i;
            }
            //Thuộc tính của TreeList
            treeList1.KeyFieldName = IDField;
            treeList1.ParentFieldName = IDParentField;
            treeList1.OptionsBehavior.Editable = false;
        }

        public void _BuildTree(XtraForm danhMucForm ,string columnField, string TableName , int[] RootID , string IDField , string IDParentField , string[] VisibleFields , string[] Captions, string getField)
        {
            this._initTree(IDField , IDParentField , VisibleFields , Captions);
            this.danhMucForm = danhMucForm;
            this.IDField = IDField;
            this.tableName = TableName;
            fieldGet = getField;
            this.RootID = RootID;
            this.columnField = columnField;
            DataTable dt = LoadTable(TableName , RootID);
            treeList1.DataSource = dt;
        }
        public void _BuildTree(XtraForm danhMucForm , string TableName , int[] RootID , string IDField , string IDParentField , string[] VisibleFields , string[] Captions , string getField)
        {
            this._initTree(IDField , IDParentField , VisibleFields , Captions);
            this.danhMucForm = danhMucForm;
            this.IDField = IDField;
            this.tableName = TableName;
            fieldGet = getField;
            this.RootID = RootID;
            DataTable dt = LoadTable(TableName , RootID);
            treeList1.DataSource = dt;
        }
        private DataTable LoadTable(string TableName , int[] RootID)
        {
            QueryBuilder query = new QueryBuilder("SELECT * FROM " + TableName + " WHERE 1=1");
            if (RootID != null)
            {
                string ids = "";
                for (int i = 0 ; i < RootID.Length ; i++)
                {
                    ids += RootID[i].ToString() + ",";
                    //query.addID(PLFN.ID_ROOT, RootID[i]);
                    //query.addID(this.ParentFieldName, RootID[i]);
                }
                query.addCondition(GlobalConst.ID_ROOT + " in (" + ids + "0)");
            }
            else
            {
                //query.addCondition(this.ParentFieldName + " is null ");
                //query.addID(this.ParentFieldName, -1);
            }
            query.setAscOrderBy(this.order);
            DataSet ds = DABase.getDatabase().LoadDataSet(query , TableName);
            return ds.Tables[0];
        }

        private void btnDanhMuc_Click(object sender , EventArgs e)
        {
            TrialPLDataTreeNew.isClosePopup = false;
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm , danhMucForm);
            this.treeList1.DataSource = _loadData();
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

        private void treeList1_DoubleClick(object sender , EventArgs e)
        {
            _selectValue();
        }

    }
}
