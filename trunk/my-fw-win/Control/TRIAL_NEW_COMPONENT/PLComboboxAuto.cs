using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    /// <summary>Control được dùng để chọn một phần tử trong danh mục mà số phần từ nó lớn
    /// Khi chọn người dùng có thể đánh nhanh chuỗi cần chọn nhấn Enter sẽ liệt kê các phần
    /// tử chứa phần tử cần chọn
    /// </summary>
    public partial class PLComboboxAuto : DevExpress.XtraEditors.XtraUserControl, ISelectionControl, IIDValidation
    {
        #region Biến
        private string _tableName;
        private string _idField;
        private string _memberField;
        private bool _startWith;        //True: Bắt đầu với False: Contains

        private DataSet ds = null;
        private bool isHasValue = false;
        private bool isCreateButton = false;

        #endregion

        #region Thuộc tính
        public string ViewName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string IDField
        {
            get { return _idField; }
            set { _idField = value; }
        }

        public string MemberField
        {
            get { return _memberField; }
            set { _memberField = value; }
        }

        public bool StartWith
        {
            get { return _startWith; }
            set { _startWith = value; }
        }

        public ComboBoxEdit MainCtrl
        {
            get
            {
                return this.comboBoxEdit1;
            }
        }
        
        #endregion

        #region Khởi tạo
        /// <summary>
        /// Khi người dùng nhập vào một chuỗi nhấn enter thì trong combobox sẽ liệt kê tất cả các giá trị 
        /// có chứa chuỗi đó
        /// </summary>
        public PLComboboxAuto()
        {
            InitializeComponent();

            comboBoxEdit1.KeyDown += new KeyEventHandler(comboBoxEdit1_KeyDown);
            comboBoxEdit1.Properties.HotTrackItems = true;
            comboBoxEdit1.Properties.ImmediatePopup = true;
            comboBoxEdit1.Properties.Buttons.Clear();
            comboBoxEdit1.ToolTip = "Hướng dẫn\n Enter: Liệt kê phần tử chứa dữ liệu cần tìm.";
        }

        public void _init(string ViewName, string DisplayFN, string ValueFN, bool StartWith)
        {
            this._tableName = ViewName;
            this._memberField = DisplayFN;
            this._idField = ValueFN;
            this._startWith = StartWith;
        }
        #endregion

        #region ISelectionControl Members
        public long _getSelectedID()
        {
            DataTable dt = ds.Tables[0];
            DataColumn[] dc = { dt.Columns[_memberField] };
            object key = comboBoxEdit1.Text;
            dt.PrimaryKey = dc;
            DataRow dr = dt.Rows.Find(key);
            if (dr != null) 
                return HelpNumber.ParseInt64(dr[_idField].ToString());
            return -1;
        }
        public void _setSelectedID(long id)
        {
            string command = "SELECT " + _idField + "," + _memberField + " FROM " + _tableName + " WHERE 1=1";
            QueryBuilder query = new QueryBuilder(command);
            query.add(_idField, Operator.Equal, id, DbType.Int64);
            ds = DABase.getDatabase().LoadDataSet(query, "DANHMUC");
            comboBoxEdit1.Text = ds.Tables[0].Rows[0][_memberField].ToString();            
            isCreateButton = false;
        }
        public void _refresh(object NewSrc)
        {
            
        }
        #endregion

        #region IValidation Members

        public void SetError(DXErrorProvider errorProvider, string errorMsg)
        {
            errorProvider.SetError(comboBoxEdit1, errorMsg);
        }

        #endregion

        protected void comboBoxEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBoxEdit1.SelectedText == "")
                {
                    e.Handled = true;
                    string command;
                    string text = comboBoxEdit1.Text.Trim();
                    if (!comboBoxEdit1.Text.Equals(String.Empty))
                    {
                        if (_startWith)
                            command = "SELECT " + _idField + "," + _memberField + " FROM " + _tableName + " WHERE " + _memberField + " like '" + comboBoxEdit1.Text + "%'";
                        else
                            command = "SELECT " + _idField + "," + _memberField + " FROM " + _tableName + " WHERE " + _memberField + " like '%" + comboBoxEdit1.Text + "%'";
                    }
                    else
                        command = "SELECT " + _idField + "," + _memberField + " FROM " + _tableName + " WHERE " + _memberField + " like ''";

                    LoadAllItems(command);

                    if (isHasValue && !isCreateButton)
                    {
                        this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
                        isCreateButton = true;
                    }
                    else if (!isHasValue)
                    {
                        this.comboBoxEdit1.Properties.Buttons.Clear();
                        isCreateButton = false;
                    }

                    comboBoxEdit1.ShowPopup();
                    comboBoxEdit1.Text = text;
                }
            }
        }

        private void LoadAllItems(string strCommand)
        {
            ds = DABase.getDatabase().LoadDataSet(strCommand, "DANHMUC");
            if (ds.Tables[0].Rows.Count > 0) isHasValue = true;
            else isHasValue = false;

            comboBoxEdit1.Properties.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                comboBoxEdit1.Properties.Items.Add(dr[_memberField].ToString());
            }
        }
    }
}
