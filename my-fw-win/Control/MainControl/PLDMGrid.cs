using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    sealed public partial class PLDMGrid : DevExpress.XtraEditors.XtraUserControl, IPermisionable
    {
        #region Biến
        private string DislayField = null;
        private bool IsUpDownKey = false;
        private bool IsFilter = true;
        private long selectId = -1;
        private System.EventHandler text;
        /// <summary>Cho phải tự tính kích thước của popup hoặc cố định
        /// </summary>
        public bool isFixPopupContainer = false;

        #endregion

        #region Thuộc tính
        #endregion

        #region Size
        [Browsable(true), Category("_PROTOCOL"), Description("Kích thước Popup gấp bao nhiêu so với Control")]
        public float ZZZWidthFactor
        {
            get { return _WidthFactor; }
            set { _WidthFactor = value; }
        }
        private float _WidthFactor = 2;//Chiều rộng của popup tùy vào số này
        void PLDMGrid_Resize(object sender, EventArgs e)
        {
            if(isFixPopupContainer == false)
                _CalcSize();
        }
        private void _CalcSize()
        {
            if (this.popupContainerControl1.Size.Width != (int)(this.Size.Width * _WidthFactor))
            {
                this.popupContainerControl1.Size = new Size((int)(this.Size.Width * _WidthFactor), this.popupContainerControl1.Size.Height);
            }
        }
        void PLDMGrid_Load(object sender, EventArgs e)
        {
            if (isFixPopupContainer == false)
                _CalcSize();
            this.Load -= PLDMGrid_Load;
        }
        #endregion

        #region Khởi tạo
        public PLDMGrid()
        {
            InitializeComponent();
            this.popupContainerControl1.PopupContainerProperties.CloseOnOuterMouseClick = false;
            popupContainerEdit1.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;

            this.popupContainerEdit1.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(popupContainerEdit1_CloseUp);
            this.popupContainerEdit1.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(popupContainerEdit1_Closed);
            dmGridTemplate1.setPopupControl(popupContainerEdit1);
            this.popupContainerEdit1.Popup += new EventHandler(popupContainerEdit1_Popup);
            this.Resize += new EventHandler(PLDMGrid_Resize);
            this.Load += new EventHandler(PLDMGrid_Load);
        }

        
        public void _init(GroupElementType type, string TableName, string IDField, string DislayField, string[] NameFields,
           string[] Subjects, DMBasicGrid.InitGridColumns InitGridCol, DMBasicGrid.GetRule Rule, DelegationLib.DefinePermission permission, params string[] editField)
        {
            dmGridTemplate1._init(type, TableName, IDField, DislayField, NameFields, Subjects, InitGridCol, Rule, permission, editField);
            this.DislayField = DislayField;
        }

        public void _init(GroupElementType type, DataTable DataSource, string IDField, string DislayField, string[] NameFields,
            string[] Subjects, DMBasicGrid.InitGridColumns InitGridCol, DMBasicGrid.GetRule Rule, DelegationLib.DefinePermission permission,
            PLDelegation.ProcessDataRow InsertFunc, PLDelegation.ProcessDataRow DeleteFunc, PLDelegation.ProcessDataRow UpdateFunc,
            params string[] editField)
        {
            dmGridTemplate1._init(  type, DataSource, IDField, DislayField, NameFields, Subjects, InitGridCol, Rule, permission, 
                                    InsertFunc, DeleteFunc, UpdateFunc, editField);
            this.DislayField = DislayField;
        }
        
        #endregion

        #region Sử dụng
        #region GET & SET

        public long _getSelectedID()
        {
            return dmGridTemplate1.getSelectedID();
        }

        public void _setSelectedID(long id)
        {
            int flag = dmGridTemplate1.setSelectedID(id);
            if (id != -1)
            {
                if (flag != -1)
                {
                    popupContainerEdit1.Text = dmGridTemplate1.getDislayText();
                    return;
                }
            }
            popupContainerEdit1.Text = popupContainerEdit1.Properties.NullText;
        }

        #endregion
        public void _setOtherInfo(bool IsShowFilter, int W, int H)
        {
            SetOtherInfo(IsShowFilter, W, H);
        }
        public void SetOtherInfo(bool IsShowFilter, int W, int H)
        {
            this.dmGridTemplate1.Grid.OptionsView.ShowAutoFilterRow = IsShowFilter;
            this.popupContainerControl1.Width = W;
            this.popupContainerControl1.Height = H;
        }

        public void _showImmediatePopup()
        {
            ShowImmediatePopup();
        }
        public void ShowImmediatePopup()
        {
            this.popupContainerEdit1.KeyDown += new KeyEventHandler(popupContainerEdit1_KeyDown);
            this.popupContainerEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            text = new System.EventHandler(this.popupContainerEdit1_TextChanged);
            this.popupContainerEdit1.TextChanged += text;
            this.popupContainerEdit1.Properties.NullText = GlobalConst.NULL_TEXT;
        }

        public void _refresh(DataTable DataSource)
        {
            this.dmGridTemplate1._refresh(DataSource);
        }
        #endregion

        #region Xử lý Filter
        private void popupContainerEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dmGridTemplate1.Grid.RowCount > 0)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    IsFilter = false;
                    if (IsUpDownKey)
                    {
                        dmGridTemplate1.Grid.Focus();
                        try
                        {
                            int index = (dmGridTemplate1.Grid.GetSelectedRows())[0];
                            dmGridTemplate1.Grid.FocusedRowHandle = (e.KeyCode == Keys.Down) ? index + 1 : index - 1;
                        }
                        catch { }

                    }
                }
                //else if (e.KeyCode == Keys.Enter)
                //{
                //    dmGridTemplate1.btnSelect_Click(null, null);
                //}
                else
                    IsFilter = true;
            }
            else
            {
                IsFilter = true;
            }
        }
        private void popupContainerEdit1_TextChanged(object sender, EventArgs e)
        {
            if (IsFilter && popupContainerEdit1.EditorContainsFocus && popupContainerEdit1.Text!="")
            {
                dmGridTemplate1.Grid.ActiveFilterString = "[" + DislayField + "]" + " Like " + "'%" + popupContainerEdit1.Text + "%'";
                if (dmGridTemplate1.Grid.RowCount >= 0)
                {
                    popupContainerEdit1.ShowPopup();
                    popupContainerEdit1.Focus();
                    IsUpDownKey = true;
                }
            }
        }
        #endregion

        #region Kiểm tra dữ liệu
        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorMsg)
        {
            errorProvider.SetError(this.popupContainerEdit1, errorMsg);
        }
        #endregion

        #region Sự kiện trên Popup
        private void popupContainerEdit1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (dmGridTemplate1.Grid.OptionsBehavior.Editable == true)
            {
                this.popupContainerEdit1.ShowPopup();
            }
        }
        private void popupContainerEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            //DataRow selectRow = dmGridTemplate1.RowSelected;
            selectId = dmGridTemplate1.getSelectedID();
            popupContainerEdit1.Text = dmGridTemplate1.getDislayText();
        }
        private void popupContainerEdit1_Popup(object sender, EventArgs e)
        {
            dmGridTemplate1.setSelectedID(selectId);
        }
        #endregion

        #region IPermisionable Members

        public List<Control> GetPermisionableControls()
        {
            throw new NotImplementedException();
        }

        

        #endregion

        public DMGrid GetDMGrid
        {
            get { return dmGridTemplate1; }
        }

        #region IPermisionable Members


        public List<object> GetObjectItems()
        {
            return dmGridTemplate1.GetObjectItems();
        }

        public void DefinePermission(DelegationLib.DefinePermission permission)
        {
            dmGridTemplate1.DefinePermission(permission);
        }
        #endregion
    }
}
