using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;
using System.Data;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class frmImportFirebird : frmImportIDB
    {
        private DatabaseFB db;
        private List<Array> lstRowError;
        private int rowError;

        public DataSet LoadDataSetTarget(string tablename)
        {
            return DABase.getDatabase().LoadTable(tablename);
        }
        public int UpdateTable(DataSet dataSet)
        {
            DbCommand sQLStringCommand = db.GetSQLStringCommand("SELECT * FROM " + dataSet.Tables[0].TableName);
            sQLStringCommand.Connection = db.OpenConnection();
            FbDataAdapter adapter = new FbDataAdapter();
            FbCommandBuilder builder = new FbCommandBuilder(adapter);
            adapter.SelectCommand = (FbCommand)sQLStringCommand;
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.RowUpdated += new FbRowUpdatedEventHandler(adapter_RowUpdated);
            return adapter.Update(dataSet.Tables[0]);
        }

        private void adapter_RowUpdated(object sender, FbRowUpdatedEventArgs e)
        {
            try
            {
                DataRow dr = e.Row;
                if (dr.HasErrors)
                {
                    dr.ClearErrors();
                    Object[] arrObj = new Object[dr.ItemArray.Length];
                    dr.ItemArray.CopyTo(arrObj, 0);
                    rowError++;
                    lstRowError.Add(arrObj);
                    dr.Delete();
                }
                else
                    dr["NEW_ROW"] = null;
            }
            catch { }
        }

        #region IDBfrmImport Members


        public void init(DatabaseFB db, List<Array> lstRowError, int rowError)
        {
            this.db = db;
            this.lstRowError = lstRowError;
            this.rowError = rowError;
        }

        public int getRowErrorAfterUpdateTable()
        {
            return rowError;
        }

        #endregion
    }
}
