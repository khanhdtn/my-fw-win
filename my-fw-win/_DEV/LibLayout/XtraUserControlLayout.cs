using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;

namespace pl.fw.layout.test
{
    public partial class XtraUserControlLayout : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlLayout()
        {
            InitializeComponent();
            TrialXtraLayoutControl.InitLayoutControlUserCtrl(this, "pl.fw.layout.test", layoutControl1, Application.StartupPath + "\\xmllayout");
        }
    }
}
