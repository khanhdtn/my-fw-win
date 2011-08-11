using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public class PLRichTextBox :RichTextBox
    {
        public PLRichTextBox()
            : base()
        {
            HelpRichTextBox.RightClick(this);
        }
    }
}
