using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Plugin.WarningSystem
{
    public interface IConfigWarning
    {
        bool Save();
        void NoSave();
        void LoadData();
    }
}
