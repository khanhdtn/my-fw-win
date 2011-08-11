using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Plugin.ImpExp;

namespace ProtocolVN.Framework.Win
{
    public class AppFirebirdServer: WinRDBMS
    {
        public void initDBServerConfig()
        {
            frmImport.rdbms = new frmImportFirebird();
            PLDebug.rdbms = new FirebirdHelpDebug();
        }
    }
}
