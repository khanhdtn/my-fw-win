using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmOldConfigServer : XtraFormPL
    {
        public frmOldConfigServer()
        {
            InitializeComponent();
            initData();
        }

        private void trimAllData()
        {
            GUIValidation.TrimAllData(new object[] { 
                this.textSmtp, this.textPop, this.textEmailTaiKhoan, this.textMatKhau, this.textEmailUsername,
                this.textLDAPFullName,this.textLDAPEmail,this.textLDAPPath,
                this.textServerName,this.textUsername,this.textPassword,this.Cong, this.textSMTPPort, this.textPOPPort
            });
        }

        private void saveData()
        {
            trimAllData();
            try
            {
                //DOServer data = frmConfigServer._GetServer();
                //if (data == null) data = new DOServer();

                DOServer data = new DOServer();
                #region ServerMail                
                data.SMTP = this.textSmtp.Text;
                data.SMTP_PORT = this.textSMTPPort.Text;
                data.POP = this.textPop.Text;
                data.POP_PORT = this.textPOPPort.Text;
                data.EMAIL_USERNAME = this.textEmailUsername.Text;
                data.EMAIL = this.textEmailTaiKhoan.Text;
                data.PASS = this.textMatKhau.Text;
                #endregion

                #region LDAP
                data.LDAP_FULLNAME = this.textLDAPFullName.Text;
                data.LDAP_EMAIL = this.textLDAPEmail.Text;
                data.LDAP_PATH = this.textLDAPPath.Text;
                #endregion

                #region Ftp
                data.FTP_HOST = this.textServerName.Text;
                data.FTP_USERNAME = this.textUsername.Text;
                data.FTP_PORT = HelpNumber.ParseInt32(this.Cong.EditValue);
                data.FTP_PASS = this.textPassword.Text;

//                string xmlFile = @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
//                            <NewDataSet>
//                              <ftp>
//                                <ip>" + this.textServerName.Text + "</ip>" +
//                                    "<username>" + this.textUsername.Text + "</username>" +
//                                    "<password>" + this.textPassword.Text + "</password>" +
//                                    "<port>" + this.Cong.Text + "</port>" +
//                                  "</ftp>" +
//                                "</NewDataSet>";
//                ConfigFile.WriteXML(HelpFTP.FTP_FILE, xmlFile);
//                HelpFTP.loadFtp();
                #endregion

                #region Private Mail Server
                data.PRIVATE_MAIL_HOST = txtMailServer.Text;
                data.PRIVATE_MAIL_PORT = HelpNumber.ParseInt32(spPort.EditValue);
                data.PRIVATE_MAIL_DOMAIN = txtDomain.Text;
                data.PRIVATE_MAIL_SMTPPORT = HelpNumber.ParseInt32(spPortSMTP.EditValue);
                data.PRIVATE_MAIL_SSL_BIT = ((chkSSL.Checked) ? "Y" : "N");
                data.PRIVATE_MAIL_SSLSMTP_BIT = ((chkSSLSMTP.Checked) ? "Y" : "N");
                if (CENormal.Checked == true) data.PRIVATE_MAIL_METHOD = "0";
                if (CESimple.Checked == true) data.PRIVATE_MAIL_METHOD = "1";
                if (CEPrefix.Checked == true) data.PRIVATE_MAIL_METHOD = "2";
                
//                string content = @"<?xml version='1.0' encoding='utf-8' ?> 
//                                    <Config>
//                                      <Host>" + txtMailServer.Text + "</Host> " +
//                                      "<Port>" + spPort.EditValue.ToString() + "</Port> " +
//                                      "<Domain>" + txtDomain.Text + "</Domain> " +
//                                      "<PortSMTP>" + spPortSMTP.EditValue.ToString() + "</PortSMTP> " +
//                                      "<SSL>" + ((chkSSL.Checked) ? "1" : "0") + "</SSL> " +
//                                      "<SSLSMTP>" + ((chkSSLSMTP.Checked) ? "1" : "0") + "</SSLSMTP> " +
//                                      "<Login>" + Login + "</Login>" +
//                                      "</Config>";
//                ConfigFile.WriteXML(fileName, content);
                #endregion

                DAServer.Instance.Update(data);

                HelpMsgBox.ShowNotificationMessage("Thực hiện cấu hình thành công!");
            }
            catch(Exception ex) {
                PLException.AddException(ex);
            }
        }

        public bool SaveConfig()
        {
            try
            {
                saveData();
                return true;
            }
            catch
            {
                PLMessageBox.ShowErrorMessage("Lưu thông tin cấu hình thất bại");
                return false;
            }
        }
        
        public void initData()
        {
            this.btnConnect.Image =  FWImageDic.CONNECT_IMAGE16;
            this.btnKetNoi.Image = FWImageDic.CONNECT_IMAGE16;            
            try
            {
                DOServer data = frmConfigServer._GetServer();
                if (data == null) data = new DOServer();

                #region ServerMail
                this.textSmtp.EditValue = data.SMTP;
                this.textSMTPPort.EditValue = data.SMTP_PORT;
                this.textPop.EditValue = data.POP;
                this.textPOPPort.EditValue = data.POP_PORT;
                this.textEmailUsername.EditValue = data.EMAIL_USERNAME;
                this.textMatKhau.EditValue = data.PASS;
                this.textEmailTaiKhoan.EditValue = data.EMAIL;
                this.textLDAPFullName.EditValue = data.LDAP_FULLNAME;
                this.textLDAPEmail.EditValue = data.LDAP_EMAIL;
                this.textLDAPPath.EditValue = data.LDAP_PATH;
                #endregion

                #region Ftp
                //DataSet ds = new DataSet();
                //ConfigFile.ReadXML(HelpFTP.FTP_FILE , ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    textServerName.EditValue = ds.Tables[0].Rows[0]["ip"];
                //    textUsername.EditValue = ds.Tables[0].Rows[0]["username"];
                //    textPassword.EditValue = ds.Tables[0].Rows[0]["password"];
                //    Cong.Text = ds.Tables[0].Rows[0]["port"].ToString();
                //}
                this.textServerName.EditValue = data.FTP_HOST;
                this.textUsername.EditValue = data.FTP_USERNAME;
                this.textPassword.EditValue = data.FTP_PASS;
                this.Cong.EditValue = data.FTP_PORT;
                #endregion

                #region ConfigServer
                //if (!File.Exists(fileName))
                //    CreateFileConfigMail();

                //ds = new DataSet();
                //ConfigFile.ReadXML(fileName, ds);

                //txtMailServer.Text = ds.Tables[0].Rows[0]["Host"].ToString();
                //txtDomain.Text = ds.Tables[0].Rows[0]["Domain"].ToString();
                //spPort.EditValue = HelpNumber.ParseInt32(ds.Tables[0].Rows[0]["Port"].ToString());
                //spPortSMTP.EditValue = HelpNumber.ParseInt32(ds.Tables[0].Rows[0]["PortSMTP"].ToString());
                //chkSSL.Checked = ((ds.Tables[0].Rows[0]["SSL"].ToString().Equals("0")) ? false : true);
                //chkSSLSMTP.Checked = ((ds.Tables[0].Rows[0]["SSLSMTP"].ToString().Equals("0")) ? false : true);
                //switch (HelpNumber.ParseInt32(ds.Tables[0].Rows[0]["Login"].ToString()))
                //{
                //    case 0:
                //        CENormal.Checked = true;
                //        break;
                //    case 1:
                //        CESimple.Checked = true;
                //        break;
                //    case 2:
                //        CEPrefix.Checked = true;
                //        break;
                //}

                txtMailServer.Text = data.PRIVATE_MAIL_HOST;
                txtDomain.Text = data.PRIVATE_MAIL_DOMAIN;
                spPort.EditValue = data.PRIVATE_MAIL_PORT;
                spPortSMTP.EditValue = data.PRIVATE_MAIL_SMTPPORT;

                chkSSL.Checked = (data.PRIVATE_MAIL_SSL_BIT=="Y"? true : false);
                chkSSLSMTP.Checked = (data.PRIVATE_MAIL_SSLSMTP_BIT=="Y"? true:false);

                switch (HelpNumber.ParseInt32(data.PRIVATE_MAIL_METHOD))
                {
                    case 0:
                        CENormal.Checked = true;
                        break;
                    case 1:
                        CESimple.Checked = true;
                        break;
                    case 2:
                        CEPrefix.Checked = true;
                        break;
                }
                #endregion
            }
            catch { }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool bFlag = false;
            try
            {
                this.trimAllData();
                FrameworkParams.wait = new WaitingMsg();
                if (HelpFTP.Instance.checkConnect(textServerName.Text, Cong.Text, textUsername.Text, textPassword.Text))
                    bFlag = true;
            }
            catch (Exception ex)
            {
                FWMsgBox.showInvalidConnectServer(this);
            }
            finally {
                if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
                if(bFlag)
                    FWMsgBox.showValidConnect();
                else
                    FWMsgBox.showInvalidConnectServer(this);
            }
        }
        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            //bool bFlag = true;
            //int Login = 0;
            //try
            //{
            //    FrameworkParams.wait = new WaitingMsg();
            //    if (CENormal.Checked == true) Login = 0;
            //    if (CESimple.Checked == true) Login = 1;
            //    if (CEPrefix.Checked == true) Login = 2;
            //    string error = "";
            //    //HUYLX: UserMail va Password lay tu framework.
            //    Connect connect = new Connect(txtMailServer.Text, HelpNumber.ParseInt32(spPortSMTP.EditValue.ToString()),
            //    chkSSLSMTP.Checked, txtMailServer.Text, HelpNumber.ParseInt32(spPort.EditValue.ToString()),
            //    chkSSL.Checked, FrameworkParams.currentUser.username + "@" + txtDomain.Text,
            //    FrameworkParams.currentUser.id.ToString() + "_protocolvn", Login);
            //    if (connect.TestConnectServer(ref error))
            //        bFlag = true;
            //    if (error.Equals("Server"))
            //        bFlag = false;
            //}
            //catch (Exception ex)
            //{
            //    PLMessageBox.ShowErrorMessage("Kết nối với máy chủ không thành công");
            //}
            //finally {
            //    if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            //    if(bFlag)
            //        HelpMsgBox.ShowNotificationMessage("Kết nối với máy chủ thành công");
            //    else
            //        PLMessageBox.ShowErrorMessage("Kết nối với máy chủ không thành công");
            //}
        }

        private void checkEmail_Click(object sender, EventArgs e)
        {
            trimAllData();
            try
            {
                HelpEmail.sendEmail(textSmtp.Text, HelpNumber.ParseInt32(textSMTPPort.Text),
                    textEmailUsername.Text, textMatKhau.Text, textEmailTaiKhoan.Text, 
                    new string[] { textEmailTaiKhoan.Text }, "TEST SEND EMAIL", "OK", true);
                //HelpMsgBox.ShowNotificationMessage("Vui lòng kiểm tra hộp thư của bạn nếu nhận được email thì cấu hình Email là hợp lệ");
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
                HelpMsgBox.ShowNotificationMessage("Thông tin Email không hợp lệ. Xin vui lòng kiểm tra lại.");
            }
        }

        private void checkLDAP_Click(object sender, EventArgs e)
        {
            //???
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public static DOServer _GetServer(){
            try
            {
                DOServer data = DAServer.Instance.LoadAll(1);
                return data;
            }
            catch {
                return null;
            }
        }

        private void frmConfigServer_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
    }

    ///// <summary>
    ///// Đối tượng chứa các thông tin cấu hình 
    ///// 1.Mail Server (Smtp & POP)
    ///// 2.Mail System (Name,Email,Pass)
    ///// 3.LDAP (FULLNAME,EMAIL,PATH)
    ///// Bảng dữ liệu : OPTION_SERVER
    ///// (Record ID=1)
    ///// </summary>
    //public class DOServer : DOPhieu
    //{
    //    public Int64 ID;
    //    /// <summary>
    //    /// SMTP Server dùng để gửi email ra ngoài.
    //    /// </summary>
    //    public string SMTP;
    //    /// <summary>
    //    /// SMTP Port dùng để gửi email ra ngoài 
    //    /// </summary>
    //    public string SMTP_PORT;
    //    /// <summary>
    //    /// Tên đầy đủ của email ( thường để vào chỗ FROM )
    //    /// </summary>
    //    public string EMAIL;
    //    /// <summary>
    //    /// Tên tài khoản dùng để gửi ra ngoài, tên này là tên quan trong để authenticate trước khi gửi.
    //    /// </summary>
    //    public string EMAIL_USERNAME;
    //    /// <summary>
    //    /// Mật khẩu của tài khoản email dùng để gửi email ra bên ngoài.
    //    /// </summary>
    //    public string PASS;
    //    /// <summary>
    //    /// Tạm thời dùng thuộc tính này cho Tài khoản mail gửi thay vì cho Private Mail
    //    /// </summary>
    //    public string PRIVATE_MAIL_SSLSMTP_BIT;
        


    //    /// <summary>
    //    /// POP Server dùng để nhận email từ ngoài gửi vào
    //    /// </summary>
    //    public string POP;
    //    /// <summary>
    //    /// POP Port dùng để gửi email ra ngoài 
    //    /// </summary>
    //    public string POP_PORT;


    //    //Thông tin LDAP
    //    public string LDAP_FULLNAME;
    //    public string LDAP_EMAIL;
    //    public string LDAP_PATH;


    //    public string FTP_HOST;
    //    public int FTP_PORT;
    //    public string FTP_USERNAME;
    //    public string FTP_PASS;
        




    //    /// <summary>
    //    /// Hiện tại chưa dùng
    //    /// </summary>
    //    public string PRIVATE_MAIL_HOST;
    //    /// <summary>
    //    /// Hiện tại chưa dùng
    //    /// </summary>
    //    public int PRIVATE_MAIL_PORT;
    //    /// <summary>
    //    /// Hiện tại chưa dùng
    //    /// </summary>
    //    public int PRIVATE_MAIL_SMTPPORT;
    //    /// <summary>
    //    /// Hiện tại chưa dùng
    //    /// </summary>
    //    public string PRIVATE_MAIL_DOMAIN;
        
    //    /// <summary>
    //    /// Hiện tại chưa dùng
    //    /// </summary>
    //    public string PRIVATE_MAIL_SSL_BIT;
    //    /// <summary>
    //    /// Hiện tại chưa dùng
    //    /// </summary>
    //    public string PRIVATE_MAIL_METHOD;

    //    public DataSet DetailDataSet;
    //    public DOServer()
    //    {

    //    }
    //}

    //class DAServer : DAPhieu<DOServer>
    //{
    //    private static string KEY_FIELD_NAME = "ID";
    //    object[] FIELD_MAP = new object[] {  
    //            "ID", new IDConverter(),
    //            "SMTP", null,
    //            "SMTP_PORT", null,
    //            "POP", null,	
    //            "POP_PORT", null,
    //            "EMAIL_USERNAME", null,				
    //            "EMAIL", null,
    //            "PASS",null,
    //            "LDAP_FULLNAME",null,
    //            "LDAP_EMAIL",null,
    //            "LDAP_PATH",null,
    //            "FTP_HOST", null,
    //            "FTP_PORT", null,
    //            "FTP_USERNAME", null,
    //            "FTP_PASS", null,
    //            "PRIVATE_MAIL_HOST", null,
    //            "PRIVATE_MAIL_PORT", null,
    //            "PRIVATE_MAIL_SMTPPORT", null,
    //            "PRIVATE_MAIL_DOMAIN", null,
    //            "PRIVATE_MAIL_SSL_BIT", null,
    //            "PRIVATE_MAIL_SSLSMTP_BIT", null,
    //            "PRIVATE_MAIL_METHOD", null
    //    };
    //    public static DAServer Instance = new DAServer();
    //    public DAServer() : base() { }

    //    public override DataTypeBuilder DefineDetailDataTypeBuilder()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override DOServer LoadAll(long IDKey)
    //    {
    //        DOServer phieu = this.Load(IDKey);
    //        phieu.DetailDataSet = DatabaseFB.LoadDataSet("FW_SERVER_OPTION", KEY_FIELD_NAME, IDKey);
    //        return phieu;
    //    }

    //    public override bool UpdateDetail(DataSet Detail, DataSet Grid)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool ValidateDetail(DataRow row)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override DataTypeBuilder DefineDataTypeBuilder()
    //    {
    //        return new DataTypeBuilder(FIELD_MAP);
    //    }

    //    public override bool Delete(long IDKey)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override DOServer Load(long IDKey)
    //    {
    //        IDataReader reader = DatabaseFB.LoadRecord("FW_SERVER_OPTION", KEY_FIELD_NAME, IDKey);
    //        using (reader)
    //        {
    //            if (reader.Read())
    //            {
    //                DOServer data = (DOServer)this.Builder.CreateFilledObjectExt(typeof(DOServer), reader);

    //                return data;
    //            }
    //        }
    //        return new DOServer();
    //    }

    //    public override bool Update(DOServer data)
    //    {
    //        bool flag = false;
    //        if (data.DetailDataSet.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow row in data.DetailDataSet.Tables[0].Rows)
    //            {
    //                flag = HelpDataSet.UpdataDataRowExt(row, FIELD_MAP, data);
    //            }
    //        }
    //        else
    //        {
    //            DataRow row = data.DetailDataSet.Tables[0].NewRow();
    //            row["ID"] = 1;
    //            data.DetailDataSet.Tables[0].Rows.Add(row);
    //            flag = HelpDataSet.UpdataDataRowExt(row, FIELD_MAP, data);

    //        }
    //        if (flag)
    //        {
    //            DatabaseFB db = DABase.getDatabase();
    //            flag = db.UpdateDataSet(data.DetailDataSet);
    //        }
    //        return flag;
    //    }

    //    public override bool Validate(DOServer element)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}