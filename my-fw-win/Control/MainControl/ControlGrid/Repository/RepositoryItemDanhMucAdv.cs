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
    public class RepositoryItemDanhMucAdv:RepositoryItemPopupContainerEdit
    {
        public const string ControlName = "PLDanhMucAdv";
        private TrialPLDanhMucAdvCtrl plDanhMuc;
        private PopupContainerControl popupControl;
        static RepositoryItemDanhMucAdv()
        {
            Register();
        }
        private void init()
        {
            plDanhMuc = new TrialPLDanhMucAdvCtrl();
            popupControl = new PopupContainerControl();
            this.PopupControl = popupControl;
            popupControl.Controls.Add(plDanhMuc);
            popupControl.Size = plDanhMuc.Size;
            this.PopupSizeable = false;
            this.ShowPopupCloseButton = false;
        }
        public RepositoryItemDanhMucAdv(XtraForm frmDanhMuc ,string columnField, string tableName , string ValueField , string[] visibleField , string[] caption , string getField , GridView gridView)
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

        public RepositoryItemDanhMucAdv(XtraForm frmDanhMuc ,string columnField, string tableName , string ValueField , string[] visibleField , string[] caption , string getField , TreeList treeList)
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
                                        typeof(TrialPLDanhMucAdv) , typeof(RepositoryItemDanhMucAdv) ,
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
