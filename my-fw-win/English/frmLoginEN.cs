using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Diagnostics;
using ProtocolVN.Framework.Win.Properties;
using DevExpress.UserSkins;
namespace ProtocolVN.Framework.Win
{
    sealed public partial class frmLoginEN : XtraFormPublicPL
    {
        public static frmLoginEN frmLoginInstance;
        private bool isDirect = false;
        private User user;

        public frmLoginEN()
        {
            InitializeComponent();
            //...
            this.SanPham.Text = FrameworkParams.ProductName;
            this.PhienBan.Text += HelpApplication.getVersion();

            this.Text = FrameworkParams.ProductName + " :: " + this.Text;
        }

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            frmRibbonMain.isForceExit = true;
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            this.Hide();     
            frmFWConfigDBSecurity frmCon = new frmFWConfigDBSecurity();
            frmCon.ShowDialog();
        }

        public void LoginAction()
        {
            if(isDirect == false)
            {
                SplashScreen.Instance.Font = new System.Drawing.Font("Verdana", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SplashScreen.SetBackgroundImage(Resources.splashbg);
                SplashScreen.SetTitleString("");
                SplashScreen.BeginDisplay();
            }

            bool check = false;
            string remPass = "N";
            string remAuto = "N";
            try
            {
                RadParams.isEMB = null;
                SplashScreen.SetCommentaryString("..Đang kiểm tra kết nối.");
                if (DABase.checkDBConnection())
                {
                    user.username = txtUsername.Text.Trim();
                    user.password = txtPassword.Text.Trim();
                    
                    if (FrameworkParams.UsingLDAP && !user.username.Equals("admin"))
                        check = LDAPUser.Login(user.username, user.password);
                    else
                        check = user.login();

                    if (check)
                    {
                        this.Hide();
                        //InternetConn.Monitor(); 
                        if (chkRememberPwd.Checked) remPass = "Y";
                        else
                        {
                            user.password = "";
                            remPass = "N";
                        }
                        if (chkAutoLogin.Checked) remAuto = "Y";
                        SplashScreen.SetCommentaryString("..Đang nạp thông tin phân quyền.");
                        user.updateCookies(remPass, remAuto);                        
                        user.loadByUserName();
                        this.UseWaitCursor = false;
                        FrameworkParams.currentUser = user;
                        FrameworkParams.Custom.InitResAfterLogin();
                        XtraForm main = new frmRibbonMain();
                        FrameworkParams.MainForm = main;                        
                        SplashScreen.SetCommentaryString("..Đang nạp menu chương trình.");
                        ((frmRibbonMain)main).LoadBarManager(user.username);                        
                        //new AppWarning(((frmRibbonMain) main));
                        while (this.UseWaitCursor == true);                        
                        //RadParams.isEMB = false;
                        SplashScreen.SetCommentaryString("..Đang nạp màn hình nền.");
                        ((frmRibbonMain)main).LoadDesktopForm();
                        if (RadParams.isEMB == false)//Khi DLL không tồn tại                        
                        {
                            this.Hide(); this.Dispose();
                            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);
                            return;
                        }

                        if (FrameworkParams.IsCheckNewVersion)
                        {
                            LiveUpdateHelper.updateVersionFromLocalServer();
                        }

                        HelpUserLog.log("Đăng nhập hệ thống");
                    }
                    else
                    {
                        FWMsgBox.showInvalidUser();
                        FrameworkParams.isSkipLogin = false;
                    }
                }
                else
                {
                    FWMsgBox.showInvalidConnectServer(this);
                    FrameworkParams.isSkipLogin = false;
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                RadParams.isEMB = false;
                this.Hide(); this.Dispose();
                FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.ERROR);                
                FrameworkParams.isSkipLogin = false;
            }
            finally
            {
                if (isDirect == false)
                {
                    SplashScreen.EndDisplay();
                }
                isDirect = false;
            }            
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            LoginAction();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrameworkParams.ExitApplication(FrameworkParams.EXIT_STATUS.NORMAL_THANKS);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SplashScreen.EndDisplay();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SplashScreen.SetCommentaryString("..Đang nạp giao diện.");            
            this.btnConfig.Image = FWImageDic.CONFIG_IMAGE16;
            //this.btnExit.Image = FWImageDic.EXIT_IMAGE16;
            //this.btnLogin.Image = FWImageDic.LOGIN_IMAGE16;
            this.Icon = FrameworkParams.ApplicationIcon;
            components = new System.ComponentModel.Container();
            user = new User();
            user.loadCookies();
            txtUsername.EditValue = user.username;
            txtPassword.EditValue = user.password;
            if (user.savePass == "Y") chkRememberPwd.Checked = true;
            if (user.isAutoLogin == "Y") chkAutoLogin.Checked = true;
            if (frmLoginInstance == null)
            {
                if (chkAutoLogin.Checked) FrameworkParams.isSkipLogin = true;
                frmLoginInstance = this;
            }
            // init skin
            if(FrameworkParams.UsingSkin == true)
                FrameworkParams.currentSkin = new DevExpressSkin(this.components);
            
            FrameworkParams.option = new Option();
            FrameworkParams.option.load();

            Application.CurrentCulture = ApplyFormatAction.GetCultureInfo();            
            if(FrameworkParams.currentSkin!=null)
                FrameworkParams.currentSkin.SelectSkin(HelpNumber.ParseInt32(FrameworkParams.option.Skin));

            if (FrameworkParams.IsBeforeLogin == true)
            {
                FrameworkParams.MainForm = this;
                RadParams.isEMB = false;
                new DevExpres.Tutor.WinLic();
                if (FrameworkParams.MainForm.Visible == false) return;       
            }

            if (FrameworkParams.isSkipLogin==true)
            {
                SplashScreen.SetCommentaryString("..Đang xử lý đăng nhập tự động.");
                
                this.isDirect = true;
                this.LoginAction();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            SplashScreen.EndDisplay();
        }
    }
}