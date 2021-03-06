using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using System.Resources;
using System.Collections;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win
{
    public partial class frmCapNhatTiGia : XtraFormPL
    {
        #region Vùng khai báo biến
        private ImageComboBoxEdit ComboBoxEdit_TienTe = new ImageComboBoxEdit();
        DataSet DS=new DataSet ();
        long TT_ID = new long ();
        int rowHandle = new int();
        private long TT_ID_CellValueChanging = new long();
        #endregion

        #region Hàm dựng và Form Load
        public frmCapNhatTiGia()
        {
            InitializeComponent();
            InitGrid();
        }

        private void frmCapNhatTiGia_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
            XemTheoNgay(Ngay);
        }
        #endregion

        #region Hàm khởi tạo
        public void InitGrid()
        {
            gridViewUpdateTiGia.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            InitGridControl(this.gridUpdateTiGia, this.gridViewUpdateTiGia);
            XtraGridSupportExt.LookUpGridColumn(CotTienTe, "ID", "NAME", "FW_DM_TIEN_TE", new string[] { "NAME" }, new string[] { "Tiền tệ" }, "TT_ID");
            XtraGridSupportExt.TextCenterColumn(CotTiGiaHienTai, "TI_GIA");
            XtraGridSupportExt.TextCenterColumn(CotNGuoiCapNhat, "TENNV");
            XtraGridSupportExt.TextCenterColumn(CotNgayCapNhat, "NGAY_CAP_NHAT");
            XtraGridSupportExt.DecimalGridColumn(CotTiGiaMoi, "GIA_MOI");  
            HelpDate.OneWeekAgo(Ngay, Ngay);
        }

        public  void InitGridControl(GridControl gridControl, GridView gridView)
        {            
            ((PLGridView)gridView)._SetUserLayout();
            CotDong = XtraGridSupportExt.CloseButton(gridControl, gridView);           
        }        
        #endregion

        #region Xử lý sự kiện Button        
        private void btnSave_Click(object sender, EventArgs e)
        {           
            DataView view = (DataView)gridViewUpdateTiGia.DataSource;
            
            bool flag = true;
            foreach (DataRow row in view.Table.Rows)
            {
                long ID = new long();                
                decimal GIA_MOI = new decimal();
                long TT_ID = new long();
                long NGUOI_CAP_NHAT = new long();
                DateTime NGAY_CAP_NHAT = new DateTime();                
                if (row.RowState != DataRowState.Unchanged)
                {
                    if ((row.RowState == DataRowState.Added ))
                    {
                        ID = HelpNumber.ParseInt64(row["ID"]);
                        GIA_MOI = HelpNumber.ParseDecimal(row["TI_GIA"]);
                        TT_ID = HelpNumber.ParseInt64(row["TT_ID"]);
                        NGUOI_CAP_NHAT = FrameworkParams.currentUser.employee_id;
                        NGAY_CAP_NHAT = DABase.getDatabase().GetSystemCurrentDateTime();
                        // Lưu dòng vừa cập nhật vào database                        
                        flag = Update_Data(ID, GIA_MOI, TT_ID, NGUOI_CAP_NHAT, NGAY_CAP_NHAT, ActionSQL.ThêmMới);
                        if (flag == true)
                        {
                            row.AcceptChanges();
                            row.SetModified();
                        }
                    }                   
                }
            }
            if (flag == true)
            {               
                this.gridViewUpdateTiGia.FocusedRowHandle = DS.Tables[0].Rows.Count - 1;
                if (this.rowHandle >= 0)
                    this.TT_ID = this.Get_TT_ID(this.rowHandle);
            }
            else
                ErrorSave(this);
            btnAddNew.Enabled = true;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {               
               this.CotTiGiaMoi.OptionsColumn.AllowEdit = true;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            XemTheoNgay(Ngay);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            HelpXtraForm.CloseFormNoConfirm(this);
        }

        private void btnDMTienTe_Click(object sender, EventArgs e)
        {
            ProtocolForm.ShowModalDialog(this, FW_DM_TIEN_TE.GetFW_DM_TIEN_TE_Dialog(), false);
        }
        #endregion

        #region Xử lý sự kiện trên lưới        
        private void gridViewUpdateTiGia_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            PLGridView grid = ((PLGridView)sender);
            DataRow row = grid.GetDataRow(e.RowHandle);
            row.ClearErrors();
            if (HelpNumber.ParseDecimal(row["GIA_MOI"]) <= 0)
            {
                e.Valid = false;
                row.SetColumnError("GIA_MOI", "Tỉ giá mới phải lớn hơn 0");
                return;
            }
            else if (HelpNumber.ParseDecimal(row["GIA_MOI"]) >= 999999)
            {
                e.Valid = false;
                row.SetColumnError("GIA_MOI", "Tỉ giá mới nhập vào quá lớn, vui lòng nhập lại!");
                return;
            }
        }

        private void gridViewUpdateTiGia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataView view = (DataView)gridViewUpdateTiGia.DataSource;
            if (e.FocusedRowHandle >= 0)
            {

                if (view.Table.Rows[e.FocusedRowHandle].RowState == DataRowState.Added)
                {
                    this.CotTienTe.OptionsColumn.AllowEdit = true;
                    this.CotTiGiaMoi.OptionsColumn.AllowEdit = true;
                }
                else
                {
                    this.CotTienTe.OptionsColumn.AllowEdit = false;
                    this.CotTiGiaMoi.OptionsColumn.AllowEdit = false;

                }                
                this.rowHandle = e.FocusedRowHandle;
                this.TT_ID = this.Get_TT_ID(this.rowHandle);

            }
        }

        private void gridViewUpdateTiGia_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == CotTiGiaMoi)
            {                
                long ID = DABase.getDatabase().GetID( HelpGen.G_FW_DM_ID );
                decimal GIA_MOI = HelpNumber.ParseDecimal(e.Value);
                long TT_ID = HelpNumber.ParseInt64(gridViewUpdateTiGia.GetDataRow(e.RowHandle)["TT_ID"]);
                long NGUOI_CAP_NHAT = FrameworkParams.currentUser.employee_id;
                DateTime NGAY_CAP_NHAT = DABase.getDatabase().GetSystemCurrentDateTime();                                
                if (GIA_MOI > 0 && GIA_MOI < 999999)
                {
                    DataRow newRow = this.DS.Tables[0].NewRow();
                    newRow["ID"] = ID;
                    newRow["TT_ID"] = TT_ID;
                    newRow["TI_GIA"] = GIA_MOI;
                    newRow["NGAY_CAP_NHAT"] = NGAY_CAP_NHAT;
                    newRow["NGUOI_CAP_NHAT"] = NGUOI_CAP_NHAT;
                    newRow["TENNV"] = this.GetTEN_NGUOI_CAP_NHAT(FrameworkParams.currentUser.employee_id);
                    this.DS.Tables[0].Rows.Add(newRow);
                    gridUpdateTiGia.DataSource = DS.Tables[0].Copy();
                }
            }
        }

        private void gridViewUpdateTiGia_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "TT_ID")
            {              
                this.TT_ID_CellValueChanging = HelpNumber.ParseInt64(e.Value.ToString());
            }
        }

        private void gridViewUpdateTiGia_DoubleClick(object sender, EventArgs e)
        {
            PLGridView grid = (PLGridView)sender;
            if (grid.FocusedColumn == CotTiGiaMoi)
            {
                this.CotTiGiaMoi.OptionsColumn.AllowEdit = true;
            }
        }

        private void gridViewUpdateTiGia_RowCountChanged(object sender, EventArgs e)
        {
            DataView view = (DataView)gridViewUpdateTiGia.DataSource;
            foreach (DataRow row in view.Table.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    this.Update_Data(0, 0, this.TT_ID, 0, DateTime.Now, ActionSQL.Xóa);
            }

        }
        #endregion

        #region Các hàm Support
        // hàm lấy tên thật nhân viên theo ID
        public string GetTEN_NGUOI_CAP_NHAT(long id)
        {
            return DABase.getDatabase().LoadDataSet(
                "select * from DM_NHAN_VIEN where ID=" + id + "").Tables[0].Rows[0]["NAME"].ToString();
        }

        public enum ActionSQL
        {
            ThêmMới,
            CâpNhật,
            Xóa
        };       

        public DataSet LoadData()
        {
            DatabaseFB db = DABase.getDatabase();
            DataSet ds = new DataSet();
            ds = db.LoadDataSet(@"  select c.ID,nv.name TENNV,t.id TT_ID,t.name,c.ti_gia,c.nguoi_cap_nhat,c.ngay_cap_nhat 
                                    from dm_nhan_vien nv, fw_dm_tien_te t, fw_ti_gia c 
                                    where t.id=c.tt_id and nv.id=c.nguoi_cap_nhat 
                                    order by t.name asc, c.ngay_cap_nhat desc");
            
            ds.Tables[0].Columns.Add("GIA_MOI"); 
            return ds;
        }

        public long Get_TT_ID(int rowHandle)
        {
            if (rowHandle < 0)
                return -2;
            return HelpNumber.ParseInt64(this.DS.Tables[0].Rows[rowHandle]["ID"]);
        }

        public bool Update_Data(long ID, decimal GIA_MOI, long TT_ID, 
            long NGUOI_CAP_NHAT, DateTime NGAY_CAP_NHAT, ActionSQL action)
        {
            DatabaseFB db = DABase.getDatabase();
            DbTransaction dbtran = db.BeginTransaction(db.OpenConnection());
            try
            {
                string strcmd = string.Empty;
                if (action == ActionSQL.ThêmMới)
                {
                    strcmd = "INSERT INTO FW_TI_GIA VALUES(@ID,@TT_ID,@TI_GIA,@NGUOI_CAP_NHAT,@NGAY_CAP_NHAT)";
                    DbCommand cmd = db.GetSQLStringCommand(strcmd);
                    db.AddInParameter(cmd, "@ID", DbType.Int64, ID);
                    db.AddInParameter(cmd, "@TT_ID", DbType.Int64, TT_ID);
                    db.AddInParameter(cmd, "@TI_GIA", DbType.Decimal, GIA_MOI);                    
                    db.AddInParameter(cmd, "@NGUOI_CAP_NHAT", DbType.Int64, NGUOI_CAP_NHAT);
                    db.AddInParameter(cmd, "@NGAY_CAP_NHAT", DbType.DateTime, NGAY_CAP_NHAT);
                    db.ExecuteNonQuery(cmd, dbtran);
                    db.CommitTransaction(dbtran);
                }                
                else if (action == ActionSQL.Xóa)
                {
                    strcmd = "DELETE FROM FW_TI_GIA WHERE ID='" + TT_ID + "'";
                    DbCommand cmd = db.GetSQLStringCommand(strcmd);
                    db.ExecuteNonQuery(cmd, dbtran);
                    db.CommitTransaction(dbtran);
                }
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                db.RollbackTransaction(dbtran);
                return false;
            }
        }        
        
        public void XemTheoNgay(DateEdit date)
        {           
            DatabaseFB db = DABase.getDatabase();      

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("GET_TI_GIA_THEO_NGAY");
                if(date.EditValue== null)
                    db.AddInParameter(cmd, "@NGAY_XEM", DbType.Date, DBNull.Value);
                else
                    db.AddInParameter(cmd, "@NGAY_XEM", DbType.Date, date.DateTime);
                DataSet ds = db.LoadDataSet(cmd, "CAP_NHAT_TI_GIA");
                ds.Tables[0].Columns.Add("GIA_MOI");
                ds = BoSungInfo(ds.Copy());
                this.DS = ds.Copy();
                // thay doi mau cua dong co ngay cap nhat nho hon ngay can xem
                HelpGrid.SetColorCondition(gridViewUpdateTiGia, "NGAY_CAP_NHAT",
                    new FormatConditionEnum[1] { FormatConditionEnum.Less },
                    new object[] { (object)StringToDate(Ngay.DateTime) }, new object[] { null }, Color.Red);
                             
                gridUpdateTiGia.DataSource = this.DS.Copy().Tables[0];
                this.rowHandle = 0;
                // khoi tao du lieu cho PLMoneyType
                plMoneyType1._init(true);
            }
            catch { }
        }

        DataSet BoSungInfo(DataSet ds)
        { 
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                if (row["ID"] == DBNull.Value)
                {
                    row["NGUOI_CAP_NHAT"] = FrameworkParams.currentUser.employee_id;
                    row["TENNV"] = this.GetTEN_NGUOI_CAP_NHAT(FrameworkParams.currentUser.employee_id);
                    row["NGAY_CAP_NHAT"] = DABase.getDatabase().GetSystemCurrentDateTime();
                }
            }
            return ds;           
        }

        DateTime StringToDate(DateTime date)
        {
           int ngay,thang,nam;
           ngay = date.Day;
           thang = date.Month;
           nam = date.Year;
           string strdate = ngay.ToString() + "/" + thang.ToString() + "/" + nam.ToString() + " 01:00:00 AM";
           try
           {
               return Convert.ToDateTime(strdate);
           }
           catch { return DateTime.Now; }
        }

        public static void ErrorSave(object a)
        {
            string msg = "Lưu thông tin không thành công. Vui lòng kiểm tra lại số liệu.";
            List<Exception> listExs = PLException.GetLastestExceptions();

            if (listExs.Count >= 0)
            {
                string tmp = PLDebug.GetUserErrorMsg(PLException.GetLastestExceptions());
                if (tmp != "" && tmp.IndexOf("PROTOCOL") == 0) msg = tmp;
            }
            HelpMsgBox.ShowNotificationMessage(msg);
        }
        #endregion        
    }
}