using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using ProtocolVN.Framework.Core;
using ProtocolVN.DanhMuc;

namespace ProtocolVN.Framework.Win
{
    public class DynamicFieldManMethodExec
    {
        public void Show()
        {
            frmCategoryDialog cat = new frmCategoryDialog(this.DM_QUAN_LY_FIELD());
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, cat);
        }

        #region - Danh mục Quản lý field
        private GridColumn[] CreateDM_QUAN_LY_FIELD(GridControl gridControl, GridView gridView)
        {
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Field ID", -1, -1), "FIELD_ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tên trường", 0, 150), "CAPTION");

            #region Khởi tạo DataSource cho cột Kiểu dữ liệu
            DataTable dt = new DataTable("DATA_TYPE");
            DataColumn c1 = new DataColumn("STT");            
            DataColumn c2 = new DataColumn("Name");

            dt.Columns.Add(c1);
            dt.Columns.Add(c2);

            DataRow dr1 = dt.NewRow();
            dr1[0] = "1";
            dr1[1] = "Văn bản";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2[0] = "2";
            dr2[1] = "Số nguyên";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3[0] = "3";
            dr3[1] = "Số thực";
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4[0] = "4";
            dr4[1] = "Logic";
            dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr5[0] = "5";
            dr5[1] = "Ngày";
            dt.Rows.Add(dr5);
            #endregion

            // Khởi tạo cột Kiểu dữ liệu bằng code
            RepositoryItemLookUpEdit Cot_KieuDuLieu_Edit = new RepositoryItemLookUpEdit();
            Cot_KieuDuLieu_Edit.DataSource = dt;
            Cot_KieuDuLieu_Edit.DisplayMember = "Name";
            Cot_KieuDuLieu_Edit.ValueMember = "STT";
            GridColumn Cot_KieuDuLieu = new GridColumn();
            Cot_KieuDuLieu.Caption = "Kiểu dữ liệu";
            Cot_KieuDuLieu.FieldName = "DATA_TYPE";
            Cot_KieuDuLieu.ColumnEdit = Cot_KieuDuLieu_Edit;
            Cot_KieuDuLieu.Width = 150;
            Cot_KieuDuLieu.VisibleIndex = 1;
            gridView.Columns.Add(Cot_KieuDuLieu);
            //----------------------------------     

            GridColumn ColTenDoiTuong = XtraGridSupportExt.CreateGridColumn(gridView, "Tên đối tượng", 2, 150);
            ColTenDoiTuong.FieldName = "TABLE_ID";
            XtraGridSupportExt.IDGridColumn(ColTenDoiTuong, "ID", "DESCRIPTION", "FW_TABLE_OBJECT", "TABLE_ID");
            ColTenDoiTuong.OptionsColumn.AllowEdit = true;
            ColTenDoiTuong.OptionsColumn.AllowFocus = false;
            ColTenDoiTuong.OptionsColumn.ReadOnly = true;
            gridView.GroupCount = 1;
            gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
                new DevExpress.XtraGrid.Columns.GridColumnSortInfo(ColTenDoiTuong, DevExpress.Data.ColumnSortOrder.Ascending)}
            );
            HelpGridColumn.CotCheckEdit(
                XtraGridSupportExt.CreateGridColumn(gridView, GlobalConst.VISIBLE_TITLE, 100, 100), "VISIBLE_BIT");

            gridView.OptionsView.ShowGroupedColumns = false;
            gridView.OptionsView.ColumnAutoWidth = false;

            return null;
        }

        private FieldNameCheck[] GetRuleDM_QUAN_LY_FIELD(object param)
        {
            return new FieldNameCheck[] {                                            
                        new FieldNameCheck("CAPTION",
                            new CheckType[]{ CheckType.Required, CheckType.RequireMaxLength },
                            "Tên trường", 
                            new object[]{ null, 100 }),
                        new FieldNameCheck("DATA_TYPE",
                            new CheckType[]{ CheckType.Required},
                            "Kiểu dữ liệu", 
                            new object[]{ null})
            };
        }

        public XtraUserControl DM_QUAN_LY_FIELD()
        {
            DataSet dt = DABase.getDatabase().LoadTable("FW_TABLE_FIELD_EXT");
            DMTreeGroupElement pl = new DMTreeGroupElement();
            pl.Init(GroupElementType.CHOICE_N_ADD,
                "FW_TABLE_OBJECT", null, "ID", "PARENT_ID",
                new string[] { "DESCRIPTION" }, new string[] { "Tên đối tượng" }, dt,
                "FIELD_ID", "TABLE_ID", "CAPTION", HelpGen.G_FW_ID, CreateDM_QUAN_LY_FIELD, GetRuleDM_QUAN_LY_FIELD);
            return pl;
        }
        #endregion     
    }
}
