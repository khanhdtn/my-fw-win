using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Nơi chưa delegation dùng chung trong chương trình
    /// </summary>
    [Obsolete("Không dùng")]
    public class PLDelegation
    {
        public delegate bool ProcessDataRow(DataRow data);    //Xử lý trên Hàng
        public delegate bool ProcessGridView(GridView table); //Xử lý trên Bảng
    }
}
