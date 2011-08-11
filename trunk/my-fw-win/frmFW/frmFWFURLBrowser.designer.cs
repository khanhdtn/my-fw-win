namespace ProtocolVN.Framework.Win
{
    public partial class frmFWFURLBrowser {
        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFWFURLBrowser));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.iBack = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iForward = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iRefresh = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iHome = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iSearch = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iFavorites = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iEdit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.siFavorites = new DevExpress.XtraBars.BarSubItem();
            this.iAdd = new DevExpress.XtraBars.BarButtonItem();
            this.siFile = new DevExpress.XtraBars.BarSubItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.eAddress = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.iGo = new DevExpress.XtraBars.BarButtonItem();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.iText = new DevExpress.XtraBars.BarStaticItem();
            this.eProgress = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ctrlFavorites1 = new ctrlFURLFavorites();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.iMedia = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iToolBars = new DevExpress.XtraBars.BarToolbarsListItem();
            this.iExit = new DevExpress.XtraBars.BarButtonItem();
            this.ipsWXP = new DevExpress.XtraBars.BarButtonItem();
            this.ipsOXP = new DevExpress.XtraBars.BarButtonItem();
            this.ipsO2K = new DevExpress.XtraBars.BarButtonItem();
            this.iPaintStyle = new DevExpress.XtraBars.BarSubItem();
            this.ipsDefault = new DevExpress.XtraBars.BarButtonItem();
            this.ipsO3 = new DevExpress.XtraBars.BarButtonItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ctrMdiMain = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrMdiMain)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3,
            this.bar4});
            this.barManager1.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Built-in Menus", new System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")),
            new DevExpress.XtraBars.BarManagerCategory("Standard", new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b")),
            new DevExpress.XtraBars.BarManagerCategory("Address", new System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d")),
            new DevExpress.XtraBars.BarManagerCategory("StatusBar", new System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283")),
            new DevExpress.XtraBars.BarManagerCategory("Items", new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")),
            new DevExpress.XtraBars.BarManagerCategory("Favorites", new System.Guid("e1ba440c-33dc-4df6-b712-79cdc4dcd983"))});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList2;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.siFile,
            this.siFavorites,
            this.iBack,
            this.iForward,
            this.iRefresh,
            this.iHome,
            this.iSearch,
            this.iFavorites,
            this.iMedia,
            this.iEdit,
            this.iGo,
            this.eAddress,
            this.iText,
            this.eProgress,
            this.iToolBars,
            this.iExit,
            this.iAdd,
            this.ipsWXP,
            this.ipsOXP,
            this.ipsO2K,
            this.iPaintStyle,
            this.ipsO3,
            this.ipsDefault});
            this.barManager1.LargeImages = this.imageList1;
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 39;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemProgressBar1});
            this.barManager1.StatusBar = this.bar4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Standard Buttons";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(48, 104);
            this.bar1.FloatSize = new System.Drawing.Size(20, 22);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iBack),
            new DevExpress.XtraBars.LinkPersistInfo(this.iForward),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.iHome),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSearch, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iFavorites),
            new DevExpress.XtraBars.LinkPersistInfo(this.iEdit)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.Text = "Standard Buttons";
            // 
            // iBack
            // 
            this.iBack.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iBack.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iBack.Enabled = false;
            this.iBack.Hint = "Back";
            this.iBack.Id = 6;
            this.iBack.LargeImageIndex = 0;
            this.iBack.LargeImageIndexDisabled = 12;
            this.iBack.Name = "iBack";
            this.iBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iBack_ItemClick);
            // 
            // iForward
            // 
            this.iForward.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iForward.Enabled = false;
            this.iForward.Hint = "Forward";
            this.iForward.Id = 8;
            this.iForward.LargeImageIndex = 3;
            this.iForward.LargeImageIndexDisabled = 13;
            this.iForward.Name = "iForward";
            this.iForward.ShowCaptionOnBar = false;
            this.iForward.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iForward_ItemClick);
            // 
            // iRefresh
            // 
            this.iRefresh.Caption = "Refresh";
            this.iRefresh.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iRefresh.Hint = "Refresh";
            this.iRefresh.Id = 10;
            this.iRefresh.LargeImageIndex = 9;
            this.iRefresh.Name = "iRefresh";
            this.iRefresh.ShowCaptionOnBar = false;
            this.iRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
            this.iRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iRefresh_ItemClick);
            // 
            // iHome
            // 
            this.iHome.Caption = "Home";
            this.iHome.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iHome.Hint = "Home";
            this.iHome.Id = 11;
            this.iHome.LargeImageIndex = 5;
            this.iHome.Name = "iHome";
            this.iHome.ShowCaptionOnBar = false;
            this.iHome.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
            this.iHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iHome_ItemClick);
            // 
            // iSearch
            // 
            this.iSearch.Caption = "Tìm FURL";
            this.iSearch.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iSearch.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iSearch.Hint = "Search";
            this.iSearch.Id = 12;
            this.iSearch.LargeImageIndex = 10;
            this.iSearch.Name = "iSearch";
            this.iSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iSearch_ItemClick);
            // 
            // iFavorites
            // 
            this.iFavorites.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.iFavorites.Caption = "Sổ địa chỉ";
            this.iFavorites.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iFavorites.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iFavorites.Down = true;
            this.iFavorites.Hint = "Favorites";
            this.iFavorites.Id = 13;
            this.iFavorites.LargeImageIndex = 2;
            this.iFavorites.Name = "iFavorites";
            this.iFavorites.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iFavorites_ItemClick);
            // 
            // iEdit
            // 
            this.iEdit.Caption = "Edit";
            this.iEdit.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iEdit.Hint = "Open Notepad";
            this.iEdit.Id = 19;
            this.iEdit.LargeImageIndex = 1;
            this.iEdit.Name = "iEdit";
            this.iEdit.ShowCaptionOnBar = false;
            this.iEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.iEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iEdit_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarItemHorzIndent = 6;
            this.bar2.BarName = "MainMenu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(39, 106);
            this.bar2.FloatSize = new System.Drawing.Size(20, 22);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.siFavorites),
            new DevExpress.XtraBars.LinkPersistInfo(this.siFile)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.RotateWhenVertical = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "MainMenu";
            // 
            // siFavorites
            // 
            this.siFavorites.Caption = "Sổ địa chỉ";
            this.siFavorites.CategoryGuid = new System.Guid("4712321c-b9cd-461f-b453-4a7791063abb");
            this.siFavorites.Id = 3;
            this.siFavorites.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iAdd)});
            this.siFavorites.Name = "siFavorites";
            // 
            // iAdd
            // 
            this.iAdd.Caption = "Thêm FURL vào sổ";
            this.iAdd.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iAdd.Id = 28;
            this.iAdd.Name = "iAdd";
            this.iAdd.OwnFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.iAdd.UseOwnFont = true;
            this.iAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iAdd_ItemClick);
            // 
            // siFile
            // 
            this.siFile.Caption = "Đóng";
            this.siFile.CategoryGuid = new System.Guid("4712321c-b9cd-461f-b453-4a7791063abb");
            this.siFile.Id = 0;
            this.siFile.Name = "siFile";
            this.siFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.siFile_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Address Bar";
            this.bar3.DockCol = 1;
            this.bar3.DockRow = 1;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.eAddress),
            new DevExpress.XtraBars.LinkPersistInfo(this.iGo)});
            this.bar3.Offset = 401;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.Text = "Address Bar";
            // 
            // eAddress
            // 
            this.eAddress.AutoFillWidth = true;
            this.eAddress.Caption = "FURL";
            this.eAddress.CategoryGuid = new System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d");
            this.eAddress.Edit = this.repositoryItemComboBox1;
            this.eAddress.Id = 21;
            this.eAddress.IEBehavior = true;
            this.eAddress.Name = "eAddress";
            this.eAddress.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.eAddress.Width = 400;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AllowFocused = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.CycleOnDblClick = false;
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repositoryItemComboBox1_KeyDown);
            this.repositoryItemComboBox1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox1_SelectedItemChanged);
            // 
            // iGo
            // 
            this.iGo.Caption = "Truy cập";
            this.iGo.CategoryGuid = new System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d");
            this.iGo.Glyph = ((System.Drawing.Image)(resources.GetObject("iGo.Glyph")));
            this.iGo.Hint = "Go to ...";
            this.iGo.Id = 20;
            this.iGo.Name = "iGo";
            this.iGo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.iGo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iGo_ItemClick);
            // 
            // bar4
            // 
            this.bar4.BarName = "Status Bar";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.FloatLocation = new System.Drawing.Point(30, 434);
            this.bar4.FloatSize = new System.Drawing.Size(20, 22);
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iText),
            new DevExpress.XtraBars.LinkPersistInfo(this.eProgress)});
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.DrawSizeGrip = true;
            this.bar4.OptionsBar.RotateWhenVertical = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Status Bar";
            this.bar4.Visible = false;
            // 
            // iText
            // 
            this.iText.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.iText.CategoryGuid = new System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283");
            this.iText.Id = 22;
            this.iText.Name = "iText";
            this.iText.RightIndent = 3;
            this.iText.TextAlignment = System.Drawing.StringAlignment.Near;
            this.iText.Width = 32;
            // 
            // eProgress
            // 
            this.eProgress.CanOpenEdit = false;
            this.eProgress.CategoryGuid = new System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283");
            this.eProgress.Edit = this.repositoryItemProgressBar1;
            this.eProgress.EditHeight = 10;
            this.eProgress.Id = 24;
            this.eProgress.Name = "eProgress";
            this.eProgress.Width = 70;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.repositoryItemProgressBar1.Appearance.Options.UseBackColor = true;
            this.repositoryItemProgressBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PaintStyleName = "Skin";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("1734463f-4924-485e-9b75-59ea0e8bfee3");
            this.dockPanel1.Location = new System.Drawing.Point(0, 60);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.AllowDockBottom = false;
            this.dockPanel1.Options.AllowDockFill = false;
            this.dockPanel1.Options.AllowDockRight = false;
            this.dockPanel1.Options.AllowDockTop = false;
            this.dockPanel1.Options.AllowFloating = false;
            this.dockPanel1.Options.FloatOnDblClick = false;
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.Options.ShowMaximizeButton = false;
            this.dockPanel1.Size = new System.Drawing.Size(159, 404);
            this.dockPanel1.Text = "Sổ địa chỉ";
            this.dockPanel1.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel1_VisibilityChanged);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.ctrlFavorites1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(153, 376);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // ctrlFavorites1
            // 
            this.ctrlFavorites1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlFavorites1.Location = new System.Drawing.Point(0, 0);
            this.ctrlFavorites1.Name = "ctrlFavorites1";
            this.ctrlFavorites1.Size = new System.Drawing.Size(153, 376);
            this.ctrlFavorites1.TabIndex = 0;
            this.ctrlFavorites1.AddNewFavorite += new System.EventHandler(this.ctrlFavorites1_AddNewFavorite);
            this.ctrlFavorites1.OpenFavorite += new System.EventHandler(this.ctrlFavorites1_OpenFavorite);
            this.ctrlFavorites1.EditFavorite += new System.EventHandler(this.ctrlFavorites1_EditFavorite);
            this.ctrlFavorites1.DeleteFavorite += new System.EventHandler(this.ctrlFavorites1_DeleteFavorite);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            this.imageList2.Images.SetKeyName(5, "");
            // 
            // iMedia
            // 
            this.iMedia.Caption = "Media";
            this.iMedia.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iMedia.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iMedia.Hint = "Media";
            this.iMedia.Id = 15;
            this.iMedia.LargeImageIndex = 7;
            this.iMedia.Name = "iMedia";
            // 
            // iToolBars
            // 
            this.iToolBars.Caption = "ToolBarsList";
            this.iToolBars.CategoryGuid = new System.Guid("4712321c-b9cd-461f-b453-4a7791063abb");
            this.iToolBars.Id = 25;
            this.iToolBars.Name = "iToolBars";
            // 
            // iExit
            // 
            this.iExit.Caption = "Đóng";
            this.iExit.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iExit.Id = 27;
            this.iExit.Name = "iExit";
            this.iExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iExit_ItemClick);
            // 
            // ipsWXP
            // 
            this.ipsWXP.Caption = "Windows XP";
            this.ipsWXP.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.ipsWXP.Description = "WindowsXP";
            this.ipsWXP.Id = 32;
            this.ipsWXP.ImageIndex = 4;
            this.ipsWXP.Name = "ipsWXP";
            this.ipsWXP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ips_ItemClick);
            // 
            // ipsOXP
            // 
            this.ipsOXP.Caption = "Office XP";
            this.ipsOXP.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.ipsOXP.Description = "OfficeXP";
            this.ipsOXP.Id = 33;
            this.ipsOXP.ImageIndex = 2;
            this.ipsOXP.Name = "ipsOXP";
            this.ipsOXP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ips_ItemClick);
            // 
            // ipsO2K
            // 
            this.ipsO2K.Caption = "Office 2000";
            this.ipsO2K.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.ipsO2K.Description = "Office2000";
            this.ipsO2K.Id = 34;
            this.ipsO2K.ImageIndex = 3;
            this.ipsO2K.Name = "ipsO2K";
            this.ipsO2K.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ips_ItemClick);
            // 
            // iPaintStyle
            // 
            this.iPaintStyle.Caption = "Paint Style";
            this.iPaintStyle.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iPaintStyle.Id = 35;
            this.iPaintStyle.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsDefault),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsWXP),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsOXP),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsO2K),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsO3)});
            this.iPaintStyle.Name = "iPaintStyle";
            this.iPaintStyle.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // ipsDefault
            // 
            this.ipsDefault.Caption = "Default";
            this.ipsDefault.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.ipsDefault.Description = "Default";
            this.ipsDefault.Id = 37;
            this.ipsDefault.Name = "ipsDefault";
            this.ipsDefault.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ips_ItemClick);
            // 
            // ipsO3
            // 
            this.ipsO3.Caption = "Office 2003";
            this.ipsO3.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.ipsO3.Description = "Office2003";
            this.ipsO3.Id = 36;
            this.ipsO3.ImageIndex = 5;
            this.ipsO3.Name = "ipsO3";
            this.ipsO3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ips_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "");
            // 
            // ctrMdiMain
            // 
            this.ctrMdiMain.Controller = this.barAndDockingController1;
            this.ctrMdiMain.MdiParent = this;
            this.ctrMdiMain.SelectedPageChanged += new System.EventHandler(this.ctrMdiMain_SelectedPageChanged);
            this.ctrMdiMain.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.ctrMdiMain_PageRemoved);
            // 
            // frmBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(668, 490);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.IsMdiContainer = true;
            this.Name = "frmBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trình duyệt FURL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrMdiMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarSubItem siFile;
        private DevExpress.XtraBars.BarSubItem siFavorites;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarLargeButtonItem iBack;
        private DevExpress.XtraBars.BarLargeButtonItem iForward;
        private DevExpress.XtraBars.BarLargeButtonItem iRefresh;
        private DevExpress.XtraBars.BarLargeButtonItem iHome;
        private DevExpress.XtraBars.BarLargeButtonItem iSearch;
        private DevExpress.XtraBars.BarLargeButtonItem iFavorites;
        private DevExpress.XtraBars.BarLargeButtonItem iMedia;
        private DevExpress.XtraBars.BarLargeButtonItem iEdit;
        private DevExpress.XtraBars.BarButtonItem iGo;
        private DevExpress.XtraBars.BarEditItem eAddress;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarEditItem eProgress;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraBars.BarStaticItem iText;
        private DevExpress.XtraBars.BarToolbarsListItem iToolBars;        
        private DevExpress.XtraBars.BarButtonItem iExit;
        private DevExpress.XtraBars.BarButtonItem iAdd;
        private System.Windows.Forms.ImageList imageList2;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.BarButtonItem ipsWXP;
        private DevExpress.XtraBars.BarButtonItem ipsOXP;
        private DevExpress.XtraBars.BarButtonItem ipsO2K;
        private DevExpress.XtraBars.BarSubItem iPaintStyle;
        private ctrlFURLFavorites ctrlFavorites1;
        private DevExpress.XtraBars.BarButtonItem ipsO3;
        private DevExpress.XtraBars.BarButtonItem ipsDefault;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager ctrMdiMain;
    }
}
