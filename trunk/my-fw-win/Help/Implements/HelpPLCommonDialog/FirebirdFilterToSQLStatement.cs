using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraEditors.Filtering;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp này mới xử lý 1 vài tình huống query thôi chứ chưa đầu đủ    
    /// </summary>
    public class FirebirdFilterToSQLStatement : FilterControlHelper
    {        
        public FirebirdFilterToSQLStatement(DevExpress.XtraEditors.FilterControl filterControl)
            :base(filterControl)
        {            
        }
    }
}
