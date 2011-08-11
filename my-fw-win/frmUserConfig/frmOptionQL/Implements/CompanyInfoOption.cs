using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class CompanyInfoOption : DevExpress.XtraEditors.XtraUserControl, IConfigOption
    {
        private CompanyInfo company;
        public CompanyInfoOption()
        {
            InitializeComponent();
            initData();
            this.pictureEdit1.Properties.ReadOnly = true;
        }

        public bool SaveConfig()
        {
            PLMessageBox.ShowErrorMessage("Thông tin chỉ đọc nếu muốn cập nhật vào \"Hệ thống\\Hồ sơ công ty\" để cập nhật.");
            return true;

            //if (User.isAdmin() == false)
            //{
            //    PLMessageBox.ShowErrorMessage("Bạn không được phép cập nhật thông tin này."+
            //                                  "\nTài khoản admin mới được cập nhật thông tin này.");
            //    return false;
            //}

            //try
            //{
            //    getData();
            //    company.update();
            //    ApplyFormatAction.Culture = null;
            //    Application.CurrentCulture = ApplyFormatAction.GetCultureInfo();
            //    HelpMsgBox.ShowNotificationMessage("Lưu thông tin cấu hình thành công.");
            //    return true;
            //}
            //catch
            //{
            //    PLMessageBox.ShowErrorMessage("Lưu thông tin cấu hình thất bại.");
            //    return false;
            //}
        }

        //private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        //{
        //    DialogResult dialogResult = this.openFileDialog1.ShowDialog();
        //    if (dialogResult == DialogResult.Cancel || openFileDialog1.FileName == "")
        //        return;
        //    try
        //    {
        //        byte[] logoByte = CompanyInfo.readBitmap2ByteArray(openFileDialog1.FileName);
        //        company.logo = logoByte;
        //        HelpImage.LoadImage(this.pictureEdit1, logoByte);
        //    }
        //    catch (Exception ex)
        //    {
        //        PLException.AddException(ex);
        //        FWMsgBox.showErrorImage();
        //    }
        //}

        private void initData()
        {
            company = new CompanyInfo();
            company.load();
            this.txtCompanyName.Text = company.name;
            this.txtTradeName.Text = company.tradeName;
            this.txtRepresentative.Text = company.representative;
            this.mmeAddress.Text = company.address;
            this.txtPhone.Text = company.phone;
            this.txtFax.Text = company.fax;
            this.txtEmail.Text = company.email;
            this.txtWebsite.Text = company.website;
            this.txtAccountNo.Text = company.accountNo;
            this.txtBankName.Text = company.bankName;
            this.txtTaxCode.Text = company.taxCode;
            HelpImage.LoadImage(this.pictureEdit1, company.logo);
        }

        private void trimAllData()
        {
            GUIValidation.TrimAllData(
                new object[]{
                    this.txtCompanyName,
                    this.txtTradeName,
                    this.txtRepresentative,
                    this.mmeAddress,
                    this.txtPhone,
                    this.txtFax,
                    this.txtEmail,
                    this.txtWebsite,
                    this.txtAccountNo,
                    this.txtBankName,
                    this.txtTaxCode
                }
            );
        }

        private void getData()
        {
            trimAllData();
            company.name = txtCompanyName.Text;
            company.tradeName = txtTradeName.Text;
            company.representative = txtRepresentative.Text;
            company.address = mmeAddress.Text;
            company.phone = txtPhone.Text;
            company.fax = txtFax.Text;
            company.email = txtEmail.Text;
            company.website = txtWebsite.Text;
            company.accountNo = txtAccountNo.Text;
            company.bankName = txtBankName.Text;
            company.taxCode = txtTaxCode.Text;
        }

        #region IConfigOption Members


        public object runAfterShowControl(frmXPOption input)
        {
            input.btnSave.Visible = false;
            return null;
        }

        #endregion
    }
}
