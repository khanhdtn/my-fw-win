using System;
using System.Collections.Generic;
using System.Text;

using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Plugin.NoteBook
{
    public class StickiesMethodExec
    {
        public static bool IsOpen = false;
        public static frmStickiesMain stickies = null;
        public static void RunStickies()
        {
            try{
                if (IsOpen == false)
                {
                    stickies = new frmStickiesMain();
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Sổ ghi chú đang mở. Xin vui lòng kiểm tra lại.");
                }
            }
            catch(Exception ex){
                PLException pl = new PLException(ex, "frmStickiesMain", "RunStickies", "", "Lỗi mở sổ ghi chú");
                PLException.AddException(pl);
            }
        }
        public static void StopStickies()
        {
            try{
                if (stickies != null && stickies.stickyNotes!=null)
                {
                    foreach (frmStickyNote f in stickies.stickyNotes)
                    {
                        f.Close();
                    }
                    stickies.stickyNotes = new List<frmStickyNote>();
                    stickies.Close();
                    stickies.Dispose();
                }
                StickiesMethodExec.IsOpen = false;
            }
            catch (Exception ex)
            {
                PLException pl = new PLException(ex, "frmStickiesMain", "StopStickies", "", "Lỗi đóng sổ ghi chú");
                PLException.AddException(pl);
            }
        }
    }
}
