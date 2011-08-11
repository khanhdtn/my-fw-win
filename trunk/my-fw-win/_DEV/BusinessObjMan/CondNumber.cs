using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class CondNumber : XtraUserControl
    {
        public CondNumber()
        {
            InitializeComponent();

            SpinTu.EditValue = null;
            SpinDen.EditValue = null;
        }

        public SpinEdit SpinTu
        {
            get {
                return this.SoTu;
            }
        }

        public SpinEdit SpinDen
        {
            get {
                return this.SoDen;
            }
        }
    }
}
