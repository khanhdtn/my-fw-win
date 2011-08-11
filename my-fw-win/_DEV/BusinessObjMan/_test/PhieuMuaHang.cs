using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win.Test
{
    public class PhieuMuaHang : IPhieu 
    {
        #region IPhieu Members

        public void CreateResultGrid(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "PMH_ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Mã PMH", 0, 150), "MA_PMH");
            XtraGridSupportExt.IDGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Người cập nhật", 1, 150), "ID", "NAME", "DM_NHAN_VIEN", "NGUOI_CAP_NHAT");
            XtraGridSupportExt.DateTimeGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Ngày cập nhật", 2, 150), "NGAY_CAP_NHAT");
            XtraGridSupportExt.IDGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Người mua hàng", 3, 150), "ID", "NAME", "DM_NHAN_VIEN", "NGUOI_MUA_HANG");
            XtraGridSupportExt.DateTimeGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Ngày mua hàng", 4, 150), "NGAY_MUA_HANG");
            XtraGridSupportExt.IDGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Người duyệt", 5, 150), "ID", "NAME", "DM_NHAN_VIEN", "NGUOI_DUYET");
            XtraGridSupportExt.DateTimeGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Ngày duyệt", 6, 150), "NGAY_DUYET");
            XtraGridSupportExt.CreateDuyetGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Duyệt", 7, 150));
            XtraGridSupportExt.IDGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Nhà cung cấp", 8, 150), "ID", "NAME", "DM_NHA_CUNG_CAP", "NCC_ID");
            XtraGridSupportExt.DecimalGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Chiết khấu", 9, 150), "CHIET_KHAU");
            XtraGridSupportExt.DecimalGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tổng tiền", 10, 150), "TONG_TIEN");
            XtraGridSupportExt.DecimalGridColumn(
               XtraGridSupportExt.CreateGridColumn(gridView, "Đã thanh toán", 11, 150), "DA_THANH_TOAN");
            XtraGridSupportExt.DecimalGridColumn(
               XtraGridSupportExt.CreateGridColumn(gridView, "Tổng thuế nhập khẩu", 12, 150), "TONG_THUE_NHAP_KHAU");
            XtraGridSupportExt.DecimalGridColumn(
               XtraGridSupportExt.CreateGridColumn(gridView, "Tổng chi phí lãnh hàng", 13, 150), "TONG_CHI_PHI_LANH_HANG");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Ghi chú", 14, 150), "GHI_CHU");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Hóa đơn tài chính", 15, 150), "HOA_DON_TAI_CHINH");            
            XtraGridSupportExt.IDGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Tiền tệ", 16, 150), "ID", "NAME", "DM_NHA_CUNG_CAP", "NCC_ID");                  
            
            gridView.OptionsView.ColumnAutoWidth = false;
        }

        #endregion
    }
}
