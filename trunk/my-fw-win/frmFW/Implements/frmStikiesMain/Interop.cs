using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ProtocolVN.Plugin.NoteBook
{
    class Interop
    {
        //imported win32 function
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The HideCaret function removes the caret from the screen.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);
    }
}
