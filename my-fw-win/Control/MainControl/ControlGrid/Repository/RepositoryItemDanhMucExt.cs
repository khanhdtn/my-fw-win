using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.Accessibility;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;

namespace ProtocolVN.Framework.Win
{
    [UserRepositoryItem("Register")]
    public class RepositoryItemDanhMucExt:RepositoryItemPopupContainerEdit
    {
        public const string ControlName = "PLDanhMucAdv";
        private TrialPLDanhMucExtCtrl plDanhMuc;
        private PopupContainerControl popupControl;
        static RepositoryItemDanhMucExt()
        {
            Register();
        }
        private void init()
        {
            plDanhMuc = new TrialPLDanhMucExtCtrl();
            popupControl = new PopupContainerControl();
            this.PopupControl = popupControl;
            popupControl.Controls.Add(plDanhMuc);
            popupControl.Size = plDanhMuc.Size;
            this.PopupSizeable = false;
            this.ShowPopupCloseButton = false;
        }

        #region Su dung tren grid
        /// <summary>
        /// Sử dụng danh mục Advance
        /// </summary>
        /// <param name="frmDanhMuc"></param>
        /// <param name="columnField"></param>
        /// <param name="tableName"></param>
        /// <param name="ValueField"></param>
        /// <param name="visibleField"></param>
        /// <param name="caption"></param>
        /// <param name="getField"></param>
        /// <param name="gridView"></param>
        public RepositoryItemDanhMucExt(XtraForm frmDanhMuc ,string columnField, string tableName , string ValueField , string[] visibleField , string[] caption , string getField , GridView gridView)
        {
            init();
            plDanhMuc.GridView = gridView;
            plDanhMuc._init(frmDanhMuc ,columnField, tableName ,ValueField , visibleField , caption , getField);

            gridView.GotFocus += delegate(object sender , EventArgs e)
            {
                if (gridView.DataSource != null &&
                    !((DataView)gridView.DataSource).Table.Columns.Contains(columnField + getField))
                {
                    ((DataView)gridView.DataSource).Table.Columns.Add(new DataColumn(columnField + getField));
                }
            };
            int idValue;
            gridView.CellValueChanged += delegate(object sender , DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                
                if (e.Column.FieldName.Equals(columnField+getField))
                {
                    idValue = _getId(e.Value);
                    if (e.RowHandle < 0)
                    {
                        gridView.EditingValue = idValue;
                        gridView.SetFocusedRowCellValue(gridView.Columns.ColumnByFieldName(columnField) , idValue);
                    }
                    else
                    {
                        DataRow row = gridView.GetDataRow(e.RowHandle);
                        row[columnField] = idValue;
                    }
                }
                
            };

            gridView.InitNewRow += delegate(object sender , InitNewRowEventArgs e)
            {
                  gridView.SetRowCellValue(e.RowHandle , columnField + getField , plDanhMuc.GridViewEditor.FocusedValue);
            };
            this.Popup += new EventHandler(RepositoryHuyDanhMuc_Popup);
            this.Leave += new EventHandler(RepositoryDanhMucAdv_Leave);
        }

        /// <summary>
        /// Sử dụng danh mục extend
        /// </summary>
        /// <param name="frmDanhMuc"></param>
        /// <param name="btnCaption"></param>
        /// <param name="function"></param>
        /// <param name="columnField"></param>
        /// <param name="tableName"></param>
        /// <param name="ValueField"></param>
        /// <param name="visibleField"></param>
        /// <param name="caption"></param>
        /// <param name="getField"></param>
        /// <param name="gridView"></param>
        public RepositoryItemDanhMucExt(XtraForm frmDanhMuc, string btnCaption , TrialPLDanhMucExtCtrl.GetValue function, string columnField, string tableName, string ValueField, string[] visibleField, string[] caption, string getField, GridView gridView)
        {
            init();
            plDanhMuc.GridView = gridView;
            plDanhMuc._init(frmDanhMuc, btnCaption, function, columnField, tableName, ValueField, visibleField, caption, getField);

            gridView.GotFocus += delegate(object sender, EventArgs e)
            {
                if (gridView.DataSource != null &&
                    !((DataView)gridView.DataSource).Table.Columns.Contains(columnField + getField))
                {
                    ((DataView)gridView.DataSource).Table.Columns.Add(new DataColumn(columnField + getField));
                }
            };
            int idValue;
            gridView.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {

                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    idValue = _getId(e.Value);
                    if (e.RowHandle < 0)
                    {
                        gridView.EditingValue = idValue;
                        gridView.SetFocusedRowCellValue(gridView.Columns.ColumnByFieldName(columnField), idValue);
                    }
                    else
                    {
                        DataRow row = gridView.GetDataRow(e.RowHandle);
                        row[columnField] = idValue;
                    }
                }

            };

            gridView.InitNewRow += delegate(object sender, InitNewRowEventArgs e)
            {
                gridView.SetRowCellValue(e.RowHandle, columnField + getField, plDanhMuc.GridViewEditor.FocusedValue);
            };
            this.Popup += new EventHandler(RepositoryHuyDanhMuc_Popup);
            this.Leave += new EventHandler(RepositoryDanhMucAdv_Leave);
        }

        #endregion

        #region Sử dụng trên treelist
        /// <summary>
        /// Sử dụng danh mục Advance
        /// </summary>
        /// <param name="frmDanhMuc"></param>
        /// <param name="columnField"></param>
        /// <param name="tableName"></param>
        /// <param name="ValueField"></param>
        /// <param name="visibleField"></param>
        /// <param name="caption"></param>
        /// <param name="getField"></param>
        /// <param name="treeList"></param>
        public RepositoryItemDanhMucExt(XtraForm frmDanhMuc, string columnField, string tableName, string ValueField, string[] visibleField, string[] caption, string getField, TreeList treeList)
        {
            init();
            plDanhMuc.TreeList = treeList;
            plDanhMuc._init(frmDanhMuc ,columnField, tableName , ValueField , visibleField , caption , getField);

            treeList.GotFocus += delegate(object sender , EventArgs e)
            {
                if (treeList.DataSource != null &&
                        !((DataTable)treeList.DataSource).Columns.Contains(columnField + getField))
                {
                    ((DataTable)treeList.DataSource).Columns.Add(new DataColumn(columnField + getField));
                }
            };
            treeList.CellValueChanged += delegate(object sender , CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField+getField))
                {
                    DataRowView rowView = (DataRowView)treeList.GetDataRecordByNode(e.Node);
                    DataRow row = rowView.Row;
                    row[columnField] = _getId(e.Value);
                }
            };
            this.Popup += new EventHandler(RepositoryHuyDanhMuc_Popup);
            this.Leave += new EventHandler(RepositoryDanhMucAdv_Leave);
        }
        /// <summary>
        /// Sử dụng danh mục Extend
        /// </summary>
        /// <param name="frmDanhMuc"></param>
        /// <param name="btnCaption"></param>
        /// <param name="function"></param>
        /// <param name="columnField"></param>
        /// <param name="tableName"></param>
        /// <param name="ValueField"></param>
        /// <param name="visibleField"></param>
        /// <param name="caption"></param>
        /// <param name="getField"></param>
        /// <param name="treeList"></param>
        public RepositoryItemDanhMucExt(XtraForm frmDanhMuc,string btnCaption, TrialPLDanhMucExtCtrl.GetValue function, string columnField, string tableName, string ValueField, string[] visibleField, string[] caption, string getField, TreeList treeList)
        {
            init();
            plDanhMuc.TreeList = treeList;
            plDanhMuc._init(frmDanhMuc,btnCaption,function, columnField, tableName, ValueField, visibleField, caption, getField);

            treeList.GotFocus += delegate(object sender, EventArgs e)
            {
                if (treeList.DataSource != null &&
                        !((DataTable)treeList.DataSource).Columns.Contains(columnField + getField))
                {
                    ((DataTable)treeList.DataSource).Columns.Add(new DataColumn(columnField + getField));
                }
            };
            treeList.CellValueChanged += delegate(object sender, CellValueChangedEventArgs e)
            {
                if (e.Column.FieldName.Equals(columnField + getField))
                {
                    DataRowView rowView = (DataRowView)treeList.GetDataRecordByNode(e.Node);
                    DataRow row = rowView.Row;
                    row[columnField] = _getId(e.Value);
                }
            };
            this.Popup += new EventHandler(RepositoryHuyDanhMuc_Popup);
            this.Leave += new EventHandler(RepositoryDanhMucAdv_Leave);
        }
        #endregion

        void RepositoryDanhMucAdv_Leave(object sender , EventArgs e)
        {
            TrialPLDanhMucAdv.isClosePopup = true;
        }

        void RepositoryHuyDanhMuc_Popup(object sender , EventArgs e)
        {
            plDanhMuc._focusRow();
        }
       
        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(ControlName ,
                                        typeof(TrialPLDanhMucExt) , typeof(RepositoryItemDanhMucExt) ,
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
            return plDanhMuc._getId(value);
        }
    }
}
