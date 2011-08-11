using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public interface IRequiredDBObject
    {
        void Requirement();
    }

    public class DatabaseMan
    {
        public static bool IsCheckDB = true;
        public static List<DBObject> DBObjectList = new List<DBObject>();


        public static bool RequireDBObject(params DBObject[] dbObjs)
        {
            if (IsCheckDB == true)
            {
                //AAAA
                DBObjectList.AddRange(dbObjs);
            }
            return true;
        }

        public static bool CompareTwoDBObject(DBObject a, DBObject b)
        {
            return true;
        }

        public static bool VerifyDatabase()
        {
            return true;
        }
    }
}
