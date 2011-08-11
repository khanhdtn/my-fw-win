using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public interface frmImportIDB
    {
        DataSet LoadDataSetTarget(string tablename);
        int UpdateTable(DataSet dataSet);

        void init(DatabaseFB db, List<Array> lstRowError, int rowError);
        int getRowErrorAfterUpdateTable();
    }
}
