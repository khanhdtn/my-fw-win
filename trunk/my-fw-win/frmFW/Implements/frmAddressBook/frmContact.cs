using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors.DXErrorProvider;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    public partial class frmContact : DevExpress.XtraEditors.XtraForm, IPublicForm
    {
        private long idGroup;
        private DataRow data;
        private string genContact;
        private string tableNameContact;
        private DataOperation flag;
        private OpenFileDialog fileDialog;
        DXErrorProvider errorProvider;
        private long idContact;
        public frmContact()
        {
            InitializeComponent();
            picHinh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;

            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Hình ảnh(*.bmp,*.icon,*.ico,*.jpg,*.jpeg,*.gif,*.png)|*.bmp;*.icon;*.ico;*.jpg;*.jpeg;*.gif;*.png";
            fileDialog.Title = "Mở tập tin";
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            fileDialog.Multiselect = false;

            errorProvider  = new DXErrorProvider();
            data = CreateTable().NewRow();
        }

        public frmContact(object idGroup, string genContact,string tableNameContact, DataRow dr):this()
        {
            this.idGroup = HelpNumber.ParseInt64(idGroup);
            idContact = HelpNumber.ParseInt64(dr["ID"].ToString());
            this.data = dr;
            this.flag = DataOperation.Edit;
            this.genContact = genContact;
            this.tableNameContact = tableNameContact;
            SetValue(dr);
        }

        public frmContact(object idGroup,string genContact, string tableNameContact):this()
        {
            this.idGroup = HelpNumber.ParseInt64(idGroup);
            this.genContact = genContact;
            this.flag = DataOperation.Add;
            this.tableNameContact = tableNameContact;
        }

        #region Event
        private void barThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.flag = DataOperation.Add;
            SetEmptyValue();
        }

        private void barXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chăc muốn xóa không?");
                if (result == DialogResult.Yes)
                {
                    UpdateContact(DataOperation.Delete);
                    SetEmptyValue();
                    flag = DataOperation.Add;
                }
            }
            catch { }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                if (flag == DataOperation.Add)
                {
                    if (UpdateContact(DataOperation.Add) == -1)
                        PLMessageBox.ShowErrorMessage("Lưu không thành công");
                    else
                    {
                        HelpMsgBox.ShowNotificationMessage("Thông tin cá nhân đã được lưu");
                        flag = DataOperation.Edit;
                    }
                }
                else
                {
                    if (UpdateContact(DataOperation.Edit) == -1)
                        PLMessageBox.ShowErrorMessage("Lưu không thành công");
                    else
                        HelpMsgBox.ShowNotificationMessage("Thông tin cá nhân đã được lưu");
                }
            }
        }

        private bool CheckValidation()
        {
            errorProvider.ClearErrors();
            if (txtHoTen.Text == "")
            {
                errorProvider.SetError(txtHoTen, "Bạn chưa nhập tên");
                return false;
            }
            else if (txtDienThoai.Text == "")
            {
                errorProvider.SetError(txtDienThoai, "Bạn chưa nhập số điện thoại");
                return false;
            }
            else if (txtEmail.Text == "")
            {
                errorProvider.SetError(txtEmail, "Bạn chưa nhập email");
                return false;
            }

            try
            {
                DateTime dt = DateTime.Parse(txtNgaySinh.Text);
            }
            catch { 
                errorProvider.SetError(txtNgaySinh, "Bạn nhập ngày sinh không hợp lệ");
                return false;
            }
            return true;    
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            if (flag == DataOperation.Add)
                SetEmptyValue();
            else if(flag == DataOperation.Edit)
                SetValue(data);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barThemHinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                picHinh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                try
                {
                    using (Bitmap image = new Bitmap(fileDialog.FileName))
                    {
                        MemoryStream stream = new MemoryStream();
                        image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                        picHinh.EditValue = stream.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
        }
       
        private void barXoaHinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            picHinh.EditValue = null;
        }
        #endregion

        #region Function
        private void SetEmptyValue()
        {
            txtHoTen.Text = "";
            txtCMND.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtEmail.Text = "";
            txtGhiChu.Text = "";
            picHinh.EditValue = null;
        }

        private void SetValue(DataRow dr)
        {
            txtHoTen.Text = dr["NAME"].ToString();
            txtCMND.Text = dr["CMND"].ToString();
            try
            {
                txtNgaySinh.Text = dr["NGAY_SINH"].ToString().Remove(dr["NGAY_SINH"].ToString().IndexOf(" "));
            }
            catch { }
            txtDiaChi.Text = dr["DIA_CHI"].ToString();
            txtDienThoai.Text = dr["DIEN_THOAI"].ToString();
            txtEmail.Text = dr["EMAIL"].ToString();
            txtGhiChu.Text = dr["GHI_CHU"].ToString();
            try
            {
                byte[] binImag = (byte[])dr["HINH"];
                picHinh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                picHinh.EditValue = binImag;
            }
            catch { }
        }
        
        private DataTable CreateTable()
        {
            DataTable dt = new DataTable("CONTACT");
            DataColumn dtId = new DataColumn("ID");
            dt.Columns.Add(dtId);
            DataColumn dtName = new DataColumn("NAME");
            dt.Columns.Add(dtName);
            DataColumn dtCMND = new DataColumn("CMND");
            dt.Columns.Add(dtCMND);
            DataColumn dtDiaChi = new DataColumn("DIA_CHI");
            dt.Columns.Add(dtDiaChi);
            DataColumn dtDienThoai = new DataColumn("DIEN_THOAI");
            dt.Columns.Add(dtDienThoai);
            DataColumn dtNgaySinh = new DataColumn("NGAY_SINH");
            dt.Columns.Add(dtNgaySinh);
            DataColumn dtEmail = new DataColumn("EMAIL");
            dt.Columns.Add(dtEmail);
            DataColumn dtGhiChu = new DataColumn("GHI_CHU");
            dt.Columns.Add(dtGhiChu);
            DataColumn dtHinh = new DataColumn("HINH");
            dt.Columns.Add(dtHinh);
            return dt;
        }
        private void SetNewData()
        {
            data["ID"] = idContact.ToString();
            data["NAME"] = txtHoTen.Text;
            data["CMND"] = txtCMND.Text;
            data["DIA_CHI"] = txtDiaChi.Text;
            data["DIEN_THOAI"] = txtDienThoai.Text;
            data["NGAY_SINH"] = txtNgaySinh.Text;
            data["EMAIL"] = txtEmail.Text;
            data["GHI_CHU"] = txtGhiChu.Text;
            if (picHinh.EditValue != null)
                data["HINH"] = (byte[])picHinh.EditValue;
        }
        private int UpdateContact(DataOperation operation)
        {
            try
            {
                string command;
                if (operation == DataOperation.Add)
                {
                    DataSet ds = DABase.getDatabase().LoadDataSet("SELECT * FROM " + tableNameContact + " WHERE 1=" + -1, tableNameContact);
                    DataRow row = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(row);

                    idContact = DABase.getDatabase().GetID(genContact);
                    row["ID"] = idContact;
                    row["NAME"] = txtHoTen.Text.Trim();
                    row["CMND"] = txtCMND.Text.Trim();
                    row["NGAY_SINH"] = DateTime.Parse(txtNgaySinh.Text).Date;
                    row["DIA_CHI"] = txtDiaChi.Text.Trim();
                    row["DIEN_THOAI"] = txtDienThoai.Text.Trim();
                    row["EMAIL"] = txtEmail.Text.Trim();
                    row["GROUP_ID"] = idGroup;
                    row["GHI_CHU"] = txtGhiChu.Text.Trim();

                    if (picHinh.EditValue != null)
                        row["HINH"] = (byte[])picHinh.EditValue;
                    else
                        row["HINH"] = null;


                    if (DABase.getDatabase().UpdateDataSet(ds) == true)
                    {
                        SetNewData();
                        return 1;
                    }
                    return -1;
                    //command = "INSERT INTO " + tableNameContact + "(ID,NAME,CMND,NGAY_SINH,DIEN_THOAI,DIA_CHI,EMAIL,GROUP_ID,GHI_CHU,HINH) VALUES(@ID,@NAME,@CMND,@NGAY_SINH,@DIEN_THOAI,@DIA_CHI,@EMAIL,@GROUP_ID,@GHI_CHU,@HINH)";
                    //DbCommand dbCommand = DABase.getDatabase().GetSQLStringCommand(command);
                    //idContact = DABase.getDatabase().GetID(genContact);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@ID", DbType.Int64,idContact );
                    //DABase.getDatabase().AddInParameter(dbCommand, "@NAME", DbType.String, txtHoTen.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@CMND", DbType.String, txtCMND.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@NGAY_SINH", DbType.Date, DateTime.Parse(txtNgaySinh.Text).Date);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@DIA_CHI", DbType.String, txtDiaChi.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@DIEN_THOAI", DbType.String, txtDienThoai.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@EMAIL", DbType.String, txtEmail.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@GROUP_ID", DbType.Int64, idGroup);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@GHI_CHU", DbType.String, txtGhiChu.Text);
                    //if (picHinh.EditValue != null)
                    //    DABase.getDatabase().AddInParameter(dbCommand, "@HINH", DbType.Binary, (byte[])picHinh.EditValue);
                    //else
                    //    DABase.getDatabase().AddInParameter(dbCommand, "@HINH", DbType.Binary, null);
                    //if (DABase.getDatabase().ExecuteNonQuery(dbCommand) != -1)
                    //{
                    //    SetNewData();
                    //    return 1;
                    //}
                    //return -1;
                }
                else if (operation == DataOperation.Edit)
                {
                    DataSet ds = DABase.getDatabase().LoadDataSet("SELECT * FROM " + tableNameContact + " WHERE ID=" + idContact, tableNameContact);

                    ds.Tables[0].Rows[0]["NAME"] = txtHoTen.Text.Trim();
                    ds.Tables[0].Rows[0]["CMND"] = txtCMND.Text.Trim();
                    ds.Tables[0].Rows[0]["NGAY_SINH"] = DateTime.Parse(txtNgaySinh.Text).Date;
                    ds.Tables[0].Rows[0]["DIA_CHI"] = txtDiaChi.Text.Trim();
                    ds.Tables[0].Rows[0]["DIEN_THOAI"] = txtDienThoai.Text.Trim();
                    ds.Tables[0].Rows[0]["EMAIL"] = txtEmail.Text.Trim();
                    ds.Tables[0].Rows[0]["GROUP_ID"] = idGroup;
                    ds.Tables[0].Rows[0]["GHI_CHU"] = txtGhiChu.Text.Trim();
                    if (picHinh.EditValue != null)
                        ds.Tables[0].Rows[0]["HINH"] = (byte[])picHinh.EditValue;
                    else
                        ds.Tables[0].Rows[0]["HINH"] = null;

                    if (DABase.getDatabase().UpdateDataSet(ds) == true)
                    {
                        SetNewData();
                        return 1;
                    }
                    return -1;

                    //CÁCH 1:
                    //command = @"UPDATE " + tableNameContact + " SET NAME = @NAME , CMND = @CMND, NGAY_SINH=@NGAY_SINH, DIA_CHI = @DIA_CHI,DIEN_THOAI = @DIEN_THOAI,EMAIL = @EMAIL,GHI_CHU=@GHI_CHU ,HINH=@HINH " +
                    //" WHERE ID=@ID";
                    //DbCommand dbCommand = DABase.getDatabase().GetSQLStringCommand(command);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@ID", DbType.Int64, idContact);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@NAME", DbType.String, txtHoTen.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@CMND", DbType.String, txtCMND.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@NGAY_SINH", DbType.Date, DateTime.Parse(txtNgaySinh.Text).Date);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@DIA_CHI", DbType.String, txtDiaChi.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@DIEN_THOAI", DbType.String, txtDienThoai.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@EMAIL", DbType.String, txtEmail.Text);
                    //DABase.getDatabase().AddInParameter(dbCommand, "@GHI_CHU", DbType.String, txtGhiChu.Text);
                    //if (picHinh.EditValue!=null)
                    //    DABase.getDatabase().AddInParameter(dbCommand, "@HINH", DbType.Binary, (byte[])picHinh.EditValue);
                    //else
                    //    DABase.getDatabase().AddInParameter(dbCommand, "@HINH", DbType.Binary, null);
                    //if (DABase.getDatabase().ExecuteNonQuery(dbCommand) != -1)
                    //{
                    //    SetNewData();
                    //    return 1;
                    //}
                    //return -1;
                }
                else
                {
                    command = "DELETE FROM " + tableNameContact + " WHERE ID = " + data["ID"].ToString();
                    DbCommand dbCommand = DABase.getDatabase().GetSQLStringCommand(command);
                    return DABase.getDatabase().ExecuteNonQuery(dbCommand);
                }
            }
            catch { return -1; }

        }

        #endregion

        private void frmContact_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
    }
}
