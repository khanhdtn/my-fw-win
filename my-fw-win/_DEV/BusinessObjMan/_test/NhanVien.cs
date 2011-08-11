using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win.Test
{
    public class NhanVien : IObject 
    {
        #region IObject Members

        public void CreateInfoGrid(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Họ tên nhân viên", 0, 150), "NAME");

            GridColumn ColPhongBan = XtraGridSupportExt.CreateGridColumn(gridView, "Tên phòng ban", 1, -1);            
            ColPhongBan.Tag = "DEPARTMENT;ID;NAME";
            XtraGridSupportExt.IDGridColumn(ColPhongBan, "ID", "NAME", "DEPARTMENT", "DEPARTMENT_ID");            
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "CMND", 2, 150), "CMND");
            XtraGridSupportExt.DateTimeGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Ngày sinh", 3, 150), "NGAY_SINH");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Điện thoại", 4, 150), "DIEN_THOAI");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Địa chỉ", 5, 150), "DIA_CHI");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Email", 6, 150), "EMAIL");             
            gridView.OptionsView.ColumnAutoWidth = false;            
        }

        public void CreateVGrid(DevExpress.XtraVerticalGrid.VGridControl vgrid)
        {
            EditorRow[] Rows = HelpEditorRow.CreateEditorRow(
                new string[] {  "ID", "Tên nhân viên", "CMND",
                                "Ngày sinh", "Điện thoại", "Địa chỉ", "Email" },
                new bool[]   {  false, true, true, true, true, 
                                true, true },
                new int[]  {  0, 10, 10, 10, 10, 
                                10, 10 });
            HelpEditorRow.DongTextLeft(Rows[0], "ID");            
            HelpEditorRow.DongTextLeft(Rows[1], "NAME");
            HelpEditorRow.DongTextLeft(Rows[2], "CMND");
            HelpEditorRow.DongDateEdit(Rows[3], "NGAY_SINH");
            HelpEditorRow.DongTextLeft(Rows[4], "DIEN_THOAI");
            HelpEditorRow.DongTextLeft(Rows[5], "DIA_CHI");           
            HelpEditorRow.DongTextLeft(Rows[6], "EMAIL");            

            vgrid.Rows.AddRange(Rows);
        }

        public FieldNameCheck[] GetRuleVGrid(object param)
        {
            return new FieldNameCheck[] { 
                        new FieldNameCheck("NAME",
                            new CheckType[]{ CheckType.Required , CheckType.RequireMaxLength },
                            "Họ tên nhân viên", 
                            new object[]{ null, 200 }),
                        new FieldNameCheck("CMND",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "CMND", 
                            new object[]{ 100 }),
                        new FieldNameCheck("DIEN_THOAI",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Điện thoại", 
                            new object[]{ 100 }),
                        new FieldNameCheck("DIA_CHI",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Địa chỉ", 
                            new object[]{ 200 }),
                        new FieldNameCheck("EMAIL",
                            new CheckType[]{ CheckType.OptionEmail, CheckType.OptionMaxLength },
                            "Email", 
                            new object[]{ null, 200 })
            };
        }

        #endregion
    }
}
