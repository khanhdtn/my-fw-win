using System;
using System.Data;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    [UserRepositoryItem("Register")]
    public class RepositoryComboboxAdd : RepositoryItemComboBox
    {
        #region Member
        private string tableName;
        private string genName;
        private string idField;
        private string displayField;
        private string ValueField;
        private GridView gridView = null;
        private TreeList treeList = null;
        private string newValue;
        private int rowFocus;
        private TreeListNode nodeFocus;
       
        #endregion

        #region Contructor
        /// <summary>
        /// Sử dụng để thêm danh mục vào database khi người dùng nhập vào một danh mục mới
        /// Sau khi truyền tham số , khởi tạo bằng cách gọi hàm _init()
        /// </summary>
        /// <param name="TableName">Ten table danh muc</param>
        /// <param name="ColumnName">Ten column co noi dung duoc add vao ComboBox</param>
        /// <param name="GenName">Ten generator id cua danh muc moi khi them vao</param>
        public RepositoryComboboxAdd():base()
        {
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(RepositoryItemComboBoxAdd_KeyDown);
        }
        public RepositoryComboboxAdd(string TableName, string IDField, string ValueField, string DisplayField, string GenName, GridView gridView)
            : this()
        {
            this.tableName = TableName;
            this.genName = GenName;
            this.gridView = gridView;
            this.displayField = DisplayField;
            this.idField = IDField;
            this.ValueField = ValueField;
        }
        public RepositoryComboboxAdd(string TableName, string IDField, string ValueField, string DisplayField, string GenName, TreeList treeList)
            : this()
        {
            this.tableName = TableName;
            this.genName = GenName;
            this.treeList = treeList;
            this.displayField = DisplayField;
            this.idField = IDField;
            this.ValueField = ValueField;
            try
            {
                nodeFocus = treeList.Nodes[0];
            }
            catch { }
        }
        #endregion

        #region EventHandler

        void RepositoryItemComboBoxAdd_KeyDown(object sender , System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == (System.Windows.Forms.Keys.Insert | System.Windows.Forms.Keys.Control))
            {
                string value = ((ComboBoxEdit)sender).Text.Trim();
                if ( value!="" ) InsertItems(value);
                ((ComboBoxEdit)sender).ClosePopup();
                if (gridView != null)
                {
                    gridView.FocusedColumn = gridView.Columns.ColumnByFieldName(idField);
                    gridView.FocusedColumn = gridView.Columns.ColumnByFieldName(idField + displayField);
                }
                else if (treeList != null)
                {
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(idField);
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(idField + displayField);
                }
            }
            else if (e.KeyData == (System.Windows.Forms.Keys.Delete | System.Windows.Forms.Keys.Control))
            {
                string value = ((ComboBoxEdit)sender).Text.Trim();
                if (value != "") DeleteItems(value);
                ((ComboBoxEdit)sender).ClosePopup();
                if (gridView != null)
                {
                    gridView.FocusedColumn = gridView.Columns.ColumnByFieldName(idField);
                    gridView.FocusedRowHandle = rowFocus;
                }
                else if (treeList != null)
                {
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(idField);
                    treeList.FocusedColumn = treeList.Columns.ColumnByFieldName(idField + displayField);
                }
            }

        }

        #endregion

        #region Properties

        public string TableName
        {
            set
            {
                tableName = value;
            }
        }
        public string IDField
        {
            set
            {
                idField = value;
            }
        }
        public string DisplayField
        {
            set { displayField = value; }
        }
        public string GenName
        {
            set
            {
                genName = value;
            }
        }

        #endregion
      
        #region Function
        private void LoadAllItems()
        {
            DataTable dt = HelpDanhMucDB.LoadData(tableName, ValueField, displayField);
            this.Items.Clear();
            foreach (DataRow dr in dt.Rows)
                this.Items.Add(new ItemData(HelpNumber.ParseInt64(dr[0]) , dr[1].ToString()));
        }

        private void _refresh()
        {
            try
            {
                LoadAllItems();
                if (gridView != null)
                {
                    DataRow row = gridView.GetDataRow(rowFocus);
                    row[idField] = getID(newValue);
                }
                else if (treeList != null)
                {
                    DataRowView view = (DataRowView)treeList.GetDataRecordByNode(nodeFocus);
                    DataRow row = view.Row;
                    row[idField] = getID(newValue);
                }
            }
            catch { }
        }
        
        private long getID(string value)
        {
            for (int i = 0 ; i < this.Items.Count ; i++)
            {
                ItemData data = (ItemData)this.Items[i];
                if (data.Name == value)
                    return data.ID;
            }
            return -1;
        }

        private void InsertItems(string value)
        {
            if (!Exist(value))
            {
                long id = HelpDanhMucDB.InsertItem(tableName, genName, value, ValueField, displayField);
                if (id > 0)
                {
                    this.Items.Add(new ItemData(id, value));
                }
                //_refresh();
            }
        }

        private void DeleteItems(string value)
        {
            long id = getID(value);
            if (HelpDanhMucDB.DeleteItem(tableName, ValueField, id))
            {
                this.Items.Remove(new ItemData(id, value));
            }
            //_refresh();

            if (gridView != null)
            {
                gridView.SetRowCellValue(rowFocus, gridView.Columns.ColumnByFieldName(idField + displayField), "");
                gridView.SetRowCellValue(rowFocus , gridView.Columns.ColumnByFieldName(idField) , -1);
            }
            else if (treeList != null)
            {
                DataRowView view = (DataRowView)treeList.GetDataRecordByNode(nodeFocus);
                DataRow row = view.Row;
                row[idField + displayField] = "";
                row[idField] = -1;
            }

        }

        private bool Exist(string value)
        {
            for (int i = 0 ; i < this.Items.Count ; i++)
            {
                ItemData data = (ItemData)this.Items[i];
                if (data.Name == value)
                    return true;
            }
            return false;
        }

        #endregion

        #region Init

        public void _init()
        {
            LoadAllItems();
            if (gridView != null)
            {
                long idValue = -1;
                gridView.CellValueChanging += delegate(object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
                {
                    //PHUOC: Tao cot ao neu chua co                    
                    if(gridView.DataSource != null &&
                        !((DataView)gridView.DataSource).Table.Columns.Contains(idField + displayField))
                    {
                        ((DataView)gridView.DataSource).Table.Columns.Add(new DataColumn(idField + displayField));
                    }

                    if ((idField + displayField).Equals(e.Column.FieldName))
                    {
                        if (e.Value is ItemData)
                            idValue = ((ItemData)e.Value).ID;
                        else
                        {
                            idValue = -1;
                            newValue = e.Value.ToString();
                        }
                        if(e.RowHandle < 0)
                            gridView.SetRowCellValue(e.RowHandle , idField , idValue);
                        else
                        {
                            DataRow row = gridView.GetDataRow(rowFocus);
                            row[idField] = idValue;
                        }
                    }
                };
                gridView.InitNewRow += delegate(object sender , InitNewRowEventArgs e)
                {
                    gridView.SetRowCellValue(e.RowHandle , idField , idValue);
                };
                gridView.FocusedRowChanged += delegate(object sender , FocusedRowChangedEventArgs e)
                {
                    rowFocus = e.FocusedRowHandle;
                };
            }
            else if (treeList != null)
            {
                treeList.CellValueChanging += delegate(object sender , DevExpress.XtraTreeList.CellValueChangedEventArgs e)
                {
                   
                    if ((idField + displayField).Equals(e.Column.FieldName))
                    {
                        nodeFocus = e.Node;
                        DataRowView view = (DataRowView)treeList.GetDataRecordByNode(e.Node);
                        DataRow row = view.Row;
                        if (e.Value is ItemData)
                            row[idField] = ((ItemData)e.Value).ID;
                        else
                        {
                            row[idField] = -1;
                            newValue = e.Value.ToString();
                        }
                    }
                };
                treeList.FocusedNodeChanged += delegate(object sender , FocusedNodeChangedEventArgs e)
                {
                    nodeFocus = e.Node;
                };
                treeList.GotFocus += delegate(object sender , EventArgs e)
                {
                    if (treeList.DataSource != null &&
                     !((DataTable)treeList.DataSource).Columns.Contains(idField + displayField))
                    {
                        ((DataTable)treeList.DataSource).Columns.Add(new DataColumn(idField + displayField));
                    }
                };
            }
        }

        #endregion

        #region Đăng kí RepositoryItem
        static RepositoryComboboxAdd()
        {
            Register();
        }
        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(ComboBoxName ,
                                        typeof(ComboBoxEdit) , typeof(RepositoryComboboxAdd) ,
                                        typeof(DevExpress.XtraEditors.ViewInfo.ComboBoxViewInfo) ,
                                        new DevExpress.XtraEditors.Drawing.ButtonEditPainter() ,
                                        true , null , typeof(ComboBoxEdit)));
        }
        public override bool ImmediatePopup
        {
            get
            {
                return true;
            }
        }
        public const string ComboBoxName = "RepositoryComboBoxAdd";
        public override string EditorTypeName
        {
            get
            {
                return ComboBoxName;
            }
        }
        #endregion
    } 
}

