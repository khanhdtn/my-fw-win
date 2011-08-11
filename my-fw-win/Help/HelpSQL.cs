using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class HelpSQL
    {
        public static string SelectAll(string TableName, string SortFieldName, bool IgnoreCase)
        {
            if (SortFieldName == null || SortFieldName == "")
                return "select * from " + TableName;
            else{
                if (IgnoreCase)
                    return "select * from " + TableName + " order by lower(" + SortFieldName + ")";
                else
                    return "select * from " + TableName + " order by " + SortFieldName;
            }
        }
        public static string SelectAll(string TableName, string[] SortFieldNames)
        {
            if (SortFieldNames == null || SortFieldNames.Length == 0)
                return "select * from " + TableName;
            else
            {
                string query = "select * from " + TableName + " order by " + SortFieldNames[0];
                for (int i = 1; i < SortFieldNames.Length; i++)
                    query += ", " + SortFieldNames[i];
                return query;                    
            }
        }
        public static string SelectAll(string TableName, string[] SortFieldNames, bool[] IgnoreCase)
        {
            if (SortFieldNames == null || SortFieldNames.Length == 0)
                return "select * from " + TableName;
            else
            {
                string query = "";
                if( IgnoreCase[0] == true)
                    query = "select * from " + TableName + " order by lower(" + SortFieldNames[0] + ")";
                else
                    query = "select * from " + TableName + " order by " + SortFieldNames[0];

                for (int i = 1; i < SortFieldNames.Length; i++)
                {
                    if (IgnoreCase[i] == true)
                        query += ", lower(" + SortFieldNames[i] + ")";
                    else
                        query += ", " + SortFieldNames[i];
                }
                return query;
            }
        }
        
        public static string SelectWhere(string TableName, string Where, string SortFieldName, bool IgnoreCase)
        {
            if (Where.ToLower().IndexOf("order by") >= 0) SortFieldName = null;
            if (SortFieldName == null || SortFieldName == "")
                return "select * from " + TableName + " where " + Where;
            else
            {
                if (IgnoreCase)
                    return "select * from " + TableName + " where " + Where + " order by lower(" + SortFieldName + ")";
                else
                    return "select * from " + TableName + " where " + Where + " order by " + SortFieldName;
            }
        }

        public static string SelectID(string TableName, string Key, object ID)
        {
            return SelectWhere(TableName, Key + "='" + ID + "'", null, false);
        }
    }
}
