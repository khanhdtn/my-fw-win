using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Lớp cho phép lấy ID tăng dần cho Item va cho Group.
    /// Được dùng chủ yếu cho việc sinh ID của menu & toolbar    
    /// </summary>
    public class GenID
    {
        private int ItemID;
        private int GroupID;
        public GenID()
        {
            ItemID = 0;
            GroupID = 1000;
        }

        public string NewItem()
        {
            ItemID += 1;
            return "I" + ItemID;
        }
        
        public string NewGroup()
        {
            GroupID += 1;
            return "G" + GroupID;
        }

        public string CurrentGroup()
        {
            return "G" + GroupID;
        }

        public string ParentGroup()
        {
            return "G" + (GroupID - 1);
        }
    }
}
