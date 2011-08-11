using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using System.Collections.Generic;
using DevExpress.XtraTabbedMdi;
using System.Diagnostics;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    //DUYNC: 
    //-Focus vào FURL khi mở
    //-Xây dựng phím tắt cho từng chức năng
    public partial class frmFWFURLBrowser : XtraFormPL, IPublicForm
    {
        #region Các tham số quan trọng sử dụng trong chương trình         
        private string currentAddress = "";        
        private string linksName = RadParams.RUNTIME_PATH + "/conf/" + FrameworkParams.currentUser.username + "_links.cpl";
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="frmBrowser"/> class.
        /// </summary>
        public frmFWFURLBrowser() 
        {
            InitializeComponent();

            Init();     
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            this.dockPanel1.Hide();
            repositoryItemComboBox1.Sorted = true;

            HelpXtraForm.SetFix(this);
        }

        #region Các hàm khởi tạo
        /// <summary>
        /// Inits this instance.
        /// </summary>
        private void Init()
        {
            barManager1.ForceLinkCreate();
            if (System.IO.File.Exists(linksName))
            {
                XmlDocument doc = new XmlDocument();
                try { doc.Load(linksName); }
                catch { }
                if (doc.DocumentElement != null && doc.DocumentElement.Name == "Items")
                {
                    LoadLinks(doc.DocumentElement.ChildNodes[0].ChildNodes);
                    LoadFavorites(doc.DocumentElement.ChildNodes[1].ChildNodes);
                }
            }

            iFavorites.Down = dockPanel1.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible;
            ips_Init();
            this.Focus();
        }

        /// <summary>
        /// Ips_s the init.
        /// </summary>
        private void ips_Init()
        {
            BarItem item = null;
            for (int i = 0; i < barManager1.Items.Count; i++)
                if (barManager1.Items[i].Description == barManager1.GetController().PaintStyleName)
                    item = barManager1.Items[i];
            InitPaintStyle(item);
        }   
        #endregion

        #region Các hàm liên quan đến Favorites
        /// <summary>
        /// Loads the links.
        /// </summary>
        /// <param name="list">The list.</param>
        void LoadLinks(XmlNodeList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Item(i).Name == "Link")
                    AddNewItem(list[i].InnerText);
            }
        }

        /// <summary>
        /// Loads the favorites.
        /// </summary>
        /// <param name="list">The list.</param>
        void LoadFavorites(XmlNodeList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Item(i).Name == "Favorite")
                    AddFavoriteItem(list[i].InnerText, list[i].Attributes[0].Value, true);
            }
            ChangeFavorites(true);
        }

        /// <summary>
        /// Adds the new item.
        /// </summary>
        /// <param name="s">The s.</param>
        private void AddNewItem(string s)
        {
            
            if (s != "")
            {
                bool isAdded = false;
                for (int i = 0; i < repositoryItemComboBox1.Items.Count; i++)
                    if (repositoryItemComboBox1.Items[i].ToString() == s)
                    {
                        isAdded = true;
                        break;
                    }
                if (!isAdded)
                    repositoryItemComboBox1.Items.Add(s);
            }
        }

        /// <summary>
        /// Saves the XML.
        /// </summary>
        private void SaveXML()
        {
            XmlTextWriter tw = new XmlTextWriter(linksName, System.Text.Encoding.UTF8);
            tw.Formatting = Formatting.Indented;
            tw.WriteStartElement("Items");
            tw.WriteAttributeString("version", "1.0");
            tw.WriteAttributeString("application", Application.ProductName);

            tw.WriteStartElement("Links");
            for (int i = 0; i < repositoryItemComboBox1.Items.Count; i++)
                tw.WriteElementString("Link", repositoryItemComboBox1.Items[i].ToString());
            tw.WriteEndElement();

            tw.WriteStartElement("Favorites");
            for (int i = 0; i < barManager1.Items.Count; i++)
                if (barManager1.Items[i].Category == barManager1.Categories["Favorites"])
                    tw.WriteElementString("Favorite", barManager1.Items[i].Tag.ToString(), barManager1.Items[i].Caption);
            tw.WriteEndElement();

            tw.WriteEndElement();
            tw.Close();
        }

        /// <summary>
        /// Adds the favorite item.
        /// </summary>
        /// <param name="locationName">Name of the location.</param>
        /// <param name="locationURL">The location URL.</param>
        private void AddFavoriteItem(string locationName, string locationURL)
        {
            AddFavoriteItem(locationName, locationURL, false);
        }

        /// <summary>
        /// Adds the favorite item.
        /// </summary>
        /// <param name="locationName">Name of the location.</param>
        /// <param name="locationURL">The location URL.</param>
        /// <param name="init">if set to <c>true</c> [init].</param>
        private void AddFavoriteItem(string locationName, string locationURL, bool init)
        {
            BarItem item = new BarButtonItem();
            item.ItemClick += new ItemClickEventHandler(Favorite_Click);
            item.Category = barManager1.Categories["Favorites"];
            item.Caption = locationName;
            item.Tag = locationURL;
            barManager1.Items.Add(item);
            if (!init) ChangeFavorites();
        }

        /// <summary>
        /// Adds the favorite.
        /// </summary>
        private void AddFavorite()
        {
            if (ctrMdiMain.Pages.Count == 0)
                return;

            XtraFormPL form = (XtraFormPL)ctrMdiMain.SelectedPage.MdiChild;
            //frmAddFavorites f = new frmAddFavorites(form.Text, form.Tag.ToString(), imageList1.Images[2]);

            string Favorite = TagPropertyMan.Get(form.Tag, "FAVORITE").ToString();
            frmAddFavorites f = new frmAddFavorites(form.Text, Favorite, imageList1.Images[2]);
            if (f.ShowDialog() == DialogResult.OK)
            {
                bool add = true;
                for (int i = 0; i < barManager1.Items.Count; i++)
                {
                    BarItem item = barManager1.Items[i];
                    if (item.Category == barManager1.Categories["Favorites"] && item.Caption == f.LocationName)
                    {
                        if (DevExpress.XtraEditors.XtraMessageBox.Show(
                            "Tên chỉ định đã tồn tại trong danh sách Favorites. Bạn có muốn thay thế nó?",
                            Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            item.Tag = f.LocationURL;
                        add = false;
                        break;
                    }
                }
                if (add)
                    AddFavoriteItem(f.LocationName, f.LocationURL);
            }
        }

        /// <summary>
        /// Changes the favorites.
        /// </summary>
        private void ChangeFavorites()
        {
            ChangeFavorites(false);
        }

        /// <summary>
        /// Changes the favorites.
        /// </summary>
        /// <param name="init">if set to <c>true</c> [init].</param>
        private void ChangeFavorites(bool init)
        {
            siFavorites.ClearLinks();
            ctrlFavorites1.DeleteItems();

            siFavorites.AddItem(iAdd);
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.Category == barManager1.Categories["Favorites"])
                {
                    siFavorites.AddItem(item);
                    ctrlFavorites1.AddItem(item, init);
                }
            }
            if (siFavorites.ItemLinks.Count > 1) siFavorites.ItemLinks[1].BeginGroup = true;
        }

        /// <summary>
        /// Items the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private BarItem ItemByName(string name)
        {
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.Category == barManager1.Categories["Favorites"] && item.Caption == name)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// Handles the ItemClick event of the iFavorites control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iFavorites_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (iFavorites.Down)
                dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            else
            {
                if (dockPanel1.Dock == DevExpress.XtraBars.Docking.DockingStyle.Float)
                    dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                else
                    dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            }
        }

        /// <summary>
        /// Handles the AddNewFavorite event of the ctrlFavorites1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ctrlFavorites1_AddNewFavorite(object sender, System.EventArgs e)
        {
            AddFavorite();
        }

        /// <summary>
        /// Handles the DeleteFavorite event of the ctrlFavorites1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ctrlFavorites1_DeleteFavorite(object sender, System.EventArgs e)
        {
            string s = sender.ToString();
            if (DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure you want to remove shortcut?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BarItem item = ItemByName(s);
                if (item != null)
                {
                    barManager1.Items.Remove(item);
                    ChangeFavorites();
                }
            }
        }

        /// <summary>
        /// Handles the EditFavorite event of the ctrlFavorites1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ctrlFavorites1_EditFavorite(object sender, System.EventArgs e)
        {
            string s = sender.ToString();
            BarItem item = ItemByName(s);
            if (item != null)
            {
                frmAddFavorites f = new frmAddFavorites(item.Caption, item.Tag.ToString(), imageList1.Images[1], false);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    item.Caption = f.LocationName;
                    item.Tag = f.LocationURL;
                    ChangeFavorites();
                }
            }
        }

        /// <summary>
        /// Handles the OpenFavorite event of the ctrlFavorites1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ctrlFavorites1_OpenFavorite(object sender, System.EventArgs e)
        {
            BarItem item = ItemByName(sender.ToString());
            if (item != null)
                GoFURL(item.Tag.ToString());
        }

        /// <summary>
        /// Handles the Click event of the Favorite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void Favorite_Click(object sender, ItemClickEventArgs e)
        {
            GoFURL(e.Item.Tag.ToString());
        }
        #endregion

        #region Các hàm trợ giúp khác
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>The address.</value>
        string Address
        {
            get
            {
                if (barManager1.ActiveEditor != null && barManager1.ActiveEditor.EditValue != null)
                    return barManager1.ActiveEditor.EditValue.ToString();
                return null;
            }
        }

        

        /// <summary>
        /// Gets the index form child.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        
        private int getIndexFormChild(XtraForm form)
        {
            object FavoriteObj = TagPropertyMan.Get(form.Tag, "FAVORITE");
            string Favorite = null;
            if (FavoriteObj != null) Favorite = FavoriteObj.ToString();
            for (int i = 0; i < ctrMdiMain.Pages.Count; i++)
            {
                string MDIChildFavorite = TagPropertyMan.Get(ctrMdiMain.Pages[i].MdiChild.Tag, "FAVORITE").ToString();
                if (MDIChildFavorite.Equals(Favorite))
                    return i;
                //if (ctrMdiMain.Pages[i].MdiChild.Tag.Equals(Favorite))
                //    return i;
            }
            return -1;
        }

        /// <summary>
        /// Loads the search result page.
        /// </summary>
        /// <param name="list_form">The list_form.</param>
        private void LoadSearchResultPage(List<FURLAddress> list_form)
        {
            if (list_form != null && list_form.Count > 0)
            {
                XtraFormPL frm_result = new XtraFormPL();
                frm_result.Text = "Kết quả tìm kiếm FURL";
                object temp = frm_result.Tag;
                TagPropertyMan.InsertOrUpdate(ref temp, "FAVORITE", "Tìm kiếm");
                frm_result.Tag = temp;

                ctrlFURLResult ctr = new ctrlFURLResult();
                ctr.OpenURL += new EventHandler(ctr_OpenURL);
                ctr.Dock = DockStyle.Fill;
                ctr._init(list_form);

                frm_result.Controls.Add(ctr);

                if (getIndexFormChild(frm_result) == -1)
                    ProtocolForm.ShowWindow(this, frm_result, true);
                else
                {
                    ctrMdiMain.SelectedPage = ctrMdiMain.Pages[getIndexFormChild(frm_result)];
                    ctrlFURLResult _ctrl = (ctrlFURLResult)ctrMdiMain.SelectedPage.MdiChild.Controls[0];
                    _ctrl._init(list_form);
                }
            }
            else
                HelpMsgBox.ShowNotificationMessage("Không tìm thấy kết quả.");
        }

        

        /// <summary>
        /// Opens the notepad.
        /// </summary>
        private void OpenNotepad()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            string s = System.Environment.SystemDirectory + "\\Notepad.exe";
            if (System.IO.File.Exists(s))
            {
                p.StartInfo.FileName = s;
                p.Start();
            }
        }

        /// <summary>
        /// Inits the paint style.
        /// </summary>
        /// <param name="item">The item.</param>
        private void InitPaintStyle(BarItem item)
        {
            if (item == null)
                return;
            iPaintStyle.ImageIndex = item.ImageIndex;
            iPaintStyle.Caption = item.Caption;
            iPaintStyle.Hint = item.Description;
            ctrlFavorites1.barManager1.GetController().PaintStyleName = barManager1.GetController().PaintStyleName;
        }

        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="form">The form.</param>
        private void SetStatus(XtraFormPL form)
        {
            int index = getIndexFormChild(form);
            if ((index > 0) && index < (ctrMdiMain.Pages.Count - 1))
            {
                iBack.Enabled = true;
                iForward.Enabled = true;
            }
            else
            {
                if (index == 0 && index < (ctrMdiMain.Pages.Count - 1))
                {
                    iBack.Enabled = false;
                    iForward.Enabled = true;
                }
                else if (index == (ctrMdiMain.Pages.Count - 1) && index > 0)
                {
                    iForward.Enabled = false;
                    iBack.Enabled = true;
                }
                else
                {
                    iForward.Enabled = false;
                    iBack.Enabled = false;
                }
            }
        }
        #endregion

        #region Các sự kiện Button chính
        /// <summary>
        /// Handles the ItemClick event of the iGo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iGo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoFURL(eAddress.EditValue.ToString());
        }

        /// <summary>
        /// Handles the ItemClick event of the iBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                XtraFormPL form = (XtraFormPL)ctrMdiMain.SelectedPage.MdiChild;
                int current_index = getIndexFormChild(form);
                ctrMdiMain.SelectedPage = ctrMdiMain.Pages[--current_index];
                       
                if (current_index == 0)              
                    iBack.Enabled = false;                
            }
            catch { }
        }

        /// <summary>
        /// Handles the ItemClick event of the iForward control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iForward_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                XtraFormPL form = (XtraFormPL)ctrMdiMain.SelectedPage.MdiChild;
                int current_index = getIndexFormChild(form);
                ctrMdiMain.SelectedPage = ctrMdiMain.Pages[++current_index];

                if (current_index == ctrMdiMain.Pages.Count - 1)                
                    iForward.Enabled = false;                      
            }
            catch { }
        }

        /// <summary>
        /// Handles the ItemClick event of the iRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
        }

        /// <summary>
        /// Handles the ItemClick event of the iHome control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            LoadSearchResultPage(null);
        }

        /// <summary>
        /// Handles the ItemClick event of the iSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (eAddress.EditValue != null && eAddress.EditValue.ToString() != "")
            {
                List<FURLAddress> list_form = FURLAddressStore.GetFURLAddresses(eAddress.EditValue.ToString());                
                LoadSearchResultPage(list_form);
            }
        }        

        /// <summary>
        /// Handles the ItemClick event of the iExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }        

        /// <summary>
        /// Handles the ItemClick event of the iEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenNotepad();
        }

        /// <summary>
        /// Handles the ItemClick event of the iAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void iAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddFavorite();
        }        
        #endregion 

        #region Các sự kiện khác
        /// <summary>
        /// Handles the SelectedItemChanged event of the repositoryItemComboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void repositoryItemComboBox1_SelectedItemChanged(object sender, System.EventArgs e)
        {
            //PHUOCNC Đoạn code sau ko cần thiết vì anh muốn tường minh GO ?
            //if (barManager1.ActiveEditor != null)
            //    if (!((DevExpress.XtraEditors.ComboBoxEdit)barManager1.ActiveEditor).IsPopupOpen &&
            //        ((DevExpress.XtraEditors.ComboBoxEdit)barManager1.ActiveEditor).SelectedIndex != -1)
            //        GoToForm(Address);
        }        

        /// <summary>
        /// Handles the KeyDown event of the repositoryItemComboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void repositoryItemComboBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DevExpress.XtraEditors.ComboBoxEdit edit = sender as DevExpress.XtraEditors.ComboBoxEdit;
            if (e.KeyData == Keys.Escape)
            {
                e.Handled = true;
                edit.SelectAll();
            }
            if (e.KeyData == Keys.Enter)
            {
                barManager1.ActiveEditItemLink.PostEditor();
                edit.SelectAll();
                e.Handled = true;
                GoFURL(eAddress.EditValue.ToString());
            }
        }

        /// <summary>
        /// Handles the Closing event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveXML();
        }

        /// <summary>
        /// Handles the VisibilityChanged event of the dockPanel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.Docking.VisibilityChangedEventArgs"/> instance containing the event data.</param>
        private void dockPanel1_VisibilityChanged(object sender, DevExpress.XtraBars.Docking.VisibilityChangedEventArgs e)
        {
            iFavorites.Down = e.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        /// <summary>
        /// Handles the ItemClick event of the ips control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void ips_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barManager1.GetController().PaintStyleName = e.Item.Description;
            InitPaintStyle(e.Item);
            barManager1.GetController().ResetStyleDefaults();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle();
        }

        /// <summary>
        /// Handles the SelectedPageChanged event of the ctrMdiMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ctrMdiMain_SelectedPageChanged(object sender, EventArgs e)
        {
            try
            {
                if (ctrMdiMain.SelectedPage != null)
                {
                    XtraFormPL form = (XtraFormPL)ctrMdiMain.SelectedPage.MdiChild;
                    currentAddress = TagPropertyMan.Get(form.Tag, "FAVORITE").ToString();
                    eAddress.EditValue = TagPropertyMan.Get(form.Tag, "FAVORITE");
                    //currentAddress = form.Tag.ToString();
                    //eAddress.EditValue = form.Tag;
                    SetStatus(form);
                }
            }
            catch { }
        }

        /// <summary>
        /// Handles the PageRemoved event of the ctrMdiMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraTabbedMdi.MdiTabPageEventArgs"/> instance containing the event data.</param>
        private void ctrMdiMain_PageRemoved(object sender, MdiTabPageEventArgs e)
        {
            try
            {
                if (ctrMdiMain.SelectedPage != null)
                {
                    XtraFormPL form = (XtraFormPL)ctrMdiMain.SelectedPage.MdiChild;
                    SetStatus(form);
                }
            }
            catch { }
        }

        /// <summary>
        /// Handles the ItemClick event of the siFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void siFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion                        

        /// <summary>
        /// Handles the OpenURL event of the ctr control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ctr_OpenURL(object sender, EventArgs e)
        {
            GoFURL(sender.ToString());

            #region Not use
            //FURLAddress obj = DAFURLAddress.GetFURLAddress(sender.ToString());
            //if (obj != null)
            //{
            //    object instance = GenerateClass.initObject(obj.NAME_FORM);
            //    XtraFormPL form = instance as XtraFormPL;

            //    object tmp = form.Tag;
            //    TagPropertyMan.InsertOrUpdate(ref tmp, "FAVORITE", sender.ToString());
            //    form.Tag = tmp;

            //    //form.Tag = sender.ToString();

            //    if (getIndexFormChild(form) == -1)
            //    {
            //        ProtocolForm.ShowWindow(this, form, false);
            //        currentAddress = sender.ToString();
            //        AddNewItem(sender.ToString());
            //    }
            //    else
            //        ctrMdiMain.SelectedPage = ctrMdiMain.Pages[getIndexFormChild(form)];
            //}
            #endregion
        }

        /// <summary>
        /// Goes to form.
        /// </summary>
        /// <param name="address">The address.</param>
        private void GoFURL(string AddrStr)
        {
            if (AddrStr == null) return;
            //PHUOCNC Anh thấy nó chưa cập nhật đúng địa chỉ hiện hành do đó khi làm thế này là nó ko đến được form anh đang chọn trong Favorite ?
            //if (currentAddress != address)
            {
                eAddress.EditValue = AddrStr;
                FURLAddress obj = FURLAddressStore.GetFURLAddress(AddrStr);

                object instance = OpenFURLAddress(obj);
                if (instance != null && instance is XtraForm)
                {
                    XtraForm form = instance as XtraForm;

                    //form.Tag = address;//Tag có thể chứa nhiều thông tin khác bên trong
                    object temp = form.Tag;
                    TagPropertyMan.InsertOrUpdate(ref temp, "FAVORITE", AddrStr);
                    form.Tag = temp;

                    if (getIndexFormChild(form) == -1)
                    {
                        ProtocolForm.ShowWindow(this, form, false);
                        currentAddress = AddrStr;
                        AddNewItem(AddrStr);
                    }
                    else
                        ctrMdiMain.SelectedPage = ctrMdiMain.Pages[getIndexFormChild(form)];
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("FURL không tìm thấy.");
                }                
            }
        }

        public static object OpenFURLAddress(FURLAddress AddrObj)
        {
            if (AddrObj != null) 
                return AddrObj.FURL_TYPE.CreateInstance(AddrObj);
            return null;
        }

        public static string MenuItem(string ParentID, bool IsSep)
        {
            return MenuBuilder.CreateItem(typeof(frmFWFURLBrowser).FullName,
                "Trình duyệt chức năng",
                ParentID, true,
                typeof(frmFWFURLBrowser).FullName,
                false, IsSep, "mnuTrinhDuyet.png", false, "", "");
        }
    }
}
