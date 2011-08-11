using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Data;
using ProtocolVN.Framework.Core;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public class HomePageMenu
    {
        private static BarButtonItem AddHomePageItem = null;
        private static BarButtonItem RemoveHomePageItem = null;
        private static List<int> HomePageIDItems = new List<int>();

        private static List<int> LoadItemIds()
        {
            List<int> ret = new List<int>();
            string path = "";
            try
            {
                path = FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + @"HomePage.xml";
                if (!File.Exists(path))
                {
                    StreamWriter writer = new StreamWriter(path);
                    writer.Write("<?xml version='1.0' encoding='utf-8' ?><Item><Value></Value></Item>");
                    writer.Flush();
                    writer.Close();
                }
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                string[] ItemStr = ds.Tables[0].Rows[0]["Value"].ToString().Split(',');
                for (int i = 0; i < ItemStr.Length - 1; i++)
                {
                    if (HelpNumber.ParseInt32(ItemStr[i].Trim()) != Int32.MinValue)
                    {
                        ret.Add(HelpNumber.ParseInt32(ItemStr[i].Trim()));
                    }
                }
            }
            catch {
                try {
                    if (File.Exists(path)) File.Delete(path);
                }
                catch { }
            }
            return ret;
        }
        public static void SaveItemIds()
        {
            SaveItemIds(HomePageIDItems);
        }

        private static void SaveItemIds(List<int> ItemIds)
        {
            try
            {
                string path = FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + @"HomePage.xml";
                if (!File.Exists(path))
                {
                    StreamWriter writer = new StreamWriter(path);
                    writer.Write("<?xml version='1.0' encoding='utf-8' ?><Item><Value></Value></Item>");
                    writer.Flush();
                    writer.Close();
                }
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                string ItemIDStr = "";
                for (int i = 0; i < ItemIds.Count; i++)
                    ItemIDStr += ItemIds[i] + ",";
                ds.Tables[0].Rows[0]["Value"] = ItemIDStr;
                ds.WriteXml(path);
            }
            catch { }
        }

        public static void UsingHomePage(RibbonControl RibbonCtrl)
        {
            HomePageIDItems = LoadItemIds();
            UpdateHomePage();
            //if (RemoveHomePageItem == null)
            {
                RemoveHomePageItem = new BarButtonItem();
                RemoveHomePageItem.Id = frmRibbonMain.IIII++;
                RemoveHomePageItem.Name = "REMOVE_HOMEPAGE";
                RemoveHomePageItem.Caption = "Loại bỏ khỏi trang Thường dùng";
                RemoveHomePageItem.ItemClick += new ItemClickEventHandler(RemoveHomePageItem_ItemClick);
            }

            //if (AddHomePageItem == null)
            {
                AddHomePageItem = new BarButtonItem();
                AddHomePageItem.Id = frmRibbonMain.IIII++;
                AddHomePageItem.Name = "ADD_HOMEPAGE";
                AddHomePageItem.Caption = "Thêm vào trang Thường dùng";
                AddHomePageItem.ItemClick += new ItemClickEventHandler(AddHomePageItem_ItemClick);
            }
            try { RibbonCtrl.ShowCustomizationMenu -= RibbonCtrl_ShowCustomizationMenu; }
            catch { }
            RibbonCtrl.ShowCustomizationMenu += new RibbonCustomizationMenuEventHandler(RibbonCtrl_ShowCustomizationMenu);
        }

        #region Xử lý Trang đầu
        private static object selected = null;
        
        private static void RibbonCtrl_ShowCustomizationMenu(object sender, RibbonCustomizationMenuEventArgs e)
        {
            RibbonPage HomePage = ((RibbonForm)FrameworkParams.MainForm).Ribbon.Pages[0];
            try
            {
                e.CustomizationMenu.ItemLinks.Remove(AddHomePageItem.Links[0]);
            }
            catch { }
            try
            {
                e.CustomizationMenu.ItemLinks.Remove(RemoveHomePageItem.Links[0]);
            }
            catch { }
            
            for (int i = 0; i < e.CustomizationMenu.ItemLinks.Count; i++)
            {
                BarItemLink buts = e.CustomizationMenu.ItemLinks[i];
                if (buts.Visible == false) continue;

                if (buts.Item.Caption.IndexOf("Loại bỏ") >= 0)
                {
                    selected = buts.Item.Tag;
                    break;
                }
                else if (buts.Item.Caption.IndexOf("Thêm vào") >= 0)
                {
                    selected = buts.Item.Tag;
                    break;
                }
            }

            if (selected != null)
            {
                BarItemLink item = (BarItemLink)selected;
                if (((RibbonForm)FrameworkParams.MainForm).Ribbon.SelectedPage.Equals(HomePage))
                {
                    if(HomePageIDItems.Contains(item.ItemId))
                        e.CustomizationMenu.AddItem(RemoveHomePageItem);
                }
                else
                {
                    if (!HomePageIDItems.Contains(item.ItemId))
                        e.CustomizationMenu.AddItem(AddHomePageItem);
                    else
                        e.CustomizationMenu.AddItem(RemoveHomePageItem);
                }
            }
        }

        private static void AddHomePageItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (selected != null)
                {
                    RibbonPage HomePage = ((RibbonForm)FrameworkParams.MainForm).Ribbon.Pages[0];
                    BarItemLink item = (BarItemLink)selected;
                    if (item.ItemId != -1 && !HomePageIDItems.Contains(item.ItemId))
                    {
                        HomePageIDItems.Add(item.ItemId);
                        if (FrameworkParams.UsingGallerySkins)
                            HomePage.Groups[1].ItemLinks.Add(item.Item);
                        else
                            HomePage.Groups[0].ItemLinks.Add(item.Item);
                        ((RibbonForm)FrameworkParams.MainForm).Ribbon.Update();
                    }
                    selected = null;
                }
            }
            catch { }
        }
        private static void RemoveHomePageItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (selected != null)
                {
                    RibbonPage HomePage = ((RibbonForm)FrameworkParams.MainForm).Ribbon.Pages[0];
                    BarItemLink item = (BarItemLink)selected;

                    HomePageIDItems.Remove(item.ItemId);
                    if( FrameworkParams.UsingGallerySkins)
                        HomePage.Groups[1].ItemLinks.Remove(item.Item);
                    else
                        HomePage.Groups[0].ItemLinks.Remove(item.Item);
                    ((RibbonForm)FrameworkParams.MainForm).Ribbon.Update();

                    selected = null;
                }
            }
            catch { }
        }
        #endregion

        private static BarItemLink FindBarItemLink(BarSubItemLink barSub, int ItemId)
        {
            if (barSub.ItemId == ItemId) return barSub;
            BarSubItem itemTemp = (BarSubItem)barSub.Item;
            
            foreach (BarItemLink subItem in itemTemp.ItemLinks)
            {
                if (subItem is BarSubItemLink)
                {
                    BarItemLink ret = FindBarItemLink((BarSubItemLink)subItem, ItemId);
                    if(ret!=null) { return ret; }
                }
                else if (subItem is BarButtonItemLink){
                    if (subItem.ItemId == ItemId) return subItem;
                }
            }
            return null;
        }
        private static BarItemLink FindItem(int id)
        {
            RibbonControl ribbonControl = ((RibbonForm)FrameworkParams.MainForm).Ribbon;
            //Không tìm trong HomePage
            for(int k = 1; k < ribbonControl.Pages.Count; k++)
            {
                RibbonPage page = ribbonControl.Pages[k];

                for (int i = 0; i < page.Groups.Count; i++)
                {
                    foreach (BarItemLink item in page.Groups[i].ItemLinks)
                    {
                        if (item is BarSubItemLink)
                        {
                            BarItemLink ret = FindBarItemLink((BarSubItemLink)item, id);
                            if (ret != null) return ret;
                        }
                        else if ( item is BarButtonItemLink)
                        {
                            if (item.ItemId == id) return item;
                        }
                    }
                }
            }
            return null;
        }
        private static void UpdateHomePage()
        {
            if (((RibbonForm)FrameworkParams.MainForm).Ribbon.Pages.Count > 0)
            {
                RibbonPage HomePage = ((RibbonForm)FrameworkParams.MainForm).Ribbon.Pages[0];
                if (HomePageIDItems.Count > 0)
                {
                    if (FrameworkParams.UsingGallerySkins)
                        HomePage.Groups[1].ItemLinks.Clear();
                    else
                        HomePage.Groups[0].ItemLinks.Clear();

                    for (int i = 0; i < HomePageIDItems.Count; i++)
                    {
                        BarItemLink item = FindItem(HomePageIDItems[i]);
                        if (item != null)
                        {
                            if (FrameworkParams.UsingGallerySkins)
                                HomePage.Groups[1].ItemLinks.Add(item.Item);
                            else
                                HomePage.Groups[0].ItemLinks.Add(item.Item);
                        }
                    }
                    ((RibbonForm)FrameworkParams.MainForm).Ribbon.Update();
                }
                else
                {
                    if (FrameworkParams.UsingGallerySkins)
                    {
                        foreach (BarItemLink item in HomePage.Groups[1].ItemLinks)
                        {
                            if (item is BarButtonItemLink)
                            {
                                HomePageIDItems.Add(item.ItemId);
                            }
                        }
                    }
                    else
                    {
                        foreach (BarItemLink item in HomePage.Groups[0].ItemLinks)
                        {
                            if (item is BarButtonItemLink)
                            {
                                HomePageIDItems.Add(item.ItemId);
                            }
                        }
                    }
                }
            }
        }        
    }
}