﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Diagnostics;

using System.IO;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class PLEditor : XtraUserControl
    {
        private IHTMLDocument2 doc;
        private bool updatingFontName = false;
        private bool updatingFontSize = false;
        private bool setup = false;
        private string state = "HTML";
        private List<string> lstImage;
        private List<string> lstImgName;
        private string pathTemp = FrameworkParams.TEMP_FOLDER + @"\WYSWYG";

        public delegate void TickDelegate();

        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public class EnterKeyEventArgs : EventArgs
        {
            private bool _cancel = false;

            public bool Cancel
            {
                get { return _cancel; }
                set { _cancel = value; }
            }

        }

        public event TickDelegate Tick;
        
        public event WebBrowserNavigatedEventHandler Navigated;

        public event EventHandler<EnterKeyEventArgs> EnterKeyEvent;

        public PLEditor()
        {
            InitializeComponent();
            SetupEvents();
            SetupTimer();
            SetupBrowser();
            SetupFontComboBox();
            SetupFontSizeComboBox();
            lstImage = new List<string>();
            lstImgName = new List<string>();
            CreateTempPath();
        }

        #region Chức năng
        /// <summary>
        /// Setup navigation and focus event handlers.
        /// </summary>
        private void SetupEvents()
        {
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            webBrowser1.GotFocus += new EventHandler(webBrowser1_GotFocus);
        }

        /// <summary>
        /// When this control receives focus, it transfers focus to the 
        /// document body.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void webBrowser1_GotFocus(object sender, EventArgs e)
        {
            SuperFocus();
        }

        /// <summary>
        /// This is called when the initial html/body framework is set up, 
        /// or when document.DocumentText is set.  At this point, the 
        /// document is editable.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">navigation args</param>
        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            SetBackgroundColor(BackColor);
            if (Navigated != null)
            {
                Navigated(this, e);
            }
        }

        /// <summary>
        /// Setup timer with 200ms interval
        /// </summary>
        private void SetupTimer()
        {
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        /// <summary>
        /// Add document body, turn on design mode on the whole document, 
        /// and overred the context menu
        /// </summary>
        private void SetupBrowser()
        {
            webBrowser1.DocumentText = "<html><body></body></html>";
            doc =
                webBrowser1.Document.DomDocument as IHTMLDocument2;
            doc.designMode = "On";
            webBrowser1.Document.ContextMenuShowing += 
                new HtmlElementEventHandler(Document_ContextMenuShowing);
        }

        /// <summary>
        /// Set the focus on the document body.  
        /// </summary>
        private void SuperFocus()
        {
            if (webBrowser1.Document != null &&
                webBrowser1.Document.Body != null)
                webBrowser1.Document.Body.Focus();
        }

        /// <summary>
        /// Get/Set the background color of the editor.
        /// Note that if this is called before the document is rendered and 
        /// complete, the navigated event handler will set the body's 
        /// background color based on the state of BackColor.
        /// </summary>
        [Browsable(true)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (ReadyState == ReadyState.Complete)
                {
                    SetBackgroundColor(value);
                }
            }
        }

        /// <summary>
        /// Set the background color of the body by setting it's CSS style
        /// </summary>
        /// <param name="value">the color to use for the background</param>
        private void SetBackgroundColor(Color value)
        {
            if (webBrowser1.Document != null &&
                webBrowser1.Document.Body != null)
                webBrowser1.Document.Body.Style =
                    string.Format("background-color: {0}", value.Name);
        }

        /// <summary>
        /// Clear the contents of the document, leaving the body intact.
        /// </summary>
        public void Clear()
        {
            if (webBrowser1.Document.Body != null)
                webBrowser1.Document.Body.InnerHtml = "";
        }

        /// <summary>
        /// Get the web browser component's document
        /// </summary>
        public HtmlDocument Document
        {
            get { return webBrowser1.Document; }
        }

        /// <summary>
        /// Document text should be used to load/save the entire document, 
        /// including html and body start/end tags.
        /// </summary>
        [Browsable(false)]
        public string DocumentText
        {
            get
            {
                return webBrowser1.DocumentText;
            }
            set
            {
                webBrowser1.DocumentText = value;
            }
        }

        /// <summary>
        /// Get the html document title from document.
        /// </summary>
        [Browsable(false)]
        public string DocumentTitle
        {
            get
            {
                return webBrowser1.DocumentTitle;
            }
        }

        /// <summary>
        /// Get/Set the contents of the document Body, in html.
        /// </summary>
        [Browsable(false)]
        public string BodyHtml
        {
            get
            {
                if (webBrowser1.Document != null &&
                    webBrowser1.Document.Body != null)
                {
                    return webBrowser1.Document.Body.InnerHtml;
                }
                else
                    return string.Empty;
            }
            set
            {
                if (webBrowser1.Document.Body != null)
                    webBrowser1.Document.Body.InnerHtml = value;
            }
        }

        /// <summary>
        /// Get/Set the documents body as text.
        /// </summary>
        [Browsable(false)]
        public string BodyText
        {
            get
            {
                if (webBrowser1.Document != null &&
                    webBrowser1.Document.Body != null)
                {
                    return webBrowser1.Document.Body.InnerText;
                }
                else
                    return string.Empty;
            }
            set
            {
                if (webBrowser1.Document.Body != null)
                    webBrowser1.Document.Body.InnerText = value;
            }
        }

        /// <summary>
        /// Determine the status of the Undo command in the document editor.
        /// </summary>
        /// <returns>whether or not an undo operation is currently valid</returns>
        public bool CanUndo()
        {
            try
            {
                return doc.queryCommandEnabled("Undo");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine the status of the Redo command in the document editor.
        /// </summary>
        /// <returns>whether or not a redo operation is currently valid</returns>
        public bool CanRedo()
        {
            try
            {
                return doc.queryCommandEnabled("Redo");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine the status of the Cut command in the document editor.
        /// </summary>
        /// <returns>whether or not a cut operation is currently valid</returns>
        public bool CanCut()
        {
            try
            {
                return doc.queryCommandEnabled("Cut");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine the status of the Copy command in the document editor.
        /// </summary>
        /// <returns>whether or not a copy operation is currently valid</returns>
        public bool CanCopy()
        {
            try
            {
                return doc.queryCommandEnabled("Copy");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine the status of the Paste command in the document editor.
        /// </summary>
        /// <returns>whether or not a copy operation is currently valid</returns>
        public bool CanPaste()
        {
            try
            {
                return doc.queryCommandEnabled("Paste");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine the status of the Delete command in the document editor.
        /// </summary>
        /// <returns>whether or not a copy operation is currently valid</returns>
        public bool CanDelete()
        {
            try
            {
                return doc.queryCommandEnabled("Delete");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current block is left justified.
        /// </summary>
        /// <returns>true if left justified, otherwise false</returns>
        public bool IsJustifyLeft()
        {
            try
            {
                return doc.queryCommandState("JustifyLeft");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current block is right justified.
        /// </summary>
        /// <returns>true if right justified, otherwise false</returns>
        public bool IsJustifyRight()
        {
            try
            {
                return doc.queryCommandState("JustifyRight");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current block is center justified.
        /// </summary>
        /// <returns>true if center justified, false otherwise</returns>
        public bool IsJustifyCenter()
        {
            try
            {
                return doc.queryCommandState("JustifyCenter");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current block is full justified.
        /// </summary>
        /// <returns>true if full justified, false otherwise</returns>
        public bool IsJustifyFull()
        {
            try
            {
                return doc.queryCommandState("JustifyFull");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current selection is in Bold mode.
        /// </summary>
        /// <returns>whether or not the current selection is Bold</returns>
        public bool IsBold()
        {
            try
            {
                return doc.queryCommandState("Bold");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current selection is in Italic mode.
        /// </summary>
        /// <returns>whether or not the current selection is Italicized</returns>
        public bool IsItalic()
        {
            try
            {
                return doc.queryCommandState("Italic");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current selection is in Underline mode.
        /// </summary>
        /// <returns>whether or not the current selection is Underlined</returns>
        public bool IsUnderline()
        {
            try
            {
                return doc.queryCommandState("Underline");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current paragraph is an ordered list.
        /// </summary>
        /// <returns>true if current paragraph is ordered, false otherwise</returns>
        public bool IsOrderedList()
        {
            try
            {
                return doc.queryCommandState("InsertOrderedList");
            }
            catch { return false; }
        }

        /// <summary>
        /// Determine whether the current paragraph is an unordered list.
        /// </summary>
        /// <returns>true if current paragraph is ordered, false otherwise</returns>
        public bool IsUnorderedList()
        {
            try
            {
                return doc.queryCommandState("InsertUnorderedList");
            }
            catch { return false; }
        }

        /// <summary>
        /// Called when the editor context menu should be displayed.
        /// The return value of the event is set to false to disable the 
        /// default context menu.  A custom context menu (contextMenuStrip1) is 
        /// shown instead.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">HtmlElementEventArgs</param>
        private void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            e.ReturnValue = false;
            cutToolStripMenuItem1.Enabled = CanCut();
            copyToolStripMenuItem2.Enabled = CanCopy();
            pasteToolStripMenuItem3.Enabled = CanPaste();
            deleteToolStripMenuItem.Enabled = CanDelete();
            contextMenuStrip1.Show(this, e.ClientMousePosition);
        }

        /// <summary>
        /// Populate the font size combobox.
        /// Add text changed and key press handlers to handle input and update 
        /// the editor selection font size.
        /// </summary>
        private void SetupFontSizeComboBox()
        {
            for (int x = 1; x <= 7; x++)
            {
                fontSizeComboBox.Items.Add(x.ToString());
            }
            fontSizeComboBox.TextChanged += new EventHandler(fontSizeComboBox_TextChanged);
            fontSizeComboBox.KeyPress += new KeyPressEventHandler(fontSizeComboBox_KeyPress);
        }

        /// <summary>
        /// Called when a key is pressed on the font size combo box.
        /// The font size in the boxy box is set to the key press value.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">KeyPressEventArgs</param>
        private void fontSizeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar <= '7' && e.KeyChar > '0')
                    fontSizeComboBox.Text = e.KeyChar.ToString();
            }
            else if (!Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Set editor's current selection to the value of the font size combo box.
        /// Ignore if the timer is currently updating the font size to synchronize 
        /// the font size combo box with the editor's current selection.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void fontSizeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (updatingFontSize) return;
            switch (fontSizeComboBox.Text.Trim())
            {
                case "1":
                    FontSize = FontSize.One;
                    break;
                case "2":
                    FontSize = FontSize.Two;
                    break;
                case "3":
                    FontSize = FontSize.Three;
                    break;
                case "4":
                    FontSize = FontSize.Four;
                    break;
                case "5":
                    FontSize = FontSize.Five;
                    break;
                case "6":
                    FontSize = FontSize.Six;
                    break;
                case "7":
                    FontSize = FontSize.Seven;
                    break;
                default:
                    FontSize = FontSize.Seven;
                    break;
            }
        }

        /// <summary>
        /// Populate the font combo box and autocomplete handlers.
        /// Add a text changed handler to the font combo box to handle new font selections.
        /// </summary>
        private void SetupFontComboBox()
        {
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            foreach (FontFamily fam in FontFamily.Families)
            {
                fontComboBox.Items.Add(fam.Name);
                ac.Add(fam.Name);
            }
            fontComboBox.Leave += new EventHandler(fontComboBox_TextChanged);
            fontComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            fontComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            fontComboBox.AutoCompleteCustomSource = ac;
        }

        /// <summary>
        /// Called when the font combo box has changed.
        /// Ignores the event when the timer is updating the font combo Box 
        /// to synchronize the editor selection with the font combo box.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void fontComboBox_TextChanged(object sender, EventArgs e)
        {
            if (updatingFontName) return;
            FontFamily ff;
            try
            {
                ff = new FontFamily(fontComboBox.Text);
            }
            catch (Exception)
            {
                updatingFontName = true;
                fontComboBox.Text = FontName.GetName(0);
                updatingFontName = false;
                return;
            }
            FontName = ff;
        }

        /// <summary>
        /// Called when the timer fires to synchronize the format buttons 
        /// with the text editor current selection.
        /// SetupKeyListener if necessary.
        /// Set bold, italic, underline and link buttons as based on editor state.
        /// Synchronize the font combo box and the font size combo box.
        /// Finally, fire the Tick event to allow external components to synchronize 
        /// their state with the editor.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            // don't process until browser is in ready state.
            if (ReadyState != ReadyState.Complete)
                return;

            SetupKeyListener();
            boldButton.Checked = IsBold();
            italicButton.Checked = IsItalic();
            underlineButton.Checked = IsUnderline();
            orderedListButton.Checked = IsOrderedList();
            unorderedListButton.Checked = IsUnorderedList();
            justifyLeftButton.Checked = IsJustifyLeft();
            justifyCenterButton.Checked = IsJustifyCenter();
            justifyRightButton.Checked = IsJustifyRight();
            justifyFullButton.Checked = IsJustifyFull();

            linkButton.Enabled = SelectionType == SelectionType.Text;

            UpdateFontComboBox();
            UpdateFontSizeComboBox();

            if (Tick != null)
                Tick();
        }

        /// <summary>
        /// Update the font size combo box.
        /// Sets a flag to indicate that the combo box is updating, and should 
        /// not update the editor's selection.
        /// </summary>
        private void UpdateFontSizeComboBox()
        {
            if (!fontSizeComboBox.Focused)
            {
                int foo;
                switch (FontSize)
                {
                    case FontSize.One:
                        foo = 1;
                        break;
                    case FontSize.Two:
                        foo = 2;
                        break;
                    case FontSize.Three:
                        foo = 3;
                        break;
                    case FontSize.Four:
                        foo = 4;
                        break;
                    case FontSize.Five:
                        foo = 5;
                        break;
                    case FontSize.Six:
                        foo = 6;
                        break;
                    case FontSize.Seven:
                        foo = 7;
                        break;
                    case FontSize.NA:
                        foo = 0;
                        break;
                    default:
                        foo = 7;
                        break;
                }
                string fontsize = Convert.ToString(foo);
                if (fontsize != fontSizeComboBox.Text)
                {
                    updatingFontSize = true;
                    fontSizeComboBox.Text = fontsize;
                    updatingFontSize = false;
                }
            }
        }

        /// <summary>
        /// Update the font combo box.
        /// Sets a flag to indicate that the combo box is updating, and should 
        /// not update the editor's selection.
        /// </summary>
        private void UpdateFontComboBox()
        {
            if (!fontComboBox.Focused)
            {
                FontFamily fam = FontName;
                if (fam != null)
                {
                    string fontname = fam.Name;
                    if (fontname != fontComboBox.Text)
                    {
                        updatingFontName = true;
                        fontComboBox.Text = fontname;
                        updatingFontName = false;
                    }
                }
            }
        }

        /// <summary>
        /// Set up a key listener on the body once.
        /// The key listener checks for specific key strokes and takes 
        /// special action in certain cases.
        /// </summary>
        private void SetupKeyListener()
        {
            if (!setup)
            {
                webBrowser1.Document.Body.KeyDown += new HtmlElementEventHandler(Body_KeyDown);
                setup = true;
            }
        }

        /// <summary>
        /// If the user hits the enter key, and event will fire (EnterKeyEvent), 
        /// and the consumers of this event can cancel the projecessing of the 
        /// enter key by cancelling the event.
        /// This is useful if your application would like to take some action 
        /// when the enter key is pressed, such as a submission to a web service.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">HtmlElementEventArgs</param>
        private void Body_KeyDown(object sender, HtmlElementEventArgs e)
        {
            if (e.KeyPressedCode == 13 && !e.ShiftKeyPressed)
            {
                // handle enter code cancellation
                bool cancel = false;
                if (EnterKeyEvent != null)
                {
                    EnterKeyEventArgs args = new EnterKeyEventArgs();
                    EnterKeyEvent(this, args);
                    cancel = args.Cancel;
                }
                e.ReturnValue = !cancel;
            }
        }

        
        /// <summary>
        /// Embed a break at the current selection.
        /// This is a placeholder for future functionality.
        /// </summary>
        public void EmbedBr()
        {
            IHTMLTxtRange range =
                doc.selection.createRange() as IHTMLTxtRange;
            range.pasteHTML("<br/>");
            range.collapse(false);
            range.select();
        }

        /// <summary>
        /// Paste the clipboard text into the current selection.
        /// This is a placeholder for future functionality.
        /// </summary>
        private void SuperPaste()
        {
            if (Clipboard.ContainsText())
            {
                IHTMLTxtRange range =
                    doc.selection.createRange() as IHTMLTxtRange;
                range.pasteHTML(Clipboard.GetText(TextDataFormat.Text));
                range.collapse(false);
                range.select();
            }
        }

        /// <summary>
        /// Print the current document
        /// </summary>
        public void Print()
        {
            webBrowser1.Document.ExecCommand("Print", true, null);
        }

        /// <summary>
        /// Insert a paragraph break
        /// </summary>
        public void InsertParagraph()
        {
            webBrowser1.Document.ExecCommand("InsertParagraph", false, null);
        }

        /// <summary>
        /// Insert a horizontal rule
        /// </summary>
        public void InsertBreak()
        {
            webBrowser1.Document.ExecCommand("InsertHorizontalRule", false, null);
        }

        /// <summary>
        /// Select all text in the document.
        /// </summary>
        public void SelectAll()
        {
            webBrowser1.Document.ExecCommand("SelectAll", false, null);
        }

        /// <summary>
        /// Undo the last operation
        /// </summary>
        public void Undo()
        {
            webBrowser1.Document.ExecCommand("Undo", false, null);
        }

        /// <summary>
        /// Redo based on the last Undo
        /// </summary>
        public void Redo()
        {
            webBrowser1.Document.ExecCommand("Redo", false, null);
        }

        /// <summary>
        /// Cut the current selection and place it in the clipboard.
        /// </summary>
        public void Cut()
        {
            webBrowser1.Document.ExecCommand("Cut", false, null);
        }

        /// <summary>
        /// Paste the contents of the clipboard into the current selection.
        /// </summary>
        public void Paste()
        {
            webBrowser1.Document.ExecCommand("Paste", false, null);
        }

        /// <summary>
        /// Copy the current selection into the clipboard.
        /// </summary>
        public void Copy()
        {
            webBrowser1.Document.ExecCommand("Copy", false, null);
        }

        /// <summary>
        /// Toggle the ordered list property for the current paragraph.
        /// </summary>
        public void OrderedList()
        {
            webBrowser1.Document.ExecCommand("InsertOrderedList", false, null);
        }

        /// <summary>
        /// Toggle the unordered list property for the current paragraph.
        /// </summary>
        public void UnorderedList()
        {
            webBrowser1.Document.ExecCommand("InsertUnorderedList", false, null);
        }

        /// <summary>
        /// Toggle the left justify property for the currnet block.
        /// </summary>
        public void JustifyLeft()
        {
            webBrowser1.Document.ExecCommand("JustifyLeft", false, null);
        }

        /// <summary>
        /// Toggle the right justify property for the current block.
        /// </summary>
        public void JustifyRight()
        {
            webBrowser1.Document.ExecCommand("JustifyRight", false, null);
        }

        /// <summary>
        /// Toggle the center justify property for the current block.
        /// </summary>
        public void JustifyCenter()
        {
            webBrowser1.Document.ExecCommand("JustifyCenter", false, null);
        }

        /// <summary>
        /// Toggle the full justify property for the current block.
        /// </summary>
        public void JustifyFull()
        {
            webBrowser1.Document.ExecCommand("JustifyFull", false, null);
        }

        /// <summary>
        /// Toggle bold formatting on the current selection.
        /// </summary>
        public void Bold()
        {
            webBrowser1.Document.ExecCommand("Bold", false, null);
        }

        /// <summary>
        /// Toggle italic formatting on the current selection.
        /// </summary>
        public void Italic()
        {
            webBrowser1.Document.ExecCommand("Italic", false, null);
        }

        /// <summary>
        /// Toggle underline formatting on the current selection.
        /// </summary>
        public void Underline()
        {
            webBrowser1.Document.ExecCommand("Underline", false, null);
        }

        /// <summary>
        /// Delete the current selection.
        /// </summary>
        public void Delete()
        {
            webBrowser1.Document.ExecCommand("Delete", false, null);
        }

        /// <summary>
        /// Insert an imange.
        /// </summary>
        #endregion
        public void InsertImage()
        {
            webBrowser1.Document.ExecCommand("InsertImage", true, null);
        }

        //Tạo đường dẫn file tạm chứa hình
        private void CreateTempPath()
        {
            if (!Directory.Exists(pathTemp))
                Directory.CreateDirectory(pathTemp);
        }
        private  void ReadImage(string html)
        {
            string comment = html.Substring(html.LastIndexOf("<!--") + 4, html.LastIndexOf("-->") - html.LastIndexOf("<!--") - 4);
            string[] image = comment.Split(new string[] { "<image>" },StringSplitOptions.RemoveEmptyEntries);
            lstImage.Clear();
            lstImgName.Clear();
            for (int i = 0; i < image.Length; i++)
            {
                string[] fileImage = image[i].Split(new string[] { "<|>" }, StringSplitOptions.RemoveEmptyEntries);
                lstImgName.Add(fileImage[0]);
                lstImage.Add(fileImage[1]);
            }
        }

        //Copy hình từ thư mục đích sang thư mục tạm
        private  void CreateTempImage()
        {
            for(int i =0; i<lstImgName.Count;i++)
            {
                FileStream file = new FileStream(pathTemp + @"\" + lstImgName[i],FileMode.Create,FileAccess.Write);
                byte[] byteCoding = Convert.FromBase64String(lstImage[i]);
                file.Write(byteCoding, 0, byteCoding.Length);
                file.Close();
            }
        }
        //Set đường dẫn đến file tạm trong ứng dụng

        private List<string> GetImgTag(string html)
        {
            int i=0;
            List<string> tagImg = new List<string>();
            try
            {
                while (i < html.Length)
                {
                    int iFirst = html.IndexOf("<IMG", i);
                    int iLast = html.IndexOf(">", iFirst);
                    tagImg.Add(html.Substring(iFirst, iLast - iFirst + 1));
                    i = iLast;
                }
            }
            catch { }
            return tagImg;
        }
        private  void SetPathImage(ref string html)
        {
            List<string> images = GetImgTag(html);
            for (int i = 0; i < images.Count; i++)
            {
                string oldpath = images[i].Substring(images[i].LastIndexOf("src=") + 5, images[i].LastIndexOf("align=") - images[i].LastIndexOf("src=") - 7);
                string newPath = pathTemp + @"\" + lstImgName[i];
                html = html.Replace(oldpath, newPath);
            }
        }

        private string GetFileName(string path)
        {
            FileInfo info = new FileInfo(path);
            return info.Name;
        }

        public void ShowImage(ref string html)
        {
            ReadImage(html);
            CreateTempImage();
            SetPathImage(ref html);
        }
        //Set nội dung byte của file vào code html
        public void SaveImage()
        {
            ClearImg();
            GetImage();
            string tag = "<!--";
            for (int i = 0; i < lstImage.Count; i++)
            {
                tag += "<image>" + lstImgName[i] + "<|>"+ lstImage[i];
            }
            tag += "-->";
            webBrowser1.Document.Body.InnerHtml += tag;
        }
        private void ClearImg()
        {
            try
            {
                string outerHtml = webBrowser1.Document.Body.OuterHtml;
                webBrowser1.Document.Body.InnerHtml = outerHtml.Remove(outerHtml.LastIndexOf("<!--"));
            }
            catch { }
        }
        private void GetImage()
        {
            HtmlElementCollection imageElement =  webBrowser1.Document.Images;
            lstImage.Clear();
            lstImgName.Clear();
            foreach(HtmlElement e in imageElement)
            {
                string path = e.OuterHtml.Substring(e.OuterHtml.LastIndexOf("src=") + 5, e.OuterHtml.LastIndexOf("align=") - e.OuterHtml.LastIndexOf("src=") - 7);
                try
                {
                    path = path.Remove(path.LastIndexOf("file:///"), 8);
                    path = path.Replace("%20", " ");
                }
                catch { }
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] byteContent = new byte[file.Length];
                file.Read(byteContent, 0, byteContent.Length);
                string byteEncoding = Convert.ToBase64String(byteContent);
                lstImage.Add(byteEncoding);
                lstImgName.Add(GetFileName(path));
            }
        }
        /// <summary>
        /// Indent the current paragraph.
        /// </summary>
        public void Indent()
        {
            webBrowser1.Document.ExecCommand("Indent", false, null);
        }

        /// <summary>
        /// Outdent the current paragraph.
        /// </summary>
        public void Outdent()
        {
            webBrowser1.Document.ExecCommand("Outdent", false, null);
        }

        /// <summary>
        /// Insert a link at the current selection.
        /// </summary>
        /// <param name="url">The link url</param>
        public void InsertLink(string url)
        {
            webBrowser1.Document.ExecCommand("CreateLink", false, url);
        }

        /// <summary>
        /// Get the ready state of the internal browser component.
        /// </summary>
        public ReadyState ReadyState
        {
            get
            {
                switch (doc.readyState.ToLower())
                {
                    case "uninitialized":
                        return ReadyState.Uninitialized;
                    case "loading":
                        return ReadyState.Loading;
                    case "loaded":
                        return ReadyState.Loaded;
                    case "interactive":
                        return ReadyState.Interactive;
                    case "complete":
                        return ReadyState.Complete;
                    default:
                        return ReadyState.Uninitialized;
                }
            }
        }

        /// <summary>
        /// Get the current selection type.
        /// </summary>
        public SelectionType SelectionType
        {
            get
            {
                switch (doc.selection.type.ToLower())
                {
                    case "text":
                        return SelectionType.Text;
                    case "control":
                        return SelectionType.Control;
                    case "none":
                        return SelectionType.None;
                    default:
                        return SelectionType.None;
                }
            }
        }

        /// <summary>
        /// Get/Set the current font size.
        /// </summary>
        [Browsable(false)]
        public FontSize FontSize
        {
            get
            {
                try
                {
                    if (ReadyState != ReadyState.Complete) return FontSize.NA;
                    switch (doc.queryCommandValue("FontSize").ToString())
                    {
                        case "1":
                            return FontSize.One;
                        case "2":
                            return FontSize.Two;
                        case "3":
                            return FontSize.Three;
                        case "4":
                            return FontSize.Four;
                        case "5":
                            return FontSize.Five;
                        case "6":
                            return FontSize.Six;
                        case "7":
                            return FontSize.Seven;
                        default:
                            return FontSize.NA;
                    }
                }
                catch { return FontSize.NA; }
            }
            set
            {
                int sz;
                switch (value)
                {
                    case FontSize.One:
                        sz = 1;
                        break;
                    case FontSize.Two:
                        sz = 2;
                        break;
                    case FontSize.Three:
                        sz = 3;
                        break;
                    case FontSize.Four:
                        sz = 4;
                        break;
                    case FontSize.Five:
                        sz = 5;
                        break;
                    case FontSize.Six:
                        sz = 6;
                        break;
                    case FontSize.Seven:
                        sz = 7;
                        break;
                    default:
                        sz = 7;
                        break;
                }
                webBrowser1.Document.ExecCommand("FontSize", false, sz.ToString());
            }
        }

        /// <summary>
        /// Get/Set the current font name.
        /// </summary>
        [Browsable(false)]
        public FontFamily FontName
        {
            get
            {
                try
                {
                    if (ReadyState != ReadyState.Complete)
                        return null;
                    string name = doc.queryCommandValue("FontName") as string;
                    if (name == null) return null;
                    return new FontFamily(name);
                }catch{
                    return new FontFamily("Arial");
                }
            }
            set
            {
                if (value != null)
                    webBrowser1.Document.ExecCommand("FontName", false, value.Name);
            }
        }

        /// <summary>
        /// Get/Set the editor's foreground (text) color for the current selection.
        /// </summary>
        [Browsable(false)]
        public Color EditorForeColor
        {
            get
            {
                if (ReadyState != ReadyState.Complete)
                    return Color.Black;
                try
                {
                    return ConvertToColor(doc.queryCommandValue("ForeColor").ToString());
                }
                catch {
                    return Color.Black;
                }
            }
            set
            {
                string colorstr = 
                    string.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
                webBrowser1.Document.ExecCommand("ForeColor", false, colorstr);
            }
        }

        /// <summary>
        /// Get/Set the editor's background color for the current selection.
        /// </summary>
        [Browsable(false)]
        public Color EditorBackColor
        {
            get
            {
                if (ReadyState != ReadyState.Complete)
                    return Color.White;
                try
                {
                    return ConvertToColor(doc.queryCommandValue("BackColor").ToString());
                }
                catch { return Color.White;  }
            }
            set
            {
                string colorstr =
                    string.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
                webBrowser1.Document.ExecCommand("BackColor", false, colorstr);
            }
        }

        /// <summary>
        /// Initiate the foreground (text) color dialog for the current selection.
        /// </summary>
        public void SelectForeColor()
        {
            Color color = EditorForeColor;
            if (ShowColorDialog(ref color))
                EditorForeColor = color;
        }

        /// <summary>
        /// Initiate the background color dialog for the current selection.
        /// </summary>
        public void SelectBackColor()
        {
            Color color = EditorBackColor;
            if (ShowColorDialog(ref color))
                EditorBackColor = color;
        }

        /// <summary>
        /// Convert the custom integer (B G R) format to a color object.
        /// </summary>
        /// <param name="clrs">the custorm color as a string</param>
        /// <returns>the color</returns>
        private static Color ConvertToColor(string clrs)
        {
            int red, green, blue;
            // sometimes clrs is HEX organized as (RED)(GREEN)(BLUE)
            if (clrs.StartsWith("#"))
            {
                int clrn = Convert.ToInt32(clrs.Substring(1), 16);
                red = (clrn >> 16) & 255;
                green = (clrn >> 8) & 255;
                blue = clrn & 255;
            }
            else // otherwise clrs is DECIMAL organized as (BlUE)(GREEN)(RED)
            {
                int clrn = Convert.ToInt32(clrs);
                red = clrn & 255;
                green = (clrn >> 8) & 255;
                blue = (clrn >> 16) & 255;
            }
            Color incolor = Color.FromArgb(red, green, blue);
            return incolor;
        }

        /// <summary>
        /// Called when the cut tool strip button on the editor context menu 
        /// is clicked.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            Cut();
        }

        /// <summary>
        /// Called when the paste tool strip button on the editor context menu 
        /// is clicked.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            Paste();
        }

        /// <summary>
        /// Called when the copy tool strip button on the editor context menu 
        /// is clicked.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            Copy();
        }

        /// <summary>
        /// Called when the bold button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void boldButton_Click(object sender, EventArgs e)
        {
            Bold();
        }

        /// <summary>
        /// Called when the italic button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void italicButton_Click(object sender, EventArgs e)
        {
            Italic();
        }

        /// <summary>
        /// Called when the underline button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void underlineButton_Click(object sender, EventArgs e)
        {
            Underline();
        }

        /// <summary>
        /// Called when the foreground color button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void colorButton_Click(object sender, EventArgs e)
        {
            SelectForeColor();
        }

        /// <summary>
        /// Called when the background color button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void backColorButton_Click(object sender, EventArgs e)
        {
            SelectBackColor();
        }

        /// <summary>
        /// Show the interactive Color dialog.
        /// </summary>
        /// <param name="color">the input and output color</param>
        /// <returns>true if dialog accepted, false if dialog cancelled</returns>
        private bool ShowColorDialog(ref Color color)
        {
            bool selected;
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.SolidColorOnly = true;
                dlg.AllowFullOpen = false;
                dlg.AnyColor = false;
                dlg.FullOpen = false;
                dlg.CustomColors = null;
                dlg.Color = color;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    selected = true;
                    color = dlg.Color;
                }
                else
                {
                    selected = false;
                }
            }
            return selected;
        }

        /// <summary>
        /// Called when the link button on the toolstrip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void linkButton_Click(object sender, EventArgs e)
        {
            SelectLink();
        }

        /// <summary>
        /// Show a custom insert link dialog, and create the link.
        /// </summary>
        public void SelectLink()
        {
            using (LinkDialog dlg = new LinkDialog())
            {
                dlg.ShowDialog(this.ParentForm);
                if (!dlg.Accepted) return;
                string link = dlg.URI;
                if (link == null || link.Length == 0)
                {
                    PLMessageBox.ShowErrorMessage("Invalid URL");
                    return;
                }
                InsertLink(dlg.URL);
            }
        }

        /// <summary>
        /// Called when the image button on the toolstrip is clicked.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void imageButton_Click(object sender, EventArgs e)
        {
            InsertImage();
        }

        /// <summary>
        /// Called when the outdent button on the toolstrip is clicked.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void outdentButton_Click(object sender, EventArgs e)
        {
            Outdent();
        }

        /// <summary>
        /// Called when the indent button on the toolstrip is clicked.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void indentButton_Click(object sender, EventArgs e)
        {
            Indent();
        }

        /// <summary>
        /// Called when the cut button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cut();
        }

        /// <summary>
        /// Called when the copy button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Copy();
        }

        /// <summary>
        /// Called when the paste button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void pasteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Paste();
        }

        /// <summary>
        /// Called when the delete button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// Search the document from the current selection, and reset the 
        /// the selection to the text found, if successful.
        /// </summary>
        /// <param name="text">the text for which to search</param>
        /// <param name="forward">true for forward search, false for backward</param>
        /// <param name="matchWholeWord">true to match whole word, false otherwise</param>
        /// <param name="matchCase">true to match case, false otherwise</param>
        /// <returns></returns>
        public bool Search(string text, bool forward, bool matchWholeWord, bool matchCase)
        {
            bool success = false;
            if (webBrowser1.Document != null)
            {
                IHTMLDocument2 doc =
                    webBrowser1.Document.DomDocument as IHTMLDocument2;
                IHTMLBodyElement body = doc.body as IHTMLBodyElement;
                if (body != null)
                {
                    IHTMLTxtRange range;
                    if (doc.selection != null)
                    {
                        range = doc.selection.createRange() as IHTMLTxtRange;
                        IHTMLTxtRange dup = range.duplicate();
                        dup.collapse(true);
                        // if selection is degenerate, then search whole body
                        if (range.isEqual(dup))
                        {
                            range = body.createTextRange();
                        }
                        else
                        {
                            if (forward)
                                range.moveStart("character", 1);
                            else
                                range.moveEnd("character", -1);
                        }
                    }
                    else
                        range = body.createTextRange();
                    int flags = 0;
                    if (matchWholeWord) flags += 2;
                    if (matchCase) flags += 4;
                    success =
                        range.findText(text, forward ? 999999 : -999999, flags);
                    if (success)
                    {
                        range.select();
                        range.scrollIntoView(!forward);
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Event handler for the ordered list toolbar button
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void orderedListButton_Click(object sender, EventArgs e)
        {
            OrderedList();
        }

        /// <summary>
        /// Event handler for the unordered list toolbar button
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void unorderedListButton_Click(object sender, EventArgs e)
        {
            UnorderedList();
        }

        /// <summary>
        /// Event handler for the left justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyLeftButton_Click(object sender, EventArgs e)
        {
            JustifyLeft();
        }

        /// <summary>
        /// Event handler for the center justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyCenterButton_Click(object sender, EventArgs e)
        {
            JustifyCenter();
        }

        /// <summary>
        /// Event handler for the right justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyRightButton_Click(object sender, EventArgs e)
        {
            JustifyRight();
        }

        /// <summary>
        /// Event handler for the full justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyFullButton_Click(object sender, EventArgs e)
        {
            JustifyFull();
        }

        private void btTypeDoc_Click(object sender, EventArgs e)
        {
            if (state == "HTML")
            {
                // Hàm PLMessageBox.ShowConfirmMessage có vấn đề, khi nhấn ok không có tác dụng 
                DialogResult result = PLMessageBox.ShowConfirmMessage("Toàn bộ các điều chỉnh văn bản của bạn sẽ bị mất");
                if (result == DialogResult.Yes)
                {
                    string s = BodyText;
                    BodyHtml = s;
                    //webBrowser1.DocumentText = s;
                    btTypeDoc.Text = "Văn bản HTML";
                    state = "Text";

                    fontComboBox.Visible = false;
                    fontSizeComboBox.Visible = false;
                    boldButton.Visible = false;
                    italicButton.Visible = false;
                    underlineButton.Visible = false;
                    colorButton.Visible = false;
                    backColorButton.Visible = false;
                    linkButton.Visible = false;
                    imageButton.Visible = false;
                    justifyLeftButton.Visible = false;
                    justifyRightButton.Visible = false;
                    justifyCenterButton.Visible = false;
                    justifyFullButton.Visible = false;
                    orderedListButton.Visible = false;
                    unorderedListButton.Visible = false;
                    outdentButton.Visible = false;
                    indentButton.Visible = false;
                    toolStripSeparator1.Visible = false;
                    toolStripSeparator2.Visible = false;
                    toolStripSeparator3.Visible = false;
                    toolStripSeparator4.Visible = false;
                    toolStripSeparator5.Visible = false;
                    toolStripSeparator6.Visible = false;
                }
            }
            else
            {                
                btTypeDoc.Text = "Thuần văn bản";
                state = "HTML";

                fontComboBox.Visible = true;
                fontSizeComboBox.Visible = true;
                boldButton.Visible = true;
                italicButton.Visible = true;
                underlineButton.Visible = true;
                colorButton.Visible = true;
                backColorButton.Visible = true;
                linkButton.Visible = true;
                imageButton.Visible = true;
                justifyLeftButton.Visible = true;
                justifyRightButton.Visible = true;
                justifyCenterButton.Visible = true;
                justifyFullButton.Visible = true;
                orderedListButton.Visible = true;
                unorderedListButton.Visible = true;
                outdentButton.Visible = true;
                indentButton.Visible = true;
                toolStripSeparator1.Visible = true;
                toolStripSeparator2.Visible = true;
                toolStripSeparator3.Visible = true;
                toolStripSeparator4.Visible = true;
                toolStripSeparator5.Visible = true;
                toolStripSeparator6.Visible = true;
            }

        }


        #region PHUOC 
        public void _setHTMLText(string text)
        {
            this.DocumentText = text;
        }
        
        public string _getHTMLText()
        {
            string temp = "<body topmargin=0 bottommargin=0 leftmargin=0 rightmargin=0>";
            temp += this.BodyHtml;
            temp += "</body>";
            return temp;
        }
        #endregion
    }

    /// <summary>
    /// Enumeration of possible font sizes for the Editor component
    /// </summary>
    public enum FontSize
    {
        One,
        Two,
        Three,
        Four, 
        Five,
        Six,
        Seven,
        NA
    }

    public enum SelectionType
    {
        Text,
        Control,
        None
    }

    public enum ReadyState
    {
        Uninitialized,
        Loading,
        Loaded,
        Interactive,
        Complete
    }

}