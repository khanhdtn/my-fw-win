using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Data;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lấy thông tin về hệ quản trị cơ sở dữ liệu Firebird
    /// </summary>
    public class HelpFbDbInfo
    {
        //Load tất cả các table có trong database
        public static ArrayList CreateListTable()
        {
            string command = @"SELECT RDB$RELATION_NAME
                               FROM RDB$RELATIONS
                               WHERE RDB$SYSTEM_FLAG = 0
                               --AND RDB$VIEW_BLR IS NULL
                               ORDER BY RDB$RELATION_NAME";
            ArrayList array = new ArrayList();
            DataSet dsTableName = DABase.getDatabase().LoadDataSet(command);
            foreach (DataRow row in dsTableName.Tables[0].Rows)
            {
                array.Add(row[0].ToString().Trim());
            }

            return array;
        }

        
    }
}
