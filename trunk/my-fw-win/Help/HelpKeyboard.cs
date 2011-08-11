using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ xử lý phím tắt trên form và trên toàn bộ ứng dụng
    /// </summary>
    public class HelpKeyboard
    {
        [Obsolete("Dùng hàm FWShortcutKey")]
        public static PLKey getPLKey(XtraForm frm)
        {
            return new PLKey(frm);
        }

        public static FWShortcutKey getFWShortcutKey(XtraForm frm)
        {
            return new FWShortcutKey(frm);
        }

        public static PLHotKey getPLHotKey()
        {
            return new PLHotKey();
        }
    }

    /// <summary>
    /// Xử lý phím tắt trên toàn bộ ứng dụng
    /// </summary>
    [Obsolete("Sử dụng lớp HelpShortcutKey.getPLHotKey để thay thế.")]
    public class PLHotKey
    {
        public static KeyboardHook hook = new KeyboardHook();
        private static List<HotKeyItem> items = new List<HotKeyItem>();        
        public PLHotKey()
        {
            initHotkey();
        }

        public static void AddHotKeyItem(HotKeyItem item)
        {
            items.Add(item);
            item.id = hook.RegisterHotKey(item.modifier, item.key);
        }

        public static void RemoveHotKeyItem(HotKeyItem item)
        {
            items.Remove(item);
        }

        /// <summary>
        /// Hàm xử lý các phím tắt
        /// </summary>
        private static void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (e.Key == items[i].key)
                {                    
                    items[i].func(null);
                    return;
                }
            }
        }

        private static void initHotkey()
        {
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(PLHotKey.hook_KeyPressed);            
        }                
    }

    public class HotKeyItem
    {
        public ProtocolVN.Framework.Win.ModifierKeys modifier;
        public Keys key;
        public DelegationLib.CallFunction_SinIn_SinOut func;
        public int id;  //ID đã đăng ký Hot_Keys

        public HotKeyItem(ProtocolVN.Framework.Win.ModifierKeys modifier, Keys key, DelegationLib.CallFunction_SinIn_SinOut func)
        {
            this.modifier = modifier;
            this.key = key;
            this.func = func;
        }

        public override bool Equals(Object obj)
        {
            HotKeyItem that = (HotKeyItem)obj;
            return that.id == this.id;
        }
    }
}
