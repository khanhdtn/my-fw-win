using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using System.Drawing;
using System.Xml;
using System.Timers;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp cho phép hiện thị messagebox.
    /// </summary>
    public class XMessageBox : XtraForm, IPublicForm
    {
        private PictureEdit pictureEdit1;
        private Label lbText;
        private PanelControl pc1;
        private int width;
        private int height;
        private IDialogAction result = null;
        private IDialogAction[] actions = null;
        private SimpleButton[] nButtons = null;
        private static int[] sizetext_buttons = null;
        private static int margin_default = 20;          

        public XMessageBox()
        {           
            this.width = 361;
            this.height = 103;
        }

        private XMessageBox(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        private void InitializeComponent(string msg, Image img, string[] buttonNames)
        {
            int y = 9;
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lbText = new System.Windows.Forms.Label();
            this.pc1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc1)).BeginInit();
            this.pc1.SuspendLayout();
            this.SuspendLayout();

            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(12, y);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Image = img;
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(54, 51);
            this.pictureEdit1.TabIndex = 7;

            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(83, y);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(49, 13);
            this.lbText.TabIndex = 6;
            this.lbText.Text = msg;
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;           
            
            // 
            // pc1
            // 
            this.pc1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //
            // buttons dynamic
            //
            this.nButtons = new SimpleButton[buttonNames.Length];
            for (int i = buttonNames.Length - 1; i >= 0; i--)
            {
                SimpleButton bt = new DevExpress.XtraEditors.SimpleButton();               
                bt.Location = new System.Drawing.Point(width - 75 - 10 - ((buttonNames.Length - 1 - i) * 85), 5);
                bt.Name = i + "";
                bt.Size = new System.Drawing.Size(sizetext_buttons[i] + margin_default, 23);
                bt.TabIndex = 0;
                bt.Text = buttonNames[i];
                bt.Click += new System.EventHandler(this.userClick);
                bt.Tag = this.actions[i];

                this.pc1.Controls.Add(bt);
                this.nButtons[i] = bt;
            }
            y = 71;
            //this.pc1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pc1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.pc1.Location = new System.Drawing.Point(0, y);
            this.pc1.Name = "pc1";
            this.pc1.Size = new System.Drawing.Size(361, 32);
            this.pc1.TabIndex = 0;

            // 
            // XMessageBox
            // 
            this.ClientSize = new System.Drawing.Size(this.width, this.height);
            this.Controls.Add(this.pc1);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.lbText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc1)).EndInit();
            this.pc1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void DoAutoPos()
        {
            this.Width = Math.Max((this.nButtons.Length * 85) + 20, Math.Max(Math.Max((80 + this.lbText.Width + 12), (80 + this.pictureEdit1.Width + 12)), 200));
            this.Height = 5 + Math.Max((32 + this.lbText.Height + 24), (32 + this.pictureEdit1.Height + 24)) + 16;
            
            int x = 80 + ((this.Width - 80 - 12) / 2) - (this.lbText.Width / 2);
            int y = ((this.Height - 32 - 16) / 2) - (this.lbText.Height / 2) - 5;
            this.lbText.Location = new System.Drawing.Point(x, y);

            int h = this.Height - 63;
            this.pc1.Location = new System.Drawing.Point(0, h);
            this.pc1.Size = new System.Drawing.Size(this.Width, this.Height);
            //for (int i = this.nButtons.Length - 1; i >= 0; i--)
            //{
            //    this.nButtons[i].Location = new System.Drawing.Point(this.Width - 75 - 15 - ((this.nButtons.Length - 1 - i) * 85), 5);            
            //}
            for (int i = this.nButtons.Length - 1; i >= 0; i--)
            {
                int total_width_next_btn = 0;
                for (int j = i; j < nButtons.Length; j++)
                    total_width_next_btn += sizetext_buttons[j] + margin_default + 6;

                this.nButtons[i].Location =
                    new System.Drawing.Point(this.Width - total_width_next_btn - 15, 5);
                this.nButtons[i].Anchor = AnchorStyles.Right;                
            }            
        }        

        private void userClick(object sender, EventArgs e)
        {
            if (this.actions != null)
            {
                try
                {
                    IDialogAction SelectedAction = (IDialogAction)((SimpleButton)sender).Tag;
                    this.result = SelectedAction;
                    if (SelectedAction != null)
                    {
                        SelectedAction.Action();
                    }
                }
                catch (Exception ex) { }
            }
            this.Close();
        }

        #region Sử dụng
        public static IDialogAction ShowMessage(string title, string msg, Image img, IDialogAction[] actions)
        {
            sizetext_buttons = new int[actions.Length];
            string[] acc = new string[actions.Length];
            for (int i = 0; i < acc.Length; i++)
            {
                acc[i] = actions[i].GetTitle();                 
                Size s = TextRenderer.MeasureText(acc[i],Control.DefaultFont);
                sizetext_buttons[i] = s.Width;                
            }
            return ShowCusMessage(title, msg, img, acc, actions);
        }

        private static IDialogAction ShowCusMessage(string title, string msg, Image img, 
            string[] buttonNames, IDialogAction[] actions)
        {
            XMessageBox box = new XMessageBox();
            box.actions = actions;
            box.InitializeComponent(msg, img, buttonNames);
            box.Text = title;
            box.DoAutoPos();

            int total_width_btn = 0;
            for (int i = 0; i < sizetext_buttons.Length; i++)            
                total_width_btn += sizetext_buttons[i];

            box.MinimumSize = new Size(total_width_btn + 
                ((margin_default + 6) * (sizetext_buttons.Length  + 1)), 0);             
           
            box.ShowDialog();

            //box.Width = 5;
            box.Height = 5;

            return box.result;
        }
        
        #endregion
    }    
}
