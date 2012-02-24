using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
//using DevExpres.Tutor;
using DevExpress.XtraEditors;
using ProtocolVN.Plugin.VietInput;
using ProtocolVN.Framework.Win.Properties;
using System.IO;
using System.Drawing;

namespace ProtocolVN.Framework.Win
{
    public class HelpApplication
    {
        public static String getVersion()
        {
            int version = LiveUpdateHelper.getLocalhostVersion();
            string major = "" + (version / 10);
            string minor = "" + (version % 10);
            return major + "." + minor;
        }

        public static String getExecutePath()
        {
            return RadParams.RUNTIME_PATH;
        }

        public static String getExecuteFileName()
        {
            return Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf('\\') + 1);
        }

        /// <summary>Tiêu đề cho từng màn hình thường là Tên sản phẩm - Title
        /// </summary>
        public static String getTitleForm(String title, params object[] input)
        {
            if (title != String.Empty)
            {
                return FrameworkParams.ProductName + " :: " + title;
            }
            return title;
        }

        public static String getProductName()
        {
            return FrameworkParams.ProductName;
        }

        #region ExitApplication
        private static LOCKBOOL ExitFlag = new LOCKBOOL(false);

        public static void ExitApplication(ProtocolVN.Framework.Win.FrameworkParams.EXIT_STATUS status)
        {
            if (FrameworkParams.MainForm != null && FrameworkParams.MainForm.IsDisposed == false)
            {
                if (FrameworkParams.MainForm.MdiChildren.Length > 0)
                {
                    for (int i = 0; i < FrameworkParams.MainForm.MdiChildren.Length; )
                    {
                        FrameworkParams.MainForm.MdiChildren[i].Dispose();
                        //FrameworkParams.MainForm.MdiChildren[i].Close();
                    }
                }

                try
                {
                    FrameworkParams.MainForm.Hide();
                    FrameworkParams.MainForm.Dispose();
                }
                catch { }
            }

            if (status == ProtocolVN.Framework.Win.FrameworkParams.EXIT_STATUS.NORMAL_THANKS)
            {
                //                if (IsThanksMsg)
                //                {
                //                    PLMessageBoxWin box = new PLMessageBoxWin(@"
                //                        Cám ơn bạn đã sử dụng sản phẩm của công ty ProtocolVN.\n
                //                        Khi gặp sự cố sử dụng sản phẩm xin vui lòng liên hệ với ProtocolVN");
                //                }
            }
            else if (status == ProtocolVN.Framework.Win.FrameworkParams.EXIT_STATUS.NORMAL_NO_THANKS)
            {

            }
            else if (status == ProtocolVN.Framework.Win.FrameworkParams.EXIT_STATUS.ERROR)
            {
                PLMessageBox box = PLMessageBox.GetSystemErrorMessage(@"Cám ơn bạn đã sử dụng sản phẩm của công ty ProtocolVN. Alt-F10 Xem thông thêm.");
                PLKey key = new PLKey(box);
                key.Add(Keys.Alt | Keys.F10, delegate()
                {
                    PLDebug.ShowExceptionInfo();
                });

                box.ShowDialog();
            }

            ExitApplication();
        }

        public static void ExitApplication()//Hàm để thoát khởi ứng dụng
        {
            if (FrameworkParams.MainForm != null && FrameworkParams.MainForm.IsDisposed == false)
            {
                FrameworkParams.MainForm.Hide();
                FrameworkParams.MainForm.Dispose();
            }
            try
            {
                lock (ExitFlag)
                {
                    if (ExitFlag.Value == false)
                    {
                        //Close all Output
                        SystemTrayOut.Dispose();
                        RibbonStatusOut.Dispose();
                        //Stop Stickies
                        ProtocolVN.Plugin.NoteBook.StickiesMethodExec.StopStickies();
                        //Class Microsoft Word
                        try
                        {
                            if (PLMicrosoftWord.wd != null)
                            {
                                object dummy = null;
                                object dummy2 = (object)false;
                                PLMicrosoftWord.wd.Quit(ref dummy2, ref dummy, ref dummy);
                            }
                        }
                        catch { }
                        //Cập nhật thông tin của Licence
                        if (FrameworkParams.Lic != null)
                            ((DevExpres.Tutor.ILicence)FrameworkParams.Lic).updateReleaseLicence("NOOP");
                        FrameworkParams.Custom.exitApplication();
                        ExitFlag.Value = true;
                    }
                }
            }
            catch (Exception ex)
            {
                PLException pl = new PLException(ex, "", "", "", "Lỗi thoát khỏi ứng dụng");
                PLException.AddException(pl);

                lock (ExitFlag)
                {
                    FrameworkParams.Custom.exitApplication();
                    ExitFlag.Value = true;
                }
            }
        }
        #endregion

        internal static void enableVietKey(XtraForm frm)
        {
            if(FrameworkParams.IsEmbededVietkey){
                if (!HelpExe.IsExeRunning("UniKeyNT") &&
                    !HelpExe.IsExeRunning("VietKey") &&
                    !HelpExe.IsExeRunning("vknt") )
                {
                    new PLVietKey(frm);
                    PLVietKey.KieuGo = VietKeyHandler.InputType.Auto;
                }            
            }
        }

        internal static void initAppParams()
        {
            RadParams.HELP_FILE = null;

            //Chứa các thông tin câu hình của sản phẩm
            if (HelpFile.NeedFolder("conf") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'conf'.");
                Application.Exit();
            }
            //Chứa các layout mặc định của hệ thông
            if (HelpFile.NeedFolder("conf/layout") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'conf/layout'.");
                Application.Exit();
            }
            //Chứa các dữ liệu tạm thời
            if (HelpFile.NeedFolder("temp") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'temp'.");
                Application.Exit();
            }
            //Chứa các plugins
            if (HelpFile.NeedFolder("plugins") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'plugin'.");
                Application.Exit();
            }
            //Chưa các EXE dành cho việc cập nhật chuơng trình
            if (HelpFile.NeedFolder("update") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'update'.");
                Application.Exit();
            }

            //Chưa các EXE dành cho việc cập nhật chuơng trình
            if (HelpFile.NeedFolder("update/download") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'update/download'.");
                Application.Exit();
            }
            //Chứa layout của người dùng
            if (HelpFile.NeedFolder("layout") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'layout'.");
                Application.Exit();
            }
            //Chứa các tập tin template mà ứng dụng cần
            if (HelpFile.NeedFolder("template") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'template'.");
                Application.Exit();
            }
            //Chứa các định nghĩa warning
            if (HelpFile.NeedFolder("warningsystem") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'warningsystem'.");
                Application.Exit();
            }
            //Chứa dữ liệu log error
            if (HelpFile.NeedFolder("logs") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'logs'.");
                Application.Exit();
            }
            //Chứa dữ liệu DB và backup DB
            if (HelpFile.NeedFolder("data") == false)
            {
                PLMessageBox.ShowErrorMessage("Không cho phép tạo thư mục 'data'.");
                Application.Exit();
            }

            //Đăng ký việt hóa
            DevExpressVN.RegisterVi();

            //Đăng ký sử dụng nhật ký hệ thống
            HelpSysLog.RegisterConfig();

            //Khởi tạo các giá trị của dự án
            FrameworkParams.ApplicationIcon = global::ProtocolVN.Framework.Win.Properties.Resources.App;
            RadParams.ApplicationIcon = FrameworkParams.ApplicationIcon;
            FrameworkParams.LICENCE_FILE = RadParams.RUNTIME_PATH + @"\pl-licence.licx";
            FrameworkParams.TEMP_FOLDER = RadParams.RUNTIME_PATH + @"\temp";
            FrameworkParams.CONF_FOLDER = RadParams.RUNTIME_PATH + @"\conf";
            FrameworkParams.LAYOUT_FOLDER = RadParams.RUNTIME_PATH + @"\layout";
            FrameworkParams.TEMPLETE_FOLDER = RadParams.RUNTIME_PATH + @"\template";

            FrameworkParams.desktopForm = typeof(frmFWPromotionDesktop).FullName;//FWFormName.frmIntro;
            FrameworkParams.UpdateURL = "www.protocolvn.com";
            FrameworkParams.CustomerName = "Công ty phần mềm P R O T O C O L";
            FrameworkParams.ProductName = "PL-PRODUCTS";
            FrameworkParams.IsCheckNewVersion = false;

            //FrameworkParams.headerStartTitleGridEndFooter = new ProtocolHeaderStartTitleGridEndFooter();
            //FrameworkParams.headerStartTitleGridEndFooter = new CompanyInfoHeaderStartTitleGridEndFooter();
            //Nap report len san de ko phai cho khi nap report lan dau
            //Chua co giai phap
        }

        public static void initRunFast()
        {
            FrameworkParams.IsCheckNewVersion = false;
            FrameworkParams.IsHotKey = false;
            FrameworkParams.desktopForm = "";
            FrameworkParams.isLog = null;
            FrameworkParams.IsTrial = true;
            FrameworkParams.UsingSkin = false;
            //FrameworkParams.licenceString = "AAEAAAD/////AQAAAAAAAAAMAgAAAEVEZXZFeHByZXMuQ29yZSwgVmVyc2lvbj0xLjIuMy40LCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGwFAQAAACFEZXZFeHByZXMuVHV0b3IuRGF0ZUxpY2VuY2VLZXlFeHQEAAAAC2NyZWF0ZWREYXRlCmV4cGlyZURhdGUJZXJyb3JNc2dzEklMaWNlbmNlK2Vycm9yTXNncwAAAQENDQIAAACwiFTrYrPMiADAqrkSL80ICgoL";
        }

        #region Đóng gói dạng phát triển giúp cho DEBUG - TEST dễ phát hiện lỗi.
        public static void initDevelop()
        {            
            FrameworkParams.isSupportDeveloper = true;
            FrameworkParams.IsBeforeLogin = false;
        }

        public static void initDevelopA()
        {
            FrameworkParams.IsCheckNewVersion = false;
            FrameworkParams.IsUpdateVersionAtLocalServer = false;
            FrameworkParams.IsTrial = true;
            FrameworkParams.desktopForm = "";
            if (__PL__.getFree())
            {
                __PL__.kickFree();
            }
            else
            {
                __PL__.kickCommerce();
            }
        }
        #endregion

        #region Đóng gói miễn phí -> chuyển đên OK.
        public static void initPackageRelease()
        {
            FrameworkParams.isSupportDeveloper = false;
            FrameworkParams.IsBeforeLogin = true;            
        }

        public static void initPackageReleaseA()
        {
            if (FrameworkParams.LoginCompany <= 0)
                FrameworkParams.LoginCompany = 1;
            FrameworkParams.IsCheckNewVersion = false;
            FrameworkParams.IsUpdateVersionAtLocalServer = false;
            FrameworkParams.IsTrial = false;
            if (__PL__.getFree())
            {
                __PL__.kickFree();
            }
            else
            {
                __PL__.kickCommerce();
            }
        }
        #endregion  
        
        public static void initCustom(bool isSupportDeveloper,bool isBeforeLogin)
        {
            FrameworkParams.isSupportDeveloper = isSupportDeveloper;
            FrameworkParams.IsBeforeLogin = isBeforeLogin;
        }
        
    }


    public class __PL__{
        private static bool IsFree = true;//True: đang sử dụng FREE; False: đang sử dụng có bản quyền.
        private static bool isUseLicx = true;
        #region FREE LICENCE
        public static object kickFree()
        {
            FrameworkParams.headerLetter = new ImageHeaderStartTitleGridEndFooter(Resources.header);
            FrameworkParams.isPermision = null;
            return null;
        }
        #endregion

        #region COMMERCE LICENCE
        public static object kickCommerce()
        {
            CompanyInfo company = new CompanyInfo();
            company.load();
            try
            {
                if (company.headerletter != null)
                {
                    MemoryStream stream = new MemoryStream(company.headerletter);
                    Bitmap image = new Bitmap(stream);
                    FrameworkParams.headerLetter = new ImageHeaderStartTitleGridEndFooter(image);
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }    
        
            return null;
        }
        #endregion

        public static void setFree()
        {
            //Kiểm tra nơi gọi phương thức này chỉ cho phép xuất phát từ LICX.
            IsFree = true;
        }

        public static void setLicx()
        {
            //Kiểm tra nơi gọi phương thức này chỉ cho phép xuất phát từ LICX.
            IsFree = false;
        }

        public static bool getFree()
        {
            return IsFree;
        }


        public static bool IsUseLicx
        {
            get { return isUseLicx; }
            set { isUseLicx = value; }
        }
    }
}