using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public abstract class DBObject
    {
        public List<DBObject> RequireObjectName = new List<DBObject>();

        public string NAME;
        public string DDL;

        public DBObject()
        {
            if (DatabaseMan.IsCheckDB == true)
            {
                DatabaseMan.RequireDBObject(this);
            }
        }
    }
}
