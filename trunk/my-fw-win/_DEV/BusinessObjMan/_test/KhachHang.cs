using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid.Rows;
using ProtocolVN.DanhMuc;

namespace ProtocolVN.Framework.Win.Test
{
	public class KhachHang : IObject 
	{
        public void CreateInfoGrid(GridView gridView)
        {
            //Khởi tạo cột
            GridColumn[] Cols = HelpGridColumn.CreateGridColumns(
                new string[] { "ID", "Mã khách hàng", "Tên khách hàng", "Địa chỉ", "Điện thoại" },
                new bool[] { false, true, true, true, true },
                new int[] { -1, -1, -1, -1, -1 });
            //Chọn loại Cột
            HelpGridColumn.CotTextLeft(Cols[0], "ID");
            HelpGridColumn.CotTextLeft(Cols[1], "MA_KH");
            HelpGridColumn.CotTextLeft(Cols[2], "NAME");
            HelpGridColumn.CotTextLeft(Cols[3], "DIA_CHI");
            HelpGridColumn.CotTextLeft(Cols[4], "DIEN_THOAI");
            //Gắn xử lý

            //Tuỳ chọn grid
            gridView.OptionsView.ColumnAutoWidth = false;            
            gridView.Columns.AddRange(Cols);
        }

        public void CreateVGrid(VGridControl vgrid)
        {
            EditorRow[] Rows = HelpEditorRow.CreateEditorRow(
                new string[] {  "ID", "Mã khách hàng", "Tên khách hàng", "Địa chỉ", "Điện thoại", 
                                "Fax", "Email", "Trang web", "Mã số thuế", "Người đại diện", "Chiết khấu", GlobalConst.VISIBLE_TITLE },
                new bool[]   {  false, true, true, true, true, 
                                true, true, true, true, true, true, true },
                new int[]  {  0, 10, 10, 10, 10, 
                                10, 10, 10, 10, 10, 10, 10 });
            HelpEditorRow.DongTextLeft(Rows[0], "ID");
            HelpEditorRow.DongTextLeft(Rows[1], "MA_KH");
            HelpEditorRow.DongTextLeft(Rows[2], "NAME");
            HelpEditorRow.DongTextLeft(Rows[3], "DIA_CHI");
            HelpEditorRow.DongTextLeft(Rows[4], "DIEN_THOAI");
            HelpEditorRow.DongTextLeft(Rows[5], "FAX");
            HelpEditorRow.DongTextLeft(Rows[6], "EMAIL");
            HelpEditorRow.DongTextLeft(Rows[7], "WEBSITE");
            HelpEditorRow.DongTextLeft(Rows[8], "MA_SO_THUE");
            HelpEditorRow.DongTextLeft(Rows[9], "NGUOI_DAI_DIEN");
            HelpEditorRow.DongTextLeft(Rows[10], "CHIET_KHAU");
            HelpEditorRow.DongCheckEdit(Rows[11], "VISIBLE_BIT");

            vgrid.Rows.AddRange(Rows);
        }

        public FieldNameCheck[] GetRuleVGrid(object param)
        {
            return new FieldNameCheck[] { 
                        new FieldNameCheck("MA_KH",
                            new CheckType[]{ CheckType.Required , CheckType.RequireMaxLength },
                            "Mã khách hàng", 
                            new object[]{ null, 100 })
                        ,new FieldNameCheck("NAME",
                            new CheckType[]{ CheckType.Required ,CheckType.RequireMaxLength},
                            "Tên khách hàng", 
                            new object[]{ null,200 })
                        ,new FieldNameCheck("DIA_CHI",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Địa chỉ", 
                            new object[]{ 400 })
                        ,new FieldNameCheck("DIEN_THOAI",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Điện thoại", 
                            new object[]{ 200 })
                        ,new FieldNameCheck("FAX",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Fax", 
                            new object[]{ 100 })
                        ,new FieldNameCheck("WEBSITE",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Website", 
                            new object[]{ 200 })
                        ,new FieldNameCheck("MA_SO_THUE",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Mã số thuế", 
                            new object[]{ 200 })
                        ,new FieldNameCheck("NGUOI_DAI_DIEN",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Người đại diện", 
                            new object[]{ 200 })
                        ,new FieldNameCheck("CHIET_KHAU",
                            new CheckType[]{ CheckType.DecGreaterEqual0 },
                            "Chiết khấu", 
                            new object[]{ null })                        
                        ,new FieldNameCheck("EMAIL",
                            new CheckType[]{ CheckType.OptionEmail, CheckType.OptionMaxLength},
                            "Email", 
                            new object[]{ null, 200 })
            };
        }
	}
}
