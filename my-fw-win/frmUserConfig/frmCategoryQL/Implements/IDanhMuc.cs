using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public interface IDanhMuc
    {
        String Item();
        XtraUserControl Init();
        String MenuItem(String mainCat, string title, string parentID, bool isSep);
        String MenuItem(String mainCat, string parentID, bool isSep);
    }
}
