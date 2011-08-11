using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class frmGridInfo : XtraFormPL, IPublicForm
    {
        public frmGridInfo()
        {
            InitializeComponent();
            this.viewDebug.OptionsView.ColumnAutoWidth = false;            
            this.viewDebug.OptionsBehavior.Editable = false;
            this.cmdXoa.Visible = false;
        }

        public void InitData(DataSet Ds, string Title)
        {
            this.gridDebug.DataSource = Ds.Tables[0];
            this.viewDebug.PopulateColumns();
            this.Text = Title;
            this.viewDebug.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PLException.GetLastestExceptions().Clear();
            this.Close();
        }
    }
}