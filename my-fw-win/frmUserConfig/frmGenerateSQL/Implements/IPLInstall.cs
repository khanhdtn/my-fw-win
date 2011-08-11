using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public interface IPLInstall
    {
        string GetInstallSQL();
        string GetUnInstallSQL();
        string CheckSQL();
    }    
}
