using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win
{
    public class GridInteraction
    {
        /// <summary>Hàm này chỉ dùng khi cột tính toán chỉ cần hiển thị không có lưu trữ 
        /// </summary>
        public static void AddCalc(GridView gridView, ref GridColumn ResultColumn, string[] FieldNames, ProtocolVN.Framework.Win.RowInteraction.GridColumnFunction func)
        {
            RowInteraction grid = new RowInteraction(gridView);
            gridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(grid.gridView_CustomUnboundColumnDataCalc);
            ResultColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            grid.AddCalcHelp(null, ResultColumn, FieldNames, func);
        }

        /// <summary> Hàm này chỉ sử dụng khi ResultFieldName là 1 field thật trong DataSource của Grid
        /// </summary>
        public static void AddCalc(GridView gridView, string ResultFieldName, string[] FieldNames, ProtocolVN.Framework.Win.RowInteraction.GridColumnFunction func)
        {
            RowInteraction grid = new RowInteraction(gridView);
            grid.AddCalcHelp(ResultFieldName, null, FieldNames, func);
        }
    }
}
