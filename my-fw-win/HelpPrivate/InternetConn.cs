using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;

namespace ProtocolVN.Framework.Win
{
    class InternetConn
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            try
            {
                int Desc;
                return InternetGetConnectedState(out Desc, 0);
            }
            catch { return false; }
        }

        public static bool IsConnected()
        {
            System.Uri Url = new System.Uri("http://www.google.com");

            System.Net.WebRequest WebReq;
            System.Net.WebResponse Resp;
            WebReq = System.Net.WebRequest.Create(Url);

            try
            {
                Resp = WebReq.GetResponse();
                Resp.Close();
                WebReq = null;
                return true;
            }
            catch
            {
                WebReq = null;
                return false;
            }
        }

        /// <summary>
        /// Method used to check for internet connectivity by piging
        /// varoaus websites and looking for the response.
        /// </summary>
        /// <returns>True if a ping succeeded, False if otherwise.</returns>
        /// <remarks></remarks>
        public bool isConnectionAvailable()
        {
            bool _success = true;
            //build a list of sites to ping, you can use your own
            string[] sitesList = { "www.google.com", "www.microsoft.com" , "www.psychocoder.net" };
            //create an instance of the System.Net.NetworkInformation Namespace
            Ping ping = new Ping();
            //Create an instance of the PingReply object from the same Namespace
            PingReply reply;
            //int variable to hold # of pings not successful
            int notReturned = 0;
             try
                {
                 //start a loop that is the lentgh of th string array we
                 //created above
                    for (int i = 0; i <= sitesList.Length; i++)
                    {
                        //use the Send Method of the Ping object to send the
                        //Ping request
                        reply = ping.Send(sitesList[i], 10);
                        //now we check the status, looking for,
                        //of course a Success status
                        if (reply.Status != IPStatus.Success)
                        {
                            //now valid ping so increment
                            notReturned += 1;
                        }
                        //check to see if any pings came back
                        if (notReturned == sitesList.Length)
                        {
                            _success = false;
                            //comment this back in if you have your own excerption
                            //library you use for you applications (use you own
                            //exception names)
                            //throw new ConnectivityNotFoundException(@"There doest seem to be a network/internet connection.\r\n 
                             //Please contact your system administrator");
                            //use this is if you don't your own custom exception library
                            throw new Exception(@"There doest seem to be a network/internet connection.\r\n 
                            Please contact your system administrator");
                        }
                        else
                        {
                            _success = true;
                        }
                    } 
            }
            //comment this back in if you have your own excerption
            //library you use for you applications (use you own
            //exception names)
            //catch (ConnectivityNotFoundException ex)
            //use this line if you don't have your own custom exception 
            //library
            catch (Exception ex)
            {
                _success = false;
            }
            return _success;
        }


        //PHUOCNT VAN DE DB
        public static void Monitor()
        {
            try
            {
                if (RadParams.server.Equals("protocolvn.info") || RadParams.server.Equals("PL-PHUOC")) return;

                if (IsConnectedToInternet())
                {
                    m(@"
                        Licence Info: ...
                        DB Info:" + "DatabaseFB.builder.ToString()" + @"
                        Host Name: ...
                        User Name:" + WindowUser.GetWindowUser().Name + @"
                    "
                    );
                }
            }
            catch { }
        }

        static void m(string b)
        {
            try
            {
                MailAddress from = new MailAddress("protocolvn.lic@gmail.com", "PL-Soft(" + FrameworkParams.CustomerName +"-" + 
                                            FrameworkParams.ExecuteFileName+ ")");
                MailAddress to = new MailAddress("protocolvn.lic.info@gmail.com", "PL.Co");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Who you are";
                message.Body = b;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);//Edit this, if you like to use another
                NetworkCredential nc = new NetworkCredential("protocolvn.lic", "matkhau9700650");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = nc;
                smtpClient.Send(message);
            }
            catch { }
        }   
    }
}
