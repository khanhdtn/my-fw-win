using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp hỗ trợ xử lý dữ liệu trước khi lưu trữ.
    /// </summary>
    public class HelpInputData : GUIValidation
    {

    }

    class HelpInputDataImpl : HelpInputData
    {
    }

    /// <summary>
    /// Lớp hỗ trợ xử lý kiểm tra dữ liệu trên các màn hình.
    /// </summary>
    [Obsolete("Sử dụng lớp HelpInputData để thay thế.")]
    public class GUIValidation
    {
        #region 1.Hạn chế lỗi - Xử lý dữ liệu thô
        /// <summary>1. Hạn chế số ký tự có thể nhập từ phiếu người dùng
        /// </summary>
        public static void SetMaxLength(object[] ctrl)
        {
            CtrlValidation.SetMaxLength(ctrl);
        }
        
        /// <summary>Cắt các khoản trắng cần thiết
        /// </summary>
        public static void TrimAllData(object[] data)
        {            
            CtrlValidation.TrimAllData(data);
        }

        public static void TrimAllData(DataRow row)
        {
            if (row != null)
            {
                object[] rets = row.ItemArray;
                for (int i = 0; i < rets.Length; i++)
                {
                    if (rets[i].GetType().FullName.Equals("System.String"))
                    {
                        //if(rets[i]!=null) rets[i] = ((String)rets[i]).Trim();
                        row[i] = row.ItemArray[i].ToString().Trim();
                    }
                }
            }
        }

        #endregion

        #region 2.Kiểm tra lỗi
        /// <summary>Kiểm tra và đưa thông báo lỗi tương ứng 
        /// True: Nếu ko có bất cứ lỗi nào
        /// False: Nếu có bất kỳ lỗi nào.
        /// </summary>
        public static bool ShowRequiredError(DXErrorProvider error, object[] dataNErrorMsg)
        {
            return CtrlValidation.ShowRequiredError(error, dataNErrorMsg);
        }

        /// <summary>Kiểm tra trùng dữ liệu ở hai dòng
        /// </summary>
        public static bool CheckDuplicateField(GridView grid, DataSet gridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e, string[] fieldChecks, string[] subjects)
        {
            return GridValidation.CheckDuplicateField(grid, gridDataSet, e, fieldChecks, subjects);
        }

        /// <summary>Kiểm tra trùng dữ liệu ở hai dòng
        /// </summary>
        public static bool CheckDuplicateField(GridView grid, DataSet gridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e, string fieldCheck, string subject)
        {
            var fieldChecks = new[] { fieldCheck };
            var subjects = new[] { subject };

            return GridValidation.CheckDuplicateField(grid, gridDataSet, e, fieldChecks, subjects);
        }
        /// <summary>Kiểm tra và đánh dấu lỗi
        /// </summary>
        /// <returns></returns>
        public static bool ValidateRow(GridView grid, DataRow row, FieldNameCheck[] checkList)
        {
            return GridValidation.ValidateRow(grid, row, checkList);
        }
        /// <summary>Kiểm tra trên toàn grid
        /// </summary>
        public static bool ValidateGrid(GridView grid, FieldNameCheck[] checkList)
        {
            return GridValidation.ValidateGrid(grid, checkList);
        }

        #endregion
        
        #region 3.Thông báo lỗi
        /// <summary>Hỗ trợ Error Icon
        /// </summary>
        public static DXErrorProvider GetErrorProvider(XtraForm form)
        {
            var dxErrorProvider1 = new DXErrorProvider(form) {ContainerControl = form};
            return dxErrorProvider1;
        }
        public static void ShowGridError(GridControl grid, string errorMsg)
        {
            GridValidation.ShowGridError(grid, errorMsg);
        }

        #endregion
    }
}
