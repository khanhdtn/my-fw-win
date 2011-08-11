using System;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    /// <summary>Cho phép nhập vào tháng / năm hoặc quý / năm     
    /// </summary>
    public class PLMonthOrQuater : TextEdit
    {
        private DateTime date;

        private bool IsFrom = true;
        private bool IsMonth = true;

        protected override bool OnInvalidValue(Exception sourceException)
        {
            Exception source = new Exception("Giá trị không hợp lệ");            
            return base.OnInvalidValue(source);
        }

        private void _initControl(bool IsMonth)
        {
            if (IsMonth == true)
                this.Properties.Mask.EditMask = "(0?[1-9]|10|11|12)/\\d{4}";
            else
                this.Properties.Mask.EditMask = "([1-4])/\\d{4}";
            this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.Properties.Mask.PlaceHolder = '_';
            this.Properties.Mask.SaveLiteral = true;
            this.Properties.Mask.ShowPlaceHolders = true;
            this.Properties.Mask.IgnoreMaskBlank = false;
        }

        public void _init(bool IsMonth, bool IsFrom)
        {
            this.IsMonth = IsMonth;
            this.IsFrom = IsFrom;
            _initControl(IsMonth);
        }

        private int getParamFirst()
        {
            try
            {
                string text = this.EditValue.ToString();
                string value = text.Substring(0, text.LastIndexOf('/'));
                return HelpNumber.ParseInt32(value);
            }
            catch { return -1; }
        }

        public int GetQuarter()
        {
            if (IsMonth == false)
            {
                return getParamFirst();
            }
            return -1; 
        }
        public int GetMonth()
        {
            if (IsMonth == true)
            {
                return getParamFirst();
            }
            return -1;
        }
        public int GetYear()
        {
            try
            {
                string text = this.EditValue.ToString();
                string year = text.Substring(text.LastIndexOf('/') + 1);
                return HelpNumber.ParseInt32(year);
            }
            catch { return -1; }
        }
        public DateTime GetDateTime()
        {
            try
            {
                if(IsFrom)
                {
                    if (IsMonth)
                    {
                        date = HelpDate.GetStartOfMonth(getParamFirst(), GetYear());
                    }
                    else
                    {
                        Quy quy = Quy.Mot;
                        switch (getParamFirst())
                        {
                            case 1:
                                quy = Quy.Mot;
                                break;
                            case 2:
                                quy = Quy.Hai;
                                break;
                            case 3:
                                quy = Quy.Ba;
                                break;
                            case 4:
                                quy = Quy.Bon;
                                break;
                        }
                        date = HelpDate.GetStartOfQuarter(GetYear(), quy);
                    }
                }
                else
                {
                    if (IsMonth)
                        date = HelpDate.GetEndOfMonth(getParamFirst() , GetYear());
                    else{
                        Quy quy = Quy.Mot;
                        switch (getParamFirst())
                        {
                            case 1:
                                quy = Quy.Mot;
                                break;
                            case 2:
                                quy = Quy.Hai;
                                break;
                            case 3:
                                quy = Quy.Ba;
                                break;
                            case 4:
                                quy = Quy.Bon;
                                break;
                        }
                        date = HelpDate.GetEndOfQuarter(GetYear() , quy);
                    }
                }
                return date;
            }
            catch { return DateTime.MinValue; }
        }
    }    
}
