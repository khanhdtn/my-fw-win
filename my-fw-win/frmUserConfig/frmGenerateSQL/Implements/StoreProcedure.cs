using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class StoreProcedure : DBObject
    {
        public StoreProcedure():base()
        {

        }
        public StoreProcedure(string Name, String DDL):base()
        {
            this.NAME = Name;
            this.DDL = DDL;                 
        }

        public StoreProcedure(string Name, String DDL, params DBObject[] ObjNames ) :base()
        {
            this.NAME = Name;
            this.DDL = DDL;
            this.RequireObjectName.AddRange(ObjNames);
        }
    }
}
