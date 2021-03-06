using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp đặt thông tin vào status bar
    ///    FrameworkParams.statusBar.Function
    /// Được chuyển từ PLStatusBar qua.
    /// </summary>    
    public class FWStatusBar
    {
        public BarStaticItem panelUser;
        public BarStaticItem panelDate;
        public BarStaticItem panelState;

        public BarManager barManager;           //For Form thường
        public Bar statusBar;

        public RibbonStatusBar rStatusBar;      //For Form Ribbon

        public FWStatusBar(BarManager barManager)
        {
            this.barManager = barManager;
            ((System.ComponentModel.ISupportInitialize)(barManager)).BeginInit();
            this.statusBar = new Bar();            
            barManager.Bars.Add(this.statusBar);
            barManager.StatusBar = this.statusBar;

            this.statusBar.BarName = "Status Bar";
            this.statusBar.DockCol = 0;
            this.statusBar.DockRow = 0;
            this.statusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.statusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;            
            this.statusBar.OptionsBar.AllowQuickCustomization = false;
            this.statusBar.OptionsBar.DrawDragBorder = false;
            this.statusBar.OptionsBar.DrawSizeGrip = true;            
            this.statusBar.OptionsBar.UseWholeRow = true;
            this.statusBar.Text = "Status Bar";

            this.panelUser = new BarStaticItem();
            this.panelUser.Caption = "";
            this.panelUser.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;            
            this.panelUser.TextAlignment = System.Drawing.StringAlignment.Near;
            this.panelUser.Width = 0;

            this.panelDate = new BarStaticItem();
            this.panelDate.Caption = "";
            this.panelDate.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.panelDate.TextAlignment = System.Drawing.StringAlignment.Near;
            this.panelDate.Width = 0;

            this.panelState = new BarStaticItem();
            this.panelState.Caption = "";
            this.panelState.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.panelState.TextAlignment = System.Drawing.StringAlignment.Near;
            this.panelState.Width = 0;

            this.statusBar.AddItem(this.panelUser);
            this.statusBar.AddItem(this.panelDate);
            this.statusBar.AddItem(this.panelState);            
            ((System.ComponentModel.ISupportInitialize)(barManager)).EndInit();
        }

        public FWStatusBar(RibbonForm form)
        {
            //((System.ComponentModel.ISupportInitialize)(form)).BeginInit();
            this.rStatusBar = new RibbonStatusBar();
            form.Controls.Add(rStatusBar);
            rStatusBar.Ribbon = form.Ribbon;
            form.Ribbon.StatusBar = rStatusBar;
            rStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            rStatusBar.ShowSizeGrip = true;

            this.panelUser = new BarStaticItem();
            this.panelUser.Caption = "";
            this.panelUser.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.panelUser.TextAlignment = System.Drawing.StringAlignment.Near;
            this.panelUser.Width = 0;
            
            this.panelDate = new BarStaticItem();
            this.panelDate.Caption = "";
            this.panelDate.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.panelDate.TextAlignment = System.Drawing.StringAlignment.Near;
            this.panelDate.Width = 0;

            this.panelState = new BarStaticItem();
            this.panelState.Caption = "";
            this.panelState.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.panelState.TextAlignment = System.Drawing.StringAlignment.Near;
            this.panelState.Width = 0;

            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            barButtonItem1.Appearance.ForeColor = System.Drawing.Color.Blue;
            barButtonItem1.Appearance.Options.UseFont = true;
            barButtonItem1.Appearance.Options.UseForeColor = true;
            barButtonItem1.Caption = "www.protocolvn.com";
            barButtonItem1.Width = 0;
            barButtonItem1.ItemClick += new ItemClickEventHandler(barButtonItem1_ItemClick);
            this.rStatusBar.ItemLinks.Add(this.panelUser);
            this.rStatusBar.ItemLinks.Add(this.panelDate, true);
            this.rStatusBar.ItemLinks.Add(this.panelState, true);
            this.rStatusBar.ItemLinks.Add(barButtonItem1, true);
            //((System.ComponentModel.ISupportInitialize)(form)).EndInit();
        }

        void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.protocolvn.com");                
            } catch { }
        }
        /// <summary>
        /// Đặt dữ liệu vào phần trình bày user đăng nhập
        /// </summary>
        public void ShowUser(string s)
        {
            if (barManager != null)
            {
                ((System.ComponentModel.ISupportInitialize)(barManager)).BeginInit();
                this.panelUser.Caption = "Người đang đăng nhập: " + s ;
                ((System.ComponentModel.ISupportInitialize)(barManager)).EndInit();
            }
            else
            {
                //((System.ComponentModel.ISupportInitialize)(rStatusBar.Parent)).BeginInit();
                this.panelUser.Caption = "Người đang đăng nhập: " + s;
                //((System.ComponentModel.ISupportInitialize)(rStatusBar.Parent)).EndInit();
            }
        }
        
        /// <summary>
        /// Đặt dữ liệu vào phần ngày 
        /// </summary>
        public void ShowDate(string s)
        {
            if (barManager != null)
            {
                ((System.ComponentModel.ISupportInitialize)(barManager)).BeginInit();
                this.panelDate.Caption = "Đăng nhập lúc: " + s;
                ((System.ComponentModel.ISupportInitialize)(barManager)).EndInit();
            }
            else
            {
                //((System.ComponentModel.ISupportInitialize)(rStatusBar.Parent)).BeginInit();
                this.panelDate.Caption = "Đăng nhập lúc: " + s;
                //((System.ComponentModel.ISupportInitialize)(rStatusBar.Parent)).EndInit();
            }
        }
        
        /// <summary>
        /// Đặt dữ liệu vào phần trạng thái hệ thống
        /// </summary>
        public void ShowState(string s)
        {
            if (barManager != null)
            {
                ((System.ComponentModel.ISupportInitialize)(barManager)).BeginInit();
                this.panelState.Caption = s;
                ((System.ComponentModel.ISupportInitialize)(barManager)).EndInit();
            }
            else
            {
                //((System.ComponentModel.ISupportInitialize)(rStatusBar.Parent)).BeginInit();
                this.panelState.Caption = s;
                //((System.ComponentModel.ISupportInitialize)(rStatusBar.Parent)).EndInit();
            }
        }
    }
}
