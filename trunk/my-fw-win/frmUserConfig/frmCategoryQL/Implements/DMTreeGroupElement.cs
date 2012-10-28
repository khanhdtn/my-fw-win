using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ProtocolVN.Framework.Core;
namespace ProtocolVN.Framework.Win
{
    public partial class DMTreeGroupElement : DevExpress.XtraEditors.XtraUserControl, IActionCategory, IPluginCategory
    {
        public TreeListNode SelectedNode = null;
        public int SelectedRow = -1;
        public delegate GridColumn[] InitGridColumns(GridControl gridControl, GridView gridView);
        public delegate FieldNameCheck[] GetRule(object param);

        private long selectedID = -1;        
        private GroupElementType type;
        private string genName;
        private string IDField;
        private string LinkField;
        private string DisplayField;
        private string GroupIDField;
        
        private GetRule Rule;
        public PopupContainerEdit popUp;

        private String elementTable;
        private bool IsGridVisibleBit = false;
        private bool IsTreeVisibleBit = false;
        private bool ignore = false;//Có xử lý hay ko xử lý ngoại lệ
        private bool isEdit = false;//Cho biết có đang ở chế độ edit không 

        public bool forceExitCtrl = false;//True: Bat buoc dong popUp bang moi gia.
        public ToolStripDropDownButton btnPrint;
        public ToolStripDropDownButton btnExport;
        #region Danh sách sự kiện
        public delegate void BeforeSave(DMTreeGroupElement sender);
        public event BeforeSave _BeforeSaveEvent;

        public delegate void AfterSaveSucc(DMTreeGroupElement sender);
        public event AfterSaveSucc _AfterSaveSuccEvent;

        public delegate void AfterSaveFail(DMTreeGroupElement sender);
        public event AfterSaveFail _AfterSaveFailEvent;

        public delegate void BeforeDel(DMTreeGroupElement sender);
        public event BeforeDel _BeforeDelEvent;

        public delegate void AfterDelSucc(DMTreeGroupElement sender);
        public event AfterDelSucc _AfterDelSuccEvent;

        public delegate void AfterDelFail(DMTreeGroupElement sender);
        public event AfterDelFail _AfterDelFailEvent;
        #endregion

        #region Dùng như chọn lựa
        public DMTreeGroupElement()
        {
            InitializeComponent();
            gridView_1.KeyDown += new KeyEventHandler(Grid_KeyDown);
            GridValidation.NotAllowValidateGrid(this.gridView_1);
            //this.gridView_1.InvalidRowException -= new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(XtraGridSupport.AllowValidateGrid_Event);
            this.gridView_1.InvalidRowException += delegate(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
            {
                if (e.ExceptionMode == DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError)
                {
                    if (PLMessageBox.ShowConfirmMessage("Thông tin bạn vào không hợp lệ. Bạn có muốn vào lại thông tin?") == DialogResult.Yes)
                    {
                        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
                        ignore = true;
                    }
                    else//Có thể ko dùng
                    {
                        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
                        ((DataRowView)e.Row).Row.ClearErrors();
                        ShowGrid(true);
                    }
                }
                else if (e.ExceptionMode == DevExpress.XtraEditors.Controls.ExceptionMode.Ignore)
                {
                    ShowGrid(true);
                }
                else
                {
                    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
                }
            };
            this.TreeList_1.KeyUp += new KeyEventHandler(TreeList_1_KeyUp);
        }

        void TreeList_1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Alt && e.KeyValue == 191)
            {
                HelpGrid.setFocusFilterRow(this.gridView_1);
                e.Handled = true;
            }
        }
        /// <summary>
        /// VISIBLE_BIT :   Có trên lưới - Có trên cây
        /// Unique      :   Không hỗ trợ
        /// RootID      :   Có
        /// Phân quyền  :   Không
        /// </summary>
        public void Init(GroupElementType type,
            string GroupTableName, int[] RootID, string GroupIDField,
            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
            String elementTable, string IDField, string LinkField, string DisplayField, string GenName,
            InitGridColumns InitGridCol, GetRule Rule, bool IsGridVisibleBit, bool IsTreeVisibleBit)
        {
            this.IsGridVisibleBit = IsGridVisibleBit;
            this.IsTreeVisibleBit = IsTreeVisibleBit;

            this.type = type;
            this.genName = GenName;
            this.IDField = IDField;
            this.LinkField = LinkField;
            this.DisplayField = DisplayField;
            this.GroupIDField = GroupIDField;
            this.Rule = Rule;
            btnExport = HelpGrid.addXuatRaFileItem(this.toolStrip_1, this.gridView_1);
            btnPrint = HelpGrid.addInLuoiItem(this.toolStrip_1, this.gridView_1);
            this.UpdateActionItem(type);
            //Khởi tạo cây
            this.TreeList_1._BuildTree(GroupTableName, RootID, GroupIDField, GroupParentIDField, GroupVisibleFields, GroupCaptions, null, IsTreeVisibleBit);
            this.TreeList_1.OptionsView.ShowColumns = true;
            this.TreeList_1.BestFitColumns();
            this.TreeList_1.ExpandAll();

            //Khởi tạo lưới
            InitGridCol(this.GridControl_1, this.gridView_1);
            this.ShowGrid(true);
            this.gridView_1.OptionsSelection.MultiSelect = false;
            this.elementTable = elementTable;

            if (this.TreeList_1.FocusedNode != null)
            {
                RefreshGridData(this.TreeList_1.FocusedNode);
            }
        
            gridView_1._SetPermissionElement(btnPrint, btnExport);
        }


        /// <summary>
        /// VISIBLE_BIT :   Có trên lưới - Có trên cây
        /// Unique      :   Không hỗ trợ
        /// RootID      :   Có
        /// Phân quyền  :   Không
        /// </summary>
        public void Init(GroupElementType type,
            string GroupTableName, int[] RootID, string GroupIDField,
            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
            DataSet dtGrid, string IDField, string LinkField, string DisplayField, string GenName,
            InitGridColumns InitGridCol, GetRule Rule, bool IsGridVisibleBit, bool IsTreeVisibleBit)
        {
            this.IsGridVisibleBit = IsGridVisibleBit;
            this.IsTreeVisibleBit = IsTreeVisibleBit;

            this.type = type;
            this.genName = GenName;
            this.IDField = IDField;
            this.LinkField = LinkField;
            this.DisplayField = DisplayField;
            this.GroupIDField = GroupIDField;
            this.Rule = Rule;
            btnExport = HelpGrid.addXuatRaFileItem(this.toolStrip_1, this.gridView_1);
            btnPrint = HelpGrid.addInLuoiItem(this.toolStrip_1, this.gridView_1);

            this.UpdateActionItem(type);
            //Khởi tạo cây
            this.TreeList_1._BuildTree(GroupTableName, RootID, GroupIDField, GroupParentIDField, GroupVisibleFields, GroupCaptions, null, IsTreeVisibleBit);
            this.TreeList_1.OptionsView.ShowColumns = true;
            this.TreeList_1.BestFitColumns();
            this.TreeList_1.ExpandAll();

            //Khởi tạo lưới
            InitGridCol(this.GridControl_1, this.gridView_1);
            this.ShowGrid(true);

            this.gridView_1.OptionsSelection.MultiSelect = false;
            this.GridControl_1.DataSource = dtGrid.Tables[0];
            this.elementTable = dtGrid.Tables[0].TableName;

            if (this.TreeList_1.FocusedNode != null)
            {
                ////Kỳ lạ tìm chỗ đã set FocusNode vào Node đầu tiên
                //TreeListNode tmp = this.TreeList_1.FocusedNode;
                //this.TreeList_1.SetFocusedNode(tmp.RootNode);
                //this.TreeList_1.Select();
                RefreshGridData(this.TreeList_1.FocusedNode);
            }
      
            gridView_1._SetPermissionElement(btnPrint, btnExport);

        }

        /// <summary>
        /// VISIBLE_BIT :   Không trên lưới - Không trên cây
        /// Unique      :   Không hỗ trợ
        /// RootID      :   Có
        /// Phân quyền  :   Không
        /// </summary>
        public void Init(GroupElementType type,
            string GroupTableName, int[] RootID, string GroupIDField,
            string GroupParentIDField, string[] GroupVisibleFields, string[] GroupCaptions,
            DataSet dtGrid, string IDField, string LinkField, string DisplayField, string GenName,
            InitGridColumns InitGridCol, GetRule Rule)
        {
            Init(type,
                GroupTableName, RootID, GroupIDField,
                GroupParentIDField, GroupVisibleFields, GroupCaptions,
                dtGrid, IDField, LinkField, DisplayField, GenName,
                InitGridCol, Rule, false, false);
        }
        #endregion

        private void PLGroupElement_Load(object sender, EventArgs e)
        {
            this.gridView_1.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            FWImageDic.GET_GROUP_ELEM_IMAGE16(this.imageList1);
            this.btnAdd.Image = FWImageDic.ADD_IMAGE20;
            this.btnDel.Image = FWImageDic.DELETE_IMAGE20;
            this.btnUpdate.Image = FWImageDic.EDIT_IMAGE20;
            this.btnSelect.Image = FWImageDic.CHOICE_IMAGE20;
            this.btnNoSelect.Image = FWImageDic.NO_CHOICE_IMAGE20;
            this.btnInfo.Image = FWImageDic.DETAIL_IMAGE20;
            this.Close.Image = FWImageDic.CLOSE_IMAGE20;
            this.btnSave.Image = FWImageDic.SAVE_IMAGE20;
            this.btnNoSave.Image = FWImageDic.NO_SAVE_IMAGE20;
        }

        #region Dùng cho tùy biến - Chưa dùng đến.
        public delegate XtraForm Add_Delegate();
        public delegate XtraForm Edit_Delegate(string id);
        public delegate XtraForm View_Delegate(string id);

        public Add_Delegate add;
        public Edit_Delegate edit;
        public View_Delegate view;
        #endregion
        
        #region Popup
        public void setPopUp(DevExpress.XtraEditors.PopupContainerEdit popUp)
        {
            this.popUp = popUp;
        }
        #endregion

        #region GET & SET
        public long _getSelectedID()
        {
            return this.selectedID;
        }
        public string getDislayText()
        {
            if (SelectedRow == -1)
            {
                if (popUp != null) return popUp.Properties.NullText;
                else return "";
            }
            else
            {
                _setSelectedID(this.selectedID);
                
                DataRow row = this.gridView_1.GetDataRow(SelectedRow);
                if (row == null)
                {
                    if (popUp != null) return popUp.Properties.NullText;
                    else return "";
                }
                return row[DisplayField].ToString();
            }
        }
        public DataRow _setSelectedID(long id)
        {
            try
            {
                this.selectedID = id;
                string tablename = this.elementTable;
                DataSet ds = new DataSet();
                QueryBuilder query = null;

                if (IsGridVisibleBit == true)
                    query = new QueryBuilder("SELECT " + IDField + "," +LinkField + " FROM " + tablename + " WHERE VISIBLE_BIT = 'Y' AND 1=1");
                else
                    query = new QueryBuilder("SELECT " + IDField + "," + LinkField + " FROM " + tablename + " WHERE 1=1");

                query.add(IDField, Operator.Equal, id, DbType.Int64);
                ds = DABase.getDatabase().LoadDataSet(query, tablename);
                if (ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count>0 && ds.Tables[0].Rows[0] != null)
                {
                    //Focus Tree
                    this.TreeList_1.FocusedNode = this.TreeList_1.FindNodeByKeyID(ds.Tables[0].Rows[0][LinkField]);
                    SelectedNode = TreeList_1.FocusedNode;

                    DataTable table = ((DataView)this.gridView_1.DataSource).Table;
                    for(int i = 0; i <table.Rows.Count; i++){
                        if(HelpNumber.ParseInt64(table.Rows[i][IDField]) == id){
                            this.gridView_1.FocusedRowHandle = this.gridView_1.GetRowHandle(i);
                            SelectedRow = this.gridView_1.FocusedRowHandle;
                            break;
                        }
                    }

                    return this.gridView_1.GetDataRow(this.gridView_1.FocusedRowHandle);
                }
            }
            catch (Exception ex) { PLException.AddException(ex); }
            return null;
        }
        public DataRow getSelectedRow()
        {
            return ((DataRowView)this.gridView_1.GetRow(this.SelectedRow)).Row;
        }
        #endregion
        
        #region Cập nhật trạng thái của các nút sự kiện
        private void UpdateActionItem(GroupElementType type)
        {
            if (view != null)
            {
                ChiTietSep.Visible = true;
                btnInfo.Visible = true;
            }

            if (type == GroupElementType.ONLY_INPUT)
            {
                ChonSep.Visible = false;
                btnSelect.Visible = false;
                btnNoSelect.Visible = false;

                TreeList_1.AllowDrop = true;
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
                this.Close.Visible = false;

                this.LuuSep.Visible = false;
                this.btnSave.Visible = false;
                this.btnNoSave.Visible = false;

                btnExport.Visible = false;
                btnPrint.Visible = false;

                TreeList_1.AllowDrop = false;

                this.GridControl_1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.GridControl_1_MouseMove);
                this.GridControl_1.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.GridControl_1_MouseDown);
            }
            else if (type == GroupElementType.CHOICE_N_ADD)
            {
                btnDel.Visible = true;
                btnUpdate.Visible = true;

                this.DongSep.Visible = false;
                this.Close.Visible = false;

                btnExport.Visible = false;
                btnPrint.Visible = false;

                this.GridControl_1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.GridControl_1_MouseMove);
                this.GridControl_1.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.GridControl_1_MouseDown);
                TreeList_1.AllowDrop = true;
            }
        }
        #endregion

        #region Nút hành động

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && popUp != null && btnSelect.Visible == true)
                btnSelect_Click(null, null);
        }
        private void GridControl_1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter) gridView_1_DoubleClick(null, null);
            //if (e.KeyCode == Keys.Enter) btnSelect_Click(null, null);
        }

        public void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_1.Editable == false)
                {
                    int[] select = gridView_1.GetSelectedRows();
                    if (select != null && select.Length > 0)
                    {
                        DataRow dr = gridView_1.GetDataRow(select[0]);
                        this.selectedID = HelpNumber.ParseInt64(dr[this.IDField]);

                        SelectedNode = TreeList_1.FocusedNode;
                        SelectedRow = this.gridView_1.FocusedRowHandle;

                        if (popUp != null) { popUp.CancelPopup(); }
                        //if (popUp != null) { popUp.ClosePopup(); }
                    }
                    else
                    {
                        HelpMsgBox.ShowNotificationMessage("Vui lòng chọn phần tử.");
                    }
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập.");
                }
                forceExitCtrl = true;
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        private void btnNoSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView_1.Editable == false)
                {
                    this.selectedID = -1;
                    SelectedNode = null;
                    SelectedRow = -1;
                    if (popUp != null)
                    {
                        if (gridView_1.Editable == false) popUp.CancelPopup();
                        //popUp.ClosePopup();
                    }
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập.");
                }
                forceExitCtrl = true;
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.add != null) // hiển thị formAdd
            {
                try{
                    XtraForm myform = this.add();
                    ProtocolForm.ShowModalDialog((XtraForm)this.FindForm(), myform);
                    this.RefreshGridData(this.TreeList_1.FocusedNode);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            else // Add ngay trên gridview
            {
                if (this.gridView_1.Editable == false) isEdit = false;
                if (isEdit == false)
                {
                    this.ShowGrid(false);
                    this.gridView_1.AddNewRow();
                    this.gridView_1.FocusedColumn = this.gridView_1.VisibleColumns[0];
                    //this.gridView_1.ShowEditor();
                }
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.edit != null)
            {
                try{
                    long id = HelpNumber.ParseInt64(this.gridView_1.GetFocusedRowCellValue(IDField));
                    XtraForm myform = this.edit(id.ToString());
                    ProtocolForm.ShowModalDialog((XtraForm)this.FindForm(), myform);
                    this.RefreshGridData(this.TreeList_1.FocusedNode);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            else
            {
                if (gridView_1.FocusedRowHandle > -1 && this.gridView_1.GetDataRow(gridView_1.FocusedRowHandle) != null)
                {
                    //HelpGrid.ShowRecordDialog((XtraForm)this.GridControl_1.FindForm(), this.GridControl_1, gridView_1, this.gridView_1.GetFocusedDataRow(), false);
                    isEdit = true;
                    this.ShowGrid(false);
                    //this.gridView_1.FocusedColumn = this.gridView_1.VisibleColumns[0];
                    //this.gridView_1.ShowEditor();
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Bạn chưa chọn dữ liệu. Vui lòng chọn dữ liệu bạn muốn cập nhật!");
                }
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (gridView_1.FocusedRowHandle == GridControl.AutoFilterRowHandle
                || (gridView_1.FocusedRowHandle <= -1 || this.gridView_1.GetDataRow(gridView_1.FocusedRowHandle) == null))
            {
                HelpMsgBox.ShowNotificationMessage("Bạn chưa chọn dữ liệu. Vui lòng chọn dữ liệu bạn muốn xóa!");
                return;
            }

            if (_BeforeDelEvent != null) _BeforeDelEvent(this);
            int[] select = gridView_1.GetSelectedRows();
            if (select != null && select.Length > 0)
            {
                DataRow dr = gridView_1.GetDataRow(select[0]);
                if (PLMessageBox.ShowConfirmMessage("Bạn có chắc chắn muốn xóa \""
                    + dr[gridView_1.VisibleColumns[0].FieldName].ToString() + "\" không?") == DialogResult.Yes)
                {
                    dr.Delete();
                    dr.EndEdit();
                    try
                    {
                        if (DABase.getDatabase().UpdateTable(((DataTable)this.GridControl_1.DataSource).DataSet) == -1)
                        {
                            HelpMsgBox.ShowNotificationMessage("Xóa không thành công vì dữ liệu đang sử dụng");
                            HelpGrid.ClearAllError(this.gridView_1);
                            ((DataTable)this.GridControl_1.DataSource).DataSet.RejectChanges();
                            if (_AfterDelFailEvent != null) _AfterDelFailEvent(this);
                        }
                        else
                        {
                            if (_AfterDelSuccEvent != null) _AfterDelSuccEvent(this);
                        }
                    }
                    catch (Exception ex){ PLException.AddException(ex); }
                }
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage("Bạn chưa chọn dữ liệu. Vui lòng chọn dữ liệu bạn muốn xóa!");
            }

            try { if (popUp != null) popUp.ShowPopup(); } catch { }
        }
        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (this.view != null) //hiển thị formView
            {
                try
                {
                    long id = HelpNumber.ParseInt64(this.gridView_1.GetFocusedRowCellValue("ID"));
                    XtraForm myform = this.view(id.ToString());
                    ProtocolForm.ShowModalDialog((XtraForm)this.FindForm(), myform);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
        }
        private void Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int handle = this.gridView_1.FocusedRowHandle;
            try
            {
                try { this.gridView_1.FocusedRowHandle += 1; }
                catch { }
            }
            catch { }
            finally {
                if (handle < 0)
                    this.gridView_1.MoveLastVisible();
                else
                    this.gridView_1.FocusedRowHandle = handle;
            }
        }
        private void btnNoSave_Click(object sender, EventArgs e)
        {
            try
            {
                ((DataRowView)this.gridView_1.GetRow(gridView_1.FocusedRowHandle)).Row.ClearErrors();
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            this.gridView_1.CancelUpdateCurrentRow();
            isEdit = false;
            this.ShowGrid(true);
        }
        #endregion

        #region Xử lý Drag Drop trên cây
        private void SearchTree(TreeListNode node, ArrayList ListID)
        {
            ListID.Add(HelpNumber.ParseInt64(node["ID"]));
            foreach (TreeListNode n in node.Nodes)
            {
                SearchTree(n, ListID);
            }
        }

        //PHUOCNC Cải thiện thêm thuật toán
        private void TreeList_1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && gridView_1.DataSource != null)
            {
                RefreshGridData(e.Node);
            }
        }

        private GridHitInfo hitInfo = null;
        private void TreeList_1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void TreeList_1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                long iditem = 0;
                iditem = HelpNumber.ParseInt64(e.Data.GetData(iditem.GetType()));
                TreeListHitInfo hi = TreeList_1.CalcHitInfo(TreeList_1.PointToClient(new Point(e.X, e.Y)));
                TreeListNode nodehit = hi.Node;
                long idgroup = HelpNumber.ParseInt64(nodehit["ID"]);

                // Cap nhat group cho item
                DataTable dt = (DataTable)this.GridControl_1.DataSource;
                dt.PrimaryKey = new DataColumn[] { dt.Columns[IDField] };
                DataRow row = dt.Rows.Find(iditem);
                row[this.LinkField] = idgroup;
                row.EndEdit();
                DABase.getDatabase().UpdateTable(dt.DataSet);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        public bool _IsDrapDrop = true;
        private void GridControl_1_MouseDown(object sender, MouseEventArgs e)
        {
            if(_IsDrapDrop) hitInfo = gridView_1.CalcHitInfo(new Point(e.X, e.Y));
        }
        private void GridControl_1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsDrapDrop)
            {
                try
                {
                    if (hitInfo == null) return;
                    if (e.Button != MouseButtons.Left) return;
                    Rectangle dragRect = new Rectangle(new Point(
                        hitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                        hitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
                    if (!dragRect.Contains(new Point(e.X, e.Y)))
                    {
                        if (hitInfo.InRowCell)
                        {
                            long idrow = HelpNumber.ParseInt64(gridView_1.GetRowCellValue(hitInfo.RowHandle, "ID"));
                            GridControl_1.DoDragDrop(idrow, DragDropEffects.Move);
                        }
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
        }
        #endregion

        #region Sự kiện trên lưới
        private void RefreshGridData(TreeListNode Node)
        {
            ArrayList ListID = new ArrayList();
            SearchTree(Node, ListID);
            //string tablename = ((DataTable)GridControl_1.DataSource).TableName;
            string tablename = this.elementTable;

            DataSet ds = new DataSet();
            QueryBuilder query = null;
            
            if(IsGridVisibleBit == true)
                query = new QueryBuilder("SELECT * FROM " + tablename + " WHERE VISIBLE_BIT = 'Y' AND 1=1");
            else
                query = new QueryBuilder("SELECT * FROM " + tablename + " WHERE 1=1");

            string where = this.LinkField + " in ( ";
            for (int i = 0; i < ListID.Count - 1; i++)
            {
                where += ListID[i] + ",";
            }
            where += ListID[ListID.Count - 1] + ")";
            query.addCondition(where);
            ds = DABase.getDatabase().LoadDataSet(query, tablename);

            //GridControl_1.DataSource = ds.Tables[0];
            GridControl_1.DataSource = HelpDataSetExt.GetTable0(ds);
        }

        private void gridView_1_DoubleClick(object sender, EventArgs e)
        {
            if (btnSelect.Visible && popUp != null) 
            {
                if (gridView_1.Editable == false)
                {
                    try
                    {
                        int[] select = gridView_1.GetSelectedRows();
                        if (select != null && select.Length > 0)
                        {
                            DataRow dr = gridView_1.GetDataRow(select[0]);

                            this.selectedID = HelpNumber.ParseInt64(dr[IDField]);
                        }
                        SelectedNode = TreeList_1.FocusedNode;
                        SelectedRow = this.gridView_1.FocusedRowHandle;
                        if (popUp != null) popUp.CancelPopup();
                    }
                    catch (Exception ex)
                    {
                        PLException.AddException(ex);
                    }
                }
                else
                    HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập.");
            }
            else
            {
                if (btnUpdate.Visible == true)
                    btnUpdate_Click(null, null);
            }
            forceExitCtrl = true;
        }
        private void gridView_1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow row = this.gridView_1.GetDataRow(gridView_1.FocusedRowHandle);
            if (Rule != null)
            {
                HelpInputData.TrimAllData(row);

                if (!GUIValidation.ValidateRow(this.gridView_1, row, Rule(null)))
                {
                    e.Valid = false;
                    return;
                };
            }
        }
        private void gridView_1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (_BeforeSaveEvent != null) _BeforeSaveEvent(this);
            bool flag = false;
            //Update At here
            DataRow focusRow = ((DataRowView)e.Row).Row;
            #region Đoạn code sẽ bỏ do đã làm ở ValidateRow rồi
            //Trim tất cả khoản trắng của DataRow
            HelpInputData.TrimAllData(focusRow);
            if (!GUIValidation.ValidateRow(this.gridView_1, focusRow, Rule(null)))
            {
                flag = false;
                goto Co;
            }
            #endregion

            if (genName != null && ( focusRow[IDField] == null || focusRow[IDField].ToString() == ""))
            {
                focusRow[IDField] = DABase.getDatabase().GetID(genName);
                if (focusRow["VISIBLE_BIT"] == DBNull.Value) 
                    focusRow["VISIBLE_BIT"] = "N";
                focusRow[LinkField] = this.TreeList_1.FocusedNode.GetValue(this.GroupIDField);
            }
            if (DABase.getDatabase().UpdateTable(((DataTable)this.GridControl_1.DataSource).DataSet) == -1)
            {
                HelpMsgBox.ShowNotificationMessage("Dữ liệu không hợp lệ. Xin vui lòng nhập lại dữ liệu.");
                HelpGrid.ClearAllError(this.gridView_1);
                //((DataTable)this.GridControl_1.DataSource).Rows.Remove((DataRow)e.Row);
                ((DataTable)this.GridControl_1.DataSource).DataSet.Tables[0].RejectChanges();
                flag = false;                
            }
            else
            {
                ((DataTable)this.GridControl_1.DataSource).DataSet.Tables[0].AcceptChanges();
                flag = true;
            }
        Co:
            isEdit = false;
            this.ShowGrid(true);

            if (flag == true) { if (_AfterSaveSuccEvent != null) _AfterSaveSuccEvent(this); }
            else { if (_AfterSaveFailEvent != null) _AfterSaveFailEvent(this); }
        }

        private void gridView_1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (ignore == true)
            {
                ignore = false;
                return;
            }
            if (this.gridView_1.Editable == true && isEdit == true)
            {
                ShowGrid(true);
                isEdit = false;
            }
        }
        
        #endregion

        private void ShowGrid(bool ReadOnly)
        {
            if (ReadOnly)
            {
                this.gridView_1.OptionsBehavior.Editable = false;
                this.btnNoSave.Enabled = false;
                this.btnSave.Enabled = false;

                this.btnAdd.Enabled = true;
                this.btnDel.Enabled = true;
                this.btnUpdate.Enabled = true;

                this.gridView_1.OptionsSelection.EnableAppearanceFocusedRow = true;
                this.gridView_1.OptionsSelection.EnableAppearanceFocusedCell = false;
            }
            else
            {
                this.gridView_1.OptionsBehavior.Editable = true;
                this.btnNoSave.Enabled = true;
                this.btnSave.Enabled = true;

                this.btnAdd.Enabled = false;
                this.btnDel.Enabled = false;
                this.btnUpdate.Enabled = false;

                this.gridView_1.ShowEditor();
                this.gridView_1.OptionsSelection.EnableAppearanceFocusedRow = false;
                this.gridView_1.OptionsSelection.EnableAppearanceFocusedCell = true;
            }
        }

        #region ICategory Members

        public void SetOwner(IFormCategory owner)
        {
            //NOOP
        }

        public void UpdateGUI()
        {
            this.Close.Visible = false;
        }

        public GridView GetGridView()
        {
            return this.gridView_1;
        }
        #endregion        
    
        #region IActionCategory Members

        public ToolStrip GetMenuAction()
        {
            return this.toolStrip_1;
        }

        #endregion

        #region IPluginCategory Members

        public object AddAction(object param)
        {
            btnAdd_Click(param, null);
            return null;
        }

        public object AddChildAction(object param)
        {
            return null;
        }

        public object EditAction(object param)
        {
            btnUpdate_Click(param, null);
            return null;
        }

        public object DeleteAction(object param)
        {
            btnDel_Click(param, null);
            return null;
        }

        public object PrintAction(object param)
        {
            throw new NotImplementedException();
        }

        public object SaveAction(object param)
        {
            btnSave_Click(param, null);
            return null;
        }

        public object NoSaveAction(object param)
        {
            btnNoSave_Click(param, null);
            return null;
        }

        #endregion

        private DelegationLib.DefinePermission permission;
        public void DefinePermission(DelegationLib.DefinePermission permission)
        {
            this.permission = permission;
        }

        #region IPermisionable Members

        public System.Collections.Generic.List<Control> GetPermisionableControls()
        {
            return null;
        }

        public System.Collections.Generic.List<object> GetObjectItems()
        {
            if (permission == null) return null;
            return permission(this);
        }

        #endregion

        #region IFormatable Members

        public System.Collections.Generic.List<Control> GetFormatControls()
        {
            return null;
        }

        #endregion
    }
}