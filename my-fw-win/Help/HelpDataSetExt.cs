using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class HelpDataSetExt
    {
        public static string AddNewField( DataTable dt, string[] FieldNames)
        {
            if (dt == null) return null;
            if (FieldNames == null || FieldNames.Length == 0) return null;
            string RetFieldName = "_ZC" + FieldNames[0];
            DataColumn c = dt.Columns.Add(RetFieldName);            
            foreach (DataRow dr in dt.Rows)
            {
                StringBuilder builder_value = new StringBuilder();
                for (int i = 0; i < FieldNames.Length; i++)
                {
                    builder_value.Append(dr[FieldNames[i]]);
                    if (i < FieldNames.Length - 1) 
                        builder_value.Append(" ; ");
                }
                dr[RetFieldName] = builder_value.ToString();
            }
            
            return RetFieldName;
        }

        public static void GetDataRow(ref DataRow row, String Table, String KeyField, object ID, params string[] FieldNames)
        {
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectID(Table, KeyField, ID));
            if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
            {
                if (FieldNames != null) AddNewField(ds.Tables[0], FieldNames);

                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    try
                    {
                        row[row.Table.Columns[i]] = ds.Tables[0].Rows[0][row.Table.Columns[i].ColumnName];
                    }
                    catch { row[row.Table.Columns[i]] = DBNull.Value; }
                }
            }
            else
            {
                row = null;
            }
        }

        public static DataTable CreateDataTable(string[] FieldNames, object[] DataTypes, object[] Data)
        {
            try
            {
                DataTable dt = new DataTable();

                for (int item = 0; item < FieldNames.Length; item++)
                {
                    if (!dt.Columns.Contains(FieldNames[item]))
                    {
                        if (DataTypes[item].ToString() == "") dt.Columns.Add(FieldNames[item]);
                        else dt.Columns.Add(FieldNames[item], Type.GetType("System." + DataTypes[item] + ""));
                    }
                }

                for (int i = 0; i < Data.Length; i = i + dt.Columns.Count)
                {
                    object[] dataRow = new object[dt.Columns.Count];
                    for (int j = 0; j < dt.Columns.Count; j++)
                        dataRow[j] = Data[i + j];
                    dt.Rows.Add(dataRow);                    
                }
                return dt;
            }
            catch { return null; }
        }

        public static DataSet CreateDataSet(string[] FieldNames, object[] DataTypes, object[] Data)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(CreateDataTable(FieldNames, DataTypes,Data));
            return ds;
        }

        public static DataTable GetTable0(DataSet ds)
        {
            if (ds!= null && ds.Tables.Count > 0) return ds.Tables[0];
            PLException.AddException(new GDBTbException("Không xác định"));
            return null;
        }

        public static DataTable GetTable0(DataSet ds, String tableName)
        {
            DataTable dt = null;
            try{
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[tableName];
                    if (dt != null) return dt;                    
                }
            }
            catch {  }
            PLException.AddException(new GDBTbException(tableName));
            return null;
        }

        #region Đang collect
        //Lọc tất cả các cột trùng nhau và công tổng các cột số của các cột trùng
        public static void FilterRow(ref DataTable dt, string[] filedNumber, string[] CmpField)
        {
            int rowTotal = dt.Rows.Count;
            int i = 0;
            while (i < rowTotal)
            {
                try
                {
                    DataRow rowMain = dt.Rows[i];
                    //Loc ra cac row trung
                    string filterStr = "";
                    for (int j = 0; j < CmpField.Length; j++)
                    {
                        filterStr += CmpField[j] + "='" + rowMain[CmpField[j]] + "'";
                        filterStr += " and ";
                    }
                    filterStr = filterStr.Remove(filterStr.LastIndexOf(" and "));
                    DataRow[] rowDup = dt.Select(filterStr);
                    //Xu ly cac row trung nhau                   
                    for (int k = 1; k < rowDup.Length; k++)
                    {
                        DataRow rowProcess = rowDup[k];
                        foreach (string field in filedNumber)
                        {
                            try
                            {
                                long cal = long.Parse(rowMain[field].ToString());
                                cal += long.Parse(rowProcess[field].ToString());
                                rowMain[field] = cal;
                            }
                            catch { }
                        }
                        rowProcess.Delete();
                    }
                }
                catch { }
                i++;
            }
        }
        #endregion
    }
}
