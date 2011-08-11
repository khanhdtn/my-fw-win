using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class HelpSummaries
    {
        #region DUYVT - Đưa vào FW
        /// <summary>
        /// Các dạng tính toán hổ trợ trên Grid
        /// </summary>
        public enum CalculationType
        {
            SUM,        //Tính tổng
            MIN,        //Tính min
            MAX,        //Tính max
            COUNT,      //Tính số phần tử
            AVERAGE     //Tính trung bình
        }

        /// <summary>
        /// Hiển thị thông tin tính toán (SUM, MIN, MAX, COUNT, AVERAGE) của 1 nhóm trên Grid
        /// + kiểm tra hợp lệ cột cần tính toán
        /// </summary>
        /// <param name="grid">GridView</param>
        /// <param name="column">GridColumn</param>
        public static void ShowGroupCalcInfo(GridView grid, GridColumn column, CalculationType calculationType)
        {
            if (calculationType == CalculationType.SUM)
            {
                if (checkValidate(column, CalculationType.SUM))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                        DevExpress.Data.SummaryItemType.Sum, column.FieldName, column,
                                        "SUM={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Tổng cộng> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.MIN)
            {
                if (checkValidate(column, CalculationType.MIN))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Min, column.FieldName, column,
                                       "MIN={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Giá trị nhỏ nhất> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.MAX)
            {
                if (checkValidate(column, CalculationType.MAX))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Max, column.FieldName, column,
                                       "MAX={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Giá trị lớn nhất> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.COUNT)
            {
                if (checkValidate(column, CalculationType.COUNT))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Count, column.FieldName, column,
                                       "NUM={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Số phần tử> không hợp lệ trên cột <" + column.Caption + ">.");
            }
            else if (calculationType == CalculationType.AVERAGE)
            {
                if (checkValidate(column, CalculationType.AVERAGE))
                {
                    //column.Group();
                    grid.GroupSummary.Add(new DevExpress.XtraGrid.GridGroupSummaryItem(
                                       DevExpress.Data.SummaryItemType.Average, column.FieldName, column,
                                       "AVG={0}"));
                }
                else PLMessageBox.ShowErrorMessage(
                    "Phép tính <Trung bình> không hợp lệ trên cột <" + column.Caption + ">.");
            }
        }

        /// <summary>
        /// Ẩn thông tin tính toán của 1 nhóm trên Grid
        /// </summary>
        /// <param name="grid">GridView</param>
        /// <param name="column">GridColumn</param>            
        public static void HideGroupCalcInfo(GridView grid, GridColumn column)
        {
            if (column.SummaryItem != null)
            {
                column.UnGroup();
                grid.GroupSummary.Remove(column.SummaryItem);
            }
        }

        /// <summary>
        /// Kiểm tra ràng buộc loại tính toán trên cột
        /// </summary>
        /// <param name="column">Cột check</param>
        /// <param name="calculationType">Loại tính toán</param>
        /// <returns></returns>
        private static bool checkValidate(GridColumn column, CalculationType calculationType)
        {
            //Nếu cột là kiểu Text hay DateTime thì không chấp nhận phép tính SUM và AVERAGE
            if ((column.ColumnEdit == null || column.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)
                && (calculationType == CalculationType.SUM || calculationType == CalculationType.AVERAGE))
                return false;
            return true;
        }
        #endregion
    }
}
