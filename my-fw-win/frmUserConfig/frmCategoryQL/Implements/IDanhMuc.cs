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
        String MenuItem(String MainCat, string Title, string ParentID, bool IsSep);
        String MenuItem(String MainCat, string ParentID, bool IsSep);
    }
}
