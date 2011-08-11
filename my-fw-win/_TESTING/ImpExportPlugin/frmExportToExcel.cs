using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Plugin.ImpExp
{
    public partial class frmExportToExcel : DevExpress.XtraEditors.XtraForm
    {
        public frmExportToExcel(string names)
        {
            InitializeComponent();
            meContent.Text = names;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] names = (meContent.Text).Split(';');
            ExcelSupport.ExportToExcel(names);
            this.Close();
        }

        private void frmExportToExcel_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}