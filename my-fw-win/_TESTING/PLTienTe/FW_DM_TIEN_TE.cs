using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ProtocolVN.Framework.Win
{
    [Obsolete("Danh mục này được thay thế bằng DMFWTienTe")]
    public class FW_DM_TIEN_TE
    {
        private static GridColumn[] CreateFW_DM_TIEN_TE(GridControl gridControl, GridView gridView)
        {
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Loại tiền", 1, 150), "NAME");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tên đầy đủ", 1, 150), "TITLE");
            HelpGridColumn.CotPLHienThi(
                XtraGridSupportExt.CreateGridColumn(gridView, "Cho phép sửa", 2, 150), "ALLOW_EDIT_BIT");
            HelpGridColumn.CotPLHienThi(
                XtraGridSupportExt.CreateGridColumn(gridView, "Là nội tệ", 3, 150), "IS_BASE_BIT");
            HelpGridColumn.CotPLHienThi(
                XtraGridSupportExt.CreateGridColumn(gridView, "Hiển thị", 4, 150), "VISIBLE_BIT");
            //((PLGridView)gridView)._SetUserLayout("FW_DM_TIEN_TE");
            ((PLGridView)gridView)._SetUserLayout();
            return null;
        }
        private static FieldNameCheck[] GetRuleFW_DM_TIEN_TE(object param)
        {
            return new FieldNameCheck[] { 
                        new FieldNameCheck("NAME",
                            new CheckType[]{ CheckType.Required, CheckType.RequireMaxLength },
                            "Loại tiền", 
                            new object[]{ null, 100 }),
                        new FieldNameCheck("TITLE",
                            new CheckType[]{ CheckType.Required, CheckType.RequireMaxLength },
                            "Tên đầy đủ", 
                            new object[]{ null, 200 })
            };
        }
        public static XtraUserControl GetFW_DM_TIEN_TE()
        {
            DMGrid basic = new DMGrid("FW_DM_TIEN_TE", "ID", "NAME", "Loại tiền", CreateFW_DM_TIEN_TE, GetRuleFW_DM_TIEN_TE);
            return basic;
        }
        public static frmCategoryDialog GetFW_DM_TIEN_TE_Dialog()
        {
            return new frmCategoryDialog(FW_DM_TIEN_TE.GetFW_DM_TIEN_TE());
        }
    }
}
