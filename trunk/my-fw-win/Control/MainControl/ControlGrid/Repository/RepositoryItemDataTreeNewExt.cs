using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Accessibility;
using System.Data;
using DevExpress.XtraTreeList;

namespace ProtocolVN.Framework.Win
{
    [UserRepositoryItem("Register")]
    public class RepositoryItemDataTreeNewExt : RepositoryItemPopupContainerEdit
    {
        public const string ControlName = "PLDataTreeNew";
        private UserControlDataTreeExt dataTree;
        private PopupContainerControl popupControl;
        static RepositoryItemDataTreeNewExt()
        {
            Register();
        }
        private void init()
        {
            dataTree = new UserControlDataTreeExt();
            popupControl = new PopupContainerControl();
            this.PopupControl = popupControl;
            popupControl.Controls.Add(dataTree);
            popupControl.Size = dataTree.Size;
            this.PopupSizeable = false;
            this.ShowPopupCloseButton = false;
            this.Popup += new EventHandler(RepositoryDataTree_Popup);
            this.Leave += new EventHandler(RepositoryDataTree_Leave);
        }

        #region Su dung tren Gridview
        public RepositoryItemDataTreeNewExt(XtraForm danhMucForm , GridView gridView ,string columnField, string TableName , int[] RootID , string ValueField , string IDParentField , string[] VisibleFields , string[] Captions , string getField)
        {
            init();
            dataTree.GridView = gridView;
            dataTree._BuildTree(danhMucForm ,columnField, TableName , RootID ,ValueField , IDParentField , VisibleFields , Captions , getField);
            int idValue ;

            gridView.GotFocus += delegate(object sender , EventArgs e)
            {
                if (gridView.DataSource != null &&
                   !((DataView)gridView.DataSource).Table.Columns.Contains(columnField + getField))
                {
                    ((DataView)gridView.DataSource).Table.Columns.Add(new DataColumn(columnField + getField));
                }
            };
            gridView.CellValueChanged += delegate(object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField+getField))
                {
                    idValue = _getId(e.Value);
                    if (e.RowHandle < 0)
                    {
                        gridView.EditingValue = idValue;
                        gridView.SetRowCellValue(e.RowHandle , columnField , idValue);
                    }
                    else
                    {
                        DataRow dr = gridView.GetDataRow(e.RowHandle);
                        dr[columnField] = idValue;
                    }
                }
            };
            gridView.InitNewRow +=delegate(object sender , InitNewRowEventArgs e)
            {
                gridView.SetRowCellValue(e.RowHandle , columnField + getField , dataTree.TreeListEditor.FocusedNode.GetValue(getField));
            };
            
        }
        public RepositoryItemDataTreeNewExt(XtraForm danhMucForm, GridView gridView, string btnCaption, UserControlDataTreeExt.GetValue function, string columnField, string TableName, int[] RootID, string ValueField, string IDParentField, string[] VisibleFields, string[] Captions, string getField)
        {
            init();
            dataTree.GridView = gridView;
            dataTree._BuildTree(danhMucForm,btnCaption,function, columnField, TableName, RootID, ValueField, IDParentField, VisibleFields, Captions, getField);
            int idValue;

            gridView.GotFocus += delegate(object sender, EventArgs e)
            {
                if (gridView.DataSource != null &&
                   !((DataView)gridView.DataSource).Table.Columns.Contains(columnField + getField))
                {
                    ((DataView)gridView.DataSource).Table.Columns.Add(new DataColumn(columnField + getField));
                }
            };
            gridView.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    idValue = _getId(e.Value);
                    if (e.RowHandle < 0)
                    {
                        gridView.EditingValue = idValue;
                        gridView.SetRowCellValue(e.RowHandle, columnField, idValue);
                    }
                    else
                    {
                        DataRow dr = gridView.GetDataRow(e.RowHandle);
                        dr[columnField] = idValue;
                    }
                }
            };
            gridView.InitNewRow += delegate(object sender, InitNewRowEventArgs e)
            {
                gridView.SetRowCellValue(e.RowHandle, columnField + getField, dataTree.TreeListEditor.FocusedNode.GetValue(getField));
            };

        }
        #endregion

        #region Su dung tren TreeList
        public RepositoryItemDataTreeNewExt(XtraForm danhMucForm, TreeList treeList, string columnField, string TableName, int[] RootID, string ValueField, string IDParentField, string[] VisibleFields, string[] Captions, string getField)
        {
            init();
            dataTree.TreeList = treeList;
            dataTree._BuildTree(danhMucForm ,columnField, TableName , RootID , ValueField , IDParentField , VisibleFields , Captions , getField);
            treeList.CellValueChanged += delegate(object sender , CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    DataRow dr = ((DataRowView)treeList.GetDataRecordByNode(e.Node)).Row;
                    dr[columnField] = _getId(e.Value);
                }
            };
            treeList.GotFocus += delegate(object sender , EventArgs e)
            {
                if (treeList.DataSource != null &&
                        !((DataTable)treeList.DataSource).Columns.Contains(columnField + getField))
                {
                    ((DataTable)treeList.DataSource).Columns.Add(new DataColumn(columnField + getField));
                }
            };

        }
        public RepositoryItemDataTreeNewExt(XtraForm danhMucForm, TreeList treeList,string btnCaption, UserControlDataTreeExt.GetValue function, string columnField, string TableName, int[] RootID, string ValueField, string IDParentField, string[] VisibleFields, string[] Captions, string getField)
        {
            init();
            dataTree.TreeList = treeList;
            dataTree._BuildTree(danhMucForm,btnCaption,function, columnField, TableName, RootID, ValueField, IDParentField, VisibleFields, Captions, getField);
            treeList.CellValueChanged += delegate(object sender, CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    DataRow dr = ((DataRowView)treeList.GetDataRecordByNode(e.Node)).Row;
                    dr[columnField] = _getId(e.Value);
                }
            };
            treeList.GotFocus += delegate(object sender, EventArgs e)
            {
                if (treeList.DataSource != null &&
                        !((DataTable)treeList.DataSource).Columns.Contains(columnField + getField))
                {
                    ((DataTable)treeList.DataSource).Columns.Add(new DataColumn(columnField + getField));
                }
            };

        }
        #endregion
        void RepositoryDataTree_Leave(object sender , EventArgs e)
        {
            TrialPLDataTreeNewExt.isClosePopup = true;
        }

        void RepositoryDataTree_Popup(object sender , EventArgs e)
        {
            dataTree._focusRow();
        }
       
        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(ControlName ,
                                        typeof(TrialPLDataTreeNewExt), typeof(RepositoryItemDataTreeNewExt),
                                        typeof(DevExpress.XtraEditors.ViewInfo.PopupContainerEditViewInfo) ,
                                        new DevExpress.XtraEditors.Drawing.ButtonEditPainter() ,
                                        true , null ,typeof(PopupEditAccessible)));
        }
        
        public override string EditorTypeName
        {
            get
            {
                return ControlName;
            }
        }
        public int _getId(object value)
        {
            return dataTree._getId(value);
        }
    }
}
