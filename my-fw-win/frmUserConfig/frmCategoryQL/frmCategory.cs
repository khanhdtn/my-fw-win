using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.XPath;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using ProtocolVN.Framework.Core;
using System.Drawing;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Trung tâm điều khiển danh mục
    /// </summary>
    public class frmCenterCategory : frmCategory
    {
        public frmCenterCategory() : base() { }

        public frmCenterCategory(string ConfigXMLStr) : base(ConfigXMLStr) { }
    }

    /// <summary>
    /// Trung tâm điều khiển các danh mục. Nó là nơi tập trung tất cả danh mục.
    /// </summary>
    public partial class frmCategory : XtraFormPL, IParamForm, IFormCategory
    {
        private NavBarItem previousItem;
        private NavBarItem currentItem;
        public int viewID = 2; //1: View cũ 2: View mới;

        public frmCategory()
        {
            InitializeComponent();
            this.initData(FrameworkParams.Category);
        }

        public frmCategory(string ConfigXMLStr)
        {
            InitializeComponent();
            this.initData(ConfigXMLStr);
        }

        public frmCategory(string ConfigXMLStr, int viewID)
        {
            InitializeComponent();
            this.viewID = viewID;
            this.initData(ConfigXMLStr);
        }


        #region Nạp dữ liệu từ file xml
        private void initData(string ConfigXMLStr)
        {
            WinLaw.checkLaw(this);

            this.addButton.Image = FWImageDic.ADD_IMAGE20;
            this.editButton.Image = FWImageDic.EDIT_IMAGE20;
            this.removeButton.Image = FWImageDic.DELETE_IMAGE20;
            this.closeButton.Image = FWImageDic.CLOSE_IMAGE20;
            this.btnSave.Image = FWImageDic.SAVE_IMAGE20;
            this.btnNoSave.Image = FWImageDic.NO_SAVE_IMAGE20;

            String language = "vn";

            XPathNavigator nav = null;
            XPathDocument docNav = null;
            System.IO.StringReader sr = new System.IO.StringReader(ConfigXMLStr);
            docNav = new XPathDocument(sr);
            nav = docNav.CreateNavigator();

            XPathNodeIterator iterator = nav.Select("/basiccats/group");
            while (iterator.MoveNext())
            {
                XPathNavigator groupNode = iterator.Current;
                //Nav Group
                DevExpress.XtraNavBar.NavBarGroup group = new DevExpress.XtraNavBar.NavBarGroup();
                group.Expanded = true;
                XPathNodeIterator langCatNode = groupNode.SelectChildren("lang", groupNode.NamespaceURI);
                while (langCatNode.MoveNext())
                {
                    //<group id ='3' picindex='navChuXe.png'>
                    //    <lang id='vn'>Đối tác dịch vụ</lang>
                    //    <lang id='en'></lang>
                    //    <cat>...</cat>
                    //</group>
                    XPathNavigator tmp = langCatNode.Current;
                    if (tmp.GetAttribute("id", tmp.NamespaceURI).Equals(language))
                    {
                        group.Caption = langCatNode.Current.Value.Trim();
                        group.Name = langCatNode.Current.Value.Trim();
                        group.Tag = groupNode.GetAttribute("id", groupNode.NamespaceURI);
                    }
                }
                this.navBarControl1.Groups.Add(group);
                //Nav Item
                XPathNodeIterator catNode = groupNode.SelectChildren("cat", groupNode.NamespaceURI);
                while (catNode.MoveNext())
                {
                    //<cat table='" + ProtocolVN.DanhMuc.PL.FURL(N, "Init") + @"' type='action' picindex='navChuXe.png'>
                    //    <lang id='vn'>Đối tác</lang>
                    //    <lang id='en'></lang>
                    //</cat>

                    XPathNavigator tmp = catNode.Current;
                    DevExpress.XtraNavBar.NavBarItem navItem = new DevExpress.XtraNavBar.NavBarItem();
                    navItem.Name = tmp.GetAttribute("table", tmp.NamespaceURI);
                    navItem.Tag = tmp.GetAttribute("type", tmp.NamespaceURI);
                    //navItem.LargeImage = FWImageDic.getImage4848(tmp.GetAttribute("picindex", tmp.NamespaceURI));
                    navItem.SmallImage = HelpImage.getImage1616(tmp.GetAttribute("picindex", tmp.NamespaceURI));
                    //navItem.LargeImage = ResourceMan.getImage48(tmp.GetAttribute("picindex", tmp.NamespaceURI));
                    //navItem.SmallImage = ResourceMan.getImage32(tmp.GetAttribute("picindex", tmp.NamespaceURI));

                    XPathNodeIterator tmpSub = tmp.SelectChildren("lang", tmp.NamespaceURI);
                    while (tmpSub.MoveNext())
                    {
                        XPathNavigator tmp2 = tmpSub.Current;
                        if (tmp2.GetAttribute("id", tmp2.NamespaceURI).Equals(language))
                        {
                            navItem.Caption = tmpSub.Current.Value.Trim();
                        }
                    }
                    try
                    {
                        string param = "";
                        string formName = navItem.Name;

                        if (formName.Contains("?") && formName.Contains("="))
                        {
                            param = formName.Substring(formName.IndexOf('=') + 1).Trim();
                            formName = formName.Substring(0, formName.IndexOf('?')).Trim();
                        }
                        previousItem = currentItem;

                        XtraUserControl control = (XtraUserControl)GenerateClass.initMethod(formName, param, false);

                        if (control != null)
                        {
                            if (HelpPermission.CheckCtrl(control) == false)
                            {
                                navItem.Enabled = false;
                            }
                        }
                    }
                    catch { }
                    navItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem_LinkClicked);
                    DevExpress.XtraNavBar.NavBarItemLink navItemLink = new DevExpress.XtraNavBar.NavBarItemLink(navItem);
                    group.ItemLinks.Add(navItemLink);
                    this.navBarControl1.Items.Add(navItem);
                }
                //Gắn hình vào group

                string image = groupNode.GetAttribute("picindex", groupNode.NamespaceURI);
                if (image == String.Empty)
                {
                    if (group.ItemLinks.Count > 0)
                        group.SmallImage = group.ItemLinks[0].Item.SmallImage;
                }
                else
                {
                    group.SmallImage = HelpImage.getImage1616(image);
                }
            }

            if (navBarControl1.ActiveGroup.ItemLinks.Count > 0)
            {
                lblCat.Text = navBarControl1.ActiveGroup.ItemLinks[0].Item.Caption;
            }
            else
            {
                lblCat.Text = "Chưa xác định danh mục";
            }

            if (viewID == 2)//View Old
            {
                toView2();
            }
            else if (viewID == 1)// View Old
            {
                //NOOP
            }
        }
        private void toView2()
        {
            //this.SuspendLayout();

            NavBarControl navBaControl = this.navBarControl1;
            navBaControl.PaintStyleKind = NavBarViewKind.NavigationPane;
            navBaControl.NavigationPaneMaxVisibleGroups = (this.navBarControl1.Groups.Count > 10 ? 10 : this.navBarControl1.Groups.Count);
            navBaControl.Dock = DockStyle.Left;

            ((Control)this.dockPanel1).Visible = false;
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanel1.Width = 0;
            this.dockPanel1.Height = 0;
            this.dockPanel1.Top = 0;
            this.dockPanel1.Left = 0;
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.Controls.Remove(this.dockPanel1);

            //Add lại một navBarControl
            this.Controls.Add(navBaControl);

            //this.ResumeLayout(false);
        }


        #endregion

        #region Sự kiện trên form
        /// <summary>Focus vào mục chỉ định dựa vào tham số
        /// </summary>
        public void FocusParam(string param)
        {
            foreach (NavBarGroup group in this.navBarControl1.Groups)
            {
                foreach (NavBarItemLink link in group.ItemLinks)
                {
                    if (link.Item.Name == param)
                    {
                        navBarItem_LinkClicked(null, new NavBarLinkEventArgs(link));
                        group.Expanded = true;
                        this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                        this.dockPanel1.Hide();
                        return;
                    }
                    else if (param == "")
                    {
                        navBarItem_LinkClicked(null, new NavBarLinkEventArgs(link));
                        return;
                    }
                }
                group.Expanded = false;
            }
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            if (TagPropertyMan.Get(this.Tag, "FORM_PARAM") != null)
            {
                FocusParam(TagPropertyMan.Get(this.Tag, "FORM_PARAM").ToString());
            }
            else
            {
                if (navBarControl1.ActiveGroup.ItemLinks != null &&
                    navBarControl1.ActiveGroup.ItemLinks.Count >= 1)
                    FocusParam(navBarControl1.ActiveGroup.ItemLinks[0].Item.Name);
            }
        }

        /// <summary>Đóng form
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>Chọn Xóa mục tin
        /// </summary>
        private void removeButton_Click(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.DeleteAction(null);
        }

        /// <summary>Chọn Sửa mục tin
        /// </summary>
        private void editButton_Click(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.EditAction(null);
        }

        /// <summary>Chọn Thêm mục tin
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.AddAction(null);
        }

        /// <summary>Chọn Lưu mục tin
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.SaveAction(null);
        }

        /// <summary>Chọn Không Lưu mục tin
        /// </summary>
        private void btnNoSave_Click(object sender, EventArgs e)
        {
            IPluginCategory dm = (IPluginCategory)this.control;
            dm.NoSaveAction(null);
        }

        private NavBarLinkEventArgs e;
        private ToolStripDropDownButton xuatRaFile = null;
        private ToolStripDropDownButton inLuoiItem = null;
        private void navBarItem_LinkClicked()
        {
            if (control != null)
            {
                this.panel1.Controls.Remove(control);
                this.control.Visible = false;
            }

            currentItem = e.Link.Item;
            if (previousItem != null && previousItem != currentItem)
            {
                previousItem.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                currentItem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                if (previousItem == null)
                    currentItem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            lblCat.Text = "Danh sách " + currentItem.Caption.ToLower();

            string param = "";
            string formName = currentItem.Name;

            if (formName.Contains("?") && formName.Contains("="))
            {
                param = formName.Substring(formName.IndexOf('=') + 1).Trim();
                formName = formName.Substring(0, formName.IndexOf('?')).Trim();
            }
            previousItem = currentItem;

            this.control = (XtraUserControl)GenerateClass.initMethod(formName, param, false);

            if (control == null) return;

            if (HelpPermission.CheckCtrl(control) == false)
            {
                ProtocolForm.ShowModalDialog(this, new frmPermissionFail());
                return;
            }

            ICategory cat = (ICategory)this.control;
            cat.SetOwner(this);
            cat.UpdateGUI();
            this.control.Disposed += delegate
            {
                this.Close();
            };

            this.control.Dock = DockStyle.Fill;
            this.control.Name = "control";
            //this.control.Visible = true;
            this.panel1.Controls.Add(control);
            #region Xử lý loại danh mục
            string type = currentItem.Tag.ToString();
            if (type == "action")
            {
                //IActionCategory catAction = (IActionCategory)this.control;
                //if (catAction.GetGridView() != null)
                //{
                //  //  xuatRaFile = HelpGrid.addXuatRaFileItem(catAction.GetMenuAction(), cat.GetGridView());
                //    //inLuoiItem = HelpGrid.addInLuoiItem(catAction.GetMenuAction(), cat.GetGridView());
                  
                //}
                //Dùng hoàn toàn User Control của mình
            }
            else if (type == "plugin")
            {
                //Dùng navigation của form frmCategory
                //this.closeButton.Visible = false;
                this.control.Controls.Add(this.toolStrip1);
                //Xuất ra File
                if (this.xuatRaFile != null)
                    this.toolStrip1.Items.Remove(this.xuatRaFile);
                this.xuatRaFile = HelpGrid.addXuatRaFileItem(this.toolStrip1, cat.GetGridView());

                //In lưới
                if (this.inLuoiItem != null)
                    this.toolStrip1.Items.Remove(this.inLuoiItem);
                this.inLuoiItem = HelpGrid.addInLuoiItem(this.toolStrip1, cat.GetGridView());
            }
            #endregion
            #region Xử lý 1 số chức năng mở rộng của danh mục
            if (HelpTagProperty.Get(this.control.Tag, "frmCategory_IMPORT") != null)
            {
                ICatFuncImport import = (ICatFuncImport)HelpTagProperty.Get(this.control.Tag, "frmCategory_IMPORT");
                ToolStripDropDownButton items = null;
                if (control is IActionCategory)
                {
                    IActionCategory catAction = (IActionCategory)control;
                    items = HelpGrid.addNhapTuFileItem(catAction.GetMenuAction(), catAction.GetGridView());
                }
                else //plugin
                {
                    items = HelpGrid.addNhapTuFileItem(this.toolStrip1, cat.GetGridView());
                }

                import.init(this, this.control, items);
            }
            //Mở rộng nhiều tính năng tiếp theo...
            #endregion
            this.control.Focus();
        }
        /// <summary>Chọn danh mục trên cây
        /// </summary>
        private void navBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.e = e;
            if (FrameworkParams.wait == null) FrameworkParams.wait = new WaitingMsg();
            navBarItem_LinkClicked();
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();

            //WaitingMsg.LongProcess(navBarItem_LinkClicked);
        }
        #endregion

        #region IParamForm Members

        void IParamForm.Activate()
        {
            FocusParam(TagPropertyMan.Get(this.Tag, "FORM_PARAM").ToString());
        }

        #endregion

        //#region IPermisionable Members
        //public List<Control> GetPermisionableControls()
        //{
        //    return null;
        //}
        //public List<Object> GetObjectItems()
        //{
        //    //List<Object> items = new List<Object>();
        //    //string featureName = "Quản lý danh mục";
        //    //ApplyPermissionAction.ApplyPermissionObject(items, addButton,
        //    //        new PermissionItem(featureName, PermissionType.ADD));

        //    //ApplyPermissionAction.ApplyPermissionObject(items, removeButton,
        //    //    new PermissionItem(featureName, PermissionType.DELETE));

        //    //ApplyPermissionAction.ApplyPermissionObject(items, editButton,
        //    //    new PermissionItem(featureName, PermissionType.EDIT));

        //    //return items;
        //    return null;
        //}
        //#endregion

        #region ILangable Members

        public List<Control> GetLangableControls()
        {
            return null;
        }

        #endregion

        #region IFormatable Members

        public List<Control> GetFormatControls()
        {
            return null;
        }

        #endregion

        public static void ShowCategory(XtraUserControl control, String Title, Size? size)
        {
            frmCategoryDialog item = new frmCategoryDialog(control, Title, size);
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, item);
        }

        #region IFormCategory Members

        public ToolStripButton GetRemoveBtn()
        {
            return this.removeButton;
        }

        public ToolStripButton GetAddBtn()
        {
            return this.addButton;
        }

        public ToolStripButton GetEditBtn()
        {
            return this.editButton;
        }

        public ToolStripButton GetSaveBtn()
        {
            return this.btnSave;
        }

        public ToolStripButton GetNoSaveBtn()
        {
            return this.btnNoSave;
        }
        public ToolStripDropDownButton GetPrintBtn()
        {
            return this.inLuoiItem;
        }

        public ToolStripDropDownButton GetExportBtn()
        {
            return this.xuatRaFile;
        }

        #endregion



        #region Hai hàm không sử dụng
        ///// <summary>
        ///// PHUOCNC : Refactor loại bỏ khái niệm "form"
        ///// </summary>
        //private string GetFormNameUpdate(string groupID, string tableName)
        //{
        //    XPathDocument docNav;
        //    XPathNavigator nav = null;
        //    System.IO.StringReader sr = new System.IO.StringReader(FrameworkParams.Category);
        //    docNav = new XPathDocument(sr);
        //    nav = docNav.CreateNavigator();

        //    XPathNodeIterator iterator = nav.Select("/basiccats");
        //    while (iterator.MoveNext())
        //    {
        //        XPathNavigator basiccatsNode = iterator.Current;
        //        XPathNodeIterator groupNode = basiccatsNode.SelectChildren("group", basiccatsNode.NamespaceURI);
        //        while (groupNode.MoveNext())
        //        {
        //            XPathNavigator tmp1 = groupNode.Current;
        //            if (tmp1.GetAttribute("id", tmp1.NamespaceURI).Equals(groupID))
        //            {
        //                XPathNodeIterator catNode = tmp1.SelectChildren("cat", tmp1.NamespaceURI);
        //                while (catNode.MoveNext())
        //                {
        //                    XPathNavigator tmp2 = catNode.Current;
        //                    if (tmp2.GetAttribute("table", tmp2.NamespaceURI).Equals(tableName))
        //                    {
        //                        XPathNodeIterator formUpdate = tmp2.SelectChildren("form", tmp2.NamespaceURI);
        //                        if (formUpdate != null)
        //                        {
        //                            if (formUpdate.MoveNext())
        //                                return formUpdate.Current.Value.ToString();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //to do
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //to do
        //            }
        //        }
        //    }
        //    return "";
        //}
        //private string[] getCaptions(string groupID, string tableName)
        //{
        //    List<string> temp = new List<string>();
        //    String language = "vn";
        //    XPathDocument docNav;
        //    XPathNavigator nav = null;
        //    System.IO.StringReader sr = new System.IO.StringReader(FrameworkParams.Category);
        //    docNav = new XPathDocument(sr);
        //    nav = docNav.CreateNavigator();

        //    XPathNodeIterator iterator = nav.Select("/basiccats");
        //    while (iterator.MoveNext())
        //    {
        //        XPathNavigator basiccatsNode = iterator.Current;
        //        XPathNodeIterator groupNode = basiccatsNode.SelectChildren("group", basiccatsNode.NamespaceURI);
        //        while (groupNode.MoveNext())
        //        {
        //            XPathNavigator tmp1 = groupNode.Current;
        //            if (tmp1.GetAttribute("id", tmp1.NamespaceURI).Equals(groupID))
        //            {
        //                XPathNodeIterator catNode = tmp1.SelectChildren("cat", tmp1.NamespaceURI);
        //                while (catNode.MoveNext())
        //                {
        //                    XPathNavigator tmp2 = catNode.Current;
        //                    if (tmp2.GetAttribute("table", tmp2.NamespaceURI).Equals(tableName))
        //                    {
        //                        XPathNodeIterator captionsNode = tmp2.SelectChildren("captions", tmp2.NamespaceURI);
        //                        while (captionsNode.MoveNext())
        //                        {
        //                            XPathNavigator tmp3 = captionsNode.Current;
        //                            if (tmp3.GetAttribute("id", tmp3.NamespaceURI).Equals(language))
        //                            {
        //                                XPathNodeIterator captionNode = tmp3.SelectChildren("caption", tmp3.NamespaceURI);
        //                                while (captionNode.MoveNext())
        //                                {
        //                                    XPathNavigator tmp4 = captionNode.Current;
        //                                    temp.Add(tmp4.Value.Trim());
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //to do
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //to do
        //            }
        //        }
        //    }
        //    return temp.ToArray();
        //}
        #endregion

    }
}