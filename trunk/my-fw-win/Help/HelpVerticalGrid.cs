using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;

namespace ProtocolVN.Framework.Win
{
    public class HelpVerticalGrid
    {
        public static void SetHorzAlignment(EditorRow Row, HorzAlignment HorzAlign)
        {
            //Dinh dang caption trai
            //Du lieu Trai phai giua tuy vao content 
            Row.Appearance.TextOptions.HAlignment = HorzAlign;
        }

        public static void DongRepository(EditorRow Row, string RowField, RepositoryItem Repos, HorzAlignment HorzAlign)
        {
            HelpEditorRow.SetHorzAlignment(Row, HorzAlign);
            Row.Properties.RowEdit = Repos;
            if (RowField != null) Row.Properties.FieldName = RowField;
        }

        #region Các hàm hỗ trợ cho Row trong XtraVerticalGrid
        public static void DongCalcEdit(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField, int SoThapPhan)
        {
            if (Row == null)
                return;
            SetHorzAlignment(Row, HorzAlignment.Far);
            Row.Properties.RowEdit = HelpRepository.GetCalcEdit(SoThapPhan);
            Row.Properties.FieldName = RowField;
        }

        public static void DongSpinEdit(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField, int SoThapPhan)
        {
            if (Row == null)
                return;
            SetHorzAlignment(Row, HorzAlignment.Far);
            Row.Properties.RowEdit = HelpRepository.GetSpinEdit(SoThapPhan);
            Row.Properties.FieldName = RowField;
            
        }

        public static void DongCheckEdit(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField)
        {
            if (Row == null)
                return;
            SetHorzAlignment(Row, HorzAlignment.Center);
            Row.Properties.RowEdit = HelpRepository.GetCheckEdit(false);
            Row.Properties.FieldName = RowField;
        }

        public static void DongDateEdit(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField)
        {
            if (Row == null)
                return;
            SetHorzAlignment(Row, HorzAlignment.Center);
            Row.Properties.RowEdit = HelpRepository.GetDateEdit("d");
            Row.Properties.FieldName = RowField;
        }

        public static void DongText(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField, DevExpress.Utils.HorzAlignment Alignment)
        {
            if (Row == null)
                return;
            SetHorzAlignment(Row, Alignment);
            Row.Properties.FieldName = RowField;
        }

        public static void DongTextCenter(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField)
        {
            DongText(Row, RowField, HorzAlignment.Center);
        }

        public static void DongTextLeft(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField)
        {
            DongText(Row, RowField, HorzAlignment.Near);
        }

        public static void DongTextRight(DevExpress.XtraVerticalGrid.Rows.EditorRow Row,
            string RowField)
        {
            DongText(Row, RowField, HorzAlignment.Far);
        }
        #endregion

        public static EditorRow[] CreateEditorRow(string[] Captions, bool[] Visible, int[] widths)
        {
            EditorRow[] Rows = new EditorRow[Captions.Length];
            for (int i = 0; i < Captions.Length; i++)
            {
                Rows[i] = new EditorRow();
                Rows[i].Properties.Caption = Captions[i];
                Rows[i].Visible = Visible[i];
                Rows[i].MaxCaptionLineCount = 1;
                //PHUOCNC : Vấn đề width chua giai quyet xong
            }
            return Rows;
        }

        public static void DongTimeEdit(EditorRow Row, string RowField, string Format)
        {
            if (Row != null)
            {
                HelpEditorRow.SetHorzAlignment(Row, DevExpress.Utils.HorzAlignment.Center);
                Row.Properties.RowEdit = HelpRepository.GetCotPLTimeEdit(Format);
                Row.Properties.FieldName = RowField;
            }
        }

        public static void DongLongTimeEdit(EditorRow Row, string RowField)
        {
            if (Row != null)
            {
                HelpEditorRow.SetHorzAlignment(Row, DevExpress.Utils.HorzAlignment.Center);
                Row.Properties.RowEdit = HelpRepository.GetCotPLLongTimeEdit();
                Row.Properties.FieldName = RowField;
            }
        }

        public static void DongShortTimeEdit(EditorRow Row, string RowField)
        {
            if (Row != null)
            {
                HelpEditorRow.SetHorzAlignment(Row, DevExpress.Utils.HorzAlignment.Center);
                Row.Properties.RowEdit = HelpRepository.GetCotPLShortTimeEdit();
                Row.Properties.FieldName = RowField;
            }
        }
    }
}
