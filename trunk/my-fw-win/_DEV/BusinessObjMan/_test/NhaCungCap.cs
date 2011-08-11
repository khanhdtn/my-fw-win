using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win.Test
{
    public class NhaCungCap : IObject
    {
        #region IObject Members

        public void CreateInfoGrid(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {            
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Mã nhà cung cấp", 0, 150), "MA_NCC");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tên nhà cung cấp", 1, 150), "NAME");

            GridColumn ColNhomNhaCungCap = XtraGridSupportExt.CreateGridColumn(gridView, "Tên nhóm nhà cung cấp ", 2, -1);            
            ColNhomNhaCungCap.Tag = "DM_NHA_CUNG_CAP_NHOM;ID;NAME";
            XtraGridSupportExt.IDGridColumn(ColNhomNhaCungCap, "ID", "NAME", "DM_NHA_CUNG_CAP_NHOM", "NNCC_ID");            
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Địa chỉ", 3, 150), "DIA_CHI");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Điện thoại", 4, 150), "DIEN_THOAI");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Fax", 5, 150), "FAX");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Email", 6, 150), "EMAIL");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Trang web", 7, 150), "WEBSITE");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Mã số thuế", 8, 150), "MA_SO_THUE");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Người đại diện", 9, 150), "NGUOI_DAI_DIEN");
            XtraGridSupportExt.DecimalGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Chiết khấu", 10, 150), "CHIET_KHAU");
            XtraGridSupportExt.DecimalGridColumn(
               XtraGridSupportExt.CreateGridColumn(gridView, "Hạn mục tín dụng", 11, 150), "HAN_MUC_TIN_DUNG");
            XtraGridSupportExt.DecimalGridColumn(
               XtraGridSupportExt.CreateGridColumn(gridView, "Tuổi nợ", 12, 150), "TUOI_NO");              
            
            gridView.OptionsView.ColumnAutoWidth = false;            
        }

        public void CreateVGrid(DevExpress.XtraVerticalGrid.VGridControl vgrid)
        {
            DevExpress.XtraVerticalGrid.Rows.EditorRow row_id = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_id.Properties.Caption = "ID";
            row_id.Properties.FieldName = "ID";
            row_id.Visible = false;
            vgrid.Rows.Add(row_id);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_ma_ncc = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_ma_ncc.Properties.Caption = "Mã nhà cung cấp";
            row_ma_ncc.Properties.FieldName = "MA_NCC";
            vgrid.Rows.Add(row_ma_ncc);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_name = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_name.Properties.Caption = "Tên nhà cung cấp";
            row_name.Properties.FieldName = "NAME";
            vgrid.Rows.Add(row_name);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_dia_chi = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_dia_chi.Properties.Caption = "Địa chỉ";
            row_dia_chi.Properties.FieldName = "DIA_CHI";
            vgrid.Rows.Add(row_dia_chi);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_dien_thoai = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_dien_thoai.Properties.Caption = "Điện thoại";
            row_dien_thoai.Properties.FieldName = "DIEN_THOAI";
            vgrid.Rows.Add(row_dien_thoai);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_fax = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_fax.Properties.Caption = "Fax";
            row_fax.Properties.FieldName = "FAX";
            vgrid.Rows.Add(row_fax);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_email = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_email.Properties.Caption = "Email";
            row_email.Properties.FieldName = "EMAIL";
            vgrid.Rows.Add(row_email);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_website = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_website.Properties.Caption = "Trang web";
            row_website.Properties.FieldName = "WEBSITE";
            vgrid.Rows.Add(row_website);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_ma_so_thue = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_ma_so_thue.Properties.Caption = "Mã số thuế";
            row_ma_so_thue.Properties.FieldName = "MA_SO_THUE";
            vgrid.Rows.Add(row_ma_so_thue);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_nguoi_dai_dien = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_nguoi_dai_dien.Properties.Caption = "Người đại diện";
            row_nguoi_dai_dien.Properties.FieldName = "NGUOI_DAI_DIEN";
            vgrid.Rows.Add(row_nguoi_dai_dien);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_chiet_khau = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_chiet_khau.Properties.Caption = "Chiết khấu";
            row_chiet_khau.Properties.FieldName = "CHIET_KHAU";
            vgrid.Rows.Add(row_chiet_khau);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_han_muc_tin_dung = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_han_muc_tin_dung.Properties.Caption = "Hạn mức tín dụng";
            row_han_muc_tin_dung.Properties.FieldName = "HAN_MUC_TIN_DUNG";
            vgrid.Rows.Add(row_han_muc_tin_dung);

            DevExpress.XtraVerticalGrid.Rows.EditorRow row_tuoi_no = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            row_tuoi_no.Properties.Caption = "Tuổi nợ";
            row_tuoi_no.Properties.FieldName = "TUOI_NO";
            vgrid.Rows.Add(row_tuoi_no);
        }

        public FieldNameCheck[] GetRuleVGrid(object param)
        {
            return new FieldNameCheck[] { 
                        new FieldNameCheck("MA_NCC",
                            new CheckType[]{ CheckType.Required , CheckType.RequireMaxLength },
                            "Mã nhà cung cấp", 
                            new object[]{ null, 100 })
                        ,new FieldNameCheck("NAME",
                            new CheckType[]{ CheckType.Required ,CheckType.RequireMaxLength},
                            "Tên nhà cung cấp", 
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
                        ,new FieldNameCheck("HAN_MUC_TIN_DUNG",
                            new CheckType[]{ CheckType.DecGreaterEqual0 },
                            "Hạn mục tín dụng", 
                            new object[]{ null })
                        ,new FieldNameCheck("TUOI_NO",
                            new CheckType[]{ CheckType.IntGreaterEqual0 },
                            "Tuổi nợ", 
                            new object[]{ null })                        
                        ,new FieldNameCheck("EMAIL",
                            new CheckType[]{ CheckType.OptionEmail, CheckType.OptionMaxLength },
                            "Email", 
                            new object[]{ null, 200 })
            };
        }

        #endregion
    }
}
