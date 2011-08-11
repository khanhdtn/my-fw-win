using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid;

namespace ProtocolVN.Framework.Win
{
    public class CtrlValidation
    {
        public static void SetMaxLength(object[] Ctrl)
        {
            if (Ctrl == null) return;
            for (int i = 0; i < Ctrl.Length; i = i + 2)
            {
                if (Ctrl[i].GetType().FullName == typeof(TextEdit).FullName)
                {
                    TextEdit temp = Ctrl[i] as TextEdit;
                    temp.Properties.MaxLength = (int)Ctrl[i + 1];
                }
                else if (Ctrl[i].GetType().FullName == typeof(MemoEdit).FullName)
                {
                    MemoEdit temp = Ctrl[i] as MemoEdit;
                    temp.Properties.MaxLength = (int)Ctrl[i + 1];
                }
                else
                {
                    PLMessageBoxDev.ShowMessage("Không hỗ trợ cấu hình MaxLength trên control " + Ctrl[i].GetType().Name);
                }
            }
        }
        
        public static bool SetRequired(object[] DataNErrorMsg)
        {
            System.Drawing.Color MauBatBuoc = System.Drawing.Color.BlanchedAlmond;

            if (DataNErrorMsg == null) return true;
            for (int i = 0; i < DataNErrorMsg.Length; i = i + 2)
            {
                string ErrorMsg = ErrorMsgLib.errorRequired(DataNErrorMsg[i + 1].ToString());
                string CtrlName = DataNErrorMsg[i].GetType().FullName;
                #region Kiểu PLCombobox
                if (CtrlName == typeof(PLCombobox).FullName)
                {
                    PLCombobox ctrl = (PLCombobox)DataNErrorMsg[i];
                    ctrl._lookUpEdit.Properties.Appearance.BackColor = MauBatBuoc;
                }
                #endregion
                #region PLLookupEdit
                else if (CtrlName == typeof(PLLookupEdit).FullName)
                {
                    PLLookupEdit temp = (PLLookupEdit)DataNErrorMsg[i];
                    temp._lookUpEdit.Properties.Appearance.BackColor = MauBatBuoc;
                }
                #endregion
                //#region Kiểu PLComboboxAdd
                //else if (CtrlName == typeof(PLComboboxAdd).FullName)
                //{
                //    PLComboboxAdd ctrl = (PLComboboxAdd)DataNErrorMsg[i];
                //    ctrl.Properties.Appearance.BackColor = MauBatBuoc;
                //}
                //#endregion
                #region Kiểu PLDMTreeGroup
                else if (CtrlName == typeof(PLDMTreeGroup).FullName)
                {
                    PLDMTreeGroup ctrl = (PLDMTreeGroup)DataNErrorMsg[i];
                    ctrl.popupContainerEdit1.Properties.Appearance.BackColor = MauBatBuoc;
                }
                #endregion
                #region Kiểu PLDMTreeGroupElement
                else if (CtrlName == typeof(PLDMTreeGroupElement).FullName)
                {
                    PLDMTreeGroupElement ctrl = (PLDMTreeGroupElement)DataNErrorMsg[i];
                    ctrl.plGroupElement1.popUp.Properties.Appearance.BackColor = MauBatBuoc;
                }
                #endregion
                #region Kiểu DataSet
                else if (CtrlName == typeof(DataSet).FullName)
                //PHUOCNC : Thông báo lỗi trên Grid ở góc như Refresh của Hùng
                {

                }
                #endregion
                #region GridControl
                else if (CtrlName == typeof(GridControl).FullName)
                {

                }
                #endregion
                #region Kiểu Text & Memo
                else if (CtrlName == typeof(TextEdit).FullName || CtrlName == typeof(MemoEdit).FullName ||
                    CtrlName == typeof(DateEdit).FullName || CtrlName == typeof(CalcEdit).FullName || CtrlName == typeof(PLCalcEdit).FullName ||
                    CtrlName == typeof(SpinEdit).FullName || CtrlName == typeof(PLSpinEdit).FullName || CtrlName == typeof(PLComboboxAdd).FullName)
                //Kiểu chuỗi
                {
                    TextEdit temp = (TextEdit)DataNErrorMsg[i];
                    temp.Properties.Appearance.BackColor = MauBatBuoc;
                }
                #endregion
                else
                {
                    PLMessageBoxDev.ShowMessage("Chưa hỗ trợ trên control " + CtrlName);
                }
            }
            return true;
        }
        
        
        /// <summary> Hiển thị thông tin lỗi trên các control
        /// True: nếu không có bất cứ lỗi nào
        /// False: nếu có bất cứ lỗi nào
        /// Chú ý: Sẽ gặp vấn đè khi nâng cấp DEVEXPRESS HAS NEW CONTROL        
        /// </summary>
        public static bool ShowRequiredError(DXErrorProvider Error, object[] DataNErrorMsg)
        {
            bool flag = true;
            if (DataNErrorMsg == null) return true;
            for (int i = 0; i < DataNErrorMsg.Length; i = i + 2)
            {
                string ErrorMsg = ErrorMsgLib.errorRequired(DataNErrorMsg[i + 1].ToString());
                string CtrlName = DataNErrorMsg[i].GetType().FullName;

                if (CtrlName == typeof(PLCombobox).FullName || CtrlName == typeof(PLComboboxAdd).FullName ||
                    CtrlName == typeof(PLDMTreeGroup).FullName || CtrlName == typeof(PLDMTreeGroupElement).FullName)
                #region Kiểu ID
                {
                    IIDValidation ctrl = (IIDValidation)DataNErrorMsg[i];
                    if (HelpIsCheck.isBlankID("" + ctrl._getSelectedID()))
                    {
                        ctrl.SetError(Error, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region Kiểu DataSet
                else if (CtrlName == typeof(DataSet).FullName)
                //PHUOCNC : Thông báo lỗi trên Grid ở góc như Refresh của Hùng
                {
                    DataSet temp = DataNErrorMsg[i] as DataSet;
                    if (HelpIsCheck.IsBlankDataSet(temp))
                    {
                        PLMessageBox.ShowErrorMessage(ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region GridControl
                else if (CtrlName == typeof(GridControl).FullName)
                {
                    GridControl temp = DataNErrorMsg[i] as GridControl;
                    DataSet tmpDS = ((DataTable)temp.DataSource).DataSet;
                    if (HelpIsCheck.IsBlankDataSet(tmpDS))
                    {
                        try { GridValidation.ShowGridError(temp, ErrorMsg); }
                        catch { PLMessageBox.ShowErrorMessage(ErrorMsg); }
                        flag = false;
                    }
                    else
                    {
                        try { GridValidation.ClearGridError(temp); }
                        catch { }
                    }
                }
                #endregion
                #region Kiểu CalcEdit
                else if (CtrlName == typeof(CalcEdit).FullName || CtrlName == typeof(PLCalcEdit).FullName)
                //Kiểu số
                {
                    CalcEdit temp = DataNErrorMsg[i] as CalcEdit;
                    if (HelpIsCheck.isBlankString(temp.Text))
                    {
                        Error.SetError(temp, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region Kiểu SpinEdit
                else if (CtrlName == typeof(SpinEdit).FullName || CtrlName == typeof(PLSpinEdit).FullName)
                //Kiểu số
                {
                    SpinEdit temp = DataNErrorMsg[i] as SpinEdit;
                    if (HelpIsCheck.isBlankString(temp.Text))
                    {
                        Error.SetError(temp, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region Kiểu DateEdit
                else if (CtrlName == typeof(DateEdit).FullName)
                //Kiểu ngày
                {
                    DateEdit temp = (DateEdit)DataNErrorMsg[i];
                    if (HelpIsCheck.isBlankDate(temp.DateTime))
                    {
                        Error.SetError(temp, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region PLDateTime
                else if (CtrlName == typeof(PLDateTime).FullName)
                {
                    PLDateTime temp = (PLDateTime)DataNErrorMsg[i];
                    if (temp._getDate() == null || temp._getTime() == null)
                    {
                        temp.SetError(Error, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region PLLookupEdit
                else if (CtrlName == typeof(PLLookupEdit).FullName)
                {
                    PLLookupEdit temp = (PLLookupEdit)DataNErrorMsg[i];
                    if (temp._getSelectedID() == -1)
                    {
                        Error.SetError(temp, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region Kiểu Text & Memo
                else if (CtrlName == typeof(TextEdit).FullName || CtrlName == typeof(MemoEdit).FullName)
                //Kiểu chuỗi
                {
                    TextEdit temp = (TextEdit)DataNErrorMsg[i];
                    if (HelpIsCheck.isBlankString(temp.Text))
                    {
                        Error.SetError(temp, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region PLDMGrid
                else if (CtrlName == typeof(PLDMGrid).FullName)
                {
                    PLDMGrid temp = (PLDMGrid)DataNErrorMsg[i];
                    if (temp._getSelectedID() == -1)
                    {
                        temp.SetError(Error, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region PLMoneyType
                else if (CtrlName == typeof(PLMoneyType).FullName)
                {
                    PLMoneyType temp = (PLMoneyType)DataNErrorMsg[i];
                    if (temp._getSelectedTienTeID() == -1)
                    {
                        temp.SetError(Error, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                #region PLEditor
                else if (CtrlName == typeof(PLEditor).FullName)
                {
                    PLEditor temp = (PLEditor)DataNErrorMsg[i];
                    if (HelpIsCheck.isBlankString(temp._getHTMLText()))
                    {
                        Error.SetError(temp, ErrorMsg);
                        flag = false;
                    }
                }
                #endregion
                else
                {
                    PLMessageBoxDev.ShowMessage("Chưa hỗ trợ kiểm tra Required trên control " + CtrlName);
                }
            }
            return flag;
        }
        
        public static void TrimAllData(object[] Data)
        {
            if (Data == null) return;
            for (int i = 0; i < Data.Length; i++)
            {
                try
                {
                    string temp = Data[i].GetType().GetProperty("Text").GetValue(Data[i], null).ToString();
                    Data[i].GetType().GetProperty("Text").SetValue(Data[i], temp.Trim(), null);
                }
                catch
                {
                    PLMessageBoxDev.ShowMessage("Chưa hỗ trợ cấu hình TRIM DATA trên control " + Data[i].GetType().Name);                    
                }
            }
        }
    }
}
