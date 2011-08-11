using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using System.Collections;

namespace ProtocolVN.Framework.Win
{
    public class HelpDBExt : HelpDB
    {
        /// <summary>
        /// Trả về DbType dựa vào value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbType GetDbType(object value)
        {
            if (value == null) return DbType.String;

            String type = value.GetType().FullName.ToString();

            if (type.Equals("System.DateTime"))
            {
                return DbType.DateTime;
            }
            else if (type.Equals("System.Int32"))
            {
                return DbType.Int32;
            }
            else if (type.Equals("System.Int64"))
            {
                return DbType.Int64;
            }
            else if (type.Equals("System.Decimal"))
            {
                return DbType.Decimal;
            }
            else
            {
                return DbType.String;
            }
        }

        /// <summary>
        /// Tạo ra danh sách các tham số từ 1 Dictionary
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<DbParameter> GenerateDbInputParam(Dictionary<string, object> parameters)
        {
            List<DbParameter> paramList = new List<DbParameter>();

            foreach (string keyName in parameters.Keys)
            {
                object objValue = parameters[keyName];

                DbParameter parameter = HelpDBExt.CreateParameter(keyName,
                    HelpDBExt.GetDbType(objValue), 0,
                    ParameterDirection.Input, false, 0, 0,
                    string.Empty, DataRowVersion.Default,
                    objValue);

                paramList.Add(parameter);
            }

            return paramList;
        }

        public static DbParameter CreateParameter(string name, DbType dbType, int size,
            ParameterDirection direction, bool nullable, byte precision, byte scale,
            string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            FbParameter param = new FbParameter();
            param.ParameterName = name;
            //param.set_FbDbType((FbDbType) dbType);
            param.Size = size;
            param.Value = (value == null) ? DBNull.Value : value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;

            return param;
        }

        //Load tất cả các table có trong database
        public static ArrayList GetFirebirdTableList()
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
