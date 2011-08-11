using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Plugin.NoteBook
{
    public partial class frmCustomizeLook : XtraForm
    {
        public bool IsSaved = false;

        public frmCustomizeLook()
        {
            InitializeComponent();
        }

        #region Properties

        public Color ChosenTitleBarColor
        {
            get
            {
                return pnlTitleColor.BackColor;
            }
            set
            {
                pnlTitleColor.BackColor = value;
            }
        }

        public Color ChosenBackgroundColor
        {
            get
            {
                return pnlBgColor.BackColor;
            }
            set
            {
                pnlBgColor.BackColor = value;
            }
        }

        public Color ChosenFontColor
        {
            get
            {
                return lblFont.ForeColor;
            }
            set
            {
                lblFont.ForeColor = value;
            }
        }

        public Font ChosenFont
        {
            get
            {
                return lblFont.Font;
            }
            set
            {
                lblFont.Font = value;
            }
        }

        public Color ChosenTitleFontColor
        {
            get
            {
                return lblTitleFont.ForeColor;
            }
            set
            {
                lblTitleFont.ForeColor = value;
            }
        }

        public Font ChosenTitleFont
        {
            get
            {
                return lblTitleFont.Font;
            }
            set
            {
                lblTitleFont.Font = value;
            }
        }
        public double ChosenOpacity
        {
            get
            {
                return (Convert.ToDouble(trackBar1.Value) / 100);       
            }

            set
            {
                int t = (int)Math.Floor(value * 100);
                trackBar1.Value = t;
                lblOpacity.Text = t.ToString()+"%";
            }
        }

        #endregion


        private void pnlColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = ((Panel)sender).BackColor;
            cd.ShowDialog();
            ((Panel)sender).BackColor = cd.Color;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = lblFont.Font;
            fd.Color = lblFont.ForeColor;
            fd.ShowColor = true;
            fd.ShowDialog();

            lblFont.ForeColor = fd.Color;
            lblFont.Font = fd.Font;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsSaved = false;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IsSaved = true;
            Close();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value < 10) trackBar1.Value = 10;
            lblOpacity.Text = trackBar1.Value.ToString() + "%";
        }

        private void btnTitleFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = lblTitleFont.Font;
            fd.Color = lblTitleFont.ForeColor;
            fd.ShowColor = true;
            fd.ShowDialog();

            lblTitleFont.ForeColor = fd.Color;
            lblTitleFont.Font = fd.Font;

        }

        
    }
}