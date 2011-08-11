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
    public partial class CondDate : XtraUserControl
    {
        public CondDate()
        {
            InitializeComponent();
        }

        public DateEdit DateTu
        {
            get {
                return this.NgayTu;
            }
        }

        public DateEdit DateDen
        {
            get {
                return this.NgayDen;
            }
        }
    }
}
