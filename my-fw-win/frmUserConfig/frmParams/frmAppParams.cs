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

namespace ProtocolVN.Framework.Win
{
    public partial class frmAppParams : XtraFormPL
    {
        #region Các tham số quan trọng sử dụng trong hệ thống 
        List<Param> ParamList = new List<Param>();
        List<ParamGroup> ParamGroupList = new List<ParamGroup>();
        #endregion

        private InitParams InitParam;
        private GetRule Rule;

        public delegate EditorRow[] InitParams();   //Định nghĩa các row param
        public delegate FieldNameCheck[] GetRule(object param); //Định nghĩa các luật kiểm tra

        public frmAppParams(InitParams InitParam, GetRule Rule)
        {
            InitializeComponent();
            _init(InitParam, Rule);
        }

        #region Các hàm khởi tạo
        public void _init(InitParams InitParam, GetRule Rule)
        {
            ParamList = frmAppParamsHelp._initParams();
            ParamGroupList = frmAppParamsHelp._initParamGroup();

            this.InitParam = InitParam;
            this.Rule = Rule;

            if (InitParam != null)
            {
                InitGrid(InitParam());
                InitEventGrid();
                InitDataGrid();
            }
        }

        private void InitGrid(EditorRow[] RowList)
        {
            foreach (ParamGroup pg in ParamGroupList)
            {
                CategoryRow cr = new CategoryRow(pg.NAME);
                foreach (EditorRow er in RowList)                
                    if (GetParamGroupID(er.Properties.FieldName).Equals(pg.ID))
                        if (frmAppParamsHelp.TonTaiThamSo(er.Properties.FieldName))
                        {
                            er.Properties.Caption = GetParamEndUser(er.Properties.FieldName); ;
                            cr.ChildRows.Add(er);
                        }
                vGridMain.Rows.Add(cr);
            }
            ShowGrid(false);
        }

        private void InitEventGrid()
        {
            this.vGridMain.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridMain_FocusedRowChanged);            
            this.vGridMain.LostFocus += new EventHandler(vGridMain_LostFocus);
            this.vGridMain.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(vGridMain_CellValueChanged);
        }              

        private void InitDataGrid()
        {
            this.vGridMain.DataSource = GetDataSource();
        }
        #endregion        

        #region Các hàm trợ giúp
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

        private void ShowGrid(bool ReadOnly)
        {            
            if (ReadOnly)
            {
                vGridMain.OptionsBehavior.Editable = false;
            }
            else
            {
                vGridMain.OptionsBehavior.Editable = true;
            }
        }

        private DataTable GetData()
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
                    if (dataType==FWPLDataType.SHORT_TIME)
                    {
                        dt.Rows[0][param.TEN_THAM_SO] =
                            HelpDateExt02.ToShortTimeString(
                                DateTime.Parse(dt.Rows[0][param.TEN_THAM_SO].ToString()));
                    }
                    else if (dataType==FWPLDataType.DISPLAY_DATE)
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
            catch {
                return null;
            }
        }

        private void _Update()
        {
            if (VGridValidation.ValidateRecord(vGridMain, Rule(null)))
            {
                if (!frmAppParamsHelp.Update(GetData()))
                    HelpMsgBox.ShowNotificationMessage("Cập nhật không thành công");
                else
                    _RefreshParamList();
            }
            else
            {
                if (PLMessageBox.ShowConfirmMessage(
                    "Thông tin bạn vào không hợp lệ. Bạn có muốn vào lại thông tin ?") == DialogResult.No)
                {
                    this.vGridMain.DataSource = GetDataSource();                    
                    vGridMain.ClearRowErrors();
                    vGridMain.Refresh();
                }                
            }
        }

        private void _RefreshParamList()
        {
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
            _Update();
        }       
        #endregion

        #region Các sự kiện khác
        private void vGridMain_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            if (vGridMain.Rows.Count > 0)
            {
                BaseRow br = vGridMain.FocusedRow;
                Param_lb.Text = GetParamEndUser(br.Properties.FieldName) != "" ? GetParamEndUser(br.Properties.FieldName) : "";
                ParamDescrip_lb.Text = GetParamDescription(br.Properties.FieldName) != "" ? GetParamDescription(br.Properties.FieldName) : "";
            }
        }        

        void vGridMain_LostFocus(object sender, EventArgs e)
        {
            vGridMain.Refresh();
            if (!vGridMain.HasRowErrors)
                vGridMain.ClearRowErrors();                         
        }

        void vGridMain_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            _Update();
        }  
        #endregion                
    }
}