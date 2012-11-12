using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Data;
using System.Data.Common;
using ProtocolVN.Framework.Win.Database;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using System.Drawing;
using ProtocolVN.DanhMuc;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace ProtocolVN.Framework.Win
{    
    public class HelpPhieu : IRequiredDBObject
    {        
        #region IRequiredDBObject Members

        public void Requirement()
        {
            DatabaseMan.RequireDBObject(FW_ST_NGHIEP_VU.INSTANCE);
        }

        #endregion
        //PhieuTypeID, TieuDePhieu
        public static DataSet GetPhieuLienQuan(PhieuType Phieu)
        {
            DatabaseFB db = DABase.getDatabase();
            DataSet ds = new DataSet();
            DbCommand cmd = db.GetStoredProcCommand("FW_ST_NGHIEP_VU");
            db.AddInParameter(cmd, "@TABLE_NAME", DbType.String, Phieu.GetTableName());
            db.LoadDataSet(cmd, ds, "NGHIEP_VU");
            if (ds.Tables[0].Rows.Count != 0)
                return ds;
            else
                return null;
        }


        /// <summary>Khởi tạo Grid trên phiếu 1 lưới
        /// </summary>
        public static GridColumn InitGrid(GridControl gridControl, GridView gridView, bool? IsAdd)
        {
            return InitGrid(gridControl, gridView, IsAdd, true);
        }
        /// <summary>Khởi tạo Grid trên phiếu 1 lưới
        /// </summary>
        public static GridColumn InitGrid(GridControl gridControl, GridView gridView, bool? IsAdd, bool confirmDelete)
        {
            GridColumn CotDong = HelpGridColumn.CotPLDong(gridControl, gridView,confirmDelete);
            if (IsAdd == null)
            {
                CotDong.Visible = false;
                //((PLGridView)gridView)._SetDesignLayout();
                ((PLGridView)gridView)._SetUserLayout("VIEW");
            }
            else
            {
                //((PLGridView)gridView)._SetUserLayout();
                ((PLGridView)gridView)._SetUserLayout("UPDATE");
            }
            return CotDong;
        }
        /// <summary>Khởi tạo Grid trên phiếu 1 lưới
        /// </summary>
        public static BandedGridColumn InitGrid(GridControl gridControl, BandedGridView gridView, bool? IsAdd)
        {
            BandedGridColumn CotDong = HelpGridColumn.CotPLDong(gridControl, gridView);
            if (IsAdd == null)
            {
                CotDong.Visible = false;
                CotDong.OwnerBand.Visible = false;
                //((PLGridView)gridView)._SetDesignLayout();
                ((PLBandedGridView)gridView)._SetUserLayout("VIEW");
            }
            else
            {
                //((PLGridView)gridView)._SetUserLayout();
                ((PLBandedGridView)gridView)._SetUserLayout("UPDATE");
            }
            return CotDong;
        }

        /// <summary>
        /// Khởi tạo Grid trên phiếu 1 lưới không tạo cột xóa
        /// </summary>      
        public static void InitGrid(GridView gridView, bool? IsAdd)
        {

            if (IsAdd == null)
            {

                //((PLGridView)gridView)._SetDesignLayout();
                ((PLGridView)gridView)._SetUserLayout("VIEW");
            }
            else
            {
                //((PLGridView)gridView)._SetUserLayout();
                ((PLGridView)gridView)._SetUserLayout("UPDATE");
            }
        }

        /// <summary>
        /// Khởi tạo Grid trên phiếu 1 lưới không tạo cột xóa
        /// </summary>      
        public static void InitGrid(BandedGridView gridView, bool? IsAdd)
        {

            if (IsAdd == null)
            {

                //((PLGridView)gridView)._SetDesignLayout();
                ((PLBandedGridView)gridView)._SetUserLayout("VIEW");
            }
            else
            {
                //((PLGridView)gridView)._SetUserLayout();
                ((PLBandedGridView)gridView)._SetUserLayout("UPDATE");
            }
        }

        /// <summary>Khởi tạo các nút trên phiếu 
        /// </summary>
        public static void InitBtnPhieu(XtraForm frm, bool? IsAdd, DropDownButton NghiepVu, DropDownButton InPhieu, DropDownButton Chon,
                                        SimpleButton Save, SimpleButton Delete, SimpleButton Close)
        {
            if (Save != null) Save.Image = FWImageDic.SAVE_IMAGE16;
            if (Delete != null) Delete.Image = FWImageDic.DELETE_IMAGE16;
            if (Close != null) Close.Image = FWImageDic.CLOSE_IMAGE16;
            if (NghiepVu != null)
            {
                NghiepVu.Size = new System.Drawing.Size(101, 23);
                NghiepVu.Image = FWImageDic.BUSINESS_IMAGE16;
            }
            if (InPhieu != null)
            {
                InPhieu.Size = new System.Drawing.Size(84, 23);
                InPhieu.Image = FWImageDic.PRINT_IMAGE16;
            }
            if (Chon != null)
            {
                Chon.Size = new System.Drawing.Size(130, 23);
                Chon.Image = FWImageDic.CHOICE_POPUP_IMAGE16;
            }
            if (IsAdd == null)
            {
                if (Delete != null) Delete.Visible = false;
                if (Save != null) Save.Visible = false;
                if (NghiepVu != null) NghiepVu.Enabled = true;
                if (InPhieu != null) InPhieu.Enabled = true;
                if (Chon != null) Chon.Enabled = false;
            }
            else if (IsAdd == true)
            {
                if (Delete != null) Delete.Enabled = false;
                if (Save != null) Save.Enabled = true;
                if (NghiepVu != null) NghiepVu.Enabled = false;
                if (InPhieu != null) InPhieu.Enabled = false;
                if (Chon != null) Chon.Enabled = true;
            }
            else
            {
                if (Delete != null) Delete.Enabled = true;
                if (Save != null) Save.Enabled = true;
                if (NghiepVu != null) NghiepVu.Enabled = true;
                if (InPhieu != null) InPhieu.Enabled = true;
                if (Chon != null) Chon.Enabled = true;
            }
        }

        /// <summary>Khởi tạo các nút drop down trên phiếu
        /// </summary>
        public static void InitDropDownButton(DropDownButton NghiepVu, DropDownButton InPhieu, DropDownButton Chon,
            ContextMenuStrip mnuNghiepVu, ContextMenuStrip mnuInPhieu, ContextMenuStrip mnuChon)
        {

            if (NghiepVu != null)
            {
                NghiepVu.ArrowButtonClick += delegate(object sender, EventArgs e)
                {
                    mnuNghiepVu.Show(NghiepVu, new Point(0, 23));
                };
            }
            if (InPhieu != null)
            {
                InPhieu.ArrowButtonClick += delegate(object sender, EventArgs e)
                {
                    mnuInPhieu.Show(InPhieu, new Point(0, 23));
                };
            }
            if (Chon != null)
            {
                Chon.ArrowButtonClick += delegate(object sender, EventArgs e)
                {
                    mnuChon.Show(Chon, new Point(0, 23));
                };
            }
        }

        public static void InitDropDownButton(DropDownButton NghiepVu, DropDownButton InPhieu, DropDownButton Chon, DropDownButton ChonExcel,
           ContextMenuStrip mnuNghiepVu, ContextMenuStrip mnuInPhieu, ContextMenuStrip mnuChon, ContextMenuStrip mnuExcel)
        {

            if (NghiepVu != null)
                NghiepVu.ArrowButtonClick += delegate(object sender, EventArgs e)
                {
                    mnuNghiepVu.Show(NghiepVu, new Point(0, 23));
                };
            if (InPhieu != null)
                InPhieu.ArrowButtonClick += delegate(object sender, EventArgs e)
                {
                    mnuInPhieu.Show(InPhieu, new Point(0, 23));
                };
            if (Chon != null)
                Chon.ArrowButtonClick += delegate(object sender, EventArgs e)
                {
                    mnuChon.Show(Chon, new Point(0, 23));
                };
            if (ChonExcel != null)
                ChonExcel.ArrowButtonClick += delegate(object sender, EventArgs e)
                {
                    mnuExcel.Show(ChonExcel, new Point(0, 23));
                };
        }


        #region Số Phiếu
        /// <summary>Hiển thị số Phiếu trong Phiếu
        /// </summary>
        public static void SetSoPhieu(TextEdit soPhieu, string value)
        {
            if (value != null)
            {
                soPhieu.Text = value;
                soPhieu.Properties.ReadOnly = true;
                soPhieu.Enabled = true;
                soPhieu.TabStop = false;
            }
            else
            {
                soPhieu.Enabled = false;
                soPhieu.BackColor = System.Drawing.Color.WhiteSmoke;
            }
        }

        #endregion




        /// <summary>Hiển thị thông tin trong nút I
        /// Hiện tại phải hiển thị người cập nhật, ngày cập nhật, người duyệt, ngày duyệt.
        /// </summary>
        public static void SetInfoBox(PLInfoBox Info, long NGUOI_CAP_NHAT, DateTime? NGAY_CAP_NHAT)
        {
            if (NGUOI_CAP_NHAT > 0)
            {
                Info._init(DMFWNhanVien.GetFullName(NGUOI_CAP_NHAT), NGAY_CAP_NHAT.ToString());
                Info.Enabled = true;
            }
            else
            {
                Info.Enabled = false;
            }
        }

        public static void SetInfoBoxExt(PLInfoBox Info, long NGUOI_CAP_NHAT, DateTime? NGAY_CAP_NHAT)
        {
            if (NGUOI_CAP_NHAT > 0)
            {
                Info._init(DMFWNhanVien.GetFullName(NGUOI_CAP_NHAT), NGAY_CAP_NHAT.ToString());
                Info.Enabled = true;
            }
            else
            {
                Info.Enabled = false;
            }
        }

        public static void SetInfoBox(PLInfoBox Info, long NGUOI_CAP_NHAT, DateTime? NGAY_CAP_NHAT, long NGUOI_DUYET, DateTime? NGAY_DUYET)
        {
            if (NGUOI_CAP_NHAT > 0)
            {
                if (NGUOI_DUYET == -1)
                    Info._init(DMFWNhanVien.GetFullName(NGUOI_CAP_NHAT), NGAY_CAP_NHAT.ToString(),
                                "", "");
                else
                    Info._init(DMFWNhanVien.GetFullName(NGUOI_CAP_NHAT), NGAY_CAP_NHAT.ToString(),
                                DMFWNhanVien.GetFullName(NGUOI_DUYET), NGAY_DUYET.ToString());
                Info.Enabled = true;
            }
            else
            {
                Info.Enabled = false;
            }
        }

        public static void SetInfoBoxExt(PLInfoBox Info, long NGUOI_CAP_NHAT, DateTime? NGAY_CAP_NHAT, long NGUOI_DUYET, DateTime? NGAY_DUYET)
        {
            if (NGUOI_CAP_NHAT > 0)
            {
                if (NGUOI_DUYET == -1)
                    Info._init(DMFWNhanVien.GetFullName(NGUOI_CAP_NHAT), NGAY_CAP_NHAT.ToString(),
                                "", "");
                else
                    Info._init(DMFWNhanVien.GetFullName(NGUOI_CAP_NHAT), NGAY_CAP_NHAT.ToString(),
                                DMFWNhanVien.GetFullName(NGUOI_DUYET), NGAY_DUYET.ToString());
                Info.Enabled = true;
            }
            else
            {
                Info.Enabled = false;
            }
        }

        public static void InitDuyetInfo(PLInfoBox Info, PLDuyetCombobox DuyetCtrl, DOPhieu Phieu)
        {
            try
            {
                string Duyet = "4";
                try
                {
                    Duyet = Phieu.GetType().GetProperty("DUYET").GetValue(Phieu, null).ToString().Trim();
                    DuyetCtrl.SetDuyet(Phieu);
                }
                catch { }
                string NguoiCapNhat = DMFWNhanVien.GetFullName(Phieu.GetType().GetProperty("NGUOI_CAP_NHAT").GetValue(Phieu, null));
                string NgayCapNhat = Phieu.GetType().GetProperty("NGAY_CAP_NHAT").GetValue(Phieu, null).ToString();
                if (Duyet == "2" || Duyet == "3")
                {
                    Info._init(NguoiCapNhat, NgayCapNhat,
                      DMFWNhanVien.GetFullName(Phieu.GetType().GetProperty("NGUOI_DUYET").GetValue(Phieu, null)),
                      Phieu.GetType().GetProperty("NGAY_DUYET").GetValue(Phieu, null).ToString());
                }
                else
                {
                    Info._init(NguoiCapNhat, NgayCapNhat);
                }
                DuyetCtrl.Enabled = false;
            }
            catch { }
        }

        public static void InitDuyetInfoExt(PLInfoBox Info, PLDuyetCombobox DuyetCtrl, DOPhieu Phieu)
        {
            try
            {
                string Duyet = "4";
                try
                {
                    Duyet = Phieu.GetType().GetProperty("DUYET").GetValue(Phieu, null).ToString().Trim();
                    DuyetCtrl.SetDuyet(Phieu);
                }
                catch { }
                string NguoiCapNhat = DMFWNhanVien.GetFullName(Phieu.GetType().GetProperty("NGUOI_CAP_NHAT").GetValue(Phieu, null));
                string NgayCapNhat = Phieu.GetType().GetProperty("NGAY_CAP_NHAT").GetValue(Phieu, null).ToString();
                if (Duyet == "2" || Duyet == "3")
                {
                    Info._init(NguoiCapNhat, NgayCapNhat,
                      DMFWNhanVien.GetFullName(Phieu.GetType().GetProperty("NGUOI_DUYET").GetValue(Phieu, null)),
                      Phieu.GetType().GetProperty("NGAY_DUYET").GetValue(Phieu, null).ToString());
                }
                else
                {
                    Info._init(NguoiCapNhat, NgayCapNhat);
                }
                DuyetCtrl.Enabled = false;
            }
            catch { }
        }
    }

    [Obsolete("Sử dụng lớp HelpPhieu để thay thế")]
    public class HelpPhieuForm : HelpPhieu
    {
    }
}
