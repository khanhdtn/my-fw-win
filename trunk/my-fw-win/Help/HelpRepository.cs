using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors.Controls;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    public class HelpRepository
    {
        public static RepositoryItemCheckEdit GetCheckEdit(bool UsingImage)
        {
            RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit();
            checkEdit.ValueChecked = "Y";
            checkEdit.ValueUnchecked = "N";
            //checkEdit.ValueGrayed = DBNull.Value;
            //checkEdit.AllowGrayed = true; 
            if (UsingImage)
            {
                checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
                checkEdit.PictureChecked = HelpImage.getImage1616("ctsCheck.png");
            }
            else
            {
                checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            }
            return checkEdit;
            
        }
            
        //SoThapPhan = -1 Cho so nguyen
        public static RepositoryItemCalcEdit GetCalcEdit(int SoThapPhan)
        {
            RepositoryItemCalcEdit _caledit = new RepositoryItemCalcEdit();
            ApplyFormatAction.applyElement(_caledit, SoThapPhan);
            return _caledit;            
        }

        //SoThapPhan = -1 Cho so nguyen
        public static RepositoryItemSpinEdit GetSpinEdit(int SoThapPhan)
        {
            RepositoryItemSpinEdit _spinEdit = new RepositoryItemSpinEdit();
            ApplyFormatAction.applyElement(_spinEdit, SoThapPhan);
            return _spinEdit;
        }

        public static RepositoryItemDateEdit GetDateEdit(String Format)
        {
            RepositoryItemDateEdit _dateEdit = new RepositoryItemDateEdit();
            _dateEdit.EditFormat.FormatType = FormatType.DateTime;
            _dateEdit.EditFormat.FormatString = Format;

            _dateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            _dateEdit.Mask.EditMask = FrameworkParams.option.dateFormat;

            return _dateEdit;
        }

        public static RepositoryItemMemoExEdit GetMemoExEdit()
        {
            RepositoryItemMemoExEdit MemoEditColumn = new RepositoryItemMemoExEdit();
            //((System.ComponentModel.ISupportInitialize)(MemoEditColumn)).BeginInit();
            //MemoEditColumn.Name = "MemoEditColumn";
            //((System.ComponentModel.ISupportInitialize)(MemoEditColumn)).EndInit();
            return MemoEditColumn;
        }

        public static RepositoryItemImageComboBox GetCotDuyet()
        {
            ImageCollection imglist = new ImageCollection();
            FWImageDic.GET_DUYET_STATUS16(imglist);

            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox itemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            itemImageComboBox.SmallImages = imglist;
            itemImageComboBox.Items.AddRange(
                new object[] { new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Chưa duyệt", "1", 0), 
                               new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Duyệt", "2", 1), 
                               new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Không duyệt", "3", 2) });
            itemImageComboBox.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;

            return itemImageComboBox;
        }

        public static RepositoryItemComboBox GetCotVAT()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemComboBox ItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(ItemComboBox1)).BeginInit();
            
            ItemComboBox1.ImmediatePopup = true;
            ItemComboBox1.ReadOnly = false;
            ItemComboBox1.AutoHeight = false;
            ItemComboBox1.TextEditStyle = TextEditStyles.DisableTextEditor;
            ItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            ItemComboBox1.Name = "repositoryItemComboBox1";
            ((System.ComponentModel.ISupportInitialize)(ItemComboBox1)).EndInit();

            ItemComboBox1.Items.Add(0);
            ItemComboBox1.Items.Add(5);
            ItemComboBox1.Items.Add(10);

            return ItemComboBox1;
        }

        public static RepositoryItemLookUpEdit GetCotPLLookUp(
            string IDField, string DisplayField, 
            DataTable DataLookup, string[] LookupVisibleFields, 
            string[] Captions, string ColumnField, bool AllowBlank, 
            params int[] Widths)
        {
            RepositoryItemLookUpEdit lookup = new RepositoryItemLookUpEdit();
            if (AllowBlank)
            {
                DataRow row = DataLookup.NewRow();
                row[IDField] = -1;
                row[DisplayField] = "";
                DataLookup.Rows.InsertAt(row, 0);
            }

            lookup.DataSource = DataLookup;
            lookup.ValueMember = IDField;
            lookup.DisplayMember = DisplayField;
            lookup.ImmediatePopup = true;
            lookup.ShowLines = true;
            lookup.NullText = GlobalConst.NULL_TEXT;
            int totalWidth = 0;
            if (LookupVisibleFields != null)
            {
                for (int i = 0; i < LookupVisibleFields.Length; i++)
                {
                    LookUpColumnInfo colLook = new LookUpColumnInfo();
                    colLook.FieldName = LookupVisibleFields[i];
                    colLook.Caption = Captions[i];

                    if (Widths != null) colLook.Width = Widths[i];
                    else colLook.Width = 40;
                    totalWidth += colLook.Width;
                    lookup.Columns.Add(colLook);
                }
            }
            lookup.PopupWidth = totalWidth;
            lookup.TextEditStyle = TextEditStyles.Standard;
            if (Widths == null) lookup.BestFit();

            return lookup;
        }

        public static RepositoryItemTimeEdit GetCotPLTimeEdit(String Format, HourFormat HFormat)
        {
            RepositoryItemTimeEdit timeEdit = new RepositoryItemTimeEdit();
            timeEdit.Mask.EditMask = Format;
            timeEdit.HourFormat = HFormat;
            timeEdit.Mask.UseMaskAsDisplayFormat = true;           
            return timeEdit;
        }
        public static RepositoryItemTimeEdit GetCotPLTimeEdit(String Format)
        {
            return GetCotPLTimeEdit(Format, HourFormat.Default);
        }
        public static RepositoryItemTimeEdit GetCotPLShortTimeEdit()
        {
            RepositoryItemTimeEdit timeEdit = new RepositoryItemTimeEdit();
            timeEdit.Mask.EditMask = "HH:mm";
            timeEdit.Mask.UseMaskAsDisplayFormat = true;
            return timeEdit;
        }

        public static RepositoryItemTimeEdit GetCotPLLongTimeEdit()
        {
            RepositoryItemTimeEdit timeEdit = new RepositoryItemTimeEdit();
            timeEdit.Mask.EditMask = "HH:mm:ss";
            timeEdit.Mask.UseMaskAsDisplayFormat = true;
            return timeEdit;
        }
    }
}
