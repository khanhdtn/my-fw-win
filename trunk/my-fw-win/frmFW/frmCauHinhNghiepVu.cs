using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraVerticalGrid.Rows;
using ProtocolVN.Framework.Win;
using DevExpress.XtraVerticalGrid;
using ProtocolVN.Framework.Core;
using System.Globalization;
using System.Data.Common;
using DevExpress.XtraVerticalGrid.ViewInfo;
using DevExpress.XtraEditors.DXErrorProvider;

namespace ProtocolVN.Framework.Win
{        
    /// <summary>
    /// Một số vấn đề cần chú ý:
    /// 1. Thêm 1 tham số 
    ///     - Thêm 1 field mới chú ý phải thêm description của nó có định dạng như sau
    ///       Kế toán trưởng;Tên kế toán trưởng được dùng để hiện thị trong báo cáo
    /// </summary>
    public partial class frmCauHinhNghiepVu : XtraFormPL
    {
        #region Cấu hình số phiếu
        private DXErrorProvider Error;
        private Dictionary<int, string> ListMaPhieu;
        int MaxMainPanelHeigh = 520;
        private InitList PhieuList;
        public delegate Dictionary<int, string> InitList();
        #endregion

        #region Tham số nghiệp vụ
        List<Param> ParamList = new List<Param>();
        List<ParamGroup> ParamGroupList = new List<ParamGroup>();
        private InitParams InitParam;
        private GetRule Rule;
        public delegate EditorRow[] InitParams();   //Định nghĩa các row param
        public delegate FieldNameCheck[] GetRule(object param); //Định nghĩa các luật kiểm tra
        #endregion 

        #region Tham số báo cáo
        List<ParamField> FieldList = new List<ParamField>();
        public FieldNameCheck[] rules;
        #endregion

        //List<Param> ParamList = new List<Param>();
        byte[] _headerLetter;

        public frmCauHinhNghiepVu()
        {
            InitializeComponent();

            FieldList = frmAppReportParamsHelp.getFields();
            InitGridBaoCao();
            InitDataGridBaoCao();

            InitEventGrid();

            InitHeaderLetter();

            if (FrameworkParams.IsTrial == false)
            {
                if (__PL__.getFree())
                {
                    btnLuu.Enabled = false;
                }
                else
                {
                    btnLuu.Enabled = true;
                }
            }
        }
        private void frmCauHinhNghiepVu_Load(object sender, EventArgs e)
        {
            HelpXtraForm.SetFix(this);
        }
        #region Các hàm khởi tạo
        public void _initThamSoNghiepVu(InitParams InitParam, GetRule Rule)
        {
            ParamList = frmAppParamsHelp._initParams();
            ParamGroupList = frmAppParamsHelp._initParamGroup();

            this.InitParam = InitParam;
            this.Rule = Rule;

            if (InitParam != null)
            {
                InitGridNghiepVu(InitParam());
                InitDataGridNghiepVu();
            }

            vGridMain_FocusedRowChanged(null, null);
        }
        public void _initCauHinhMauPhieu(InitList listPhieu)
        {
            //Khai báo đúng dạng bên dưới
            //ListMaPhieu = new Dictionary<int, string>();
            //ListMaPhieu.Add(1, "MA_PNKHT;nhập kho hàng trả");
            //ListMaPhieu.Add(2, "MA_PNKHM;nhập kho hàng mua");
            //ListMaPhieu.Add(3, "MA_PXKHT;xuất kho hàng trả");
            //ListMaPhieu.Add(4, "MA_PXKHB;xuất kho hàng bán");
            //ListMaPhieu.Add(5, "MA_PCK;chuyển kho");
            //ListMaPhieu.Add(8, "MA_PKK;kiểm kho");
            if (listPhieu != null)
            {
                this.Error = HelpInputData.GetErrorProvider(this);
                ListMaPhieu = listPhieu();
                InitControl();
            }
        }

        private void InitHeaderLetter()
        {
            string str = "select HEADER_LETTER from company_info";
            FWDBService db = HelpDB.getDBService();
            DbCommand cmd = db.GetSQLStringCommand(str);
            _headerLetter = (byte[])HelpDB.getDBService().ExecuteScalar(cmd);
            HelpImage.LoadImage(this.headerLetter, _headerLetter);
        }
        //private void InitDataGrid()
        //{
        //    this.vGridMain.DataSource = GetDataSource();
        //}
        /// <summary>
        /// Khởi tạo VGrid
        /// </summary>
        private void InitGridBaoCao()
        {
            List<string> captions = new List<string>();
            List<bool> visibles = new List<bool>();
            List<int> widths = new List<int>();
            List<string> fieldNames = new List<string>();
            foreach (ParamField field in FieldList)
            {
                if (field.DESCRIPTION == null || field.DESCRIPTION.Equals(""))
                {
                    captions.Add(field.FIELD_NAME);
                }
                else
                {
                    captions.Add(field.DESCRIPTION.Split(';')[0]);
                }
                fieldNames.Add(field.FIELD_NAME);
                visibles.Add(true);
                widths.Add(40);
            }
            EditorRow[] Rows = HelpEditorRow.CreateEditorRow(
                captions.ToArray(), visibles.ToArray(), widths.ToArray());
            
            int i = 0;
            foreach (EditorRow er in Rows)
            {
                //er.Properties.FieldName = er.Properties.Caption;
                er.Properties.FieldName = fieldNames[i];
                er.Height = 25;//duchs: cho height to ra
                this.InitEditorRowType(er);
                vGridMainBaoCao.Rows.Add(er);
                i++;
            }
            ShowGrid(false);
        }
        private void InitGridNghiepVu(EditorRow[] RowList)
        {
            foreach (ParamGroup pg in ParamGroupList)
            {
                //duchs: bo dong group CategoryRow cr = new CategoryRow();// CategoryRow(pg.NAME);
                foreach (EditorRow er in RowList)
                    if (GetParamGroupID(er.Properties.FieldName).Equals(pg.ID))
                        if (frmAppParamsHelp.TonTaiThamSo(er.Properties.FieldName))
                        {                            
                            er.Properties.Caption = GetParamEndUser(er.Properties.FieldName);
                            er.Height = 25;
                            //cr.ChildRows.Add(er);
                            vGridMain.Rows.Add(er);
                        }
                //vGridMain.Rows.Add(cr);
            }
            ShowGrid(false);
        }
        
        private void InitControl()
        {
            //button          
            //this.btnXemTruoc.Image = FWImageDic.PREVIEW_IMAGE16;

            int newMainPanleHeght = 0;
            if (ListMaPhieu.Count == 0)
            {
                HelpMsgBox.ShowNotificationMessage("Chưa có mã phiếu để cấu hình!");
                HelpXtraForm.CloseFormHasConfirm(this);
                return;
            }

            foreach (int key in ListMaPhieu.Keys)
            {
                PatternSelect ps = new PatternSelect();
                ps.Name = "PS" + key;
                this.flowLayoutPanelPattern.Controls.Add(ps);
                ps.f_setValue(ListMaPhieu[key].Split(';')[0]);

                LabelControl lbl = new LabelControl();
                lbl.Text = "Phiếu " + ListMaPhieu[key].Split(';')[1];
                lbl.ToolTip = lbl.Text;
                lbl.AutoSizeMode = LabelAutoSizeMode.None;
                lbl.AutoEllipsis = true;
                lbl.Size = new System.Drawing.Size(flowLayoutPanelLabel.Size.Width - 20, ps.Size.Height);
                this.flowLayoutPanelLabel.Controls.Add(lbl);

                TextEdit txt = new TextEdit();
                txt.Name = "TXT" + key;
                txt.Properties.ReadOnly = true;
                txt.Properties.AutoHeight = false;
                txt.TabStop = false;
                txt.Size = new System.Drawing.Size(flowLayoutPanelDemo.Size.Width - 8, ps.Size.Height);
                this.flowLayoutPanelDemo.Controls.Add(txt);
                //  txt.Text = DatabaseFB.getSoPhieu(ps.f_getValue());


            }
            newMainPanleHeght = flowLayoutPanelPattern.Size.Height + 30;
            if (MaxMainPanelHeigh > newMainPanleHeght)
            {
                MaxMainPanelHeigh = newMainPanleHeght;
            }           
            panelControlSoPhieu.Dock = DockStyle.Fill;
            this.xtraScrollableControlConfig.VerticalScroll.Enabled = false;
            //duchs: cho hien cau hinh minh hoa
            try
            {
                foreach (int key in ListMaPhieu.Keys)
                {
                    PatternSelect ps = flowLayoutPanelPattern.Controls["PS" + key] as PatternSelect;
                    TextEdit txt = flowLayoutPanelDemo.Controls["TXT" + key] as TextEdit;
                    txt.Text = DatabaseFB.getSoPhieu(ps.f_getValue());
                }
            }
            catch { }

        }
      
        public bool ValidateData()
        {
            Error.ClearErrors();
            foreach (int key in ListMaPhieu.Keys)
            {
                PatternSelect ps = flowLayoutPanelPattern.Controls["PS" + key] as PatternSelect;
                ps.f_checkInput(Error);
            }
            if (Error.HasErrors) return false;
            return true;
        }
        private string GetParamEndUser(string param_name)
        {
            foreach (Param p in ParamList)
                if (p.TEN_THAM_SO.Equals(param_name))
                    return p.TEN_THAM_SO_USER;
            return "";
        }
        private long GetParamGroupID(string param_name)
        {
            foreach (Param p in ParamList)
                if (p.TEN_THAM_SO.Equals(param_name))
                    return p.NHOM_THAM_SO;
            return 0;
        }    
        /// <summary>
        /// Khởi tạo các sự kiện trên VGrid
        /// </summary>
        private void InitEventGrid()
        {
            this.vGridMainBaoCao.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridMain_FocusedRowChanged);            
            this.vGridMainBaoCao.LostFocus += new EventHandler(vGridMain_LostFocus);
            this.vGridMain.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridMain_FocusedRowChanged);
            this.vGridMain.LostFocus += new EventHandler(vGridMain_LostFocus);
       
        }
      
        /// <summary>
        /// Khởi tạo dữ liệu VGrid
        /// </summary>
        private void InitDataGridBaoCao()
        {
            DataSet ds = frmAppReportParamsHelp.LoadDataSource();
            if (ds != null && ds.Tables.Count > 0)
                this.vGridMainBaoCao.DataSource = ds.Tables[0]; 
        }
        private void InitDataGridNghiepVu()
        {
            this.vGridMain.DataSource = GetDataSource();
        }
        private DataTable GetDataSource()
        {
            DataTable dt = new DataTable();
            try
            {
                foreach (Param param in ParamList)
                {
                    DataColumn col = new DataColumn(param.TEN_THAM_SO);
                    dt.Columns.Add(col);
                }

                DataRow dr = dt.NewRow();
                foreach (Param param in ParamList)
                    dr[param.TEN_THAM_SO] = param.GIA_TRI;
                dt.Rows.Add(dr);
            }
            catch { }

            return dt;
        }
        /// <summary>
        /// Khởi tạo kiểu dữ liệu của dòng thuộc VGrid
        /// </summary>
        /// <param name="row"></param>
        private void InitEditorRowType(EditorRow row)
        {
            switch (GetFieldType(row.Properties.FieldName))
            {
                case FB_DATA_TYPE.SMALLINT:
                case FB_DATA_TYPE.INTEGER:
                case FB_DATA_TYPE.INT64:
                    HelpEditorRow.DongSpinEdit(row, row.Properties.FieldName, 1);
                    break;
                case FB_DATA_TYPE.FLOAT:
                case FB_DATA_TYPE.D_FLOAT:
                case FB_DATA_TYPE.DOUBLE:
                    HelpEditorRow.DongCalcEdit(row, row.Properties.FieldName, 3);
                    break;
                case FB_DATA_TYPE.BOOLEAN:
                    HelpEditorRow.DongCheckEdit(row, row.Properties.FieldName);
                    break;
                case FB_DATA_TYPE.DATE:
                    HelpEditorRow.DongDateEdit(row, row.Properties.FieldName);
                    break;
                case FB_DATA_TYPE.TIMESTAMP:
                    //Tạm thời dùng timestamp cho time -> Chưa hỗ trợ Date và Time cùng lúc
                    //Nếu có hỗ trợ date và time mình sẽ đọc chuỗi định dạng từ Description
                    //PHUOCNT TODO
                    HelpEditorRow.DongTimeEdit(row, row.Properties.FieldName, "HH:mm:ss");
                    break;                
                default:
                    HelpEditorRow.DongTextLeft(row, row.Properties.FieldName);
                    break;
            }
        }
        #endregion        

        //#region Tham số truyền vào cho tab tham số nghiệp vụ
        //private static EditorRow[] CreateVGrid_Basic()
        //{
        //    EditorRow[] Rows = HelpEditorRow.CreateEditorRow(
        //              new string[]{ 
        //                        "KHO_MAC_DINH",
        //                        "GIA_VON_MAC_DINH",
        //                        "TON_KHO_AM",
        //                        "CHO_PHEP_CANH_BAO",
        //                        "SO_PHUT_LAP_LAI_CBTK"
        //                    },
        //              new bool[]  {  
        //                        true,
        //                        true,
        //                        true,
        //                        true,
        //                        true
        //                    },
        //              new int[]   {  
        //                        20,
        //                        20, 
        //                        20,
        //                        20,
        //                        20,
        //                    });

        //    HelpEditorRow.DongRepository(Rows[0], "KHO_MAC_DINH",
        //        HelpRepository.GetCotPLLookUp(
        //                        "ID", "NAME", HelpDB.getDatabase().LoadDataSet("select * from DM_KHO", "DM_KHO").Tables[0],
        //                        new string[] { "NAME" }, new string[] { "Tên kho" }, "KHO_MAC_DINH",
        //                        true, new int[] { 20 }), DevExpress.Utils.HorzAlignment.Near);
        //    HelpEditorRow.DongRepository(Rows[1], "GIA_VON_MAC_DINH",
        //      HelpRepository.GetCotPLLookUp(
        //                      "TEN_COT_TRONG_TON_KHO", "NAME", HelpDB.getDatabase().LoadDataSet("select * from DM_LOAI_GIA_VON", "DM_LOAI_GIA_VON").Tables[0],
        //                      new string[] { "NAME" }, new string[] { "Tên giá vốn" }, "GIA_VON_MAC_DINH",
        //                      true, new int[] { 20 }), DevExpress.Utils.HorzAlignment.Near);
        //    HelpEditorRow.DongRepository(Rows[2], "TON_KHO_AM", HelpRepository.GetCheckEdit(false),
        //        DevExpress.Utils.HorzAlignment.Far);
        //    HelpEditorRow.DongRepository(Rows[3], "CHO_PHEP_CANH_BAO", HelpRepository.GetCheckEdit(false),
        //        DevExpress.Utils.HorzAlignment.Default);
        //    HelpEditorRow.DongRepository(Rows[4], "SO_PHUT_LAP_LAI_CBTK", 
        //        HelpRepository.GetSpinEdit(0), DevExpress.Utils.HorzAlignment.Center);           
           
        //    return Rows;

        //}
        //private static FieldNameCheck[] GetRuleVGrid_Basic(object param)
        //{
        //    return new FieldNameCheck[] { 
        //            new FieldNameCheck("KHO_MAC_DINH",
        //                new CheckType[]{ CheckType.Required},
        //                new string[]{ ErrorMsgLib.errorGreater0("Kho mặc định")}, 
        //                new object[]{ null }),
        //            new FieldNameCheck("GIA_VON_MAC_DINH",
        //                new CheckType[]{ CheckType.OptionMaxLength},
        //                new string[]{ ErrorMsgLib.errorGreaterEqual0("Giá vốn mặc định")}, 
        //                new object[]{ 50 }),
        //            new FieldNameCheck("SO_PHUT_LAP_LAI_CBTK",
        //                new CheckType[]{ CheckType.IntGreater0},
        //                new string[]{ErrorMsgLib.errorGreater0("Số phút lặp lại thông báo CBTK")},
        //                new object[]{null})};
        //    // return new FieldNameCheck[]{ null};
        //}
        //#endregion

        #region Các hàm trợ giúp
        /// <summary>
        /// Trả về mô tả field
        /// </summary>
        /// <param name="field_name">Tên field</param>
        /// <returns>Mô tả field</returns>
        private string GetFieldDescription(string field_name)
        {
            foreach (ParamField field in FieldList)
                if (field.FIELD_NAME.Equals(field_name)){
                    if (field.DESCRIPTION == null || field.DESCRIPTION.Equals(""))
                    {
                        return field.FIELD_NAME;
                    }
                    if(field.DESCRIPTION.Split(';').Length == 2)
                        return field.DESCRIPTION.Split(';')[1];
                    else
                        return field.DESCRIPTION.Split(';')[0];
                }
            return "";
        }
        private string GetParamGroupName(long id)
        {
            foreach (ParamGroup pg in ParamGroupList)
                if (pg.ID.Equals(id))
                    return pg.NAME;
            return "";
        }
        private string GetParamDescription(string param_name)
        {
            foreach (Param p in ParamList)
                if (p.TEN_THAM_SO.Equals(param_name))
                    return p.MO_TA;
            return "";
        }

        /// <summary>
        /// Trả về kiểu dữ liệu field
        /// </summary>
        /// <param name="field_name">Tên field</param>
        /// <returns>Kiểu dữ liệu field</returns>
        private FB_DATA_TYPE GetFieldType(string field_name)
        {
            foreach (ParamField field in FieldList)
                if (field.FIELD_NAME.Equals(field_name))
                    return (FB_DATA_TYPE)field.FIELD_TYPE;
            return FB_DATA_TYPE.VARCHAR;
        }        

        /// <summary>
        /// Trả về chiều dài field
        /// </summary>
        /// <param name="field_name">Tên field</param>
        /// <returns>Chiều dài</returns>
        private long GetFieldLength(string field_name)
        {
            foreach (ParamField field in FieldList)
                if (field.FIELD_NAME.Equals(field_name))
                    return field.FIELD_LENGTH;
            return -1;
        }   

        private void ShowGrid(bool ReadOnly)
        {            
            if (ReadOnly)
            {
                vGridMain.OptionsBehavior.Editable = false;
                vGridMainBaoCao.OptionsBehavior.Editable = false;
            }
            else
            {
                vGridMain.OptionsBehavior.Editable = true;
                vGridMainBaoCao.OptionsBehavior.Editable = true;
            }
        }

        /// <summary>
        /// Trả về DataSource của VGrid
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataBaoCao()
        {
            try
            {
                return (DataTable)vGridMainBaoCao.DataSource;
            }
            catch
            {
                return null;
            }            
        }
        private DataTable GetDataNghiepVu()
        {
            try
            {
                DataTable dt = (DataTable)vGridMain.DataSource;
                DataTable dt_new = frmAppParamsHelp.LoadSchema();

                foreach (Param param in ParamList)
                {
                    DataRow dr = dt_new.NewRow();
                    dr["NHOM_THAM_SO"] = param.NHOM_THAM_SO;
                    dr["TEN_THAM_SO"] = param.TEN_THAM_SO;
                    FWPLDataType dataType =
                        HelpMultiDataTypeField.ToFWDatType(
                            HelpNumber.ParseInt32(param.DATA_TYPE));
                    if (dataType == FWPLDataType.SHORT_TIME)
                    {
                        dt.Rows[0][param.TEN_THAM_SO] =
                            HelpDateExt02.ToShortTimeString(
                                DateTime.Parse(dt.Rows[0][param.TEN_THAM_SO].ToString()));
                    }
                    else if (dataType == FWPLDataType.DISPLAY_DATE)
                    {
                        dt.Rows[0][param.TEN_THAM_SO] =
                            HelpDateExt02.ToDisplayDateString(
                                DateTime.Parse(dt.Rows[0][param.TEN_THAM_SO].ToString()));
                    }

                    dr["GIA_TRI"] = dt.Rows[0][param.TEN_THAM_SO];
                    dr["MO_TA"] = param.MO_TA;
                    dr["TEN_NHOM_THAM_SO"] = GetParamGroupName(param.NHOM_THAM_SO);
                    dr["VISIBLE_BIT"] = "Y";
                    dr["TEN_THAM_SO_USER"] = param.TEN_THAM_SO_USER;
                    dr["DATA_TYPE"] = param.DATA_TYPE != "" ? int.Parse(param.DATA_TYPE) : 0;
                    dt_new.Rows.Add(dr);
                }
                return dt_new;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Lưu lại thay đổi trên tab thiết lập định dạng số phiếu
        /// </summary>
        /// <returns></returns>
        private bool _UpdateSoPhieu()
        {

            foreach (int key in ListMaPhieu.Keys)
            {
                PatternSelect ps = flowLayoutPanelPattern.Controls["PS" + key] as PatternSelect;
                if (DatabaseFB.SetThamSo(ListMaPhieu[key].Split(';')[0], ps.f_getValue()) == false)
                    return false;
            }

            return true;
        }
        /// <summary>
        /// Cập nhật giá trị của các field trên VGrid xuống DB
        /// </summary>
        private bool _UpdateBaoCao()
        {           
            if (!(frmAppReportParamsHelp.Update(GetDataBaoCao()) && _UpdateHeaderLetter()))                
                return false;
            else
            {                
                _RefreshParamList();
                return true;
            }           
        }
        private bool _UpdateNghiepVu()
        {            
            if (!frmAppParamsHelp.Update(GetDataNghiepVu()))               
                return false;
            else
            {                
                _RefreshParamList();
                return true;
            }          
        }
        private bool _UpdateHeaderLetter()
        {
            try
            {
                string str = "update company_info set HEADER_LETTER=@header_letter";
                FWDBService db = HelpDB.getDBService();
                DbCommand cmd = db.GetSQLStringCommand(str);
                db.AddInParameter(cmd, "@header_letter", DbType.Binary, _headerLetter);
                if (db.ExecuteNonQuery(cmd) > 0)
                    return true;
                else return false;
            }
            catch { return false; }
        }
        private void _RefreshParamList()
        {
            if(tabNghiepVu.SelectedTabPage==tabPageNghiepVu)
                FieldList = frmAppReportParamsHelp.getFields();
            else if(tabNghiepVu.SelectedTabPage==tabPageBaoCao)
                ParamList = frmAppParamsHelp._initParams();
        }
        #endregion

        #region Các sự kiện Button
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateData() != true)
            {
                PLMessageBox.ShowConfirmMessage("Thiết lập định dạng số phiếu chưa hợp lệ");
                return;
            }
            if (!VGridValidation.ValidateRecord(vGridMain, Rule(null)))
            {
                if (PLMessageBox.ShowConfirmMessage(
                    "Thông tin bạn vào tham số nghiệp vụ không hợp lệ. Bạn có muốn vào lại thông tin ?") == DialogResult.No)
                {
                    this.vGridMain.DataSource = GetDataSource();
                    vGridMain.ClearRowErrors();
                    vGridMain.Refresh();
                }
                return;
            }

            if (_UpdateSoPhieu() && _UpdateNghiepVu() && _UpdateBaoCao())
                this.Close();
            else HelpMsgBox.ShowNotificationMessage("Lưu thông tin không thành công");

        }       
        #endregion

        #region Các sự kiện khác
        /// <summary>
        /// Xử lý sự kiện khi focus dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vGridMain_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            //if (tabNghiepVu.SelectedTabPage == tabPageNghiepVu)
            //{
            //    if (vGridMain.Rows.Count > 0)
            //    {
            //        BaseRow br = vGridMain.FocusedRow;
            //        //Param_lb.Text = GetParamEndUser(br.Properties.FieldName) != "" ? GetParamEndUser(br.Properties.FieldName) : "";
            //        if (br != null)
            //        {   
            //            lblDescripNghiepVu.Text = GetParamDescription(br.Properties.FieldName) != "" ? GetParamDescription(br.Properties.FieldName) : "";                                                                     
            //        }
            //    }
            //}
            //else if (tabNghiepVu.SelectedTabPage == tabPageBaoCao)
            //{
            //    if (vGridMainBaoCao.Rows.Count > 0)
            //    {
            //        BaseRow br = vGridMainBaoCao.FocusedRow;
            //        lblDescripBaoCao.Text = GetFieldDescription(br.Properties.FieldName) != "" ? GetFieldDescription(br.Properties.FieldName) : "";
            //    }
            //}
            //else
            {
                if (vGridMain.Rows.Count > 0)
                {
                    BaseRow br = vGridMain.FocusedRow;
                    //Param_lb.Text = GetParamEndUser(br.Properties.FieldName) != "" ? GetParamEndUser(br.Properties.FieldName) : "";
                    if (br != null)
                    {
                        lblDescripNghiepVu.Text = GetParamDescription(br.Properties.FieldName) != "" ? GetParamDescription(br.Properties.FieldName) : "";
                    }
                }
                
                if (vGridMainBaoCao.Rows.Count > 0)
                {
                    BaseRow br = vGridMainBaoCao.FocusedRow;
                    lblDescripBaoCao.Text = GetFieldDescription(br.Properties.FieldName) != "" ? GetFieldDescription(br.Properties.FieldName) : "";
                }
            }
        }        

        /// <summary>
        /// Xử lý sự kiện khi mất focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vGridMain_LostFocus(object sender, EventArgs e)
        {
            if (tabNghiepVu.SelectedTabPage == tabPageBaoCao)
            {
                vGridMainBaoCao.Refresh();
                if (!vGridMainBaoCao.HasRowErrors)
                    vGridMainBaoCao.ClearRowErrors();
            }
            else if (tabNghiepVu.SelectedTabPage == tabPageNghiepVu)
            {
                vGridMain.Refresh();
                if (!vGridMain.HasRowErrors)
                    vGridMain.ClearRowErrors();
            }
        }
        private void xtraScrollableControlExample_Scroll(object sender, XtraScrollEventArgs e)
        {
            xtraScrollableControlConfig.VerticalScroll.Value = xtraScrollableControlExample.VerticalScroll.Value;
        }

        private void xtraScrollableControlConfig_Scroll(object sender, XtraScrollEventArgs e)
        {
            xtraScrollableControlExample.VerticalScroll.Value = xtraScrollableControlConfig.VerticalScroll.Value;
        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (int key in ListMaPhieu.Keys)
                {
                    PatternSelect ps = flowLayoutPanelPattern.Controls["PS" + key] as PatternSelect;
                    TextEdit txt = flowLayoutPanelDemo.Controls["TXT" + key] as TextEdit;
                    txt.Text = DatabaseFB.getSoPhieu(ps.f_getValue());
                }
            }
            catch { }
        }
        private void tabNghiepVu_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == TabPageThietLapSoPhieu)
                btnXemTruoc.Visible = true;
            else btnXemTruoc.Visible = false;
        }

        private void headerLetter_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.Cancel || openFileDialog1.FileName == "")
                return;
            try
            {
                _headerLetter = HelpImage.GetImage(openFileDialog1.FileName);
                HelpImage.LoadImage(headerLetter, _headerLetter);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                FWMsgBox.showErrorImage();
            }
        }

        private void btnChonPic_Click(object sender, EventArgs e)
        {
            String fileName = HelpCommonDialog.showChooseFileByOpenFileDialog("Hình ảnh(*.bmp,*.icon,*.ico,*.jpg,*.jpeg,*.gif,*.png)|*.bmp;*.icon;*.ico;*.jpg;*.jpeg;*.gif;*.png", "Chọn hình", -1);
            if (fileName == "")
            {
                return;
            }
            else
            {
                try
                {
                    _headerLetter = HelpImage.GetImage(fileName);
                    HelpImage.LoadImage(headerLetter, _headerLetter);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                    FWMsgBox.showErrorImage();
                }
            }
        }

        private void btnXoaPic_Click(object sender, EventArgs e)
        {
            headerLetter.Image = null;
            headerLetter.Refresh();
        }
        #endregion

        #region Các hàm định nghĩa
        /// <summary>
        /// Định nghĩa các rule trên field
        /// </summary>
        /// <returns>Danh sách các field đã được định nghĩa rule</returns>
        private FieldNameCheck[] GetRuleBaoCao()
        {
            //return new FieldNameCheck[] { 
            //                new FieldNameCheck("FIELD_1",
            //                    new CheckType[]{ CheckType.Required , CheckType.RequireMaxLength },
            //                    "Field 1", new object[]{ null, 100 })
            //};
            return rules;
        }
        #endregion
    }
}