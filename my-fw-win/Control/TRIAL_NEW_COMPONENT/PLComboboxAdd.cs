using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Control cho phép 
    /// - Chọn phần tử -> Trả về ID
    /// - Thêm mới bằng "Ctrl-Insert"
    /// - Xóa phần tử đang chọn bằng "Ctrl-Delete"
    /// </summary>
    public class PLComboboxAdd : ComboBoxEdit, ISelectionControl, IIDValidation
    {
        #region Danh sách các biến
        private DataTable _DataSource;  //Nguồn hiển thị

        private string _tableName;      //Được dùng cho Insert && Delete
        private string _idField;        
        private string _displayField;   
        private string _genName = HelpGen.G_FW_DM_ID;
        
        #endregion

        #region Danh sách các thuộc tính
        public DataTable DataSource
        {
            get {
                return _DataSource;
            }
            set{
                _DataSource = value;
            }
        }
        public string TableName
        {
            get
            {
                return _tableName;
            }
        }
        public string IDField
        {
            get
            {
                return _idField;
            }
            set
            {
                _idField = value;
            }
        }
        public string DisplayField
        {
            get
            {
                return _displayField;
            }
            set
            {
                _displayField = value;
            }
        }
        public string GenID
        {
            get
            {
                return _genName;
            }
            set
            {
                _genName = value;
            }
        }
        #endregion

        #region Hàm khởi tạo control
        public PLComboboxAdd()
        {
            this.Properties.ImmediatePopup = true;
            this.Properties.HotTrackItems = true;
            this.Properties.NullText = GlobalConst.NULL_TEXT;
            this.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

            this.KeyDown += new KeyEventHandler(TrialPLComboboxAdd_KeyDown);
            this.ToolTip = "Sử dụng: \n Ctrl-Insert: Thêm mục đang nhập. \n Ctrl-Delete: Xóa mục đang chọn.";
        }
        /// <summary>Predicate: Phải khởi tạo IdField, DisplayField, DataSource trước khi Init 
        /// DataSource phải có tên table giống như tên table trong DB để có thể Insert
        /// </summary>
        public void _init()
        {
            _tableName = _DataSource.TableName;
            this.Properties.Items.Clear();
            foreach (DataRow dr in _DataSource.Rows)
            {
                this.Properties.Items.Add(new ItemData(HelpNumber.ParseInt64(dr[0].ToString()), dr[1].ToString()));
            }
        }

        public void _init(DataTable Src, string DisplayFN, string ValueFN)
        {
            this._DataSource = Src;
            this._displayField = DisplayFN;
            this._idField = ValueFN;
            _init();
        }
        public bool _IgnoreCase = true;
        public void _init(string TableName, string DisplayFN, string ValueFN)
        {
            this._DataSource = DABase.getDatabase().LoadDataSet(HelpSQL.SelectAll(TableName, _displayField, _IgnoreCase), TableName).Tables[0];
            this._displayField = DisplayFN;
            this._idField = ValueFN;
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

        /// <summary>Làm tươi control
        /// </summary>
        /// <param name="NewSrc">Phải là 1 DataTable</param>
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
        
        private void TrialPLComboboxAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Insert))
                InsertItem();
            else if (e.KeyData == (Keys.Control | Keys.Delete))
                DeleteItem();
        }
        private bool Exist()
        {
            if (this.Text == GlobalConst.NULL_TEXT) return true;

            for (int i = 0; i < this.Properties.Items.Count; i++)
            {
                ItemData data = (ItemData)this.Properties.Items[i];
                if (data.Name == this.Text)
                    return true;
            }
            return false;
        }
        private void InsertItem()
        {
            this.Text = this.Text.Trim();
            if (this.Text != "")
            {
                if (!Exist())
                {
                    long newID = HelpDanhMucDB.InsertItem(_tableName, _genName, this.Text, _idField, _displayField);
                    if (newID > 0)
                    {
                        ItemData newItem = new ItemData(newID, this.EditValue.ToString());
                        this.Properties.Items.Add(newItem);
                        this.EditValue = newItem;
                    }
                }
            }
        }
        private void DeleteItem()
        {
            long id = _getSelectedID();
            if (id > 0)
            {
                if (HelpDanhMucDB.DeleteItem(_tableName, _idField, id))
                {
                    this.Properties.Items.Remove(new ItemData(id, ""));
                }
            }
        }
    }
}
