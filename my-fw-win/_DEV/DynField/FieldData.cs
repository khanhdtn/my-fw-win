using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class FieldData
    {
        private long _FIELD_ID;

        public long FIELD_ID
        {
            get { return _FIELD_ID; }
            set { _FIELD_ID = value; }
        }        

        private string _CAPTION;

        public string CAPTION
        {
            get { return _CAPTION; }
            set { _CAPTION = value; }
        }
        private long _DATA_TYPE;

        /// <summary>
        /// 1: Text
        /// 2: Number với Spin Edit
        /// 3: Number vớic Calc Edit
        /// 4: Boolean
        /// 5. Date
        /// </summary>
        public long DATA_TYPE
        {
            get { return _DATA_TYPE; }
            set { _DATA_TYPE = value; }
        }
        private string _CONTENT;

        public string CONTENT
        {
            get { return _CONTENT; }
            set { _CONTENT = value; }
        }
    }
}
