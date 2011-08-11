using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using DevExpress.XtraBars;

namespace ProtocolVN.Framework.Win
{
    public partial class frmWebBrowser : XtraFormPL, IPublicForm 
    {
        #region Local variable
        string Murl = "";        
        #endregion

        private DevExpress.XtraBars.Docking.DockVisibility _FavoritesStatus = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        public DevExpress.XtraBars.Docking.DockVisibility FavoritesStatus {
            get { return this._FavoritesStatus; }
            set { this._FavoritesStatus = value; }
        }

        public frmWebBrowser(string url)
        {
            //webBrowser1.AllowWebBrowserDrop = false;
            Murl = url;
            InitializeComponent();
            this.MinimizeBox = true;
            _Browser.StatusTextChanged += new EventHandler(webBrowser1_StatusTextChanged);
            _Browser.CanGoBackChanged += new EventHandler(webBrowser1_CanGoBackChanged);
            _Browser.CanGoForwardChanged += new EventHandler(webBrowser1_CanGoForwardChanged);
            barManager1.ForceLinkCreate();
            if(System.IO.File.Exists(linksName))
            {
                XmlDocument doc = new XmlDocument();
                try { doc.Load(linksName); }
                catch { }
                if(doc.DocumentElement != null && doc.DocumentElement.Name == "Items") {
                    LoadLinks(doc.DocumentElement.ChildNodes[0].ChildNodes);
                    LoadFavorites(doc.DocumentElement.ChildNodes[1].ChildNodes);
                }
            }
            if(System.IO.File.Exists(layoutName)) 
            {
                barManager1.RestoreFromXml(layoutName);
            }
            barManager1.GetController().Changed += new EventHandler(ChangedController);
            iFavorites.Down = dockPanel1.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible;
            ips_Init();
            InitSkins();
            this.Focus();
        }
        string layoutName = "layout.xml";
        string skinMask = "Skin: ";
        private bool skinProcessing = false;
        string currentAddress = "";
        int tc = 0;
        string linksName = "links.xml";

        #region Skins

        void InitSkins() 
        {       
        }
        void OnSkinClick(object sender, ItemClickEventArgs e)
        {           
        }

        private void ChangedController(object sender, EventArgs e) 
        {
            if(skinProcessing) return;
            string paintStyleName = barManager1.GetController().PaintStyleName;
            if("DefaultSkin".IndexOf(paintStyleName) >= 0)
                DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.DisableFormSkins();
            skinProcessing = true;
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            skinProcessing = false;
        }
        #endregion

        void LoadLinks(XmlNodeList list)
        {
            for(int i = 0; i < list.Count; i++) {
                if(list.Item(i).Name == "Link")
                    AddNewItem(list[i].InnerText);
            }
        }

        void LoadFavorites(XmlNodeList list)
        {
            for(int i = 0; i < list.Count; i++) {
                if(list.Item(i).Name == "Favorite")
                    AddFavoriteItem(list[i].InnerText, list[i].Attributes[0].Value, true);
            }
            ChangeFavorites(true);
        }

        private void AddNewItem(string s)
        {
            if(s != "") {
                bool isAdded = false;
                for(int i = 0; i < repositoryItemComboBox1.Items.Count; i++)
                    if(repositoryItemComboBox1.Items[i].ToString() == s) {
                        isAdded = true;
                        break;
                    }
                if(!isAdded)
                    repositoryItemComboBox1.Items.Add(s);
            }
        }

        private void GoToItem(string address)
        {
            if(address == null) return;
            if(currentAddress != address) {
                eAddress.EditValue = address;
                _Browser.Navigate(address);
            }
        }

        string Address {
            get {
                if(barManager1.ActiveEditor != null && barManager1.ActiveEditor.EditValue != null)
                    return barManager1.ActiveEditor.EditValue.ToString();
                return null;
            }
        }

        private void repositoryItemComboBox1_SelectedItemChanged(object sender, System.EventArgs e) 
        {
            if(barManager1.ActiveEditor != null)
                if(!((DevExpress.XtraEditors.ComboBoxEdit)barManager1.ActiveEditor).IsPopupOpen &&
                    ((DevExpress.XtraEditors.ComboBoxEdit)barManager1.ActiveEditor).SelectedIndex != -1)
                    GoToItem(Address);
        }

        private void repositoryItemComboBox1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            GoToItem(Address);
        }

        private void repositoryItemComboBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DevExpress.XtraEditors.ComboBoxEdit edit = sender as DevExpress.XtraEditors.ComboBoxEdit;
            if(e.KeyData == Keys.Escape) {
                e.Handled = true;
                edit.SelectAll();
            }
            if(e.KeyData == Keys.Enter) {
                barManager1.ActiveEditItemLink.PostEditor();
                edit.SelectAll();
                e.Handled = true;
                GoToItem(eAddress.EditValue.ToString());
            }
        }

        private void SaveXML() {
            XmlTextWriter tw = new XmlTextWriter(linksName, System.Text.Encoding.UTF8);
            tw.Formatting = Formatting.Indented;
            tw.WriteStartElement("Items");
            tw.WriteAttributeString("version", "1.0");
            tw.WriteAttributeString("application", Application.ProductName);

            tw.WriteStartElement("Links");
            for(int i = 0; i < repositoryItemComboBox1.Items.Count; i++)
                tw.WriteElementString("Link", repositoryItemComboBox1.Items[i].ToString());
            tw.WriteEndElement();

            tw.WriteStartElement("Favorites");
            for(int i = 0; i < barManager1.Items.Count; i++)
                if(barManager1.Items[i].Category == barManager1.Categories["Favorites"])
                    tw.WriteElementString("Favorite", barManager1.Items[i].Tag.ToString(), barManager1.Items[i].Caption);
            tw.WriteEndElement();

            tw.WriteEndElement();
            tw.Close();
            barManager1.SaveToXml(layoutName);
        }

        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveXML();
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            repositoryItemProgressBar1.Maximum = (int)(e.MaximumProgress + (e.MaximumProgress == repositoryItemProgressBar1.Minimum ? 1 : 0));
            eProgress.EditValue = e.CurrentProgress;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e) 
        {                      
            if (e.Url.ToString() != "about:blank")
            {

                string s = e.Url.AbsoluteUri;
                if (barManager1.ActiveEditor != null)
                    barManager1.ActiveEditItemLink.CloseEditor();
                if (CorrectAddress(s))
                {                   
                    currentAddress = s;
                    AddNewItem(s);
                }
            }            
        }        
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            eAddress.EditValue = _Browser.Url.ToString();
        }
        private void webBrowser1_StatusTextChanged(object sender, EventArgs e) 
        {
            iText.Caption = _Browser.StatusText;
        }
        private void webBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
            iForward.Enabled = _Browser.CanGoForward;
        }
        private void webBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            iBack.Enabled = _Browser.CanGoBack;
        }

        bool CorrectAddress(string name) {
            string[] names = new string[] { "javascript:" };
            foreach(string s in names)
                if(name.IndexOf(s) == 0) return false;
            return true;
        }

        private void iGo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoToItem(eAddress.EditValue.ToString());
        }

        private void iBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try {
                _Browser.GoBack();
            }
            catch { }
        }

        private void iForward_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try {
                _Browser.GoForward();
            }
            catch { }
        }

        private void iStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _Browser.Stop();
        }

        private void iRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _Browser.Refresh();
        }

        private void iHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            _Browser.GoHome();
        }

        private void iSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _Browser.GoSearch();
        }

        private void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.Utils.About.frmAbout dlg = new DevExpress.Utils.About.frmAbout("Web Browser Demo for the XtraBars by Developer Express Inc.");
            dlg.ShowDialog();
        }

        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "HTML Files|*.htm; *.html|" +
                "GIF Files|*.gif|" +
                "JPEG Files|*.jpg;*.jpeg|" +
                "XML Files|*.xml|" +
                "All Files |*.*";
            dlg.Title = "Open";
            if(dlg.ShowDialog() == DialogResult.OK)
                GoToItem(dlg.FileName);
        }

        private void iPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try {
                System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                //todo create pd     
                PrintDialog dlg = new PrintDialog();
                dlg.Document = pd;
                if(dlg.ShowDialog() == DialogResult.OK) 
                {
                    pd.Print();
                }
            }
            catch { }
        }

        private void OpenNotepad() {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            string s = System.Environment.SystemDirectory + "\\Notepad.exe";
            if(System.IO.File.Exists(s)) {
                p.StartInfo.FileName = s;
                p.Start();
            }
        }

        private void iEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            OpenNotepad();
        }

        private void iFavorites_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            if(iFavorites.Down)
                dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            else {
                if(dockPanel1.Dock == DevExpress.XtraBars.Docking.DockingStyle.Float)
                    dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                else
                    dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            }
        }

        private void AddFavoriteItem(string locationName, string locationURL)
        {
            AddFavoriteItem(locationName, locationURL, false);
        }
        private void AddFavoriteItem(string locationName, string locationURL, bool init)
        {
            BarItem item = new BarButtonItem();
            item.ItemClick += new ItemClickEventHandler(Favorite_Click);
            item.Category = barManager1.Categories["Favorites"];
            item.Caption = locationName;
            item.Tag = locationURL;
            barManager1.Items.Add(item);
            if(!init) ChangeFavorites();
        }

        private void AddFavorite() {
            frmAddFavorites f = new frmAddFavorites(_Browser.DocumentTitle, _Browser.Url.AbsoluteUri, imageList1.Images[2]);
            if(f.ShowDialog() == DialogResult.OK) {
                bool add = true;
                for(int i = 0; i < barManager1.Items.Count; i++) {
                    BarItem item = barManager1.Items[i];
                    if(item.Category == barManager1.Categories["Favorites"] && item.Caption == f.LocationName) 
                    {
                        if(DevExpress.XtraEditors.XtraMessageBox.Show("The name specified for the shortcut already exists in your Favorites list. Would you like to overwrite it?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            item.Tag = f.LocationURL;
                        add = false;
                        break;
                    }
                }
                if(add)
                    AddFavoriteItem(f.LocationName, f.LocationURL);
            }
        }

        private void iAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AddFavorite();
        }

        private void Favorite_Click(object sender, ItemClickEventArgs e) {
            GoToItem(e.Item.Tag.ToString());
        }

        private void ChangeFavorites() { ChangeFavorites(false); }
        private void ChangeFavorites(bool init) {
            siFavorites.ClearLinks();
            ctrlFavorites1.DeleteItems();

            siFavorites.AddItem(iAdd);
            for(int i = 0; i < barManager1.Items.Count; i++) {
                BarItem item = barManager1.Items[i];
                if(item.Category == barManager1.Categories["Favorites"]) {
                    siFavorites.AddItem(item);
                    ctrlFavorites1.AddItem(item, init);
                }
            }
            if(siFavorites.ItemLinks.Count > 1) siFavorites.ItemLinks[1].BeginGroup = true;
        }

        private void ctrlFavorites1_AddNewFavorite(object sender, System.EventArgs e) 
        {
            AddFavorite();
        }

        private BarItem ItemByName(string name) 
        {
            for(int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if(item.Category == barManager1.Categories["Favorites"] && item.Caption == name)
                    return item;
            }
            return null;
        }

        private void ctrlFavorites1_DeleteFavorite(object sender, System.EventArgs e)
        {
            string s = sender.ToString();
            if(DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure you want to remove shortcut?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                BarItem item = ItemByName(s);
                if(item != null) {
                    barManager1.Items.Remove(item);
                    ChangeFavorites();
                }
            }
        }

        private void ctrlFavorites1_EditFavorite(object sender, System.EventArgs e) 
        {
            string s = sender.ToString();
            BarItem item = ItemByName(s);
            if(item != null) {
                frmAddFavorites f = new frmAddFavorites(item.Caption, item.Tag.ToString(), imageList1.Images[1], false);
                if(f.ShowDialog() == DialogResult.OK) 
                {
                    item.Caption = f.LocationName;
                    item.Tag = f.LocationURL;
                    ChangeFavorites();
                }
            }
        }

        private void ctrlFavorites1_OpenFavorite(object sender, System.EventArgs e)
        {
            BarItem item = ItemByName(sender.ToString());
            if(item != null) {
                Favorite_Click(item, new ItemClickEventArgs(item, null));
            }
        }

        private void dockPanel1_VisibilityChanged(object sender, DevExpress.XtraBars.Docking.VisibilityChangedEventArgs e) {
            iFavorites.Down = e.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        private void ips_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barManager1.GetController().PaintStyleName = e.Item.Description;
            InitPaintStyle(e.Item);
            barManager1.GetController().ResetStyleDefaults();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle();
        }

        private void InitPaintStyle(BarItem item) 
        {
            //if(item == null) return;
            //iPaintStyle.ImageIndex = item.ImageIndex;
            //iPaintStyle.Caption = item.Caption;
            //iPaintStyle.Hint = item.Description;
            //ctrlFavorites1.barManager1.GetController().PaintStyleName = barManager1.GetController().PaintStyleName;

        }

        private void ips_Init() 
        {
            BarItem item = null;
            for(int i = 0; i < barManager1.Items.Count; i++)
                if(barManager1.Items[i].Description == barManager1.GetController().PaintStyleName)
                    item = barManager1.Items[i];
            InitPaintStyle(item);
        }


        private void timer1_Tick(object sender, System.EventArgs e) 
        {
            //if(barManager1.HighlightedLink != null )
            //{
            //    timer1.Stop();
            //    return;
            //}
            //barManager1.SelectLink(barManager1.HighlightedLink == null ? bar5.ItemLinks[0] : null);
            if(tc++ > 10) {
                timer1.Stop();
                barManager1.SelectLink(null);
            }
        }

        void InitHomePage()
        {
            GoToItem(Murl);
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            BeginInvoke(new MethodInvoker(InitHomePage));
            this.dockPanel1.Visibility = this._FavoritesStatus;
        }

        
              
    }
}
