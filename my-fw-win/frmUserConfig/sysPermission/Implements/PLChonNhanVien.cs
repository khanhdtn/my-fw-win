using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
namespace ProtocolVN.Framework.Win
{     
    partial class PLChonNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        #region HUNG
        public delegate void DlgGetSelectDataset(DataSet ds);
        public delegate void DlgGetUnSelectDataset();
        DlgGetSelectDataset dlgGetSelectDataset = null;        
        public DlgGetSelectDataset mDlgGetSelectDataset
        {
            get
            {
                return dlgGetSelectDataset;
            }
            set
            {
                dlgGetSelectDataset = value;
            }
        }
        DlgGetUnSelectDataset dlgGetUnSelectDataset = null;   
        public DlgGetUnSelectDataset mDlgGetUnSelectDataset
        {
            get
            {
                return dlgGetUnSelectDataset;
            }
            set
            {
                dlgGetUnSelectDataset = value;
            }
        }
        public void mVisibleGridColumn(string[] fieldNames)
        {
            foreach (GridColumn col in gridView_1.Columns)
                foreach (string s in fieldNames)        
                        if (col.FieldName.ToLower() ==s.ToLower())
                            col.Visible = false;  
        }
        
        public bool m_IsMultiselect
        {
            set
            {
                if (value == true)                                    
                    gridView_1.OptionsSelection.MultiSelect = true;                
            }
        }
        #endregion

        //private GroupElementType type;      //Loại group element
        private String LinkField;           //Field liên kết giữa group và element
        private String displayField;        //Field trình bày khi chọn 
        private String genName;             //Tên genName của element
        public TreeListNode selectNode = null;

        public PLChonNhanVien()
        {
            InitializeComponent();
            this.btnSelect.Image = FWImageDic.CHOICE_IMAGE20;
            this.btnNoSelect.Image = FWImageDic.NO_CHOICE_IMAGE20;
            
        }

        //Quan trọng : dtGrid bắt buộc phải có 1 Field làm khóa chính và một gen nam la G_Table_Name
        public void Init(
            String tableTreeName, String GroupIDField, String GroupParentIDField, String[] visibleFieldTree, String[] captionVisibleFieldTree,
            DataSet dtGrid, String[] allCaption, String LinkField, String displayField, String genName)
        {
            this.genName = genName;
            this.LinkField = LinkField;
            this.displayField = displayField;
            this.UpdateActionItem();
            PLDynamicGrid pl = new PLDynamicGrid(this.GridControl_1, dtGrid, true, allCaption, this.gridView_1);
            this.gridView_1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            this.gridView_1.OptionsBehavior.Editable = false;
            pl.gv.OptionsSelection.EnableAppearanceFocusedRow = true;
            pl.gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            pl.gv.OptionsSelection.MultiSelect = false;
            pl.gv.Columns[LinkField].Tag = "CBB?" + tableTreeName + "?" + GroupIDField + "?" + visibleFieldTree[0];
            //XtraGridSupport.CBBTagToImageComboBox(pl.gv);
            pl.gv.Columns["ID"].Visible = false;    //ẩn column ID

            this.TreeList_1._BuildTree(tableTreeName, GroupIDField, GroupParentIDField, visibleFieldTree, captionVisibleFieldTree);
            TreeList_1.Columns[0].Visible = false;
            this.TreeList_1.OptionsView.ShowColumns = true;
            this.TreeList_1.ExpandAll();
        }


        #region Cập nhật trạng thái của các nút sự kiện
        private void UpdateActionItem()
        {
            ChonSep.Visible = true;
            btnSelect.Visible = true;
            btnNoSelect.Visible = true;
            TreeList_1.AllowDrop = false;
            this.DongSep.Visible = false;
            this.Close.Visible = false;
        }
        #endregion

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                int[] select = gridView_1.GetSelectedRows();
                if (select != null && select.Length > 0)
                {
                    //DataRow dr = gridView_1.GetDataRow(select[0]);
                    //this.selectedID = int.Parse(dr["ID"].ToString());
                    ////this.selectedID = InputTool.ParseInt32(dr["ID"].ToString());
                    //this.displayData = dr[displayField].ToString();
                    //focusedRow = select[0];

                    //HUNG
                    if (dlgGetSelectDataset != null)
                    {
                        DataSet dsSelect = (GridControl_1.DataSource as DataTable).DataSet.Clone();
                        foreach (int index in select)
                            dsSelect.Tables[0].Rows.Add(gridView_1.GetDataRow(index).ItemArray);
                        dlgGetSelectDataset(dsSelect);                        
                        Close_Click(null, null);
                    }
                    //-----------------
                }
                selectNode = TreeList_1.Selection[0];
            }
            catch 
            {
                return;
            }
        }

        private void btnNoSelect_Click(object sender, EventArgs e)
        {            
            try
            {
                selectNode = null;
            }
            catch
            {
                return;
            }

            if(dlgGetUnSelectDataset!=null) dlgGetUnSelectDataset();
            //HUNG
            Close_Click(null, null);
            //---------------
        }
        private void gridView_1_DoubleClick(object sender, EventArgs e)
        {
            btnSelect_Click(null, null);
        }
        private void Close_Click(object sender, EventArgs e)
        {
            if (this.Parent is PopupContainerControl)
            {
                PopupContainerControl temp = this.Parent as PopupContainerControl;
                temp.PopupContainerProperties.OwnerEdit.ClosePopup();
            }
            else this.Dispose();
        }
        public void SetShowChonNhanVien(SimpleButton btnChon)
        {
            if (this.Parent == null)
            {
                //tao khung chua GroupElement
                PopupContainerControl popupContainerControl1 = new PopupContainerControl();
                this.Dock = DockStyle.Fill;
                popupContainerControl1.Width = 450;
                popupContainerControl1.Height = 300;
                this.Size = popupContainerControl1.Size;
                popupContainerControl1.Controls.Add(this);

                //gan khung chua vao popupContainerEdit1
                PopupContainerEdit popupContainerEdit1 = new PopupContainerEdit();
                popupContainerEdit1.Size = new Size(0, 0);
                popupContainerEdit1.Properties.PopupControl = popupContainerControl1;//gan vao edit

                //gan popupContainerEdit1 vao dropDownButtonChonUser
                btnChon.Controls.Add(popupContainerEdit1);//them vao dropDownButtonChonUser                
            }
            //dang ky su kien ben trong, do do chi dat ham nay trong khoi dung
            btnChon.Click += delegate(object sender, EventArgs e)
            {
                foreach (Control control in btnChon.Controls)
                    if (control is PopupContainerEdit)
                        ((PopupContainerEdit)control).ShowPopup();
            };
        }
        private void PLChonNhanVien_Load(object sender, EventArgs e)
        {
            FWImageDic.GET_GROUP_ELEM_IMAGE16(this.imageList1);
        }
        private void TreeList_1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && gridView_1.DataSource != null)       
               (GridControl_1.DataSource as DataTable).DefaultView.RowFilter = LinkField + " =" + e.Node.GetValue(0);            
        }        
    }
}