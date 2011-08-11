using System;
using System.Data;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp hỗ trợ kiểm tra dữ liệu trên lưới.
    /// </summary>
    [Obsolete("Sử dụng lớp HelpInputData để thay thế")]   
    public class GridValidation
    {
        #region Thông báo lỗi trên Grid
        /// <summary>Hiển thị thông báo lỗi trên lưới      
        /// </summary>        
        public static void ShowGridError(GridControl grid, string ErrorMsg)
        {
            if (grid.Controls.ContainsKey("picErrorIcon") == false)
            {
                Icon icon = System.Drawing.SystemIcons.Error;
                PictureBox pictureBoxTemp = new PictureBox();
                pictureBoxTemp.Name = "picErrorIcon";
                pictureBoxTemp.BackColor = Color.Transparent;
                pictureBoxTemp.Image = icon.ToBitmap();
                pictureBoxTemp.SizeMode = PictureBoxSizeMode.StretchImage;
                //pictureBoxTemp.Location = new Point(grid.Width - 50, 3);
                pictureBoxTemp.SetBounds(grid.Width - 30, 10, 20, 20);
                grid.Controls.Add(pictureBoxTemp);

                System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
                toolTip1.SetToolTip(pictureBoxTemp, ErrorMsg);
            }
            else
            {
                grid.Controls["picErrorIcon"].Visible = true;
            }
        }

        public static void ClearGridError(GridControl grid)
        {
            if (grid.Controls.ContainsKey("picErrorIcon") == true)
                grid.Controls["picErrorIcon"].Visible = false;
        }
        #endregion

        #region ValidateGrid
        public static bool ValidateGrid(GridView Grid, FieldNameCheck[] CheckList)
        {
            bool flag = true;
            DataView view = (DataView)Grid.DataSource;
            for (int i = 0; i < view.DataViewManager.DataSet.Tables[0].Rows.Count; i++)
            {
                if (view.DataViewManager.DataSet.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    if (!ValidateRow(Grid, view.DataViewManager.DataSet.Tables[0].Rows[i], CheckList))
                        flag = false;
                }
            }
            return flag;
        }
        #endregion

        #region ValidateRow
        public static void SetError(DataRow Row, string FieldName, string ErrMsg)
        {
            SetError(Row, FieldName, ErrMsg, OutputError.ERROR_PROVIDER);
        }

        public static void SetError(DataRow Row, string FieldName, string ErrMsg, OutputError Output)
        {
            switch (Output)
            {
                case OutputError.ERROR_PROVIDER:
                    Row.SetColumnError(FieldName, ErrMsg);
                    break;
            }
        }

        public static void ClearError(DataRow Row, string FieldName)
        {
            ClearError(Row, FieldName, OutputError.ERROR_PROVIDER);
        }

        public static void ClearError(DataRow Row, string FieldName, OutputError Output)
        {
            switch (Output)
            {
                case OutputError.ERROR_PROVIDER:
                    Row.SetColumnError(FieldName, "");
                    break;
            }
        }
        
        private static string GetErrMsg(CheckType Check, string Subject)
        {
            if (Subject == null)
            {
                PLMessageBoxDev.ShowMessage("Cấu hình kiểm tra lỗi bị sai.");
                return "ProtocolVN";
            }
            switch (Check)
            {
                case CheckType.IntALessB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.IntALessEqualB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.IntGreater0:
                    return ErrorMsgLib.errorGreater0(Subject);
                case CheckType.IntGreaterEqual0:
                    return ErrorMsgLib.errorGreaterEqual0(Subject);
                case CheckType.IntGreaterZero:
                    return ErrorMsgLib.errorGreater0(Subject);

                case CheckType.DecALessB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.DecALessEqualB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.DecGreater0:
                    return ErrorMsgLib.errorGreater0(Subject);
                case CheckType.DecGreaterEqual0:
                    return ErrorMsgLib.errorGreaterEqual0(Subject);
                case CheckType.DecGreaterZero:
                    return ErrorMsgLib.errorGreater0(Subject);

                case CheckType.DateALessB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.DateALessEqualB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.RequireDate:
                    return ErrorMsgLib.errorRequired(Subject);
                case CheckType.OptionDate:
                    return ErrorMsgLib.errorRequired(Subject);

                case CheckType.OptionEmail:
                    return ErrorMsgLib.errorEmail(Subject);
                case CheckType.RequireEmail:
                    return ErrorMsgLib.errorEmail(Subject);
                case CheckType.OptionMaxLength:
                    return ErrorMsgLib.errorMaxLength(Subject);
                case CheckType.RequireMaxLength:
                    return ErrorMsgLib.errorMaxLength(Subject);
                case CheckType.Required:
                    return ErrorMsgLib.errorRequired(Subject);
            }
            return Subject;
        }

        /// <summary>Kiểm tra và đánh dấu lỗi
        /// </summary>
        /// <returns></returns>
        public static bool ValidateRow(GridView Grid, DataRow Row, FieldNameCheck[] CheckList)
        {
            if (CheckList == null) return true;
            bool flag = true;
            for (int i = 0; i < CheckList.Length; i++)
            {
                string FieldName = CheckList[i].FieldName;
                string Data = null;
                if (Row[FieldName] != null) Data = Row[FieldName].ToString();
                for (int j = 0; j < CheckList[i].Types.Length; j++)
                {
                    string ErrMsg = "";
                    if (CheckList[i].Subject != null)
                        ErrMsg = GetErrMsg(CheckList[i].Types[j], CheckList[i].Subject);
                    else
                        ErrMsg = CheckList[i].ErrMsgs[j];
                    switch (CheckList[i].Types[j])
                    {
                        #region 1. Số thực
                        case CheckType.DecGreaterZero:
                            if (HelpIsCheck.isDecGreater0(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg); flag = false;
                            }
                            break;
                        case CheckType.DecGreater0:
                            if (HelpIsCheck.isDecGreater0(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg); flag = false;
                            }
                            break;
                        case CheckType.DecGreaterEqual0:
                            if (HelpIsCheck.isDecGreaterEqual0(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg); flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Số nguyên
                        case CheckType.IntGreaterZero:
                            if (HelpIsCheck.isIntGreater0(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.IntGreater0:
                            if (HelpIsCheck.isIntGreater0(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.IntGreaterEqual0:
                            if (HelpIsCheck.isIntGreaterEqual0(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Email
                        case CheckType.RequireEmail:
                            if (Data != null && Data != "" && HelpIsCheck.isValidEmail(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.OptionEmail:
                            if (Data == null || Data == "" || HelpIsCheck.isValidEmail(Data))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Chuỗi
                        case CheckType.RequireMaxLength:
                            if (Data != null && Data != "" && HelpIsCheck.IsMaxLength(Data, (int)CheckList[i].Params[j]))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                if (Data == String.Empty) ErrMsg = GetErrMsg(CheckType.Required, CheckList[i].Subject);
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.OptionMaxLength:
                            if (Data == null || Data == "" || HelpIsCheck.IsMaxLength(Data, (int)CheckList[i].Params[j]))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Date
                        case CheckType.RequireDate:
                            if (Data != null && Data != "" && Row[FieldName] is DateTime && !HelpIsCheck.isBlankDate((DateTime)Row[FieldName]))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                if (Data == String.Empty) ErrMsg = GetErrMsg(CheckType.Required, CheckList[i].Subject);
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.OptionDate:
                            if (Data == null || Data == "" || Row[FieldName] is DateTime)
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.DateALessB:
                            DateTime B;
                            if (CheckList[i].Params[j] is DateTime) B = (DateTime)CheckList[i].Params[j];
                            else B = (DateTime)Row[CheckList[i].Params[j].ToString()];
                            if (HelpIsCheck.IsALessB((DateTime)Row[FieldName], B))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.DateALessEqualB:
                            DateTime C;
                            if (CheckList[i].Params[j] is DateTime) C = (DateTime)CheckList[i].Params[j];
                            else C = (DateTime)Row[CheckList[i].Params[j].ToString()];
                            if (HelpIsCheck.IsALessEqualB((DateTime)Row[FieldName], C))
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion                        
                        #region 1. Danh mục
                        case CheckType.RequiredID:
                            long ID = HelpNumber.DecimalToInt64(Data);
                            if (ID > 0)
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Other
                        case CheckType.Required:
                            if (Data != null && Data != "" && Data != "-1")
                            {
                                ClearError(Row, FieldName);
                            }
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                    }

                    if (Row.HasErrors) break;
                }
            }
            return flag;
        }
        #endregion

        /// <summary>Kiểm tra Duplicate trên từng Field một ( Dùng cho 1 field unique ) 
        /// </summary>
        public static bool CheckDuplicateField(GridView grid, DataSet GridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e, string[] FieldChecks, string[] Subjects)
        {
            DataRow row = grid.GetDataRow(e.RowHandle);
            for (int j = 0; j < FieldChecks.Length; j++)
            {
                int count = 0;
                for (int i = 0; i < GridDataSet.Tables[0].Rows.Count; i++)
                {
                    if (GridDataSet.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (row[FieldChecks[j]] == null || row[FieldChecks[j]] == DBNull.Value)
                        {
                            //PHUOCNC : Tạm thời không xử lý
                        }
                        else
                        {
                            string tmp = row[FieldChecks[j]].ToString().Trim();
                            if (GridDataSet.Tables[0].Rows[i][FieldChecks[j]] != null &&
                                GridDataSet.Tables[0].Rows[i][FieldChecks[j]] != DBNull.Value &&
                                tmp == GridDataSet.Tables[0].Rows[i][FieldChecks[j]].ToString())
                            {
                                count++;
                                if (count >= 2) break;
                            }
                        }
                    }
                }

                if (row.RowState == DataRowState.Detached && count == 1)
                {
                    row.SetColumnError(FieldChecks[j], "Thông tin " + Subjects[j] + " đã sử dụng!");
                    e.Valid = false;
                }

                else if (count > 1)
                {
                    row.SetColumnError(FieldChecks[j], "Thông tin " + Subjects[j] + " đã sử dụng!");
                    e.Valid = false;
                }

                else
                {
                    row.SetColumnError(FieldChecks[j], "");
                }
            }
            return e.Valid;
        }

        /// <summary>Kiểm tra Duplicate trên 1 tổ hợp Field ( Dùng cho 2 field unique chính trở lên )
        /// </summary>
        public static bool CheckDuplicateAllField(GridView grid, DataSet GridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e, string[] FieldChecks, string[] Subjects)
        {
            DataRow row = grid.GetDataRow(e.RowHandle);
            int times = 0;
            for (int i = 0; i < GridDataSet.Tables[0].Rows.Count; i++)
            {
                if (GridDataSet.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    int count = 0;
                    for (int j = 0; j < FieldChecks.Length; j++)
                    {
                        if (row[FieldChecks[j]] == null || row[FieldChecks[j]] == DBNull.Value)
                        {
                            //PHUOCNC : Tạm thời không xử lý
                        }
                        else
                        {
                            string tmp = row[FieldChecks[j]].ToString().Trim();
                            if (GridDataSet.Tables[0].Rows[i][FieldChecks[j]] != null &&
                                GridDataSet.Tables[0].Rows[i][FieldChecks[j]] != DBNull.Value &&
                                tmp == GridDataSet.Tables[0].Rows[i][FieldChecks[j]].ToString())
                            {
                                count++;                                    
                            }
                        }
                    }
                    if(count == FieldChecks.Length) {
                        times++;
                        if (times >= 2) break;
                    }
                }
            }
            string error = "Thông tin tại cột (" + Subjects[0] ;
            for (int i = 1; i < Subjects.Length; i++)
            {
                error += "," + Subjects[i];
            }
            error += ") đã sử dụng !";

            for (int i = 0; i < Subjects.Length; i++)
            {
                if ((row.RowState == DataRowState.Detached && times == 1)||(times > 1))
                {
                    row.SetColumnError(FieldChecks[i], error);
                    e.Valid = false;
                }
                else
                {
                    row.SetColumnError(FieldChecks[i], "");
                }
            }
            
            return e.Valid;
        }

        #region - Gắn xử lý thao tác không hợp lệ trên lưới
        private static void AllowValidateGrid_Event(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            try
            {
                object errObj = TagPropertyMan.Get(((PLGridView)sender).Tag, "PLGridErrorMsg");
                string err = "Thông tin bạn vào không hợp lệ. Bạn có muốn vào lại thông tin ?";
                if (errObj != null)
                {
                    err = errObj.ToString();
                    TagPropertyMan.Remove(((PLGridView)sender).Tag, "PLGridErrorMsg");
                }
                if (e.ExceptionMode == DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError)
                {
                    if (PLMessageBox.ShowConfirmMessage(err) == DialogResult.Yes)
                    {
                        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
                    }
                    else
                    {
                        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
                        ((DataRowView)e.Row).Row.ClearErrors();
                    }
                }
            }
            catch { }
        }
        public static void AllowValidateGrid(GridView gridView)
        {
            gridView.InvalidRowException += AllowValidateGrid_Event;
        }
        public static void NotAllowValidateGrid(GridView gridView)
        {
            gridView.InvalidRowException -= AllowValidateGrid_Event;
        }
        #endregion
    }
}
