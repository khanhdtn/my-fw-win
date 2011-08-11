using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel;

namespace System.Windows.Forms
{
    public class PLLabel : LabelControl
    {
        public enum LabelType{
            OPTION,
            REQUIRED,
            DESCRIPTION,
            NORMAL
        }
        LabelType _type = LabelType.REQUIRED;

        [Browsable(true), Category("_PROTOCOL"), Description("Loại nhãn hiện thị")]
        public LabelType ZZZType
        {
            get { return _type; }
            set
            {
                _type = value;
                switch (_type)
                {
                    case LabelType.OPTION:
                        this.Appearance.Font = new System.Drawing.Font(this.Appearance.Font.FontFamily.Name, this.Appearance.Font.Size, System.Drawing.FontStyle.Underline);
                        this.ToolTip = "Dữ liệu không bắt buộc nhập";
                        break;
                    case LabelType.REQUIRED:
                        this.Appearance.Font = new System.Drawing.Font(this.Appearance.Font.FontFamily.Name, this.Appearance.Font.Size, System.Drawing.FontStyle.Regular);
                        this.ToolTip = "Dữ liệu bắt buộc nhập";
                        break;
                    case LabelType.DESCRIPTION:
                        this.Appearance.Font = new System.Drawing.Font(this.Appearance.Font.FontFamily.Name, this.Appearance.Font.Size, System.Drawing.FontStyle.Italic);            
                        break;
                    case LabelType.NORMAL:
                        break;
                }
            }
        }

        public PLLabel() : base()
        {
        }

        public void _setToolTip(String toolTip)
        {
            this.ToolTip = toolTip;
        }
    }    
}
