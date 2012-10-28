using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public partial class DMGrid : DevExpress.XtraEditors.XtraUserControl, IActionCategory, IPermisionable, IPluginCategory
    {
        public DMBasicGrid _DMCore =null;
        private PopupContainerEdit popupContainer = null;
        private string IDField = null;
        private string DislayField = null;
        public DataRow RowSelected = null;
        public bool _UseDeleteEvent = true;
        public bool _UseAddEvent = true;
        public bool _UseUpdateEvent = true;
        public ToolStripDropDownButton btnPrint;
        public ToolStripDropDownButton btnExport;
        private DelegationLib.DefinePermission permission;

        #region Dùng như control chọn lựa
        public DMGrid()
        {
            InitializeComponent();
        }

        public void _init(GroupElementType type,
            DataTable DataSource, string IDField, string DislayField, string[] NameFields, string[] Subjects,
            DMBasicGrid.InitGridColumns InitGridCol, DMBasicGrid.GetRule Rule,
            DelegationLib.DefinePermission permission,
            PLDelegation.ProcessDataRow InsertFunc, 
            PLDelegation.ProcessDataRow DeleteFunc, 
            PLDelegation.ProcessDataRow UpdateFunc,
            params string[] readOnlyField)
        {
            this.btnAdd.Image = FWImageDic.ADD_IMAGE20;
            this.btnDel.Image = FWImageDic.DELETE_IMAGE20;
            this.btnUpdate.Image = FWImageDic.EDIT_IMAGE20;
            this.btnSelect.Image = FWImageDic.CHOICE_IMAGE20;
            this.btnNoSelect.Image = FWImageDic.NO_CHOICE_IMAGE20;
            this.btnInfo.Image = FWImageDic.DETAIL_IMAGE20;
            this.btnClose.Image = FWImageDic.CLOSE_IMAGE20;
            this.btnSave.Image = FWImageDic.SAVE_IMAGE20;
            this.btnNoSave.Image = FWImageDic.NO_SAVE_IMAGE20;

            _DMCore = new DMBasicGrid(DataSource, IDField, NameFields, Subjects, InitGridCol, Rule, permission,
                                    InsertFunc, DeleteFunc, UpdateFunc);

            _DMCore.Dock = DockStyle.Fill;
            this.Controls.Add(_DMCore);
            if (popupContainer != null) _DMCore.SupportDoubleClick = false;

            this.Controls.Add(this.btnBar);
            this.IDField = IDField;
            this.DislayField = DislayField;
            btnExport = HelpGrid.addXuatRaFileItem(this.btnBar, this.Grid);
            btnPrint = HelpGrid.addInLuoiItem(this.btnBar, this.Grid);
            SetMode(type);
            EditMode(readOnlyField);
            Grid.KeyDown += new KeyEventHandler(Grid_KeyDown);
            Grid.DoubleClick += new EventHandler(Grid_DoubleClick);
            _DMCore.SetDMGridOwner(this);
       
            if (Grid is PLGridView)
            {
                ((PLGridView)Grid)._SetPermissionElement(btnPrint, btnExport);
            }
            
        }
        /// <summary>
        /// Unique:     N Field
        /// Phân quyền: Có (Chưa xử lý), 
        /// Cho thêm:   Có Nếu readOnlyField = null hoặc không truyền vào
        ///             Không Nếu readOnlyFiled !=null (chỉ cập nhật)
        /// VISIBLE_BIT:Không
        /// GroupElementType: Chọn kiểu chọn lựa ( Chỉ chọn , Chọn và Thêm )
        /// </summary>
        public void _init(GroupElementType type, 
            string TableName, string IDField, string DislayField, string[] NameFields, string[] Subjects, 
            DMBasicGrid.InitGridColumns InitGridCol, DMBasicGrid.GetRule Rule, 
            DelegationLib.DefinePermission permission, 
            params string[] readOnlyField)
        {
            this.btnAdd.Image = FWImageDic.ADD_IMAGE20;
            this.btnDel.Image = FWImageDic.DELETE_IMAGE20;
            this.btnUpdate.Image = FWImageDic.EDIT_IMAGE20;
            this.btnSelect.Image = FWImageDic.CHOICE_IMAGE20;
            this.btnNoSelect.Image = FWImageDic.NO_CHOICE_IMAGE20;
            this.btnInfo.Image = FWImageDic.DETAIL_IMAGE20;
            this.btnClose.Image = FWImageDic.CLOSE_IMAGE20;
            this.btnSave.Image = FWImageDic.SAVE_IMAGE20;
            this.btnNoSave.Image = FWImageDic.NO_SAVE_IMAGE20;

            _DMCore = new DMBasicGrid(TableName, IDField, NameFields, Subjects, InitGridCol, Rule, permission);
            _DMCore.Dock = DockStyle.Fill;
            this.Controls.Add(_DMCore);
            if (popupContainer != null) _DMCore.SupportDoubleClick = false;

            this.Controls.Add(this.btnBar);
            this.IDField = IDField;
            this.DislayField = DislayField;
            btnExport = HelpGrid.addXuatRaFileItem(this.btnBar, this.Grid);
            btnPrint = HelpGrid.addInLuoiItem(this.btnBar, this.Grid);
            SetMode(type);
            EditMode(readOnlyField);
            Grid.KeyDown += new KeyEventHandler(Grid_KeyDown);
            Grid.DoubleClick += new EventHandler(Grid_DoubleClick);
            _DMCore.SetDMGridOwner(this);
          
            if (Grid is PLGridView)
            {
                ((PLGridView)Grid)._SetPermissionElement(btnPrint, btnExport);
            }
        }

        public void _refresh(DataTable DT)
        {
            this._DMCore.Grid.GridControl.DataSource = DT;
            this._DMCore.Grid.GridControl.RefreshDataSource();
            try { ((PLGridView)this._DMCore.Grid)._RefreshLayout(); }
            catch { }
        }
        #endregion

        #region Dùng như control input trong form Category - ItemCategory
        /// <summary>
        /// Unique:     N Field
        /// Phân quyền: Có (Chưa xử lý), 
        /// Cho thêm    Có Nếu readOnlyField = null hoặc không truyền vào
        ///             Không Nếu readOnlyFiled !=null (chỉ cập nhật)
        /// VISIBLE_BIT:Không
        /// </summary>
        public DMGrid(string TableName, string IDField, string DislayField, string[] NameFields, string[] Subjects, 
            DMBasicGrid.InitGridColumns InitGridCol, DMBasicGrid.GetRule Rule, 
            DelegationLib.DefinePermission permission, 
            params string[] readOnlyField)
        {
            InitializeComponent();

            _init(GroupElementType.ONLY_INPUT, TableName, IDField, DislayField, NameFields, Subjects, 
                InitGridCol, Rule,
                permission, 
                readOnlyField);
        }

        /// <summary>
        /// Unique:         1 Field
        /// Phân quyền:     Không
        /// VISIBLE_BIT:    Không
        /// Cho thêm:       Có
        /// </summary>
        public DMGrid(string TableName, string IDField, string NameField, string Subject,
                DMBasicGrid.InitGridColumns InitGridCol, DMBasicGrid.GetRule Rule)
            : this( TableName, IDField, NameField, new string[] { NameField }, 
                    new string[] { Subject }, InitGridCol, Rule, null)
        { 
        }

        public DMGrid(string TableName)
            :this(TableName, "ID", "NAME", 
                new string[]{"NAME"},
                new string[]{"Tên"},
                DMBasicGrid.CreateDM_BASIC_No_V, 
                DMBasicGrid.GetRuleDM_BASIC,
                null,
                null)
        {
        }

        public DMGrid(string TableName, bool Visible)
            : this(TableName, "ID", "NAME",
                    new string[] { "NAME" },
                    new string[] { "Tên" },
                    DMBasicGrid.CreateDM_BASIC_V,
                    DMBasicGrid.GetRuleDM_BASIC,
                    null,
                    null)
        {                       
        }

        public void DefinePermission(DelegationLib.DefinePermission permission)
        {
            this.permission = permission;
        }

        #endregion

        public GridView Grid
        {
            get { return _DMCore.Grid; }
        }
        public DMBasicGrid BasicTemplate
        {
            get
            {
                return this._DMCore;
            }
        }
        public DMBasicGrid dmBasicGrid
        {
            get
            {
                return this._DMCore;
            }
        }

        private void Grid_DoubleClick(object sender, EventArgs e)
        {
            if (popupContainer != null && btnSelect.Visible == true)
                btnSelect_Click(null, null);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && popupContainer!=null && btnSelect.Visible == true) 
                btnSelect_Click(null, null);
        }

        private void SetMode(GroupElementType type)
        {
            this.btnSave.Enabled = false;
            this.btnNoSave.Enabled = false;

            if (type == GroupElementType.ONLY_INPUT)
            {
                ChonSep.Visible = false;
                btnSelect.Visible = false;
                btnNoSelect.Visible = false;

                Grid.OptionsBehavior.ImmediateUpdateRowPosition = true;
            }
            else if (type == GroupElementType.ONLY_CHOICE)
            {
                btnAdd.Visible = false;
                btnDel.Visible = false;
                btnUpdate.Visible = false;

                ChonSep.Visible = true;
                btnSelect.Visible = true;
                btnNoSelect.Visible = true;

                this.DongSep.Visible = false;
                this.btnClose.Visible = false;

                this.LuuSep.Visible = false;
                this.btnSave.Visible = false;
                this.btnNoSave.Visible = false;

                btnSelect.Visible = false;
                btnNoSelect.Visible = false;

                btnPrint.Visible = false;
                btnExport.Visible = false;

            }
            else if (type == GroupElementType.CHOICE_N_ADD)
            {
                btnDel.Visible = true;
                btnUpdate.Visible = true;

                this.DongSep.Visible = false;
                this.btnClose.Visible = false;

                btnPrint.Visible = false;
                btnExport.Visible = false;
            }
        }

        private void EditMode(params string[] readOnlyField)
        {
            if (readOnlyField != null && readOnlyField.Length >0)
            {
                btnAdd.Visible = false;
                btnDel.Visible = false;

                foreach (string field in readOnlyField)
                {
                    Grid.Columns[field].OptionsColumn.AllowEdit = false;
                    Grid.Columns[field].OptionsColumn.AllowFocus = false;
                }
            }
        }

        public void setPopupControl(PopupContainerEdit popupConEdit)
        {
            this.popupContainer = popupConEdit;
            
        }

        #region GET & SET trên cây
        public long getSelectedID()
        {
            if (RowSelected == null) return -1;
            return HelpNumber.ParseInt64(RowSelected[IDField]);
        }

        public String getDislayText()
        {
            if (RowSelected != null) return RowSelected[DislayField].ToString();
            if (this.popupContainer != null) return this.popupContainer.Properties.NullText;
            else return "";
        }

        public int setSelectedID(long id)
        {
            DataTable dt = ((DataView)(_DMCore.Grid.DataSource)).Table;
            try
            {
                DataRow[] dr = dt.Select(IDField + " ='" + id.ToString() + "'");
                if (dr == null || dr.Length == 0)
                {
                    RowSelected = null;
                    this.Grid.FocusedRowHandle = -1;
                    return -1;
                }
                _DMCore.Grid.FocusedRowHandle = _DMCore.Grid.GetRowHandle(dt.Rows.IndexOf(dr[0]));
                //RowSelected = dm.Grid.GetDataRow((dm.Grid.GetSelectedRows())[0]);                 
                RowSelected = dr[0];
            }
            catch {
                RowSelected = null;
                this.Grid.FocusedRowHandle = -1;
                return -1;
            }
            return _DMCore.Grid.FocusedRowHandle;
        }
        #endregion

        private GridColumn[] InitGridCol(GridControl gridControl, GridView gridView)
        {
            return new GridColumn[] { };
        }
        private FieldNameCheck[] GetRule(object param)
        {
            return new FieldNameCheck[] { };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_UseAddEvent)
                _DMCore.AddAction(null);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_UseDeleteEvent)
                _DMCore.DeleteAction(null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_UseUpdateEvent)
                _DMCore.EditAction(null);
        }

        public void btnSelect_Click(object sender, EventArgs e)
        {
            if (_DMCore.Grid.OptionsBehavior.Editable == false)
            {
                if (_DMCore.Grid.SelectedRowsCount > 0)
                    RowSelected = _DMCore.Grid.GetDataRow((_DMCore.Grid.GetSelectedRows())[0]);

                if (popupContainer != null) popupContainer.ClosePopup();
            }
            else
                HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập.");
        }

        public bool flagNoSelectClick = false;
        private void btnNoSelect_Click(object sender, EventArgs e)
        {
            if (_DMCore.Grid.OptionsBehavior.Editable == false)
            {
                RowSelected = null;
                flagNoSelectClick = true;
                if (popupContainer != null) popupContainer.ClosePopup();
                flagNoSelectClick = false;
            }
            else
                HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập.");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _DMCore.SaveAction(null);
        }

        private void btnNoSave_Click(object sender, EventArgs e)
        {
            _DMCore.NoSaveAction(null);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            if (popupContainer != null)
            {
                popupContainer.ClosePopup();
            }
            else
            {
                this.Dispose();
            }
        }

        #region IActionCategory Members

        public ToolStrip GetMenuAction()
        {
            return this.btnBar;
        }

        #endregion
        #region ICategory Members

        public void SetOwner(IFormCategory owner)
        {
        }

        public void UpdateGUI()
        {
            this.btnClose.Visible = false;
        }

        public GridView GetGridView()
        {
            return this.Grid;
        }
        #endregion
        #region IPermisionable Members

        public List<Control> GetPermisionableControls()
        {
            throw new NotImplementedException();
        }

        public List<object> GetObjectItems()
        {
            if (permission == null) return null;
            return permission(this);
        }

        #endregion
        #region IPluginCategory Members

        public object AddAction(object param)
        {
            return this._DMCore.AddAction(param);
        }
        public object AddChildAction(object param)
        {
            return this._DMCore.AddChildAction(param);
        }
        public object EditAction(object param)
        {
            return this._DMCore.EditAction(param);
        }
        public object DeleteAction(object param)
        {
            return this._DMCore.DeleteAction(param);
        }
        public object PrintAction(object param)
        {
            return this._DMCore.PrintAction(param);
        }
        public object SaveAction(object param)
        {
            return this._DMCore.SaveAction(param);
        }
        public object NoSaveAction(object param)
        {
            return this._DMCore.NoSaveAction(param);
        }
        #endregion
        #region IFormatable Members

        public List<Control> GetFormatControls()
        {
            return new List<Control>();
        }

        #endregion
    }
}
