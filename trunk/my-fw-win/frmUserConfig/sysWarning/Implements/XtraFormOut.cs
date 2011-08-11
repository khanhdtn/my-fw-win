using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class XtraFormOut : PLOut
    {
        #region PLOut Members

        public object open(object param)
        {
            return "NOOP";
        }

        public object write(string title, string text)
        {
            return "NOOP";            
        }

        public object close(object param)
        {
            return "NOOP";
        }

        #endregion
    }
}
