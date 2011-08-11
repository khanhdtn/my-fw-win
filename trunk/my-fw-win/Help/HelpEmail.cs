using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Chilkat;
using System.Data;
using System.Net.Mail;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using System.Net;
using System.IO;
using System.Data.Common;
using Syncfusion.DocIO.DLS;
using LumiSoft.MailServer.API.UserAPI;
using System.Collections.Specialized;
using System.Collections;
using LumiSoft.Net.Mime;
using LumiSoft.Net.SMTP.Client;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ các hoạt động liên quan đến gửi Email
    /// </summary>
    public class HelpEmail
    {
        /// <summary>
        /// Chưa cài đặt. Tham khảo sử dụng TemplateEmailProcessor
        /// </summary>
        public static void sendXMLEmail()
        {

        }

        /// <summary>
        /// Chưa cài đặt. Tham khảo sử dụng 
        /// Message.SentClientLetter trong dự án WebLic
        /// </summary>
        public static void sendWordTemplateEMail()
        {

        }

        #region sendPlainEmail
        /// <summary>
        /// Gửi 1 Email chú ý 
        /// isShowStatusMessageBox = True : Thì gửi thành công sẽ nhận được 1 MessageBox sau khi gửi
        ///                        = False: Không cần thông báo.
        /// </summary>    
        [Obsolete("Không nên dùng hàm này")]
        public static bool sendEmail( string stmpServerName, int smtpPort, string from, string pass, string displayFrom, 
                string[] tos, string subject,
                string body, bool isShowStatusMessageBox)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(from, displayFrom, System.Text.Encoding.UTF8);
            foreach (String to in tos) msg.To.Add(to);                        
            msg.Subject = subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            //Add the Creddentials
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.Port = smtpPort;
            client.Host = stmpServerName;
            client.EnableSsl = true;

            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            object userState = msg;
            try
            {
                if(isShowStatusMessageBox){
                    //you can also call client.Send(msg)                   
                    //Nếu gửi thành công sẽ gọi lại phương thức nằm trong "client.SendCompleted"
                    client.SendAsync(msg, userState);
                }
                else{
                    client.Send(msg);
                }
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                if (isShowStatusMessageBox)
                    HelpMsgBox.ShowNotificationMessage("Email đã được gửi đi không thành công.");
                
                return false;
                PLException.AddException(ex);
            }
        }
        private static void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage mail = (MailMessage)e.UserState;
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                string cancelled = string.Format("[{0}] bị hủy.", subject);
                HelpMsgBox.ShowNotificationMessage(cancelled);
            }
            if (e.Error != null)
            {
                string error = String.Format("[{0}] {1}", subject, e.Error.ToString());
                HelpMsgBox.ShowNotificationMessage(error);
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage("Email đã được gửi thành công.");
            }
        }

        public static bool SendFromGMail(string from, string pass, string displayFrom,
                string[] tos, string subject, string body, bool isShowStatusMessageBox)
        {
            return sendEmail("smtp.gmail.com", 587, from, pass, displayFrom, tos, subject, body, isShowStatusMessageBox);
        }
        /// <summary>
        /// Sẽ gửi email từ tài khoản mặc định như được cấu hình trong màn hình cấu hình nghiệp vụ
        /// Nếu ko có cấu hình tài khoản đó sẽ dùng tại khoản mặc định của PROTOCOL để gửi.
        /// Trả về True: Gửi được không đồng nghĩa là đến với được người nhận
        ///        False: Chưa gửi được.
        /// </summary>
        public static bool sendEmail(string displayFrom, string[] tos, string subject, string body)
        {
            DOServer mailInfo = DAServer.Instance.LoadAll(1);
            return sendEmail(mailInfo.SMTP, HelpNumber.ParseInt32(mailInfo.SMTP_PORT), 
                mailInfo.EMAIL_USERNAME, mailInfo.PASS, displayFrom, tos, subject, body, false);
        }

        #region Liên quan đến 1 Email Default hỗ trợ việc gửi Email ra bên ngoài. Email này chỉ có ý nghĩa nếu mình không cấu hình Email để gửi.
        private const string DEFAULT_SENDER_EMAIL = "protocolvn.sender@gmail.com";
        private const string DEFAULT_SENDER_EMAIL_USERNAME = "protocolvn.sender";
        private const string DEFAULT_SENDER_PASS = "qazwsxedc123";
        private const string DEFAULT_SENDER_SMTP_SERVER = "smtp.gmail.com";
        private const string DEFAULT_SENDER_SSL = "N";
        private const int DEFAULT_SENDER_SMTP_PORT = 587;


        public const string DEFAULT_RECEIVER_EMAIL = "protocolvn.sender@gmail.com";        
        //public const string DEFAULT_RECEIVER_EMAIL = "protocolvn.online@gmail.com";
        
        private static DOServer GetDefaultSenderEmail()
        {
            DOServer doServer = new DOServer();
            doServer.SMTP = DEFAULT_SENDER_SMTP_SERVER;
            doServer.SMTP_PORT = "" + DEFAULT_SENDER_SMTP_PORT;
            doServer.EMAIL = DEFAULT_SENDER_EMAIL;
            doServer.EMAIL_USERNAME = DEFAULT_SENDER_EMAIL_USERNAME;
            doServer.PASS = DEFAULT_SENDER_PASS;
            doServer.PRIVATE_MAIL_SSLSMTP_BIT = DEFAULT_SENDER_SSL;

            return doServer;
        }

        #endregion
        public static bool sendFromPLEmail(string[] tos, string subject, string body)
        {
            return sendEmail(DEFAULT_SENDER_SMTP_SERVER, DEFAULT_SENDER_SMTP_PORT, 
                        DEFAULT_SENDER_EMAIL, DEFAULT_SENDER_PASS,
                        DEFAULT_SENDER_EMAIL, tos, subject, body, false);
        }
        #endregion

        #region SentMessageTemplateHTMLOutside
        private static bool FillMail(List<string> EmailReceive, string SubjectEmail, List<string> PathAttachment, ref Email Mail)
        {
            if (EmailReceive == null)
                return false;

            for (int i = 0; i < EmailReceive.Count; i++)
                if (!Mail.AddTo("", EmailReceive[i]))
                    return false;

            Mail.Subject = SubjectEmail;
            if (PathAttachment != null)
                for (int i = 0; i < PathAttachment.Count; i++)
                    Mail.AddFileAttachment(PathAttachment[i]);
            return true;
        }

        /// <summary>
        /// cover file word to body hmtl of mail,which embed fictures
        /// </summary>
        /// <param name="PathWordTemplate">path to word template file</param>
        /// <param name="Mail">Mail is embed body</param>
        /// <returns></returns>
        private static bool HTMLBody(DataSet DataFill, string PathWordTemplate, ref Email Mail)
        {
            string path = FrameworkParams.TEMP_FOLDER + @"\WordTemp";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            path += PathWordTemplate.Substring(PathWordTemplate.LastIndexOf("\\"));
            WordDocument WordDoc = new WordDocument();
            try
            {
                WordDoc.Open(PathWordTemplate);
                for (int i = 0; i < DataFill.Tables.Count; i++)
                {
                    WordDoc.MailMerge.ExecuteGroup(DataFill.Tables[i]);
                }
                WordDoc.Save(path);

                Word.ApplicationClass wd = new Word.ApplicationClass();
                Word.Document document = new Word.Document();
                object fileName = (object)path;
                object newTemplate = false;
                object docType = 0;
                object isVisible = true;
                object missing = System.Reflection.Missing.Value;

                document = wd.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);


                object oFileName = (object)(path.Substring(0, path.LastIndexOf(".")) + ".html");
                object oSaveFormat = (object)(Word.WdSaveFormat.wdFormatHTML);

                if (System.IO.File.Exists(oFileName.ToString()))
                    System.IO.File.Delete(oFileName.ToString());

                document.SaveAs(ref oFileName,
                                ref oSaveFormat,
                                ref missing,
                                ref missing,
                                ref missing,
                                ref missing,
                                ref missing,
                                ref missing,
                                ref missing,
                                ref missing,
                                ref missing);
                document.Close(ref missing, ref missing, ref missing);
                wd.Quit(ref missing, ref missing, ref missing);

                Mht mailbody = new Mht();
                mailbody.UnlockComponent("MHT-TEAMBEAN_1E1F4760821H");
                mailbody.UseCids = true;
                Mail = mailbody.GetEmail(oFileName.ToString());

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool SentMessage(string hostSMTP, int portSMTP, bool sslSMTP, string username, string password, Email Mail)
        {
            MailMan smtp = new MailMan();
            if (!(smtp.UnlockComponent("MAIL-TEAMBEAN_4895F76A292K")))
                return false;

            smtp.SmtpHost = hostSMTP;
            smtp.SmtpPort = portSMTP;
            smtp.SmtpUsername = username;
            smtp.SmtpPassword = password;

            Mail.Charset = "utf-8";
            if (sslSMTP)
                smtp.PopSsl = true;     //SSL support            
            else
                smtp.PopSsl = false;    //connect without SSL support 

            if (smtp.SendEmail(Mail))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Sent mail from a template word with content is text body 
        /// (Gửi mail từ một file template word có định dạng)
        /// </summary>
        /// <param name="EmailReceive">Sent mail to list mail address with same mail body</param>
        /// <param name="DataFill">DataSet to fill file word template</param>
        /// <param name="SubjectEmail">Subject of mail</param>
        /// <param name="PathWordTemplate">String path to file word template</param>
        /// <param name="PathAttachment">List paths to files will be sent with email</param>      
        /// <returns></returns>
        public static bool SentMessageTemplateHTMLOutside(List<string> EmailReceive, DataSet DataFill,
            string SubjectEmail, string PathWordTemplate, List<string> PathAttachment)
        {
            DOServer mailInfo = frmConfigServer._GetServer();
            if (mailInfo == null) mailInfo = GetDefaultSenderEmail();
            
            Email Mail = new Email();

            if (!HTMLBody(DataFill, PathWordTemplate, ref Mail))
                return false;

            if (!FillMail(EmailReceive, SubjectEmail, PathAttachment, ref Mail))
                return false;

            Mail.From = mailInfo.EMAIL;

            if (!SentMessage(mailInfo.SMTP, HelpNumber.ParseInt32(mailInfo.SMTP_PORT),
                (mailInfo.PRIVATE_MAIL_SSLSMTP_BIT == "Y" ? true : false), mailInfo.EMAIL_USERNAME, mailInfo.PASS, Mail))
                return false;
            return true;
        }
        #endregion

        [Obsolete("Hàm này khó dùng nên sử dụng hàm sendEmail")]
        public static void SendMail( string sEmail, string sPwd, string sSmtp, string sPop,
            string sTitle, Dictionary<string, string> dToAddress, string sToTitle, string sSubject,
            string sBody, Dictionary<string, string> dCC, List<string> sFile, int smtpPort)
        {
            try
            {
                MailAddress from = new MailAddress(sEmail, sToTitle);
                MailMessage message = new MailMessage();
                message.From = from;
                foreach (KeyValuePair<string, string> to in dToAddress)
                {
                    message.To.Add(new MailAddress(to.Key, to.Value));
                }
                foreach (KeyValuePair<string, string> cc in dCC)
                {
                    message.CC.Add(new MailAddress(cc.Key, cc.Value));
                }
                message.Subject = sSubject;
                message.Body = sBody;
                if (sFile != null)
                {
                    foreach (string file in sFile)
                    {
                        message.Attachments.Add(new Attachment(file));
                    }
                }
                SmtpClient client = new SmtpClient(sSmtp, smtpPort);//25
                NetworkCredential credential = new NetworkCredential(sEmail.Substring(0, sEmail.IndexOf('@')), sPwd);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = credential;
                client.Send(message);
            }
            catch (Exception ex)
            {
                ProtocolVN.Framework.Core.PLException.AddException(ex);
            }
        }


        #region SendSmartHost
        private static LumiSoft.Net.Mime.Mime CreateMessage(string Titile, string DisplayFrom, string EmailFrom, AddressList To, AddressList CC, AddressList Bcc, string Subject, string pFile)
        {
            LumiSoft.Net.Mime.Mime m = new LumiSoft.Net.Mime.Mime();
            MimeEntity texts_enity = null;
            MimeEntity attachments_entity = null;

            ///Văn bản
            if (pFile.Trim() != string.Empty)
            {
                m.MainEntity.ContentType = MediaType_enum.Multipart_mixed;
                texts_enity = m.MainEntity.ChildEntities.Add();
                texts_enity.ContentType = MediaType_enum.Multipart_alternative;
                attachments_entity = m.MainEntity;

                MimeEntity attachment_entity = attachments_entity.ChildEntities.Add();
                attachment_entity.ContentType = MediaType_enum.Application_octet_stream;
                attachment_entity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
                attachment_entity.ContentDisposition = ContentDisposition_enum.Attachment;
                attachment_entity.ContentDisposition_FileName = Path.GetFileName(pFile);
                attachment_entity.Data = File.ReadAllBytes(pFile);
            }
            else
            {
                m.MainEntity.ContentType = MediaType_enum.Multipart_alternative;
                texts_enity = m.MainEntity;
            }

            ///Thông tin mail
            if (EmailFrom != "")
            {
                AddressList from = new AddressList();
                from.Add(new MailboxAddress(DisplayFrom, EmailFrom));
                m.MainEntity.From = from;
            }
            if (To != null)
                m.MainEntity.To = To;
            m.MainEntity.Subject = Titile;
            if (CC != null)
                m.MainEntity.Cc = CC;
            if (Bcc != null)
                m.MainEntity.Bcc = Bcc;

            ///Nội dung
            MimeEntity text_entity = texts_enity.ChildEntities.Add();
            text_entity.ContentType = MediaType_enum.Text_plain;
            text_entity.ContentType_CharSet = "utf-8";
            text_entity.ContentTransferEncoding = ContentTransferEncoding_enum.QuotedPrintable;
            text_entity.DataText = Subject;

            MimeEntity rtfText_entity = texts_enity.ChildEntities.Add();
            rtfText_entity.ContentType = MediaType_enum.Text_html;
            rtfText_entity.ContentType_CharSet = "utf-8";
            rtfText_entity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
            rtfText_entity.DataText = Subject;
            return m;
        }
        public static bool SendSmartHost(string Title, string DisplayFrom, AddressList To,
            AddressList CC, AddressList Bcc, string Subject, string File)
        {
            DOServer mailServer = frmConfigServer._GetServer();
            if (mailServer == null) mailServer = GetDefaultSenderEmail();

            string SmartHost = mailServer.SMTP;
            Int32 Port = (HelpNumber.ParseInt32(mailServer.SMTP_PORT) < 0 ?
                                25 : HelpNumber.ParseInt32(mailServer.SMTP_PORT));
            string HostName = "";//Tham số này chưa hiểu
            string EmailFrom = mailServer.EMAIL_USERNAME;
            string Pwd = mailServer.PASS;
            //?? Không dùng thông tin có hỗ trợ SSL.

            if (DisplayFrom == null)
                DisplayFrom = mailServer.EMAIL;

            bool bFlag = false;
            try
            {
                LumiSoft.Net.Mime.Mime message = CreateMessage(Title, DisplayFrom, EmailFrom, To, CC, Bcc, Subject, File);
                SmtpClientEx.QuickSendSmartHost(SmartHost, Port, HostName, message);
                bFlag = true;
            }
            catch (Exception ex)
            {
                HelpSysLog.AddException(ex, "Gửi mail bằng LumiSoft.");
                bFlag = false;
            }
            return bFlag;
        }
        #endregion

        /// <summary>
        /// CHAUTV : Gửi mail PHUOCNT Cần phải làm rõ là lấy từ nguồn nào chứ không thể dùng nguồn cá nhân
        /// nếu config thì config ở đâu.
        /// </summary>
        public static bool SendEmails(string sTitle, Dictionary<string, string> dToAddress,
            string sToTitle, string sSubject, string sBody, Dictionary<string, string> dCC,
            string sFile)
        {
            try
            {
                DOServer serverMail = frmConfigServer._GetServer();
                if (serverMail == null) serverMail = GetDefaultSenderEmail();
                MailAddress from = new MailAddress(serverMail.EMAIL, sToTitle);
                MailMessage message = new MailMessage();
                message.From = from;
                foreach (KeyValuePair<string, string> to in dToAddress)
                {
                    message.To.Add(new MailAddress(to.Key, to.Value));
                }
                foreach (KeyValuePair<string, string> cc in dCC)
                {
                    message.CC.Add(new MailAddress(cc.Key, cc.Value));
                }
                message.Subject = sSubject;
                message.Body = sBody;
                if (sFile != string.Empty)
                    message.Attachments.Add(new Attachment(sFile));
                SmtpClient client = new SmtpClient(serverMail.SMTP, HelpNumber.ParseInt32(serverMail.SMTP_PORT));
                //NetworkCredential credential = new NetworkCredential(serverMail.EMAIL.Substring(0, serverMail.EMAIL.IndexOf('@')), serverMail.PASS);
                NetworkCredential credential = new NetworkCredential(serverMail.EMAIL_USERNAME, serverMail.PASS);
                if (serverMail.PRIVATE_MAIL_SSLSMTP_BIT == "Y")
                    client.EnableSsl = true;
                else
                    client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                client.Credentials = credential;
                client.Send(message);
                
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return false;
            }
        }
    }
}
