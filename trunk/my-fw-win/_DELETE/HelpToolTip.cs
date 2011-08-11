using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Utils;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public class HelpToolTip
    {
        public static void _setTooTip(BaseControl control, String header, String htmlText, String footer)
        {
            SuperToolTip superToolTip = new SuperToolTip();

            ToolTipItem toolTipItem = new ToolTipItem();
            toolTipItem.Text = htmlText;
            toolTipItem.AllowHtmlText = DefaultBoolean.True;
            if (header != null)
            {
                superToolTip.Items.AddTitle(header);
            }
            superToolTip.Items.Add(toolTipItem);
            if (footer != null)
            {
                superToolTip.Items.AddSeparator();
                superToolTip.Items.AddTitle(footer);
            }
            superToolTip.AllowHtmlText = DefaultBoolean.True;

            control.SuperTip = superToolTip;
        }
    }
}
