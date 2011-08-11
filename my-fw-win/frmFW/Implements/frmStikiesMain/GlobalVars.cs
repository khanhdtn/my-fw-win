using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Win.frmFW.Implements.frmStikiesMain;
namespace ProtocolVN.Plugin.NoteBook
{
    class GlobalVars
    {

        public static string ConfigDir
        {
            get
            {
                #if DEBUG
                //string ret = Path.Combine(new FileInfo( Application.ExecutablePath).DirectoryName, "Stickies");
                //PHUOCNC
                string ret = FrameworkParams.CONF_FOLDER;
                #else
                //string ret = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Stickies");
                //PHUOCNC
                string ret = FrameworkParams.CONF_FOLDER;
                #endif
                if (!Directory.Exists(ret))
                    Directory.CreateDirectory(ret);
                return ret;
            }
        }

        public static string ConfigFile
        {
            get
            {
                string cfgFile = Path.Combine(ConfigDir, "notebook.cpl");
                if (!File.Exists(cfgFile))
                {
                    dsConfig ds = new dsConfig();
                    ds.WriteXml(cfgFile, System.Data.XmlWriteMode.WriteSchema);
                }
                return cfgFile;
            }
        }        
    }
}
