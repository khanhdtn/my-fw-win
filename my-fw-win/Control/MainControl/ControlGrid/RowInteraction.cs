using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{    
    public class RowInteraction
    {
        public delegate object GridColumnFunction(DataRow gridView, string[] FieldNames);
        public delegate object[] GetInfo(object Key);
        public delegate void UpdateRow(DataRow row);
        private GridView gridView;

        public RowInteraction(GridView gridView)
        {
            this.gridView = gridView;
        }

        private string ResultFieldName;
        private string[] FieldNames;
        private GridColumnFunction func;
        private GridColumn ResultColumn;

        public void gridView_CustomUnboundColumnDataCalc(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (ResultColumn != null && ResultColumn.FieldName.Equals(e.Column.FieldName))
            {
                GridView grid = (GridView)sender;
                DataRow row = grid.GetDataRow(e.RowHandle);
                e.Value = func(row, this.FieldNames);
            }
        }

        public void AddCalcHelp(string ResultFieldName, GridColumn gridColumn, string[] FieldNames, GridColumnFunction func)
        {
            this.ResultFieldName = ResultFieldName;
            this.FieldNames = FieldNames;
            this.ResultColumn = gridColumn;

            this.func = func;
            this.gridView.CellValueChanged += new CellValueChangedEventHandler(gridView_CellValueChanged);
        }

        private void gridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                GridView grid = (GridView)sender;
                DataRow row = grid.GetDataRow(grid.FocusedRowHandle);
                for (int i = 0; i < FieldNames.Length; i++)
                {
                    //Chỉ cập nhât những cell liên quan
                    if (e.Column.FieldName.Equals(FieldNames[i]))
                    {
                        if (ResultFieldName != null)
                        {
                            //grid.SetRowCellValue(grid.FocusedRowHandle, ResultFieldName,
                            //    HelpNumber.ParseDecimal(func(row, this.FieldNames)));
                            grid.SetRowCellValue(grid.FocusedRowHandle, ResultFieldName,
                                func(row, this.FieldNames));
                        }
                        else
                        {
                            //Dữ liệu được tính toán theo hướng Custom Unbound Column
                            //grid.SetRowCellValue(grid.FocusedRowHandle, ResultColumn, 0);
                            grid.SetRowCellValue(grid.FocusedRowHandle, ResultColumn, 0);
                        }

                        break;
                    }
                }
            }
            catch { }
        }

        #region Hàm Tương tác Static
        #region Ví dụ Mã hàng hóa -> Tên hàng hóa, đơn giá, giảm giá ...
        /// <summary>Phương thức cho phép thêm chức năng vào cho Cột
        /// Khi thay đổi giá trị của cột nó làm ảnh hưởng giá trị của các
        /// cột khác
        /// </summary>
        /// <param name="Column">Cột mã hàng</param>
        /// <param name="FieldName">FieldName của Tên hàng hóa, đơn giá, giảm giá ...</param>
        /// <param name="func">Hàm gán giá trị cho các field</param>
        public static void AddCalcGridColum(GridView Grid, GridColumn Column, string[] FieldNames, GetInfo Func)
        {
            Grid.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                try
                {
                    GridView grid = (GridView)sender;
                    DataRow row = grid.GetDataRow(grid.FocusedRowHandle);
                    if (e.Column.FieldName.Equals(Column.FieldName))
                    {
                        object[] Values = Func(row[Column.FieldName]);
                        if (Values != null)
                        {
                            for (int i = 0; i < FieldNames.Length; i++)
                            {
                                grid.SetRowCellValue(grid.FocusedRowHandle, FieldNames[i], Values[i]);
                            }
                        }
                        else
                        {
                            grid.DeleteRow(grid.FocusedRowHandle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            };
        }

        public static void AddCalcGridColumn(GridView Grid, GridColumn Column, string[] FieldNames, UpdateRow Func)
        {
            Grid.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                try
                {
                    GridView grid = (GridView)sender;
                    DataRow row = grid.GetDataRow(grid.FocusedRowHandle);
                    if (e.Column.FieldName.Equals(Column.FieldName))
                    {
                        Func(row);
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            };
        }
        #endregion
        #endregion

    }

    public class RowInteractionFunc {
        
        public static object MulGridColumnFunction(DataRow row, string[] FieldNames)
        {
            decimal ret = 1;
            try
            {
                for (int i = 0; i < FieldNames.Length; i++)
                {
                    if (FieldNames[i].ToString() == "VAT")
                    {
                        if (row["VAT"] == DBNull.Value || row["VAT"].ToString() == "")
                        {
                            //NOOP
                        }
                        else
                        {
                            decimal VAT = Decimal.Parse(row["VAT"].ToString());
                            if (VAT != 0 && VAT != 5 && VAT != 10)
                            {
                                row["VAT"] = 10;
                                VAT = 10;
                            }
                            ret *= (1 + (VAT / 100));
                        }
                    }
                    else
                        ret *= Decimal.Parse(row[FieldNames[i].ToString()].ToString());
                }

            }
            catch
            {
                return null;
            }
            return ret;
        }
        public static object AddGridColumnFunction(DataRow row, string[] FieldNames)
        {
            decimal ret = 0;
            try
            {
                for (int i = 0; i < FieldNames.Length; i++)
                {
                    ret += Decimal.Parse(row[FieldNames[i].ToString()].ToString());
                }
            }
            catch
            {
                return null;
            }
            return ret;
        }
    }
}
