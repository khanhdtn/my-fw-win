using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace ProtocolVN.Framework.Win
{
    class WindowUser
    {
        public static WindowsIdentity GetWindowUser(){
            WindowsIdentity current = System.Security.Principal.WindowsIdentity.GetCurrent();
            return current;
        }        
    }
}
