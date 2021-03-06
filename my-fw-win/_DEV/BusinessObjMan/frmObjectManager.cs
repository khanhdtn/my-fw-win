using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraVerticalGrid.Rows;

namespace ProtocolVN.Framework.Win
{
    public partial class frmObjectManager : DevExpress.XtraEditors.XtraForm
    {
        #region Các biến quan trọng sử dụng trong chương trình
        List<ObjectInfo> ObjectList;
        List<Field> FieldList;
        int filter_type;
        #endregion

        public frmObjectManager()
        {
            InitializeComponent();                     
            ObjectList = ObjectManagerHelp._initAll();

            Init(); 
            InitCtrlData();
            InitEvent();
        }

        #region Các hàm khởi tạo
        public void Init()
        {   
            popupControlContainerFilter.Visible = true;
            gridViewLeft.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridViewMaster.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;            
        }

        private void InitCtrlData()
        {
            HelpDate.OneWeekAgo(NgayTao_tu, NgayTao_den);
            HelpDate.OneWeekAgo(NgayCN_tu, NgayCN_den);

            ObjectManagerHelp.ChonDoiTuong(DoiTuong);
        }

        private void InitEvent()
        {
            gridViewLeft.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewLeft_FocusedRowChanged);
            cbFieldObj.SelectedIndexChanged += new EventHandler(cbFieldObj_SelectedIndexChanged);
            btnSearchObj.Click += new EventHandler(btnSearchObj_Click);
            btnSearchPhieu.Click += new EventHandler(btnSearchPhieu_Click);
        }

        private void InitFilterPart()
        {
            filter_panel.Controls.Clear();
            Field field = getField();
            if (field.DATA_TYPE.Equals("String"))
            {
                CondText Text_ctr = new CondText();
                Text_ctr.Dock = DockStyle.Fill;
                filter_panel.Controls.Add(Text_ctr);
                filter_type = 1;
            }
            else if (field.DATA_TYPE.Equals("Int64") || field.DATA_TYPE.Equals("Decimal"))
            {
                if (field.TAG != "")
                {
                    CondComboBox Combo_ctr = new CondComboBox();
                    string[] tags = field.TAG.Split(new char[] { ';' });
                    Combo_ctr.ComboID._init(tags[0], tags[2], tags[1]);
                    Combo_ctr.Dock = DockStyle.Fill;
                    filter_panel.Controls.Add(Combo_ctr);
                    filter_type = 4;
                }
                else
                {
                    CondNumber So_ctr = new CondNumber();
                    So_ctr.Dock = DockStyle.Fill;
                    filter_panel.Controls.Add(So_ctr);
                    filter_type = 2;
                }
            }
            else if (field.DATA_TYPE.Equals("DateTime"))
            {
                CondDate Ngay_ctr = new CondDate();
                Ngay_ctr.Dock = DockStyle.Fill;
                HelpDate.OneWeekAgo(Ngay_ctr.DateTu, Ngay_ctr.DateDen);
                filter_panel.Controls.Add(Ngay_ctr);
                filter_type = 3;
            }
        }

        private void InitGrid_Obj()
        {
            gridViewLeft.Columns.Clear();            
            if (DoiTuong._getSelectedID() != -1)
            {
                ObjectInfo obj = getObject(DoiTuong._getSelectedID());
                DoiTuong_lb.Text = obj.Title;
                obj._init(false);
            }
            else
                DoiTuong_lb.Text = "";
        }

        private void InitCtrlState()
        {
            if (vGridLeft.Rows.Count > 0)
                ActiveButtonGroup1();
            else
                ActiveButtonGroup2();
        }
        #endregion

        #region Các hàm trợ giúp
        private Field getField()
        {
            if (cbFieldObj._lookUpEdit.EditValue != null)
                foreach (Field field in FieldList)
                    if (field.NAME.Equals(cbFieldObj._lookUpEdit.EditValue.ToString()))
                        return field;
            return null;
        }

        private ObjectInfo getObject(long IdKey)
        {
            foreach (ObjectInfo obj in ObjectList)
                if (obj.Id.Equals(IdKey))
                {
                    obj.View = gridViewLeft;
                    obj.Vgrid = vGridLeft;
                    return obj;
                }
            return null;
        }

        private PhieuInfo getPhieu(long IdKey)
        {
            if (DoiTuong._getSelectedID() != -1)
            {
                ObjectInfo obj = getObject(DoiTuong._getSelectedID());
                foreach (PhieuInfo phieu in obj.DsPhieu)
                    if (phieu.Id.Equals(IdKey))
                    {                        
                        phieu.View = gridViewMaster;                        
                        return phieu;
                    }
            }
            return null;
        }                    

        //Load thông tin chi tiết của mỗi đối tượng khi người dùng chọn thay đổi giá trị đối tượng
        private void ShowInfoObj(long IdKey)
        {
            ObjectInfo obj = getObject(DoiTuong._getSelectedID());
            FillVGridData(obj, IdKey);
        }

        private void FillVGridData(ObjectInfo obj, long IdKey)
        {
            QueryBuilder filter = new QueryBuilder("select * from " + obj.TableName + " where 1=1");
            filter.addID(obj.Key_field, IdKey);
            DataSet ds = DABase.getDatabase().LoadDataSet(filter);
            vGridLeft.DataSource = ds.Tables[0];
        }        

        //Load các giá trị liên quan đến đối tượng theo tiêu chí tìm kiếm
        //Ví dụ: nếu ta chọn đối tượng khách hàng thì sẽ tìm kiếm tất cả các khách hàng theo tiêu chí Id, Name,....
        private void GetListValueObj()
        {
            ObjectInfo obj = getObject(DoiTuong._getSelectedID());
            Field field = getField();
            if (obj != null && field != null)
            {
                QueryBuilder filter = new QueryBuilder("select * from " + obj.TableName + " where 1=1");
                BuildFilterString_Object(filter, field);
                DataSet ds = ObjectManagerHelp.find_DSObject(filter);
                if (ds.Tables.Count > 0)
                {
                    gridControlLeft.DataSource = ds.Tables[0];
                    gridViewLeft.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
                        new DevExpress.XtraGrid.Columns.GridColumnSortInfo(
                            gridViewLeft.Columns[field.NAME], DevExpress.Data.ColumnSortOrder.Ascending)});
                }
            }

            if (gridViewLeft.RowCount > 0)
            {
                DataRow dr = gridViewLeft.GetDataRow(gridViewLeft.FocusedRowHandle);
                ShowInfoObj(HelpNumber.ParseInt64(dr[0]));
                InitCtrlState();
            }
        }

        private void BuildFilterString_Object(QueryBuilder filter, Field field)
        {
            Control ctr = filter_panel.Controls[0];
            if (filter_type == 1)
            {
                CondText Text_ctr = (CondText)ctr;
                filter.addLike(field.NAME, Text_ctr.GiaTri.Text);
            }
            else if (filter_type == 2)
            {
                CondNumber So_ctr = (CondNumber)ctr;                
                addNumFromTo(filter, field.NAME, So_ctr.SpinTu, So_ctr.SpinDen);
            }
            else if (filter_type == 3)
            {
                CondDate Ngay_ctr = (CondDate)ctr;
                filter.addDateFromTo(field.NAME, Ngay_ctr.DateTu.DateTime, Ngay_ctr.DateDen.DateTime);
            }
            else if (filter_type == 4)
            {
                CondComboBox Combo_ctr = (CondComboBox)ctr;
                filter.addID(field.NAME, Combo_ctr.ComboID._getSelectedID());
            }
        }

        //Load tất cả field của bảng liên quan đến đối tượng gán vào cbFieldObj để cho việc tìm kiếm.
        //Ví dụ: nếu ta chọn đối tượng khách hàng có các field thì ta gán vào comboBox cbFieldObj để là tiêu chí tìm kiếm
        private void GetFieldObj()
        {
            if (DoiTuong._getSelectedID() != -1)
            {
                ObjectInfo obj = getObject(DoiTuong._getSelectedID());

                FieldList = ObjectManagerHelp.BuildFieldsFromGrid(gridViewLeft);
                DataTable fields_dt = HelpCollection.ConvertTo<Field>(FieldList);

                cbFieldObj._lookUpEdit.Properties.ShowHeader = false; 
                cbFieldObj._lookUpEdit.Properties.DataSource = fields_dt;
                cbFieldObj._lookUpEdit.Properties.Columns.Clear();
                DevExpress.XtraEditors.Controls.LookUpColumnInfo col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CAPTION");
                cbFieldObj._lookUpEdit.Properties.Columns.Add(col);
                cbFieldObj._lookUpEdit.Properties.DisplayMember = "CAPTION";
                cbFieldObj._lookUpEdit.Properties.ValueMember = "NAME";
            }
        }

        //Load tất cả các phiều của đối tượng
        //Ví dụ khi chọn đối tượng thì load tất cả các phiếu của khách hàng đó.
        private void GetPhieuDT()
        {
            if (DoiTuong._getSelectedID() != -1)
            {
                ObjectInfo obj = getObject(DoiTuong._getSelectedID());
                ObjectManagerHelp.ChonPhieu(Phieu, obj.Id);
            }
        }

        //Lấy tất cả các hoạt động liên quan đến phiếu 
        private void GetValueOfPhieu()
        {            
            if (DoiTuong.Text != "" && Phieu.Text != "" && gridViewLeft.RowCount > 0)
            {
                PhieuInfo phieu = getPhieu(Phieu._getSelectedID());
                DataRow row = gridViewLeft.GetDataRow(gridViewLeft.FocusedRowHandle);
                QueryBuilder query = ObjectManagerHelp.find_DSPhieu_Object(phieu, phieu.Obj_field, HelpNumber.ParseInt64(row[0]).ToString());
                DataSet ds = DABase.getDatabase().LoadReadOnlyDataSet(BuildFilterString_Phieu(phieu, query));
                if (ds.Tables.Count > 0)
                    gridControlMaster.DataSource = ds.Tables[0];
            }
        }

        private QueryBuilder BuildFilterString_Phieu(PhieuInfo phieu, QueryBuilder query)
        {
            query.addSoPhieu(phieu.Ma_field_name, MaPhieu.Text);
            query.addDateFromTo(phieu.Ngay_tao_fn, NgayTao_tu.DateTime, NgayTao_den.DateTime);
            query.addDateFromTo(phieu.Ngay_cn_fn, NgayCN_tu.DateTime, NgayCN_den.DateTime);
            return query;
        }                

        private void refresh_gridViewLeft()
        {
            GetListValueObj();
            gridViewLeft.MoveLastVisible();
        }        

        private void ActiveButtonGroup1()
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = gridViewLeft.RowCount > 0 ? true : false;
            btnDelete.Enabled = gridViewLeft.RowCount > 0 ? true : false;
            btnSave.Enabled = false;
            btnNonSave.Enabled = false;

            vGridLeft.OptionsBehavior.Editable = false;

            gridControlLeft.Enabled = true;
            btnSearchObj.Enabled = true;
        }

        private void ActiveButtonGroup2()
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = vGridLeft.Rows.Count > 0 ? true : false;
            btnNonSave.Enabled = vGridLeft.Rows.Count > 0 ? true : false;

            gridControlLeft.Enabled = false;
            btnSearchObj.Enabled = false;
        }
        #endregion

        #region Các sự kiện Button
        //Xử lý sự kiện nút tìm kiếm các giá trị liên quan đến phiếu đã chọn
        void btnSearchPhieu_Click(object sender, EventArgs e)
        {
            GetValueOfPhieu();
        }

        //Xử lý sự kiện nút tìm kiếm giá trị liên quan đến đối tượng ta chọn
        void btnSearchObj_Click(object sender, EventArgs e)
        {
            //Load các giá trị liên quan đến đối tượng theo tiêu chí tìm kiếm
            //Ví dụ: nếu ta chọn đối tượng khách hàng thì sẽ tìm kiếm tất cả các khách hàng theo tiêu chí Id, Name,....
            GetListValueObj();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ActiveButtonGroup2();            
            ObjectInfo obj = getObject(DoiTuong._getSelectedID());
            obj._init(true);
            vGridLeft.OptionsBehavior.Editable = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (PLMessageBox.ShowConfirmMessage("Bạn có chắc muốn xóa?") == DialogResult.Yes)
            {
                DataRow row = gridViewLeft.GetDataRow(gridViewLeft.FocusedRowHandle);
                ObjectInfo obj = getObject(DoiTuong._getSelectedID());
                if (obj.delete(HelpNumber.ParseInt64(row[0])))
                    refresh_gridViewLeft();
                else
                    HelpMsgBox.ShowNotificationMessage("Xóa không thành công");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ActiveButtonGroup2();
            vGridLeft.OptionsBehavior.Editable = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjectInfo obj = getObject(DoiTuong._getSelectedID());
            if (obj.saveOrUpdate())
            {
                ActiveButtonGroup1();
                refresh_gridViewLeft();
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage("Thông tin bạn vào không hợp lệ hay có lỗi hệ thống");
                vGridLeft.Refresh();
            }
        }
        
        private void btnNonSave_Click(object sender, EventArgs e)
        {
            ActiveButtonGroup1();
            refresh_gridViewLeft();
            vGridLeft.ClearRowErrors();
        } 
        #endregion

        #region Các sự kiện khác
        void vGridLeft_InvalidRecordException(object sender, DevExpress.XtraVerticalGrid.Events.InvalidRecordExceptionEventArgs e)
        {
            HelpMsgBox.ShowNotificationMessage("error");
        }                         

        void gridViewLeft_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {            
            DataRow row = gridViewLeft.GetDataRow(e.FocusedRowHandle);
            if(row != null)
                ShowInfoObj(HelpNumber.ParseInt64(row[0]));            
        }

        private void vGridLeft_Leave(object sender, EventArgs e)
        {
            vGridLeft.Refresh();
        }

        void cbFieldObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitFilterPart();
        } 
      
        //Load tat ca danh sach gia tri lien quan den doi tuong
        private void DoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Khởi tạo các Grid thuộc đối tượng (InfoGrid và VGrid)
            InitGrid_Obj();

            //Load tất cả các phiều của đối tượng
            //Ví dụ khi chọn đối tượng thì load tất cả các phiếu của khách hàng đó.
            GetPhieuDT();
          
            //Load tất cả field của bảng liên quan đến đối tượng gán vào cbFieldObj để cho việc tìm kiếm.
            //Ví dụ: nếu ta chọn đối tượng khách hàng có các field thì ta gán vào comboBox cbFieldObj để là tiêu chí tìm kiếm
            GetFieldObj();            
        }              

        private void Phieu_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (Phieu._getSelectedID() != -1)
            {
                PhieuInfo phieu = getPhieu(Phieu._getSelectedID());
                if(phieu != null)
                    phieu._init();
            }            
        }
        #endregion        

        //Khi đưa vô framework, không cần filter Input
        public void addNumFromTo(QueryBuilder filter, string opLeft, SpinEdit FromSpin, SpinEdit ToSpin)
        {               
            if (FromSpin.EditValue != null)
            {                
                filter.add(opLeft, Operator.GreaterEqual, FromSpin.Value, DbType.Decimal);
            }
            if (ToSpin.EditValue != null)
            {                
                filter.add(opLeft, Operator.LessEqual, ToSpin.Value, DbType.Decimal);
            }
        }

        public void addNumFromTo(QueryBuilder filter, string opLeft, CalcEdit FromCalc, CalcEdit ToCalc)
        {
            if (FromCalc.EditValue != null)
            {                
                filter.add(opLeft, Operator.GreaterEqual, FromCalc.Value, DbType.Decimal);
            }
            if (ToCalc.EditValue != null)
            {                
                filter.add(opLeft, Operator.LessEqual, ToCalc.Value, DbType.Decimal);
            }
        }
    }
}