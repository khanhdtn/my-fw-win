using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class frmPLWord : XtraFormPL, IPublicForm
    {
        RichTextBox rtb = null;

        public frmPLWord(RichTextBox rtb)
        {
            InitializeComponent();
            this.plWord1.rtbData.Rtf = rtb.Rtf;
            this.rtb = rtb;
        }

        private void frmPLWord_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.rtb.Rtf = this.plWord1.rtbData.Rtf;
        }        
    }
}