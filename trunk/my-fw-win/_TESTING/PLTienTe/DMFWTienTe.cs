using System;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.DanhMuc
{
    public class DMFWTienTe : IDanhMuc
    {
        public static DMFWTienTe I = new DMFWTienTe();
        public static String N = typeof(DMFWTienTe).FullName;
        public static bool isPermission = false;

        #region IDanhMuc Members

        public string Item()
        {
            return
            @"<cat table='" + HelpFURL.FURL(N, "Init") + @"' type='action' picindex='navTienTe.png'>
                  <lang id='vn'>Tiền tệ</lang>
                  <lang id='en'></lang>
                </cat>";
        }

        public string MenuItem(string mainCat, string parentID, bool isSep)
        {
            return MenuItem(mainCat, "Tiền tệ", parentID, isSep);
        }

        public string MenuItem(string mainCat, string title, string parentID, bool isSep)
        {
            return MenuBuilder.CreateItem(N, title, parentID, true,
                           HelpFURL.FURL(mainCat, N, "Init"), true, isSep, "navTienTe.png", false, "", "");
        }

        #region Init
        private GridColumn[] InitColumns(GridControl gridControl, GridView gridView)
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


            //XtraGridSupportExt.TextLeftColumn(
            //    XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "ID");
            //XtraGridSupportExt.ComboboxGridColumn(
            //    XtraGridSupportExt.CreateGridColumn(gridView, "Tiền tệ", 1, 150), "FW_DM_TIEN_TE", "ID", "NAME", "TT_ID");
            //HelpGridColumn.CotCalcEdit(
            //    XtraGridSupportExt.CreateGridColumn(gridView, "Tỉ giá", 2, 150), "TI_GIA", 2);
            //HelpGridColumn.CotDateEdit(
            //    XtraGridSupportExt.CreateGridColumn(gridView, "Ngày cập nhật", 3, 150), "NGAY_CAP_NHAT");
            //HelpGridColumn.CotCombobox(
            //    XtraGridSupportExt.CreateGridColumn(gridView, "Người cập nhật", 4, 150), "DM_NHAN_VIEN", "ID", "NAME", "NGUOI_CAP_NHAT");

            //gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView_CellValueChanged);

            return null;
        }

        //void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    PLGridView grid = (PLGridView)sender;
        //    if (grid.GetDataRow(e.RowHandle)["ID"] == DBNull.Value)
        //        return;
        //    if (e.Column == grid.Columns["TI_GIA"])
        //    {
        //        DataSet ds = ((DataTable)grid.GridControl.DataSource).DataSet;
        //        DataRow row;//= ds.Tables[0].Rows[e.RowHandle];
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            if (HelpNumber.ParseInt64(ds.Tables[0].Rows[i]["ID"]).Equals(
        //                HelpNumber.ParseInt64(grid.GetDataRow(e.RowHandle)["ID"])))
        //            {
        //                row = ds.Tables[0].Rows[i];
        //                row.AcceptChanges();
        //                if (row.RowState != DataRowState.Added)
        //                {
        //                    row.SetAdded();
        //                    row["ID"] = DBNull.Value;
        //                    grid.SetFocusedRowCellValue(grid.Columns["ID"], row["ID"]);
        //                }
        //            }

        //    }
        //}
        private FieldNameCheck[] GetBizRule(object param)
        {
            //return new FieldNameCheck[] { 
            //            new FieldNameCheck("TT_ID",
            //                new CheckType[]{ CheckType.Required },
            //                "Tiền tệ", 
            //                new object[]{ null}),
            //            new FieldNameCheck("TI_GIA",
            //                new CheckType[]{ CheckType.OptionMaxLength },
            //                "Tỉ giá", 
            //                new object[]{ 400 }),
            //            new FieldNameCheck("NGAY_CAP_NHAT",
            //                new CheckType[]{ CheckType.RequireDate },
            //                "Ngày cập nhật", 
            //                new object[]{ null}),
            //            new FieldNameCheck("NGUOI_CAP_NHAT",
            //                new CheckType[]{ CheckType.Required },
            //                "Người cập nhật", 
            //                new object[]{ null })                        
            //};
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

        public DevExpress.XtraEditors.XtraUserControl Init()
        {
            //DMGrid basic = new DMGrid("FW_TI_GIA", "ID", "TI_GIA", "Tỉ giá", InitColumns, GetBizRule);
            //basic.DefinePermission(DanhMucParams.GetPermission(basic, DMCapNhatTiGia.N));
            //return basic;

            DMGrid basic = new DMGrid("FW_DM_TIEN_TE", "ID", "NAME", "Loại tiền", InitColumns, GetBizRule);
            if (isPermission) basic.DefinePermission(DanhMucParams.GetPermission(basic, DMFWTienTe.N, "Danh má»¥c tiá»n tá»‡"));
            return (XtraUserControl)basic;
        }
        #endregion

        #endregion
       
    }
}
