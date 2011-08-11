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
    public partial class frmAddItem : DevExpress.XtraEditors.XtraForm, IAddItem
    {
        protected long itemId;
        protected object itemValue;
        public frmAddItem()
        {
            InitializeComponent();
        }

        #region IAddItem Members

        public long _getId()
        {
            return itemId;
        }

        public object _getValue()
        {
            return itemValue;
        }

        #endregion
    }
}