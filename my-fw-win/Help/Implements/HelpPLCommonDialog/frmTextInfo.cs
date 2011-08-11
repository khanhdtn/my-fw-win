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
    public partial class frmTextInfo : XtraFormPL, IPublicForm
    {
        public frmTextInfo()
        {
            InitializeComponent();
        }

        public void InitData(String content, string msg)
        {
            this.Text = "DEBUG: " +msg;
            this.txtDisplay.Text = content;
        }
    }
}