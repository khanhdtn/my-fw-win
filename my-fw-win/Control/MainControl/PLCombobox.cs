using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public class PLCombobox : PLLookupEdit
    {
        private string _DisplayField;
        private string _ValueField;
        private DataTable _DataSource;

        public string DisplayField
        {
            set
            {
                _DisplayField = value;
            }
            get
            {
                return _DisplayField;
            }
        }
        public string ValueField
        {
            set
            {
                _ValueField = value;
            }
            get
            {
                return _ValueField;
            }
        }
        public DataTable DataSource
        {
            set
            {
                _DataSource = value;
            }
            get
            {
                return _DataSource;
            }
        }

        public LookUpEdit imgCombo
        {
            get
            {
                return this._lookUpEdit;
            }
        }
        public string Text
        {
            get
            {
                return this._lookUpEdit.Text;
            }
        }
        public PLCombobox(): base()
        {
            this._lookUpEdit.TextChanged += new System.EventHandler(this.imgComboBox_SelectedIndexChanged);
            this._lookUpEdit.Properties.ShowHeader = false;
            this._lookUpEdit.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        #region Hàm khởi tạo Control
        /// <summary>Predicate: Phải khởi tạo DataSource, DisplayField, ValueField trước khi gọi
        /// </summary>
        public void _init()
        {
            System.Drawing.Size bkSize = this.MainCtrl.Size;            
            base._init(_DataSource, _DisplayField, _ValueField, GlobalConst.NULL_TEXT, _DisplayField, "Tên", this.Width);
            this.MainCtrl.Size = bkSize;
        }
        /// <summary>Predicate: Phải khởi tạo DisplayField, ValueField
        /// </summary>
        public void _init(string tableName)
        {
            //DataSet ds = DABase.getDatabase().LoadTable(tableName);
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(tableName, _DisplayField, _IgnoreCase),tableName);
            this._DataSource = ds.Tables[0];
            _init();            
        }

        public void _init(DataTable src, string displayFn, string valueFn)
        {
            this._DataSource = src;
            this._DisplayField = displayFn;
            this._ValueField = valueFn;
            _init();
        }

        public void _init(string TableName, string DisplayFN, string ValueFN)
        {
            //DataSet ds = DABase.getDatabase().LoadTable(TableName);
            DataSet ds = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, _DisplayField, _IgnoreCase), TableName);
            this._DataSource = ds.Tables[0];
            this._DisplayField = DisplayFN;
            this._ValueField = ValueFN;
            _init();
        }

        #endregion

        #region Đưa sự kiện ra ngoài
        public event EventHandler SelectedIndexChanged = null;
        private void imgComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(sender, e);
            }
        }
        #endregion

        public void _Clear()
        {
            ((DataTable)_lookUpEdit.Properties.DataSource).Clear();
        }
    }
}
