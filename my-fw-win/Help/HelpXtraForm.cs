using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using System.Drawing;
using System.Data;
using System.IO;
using DevExpress.XtraBars;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp hỗ trợ làm việc trên Form
    /// </summary>
    public class HelpXtraForm: HelpProtocolForm
    {
        #region Các xử lý liên quan đến kích thước FORM.
        /// <summary>Gán kích thước W - H cho màn hình.
        /// Nếu kích thước màn hình nhỏ hơn sẽ lấy kích thước của màn hình
        /// </summary>        
        public static void SetSize(XtraForm form, int width, int height)
        {
            SetLargeSize(form, width, height);
        }

        [Obsolete("Sử dụng SetSize")]
        public static void SetLargeSize(XtraForm form, int width, int height)
        {
            Size screenSize = SystemInformation.PrimaryMonitorSize;
            form.Width = Math.Min(width, screenSize.Width);
            form.Height = Math.Min(height, screenSize.Height);
            //form.Size = new Size(width, height);
        }

        /// <summary>Gán 1 FORM thường thành 1 form có thể resize.
        /// </summary>
        public static void SetResize(XtraForm form)
        {
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MinimumSize = form.Size;
            form.MinimizeBox = false;
            form.MaximizeBox = true;
            form.SizeGripStyle = SizeGripStyle.Show;
            form.ControlBox = true;
        }

        /// <summary>Gán 1 FORM thường thành 1 form có thể fix.
        /// </summary>
        public static void SetFix(XtraForm form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.SizeGripStyle = SizeGripStyle.Hide;
            form.ControlBox = true;
        }
        #endregion

        #region Gán Form thành dạng Modal.
        /// <summary>Đặt các thuộc tính để màn hình bình thường trở thành
        /// màn hình dạng MODAL
        /// </summary>
        public static void SetModal(XtraForm mainForm, XtraForm form, 
            bool IsModal, bool IsInTaskbar, bool IsFixForm)
        {
            if (IsFixForm == true) SetFix(form);
            else SetResize(form);

            form.ShowInTaskbar = IsInTaskbar;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Icon = FrameworkParams.ApplicationIcon;
            form.Text = HelpApplication.getTitleForm(form.Text);
            if (IsModal)
            {
                if (mainForm != null)
                {
                    if (mainForm.IsDisposed == false)
                    {
                        form.ShowDialog(mainForm);
                    }
                    else
                    {
                        form.Tag = "NO";
                    }
                }
                else form.Show();
            }
            else
            {
                if (mainForm != null) form.Owner = mainForm;
                form.Show();
            }
        }
        
        /// <summary>Đặt các thuộc tính để màn hình bình thường trở thành
        /// màn hình dạng MODAL
        /// </summary>
        public static void SetModal(XtraForm mainForm, XtraForm form, bool IsModal)
        {
            SetModal(mainForm, form, IsModal, false, (form is IFormFixSize));
            #region Không sử dụng
            //if(form is IFormFixSize) SetFix(form);
            //else SetResize(form);

            //form.ShowInTaskbar = false;
            //form.StartPosition = FormStartPosition.CenterScreen;
            //form.Icon = FrameworkParams.ApplicationIcon;
            //form.Text = HelpApplication.getTitleForm(form.Text);
            //if (IsModal)
            //{
            //    if (mainForm != null) {
            //        if (mainForm.IsDisposed == false)
            //        {
            //            form.ShowDialog(mainForm);
            //        }
            //        else
            //        {
            //            form.Tag = "NO";
            //        }
            //    }
            //    else form.Show();
            //}
            //else
            //{
            //    if (mainForm != null) form.Owner = mainForm;
            //    form.Show();
            //}
            #endregion
        }
        #endregion

        #region Đóng form
        private static void CloseForm(XtraForm frm, bool? isAdd, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (frm.Tag != null && frm.Tag.Equals("Q")) { e.Cancel = false; return; }
            if (isAdd == null) { e.Cancel = false; return; }
            if (PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn đóng?") == DialogResult.Yes) e.Cancel = false;
            else e.Cancel = true;
        }
        private static void CloseForm(XtraForm frm, bool? skipInfo)
        {
            if (skipInfo == true) frm.Tag = "Q";
            frm.Close();
        }
        /// <summary>Gắn xự kiện vào nút đóng form.
        /// Phát sinh: Sử dụng trong các phiếu.
        /// </summary>
        public static void SetCloseForm(XtraForm frm, SimpleButton btn, bool? IsAdd)
        {
            frm.FormClosing += delegate(object abc, System.Windows.Forms.FormClosingEventArgs eabc)
            {
                CloseForm(frm, IsAdd, eabc);
            };
            if (btn != null)
            {
                btn.Click += delegate(Object abc, System.EventArgs sea)
                {
                    if (btn.Disposing == false)
                    {
                        frm.Close();
                    }

                };
            }
        }
        /// <summary>Đóng màn hình có confirm. Phải gọi SetCloseForm trước.
        /// Phát sinh: Dùng trong tất cả các phiếu.
        /// </summary>
        public static void CloseFormHasConfirm(XtraForm frm)
        {
            HelpXtraForm.CloseForm(frm, false);
        }
        /// <summary>Đóng màn hình không confirm. Phải gọi SetCloseForm trước.
        /// Phát sinh: Dùng trong tất cả các phiếu.
        /// </summary>
        public static void CloseFormNoConfirm(XtraForm frm)
        {
            HelpXtraForm.CloseForm(frm, true);
        }

        /// <summary>Đóng màn hình khi nạp màn hình bị lỗi.
        /// Phát sinh: Dùng trong tất cả các phiếu. Ví dụ khi chọn xem 1 phiếu trong khi đó 
        /// có 1 người đã vừa mới xóa thông tin phiếu
        /// </summary>
        /// <param name="frm"></param>
        public static void CloseFormWhenLoadError(XtraForm frm)
        {
            HelpMsgBox.ShowNotificationMessage("Chức năng này không hoạt động.\nVui lòng liên hệ Công ty P R O T O C O L.");
            frm.Hide();
            frm.Dispose();
        }
        #endregion


        #region Kiểm tra các hàm chưa dùng này.
        #region Chưa triển khai dùng
        public static void SetSaveLayout(XtraForm form)
        {
            PLFormLayout.SetSaveLayout(form);
        }
        #endregion
        #region Xóa vì ko sử dụng
        public static void SetCenterLocation(XtraForm form)
        {
            Size screenSize = SystemInformation.PrimaryMonitorSize;
            int x = (screenSize.Width - form.Width) / 2;
            int y = (screenSize.Height - form.Height) / 2;
            form.Location = new Point(x, y);
        }
        public static void SetLocation(XtraForm form, int x, int y)
        {
            Size screenSize = SystemInformation.PrimaryMonitorSize;
            x = ((Math.Max(0, x) + form.Width) > screenSize.Width) ? 0 : (Math.Max(0, x));
            y = ((form.Height + y) > screenSize.Height) ? 0 : y;
            form.Location = new Point(x, y);
        }
        #endregion
        #endregion

        [Obsolete("Hàm không dùng, chỉ dùng cho PLAbout để tạo hiệu ứng sáng dần")]
        public static void SetSlideEffect(XtraForm form)
        {
            try
            {
                if (form.IsDisposed == true) return;
                lock (form)
                {
                    Timer time = new Timer();
                    time.Tick += new System.EventHandler(delegate(object sender, EventArgs e)
                    {
                        if (form.Opacity == 1)
                        {
                            time.Stop();
                            time.Dispose();
                        }
                        time.Interval -= 1;
                        form.Opacity += 0.05;
                    });
                    form.Load += new System.EventHandler(delegate(object sender, EventArgs e)
                    {
                        try
                        {
                            form.Opacity = 0;
                            time.Start();
                            time.Interval = 30;
                        }
                        catch (Exception ex) { PLException.AddException(ex); }
                    });
                }
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }
        [Obsolete("Sử dụng CloseFormWhenLoadError")]
        public static void HuyForm(XtraForm form)
        {
            CloseFormWhenLoadError(form);
        }
        [Obsolete("Sử dụng HelpProtocolForm")]
        public static XtraForm CreateInstance(String FormName, Int64 ID)
        {
            return HelpObject.CreateXtraFormInstance(FormName, ID);
        }
        [Obsolete("Sử dụng HelpProtocolForm")]
        public static XtraForm CreateInstance(String FormName, object ID)
        {
            return HelpObject.CreateXtraFormInstance(FormName, ID);
        }
        [Obsolete("Sử dụng HelpProtocolForm")]
        public static XtraForm CreateInstance(String FormName, List<Object> InitParams)
        {
            return HelpObject.CreateXtraFormInstance(FormName, InitParams);
        }

        [Obsolete("Sử dụng HelpProtocolForm")]
        public static void ShowUserDialog(XtraForm mainform, XtraForm form)
        {
            HelpProtocolForm.ShowUserDialog(mainform, form);
        }
        [Obsolete("Sử dụng HelpProtocolForm")]
        public static void ShowUserModalDialog(XtraForm mainform, XtraForm form)
        {
            HelpProtocolForm.ShowUserModalDialog(mainform, form);
        }

        [Obsolete("Sử dụng HelpProtocolForm")]
        public static void ShowTimerDialog(XtraForm mainform, XtraForm form, int time)//10ms
        {
            HelpProtocolForm.ShowTimerDialog(mainform, form, time);
        }
        [Obsolete("Sử dụng HelpProtocolForm")]
        public static void ShowModalTimerDialog(XtraForm mainform, XtraForm form, int time)//10ms
        {
            HelpProtocolForm.ShowModalTimerDialog(mainform, form, time);            
        }

        #region Kiểm soát các chức năng đã làm dựa vào title
        [Obsolete("Không sử dụng")]
        public static void SetStatusForm(Form frm, FormStatus status)
        {
            PMSupport.SetTitle(frm, status);
        }
        [Obsolete("Không sử dụng")]
        public static String GetStatusFormTitle(String title, FormStatus status)
        {
            return PMSupport.UpdateTitle(title, status);
        }
        #endregion

        #region HelpRightClickForm - ĐÃ DÙNG GIẢI PHÁP RIGHT CLICK TITLE ĐỂ THAY THẾ.
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
                catch { }
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
            if (frm.IsRefresh())
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
    /// <summary>
    /// Interface dung de dinh nghi 1 form có hỗ trợ CLICK PHẢI.
    /// </summary>
    [Obsolete("Đã dùng giải pháp Right Click trên Title bar để thay thế rồi")]
    public interface IRightClickForm
    {
        /// <summary>
        /// Thực hiện chức năng Refresh
        /// </summary>
        void _Refresh();
        /// <summary>
        /// Có hay không hỗ trợ Refresh
        /// </summary>
        bool IsRefresh();

        /// <summary>
        /// Thực hiện Reset trở về ban đầu
        /// </summary>
        void _Reset();

        /// <summary>
        /// Có / Không hỗ trợ Reset
        /// </summary>
        /// <returns></returns>
        bool IsReset();

        /// <summary>
        /// Trả về địa chỉ FURL để có thể được mở trực tiếp từ FURL Browser.
        /// </summary>        
        string FURL();
    }
}
