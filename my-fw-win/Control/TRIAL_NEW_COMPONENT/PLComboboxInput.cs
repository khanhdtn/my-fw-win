using System;
using System.Data;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    //PHUOCNC: Cho phep Hook vao luc them 1 dong moi
    /// <summary>Combobox cho phép chọn để lấy ID, đồng thời có thể nhập một giá trị
    /// mới không có trong danh sách. Còn PLCombobox chỉ cho phép chọn không cho phép
    /// nhập mới 1 dữ liệu mới.
    /// </summary>
    public class PLComboboxInput : ComboBoxEdit, ISelectionControl, IIDValidation
    {
        #region Biến
        private DataTable _DataSource;
        private string _tableName;
        private string _displayField;
        private string _genID = HelpGen.G_FW_DM_ID;
        private string _idField;
        #endregion

        #region Thuộc tính
        public string TableName
        {
            set
            {
                _tableName = value;
            }
        }
        public string DispalyField
        {
            set
            {
                _displayField = value;
            }
        }
        public string IdField
        {
            set { _idField = value; }
        }
        public string GenID
        {
            set
            {
                _genID = value;
            }
        }
        #endregion

        #region Khởi tạo
        public bool _IgnoreCase = true;
        private void InitializeComponent()
        {
            this.Properties.ImmediatePopup = true;
            this.Properties.HotTrackItems = true;
            this.Properties.NullText = GlobalConst.NULL_TEXT;
        }
        public PLComboboxInput()
        {
            InitializeComponent();    
        }
        /// <summary>
        /// Sử dụng để thêm danh mục vào database khi người dùng nhập vào một danh mục mới
        /// Sau khi truyền tham số , khởi tạo bằng cách gọi hàm _init()
        /// </summary>
        /// <param name="TableName">Ten table danh muc</param>
        /// <param name="ColumnName">Ten column co noi dung duoc add vao ComboBox</param>
        /// <param name="GenName">Ten generator id cua danh muc moi khi them vao</param>
        public PLComboboxInput(string TableName, string IDField, string DisplayField, string GenName)
        {
            InitializeComponent();
            this._tableName = TableName;
            //this._DataSource = DABase.getDatabase().LoadTable(TableName).Tables[0];
            this._DataSource = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, DisplayField, _IgnoreCase), TableName).Tables[0];
            this._idField = IDField;
            this._displayField = DisplayField;
            this._genID = GenName;
        }

        public void _init()
        {
            this._tableName = _DataSource.TableName;
            this.Properties.Items.Clear();
            foreach (DataRow dr in _DataSource.Rows)
            {
                this.Properties.Items.Add(new ItemData(HelpNumber.ParseInt64(dr[0]), dr[1].ToString()));
            }
        }

        public void _init(DataTable Src, string DisplayFN, string ValueFN)
        {
            this._DataSource = Src;
            this._displayField = DisplayFN;
            this._idField = ValueFN;
            _init();
        }

        public void _init(string TableName, string DisplayFN, string ValueFN)
        {
            this._tableName = TableName;
            //this._DataSource = DABase.getDatabase().LoadTable(TableName).Tables[0];
            this._DataSource = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, DisplayFN, _IgnoreCase), TableName).Tables[0];
            this._idField = ValueFN;
            this._displayField = DisplayFN;
            _init();
        }
        #endregion

        #region ISelectionControl Members

        public long _getSelectedID()
        {
            try { return ((ItemData)this.EditValue).ID; }
            catch { return -1; }
        }

        public void _setSelectedID(long id)
        {
            int i = this.Properties.Items.IndexOf(new ItemData(id, ""));
            this.EditValue = this.Properties.Items[i];
        }

        public void _refresh(object NewSrc)
        {
            this._DataSource = (DataTable)NewSrc;
            _init();
        }
        
        #endregion

        #region IValidation Members

        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorMsg)
        {
            errorProvider.SetError(this, errorMsg);
        }

        #endregion
    }
}
