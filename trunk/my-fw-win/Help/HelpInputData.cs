using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
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

    /// <summary>
    /// Lớp hỗ trợ xử lý kiểm tra dữ liệu trên các màn hình.
    /// </summary>
    [Obsolete("Sử dụng lớp HelpInputData để thay thế.")]
    public class GUIValidation
    {
        #region 1.Hạn chế lỗi - Xử lý dữ liệu thô
        /// <summary>1. Hạn chế số ký tự có thể nhập từ phiếu người dùng
        /// </summary>
        public static void SetMaxLength(object[] Ctrl)
        {
            CtrlValidation.SetMaxLength(Ctrl);
        }
        
        /// <summary>Cắt các khoản trắng cần thiết
        /// </summary>
        public static void TrimAllData(object[] Data)
        {            
            CtrlValidation.TrimAllData(Data);
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
        public static bool ShowRequiredError(DXErrorProvider Error, object[] DataNErrorMsg)
        {
            return CtrlValidation.ShowRequiredError(Error, DataNErrorMsg);
        }

        /// <summary>Kiểm tra trùng dữ liệu ở hai dòng
        /// </summary>
        public static bool CheckDuplicateField(GridView grid, DataSet GridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e, string[] FieldChecks, string[] Subjects)
        {
            return GridValidation.CheckDuplicateField(grid, GridDataSet, e, FieldChecks, Subjects);
        }

        /// <summary>Kiểm tra trùng dữ liệu ở hai dòng
        /// </summary>
        public static bool CheckDuplicateField(GridView grid, DataSet GridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e, string FieldCheck, string Subject)
        {
            string[] FieldChecks = new string[] { FieldCheck };
            string[] Subjects = new string[] { Subject };

            return GridValidation.CheckDuplicateField(grid, GridDataSet, e, FieldChecks, Subjects);
        }
        /// <summary>Kiểm tra và đánh dấu lỗi
        /// </summary>
        /// <returns></returns>
        public static bool ValidateRow(GridView Grid, DataRow Row, FieldNameCheck[] CheckList)
        {
            return GridValidation.ValidateRow(Grid, Row, CheckList);
        }
        /// <summary>Kiểm tra trên toàn grid
        /// </summary>
        public static bool ValidateGrid(GridView Grid, FieldNameCheck[] CheckList)
        {
            return GridValidation.ValidateGrid(Grid, CheckList);
        }

        #endregion
        
        #region 3.Thông báo lỗi
        /// <summary>Hỗ trợ Error Icon
        /// </summary>
        public static DXErrorProvider GetErrorProvider(XtraForm form)
        {
            DXErrorProvider dxErrorProvider1;
            dxErrorProvider1 = new DXErrorProvider(form);
            dxErrorProvider1.ContainerControl = form;
            return dxErrorProvider1;
        }
        #endregion
    }
}
