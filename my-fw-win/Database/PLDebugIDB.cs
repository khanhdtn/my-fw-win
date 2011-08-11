using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public interface IDBHelpDebug
    {
        string GetLastestFbException(List<Exception> listExp);
        string GetUserErrorMsg(List<Exception> listExp);
        void ShowExceptionInfo(List<Exception> listExp, string Msg);
        void ShowExceptionInfo();
        void ShowExceptionInfo(List<Exception> listExp);
    }
}
