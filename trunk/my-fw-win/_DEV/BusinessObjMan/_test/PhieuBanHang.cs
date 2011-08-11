using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win.Test
{
	public class PhieuBanHang : IPhieu
	{
        public void CreateResultGrid(GridView gridView)
        {            
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "PBH_ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Mã PBH", 0, 150), "MA_PBH");
            XtraGridSupportExt.IDGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tên khách hàng", 1, 150), "ID", "NAME", "DM_KHACH_HANG", "KH_ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Người bán", 2, 150), "NGUOI_BAN");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Dự án", 3, 150), "DU_AN");
            XtraGridSupportExt.DecimalGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Hoa hồng", 4, 150), "HOA_HONG");
            XtraGridSupportExt.DecimalGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Chiết khấu", 5, 150), "CHIET_KHAU");
            XtraGridSupportExt.DecimalGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tổng tiền", 6, 150), "TONG_TIEN");
            XtraGridSupportExt.DecimalGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Còn lại", 7, 150), "CON_LAI");           
            XtraGridSupportExt.DateTimeGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Ngày bán", 8, 150), "NGAY_BAN");         
            
            gridView.OptionsView.ColumnAutoWidth = false;
        }
	}
}
