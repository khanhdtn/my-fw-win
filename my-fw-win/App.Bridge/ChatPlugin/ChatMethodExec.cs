using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class ChatMethodExec
    {
        public static void RunMessenger()
        {
            try
            {
                new ProtocolVN.Plugin.Chat.Form1().Show(FrameworkParams.MainForm);
            }
            catch (Exception ex)
            {
                PLException pl = new PLException(ex, "Chat", "RunMessenger", "", "Lỗi mở ứng dụng Chat");
                PLException.AddException(pl);
            }
        }
    }
}
