using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public class ShortcutKey
    {
        public static Keys K_00 = Keys.F2;
        public static Keys K_000 = Keys.F3;
        public static Keys K_FOCUS_NEW_ROW = Keys.F12;


        //Tham khảo thêm trong lớp DeveloperKey.

        //Phím tắt: ALT-?
        //Muc đích: Focus vào dòng Auto Filter của lưới nếu lưới đó hiển thi Auto Filter
        public static bool K_ALT_QUESTION(KeyEventArgs e){
            if (e.Modifiers == Keys.Alt && e.KeyValue == 191)
                return true;
            return false;
        }

        public static bool K_ALT_Z(KeyEventArgs e){
            if(e.Modifiers == Keys.Alt && e.KeyValue == 90){
                return true;
            }
            return false;
        }
    }    
}
