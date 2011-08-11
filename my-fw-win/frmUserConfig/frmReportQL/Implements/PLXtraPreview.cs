using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public partial class PLXtraPreview : DevExpress.XtraPrinting.Control.PrintControl
    {
        public PLXtraPreview()
        {
            InitializeComponent();
            this.previewBar3.Visible = false;
            //this.previewBar1.OptionsBar.AllowQuickCustomization = false;           
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
