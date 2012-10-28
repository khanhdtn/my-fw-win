using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Views.Grid;
namespace ProtocolVN.Framework.Win
{
    public partial class DMTreeGroup : DevExpress.XtraEditors.XtraUserControl, IActionCategory, IPluginCategory
    {
        public TreeListNode SelectedNode = null;            //Nút đang chọn dùng cho việc chọn       
        private PopupContainerEdit popupContainer = null;   //Popup Container chứa control này

        private FieldNameCheck[] Rules;     //Danh sách các Luật cần kiểm tra
        private String[] VisibleFields;     //Danh sách các field hiển thị 
        private String GenName;             //Tên GEN dùng để lấy ID cho nhóm mới
        public String ParentIDField;       //ID_CHA đệ quy lên
        public String IDField;             //ID_FIELD : Field Int làm khóa chính
        private GroupElementType type;      //Các trường hợp dùng
        private String GroupTableName;      //Tên table
        private int DislayColumn = 1;       //Lấy Cột Thứ 1
        private string[] UniqueFields;      //Những Field không cho trùng nhau ( dùng kết hợp chứ không phải từng Field)
        private string[] Subjects;          //Title để thông báo lỗi                                    
        private bool IsVisibleBit;          //Dùng VISIBLE_BIT
        public ToolStripDropDownButton btnPrint;
        public ToolStripDropDownButton btnExport;
        private enum _FOCUS
        {
            NONE,
            ADD,
            DELETE,
            EDIT
        }
        private _FOCUS focus = _FOCUS.NONE;     //Theo dõi nguồn gốc gây ra sự kiện FocusChange
        private long UpdateID = -2;             //Giá trị cho biết mình vừa thêm mới hay cập nhật tại nút nào

        #region Danh sách sự kiện
        public delegate void BeforeSave(DMTreeGroup sender);
        public event BeforeSave _BeforeSaveEvent;

        public delegate void AfterSaveSucc(DMTreeGroup sender);
        public event AfterSaveSucc _AfterSaveSuccEvent;

        public delegate void AfterSaveFail(DMTreeGroup sender);
        public event AfterSaveFail _AfterSaveFailEvent;

        public delegate void BeforeDel(DMTreeGroup sender);
        public event BeforeDel _BeforeDelEvent;

        public delegate void AfterDelSucc(DMTreeGroup sender);
        public event AfterDelSucc _AfterDelSuccEvent;

        public delegate void AfterDelFail(DMTreeGroup sender);
        public event AfterDelFail _AfterDelFailEvent;
        #endregion

        #region GET & SET trên cây
        public long getSelectedID()
        {
            if (SelectedNode == null) return -1;
            return HelpNumber.ParseInt64(SelectedNode[IDField]);
        }

        public String getDislayText()
        {
            if (SelectedNode != null) return SelectedNode.GetDisplayText(DislayColumn);
            if (popupContainer != null) return popupContainer.Properties.NullText;
            return "";
        }
        public void setSelectedID(long id)
        {
            try
            {
                SelectedNode = TreeList_1.FindNodeByFieldValue(IDField, id);
                if (SelectedNode != null) TreeList_1.FocusedNode = SelectedNode;
            }
            catch { }
        }
        #endregion

        #region Dùng như control chọn lựa
        public DMTreeGroup()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Phân quyền  : Không
        /// VISIBLE_BIT : Có
        /// Unique      : N Field
        /// RootID      : Có
        /// </summary>
        public void Init(GroupElementType type,
            String GroupTableName, int[] RootID, String IDField, String ParentIDField,
            string[] VisibleFields, string[] Captions, string GenName,
            object[] InputColumnType, FieldNameCheck[] Rules,
            string[] UniqueFields, string[] Subjects, bool IsVisibleBit)
        {
            this.type = type;
            UpdateItemBtn(this.type);
            this.GroupTableName = GroupTableName;
            this.IDField = IDField;
            this.ParentIDField = ParentIDField;
            this.GenName = GenName;
            this.VisibleFields = VisibleFields;
            this.TreeList_1._BuildTree(this.GroupTableName, RootID, IDField, ParentIDField, VisibleFields, Captions, InputColumnType);
            this.TreeList_1.OptionsView.ShowColumns = true;
            ((DataTable)this.TreeList_1.DataSource).PrimaryKey =
                        new DataColumn[] { ((DataTable)this.TreeList_1.DataSource).Columns[IDField] };
            this.TreeList_1.ExpandAll();
            this.Rules = Rules;
            this.UniqueFields = UniqueFields;
            this.Subjects = Subjects;
            this.IsVisibleBit = IsVisibleBit;

            //HelpTree.ShowFilter(this.TreeList_1, true, IDField, ParentIDField, FilterConditionEnum.NotContains);
        }
        /// <summary>
        /// Phân quyền  : Không
        /// VISIBLE_BIT : Không hỗ trợ
        /// Unique      : N Field
        /// RootID      : Có
        /// </summary>
        public void Init(GroupElementType type, 
            String GroupTableName, int[] RootID, String IDField, String ParentIDField,
            string[] VisibleFields, string[] Captions, string GenName,
            object[] InputColumnType, FieldNameCheck[] Rules,
            string[] UniqueFields, string[] Subjects)
        {
            Init(type, GroupTableName, RootID, IDField, ParentIDField,
                                VisibleFields, Captions, GenName,
                                InputColumnType, Rules,
                                UniqueFields, Subjects, false);
        }
        /// <summary>
        /// Phân quyền  : Không
        /// VISIBLE_BIT : Không hỗ trợ
        /// Unique      : 1 "NAME" Field
        /// RootID      : Có
        /// </summary>
        public void Init(GroupElementType type, 
            String GroupTableName, int[] RootID, String IDField, String ParentIDField,
            string[] VisibleFields, string[] Captions, string GenName,
            object[] InputColumnType, FieldNameCheck[] Rules)
        {
            Init(type, GroupTableName, RootID, IDField, ParentIDField,
                                VisibleFields, Captions, GenName,
                                InputColumnType, Rules,
                                new string[] { "NAME" }, new string[] { "Tên" });
        }
        /// <summary>
        /// Phân quyền  : Không
        /// VISIBLE_BIT : Không hỗ trợ
        /// Unique      : N Field
        /// RootID      : Không
        /// </summary>
        public void Init(GroupElementType type, 
            String GroupTableName, String IDField, String ParentIDField,
            string[] VisibleFields, string[] Captions, string GenName,
            object[] InputColumnType, FieldNameCheck[] Rules)
        {
            Init(type, GroupTableName, null, IDField, ParentIDField, VisibleFields, Captions, GenName, InputColumnType, Rules);
        }
        #endregion

        #region Dùng như control trên form
        //Dùng qua Hàm dựng rỗng + Init(...)
        #endregion

        #region Hàm khởi tạo

        private void DMTreeGroup_Load(object sender, EventArgs e)
        {
            FWImageDic.GET_GROUP_ELEM_IMAGE16(this.imageList1);
            this.addSameLevel.Image = FWImageDic.ADD_IMAGE20;
            this.btnAdd.Image = FWImageDic.ADD_CHILD_IMAGE20;
            this.btnDel.Image = FWImageDic.DELETE_IMAGE20;
            this.btnUpdate.Image = FWImageDic.EDIT_IMAGE20;
            this.btnSelect.Image = FWImageDic.CHOICE_IMAGE20;
            this.btnNoSelect.Image = FWImageDic.NO_CHOICE_IMAGE20;
            this.btnClose.Image = FWImageDic.CLOSE_IMAGE20;
            this.btnLuu.Image = FWImageDic.SAVE_IMAGE20;
            this.btnKhongLuu.Image = FWImageDic.NO_SAVE_IMAGE20;
            this.TreeList_1.OptionsView.ShowHorzLines = true;
            this.TreeList_1.OptionsView.ShowVertLines = false;
        }

        //Dùng khi control nhúng vào PopUp
        public void setPopupControl(PopupContainerEdit popupConEdit)
        {
            this.popupContainer = popupConEdit;
        }
        #endregion

        #region Xử lý Validation
        //Xử lý khi ValidateNote trả về fail
        private void InvalidNodeException_1(object sender, InvalidNodeExceptionEventArgs e)
        {
            try
            {
                if (e.ExceptionMode == DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError)
                {
                    if (PLMessageBox.ShowConfirmMessage("Thông tin bạn vào không hợp lệ. Bạn có muốn vào lại thông tin ?") == DialogResult.Yes)
                    {
                        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
                        this.ShowReadOnlyTreeList(false);
                    }
                    else
                    {
                        try
                        {
                            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
                            DataRow Row = ((DataTable)this.TreeList_1.DataSource).Rows.Find(e.Node.GetValue(IDField));
                            Row.ClearErrors();

                            this.TreeList_1.BeginUpdate();
                            ((DataTable)this.TreeList_1.DataSource).RejectChanges();
                            this.TreeList_1.EndUpdate();
                        }
                        catch (Exception ex){
                            PLException.AddException(ex);
                        }
                        finally {
                            this.ShowReadOnlyTreeList(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        //Xử lý khi mất focus trên dòng đang nhập
        private void TreeList_1_ValidateNode_1(object sender, ValidateNodeEventArgs e)
        {
            if (_BeforeSaveEvent != null) _BeforeSaveEvent(this);

            if (IsFilterRow()) return;
            bool isNewNode = false;
            DataSet ds = null;
            DataRow UpdateRow = null;

            //Lấy dữ liệu nhập từ phía người dùng
            if (e.Node.GetValue(IDField).ToString() == "-2")
            {
                isNewNode = true;
                if (IsVisibleBit)
                {
                    if (e.Node.GetValue("VISIBLE_BIT") == DBNull.Value || e.Node.GetValue("VISIBLE_BIT").ToString() == "")
                    {
                        e.Node.SetValue("VISIBLE_BIT", 'Y');
                    }
                }
                else
                {
                    e.Node.SetValue("VISIBLE_BIT", 'Y');
                }
                ds = DatabaseFB.LoadDataSet(GroupTableName, IDField, -2);
                UpdateRow = ds.Tables[0].NewRow();
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    UpdateRow[ds.Tables[0].Columns[i].ColumnName] = e.Node.GetValue(ds.Tables[0].Columns[i].ColumnName);
            }
            else
            {
                isNewNode = false;
                UpdateRow = ((DataTable)this.TreeList_1.DataSource).Rows.Find(e.Node.GetValue(IDField));
            }
            if (UpdateRow == null) return;

            //Trim dữ liệu
            DataRow row = ((DataRowView)this.TreeList_1.GetDataRecordByNode(e.Node)).Row;
            HelpInputData.TrimAllData(row);

            //Kiểm tra dữ liệu dựa trên luật
            e.Valid = TreeValidation.ValidateNode(this.TreeList_1, e.Node, Rules);

            //Kiểm tra tính hợp lệ so với dòng khác
            if(e.Valid == true)
            {
                if (this.UniqueFields != null)
                {
                    e.Valid = TreeValidation.CheckDuplicateFieldNode(this.TreeList_1,
                        ((DataTable)this.TreeList_1.DataSource).DataSet,
                        e,
                        this.UniqueFields,
                        this.Subjects);
                }
                if (e.Valid == false) return;

                //Cập nhật dự liệu trên dòng đang nhập vào DataSet
                if (isNewNode)
                {
                    this.UpdateID = DABase.getDatabase().GetID(this.GenName);

                    e.Node.SetValue(IDField, this.UpdateID);
                    UpdateRow[IDField] = this.UpdateID;
                    ds.Tables[0].Rows.Add(UpdateRow);
                }
                else
                {
                    this.UpdateID = HelpNumber.ParseInt64(e.Node.GetValue(IDField));
                    try
                    {
                        ds = DatabaseFB.LoadDataSet(GroupTableName, IDField, this.UpdateID);
                        ds.Tables[0].Rows[0].SetModified();
                        ds.Tables[0].Rows[0].ItemArray = UpdateRow.ItemArray;
                    }
                    catch { }
                }
                //Cập nhật xuống DB
                if (DABase.getDatabase().UpdateTable(ds) < 0)
                {
                    if (isNewNode)
                    {
                        e.Node.SetValue(IDField, "-2");
                        this.UpdateID = -2;
                    }
                    //this.ShowReadOnlyTreeList(false);
                    e.Valid = false;
                    if (_AfterSaveFailEvent != null) _AfterSaveFailEvent(this);
                }
                else
                {
                    if (isNewNode)
                    {
                        this.TreeList_1.BeginUpdate();
                        ((DataTable)this.TreeList_1.DataSource).AcceptChanges();
                        this.TreeList_1.EndUpdate();
                    }
                    e.Valid = true;
                    this.ShowReadOnlyTreeList(true);
                    if (_AfterSaveSuccEvent != null) _AfterSaveSuccEvent(this);
                }
            }
        }
        #endregion

        #region Xử lý trên cây
        void TreeList_1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (IsFilterRow()) return;
            if(PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn thay đổi không?") == DialogResult.No){
                this.CancelCurrentAction();
                return;
            }
            //PHUOCNC : Confirm muốn lưu không.            
            if (e.Node.ParentNode == null || !e.Node[GlobalConst.ID_ROOT].Equals(e.Node.ParentNode[GlobalConst.ID_ROOT]))
            {
                HelpMsgBox.ShowNotificationMessage("Không thể chuyển qua nhóm khác.");
                this.CancelCurrentAction();
                return;
            }
            try
            {
                string query = "UPDATE " + this.GroupTableName + " SET " + this.ParentIDField + " = "
                        + e.Node.ParentNode.GetValue(this.IDField).ToString()
                        + " WHERE " + this.IDField + " = " + e.Node.GetValue(this.IDField).ToString();
                DbCommand command = DABase.getDatabase().GetSQLStringCommand(query);
                DABase.getDatabase().ExecuteNonQuery(command);

                //Thay đổi lớn do đó cập nhật lại cây
                TreeList_1.EndUpdate();
                TreeList_1.PLRefresh();
                TreeList_1.ExpandAll();
                ((DataTable)this.TreeList_1.DataSource).PrimaryKey =
                        new DataColumn[] { ((DataTable)this.TreeList_1.DataSource).Columns[IDField] };
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
                HelpMsgBox.ShowNotificationMessage("Lưu không thành công.");                
                this.CancelCurrentAction();
                return;
            }
        }
        private void TreeList_1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (focus == _FOCUS.DELETE || focus == _FOCUS.ADD)
            {
                focus = _FOCUS.NONE;
                return;
            }
            else if (focus == _FOCUS.EDIT)
            {
                this.DeleteEmptyNode();
                focus = _FOCUS.NONE;
                return;
            }

            ShowReadOnlyTreeList(true);
            if (e.Node != null && e.Node.ParentNode == null)
            {
                addSameLevel.Enabled = false;
                btnUpdate.Enabled = false;
                btnDel.Enabled = false;
            }

        }
        private void TreeList_1_DoubleClick(object sender, EventArgs e)
        {
            if (IsFilterRow()) return;
            if (btnSelect.Visible && popupContainer != null)
            {
                if (this.TreeList_1.OptionsBehavior.Editable == false)
                {
                    if (TreeList_1.Selection != null)
                    {
                        SelectedNode = TreeList_1.Selection[0];
                        try
                        {
                            if (SelectedNode.Level == 0) SelectedNode = null;
                        }
                        catch { }
                    }
                    if (popupContainer != null) popupContainer.ClosePopup();
                }
                else
                    HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập");
            }
            else
            {
                if (btnUpdate.Visible == true)
                    btnUpdate_Click(null, null);
            }
        }
        #endregion

        #region Xử lý nút
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kích hoạt thao tác cập nhật
            TreeListNode current = null;
            try
            {
                current = TreeList_1.FocusedNode;
                TreeList_1.FocusedNode = TreeList_1.MovePrevVisible();
            }
            catch {
            }
            finally
            {
                if (current != null) TreeList_1.FocusedNode = current;
            }
            
            //Focus lại nút đang cập nhật hoặc vừa mới thêm mới thành công
            if (this.UpdateID != -1)
            {
                TreeList_1.FocusedNode = TreeList_1.FindNodeByFieldValue(IDField, this.UpdateID);
                this.UpdateID = -1;
            }
        }
        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            TreeList_1.CancelCurrentEdit();
            ShowReadOnlyTreeList(true);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addClickEvent(e, true);
        }
        private void addSameLevel_Click(object sender, EventArgs e)
        {
            addClickEvent(e, false);
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_BeforeDelEvent != null) _BeforeDelEvent(this);
            if (IsFilterRow()) return;

            if (TreeList_1.FocusedNode != null)
            {
                if (TreeList_1.FocusedNode.ParentNode == null)
                {
                    HelpMsgBox.ShowNotificationMessage("Không thể xóa dữ liệu này.");
                    return;
                }
                if (DeleteEmptyNode()) return;

                if (PLMessageBox.ShowConfirmMessage("Bạn có chắc chắn muốn xóa dữ liệu này không ?") == DialogResult.Yes)
                {
                    long id = HelpNumber.ParseInt64(TreeList_1.FocusedNode[this.IDField]);
                    List<long> listID = this.nodeHasParentID(id);
                    listID.Add(id);
                    if (!this.removeListNode(listID))
                    {
                        HelpMsgBox.ShowNotificationMessage("Xóa không thành công vì dữ liệu đang được sử dụng.");
                        if (_AfterDelFailEvent != null) _AfterDelFailEvent(this);
                    }
                    else
                    {
                        focus = _FOCUS.DELETE;
                        this.TreeList_1.BeginUpdate();
                        TreeList_1.DeleteNode(TreeList_1.FocusedNode);
                        ((DataTable)this.TreeList_1.DataSource).AcceptChanges();
                        this.TreeList_1.EndUpdate();
                        if (_AfterDelSuccEvent != null) _AfterDelSuccEvent(this);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsFilterRow()) return;

            TreeListNode node = TreeList_1.FocusedNode;
            if (node != null)
            {
                if (node.ParentNode != null)
                    this.ShowReadOnlyTreeList(false);
            }
        }
        public void btnSelect_Click(object sender, EventArgs e)
        {
            if (IsFilterRow()) return;

            if (this.TreeList_1.OptionsBehavior.Editable == false)
            {
                if (TreeList_1.Selection != null)
                {
                    SelectedNode = TreeList_1.Selection[0];
                    try
                    {
                        if (SelectedNode.Level == 0) {
                            SelectedNode = null;
                        }
                    }
                    catch { }
                }
                if (popupContainer != null) popupContainer.ClosePopup();
            }
            else{
                HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập.");
                if (popupContainer != null) popupContainer.ClosePopup();
            }
        }
        private bool IsFilterRow()
        {
            try
            {
                TreeListNode node = TreeList_1.FocusedNode;
                //if (node.Id == 0) return true;
                if (HelpNumber.ParseInt64(node[IDField]) == long.MinValue) return true;
            }
            catch { }
            return false;
        }
        private void btnNoSelect_Click(object sender, EventArgs e)
        {
            
            if(this.TreeList_1.OptionsBehavior.Editable == false){
                SelectedNode = null;
                if (popupContainer != null)
                {
                    popupContainer.ClosePopup();
                }
            }
            else
            {
                if (IsFilterRow() == false)
                    HelpMsgBox.ShowNotificationMessage("Vui lòng lưu dữ liệu đang nhập.");
                SelectedNode = null;
                if (popupContainer != null)
                {
                    popupContainer.ClosePopup();
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
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
        #endregion

        #region Hàm hỗ trợ
        private bool DeleteEmptyNode()
        {
            try
            {
                TreeListNode NewNode = TreeList_1.FindNodeByFieldValue(IDField, "-2");
                if (NewNode != null)
                {
                    focus = _FOCUS.DELETE;
                    TreeList_1.DeleteNode(NewNode);
                    TreeList_1.InvalidateNode(NewNode);
                    return true;
                }
                if (TempNewNode != null && !TreeList_1.FocusedNode.Equals(TempNewNode))
                {
                    focus = _FOCUS.DELETE;
                    TreeList_1.DeleteNode(TempNewNode);
                    TreeList_1.InvalidateNode(TempNewNode);
                    return true;
                }
                if (TempNewNode != null && TempNewNode.GetValue(IDField).ToString() == "-2")
                {
                    focus = _FOCUS.DELETE;
                    TreeList_1.DeleteNode(TempNewNode);
                    TreeList_1.InvalidateNode(TempNewNode);
                    return true;
                }
                return false;
            }
            catch (Exception ex){
                PLException.AddException(ex);
                return true;
            }
        }
        private TreeListNode TempNewNode = null;
        private void ShowReadOnlyTreeList(bool IsReadOnly)
        {
            if (IsReadOnly)
            {
                this.DeleteEmptyNode();
                TreeList_1.OptionsBehavior.Editable = false;

                TreeList_1.HideEditor();
                addSameLevel.Enabled = true;
                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;
                btnDel.Enabled = true;
                btnLuu.Enabled = false;
                btnKhongLuu.Enabled = false;
                TempNewNode = null;
            }
            else
            {
                this.TreeList_1.OptionsBehavior.Editable = true;

                TreeList_1.ShowEditor();
                addSameLevel.Enabled = false;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDel.Enabled = false;
                btnLuu.Enabled = true;
                btnKhongLuu.Enabled = true;
            }
        }
        //Cập nhật các nút tương ứng với tình huống sử dụng
        private void UpdateItemBtn(GroupElementType type)
        {
            btnPrint = HelpTreeList.addXuatRaFileItem(this.toolStrip_1, this.TreeList_1);                //In
            btnExport = HelpTreeList.addInLuoiItem(this.toolStrip_1, this.TreeList_1);
            TreeList_1._SetPermissionElement(btnPrint, btnExport);
            if (type == GroupElementType.ONLY_INPUT)
            {
                btnSelect.Visible = false;
                btnNoSelect.Visible = false;
                ChonSep.Visible = false;

                //this.TreeList_1.DoubleClick -= new System.EventHandler(this.TreeList_1_DoubleClick);
                this.TreeList_1.InvalidNodeException += InvalidNodeException_1;
                TreeList_1.AllowDrop = true;

                //Xuất ra file
              
            }
            else if (type == GroupElementType.ONLY_CHOICE)
            {
                btnAdd.Visible = false;
                btnDel.Visible = false;
                btnUpdate.Visible = false;
                addSameLevel.Visible = false;

                LuuSep.Visible = false;
                btnLuu.Visible = false;
                btnKhongLuu.Visible = false;

                DongSep.Visible = false;
                btnClose.Visible = false;

                ChonSep.Visible = false;
                btnSelect.Visible = true;
                btnNoSelect.Visible = true;

                btnPrint.Visible = false;
                btnExport.Visible = false;
                this.TreeList_1.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.TreeList_1_FocusedNodeChanged);
                this.TreeList_1.ValidateNode -= new DevExpress.XtraTreeList.ValidateNodeEventHandler(this.TreeList_1_ValidateNode_1);
                this.TreeList_1.InvalidNodeException -= InvalidNodeException_1;

                TreeList_1.AllowDrop = false;
            }
            else if (type == GroupElementType.CHOICE_N_ADD)
            {
                btnDel.Visible = false;
                btnUpdate.Visible = false;

                DongSep.Visible = false;
                btnClose.Visible = false;

                btnPrint.Visible = false;
                btnExport.Visible = false;

                this.TreeList_1.InvalidNodeException += InvalidNodeException_1;
                TreeList_1.AllowDrop = false;
            }
        }
        private void CancelCurrentAction()
        {
            ((DataTable)TreeList_1.DataSource).DataSet.RejectChanges();
            TreeList_1.RefreshDataSource();
            TreeList_1.Invalidate();
        }
        private void addClickEvent(EventArgs e, bool AddChild)
        {
            if (IsFilterRow()) return;
            TreeListNode FocusedNode = TreeList_1.FocusedNode;
            long idParent = -1;
            try
            {
                if (FocusedNode != null)
                {
                    //Xóa nút mới chưa thêm xong
                    DeleteEmptyNode();

                    DataRow row = ((DataTable)TreeList_1.DataSource).NewRow();
                    
                    row[IDField] = "-2";
                    try { row["VISIBLE_BIT"] = "Y"; }
                    catch { }
                    if (AddChild)//Thêm con
                    {
                        idParent = HelpNumber.ParseInt64(FocusedNode[this.IDField]);
                        row[ParentIDField] = idParent;
                        row[GlobalConst.ID_ROOT] = FocusedNode[GlobalConst.ID_ROOT];

                        focus = _FOCUS.ADD;
                        TreeList_1.FocusedNode = TreeList_1.AppendNode(row.ItemArray, FocusedNode);
                        
                    }
                    else//Thêm cùng cấp
                    {
                        if (TreeList_1.FocusedNode.ParentNode != null)
                        {
                            idParent = HelpNumber.ParseInt64(FocusedNode.ParentNode[this.IDField]);
                            row[ParentIDField] = idParent;
                            row[GlobalConst.ID_ROOT] = FocusedNode.ParentNode[GlobalConst.ID_ROOT];

                            focus = _FOCUS.ADD;
                            TreeList_1.FocusedNode = TreeList_1.AppendNode(row.ItemArray, FocusedNode.ParentNode);
                        }
                        else
                        {
                            //Thông báo không cho phép thêm   
                            return;
                        }
                    }
                    TempNewNode = TreeList_1.FocusedNode;
                    this.ShowReadOnlyTreeList(false);
                    TreeList_1.Select();
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        #endregion

        #region Làm việc với DB
        //Hàm này sẽ bỏ khi dùng CASCADE
        //private List<int> nodeHasParentIDIn(List<int> list)
        //{
        //    List<int> result = new List<int>();
        //    foreach (int idItem in list)
        //    {
        //        result.AddRange(this.nodeHasParentID(idItem));
        //    }
        //    return result;
        //}
        private List<long> nodeHasParentID(long parentID)
        {
            try
            {
                List<long> result = new List<long>();
                ////Không CASCADE
                //string sql = "SELECT " + IDField + " FROM " + this.GroupTableName +
                //                " WHERE " + this.ParentIDField + " = " + parentID;
                //DbCommand command = DABase.getDatabase().GetSQLStringCommand(sql);
                //IDataReader reader = DABase.getDatabase().ExecuteReader(command);
                //while (reader.Read())
                //{
                //    int childID = HelpNumber.ParseInt32(reader[IDField]);
                //    result.Add(childID);
                //}
                //// clean up 
                //reader.Close();
                //result.AddRange(this.nodeHasParentIDIn(result));

                //CASCADE Update
                result.Add(parentID);
                return result;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return null;
        }
        //CASCADE Update Thì sửa lại đơn giản hơn 
        private bool removeListNode(List<long> listID)
        {
            try
            {
                string whereClause = "";
                if (listID.Count != 0)
                {
                    whereClause = " WHERE ";
                    for (int index = 0; index < listID.Count; index++)
                    {
                        whereClause += this.IDField + " = " + listID[index];
                        if (index == listID.Count - 1)
                            whereClause += " ";
                        else
                            whereClause += " OR ";
                    }
                }
                string sql2 = "DELETE FROM " + this.GroupTableName + whereClause;

                //DbCommand command = DABase.getDatabase().GetSQLStringCommand(sql2);
                //if (DABase.getDatabase().ExecuteNonQuery(command) > 0)
                //    return true;

                DatabaseFB db = DABase.getDatabase();
                DbCommand command = db.GetSQLStringCommand(sql2);
                if (db.ExecuteNonQuery(command) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return false;
        }
        #endregion        

        private void TreeList_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && popupContainer != null)
            {
                TreeList_1_DoubleClick(null, null);
            }
        }

        #region IPluginCategory Members

        public object AddAction(object param)
        {
            addClickEvent(null, false);
            return null;
        }

        public object EditAction(object param)
        {
            btnUpdate_Click(null, null);
            return null;
        }

        public object DeleteAction(object param)
        {
            btnDel_Click(null, null);
            return null;
        }

        public object PrintAction(object param)
        {
            throw new NotImplementedException();
        }

        public object SaveAction(object param)
        {
            btnLuu_Click(null, null);
            return null;
        }

        public object NoSaveAction(object param)
        {
            btnKhongLuu_Click(null, null);
            return null;
        }

        public object AddChildAction(object param)
        {
            addClickEvent(null, true);
            return null;
        }

        #endregion

        #region ICategory Members

        public void SetOwner(IFormCategory owner)
        {
            //NOOP
        }

        public void UpdateGUI()
        {
            this.btnClose.Visible = false;
        }

        public GridView GetGridView()
        {
            return null;
        }
        #endregion

        #region IActionCategory Members

        public ToolStrip GetMenuAction()
        {
            return this.toolStrip_1;
        }

        #endregion

        private DelegationLib.DefinePermission permission;
        public void DefinePermission(DelegationLib.DefinePermission permission)
        {
            this.permission = permission;
        }

        #region IPermisionable Members

        public List<Control> GetPermisionableControls()
        {
            return null;
        }

        public List<object> GetObjectItems()
        {
            if (permission == null) return null;
            return permission(this);
        }

        #endregion

        #region IFormatable Members

        public List<Control> GetFormatControls()
        {
            return null;
        }

        #endregion
    }
}
