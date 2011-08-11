using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors.Repository;
using ProtocolVN.Framework.Win;
using DevExpress.XtraTreeList;
using DevExpress.Utils;
using ProtocolVN.Framework.Core;
using System.Data;
using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Các hỗ trợ cho phép tạo column trên cây
    /// </summary>
    [Obsolete("Nên dùng lớp HelpTree để thay")]
    public class HelpTreeColumn
    {
        public static void SetHorzAlignment(TreeListColumn Column, HorzAlignment HorzAlign)
        {
            Column.AppearanceHeader.Options.UseTextOptions = true;
            Column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Column.AppearanceCell.Options.UseTextOptions = true;
            Column.AppearanceCell.TextOptions.HAlignment = HorzAlign;
        }

        //d : date; f : full date/time; ...
        public static void SetDateDisplayFormat(TreeListColumn Column, String Format)
        {
            Column.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            Column.Format.FormatString = Format;
        }
        public static void SetNumDisplayFormat(TreeListColumn Column, int SoThapPhan)
        {
            Column.Format.FormatType = FormatType.Numeric;
            Column.Format.FormatString = ApplyFormatAction.GetDisplayFormat(SoThapPhan);
        }
        public static TreeListColumn CreateTreeListColumn(TreeList treeList, string Caption, int VisibleIndex, int Width)
        {
            TreeListColumn Column = treeList.Columns.Add();
            Column.Caption = Caption;
            if (VisibleIndex != -2)
            {
                if (VisibleIndex == -1)
                {
                    Column.Visible = false;
                    Column.OptionsColumn.AllowFocus = false;
                    Column.OptionsColumn.ReadOnly = true;
                    Column.OptionsColumn.AllowEdit = false;
                }
                else
                    Column.VisibleIndex = VisibleIndex;
            }
            if (Width != -1)
            {
                Column.Width = Width;
                Column.OptionsColumn.FixedWidth = true;
            }

            return Column;
        }
        public static TreeListColumn ThemCot(TreeList treeList, string Caption, int VisibleIndex, int Width)
        {
            return CreateTreeListColumn(treeList, Caption, VisibleIndex, Width);
        }
        public static void HideTreeListColumn(TreeListColumn col)
        {
            col.OptionsColumn.AllowFocus = false;
            col.OptionsColumn.AllowSize = false;
            col.OptionsColumn.FixedWidth = true;
            col.Visible = false;
            col.Width = 0;
            col.VisibleIndex = -1;
        }
        public static void AnCot(TreeListColumn col)
        {
            HideTreeListColumn(col);
        }

        public static void CotRepository(TreeListColumn Column, string ColumnField, RepositoryItem Repos, HorzAlignment HorzAlign)
        {
            SetHorzAlignment(Column, HorzAlign);
            Column.ColumnEdit = Repos;
            if (ColumnField != null) Column.FieldName = ColumnField;
        }

        #region Phần I: Cột nhập liệu và tra cứu
        public static RepositoryItemCheckEdit CotCheckEdit(TreeListColumn Column , string ColumnField)
        {
            SetHorzAlignment(Column, HorzAlignment.Center);
            Column.ColumnEdit = HelpRepository.GetCheckEdit(false);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemCheckEdit)Column.ColumnEdit;
        }
        public static RepositoryItemCalcEdit CotCalcEdit(TreeListColumn Column , string ColumnField , int SoThapPhan)
        {
            SetHorzAlignment(Column, HorzAlignment.Far);
            Column.ColumnEdit = HelpRepository.GetCalcEdit(SoThapPhan);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemCalcEdit)Column.ColumnEdit;
        }

        public static RepositoryItemSpinEdit CotSpinEdit(TreeListColumn Column , string ColumnField , int SoThapPhan)
        {
            SetHorzAlignment(Column, HorzAlignment.Far);
            Column.ColumnEdit = HelpRepository.GetSpinEdit(SoThapPhan);
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemSpinEdit)Column.ColumnEdit;
        }

        public static RepositoryItemDateEdit CotDateEdit(TreeListColumn Column, string ColumnField)
        {
            SetHorzAlignment(Column, HorzAlignment.Center);
            SetDateDisplayFormat(Column, "d");
            Column.ColumnEdit = HelpRepository.GetDateEdit("d");
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemDateEdit)Column.ColumnEdit;
        }

        public static RepositoryItemMemoExEdit CotMemoEdit(TreeListColumn Column , string ColumnField)
        {
            SetHorzAlignment(Column, HorzAlignment.Near);
            Column.ColumnEdit = HelpRepository.GetMemoExEdit();
            if (ColumnField != null) Column.FieldName = ColumnField;
            return (RepositoryItemMemoExEdit)Column.ColumnEdit;
        }

        public static RepositoryItemImageComboBox CotCombobox(TreeListColumn column, string LookupTable, string IDField, string DisplayField, string ColumnField)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.AppearanceCell.Options.UseTextOptions = true;
            column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

            DatabaseFB db = DABase.getDatabase();
            DataSet ds = new DataSet();
            ds = db.LoadTable(LookupTable);

            RepositoryItemImageComboBox rCBB = new RepositoryItemImageComboBox();
            //rCBB.Name = "repositoryItemImageComboBoxCode" + LookupTable;
            foreach (DataRow row in ds.Tables[0].Rows)
                rCBB.Items.Add(new ImageComboBoxItem("" + row[DisplayField].ToString(), row[IDField]));

            column.ColumnEdit = rCBB;
            if (ColumnField != null) column.FieldName = ColumnField;
            return rCBB;
        }
        
        public static RepositoryItemLookUpEdit CotLookUp(TreeListColumn column , string IDField , string DisplayField , string LookupTable , string[] LookupVisibleFields , string[] Captions , string ColumnField , int[] Widths)
        {
            SetHorzAlignment(column, HorzAlignment.Near);
            DataTable Data = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(LookupTable, LookupVisibleFields[0], true)).Tables[0];
            column.ColumnEdit = HelpRepository.GetCotPLLookUp(IDField, DisplayField, Data, LookupVisibleFields, Captions, ColumnField, false, Widths);
            if (ColumnField != null) column.FieldName = ColumnField;
            return (RepositoryItemLookUpEdit)column.ColumnEdit;
        }
        public static void CotTextLeft(TreeListColumn Column , string FieldName)
        {
            Column.FieldName = FieldName;
            SetHorzAlignment(Column, HorzAlignment.Near);
        }
        public static void CotTextRight(TreeListColumn Column , string FieldName)
        {
            Column.FieldName = FieldName;
            SetHorzAlignment(Column, HorzAlignment.Far);
        }
        public static void CotTextCenter(TreeListColumn Column , string FieldName)
        {
            Column.FieldName = FieldName;
            SetHorzAlignment(Column, HorzAlignment.Center);
        }
        #endregion

        #region Phần II: Tạo cột chỉ đọc
        public static void CotReadOnlyNumber(TreeListColumn Column , string FieldName , int round)
        {
            Column.FieldName = FieldName;

            SetHorzAlignment(Column, HorzAlignment.Far);
            SetNumDisplayFormat(Column, HelpNumber.ParseInt32(ApplyFormatAction.GetDisplayFormat(round)));
        }
        public static void CotReadOnlyNumber(TreeListColumn Column , string FieldName)
        {
            CotReadOnlyNumber(Column , FieldName , HelpNumber.ParseInt32(FrameworkParams.option.round));
        }
        public static void CotReadOnlyDate(TreeListColumn Column , string FieldName)
        {
            Column.FieldName = FieldName;
            SetHorzAlignment(Column, HorzAlignment.Center);
            SetDateDisplayFormat(Column, "d");
        }
        #endregion

        #region Phần III: Tạo cột cụ thể gồm đọc và nhập
        #region Cột ĐÓNG
        public static TreeListColumn CotDong(TreeList treeList)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditDEL = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            repositoryItemButtonEditDEL.AutoHeight = false;
            repositoryItemButtonEditDEL.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            repositoryItemButtonEditDEL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", 10, true, true, false, DevExpress.Utils.HorzAlignment.Center, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))});
            repositoryItemButtonEditDEL.Name = "repositoryItemButtonEditDEL";
            repositoryItemButtonEditDEL.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repositoryItemButtonEditDEL.KeyUp += delegate(object sender , KeyEventArgs e)
            {
                if (e.KeyData == Keys.Enter)
                    treeList.DeleteNode(treeList.FocusedNode);
            };
            repositoryItemButtonEditDEL.Click += delegate(object sender , EventArgs e)
            {
                treeList.DeleteNode(treeList.FocusedNode);
            };
            treeList.RepositoryItems.Add(repositoryItemButtonEditDEL);
            TreeListColumn CotXoa = treeList.Columns.Add();
            CotXoa.AppearanceHeader.Options.UseTextOptions = true;
            CotXoa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            CotXoa.AppearanceCell.Options.UseTextOptions = true;
            CotXoa.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            CotXoa.Caption = "    ";
            CotXoa.ColumnEdit = repositoryItemButtonEditDEL;
            CotXoa.Name = "CotXoa";
            CotXoa.OptionsColumn.AllowSize = false;
            CotXoa.OptionsColumn.FixedWidth = true;
            CotXoa.Visible = true;
            CotXoa.VisibleIndex = 50;
            CotXoa.Width = 25;

            return CotXoa;
        }
        #endregion

        #region Cột SỐ LƯỢNG
        public static void CotReadOnlySOLUONG(TreeListColumn Column , string FieldName)
        {
            CotReadOnlyNumber(Column , FieldName , FormatParams.SO_LUONG);
        }
        #endregion

        #region Cột SỐ TIỀN
        public static void CotReadOnlySOTIEN(TreeListColumn Column , string FieldName)
        {
            CotReadOnlyNumber(Column , FieldName , FormatParams.SO_TIEN);
        }
        public static void CotReadOnlyTIENVND(TreeListColumn Column , string FieldName)
        {
            CotReadOnlyNumber(Column , FieldName , FormatParams.SO_TIEN);
        }
        #endregion

        #region Cột VAT
        public static RepositoryItemComboBox CotVAT(TreeListColumn Column , string ColumnField)
        {
            Column.FieldName = ColumnField;
            Column.ColumnEdit = HelpRepository.GetCotVAT();

            Column.OptionsColumn.AllowSize = false;
            Column.OptionsColumn.FixedWidth = true;
            Column.Width = 60;
            return (RepositoryItemComboBox)Column.ColumnEdit;
        }

        public static void CotReadOnlyVAT(TreeListColumn Column , string FieldName)
        {
            Column.FieldName = FieldName;
            SetHorzAlignment(Column, HorzAlignment.Far);

            Column.OptionsColumn.AllowSize = false;
            Column.OptionsColumn.FixedWidth = true;
            Column.Width = 60;
        }
        #endregion

        #region Cột Duyệt
        public static RepositoryItemImageComboBox CotDuyet(TreeListColumn Column , string ColumnField)
        {
            SetHorzAlignment(Column, HorzAlignment.Center);
            Column.ColumnEdit = HelpRepository.GetCotDuyet(); ;

            Column.FieldName = ColumnField;
            Column.Caption = "Tình trạng";
            Column.Width = 70;
            Column.OptionsColumn.FixedWidth = true;
            Column.OptionsColumn.AllowSize = false;            
            Column.VisibleIndex = 40;

            return (RepositoryItemImageComboBox)Column.ColumnEdit;
        }
        #endregion

        #region
        public static RepositoryItemCheckEdit CotHienThi(TreeListColumn column , string InputField)
        {
            RepositoryItemCheckEdit ret = CotCheckEdit(column , InputField);
            column.OptionsColumn.FixedWidth = true;
            column.Width = 70;
            return ret;
        }
        #endregion
        #endregion

        #region Phần V : Tạo cột Advance
        public static RepositoryComboboxAdd CotComboboxAdd(TreeList treeList , TreeListColumn column , string ColumnField , string ValueField, string DisplayField , string TableName)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = ColumnField+DisplayField;
            RepositoryComboboxAdd comboAdd = new RepositoryComboboxAdd(TableName, ColumnField, ValueField, DisplayField, HelpGen.G_FW_DM_ID, treeList);
            comboAdd._init();
            column.ColumnEdit = comboAdd;
            return comboAdd;
        }
        public static RepositoryComboboxAuto CotComboboxFind(TreeList treeList , TreeListColumn column , string ColumnField , string ValueField, string DisplayField , string TableName , bool StartWith)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = ColumnField+DisplayField;
            RepositoryComboboxAuto comboFind = new RepositoryComboboxAuto(treeList ,ColumnField, TableName , ValueField , DisplayField , StartWith);
            column.ColumnEdit = comboFind;
            return comboFind;
        }
        public static RepositoryItemDanhMucAdv CotDanhMucAdv(TreeList treeList , TreeListColumn column , XtraForm frmDanhMuc , string columnField, string tableName , string valueField , string[] visibleField , string[] caption , string getField)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = columnField+getField;
            RepositoryItemDanhMucAdv comboDMAdv = new RepositoryItemDanhMucAdv(frmDanhMuc ,columnField, tableName , valueField , visibleField , caption , getField ,treeList);
            column.ColumnEdit = comboDMAdv;
            return comboDMAdv;
        }
        public static RepositoryItemDataTreeNew CotTreeDanhMucAdv(TreeList treeList , TreeListColumn column , XtraForm danhMucForm ,string columnField, string TableName , int[] RootID , string valueField , string IDParentField , string[] VisibleFields , string[] Captions , string getField)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = columnField+getField;
            RepositoryItemDataTreeNew comboTreeDMAdv = new RepositoryItemDataTreeNew(danhMucForm , treeList ,columnField, TableName , RootID , valueField , IDParentField , VisibleFields , Captions , getField);
            column.ColumnEdit = comboTreeDMAdv;
            return comboTreeDMAdv;
        }
        public static RepositoryDMThamTri CotDanhMucThamTri(TreeListColumn column , string FieldName , string TableName , string Category)
        {
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.FieldName = FieldName;
            RepositoryDMThamTri DMThamTri = new RepositoryDMThamTri(TableName , Category);
            column.ColumnEdit = DMThamTri;
            return DMThamTri;
        }
        #endregion
    }
}
