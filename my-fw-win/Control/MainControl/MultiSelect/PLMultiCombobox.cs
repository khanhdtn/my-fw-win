﻿using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Data;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// DUYVT 
    /// ProtocolVN.Framework.Win.Temp.PLMultiCombobox sử dụng trong một số form QL, cho phép chọn nhiều điều kiện tìm kiếm
    /// </summary>
    public class PLMultiCombobox : CheckedComboBoxEdit
    {
        // Fields
        private DataTable _DataSource;
        private string _DisplayField;
        private string _ValueField;
        private bool useSort = true;
        private List<object[]> _list = new List<object[]>();

        // Getter, Setter
        public DataTable DataSource
        {
            get
            {
                return this._DataSource;
            }
            set
            {
                this._DataSource = value;
            }
        }
        /// <summary>
        /// Xác định xem có chọn tất cả hoặc không chọn item nào hay ko
        /// </summary>
        public bool _IsNotCheckAnyOrCheckedAll
        {
            get
            {
                string checkItems = this.Properties.GetCheckedItems().ToString();
                if(checkItems=="") return true;//Not checked any items
                string[] length = checkItems.Split(',');
                if(length.Length==this.Properties.Items.Count) return true ;//checked all
                return false;
            }
        }

           
        public string DisplayField
        {
            get
            {
                return this._DisplayField;
            }
            set
            {
                this._DisplayField = value;
            }
        }

        public string ValueField
        {
            get
            {
                return this._ValueField;
            }
            set
            {
                this._ValueField = value;
            }
        }

        public string Text
        {
            get
            {
                return base.Text;
            }
        }
        /// <summary>
        /// nếu dùng thì để trước hàm init
        /// </summary>
        public bool UseSort
        {
            get
            {
                return this.useSort;
            }
            set
            {
                this.useSort = value;
            }
        }
        // Constructor
        public PLMultiCombobox() { }

        public void _init()
        {
            _clear();
            if (this.useSort)
            {
                DataRow[] rows = this._DataSource.Select("", this._DisplayField);
                foreach (DataRow row in rows)
                {
                    int index = base.Properties.Items.Add(row[this._ValueField], row[this._DisplayField].ToString() == "" ? "" : row[this._DisplayField].ToString());
                   // object[] objs = new object[] { index, row[this._ValueField] };
                    //_list.Add(objs);
                }
            }
            else
            {
                foreach (DataRow row in this.DataSource.Rows)
                {
                
                    int index = base.Properties.Items.Add(row[this._ValueField],row[this._DisplayField].ToString() == "" ? "" : row[this._DisplayField].ToString());
                    //object[] objs = new object[] { index, row[this._ValueField] };
                   // _list.Add(objs);
                }
            }
        }

        public void _init(DataTable Src, string DisplayFN, string ValueFN)
        {
            this._DataSource = Src;
            this._DisplayField = DisplayFN;
            this._ValueField = ValueFN;
            this._init();
        }

        public void _clear()
        {
            if (this._list.Count > 0)
                this._list.Clear();
            this.Properties.Items.Clear();
        }
        public void _unChecked()
        {
            foreach (CheckedListBoxItem item in base.Properties.Items)
            {
                if (item.CheckState == System.Windows.Forms.CheckState.Checked)
                {
                    item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                }
            }
        }
        /// <summary>
        /// Trả về 1 mảng kiểu long các ID được chọn
        /// </summary>
        /// <returns></returns>
        public long[] _getSelectedIDs()
        {
            string chekedIDs = this._getStrSelectedIDs();

            chekedIDs = chekedIDs.TrimStart('(').TrimEnd(')');
            if (chekedIDs == "-1") return new long[0];
            string[] IDs = chekedIDs.Split(',');
            return Array.ConvertAll<string, long>(IDs, new Converter<string, long>(HelpNumber.ParseInt64));
            //List<long> ID_arr = new List<long>();
            //foreach (CheckedListBoxItem item in base.Properties.Items)
            //{
            //    if (item.CheckState == System.Windows.Forms.CheckState.Checked)
            //    {
            //        foreach (object[] objs in this._list)
            //            if (HelpNumber.ParseInt32(objs[0]) == base.Properties.Items.IndexOf(item))
            //                ID_arr.Add(HelpNumber.ParseInt64(objs[1]));
            //    }
            //}
            //return ID_arr.ToArray();
        }
        /// <summary>
        /// Trả về 1 mảng kiểu decimail các ID được chọn
        /// </summary>
        /// <returns></returns>
        public decimal[] _getSelectedIDsDecimal()
        {
            string chekedIDs = this._getStrSelectedIDs();
            chekedIDs = chekedIDs.TrimStart('(').TrimEnd(')');
            string[] IDs = chekedIDs.Split(',');
            return Array.ConvertAll<string, decimal>(IDs, new Converter<string, decimal>(HelpNumber.ParseDecimal));
            //List<decimal> ID_arr = new List<decimal>();
            //foreach (CheckedListBoxItem item in base.Properties.Items)
            //{
            //    if (item.CheckState == System.Windows.Forms.CheckState.Checked)
            //    {
            //        foreach (object[] objs in this._list)
            //            if (HelpNumber.ParseInt32(objs[0]) == base.Properties.Items.IndexOf(item))
            //                ID_arr.Add(HelpNumber.ParseDecimal(objs[1]));
            //    }
            //}
            //return ID_arr.ToArray();
        }

        /// <summary>
        /// Trả về 1 mảng kiểu string các NAME được chọn
        /// Dùng trong trường hợp đối tượng là không có ID
        /// </summary>
        /// <returns></returns>
        public string[] _getSelectedNAMEs()
        {
            string ids = _getStrSelectedNAMEs().TrimStart('(').TrimEnd(')');
            return ids.Split(',');
            //List<string> NAME_arr = new List<string>();
            //foreach (CheckedListBoxItem item in base.Properties.Items)
            //{
            //    if (item.CheckState == System.Windows.Forms.CheckState.Checked)
            //    {
            //        foreach (object[] objs in this._list)
            //            if (HelpNumber.ParseInt32(objs[0]) == base.Properties.Items.IndexOf(item))
            //                NAME_arr.Add("'" + objs[1].ToString() + "'");
            //    }
            //}
            //return NAME_arr.ToArray();
        }

        /// <summary>
        /// Trả về chuỗi các ID được chọn, phân cách bởi dấu ',' và nằm trong ()
        /// </summary>
        /// <returns></returns>
        public string _getStrSelectedIDs()
        {
            //long[] ids = _getSelectedIDs();
            //List<string> list_item_id = new List<string>();
            //foreach (long item in ids)
            //    list_item_id.Add(item.ToString());
            //string str_group = string.Join(",", list_item_id.ToArray());
            //if (str_group != "")
            //    return "(" + str_group + ")";
            object checkedItem = this.Properties.GetCheckedItems();
            if (checkedItem == null || checkedItem.ToString() == "")
                return "(-1)";
            return "(" + checkedItem.ToString().Replace(", ", ",") + ")";
        }

        /// <summary>
        /// Trả về chuỗi các NAME được chọn, phân cách bởi dấu ',' và nằm trong ()
        /// </summary>
        /// <returns></returns>
        public string _getStrSelectedNAMEs()
        {
            object checkedItem = this.Properties.GetCheckedItems();
            if (checkedItem == null || checkedItem.ToString() == "")
                return "('-1')";
            return "('" + checkedItem.ToString().Replace(", ", "','") + "')";//string[] names = _getSelectedNAMEs();
            //string str_group = string.Join(",", names);
            //if (str_group != "")
            //    return "(" + str_group + ")";
            //return "('-1')";
        }
        /// <summary>
        /// Trả về chuỗi các NAME được chọn, phân cách bởi dấu ';' và nằm trong ()
        /// </summary>
        /// <returns></returns>
        public string _getStrSelectedNAMEsChamPhay()
        {
            return _getStrSelectedNAMEs().Replace(',', ';');
            //string[] names = _getSelectedNAMEs();
            //string str_group = string.Join(";", names);
            //if (str_group != "")
            //    return "(" + str_group + ")";
            //return "('-1')";
        }
        /// <summary>
        /// Trả về chuỗi các giá trị ID kiểu decimal được chọn, phân cách bởi dấu ',' và nằm trong ()
        /// </summary>
        /// <returns></returns>
        public string _getStrSelectedIDsDecimal()
        {
            decimal[] ids = _getSelectedIDsDecimal();
            List<string> list_item_id = new List<string>();
            foreach (decimal item in ids)
                list_item_id.Add(item.ToString().Replace(',', '.'));
            string str_group = string.Join(",", list_item_id.ToArray());
            if (str_group != "")
                return "(" + str_group + ")";
            return "(-1)";
        }

        /// <summary>
        /// Khởi tạo chọn các ID trên control
        /// </summary>
        /// <param name="ID_array">Mảng các ID cần khởi tạo</param>
        public void _setSelectedIDs(long[] ID_array)
        {
            foreach (CheckedListBoxItem item in base.Properties.Items)
            {
                bool check = false;
                foreach (long id in ID_array)
                {
                    if (item.Value.ToString() == id.ToString())
                    {
                        item.CheckState = System.Windows.Forms.CheckState.Checked;
                        check = true;
                        break;
                    }
                }
                if (check == false) item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                
            }
            //StringBuilder builder = new StringBuilder();
            //for (int i = 0; i < ID_array.Length; i++)
            //{
            //    foreach (object[] objs in this._list)
            //    {
            //        if (HelpNumber.ParseInt64(objs[1]) == ID_array[i])
            //        {
           //            base.Properties.Items[HelpNumber.ParseInt32(objs[0])].CheckState =
            //                System.Windows.Forms.CheckState.Checked;
            //            builder.Append(base.Properties.Items[HelpNumber.ParseInt32(objs[0])].Value);
            //            if (i < ID_array.Length - 1)
            //                builder.Append(", ");
            //        }
            //        else
            //        {
            //            base.Properties.Items[HelpNumber.ParseInt32(objs[0])].CheckState = System.Windows.Forms.CheckState.Unchecked;
            //        }
            //    }
            //}
            //if (ID_array.Length > 0)
            //    this.EditValue = builder.ToString();
        }
        /// <summary>
        /// Truyền vào 1 mảng string các giá trị
        /// </summary>
        /// <param name="ID_array"></param>
        public void _setSelectedIDs(string[] ID_array)
        {
            foreach (CheckedListBoxItem item in base.Properties.Items)
            {
                bool check = false;
                foreach (string id in ID_array)
                {
                    if (item.Value.ToString() == id)
                    {
                        item.CheckState = System.Windows.Forms.CheckState.Checked;
                        check = true;
                        break;
                    }
                }
                if (check == false) item.CheckState = System.Windows.Forms.CheckState.Unchecked;

            }
            //StringBuilder builder = new StringBuilder();
            //for (int i = 0; i < ID_array.Length; i++)
            //{
            //    foreach (object[] objs in this._list)
            //    {
            //        if (objs[1].ToString() == ID_array[i].Replace("'", ""))
            //        {
            //            base.Properties.Items[HelpNumber.ParseInt32(objs[0])].CheckState =
            //                System.Windows.Forms.CheckState.Checked;
            //            builder.Append(base.Properties.Items[HelpNumber.ParseInt32(objs[0])].Value);
            //            if (i < ID_array.Length - 1)
            //                builder.Append("; ");
            //        }
            //        else
            //        {
            //            base.Properties.Items[HelpNumber.ParseInt32(objs[0])].CheckState = System.Windows.Forms.CheckState.Unchecked;
            //        }
            //    }
            //}
            //if (ID_array.Length > 0)
            //    this.EditValue = builder.ToString();
        }
        public void _setFilterGridStringID(GridColumn col)
        {
            try
            {
                this.CloseUp += delegate(object sender, CloseUpEventArgs e)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView view = col.View as DevExpress.XtraGrid.Views.Grid.GridView;
                    if (view.GridControl.DataSource == null ||
                        ((DataTable)view.GridControl.DataSource).Rows.Count == 0) return;
                    col.ClearFilter();
                    string IDString = this._getStrSelectedIDs();
                    if (IDString == "(-1)") return;
                    if (view.ActiveFilterString == "")
                    {
                        view.ActiveFilterString = col.FieldName + " in " + IDString;
                    }
                    else
                    {
                        view.ActiveFilterString += " and (" + col.FieldName + " in " + IDString + ")";
                    }


                };
            }
            catch
            {
            }
        }
        public void _setFilterGridStringDecimal(GridColumn col)
        {
            try
            {
                this.CloseUp += delegate(object sender, CloseUpEventArgs e)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView view = col.View as DevExpress.XtraGrid.Views.Grid.GridView;
                    col.ClearFilter();
                    string DecString = this._getStrSelectedIDsDecimal();
                    if (DecString == "(-1)") return;
                    if (view.ActiveFilterString == "")
                    {
                        view.ActiveFilterString = col.FieldName + " in " + DecString;
                    }
                    else
                    {
                        view.ActiveFilterString += " and (" + col.FieldName + " in " + DecString + ")";
                    }


                };
            }
            catch
            {
            }
        }
        private string ToStringDecimal(object obj)
        {
            return obj.ToString().Replace(
                FrameworkParams.option.decSeparator, "@").Replace(
                FrameworkParams.option.thousandSeparator, ",").Replace(
                "@", ".");
        }

    }
}
