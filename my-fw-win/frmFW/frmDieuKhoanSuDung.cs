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
    public partial class frmDieuKhoanSuDung : DevExpress.XtraEditors.XtraForm
    {
        public frmDieuKhoanSuDung()
        {
            InitializeComponent();
            this.richTextBox1.Rtf = DieuKhoanSuDung.GetDieuKhoanSuDung();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}