using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;

namespace DevExpress.XtraEditors
{
    public class PLSpinEdit : SpinEdit
    {
        public PLSpinEdit()
            : base()
        {
            this.GotFocus += delegate { this.SelectAll(); };
            this._SetKeys00_000();
        }

        public void _SetInt(decimal Min, decimal Max, bool AllowNULL)
        {
            this.Properties.Mask.EditMask = "0";

            if (AllowNULL == false) this.EditValue = Min;
            else this.EditValue = null;            
            this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            if (Min == Decimal.MinValue && Max == Decimal.MaxValue) return;

            this.Spin += delegate(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
            {
                try
                {
                    if ((decimal)this.EditValue == Min)
                    {
                        if (!e.IsSpinUp)
                            e.Handled = true;
                    }
                    else if ((decimal)this.EditValue == Max)
                    {
                        if (e.IsSpinUp)
                            e.Handled = true;
                    }
                }
                catch { }
            };
            this.EditValueChanged += delegate(object sender, EventArgs e)
            {
                if (AllowNULL && this.EditValue == null) return;
                try
                {
                    if ((decimal)this.EditValue < Min)
                    {
                        this.EditValue = Min;
                    }
                    else if ((decimal)this.EditValue > Max)
                    {
                        this.EditValue = Max;
                    }
                }
                catch { }
            };
        }        
        public void _SetInt(decimal Min, bool AllowNULL)
        {
            _SetInt(Min, decimal.MaxValue, AllowNULL);
        }
        public void _SetInt(bool AllowNULL)
        {
            _SetInt(decimal.MinValue, decimal.MaxValue, AllowNULL);
        }

        public void _SetDec(decimal Min, decimal Max, int numDec, bool AllowNULL)
        {
            _SetInt(Min, Max, AllowNULL);
            this.Properties.Mask.EditMask = "" + numDec;
        }
        public void _SetDec(decimal Min, int numDec, bool AllowNULL)
        {
            _SetDec(Min, decimal.MaxValue, numDec, AllowNULL);
        }
        public void _SetDec(int numDec, bool AllowNULL)
        {
            _SetDec(decimal.MinValue, decimal.MaxValue, numDec, AllowNULL);
        }

        public void _SetKeys00_000()
        {
            this.KeyDown += delegate(object sender, KeyEventArgs e)
            {               
                if (ShortcutKey.K_00 == e.KeyCode)
                    this.EditValue = HelpNumber.ParseDecimal(this.EditValue) * 100;
                else if (ShortcutKey.K_000 == e.KeyCode)
                    this.EditValue = HelpNumber.ParseDecimal(this.EditValue) * 1000;
            };
        }
    }    
}
