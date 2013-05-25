using System.Data;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPivotGrid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// DUYVT_15.03.10: Trợ giúp khởi tạo Field trên PivotGrid
    /// </summary>
    public class HelpPivotGridField
    {
        #region "Row Area"
        public static void FieldRow(PivotGridField Field, string FieldName, string Caption,
            int VisibleIndex, int Width)
        {
            SetField(Field);
            Field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            Field.AreaIndex = VisibleIndex;
            Field.Caption = Caption;
            Field.FieldName = FieldName;
            Field.Width = Width;
          
        }
        #endregion

        #region "Data Area"

        #region "Text"
        public static void FieldDataText(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, HorzAlignment Alignment)
        {
            SetHorzAlignment(Field, Alignment);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
        }

        public static void FieldDataTextCenter(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            FieldDataText(Field, FieldName, Caption, VisibleIndex, HorzAlignment.Center);
        }

        public static void FieldDataTextLeft(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            FieldDataText(Field, FieldName, Caption, VisibleIndex, HorzAlignment.Near);
        }

        public static void FieldDataTextRight(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            FieldDataText(Field, FieldName, Caption, VisibleIndex, HorzAlignment.Far);
        }
        #endregion

        #region "Memo"
        public static RepositoryItemMemoEdit FieldMemoEdit(PivotGridField Field, string FieldName, 
            string Caption, int VisibleIndex)
        {
            RepositoryItemMemoEdit edit = new RepositoryItemMemoEdit();
            edit.BeginInit();
            Field.FieldEdit = edit;
            edit.Name = "MemoEditField";
            Field.Area = PivotArea.DataArea;
            Field.FieldName = FieldName;
            Field.Caption = Caption;
            Field.Index = VisibleIndex;
            edit.EndInit();
            edit.LinesCount = 2;
            edit.AutoHeight = true;
            return edit;
        }

        public static RepositoryItemMemoExEdit FieldMemoExEdit(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            SetHorzAlignment(Field, HorzAlignment.Near);
            Field.FieldEdit = HelpRepository.GetMemoExEdit();
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            return (RepositoryItemMemoExEdit)Field.FieldEdit;
        }
        #endregion

        #region "Calc"
        public static RepositoryItemCalcEdit FieldCalcEdit(PivotGridField Field, string FieldName, 
            string Caption, int VisibleIndex, int SoThapPhan)
        {
            SetHorzAlignment(Field, HorzAlignment.Far);
            SetSummaryNumFormat(Field, SoThapPhan);
            Field.FieldEdit = HelpRepository.GetCalcEdit(SoThapPhan);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            return (RepositoryItemCalcEdit)Field.FieldEdit;
        }

        public static RepositoryItemCalcEdit FieldCalcEditDec(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int SoThapPhan, bool AllowNULL)
        {
            return FieldCalcEditDec(Field, FieldName, Caption, VisibleIndex, SoThapPhan,
                decimal.MinValue, decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemCalcEdit FieldCalcEditDec(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int SoThapPhan, decimal Min, bool AllowNULL)
        {
            return FieldCalcEditDec(Field, FieldName, Caption, VisibleIndex, SoThapPhan, Min,
                decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemCalcEdit FieldCalcEditDec(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int SoThapPhan, decimal Min, decimal Max, bool AllowNULL)
        {
            RepositoryItemCalcEdit element = FieldCalcEditInt(Field, FieldName, Caption, 
                VisibleIndex, Min, Max, AllowNULL);
            ApplyFormatAction.applyElement(element, SoThapPhan);
            SetSummaryNumFormat(Field, SoThapPhan);
            return element;
        }

        public static RepositoryItemCalcEdit FieldCalcEditInt(PivotGridField Field, 
            string FieldName, string Caption, int VisibleIndex, bool AllowNULL)
        {
            return FieldCalcEditInt(Field, FieldName, Caption, VisibleIndex,
                decimal.MinValue, decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemCalcEdit FieldCalcEditInt(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, decimal Min, bool AllowNULL)
        {
            return FieldCalcEditInt(Field, FieldName, Caption, VisibleIndex, Min, decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemCalcEdit FieldCalcEditInt(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, decimal Min, decimal Max, bool AllowNULL)
        {
            SetHorzAlignment(Field, HorzAlignment.Far);
            SetSummaryNumFormat(Field, 0);
            RepositoryItemCalcEdit calcEdit = HelpRepository.GetCalcEdit(-1);
            Field.FieldEdit = calcEdit;
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            if ((Min != decimal.MinValue) || (Max != decimal.MaxValue))
            {
                if (Min >= 0)
                {
                    calcEdit.KeyPress += new KeyPressEventHandler(delegate(object sender, KeyPressEventArgs e)
                    {
                        if (e.KeyChar.Equals('-'))
                        {
                            e.Handled = true;
                        }
                    });
                }
                calcEdit.ParseEditValue += new ConvertEditValueEventHandler(delegate(object sender, ConvertEditValueEventArgs e)
                {
                    CalcEdit edit = sender as CalcEdit;
                    if (edit.EditValue == null)
                    {
                        if (!AllowNULL)
                        {
                            e.Value = Min;
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        decimal num = decimal.MinValue;
                        try
                        {
                            num = decimal.Parse(edit.EditValue.ToString());
                        }
                        catch
                        {
                            return;
                        }
                        if (num < Min)
                        {
                            e.Value = Min;
                            e.Handled = true;
                        }
                        else if (num > Max)
                        {
                            e.Value = Max;
                            e.Handled = true;
                        }
                    }
                });
            }
            return calcEdit;
        }
        #endregion

        #region "Spin"
        public static RepositoryItemSpinEdit FieldSpinEdit(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int SoThapPhan)
        {
            SetHorzAlignment(Field, HorzAlignment.Far);
            SetSummaryNumFormat(Field, SoThapPhan);
            Field.FieldEdit = HelpRepository.GetSpinEdit(SoThapPhan);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            return (RepositoryItemSpinEdit)Field.FieldEdit;
        }

        public static RepositoryItemSpinEdit FieldSpinEditDec(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int SoThapPhan, bool AllowNULL)
        {
            return FieldSpinEditDec(Field, FieldName, Caption, VisibleIndex, SoThapPhan, 
                decimal.MinValue, decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemSpinEdit FieldSpinEditDec(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int SoThapPhan, decimal Min, bool AllowNULL)
        {
            return FieldSpinEditDec(Field, FieldName, Caption, VisibleIndex, SoThapPhan, 
                Min, decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemSpinEdit FieldSpinEditDec(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int SoThapPhan, decimal Min, decimal Max, bool AllowNULL)
        {
            RepositoryItemSpinEdit element = FieldSpinEditInt(Field, FieldName, Caption, VisibleIndex,
                Min, Max, AllowNULL);
            ApplyFormatAction.applyElement(element, SoThapPhan);
            SetSummaryNumFormat(Field, SoThapPhan);
            return element;
        }

        public static RepositoryItemSpinEdit FieldSpinEditInt(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, bool AllowNULL)
        {
            return FieldSpinEditInt(Field, FieldName, Caption, VisibleIndex,
                decimal.MinValue, decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemSpinEdit FieldSpinEditInt(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, decimal Min, bool AllowNULL)
        {
            return FieldSpinEditInt(Field, FieldName, Caption, VisibleIndex,
                Min, decimal.MaxValue, AllowNULL);
        }

        public static RepositoryItemSpinEdit FieldSpinEditInt(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, decimal Min, decimal Max, bool AllowNULL)
        {
            SetHorzAlignment(Field, HorzAlignment.Far);
            SetSummaryNumFormat(Field, 0);
            RepositoryItemSpinEdit spinEdit = HelpRepository.GetSpinEdit(-1);
            Field.FieldEdit = spinEdit;
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            if ((Min != decimal.MinValue) || (Max != decimal.MaxValue))
            {
                if (Min >= 0)
                {
                    spinEdit.KeyPress += new KeyPressEventHandler(delegate(object sender, KeyPressEventArgs e)
                    {
                        if (e.KeyChar.Equals('-'))
                        {
                            e.Handled = true;
                        }
                    });
                }
                spinEdit.Spin += new SpinEventHandler(delegate(object sender, SpinEventArgs e)
                {
                    BaseEdit edit = sender as BaseEdit;
                    if (edit.EditValue != null)
                    {
                        try
                        {
                            if (((decimal)edit.EditValue) == Min)
                            {
                                if (!e.IsSpinUp)
                                {
                                    e.Handled = true;
                                }
                            }
                            else if ((((decimal)edit.EditValue) == Max) && e.IsSpinUp)
                            {
                                e.Handled = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                });

                spinEdit.ParseEditValue += new ConvertEditValueEventHandler(delegate(object sender, ConvertEditValueEventArgs e)
                {
                    BaseEdit edit = sender as BaseEdit;
                    if (edit.EditValue == null)
                    {
                        if (!AllowNULL)
                        {
                            e.Value = Min;
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (((decimal)edit.EditValue) < Min)
                            {
                                e.Value = Min;
                                e.Handled = true;
                            }
                            else if (((decimal)edit.EditValue) > Max)
                            {
                                e.Value = Max;
                                e.Handled = true; ;
                            }
                        }
                        catch
                        {
                        }
                    }
                });
            }
            return spinEdit;
        }
        #endregion

        #region "Date"
        public static RepositoryItemDateEdit FieldDateEdit(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            return FieldDateEdit(Field, FieldName, Caption, VisibleIndex, "d");
        }

        public static RepositoryItemDateEdit FieldDateEdit(PivotGridField Field,
            string FieldName, string Caption, int VisibleIndex, string Format)
        {
            SetHorzAlignment(Field, HorzAlignment.Center);
            SetDateDisplayFormat(Field, Format);
            Field.FieldEdit = HelpRepository.GetDateEdit(Format);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            return (RepositoryItemDateEdit)Field.FieldEdit;
        }

        public static void FieldReadOnlyDate(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            FieldReadOnlyDate(Field, FieldName, Caption, VisibleIndex, "d");
        }

        public static void FieldReadOnlyDate(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, string Format)
        {
            SetHorzAlignment(Field, HorzAlignment.Center);
            SetDateDisplayFormat(Field, Format);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
        }
        #endregion

        #region "Time"
        public static RepositoryItemTimeEdit FieldPLTimeEdit(PivotGridField Field,
            string FieldName, string Caption, int VisibleIndex)
        {
            return FieldPLTimeEdit(Field, FieldName, Caption, VisibleIndex, null);
        }

        public static RepositoryItemTimeEdit FieldPLTimeEdit(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, string Format)
        {
            SetHorzAlignment(Field, HorzAlignment.Center);
            Field.FieldEdit = HelpRepository.GetCotPLTimeEdit(Format);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            return (RepositoryItemTimeEdit)Field.FieldEdit;
        }
        #endregion

        #region "Check"
        public static RepositoryItemCheckEdit FieldCheckEdit(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            SetHorzAlignment(Field, HorzAlignment.Center);
            Field.FieldEdit = HelpRepository.GetCheckEdit(false);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            return (RepositoryItemCheckEdit)Field.FieldEdit;
        }
        #endregion

        #region "ComboBox"
        public static RepositoryItemLookUpEdit FieldCombobox(PivotGridField Field, DataSet ds,
            string IDField, string DisplayField, string FieldName, string Caption, int VisibleIndex)
        {
            RepositoryItemLookUpEdit edit = FieldPLLookUp(Field, IDField, DisplayField,
                ds.Tables[0], new string[] { DisplayField }, new string[] { "Caption" },
                FieldName, Caption, VisibleIndex, new int[] { Field.Width });
            edit.ShowHeader = false;
            return edit;
        }

        public static RepositoryItemLookUpEdit FieldCombobox(PivotGridField Field, string LookupTable,
            string IDField, string DisplayField, string FieldName, string Caption, int VisibleIndex)
        {
            RepositoryItemLookUpEdit edit = FieldLookUp(Field, IDField, DisplayField,
                LookupTable, new string[] { DisplayField }, new string[] { "Caption" },
                FieldName, Caption, VisibleIndex, new int[] { Field.Width });
            edit.ShowHeader = false;
            return edit;
        }

        public static RepositoryItemLookUpEdit FieldCombobox(PivotGridField Field, string LookupTable,
            string IDField, string DisplayField, string FieldName, string Caption,
            int VisibleIndex, string where)
        {
            DataSet ds = DatabaseFBExt.LoadTable(LookupTable, where, DisplayField, true);
            return FieldCombobox(Field, ds, IDField, DisplayField, FieldName, Caption, VisibleIndex);
        }
        #endregion

        #region "Lookup"
        public static RepositoryItemLookUpEdit FieldLookUp(PivotGridField Field, string IDField,
            string DisplayField, string LookupTable, string[] LookupVisibleFields,
            string[] Captions, string FieldName, string Caption, int VisibleIndex, int[] Widths)
        {
            return FieldLookUp(Field, IDField, DisplayField, LookupTable, LookupVisibleFields,
                Captions, FieldName, Caption, VisibleIndex, Widths, false, false);
        }

        public static RepositoryItemLookUpEdit FieldLookUp(PivotGridField Field, string IDField,
            string DisplayField, string LookupTable, string[] LookupVisibleFields,
            string[] Captions, string FieldName, string Caption, int VisibleIndex,
            int[] Widths, bool UsingVisible)
        {
            return FieldLookUp(Field, IDField, DisplayField, LookupTable, LookupVisibleFields,
                Captions, FieldName, Caption, VisibleIndex, Widths, UsingVisible, false);
        }

        public static RepositoryItemLookUpEdit FieldLookUp(PivotGridField Field, string IDField,
            string DisplayField, string LookupTable, string[] LookupVisibleFields,
            string[] Captions, string FieldName, string Caption, int VisibleIndex, int[] Widths,
            bool UsingVisible, bool AllowBlank)
        {
            DataTable dataLookup = null;
            if (!UsingVisible)
            {
                dataLookup = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(LookupTable, 
                    LookupVisibleFields[0], true)).Tables[0];
            }
            else
            {
                dataLookup = DABase.getDatabase().LoadDataSet(HelpSQL.SelectWhere(
                    LookupTable, "VISIBLE_BIT = 'Y'", LookupVisibleFields[0], true)).Tables[0];
            }
            return FieldPLLookUp(Field, IDField, DisplayField, dataLookup,
                LookupVisibleFields, Captions, FieldName, Caption, VisibleIndex, AllowBlank, Widths);
        }

        public static RepositoryItemLookUpEdit FieldPLLookUp(PivotGridField Field, string IDField, 
            string DisplayField, DataTable DataLookup, string[] LookupVisibleFields,
            string[] Captions, string FieldName, string Caption, int VisibleIndex, params int[] Widths)
        {
            return FieldPLLookUp(Field, IDField, DisplayField, DataLookup, LookupVisibleFields,
                Captions, FieldName, Caption, VisibleIndex, true, Widths);
        }

        public static RepositoryItemLookUpEdit FieldPLLookUp(PivotGridField Field, 
            string IDField, string DisplayField, DataTable DataLookup, 
            string[] LookupVisibleFields, string[] Captions, string FieldName,
            string Caption, int VisibleIndex, bool AllowBlank, params int[] Widths)
        {
            SetHorzAlignment(Field, HorzAlignment.Near);
            Field.FieldEdit = HelpRepository.GetCotPLLookUp(
                IDField, DisplayField, DataLookup, LookupVisibleFields, Captions,
                FieldName, AllowBlank, Widths);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
            return (RepositoryItemLookUpEdit)Field.FieldEdit;
        }
        #endregion

        #region "Other"
        public static RepositoryItemCalcEdit FieldPLTien(PivotGridField Field, string FieldName, 
            int VisibleIndex, bool HasInput)
        {
            if (HasInput)
            {
                return FieldCalcEdit(Field, FieldName, "Số tiền(VNĐ)", VisibleIndex, FormatParams.SO_TIEN);
            }
            FieldReadOnlyNumber(Field, FieldName, "Số tiền(VNĐ)", FormatParams.SO_TIEN);
            return null;
        }

        public static RepositoryItemCalcEdit FieldPLSoLuong(PivotGridField Field, string FieldName,
            int VisibleIndex, bool HasInput)
        {
            if (HasInput)
            {
                return FieldCalcEdit(Field, FieldName, "Số lượng", VisibleIndex, FormatParams.SO_LUONG);
            }
            FieldReadOnlyNumber(Field, FieldName, "Số lượng", VisibleIndex, FormatParams.SO_LUONG);
            return null;
        }

        public static RepositoryItemCalcEdit FieldPLTrongLuong(PivotGridField Field, string FieldName, 
            int VisibleIndex, bool HasInput)
        {
            if (HasInput)
            {
                return FieldCalcEdit(Field, FieldName, "Trọng lượng", VisibleIndex, FormatParams.SO_TRONG_LUONG);
            }
            FieldReadOnlyNumber(Field, FieldName, "Trọng lượng", VisibleIndex, FormatParams.SO_TRONG_LUONG);
            return null;
        }        

        public static void FieldReadOnlyNumber(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            FieldReadOnlyNumber(Field, FieldName, Caption, VisibleIndex, 
                HelpNumber.ParseInt32(FrameworkParams.option.round));
        }

        public static void FieldReadOnlyNumber(PivotGridField Field, string FieldName, 
            string Caption, int VisibleIndex, int SoThapPhan)
        {
            SetHorzAlignment(Field, HorzAlignment.Far);
            SetNumDisplayFormat(Field, SoThapPhan);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
        }

        public static RepositoryItemComboBox FieldPLVAT(PivotGridField Field, string FieldName, 
            int VisibleIndex, bool HasInput)
        {
            if (HasInput)
            {
                return FieldVAT(Field, FieldName, VisibleIndex);
            }
            FieldReadOnlyVAT(Field, FieldName, VisibleIndex);
            return null;
        }

        public static RepositoryItemComboBox FieldVAT(PivotGridField Field, string FieldName, 
            int VisibleIndex)
        {
            Field.FieldEdit = HelpRepository.GetCotVAT();
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = "VAT";               
                Field.Index = VisibleIndex;
            }
            Field.Width = 60;
            return (RepositoryItemComboBox)Field.FieldEdit;
        }

        public static void FieldReadOnlyVAT(PivotGridField Field, string FieldName, int VisibleIndex)
        {
            SetHorzAlignment(Field, HorzAlignment.Far);
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = "VAT";
                Field.Index = VisibleIndex;
            }
            Field.Width = 60;
        }

        public static void FieldRepository(PivotGridField Field, string FieldName,string Caption, 
            int VisibleIndex, RepositoryItem Repos, HorzAlignment HorzAlign)
        {
            SetHorzAlignment(Field, HorzAlign);
            Field.FieldEdit = Repos;
            if (FieldName != null)
            {
                Field.Area = PivotArea.DataArea;
                Field.FieldName = FieldName;
                Field.Caption = Caption;
                Field.Index = VisibleIndex;
            }
        }
        #endregion

        #endregion

        #region "Column Area"
        public static void FieldColumnDay(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int Width)
        {
            FieldColumn(Field, FieldName, Caption, VisibleIndex, Width);
            Field.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateDay;
        }

        public static void FieldColumnMonth(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int Width)
        {
            FieldColumn(Field, FieldName, Caption, VisibleIndex, Width);
            Field.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
        }

        public static void FieldColumnYear(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int Width)
        {
            FieldColumn(Field, FieldName, Caption, VisibleIndex, Width);
            Field.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
        }

        public static void FieldColumn(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int Width,
            DevExpress.XtraPivotGrid.PivotGroupInterval GroupInterval)
        {
            FieldColumn(Field, FieldName, Caption, VisibleIndex, Width);
            Field.GroupInterval = GroupInterval;
        }

        public static void FieldColumn(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex, int Width)
        {
            Field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            Field.AreaIndex = VisibleIndex;
            Field.Caption = Caption;
            Field.FieldName = FieldName;
            Field.Width = Width;
        }
        #endregion

        #region "Filter Area"
        public static void FieldFilter(PivotGridField Field, string FieldName,
            string Caption, int VisibleIndex)
        {
            Field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea;
            Field.Index = VisibleIndex;
            Field.Caption = Caption;
            Field.FieldName = FieldName;
        }
        #endregion

        /// <summary>
        /// Khởi tạo giá trị mặc định
        /// </summary>
        /// <param name="Field"></param>
        private static void SetField(PivotGridField Field)
        {
            //...
        }

        public static void SetSummaryNumFormat(PivotGridField Field, int soThapPhan)
        {
            //...
        }

        public static void SetHorzAlignment(PivotGridField Field, HorzAlignment Content)
        {
            Field.Appearance.Header.Options.UseTextOptions = true;
            Field.Appearance.Header.TextOptions.HAlignment = Content;
            Field.Appearance.Value.Options.UseTextOptions = true;
            Field.Appearance.Value.TextOptions.HAlignment = Content;
        }

        public static void SetDateDisplayFormat(PivotGridField Field, string formatStr)
        {
            Field.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            Field.CellFormat.FormatString = formatStr;
        }

        public static void SetNumDisplayFormat(PivotGridField Field, int soThapPhan)
        {
            Field.CellFormat.FormatType = FormatType.Numeric;
            Field.CellFormat.FormatString = ApplyFormatAction.GetDisplayFormat(soThapPhan);
        }

        #region "Orther"
        public static bool _setField(FieldPivot fieldPivot, PivotGridField pivotGridField)
        {
            if (_checkField(fieldPivot, pivotGridField))
            {
                pivotGridField.FieldName = fieldPivot.FieldName;
                pivotGridField.Caption = fieldPivot.Caption;
                pivotGridField.AreaIndex = fieldPivot.VisibleIndex;
                pivotGridField.Width = fieldPivot.Width;
                if (fieldPivot.TypeField == TypeField.NgayThang)
                {
                    if (fieldPivot.FollowGroupField == FollowGroupField.Ngay)
                        pivotGridField.GroupInterval =
                            DevExpress.XtraPivotGrid.PivotGroupInterval.DateDay;
                    else if (fieldPivot.FollowGroupField == FollowGroupField.Thang)
                        pivotGridField.GroupInterval =
                            DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
                    else if (fieldPivot.FollowGroupField == FollowGroupField.Quy)
                        pivotGridField.GroupInterval =
                            DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter;
                    else if (fieldPivot.FollowGroupField == FollowGroupField.Nam)
                        pivotGridField.GroupInterval =
                            DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
                }
                else if (fieldPivot.TypeField == TypeField.So)
                {
                    pivotGridField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    pivotGridField.CellFormat.FormatString = fieldPivot.FormatString;
                    pivotGridField.ValueFormat.FormatString = fieldPivot.FormatString;
                }
                else if (fieldPivot.TypeField == TypeField.VND)
                {
                    pivotGridField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    pivotGridField.CellFormat.FormatString = "{0:###,##0}";
                    pivotGridField.ValueFormat.FormatString = "{0:###,##0}";
                }
                else if (fieldPivot.TypeField == TypeField.USD)
                {
                    pivotGridField.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    pivotGridField.CellFormat.FormatString = "c";
                    pivotGridField.ValueFormat.FormatString = "c";
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool _set(PivotGridControl pivotGrid, FieldPivot[] fieldPivots, PivotArea pivotArea)
        {
            foreach (FieldPivot field in fieldPivots)
            {
                PivotGridField _field = new PivotGridField();
                _field.Area = pivotArea;
                if (!_setField(field, _field))
                {
                    PLMessageBox.ShowErrorMessage(
                        (pivotArea == PivotArea.RowArea ? "Row" :
                        (pivotArea == PivotArea.ColumnArea ? "Column" : "Data")) +
                        "Field cấu hình không đúng.");
                    return false;
                }
                pivotGrid.Fields.Add(_field);
            }
            return true;
        }

        /// <summary>
        /// DUYVT: Kiểm tra ràng buộc về kiểu dữ liệu cho phép        
        /// _ RowField:     Text
        /// _ ColumnField:  Text, DateTime
        /// _ DataField:   Numeric
        /// </summary>
        /// <param name="fieldPivot"></param>
        /// <returns></returns>
        private static bool _checkField(FieldPivot fieldPivot, PivotGridField pivotGridField)
        {
            if (pivotGridField.Area == PivotArea.RowArea &&
                fieldPivot.TypeField == TypeField.VanBan)
            {
                return true;
            }
            else if (pivotGridField.Area == PivotArea.ColumnArea &&
                (fieldPivot.TypeField == TypeField.VanBan ||
                fieldPivot.TypeField == TypeField.NgayThang))
            {
                return true;
            }
            else if ((pivotGridField.Area == PivotArea.DataArea ||
                pivotGridField.Area == PivotArea.FilterArea) &&
                (fieldPivot.TypeField == TypeField.So ||
                fieldPivot.TypeField == TypeField.VND ||
                fieldPivot.TypeField == TypeField.USD))
            {
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}