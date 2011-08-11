using System;
using System.Data;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;

namespace ProtocolVN.Framework.Win
{
    public class VGridValidation
    {
        #region Thông báo lỗi trên VGrid
        /// <summary>Hiển thị thông báo lỗi trên lưới      
        /// </summary>        
        public static void ShowVGridError(VGridControl vgrid, string ErrorMsg)
        {
            if (vgrid.Controls.ContainsKey("picErrorIcon") == false)
            {
                Icon icon = System.Drawing.SystemIcons.Error;
                PictureBox pictureBoxTemp = new PictureBox();
                pictureBoxTemp.Name = "picErrorIcon";
                pictureBoxTemp.BackColor = Color.Transparent;
                pictureBoxTemp.Image = icon.ToBitmap();
                pictureBoxTemp.Location = new Point(vgrid.Width - 50, 3);
                vgrid.Controls.Add(pictureBoxTemp);

                System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
                toolTip1.SetToolTip(pictureBoxTemp, ErrorMsg);
            }
            else
            {
                vgrid.Controls["picErrorIcon"].Visible = true;  
                
            }
        }

        public static void ClearVGridError(VGridControl vgrid)
        {
            if (vgrid.Controls.ContainsKey("picErrorIcon") == true)
                vgrid.Controls["picErrorIcon"].Visible = false;
        }
        #endregion        

        #region ValidateRecord       
        public static void SetError(VGridControl vgrid, string FieldName, string ErrMsg)
        {
            SetError(vgrid, FieldName, ErrMsg, OutputError.ERROR_PROVIDER);
        }

        public static void SetError(VGridControl vgrid, string FieldName, string ErrMsg, OutputError Output)
        {
            switch (Output)
            {
                case OutputError.ERROR_PROVIDER:
                    vgrid.SetRowError(vgrid.GetRowByFieldName(FieldName).Properties, ErrMsg);
                    break;
            }
        }     

        public static void ClearError(VGridControl vgrid, string FieldName)
        {
            ClearError(vgrid, FieldName, OutputError.ERROR_PROVIDER);
        }

        public static void ClearError(VGridControl vgrid, string FieldName, OutputError Output)
        {
            switch (Output)
            {
                case OutputError.ERROR_PROVIDER:
                    vgrid.SetRowError(vgrid.GetRowByFieldName(FieldName).Properties, "");
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
        public static bool ValidateRecord(VGridControl vgrid, FieldNameCheck[] CheckList)
        {           
            if (CheckList == null) return true;
            bool flag = true;
            for (int i = 0; i < CheckList.Length; i++)
            {
                string FieldName = CheckList[i].FieldName;
                string Data = null;                
                BaseRow br = vgrid.GetRowByFieldName(FieldName);
                Data = vgrid.GetCellDisplayText(br, 0);                
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
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg); 
                                flag = false;
                            }
                            break;
                        case CheckType.DecGreater0:
                            if (HelpIsCheck.isDecGreater0(Data))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.DecGreaterEqual0:
                            if (HelpIsCheck.isDecGreaterEqual0(Data))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg); 
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Số nguyên
                        case CheckType.IntGreaterZero:
                            if (HelpIsCheck.isIntGreater0(Data))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.IntGreater0:
                            if (HelpIsCheck.isIntGreater0(Data))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.IntGreaterEqual0:
                            if (HelpIsCheck.isIntGreaterEqual0(Data))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Email
                        case CheckType.RequireEmail:
                            if (Data != null && Data != "" && HelpIsCheck.isValidEmail(Data))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.OptionEmail:
                            if (Data == null || Data == "" || HelpIsCheck.isValidEmail(Data))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Chuỗi
                        case CheckType.RequireMaxLength:
                            if (Data != null && Data != "" && HelpIsCheck.IsMaxLength(Data, (int)CheckList[i].Params[j]))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.OptionMaxLength:
                            if (Data == null || Data == "" || HelpIsCheck.IsMaxLength(Data, (int)CheckList[i].Params[j]))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Date
                        case CheckType.RequireDate:
                            if (Data != null && Data != "" && br.Properties.Value is DateTime && !HelpIsCheck.isBlankDate((DateTime)br.Properties.Value))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.OptionDate:
                            if (Data == null || Data == "" || br.Properties.Value is DateTime)
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.DateALessB:
                            DateTime B;
                            if (CheckList[i].Params[j] is DateTime)
                                B = (DateTime)CheckList[i].Params[j];
                            else                               
                                B = (DateTime)vgrid.GetRowByFieldName(CheckList[i].Params[j].ToString()).Properties.Value;                           
                            if (HelpIsCheck.IsALessB((DateTime)br.Properties.Value, B))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        case CheckType.DateALessEqualB:
                            DateTime C;
                            if (CheckList[i].Params[j] is DateTime) 
                                C = (DateTime)CheckList[i].Params[j];
                            else
                                C = (DateTime)vgrid.GetRowByFieldName(CheckList[i].Params[j].ToString()).Properties.Value;
                            if (HelpIsCheck.IsALessEqualB((DateTime)br.Properties.Value, C))
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Danh mục
                        case CheckType.RequiredID:
                            long ID = HelpNumber.DecimalToInt64(Data);
                            if (ID > 0)
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                SetError(vgrid, FieldName, ErrMsg);
                                flag = false;
                            }
                            break;
                        #endregion
                        #region 1. Other
                        case CheckType.Required:
                            if (Data != null && Data != "" && Data != "-1")
                            {
                                ClearError(vgrid, FieldName);
                            }
                            else
                            {
                                //PHUOCNT TODO Kiem tra lai
                                if (vgrid.GetCellValue(br, 0) == null)
                                {
                                    SetError(vgrid, FieldName, ErrMsg);
                                    flag = false;
                                }
                                else
                                {
                                    ClearError(vgrid, FieldName);
                                }
                            }
                            break;
                        #endregion
                    }

                    if (vgrid.HasRowErrors) break;                   
                }
            }
            return flag;
        }
        #endregion

        #region - Gắn xử lý thao tác không hợp lệ trên lưới
        private static void AllowValidateVGrid_Event(object sender, DevExpress.XtraVerticalGrid.Events.InvalidRecordExceptionEventArgs e)
        {
            if (e.ExceptionMode == DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError)
            {
                if (PLMessageBox.ShowConfirmMessage("Thông tin bạn vào không hợp lệ. Bạn có muốn vào lại thông tin ?") == DialogResult.Yes)
                {
                    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
                }
                else
                {
                    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;                    
                }
            }
        }
        public static void AllowValidateVGrid(VGridControl vgrid)
        {
            vgrid.InvalidRecordException += AllowValidateVGrid_Event;            
        }
        
        public static void NotAllowValidateVGrid(VGridControl vgrid)
        {
            vgrid.InvalidRecordException -= AllowValidateVGrid_Event;
        }
        #endregion
    }
}
