﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class CondComboBox : XtraUserControl
    {
        public CondComboBox()
        {
            InitializeComponent();
        }        

        public PLCombobox ComboID
        {
            get {
                return this.cbID;
            }
        }        
    }
}
