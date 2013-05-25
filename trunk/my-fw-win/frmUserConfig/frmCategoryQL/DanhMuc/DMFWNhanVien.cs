using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using DevExpress.XtraGrid.Views.Base;

namespace ProtocolVN.DanhMuc
{
    #region SQL

    #endregion
    public class DMFWNhanVien : IDanhMuc
    {
        public static DMFWNhanVien I = new DMFWNhanVien();
        public static String N = typeof(DMFWNhanVien).FullName;
        public static bool isPermission = false;

        #region IDanhMuc Members

        public string Item()
        {
            return
          @"<cat table='" + HelpFURL.FURL(N, "Init") + @"' type='action' picindex='navNhanVien.png'>
              <lang id='vn'>Nhân viên</lang>
              <lang id='en'></lang>
            </cat>";
        }

        public string MenuItem(string mainCat, string title, string parentID, bool isSep)
        {
            return MenuBuilder.CreateItem(N, title, parentID, true,
                            HelpFURL.FURL(mainCat, N, "Init"), true, isSep, "navNhanVien.png", false, "", "");
        }

        public string MenuItem(string mainCat, string parentID, bool isSep)
        {
            return MenuItem(mainCat, "Nhân viên", parentID, isSep);
        }

        #region Init      

        private GridColumn[] InitColumns(GridControl gridControl, GridView gridView)
        {
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "ID", -1, -1), "ID");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Họ tên nhân viên", 0, 150), "NAME");

            GridColumn ColPhongBan = XtraGridSupportExt.CreateGridColumn(gridView, "Tên phòng ban", 1, -1);
            ColPhongBan.FieldName = "DEPARTMENT_ID";
            XtraGridSupportExt.IDGridColumn(ColPhongBan, "ID", "NAME", "DEPARTMENT", "DEPARTMENT_ID");
            ColPhongBan.OptionsColumn.AllowEdit = true;
            ColPhongBan.OptionsColumn.AllowFocus = false;
            ColPhongBan.OptionsColumn.ReadOnly = true;
            gridView.GroupCount = 1;
            gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
                new DevExpress.XtraGrid.Columns.GridColumnSortInfo(ColPhongBan, DevExpress.Data.ColumnSortOrder.Ascending)}
            );

            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "CMND", 2, 150), "CMND");
            XtraGridSupportExt.DateTimeGridColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Ngày sinh", 3, 150), "NGAY_SINH");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Điện thoại", 4, 150), "DIEN_THOAI");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Địa chỉ", 5, 150), "DIA_CHI");
            XtraGridSupportExt.TextLeftColumn(
                XtraGridSupportExt.CreateGridColumn(gridView, "Email", 6, 150), "EMAIL");
            HelpGridColumn.CotCheckEdit(
                XtraGridSupportExt.CreateGridColumn(gridView, GlobalConst.VISIBLE_TITLE, 100, 100), "VISIBLE_BIT");
            ((PLGridView)gridView).DefaultNewRow = ProtocolVN.Framework.Win.HelpGrid.CheckVisibleDefault;
            //khanhdtn
            gridView.ValidateRow += delegate(object sender, ValidateRowEventArgs e)
            {
                DataRow row = gridView.GetDataRow(e.RowHandle);
                if (row == null || row.HasErrors == true) return;
                GUIValidation.CheckDuplicateField(gridView, ((DataTable)gridView.GridControl.DataSource).DataSet, e,
                    new string[] { "CMND" },
                    new string[] { "\"Chứng minh nhân dân\"" });
            };
            return null;
        }
        private FieldNameCheck[] GetBizRule(object param)
        {
            return new FieldNameCheck[] { 
                        new FieldNameCheck("NAME",
                            new CheckType[]{ CheckType.Required , CheckType.RequireMaxLength },
                            "Họ tên nhân viên", 
                            new object[]{ null, 200 }),
                        new FieldNameCheck("CMND",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "CMND", 
                            new object[]{ 100 }),
                        new FieldNameCheck("DIEN_THOAI",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Điện thoại", 
                            new object[]{ 100 }),
                        new FieldNameCheck("DIA_CHI",
                            new CheckType[]{ CheckType.OptionMaxLength },
                            "Địa chỉ", 
                            new object[]{ 200 }),
                        new FieldNameCheck("EMAIL",
                            new CheckType[]{ CheckType.OptionMaxLength, CheckType.OptionEmail },
                            "Email", 
                            new object[]{ 200, null })
            };
        }                
        public DevExpress.XtraEditors.XtraUserControl Init()
        {
            //DataSet dt = DABase.getDatabase().LoadTable("DM_NHAN_VIEN");
            DataSet dt = DABase.getDatabase().LoadDataSet("SELECT * FROM DM_NHAN_VIEN WHERE -1=1", "DM_NHAN_VIEN");

            DMTreeGroupElement pl = new DMTreeGroupElement();
            pl.Init(GroupElementType.ONLY_INPUT,
                "DEPARTMENT", null, "ID", "PARENT_ID",
                new string[] { "NAME" }, new string[] { "Tên phòng ban" }, dt,
                "ID", "DEPARTMENT_ID", "NAME", HelpGen.G_FW_DM_ID, InitColumns, GetBizRule);
            if(isPermission) pl.DefinePermission(DanhMucParams.GetPermission(pl, DMFWNhanVien.N, "Danh má»¥c nhÃ¢n viÃªn"));            
            return pl;
        }
        #endregion
        public void InitCot(GridColumn Column, string InputField)
        {
            XtraGridSupportExt.IDGridColumn(Column, "ID", "NAME", "DM_NHAN_VIEN", InputField);
        }
        public void InitCtrl(PLCombobox Input, bool ReadOnly)
        {
            DataSet ds = DABase.getDatabase().LoadDataSet("select * from DM_NHAN_VIEN where visible_bit ='Y'","DM_NHAN_VIEN");
            Input._init(ds.Tables[0], "NAME", "ID");
        }

        //Cần phải có table này FW_OBJECT_DEPARTMENT
        public void InitCtrl(PLCombobox Input, string PhongBan, bool IsVisible)
        {
            string query = "";
            if (PhongBan == null)
            {
                query = @"select NV.* from DM_NHAN_VIEN where 1=1";
            }
            else
            {
                query = @"select NV.* from DM_NHAN_VIEN NV,FW_OBJECT_DEPARTMENT PPB,DEPARTMENT D, FW_DM_OBJECT O
                            where O.CODE in (" + PhongBan + @") and O.id=PPB.object_id and PPB.DEPARTMENT_ID=D.ID 
                            and NV.DEPARTMENT_ID=D.ID";
            }
            if (IsVisible) query += " and NV.VISIBLE_BIT = 'Y'";
            query += " order by lower(NV.NAME)";
            DataSet ds = DABase.getDatabase().LoadDataSet(query);

            Input.DataSource = ds.Tables[0];
            Input.DisplayField = "NAME";
            Input.ValueField = "ID";
            Input._TableName = "DM_NHAN_VIEN";
            Input._init();
        }

        public void InitCtrl(PLCombobox Input, bool ReadOnly, bool? IsAdd)
        {
            DataSet dt;
            if (IsAdd != null && IsAdd == true)
            {
                dt = DABase.getDatabase().LoadDataSet("select * from DM_NHAN_VIEN where VISIBLE_BIT='Y' Order by lower(NAME)", "DM_NHAN_VIEN");
            }
            else
            {
                dt = DABase.getDatabase().LoadTable("DM_NHAN_VIEN");
            }
            Input.DataSource = dt.Tables[0];
            Input.DisplayField = "NAME";
            Input.ValueField = "ID";
            Input._init();
        }
        #endregion


        #region Các hàm tiện ích DA làm việc với Danh mục này.
        public static string GetFullName(object ID)
        {
            DatabaseFB db = DABase.getDatabase();
            string strsql = "SELECT dm_nhan_vien.name FROM dm_nhan_vien WHERE dm_nhan_vien.ID='" + ID + "'";
            DbCommand cmd = db.GetSQLStringCommand(strsql);
            IDataReader reader = db.ExecuteReader(cmd);
            if (reader.Read())
                return reader[0].ToString();
            return ID.ToString();
        }

        public static long GetLoginNhanVienID()
        {
            return FrameworkParams.currentUser.employee_id;
        }
        #endregion
    }
}
