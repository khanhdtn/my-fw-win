using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    //Kiểm tra tính hợp pháp khi dùng control của Win
    sealed class WinLaw
    {
        //Lớp hỗ trợ không cho phép dùng control của PROTOCOL
        //Mà chưa có sự cho phép của PROTOCOL
        public static object checkLaw(object input)
        {
            //try
            //{
            //    if (Debugger.IsAttached == false)
            //    {
            //        if (FrameworkParams.MainForm != null)
            //        {
            //            if (FrameworkParams.MainForm.GetType().FullName
            //                != typeof(frmRibbonMain).FullName)
            //            {
            //                new frmPLAbout().ShowDialog();
            //            }
            //        }
            //        else
            //        {
            //            //new frmPLAbout().ShowDialog();
            //        }
            //    }
            //}
            //catch { }
            //finally {  }
            return null;
        }            
    }
}
