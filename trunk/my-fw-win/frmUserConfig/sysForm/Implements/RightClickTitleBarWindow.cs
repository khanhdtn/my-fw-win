using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;

namespace ProtocolVN.Framework.Win
{
    public class RightClickTitleBarWindow
    {
        private frmRibbonMain form;
        private PopupMenu popupMenu;
        public XtraMdiTabPage tagPage;


        public List<BarItemLink> itemsForTabPage = null;
        public List<BarItem> ctrlItemsForTabPage = null;

        public RightClickTitleBarWindow(frmRibbonMain form, PopupMenu popupMenu, XtraMdiTabPage tagPage)
        {
            this.form = form;
            this.popupMenu = popupMenu;
            this.tagPage = tagPage;

            itemsForTabPage = new List<BarItemLink>();
            ctrlItemsForTabPage = new List<BarItem>();

            if (tagPage != null && tagPage.MdiChild != null)
            {
                Form selectedForm = tagPage.MdiChild;

                if (selectedForm is IFormRefresh)
                {
                    BarButtonItem refresh_item = new BarButtonItem();
                    refresh_item.Caption = RightClickTitleBarDialog.MENU_TITLE_FORM_REFRESH_TEXT;
                    refresh_item.Glyph = FWImageDic.REFRESH_IMAGE16;
                    refresh_item.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                    refresh_item.ItemClick += new ItemClickEventHandler(refresh_item_ItemClick);

                    itemsForTabPage.Add(popupMenu.ItemLinks.Add(refresh_item));
                    ctrlItemsForTabPage.Add(refresh_item);
                }
                if (selectedForm is IFormFURL)
                {
                    BarButtonItem furl_item = new BarButtonItem();
                    furl_item.Caption = RightClickTitleBarDialog.MENU_TITLE_FORM_FURL_TEXT;
                    furl_item.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                    furl_item.Glyph = FWImageDic.INFO_IMAGE16;
                    furl_item.ItemClick += new ItemClickEventHandler(furl_item_ItemClick);

                    itemsForTabPage.Add(popupMenu.ItemLinks.Add(furl_item));
                    ctrlItemsForTabPage.Add(furl_item);
                }
            }
        }

        void refresh_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = form.xtraTabbedMdiManager1.SelectedPage;
            if (page != null)
            {
                if (page.MdiChild is IFormRefresh)
                {
                    IFormRefresh frm = (IFormRefresh)page.MdiChild;
                    RightClickTitleBarHelper.refreshForm(frm);
                }
            }
        }

        void furl_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraMdiTabPage page = form.xtraTabbedMdiManager1.SelectedPage;
            if (page != null)
            {
                if (page.MdiChild is IFormFURL)
                {
                    IFormFURL frm = (IFormFURL)page.MdiChild;
                    RightClickTitleBarHelper.showFURL(frm);
                }
            }
        }

        public void deleteAllItem(BarManager barManager)
        {
            //Các Item dùng riêng cho từng TabPage.
            if (itemsForTabPage != null)
            {
                foreach (BarItemLink item in itemsForTabPage)
                {
                    popupMenu.RemoveLink(item);
                }
            }

            if (ctrlItemsForTabPage != null)
            {
                foreach (BarItem item in ctrlItemsForTabPage)
                {
                    barManager.Items.Remove(item);
                }
            }
        }
    }
}
