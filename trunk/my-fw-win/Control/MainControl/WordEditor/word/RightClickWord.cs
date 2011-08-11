using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Drawing;
using Microsoft.Office.Interop.Word;
using DevExpress.XtraEditors;


namespace ProtocolVN.Framework.Win
{     
    //PHUOCNC: Hiện tại mới hỗ trợ .Doc 
    //Mở rộng HTML
    //Mở rộng PDF
    public class RightClickWord
    {
        public static RightClickWord SupportRightClickWord(RichTextBox Rich)
        {
            RightClickWord wrapper = new RightClickWord(Rich);
            wrapper._init();
            return wrapper;
        }

        #region Các biến quan trọng sử dụng trong class
        private RichTextBox richTextBox;
        private PopupMenu popupMenu;

        public BarButtonItem import_item = new BarButtonItem();
        public BarButtonItem plword_item = new BarButtonItem();
        #endregion

        #region Các Setter, Getter
        /// <summary>
        /// Gets or sets the rich text box.
        /// </summary>
        /// <value>The rich text box.</value>
        public RichTextBox RichTextBox
        {
            get { return richTextBox; }
            set { richTextBox = value; }
        }
        #endregion

        #region Các Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PLRichTextBoxExt"/> class.
        /// </summary>
        /// <param name="richTextBox">The rich text box.</param>
        public RightClickWord(RichTextBox richTextBox)
        {
            this.RichTextBox = richTextBox;
        }
        #endregion

        #region Các hàm khởi tạo
        /// <summary>
        /// Inits this instance.
        /// </summary>
        public void _init()
        {            
            InitPopupMenu();
            InitEvent();
        }

        /// <summary>
        /// Inits the popup menu.
        /// </summary>
        private void InitPopupMenu()
        {
            //Init Manager for PopupMenu
            BarManager barManager = new BarManager();
            barManager.Form = this.RichTextBox.Parent;

            this.popupMenu = new PopupMenu();
            this.popupMenu.Manager = barManager;
            
            //Parent item
            //BarButtonItem import_item = new BarButtonItem();
            import_item.Caption = "Từ tập tin";
            import_item.ItemClick += new ItemClickEventHandler(fromword_item_ItemClick);
            this.popupMenu.ItemLinks.Add(import_item);
            barManager.Items.Add(import_item);

            //Mở PLWord
            //BarButtonItem plword_item = new BarButtonItem();
            plword_item.Caption = "Soạn thảo";
            plword_item.ItemClick += new ItemClickEventHandler(plword_item_ItemClick);
            this.popupMenu.ItemLinks.Add(plword_item);
            barManager.Items.Add(plword_item);

            //TO HTML

            //PHUOCNT NC Nạp từ mẫu thiết kế Tham khảo PLWord
            //...
        }

        void plword_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmPLWord w = new frmPLWord(this.richTextBox);
            Form frm = this.richTextBox.FindForm();
            if(frm!=null && frm is XtraForm)
                ProtocolForm.ShowModalDialog((XtraForm)frm, w);
            else
                ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, w);
        }

        /// <summary>
        /// Inits the event.
        /// </summary>
        private void InitEvent()
        {
            this.RichTextBox.MouseUp += new MouseEventHandler(RichTextBox_MouseUp);
        }                
        #endregion

        #region Các hàm xử lý sự kiện
        /// <summary>
        /// Handles the MouseUp event of the RichTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        void RichTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0 && RichTextBox.ClientRectangle.Contains(e.X, e.Y))
            {
                this.popupMenu.ShowPopup(Control.MousePosition);
            }
            else
            {
                if(this.popupMenu!=null) this.popupMenu.HidePopup();
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the fromword_item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        void fromword_item_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word document(*.doc;*.docx)|*.doc;*.docx";
                if (ofd.ShowDialog() == DialogResult.OK)
                    HelpRichTextBox.ImportFromWord(this.RichTextBox, ofd.FileName);
            }
        }
        #endregion
    }
}
