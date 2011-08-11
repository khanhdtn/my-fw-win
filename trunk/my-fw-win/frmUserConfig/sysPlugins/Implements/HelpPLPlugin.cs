using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace ProtocolVN.Plugin
{
    public class HelpPLPlugin
    {
        public static string GetPluginFolder()
        {
            //return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);            
        }
    }
}
