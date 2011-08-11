using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Interface dung de dinh nghi 1 form có hỗ trợ CLICK PHẢI.
    /// </summary>
    public interface IRightClickForm
    {
        void _Refresh();
        bool IsRefresh();
        void _Reset();
        bool IsReset();
        string FURL();
    }

    //PHUOCNC
    // -    Có một số form dùng các control che form nên mình ko dùng Click 
    //      phải được tương lai sẽ dùng 1 phím tắt để hiện thị
    public class HelpRightClickForm
    {


        #region HelpRightClickForm 
        /// <summary>Gắn menu click phải bao gồm các chức năng lấy FURL, RESET, REFRESH
        /// Có thể mở rộng bằng cách thêm vào BarManager.
        /// </summary>
        /// <param name="form">The form.</param>
        public static BarManager PopupRightClickForm(XtraForm form)
        {
            if (form is IRightClickForm)
            {
                BarManager barMan = InitPopupMenu(form);
                form.MouseClick += new MouseEventHandler(form_MouseClick);
                return barMan;
            }
            else
                return null;
        }

        /// <summary>
        /// Handles the MouseClick event of the form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private static void form_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                try
                {
                    PopupMenu popupMenu = (PopupMenu)TagPropertyMan.Get(((XtraForm)sender).Tag, "PLRightClick");                   
                    popupMenu.ShowPopup(Control.MousePosition);
                }
                catch{}
            }
        }

        /// <summary>
        /// Inits the popup menu.
        /// </summary>
        private static BarManager InitPopupMenu(XtraForm form)
        {
            IRightClickForm frm = (IRightClickForm)form;

            //Init Manager for PopupMenu
            BarManager barManager = new BarManager();
            barManager.Form = form;           
            barManager.UseAltKeyForMenu = true;
            
            //Init PopupMenu
            PopupMenu popupMenu = new PopupMenu();            
            popupMenu.Manager = barManager;

            //Init Refresh item
            BarButtonItem refresh_item = new BarButtonItem();
            refresh_item.Caption = "&Làm tươi";             
            refresh_item.ItemClick += new ItemClickEventHandler(refresh_item_ItemClick);
            if(frm.IsRefresh())
                popupMenu.ItemLinks.Add(refresh_item);

            //Init Reset item
            BarButtonItem reset_item = new BarButtonItem();
            reset_item.Caption = "&Nhập mới";
            reset_item.ItemClick += new ItemClickEventHandler(reset_item_ItemClick);
            if (frm.IsReset())
                popupMenu.ItemLinks.Add(reset_item);

            //Init Copy_FURL item
            BarButtonItem furl_item = new BarButtonItem();
            furl_item.Caption = "Lấy &FURL";
            furl_item.ItemClick += new ItemClickEventHandler(furl_item_ItemClick);
            if (frm.FURL() != "")
                popupMenu.ItemLinks.Add(furl_item);
            
            object obj = form.Tag;
            TagPropertyMan.InsertOrUpdate(ref obj, "PLRightClick", popupMenu);
            form.Tag = obj;
               
            //////////////////////////////////////////////////////////////////////////
            //DUYNC
            //      Cách này chưa là giải pháp tối ưu
            //      Chỉ giải quyết được trường hợp form bị PanelControl che phủ
            //      Các trường hợp còn lại chưa thể giải quyết
            foreach (Control panel in form.Controls)
                if (panel is PanelControl)
                {
                    obj = panel.Tag;
                    TagPropertyMan.InsertOrUpdate(ref obj, "PLRightClick", popupMenu);
                    panel.Tag = obj;
                    panel.MouseClick += new MouseEventHandler(panel_MouseClick);
                }
            //////////////////////////////////////////////////////////////////////////

            return barManager;
        }

        /// <summary>
        /// Handles the MouseClick event of the panel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private static void panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                try
                {
                    PopupMenu popupMenu = (PopupMenu)TagPropertyMan.Get(((PanelControl)sender).Tag, "PLRightClick");
                    popupMenu.ShowPopup(Control.MousePosition);
                }
                catch { }
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the furl_item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private static void furl_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            IRightClickForm furl = (IRightClickForm)(((BarManager)sender).Form);
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(furl.FURL());
            }
            catch { }
        }

        /// <summary>
        /// Handles the ItemClick event of the reset_item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private static void reset_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            IRightClickForm reset = (IRightClickForm)(((BarManager)sender).Form);
            reset._Reset();
        }

        /// <summary>
        /// Handles the ItemClick event of the refresh_item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private static void refresh_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            IRightClickForm refresh = (IRightClickForm)(((BarManager)sender).Form);
            refresh._Refresh();
        }
        #endregion
    }
}
