using System;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using System.ComponentModel;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ProtocolVN.DanhMuc;
using ProtocolVN.Framework.Win.Trial;

namespace ProtocolVN.Framework.Win.Demo
{
    //public partial class frmTPhieuThongKeTemplate : XtraFormPL
    public partial class frmTPhieuThongKeTemplate : frmTPhieuThongKeChange
    {
        #region Các biến của form Quan Ly Tuyệt đối không thay đổi
        //protected BarButtonItem barButtonItem3;
        //protected BarSubItem barButtonItemExport;
        //protected BarButtonItem barButtonItemSearch;
        //protected BarButtonItem barButtonItemTuyChonXemDoThi;
        //protected BarCheckItem barCheckItemFilter;
        //protected BarDockControl barDockControlBottom;
        //protected BarDockControl barDockControlLeft;
        //protected BarDockControl barDockControlRight;
        //protected BarDockControl barDockControlTop;
        //protected BarManager barManager1;
        //protected PivotGridControl pivotGridMaster;
        //protected Bar MainBar;
        //protected PopupControlContainer popupControlContainerFilter;
        //protected PopupMenu popupMenuFilter;
        //protected SplitContainerControl splitContainerControl1;
        //protected DevExpress.XtraBars.BarButtonItem barItemIn;
        //protected DevExpress.XtraBars.PopupMenu popupIn;
        //protected DevExpress.XtraBars.BarButtonItem barItemXemTruoc;
        #endregion 
         
        public frmTPhieuThongKeTemplate()
        {
            InitializeComponent();           
            new TPhieuThongKeFix(this, true);
            pivotGridMaster.OptionsChartDataSource.ChartDataVertical = false;
            //PLPivotGridControl.VietHoaMenuGridPivot(pivotGridMaster);
            //barButtonItemTuyChonXemDoThi.Visibility = BarItemVisibility.Never;
        }

        #region Static
        public static FormStatus Status = FormStatus.OK_TEST;
        public static string MenuItem(string ParentID, bool isMDI, bool IsSep)
        {
            return MenuBuilder.CreateItem(typeof(frmTPhieuThongKeTemplate).FullName,
                PMSupport.UpdateTitle("Thống kê mẫu", Status),
                ParentID, true,
                typeof(frmTPhieuThongKeTemplate).FullName,
                isMDI, IsSep, "navTinhTrangHangTonKho.png", true, "", "");
        }
        #endregion

        public override void InitFieldMaster()
        {            
            //HelpPivotGridField.FieldRow(fieldHangHoa, "HH_NAME", "Tên hàng hóa", 1, 180);
            //HelpPivotGridField.FieldColumnYear(fieldNam, "NGAY_LAP", "Năm", 0, 120); 
            //HelpPivotGridField.FieldColumnMonth(fieldThang, "NGAY_LAP", "Tháng", 2, 120);
            
            //HelpPivotGridField.FieldCalcEditDec(fieldSoLuongNhap, "SO_LUONG_NHAP", "Số lượng nhập", 1, 2, false);
            //HelpPivotGridField.FieldCalcEditDec(fieldSoLuongXuat, "SO_LUONG_XUAT", "Số lượng xuất", 2, 2, false);            
            //fieldThang.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            
        }

        public override QueryBuilder PLBuildQueryFilter()
        {
           // QueryBuilder query = PLBuldQueryXuatNhap();              
           // //DataSet ds = HelpDB.getDatabase().LoadDataSet(query);
           //// pivotGridMaster.DataSource = ds.Tables[0];
           // return query;
            return null;
        }
        public QueryBuilder PLBuldQueryXuatNhap()
        {
//              QueryBuilder query = new QueryBuilder(@"
//                SELECT hh_name, hh_id,so_luong_nhap,so_luong_xuat,ngay_lap
//                    FROM ( SELECT  hh.ma_hh ||'--'|| hh.NAME as HH_NAME,ctn.hh_id,
//                            ctn.SO_LUONG SO_LUONG_NHAP, 0 SO_LUONG_XUAT,
//                            nk.ngay_nhap ngay_lap
//                    FROM phieu_nhap_kho nk
//                        inner join phieu_nhap_kho_ct ctn on nk.pnk_id=ctn.pnk_id
//                        inner join DM_HANG_HOA hh  on ctn.HH_ID=hh.ID
//
//                    UNION
//                    SELECT hh.ma_hh ||'--'|| hh.NAME as HH_NAME,ctx.hh_id,
//                           0 SO_LUONG_NHAP,ctx.SO_LUONG SO_LUONG_XUAT,
//                           xk.ngay_xuat ngay_lap
//                    FROM   phieu_XUAT_kho xk
//                        inner join phieu_xuat_kho_ct ctx on xk.pxk_id=ctx.pxk_id
//                        inner join DM_HANG_HOA hh  on ctx.HH_ID=hh.ID
//
//                    UNION
//                    SELECT hh.ma_hh ||'--'|| hh.NAME as HH_NAME,ctc.hh_id,
//                           ctc.so_luong SO_LUONG_NHAP,ctc.so_luong SO_LUONG_XUAT,
//                           ck.ngay_chuyen ngay_lap
//                    FROM   phieu_chuyen_kho ck
//                        inner join phieu_chuyen_kho_ct ctc on ck.pck_id=ctc.pck_id
//                        inner join DM_HANG_HOA hh  on ctc.HH_ID=hh.ID) 
//                WHERE 1=1");
//            query.addID("HH_ID", this.MaHangHoa._getSelectedIDs());
//            query.addID("HH_ID", this.TenHangHoa._getSelectedIDs());
//            query.addDateFromTo("NGAY_LAP", thoiGian.FromDate, thoiGian.ToDate);
//            return query;
            return null;
        }
        public override void PLLoadFilterPart()
        {
            //DMSanPham.I.InitCtrlMulti(this.MaHangHoa, false, "MA_HH");
            //DMSanPham.I.InitCtrlMulti(this.TenHangHoa, false, "NAME");            
        } 
       
    }
}