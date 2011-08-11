using System;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace ProtocolVN.Framework.Win
{
    public enum TreeListColumnType
    {
        TextType = -1,
        CalcEdit = 0,
        DateEdit = 1,
        SpinEdit = 2,
        CheckEdit = 3
    }

    public enum TreeListValidationType
    {
        NoValidate = -1,
        CheckNumber = 0,
        ChackDate = 1
    }

    public class TreeListSupport
    {
        static RepositoryItemSpinEdit SpinEdit()
        {
            RepositoryItemSpinEdit spinEdit = new RepositoryItemSpinEdit();
            spinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            return spinEdit;
        }
        static RepositoryItemCalcEdit CalcEdit()
        {
            RepositoryItemCalcEdit calcEdit = new RepositoryItemCalcEdit();
            calcEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            return calcEdit;
        }
        static RepositoryItemDateEdit DateEdit()
        {
            RepositoryItemDateEdit dateEdit = new RepositoryItemDateEdit();
            dateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            return dateEdit;
        }

        public static void SetColumnType(TreeListColumn column, object type)
        {
            if (type.Equals(TreeListColumnType.CheckEdit))
                HelpTreeColumn.CotCheckEdit(column, column.FieldName);


            else if (type.Equals(TreeListColumnType.CalcEdit))
                column.ColumnEdit = CalcEdit();
            else if (type.Equals(TreeListColumnType.DateEdit))
                column.ColumnEdit = DateEdit();
            else if (type.Equals(TreeListColumnType.SpinEdit))
                column.ColumnEdit = SpinEdit();
        }

        public static void SetValidation(object validateType, string fieldName, ValidateNodeEventArgs e)
        {
            if (validateType.Equals(TreeListValidationType.CheckNumber))
            {
                CheckNumber(e.Node[fieldName].ToString(), e);
            }
            else if (validateType.Equals(TreeListValidationType.ChackDate))
            {
                CheckDate(e.Node[fieldName].ToString(), e);
            }
        }

        static void CheckNumber(string value, ValidateNodeEventArgs e)
        {
            try
            {
                decimal.Parse(value);
            }
            catch
            {
                e.ErrorText = "Hãy nhập giá trị sô";
                e.Valid = false;
            }
        }
        static void CheckDate(string value, ValidateNodeEventArgs e)
        {
            try
            {
                DateTime.Parse(value);
            }
            catch
            {
                e.ErrorText = "Ngày không hợp lệ";
                e.Valid = false;
            }
        }
    }
}
