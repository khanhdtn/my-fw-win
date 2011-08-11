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
    /// <summary>
    /// Một số vấn đề cần chú ý:
    /// 1. Thêm 1 tham số 
    ///     - Thêm 1 field mới chú ý phải thêm description của nó có định dạng như sau
    ///       Kế toán trưởng;Tên kế toán trưởng được dùng để hiện thị trong báo cáo
    /// </summary>
    public partial class frmAppReportParams : XtraFormPL
    {
        #region Các tham số quan trọng sử dụng trong hệ thống
        List<ParamField> FieldList = new List<ParamField>();
        #endregion        
        public FieldNameCheck[] rules;

        public frmAppReportParams()
        {
            InitializeComponent();
            _init();
        }

        #region Các hàm khởi tạo
        public void _init()
        {
            FieldList = frmAppReportParamsHelp.getFields();
            InitGrid();
            InitEventGrid();
            InitDataGrid();            
        }

        /// <summary>
        /// Khởi tạo VGrid
        /// </summary>
        private void InitGrid()
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
                this.InitEditorRowType(er);
                vGridMain.Rows.Add(er);
                i++;
            }
            ShowGrid(false);
        }

        /// <summary>
        /// Khởi tạo các sự kiện trên VGrid
        /// </summary>
        private void InitEventGrid()
        {
            this.vGridMain.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridMain_FocusedRowChanged);            
            this.vGridMain.LostFocus += new EventHandler(vGridMain_LostFocus);            
        }              

        /// <summary>
        /// Khởi tạo dữ liệu VGrid
        /// </summary>
        private void InitDataGrid()
        {
            DataSet ds = frmAppReportParamsHelp.LoadDataSource();
            if (ds != null && ds.Tables.Count > 0)
                this.vGridMain.DataSource = ds.Tables[0]; 
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
            }
            else
            {
                vGridMain.OptionsBehavior.Editable = true;
            }
        }

        /// <summary>
        /// Trả về DataSource của VGrid
        /// </summary>
        /// <returns></returns>
        private DataTable GetData()
        {
            try
            {
                return (DataTable)vGridMain.DataSource;
            }
            catch
            {
                return null;
            }            
        }

        /// <summary>
        /// Cập nhật giá trị của các field trên VGrid xuống DB
        /// </summary>
        private void _Update()
        {
            if (VGridValidation.ValidateRecord(vGridMain, this.GetRule()))
            {
                if (!frmAppReportParamsHelp.Update(GetData()))
                    PLMessageBox.ShowErrorMessage("Cập nhật không thành công.");
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Cập nhật thành công.");
                    _RefreshParamList();
                }
            }
            else
            {
                if (PLMessageBox.ShowConfirmMessage(
                    "Thông tin bạn vào không hợp lệ. Bạn có muốn vào lại thông tin ?") == DialogResult.No)
                {
                    InitDataGrid();                    
                    vGridMain.ClearRowErrors();
                    vGridMain.Refresh();
                }
            }
        }       

        private void _RefreshParamList()
        {
            FieldList = frmAppReportParamsHelp.getFields();
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
        /// <summary>
        /// Xử lý sự kiện khi focus dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vGridMain_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            if (vGridMain.Rows.Count > 0)
            {
                BaseRow br = vGridMain.FocusedRow;                
                ParamDescrip_lb.Text = GetFieldDescription(br.Properties.FieldName) != "" ? GetFieldDescription(br.Properties.FieldName) : "";
            }
        }        

        /// <summary>
        /// Xử lý sự kiện khi mất focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vGridMain_LostFocus(object sender, EventArgs e)
        {
            vGridMain.Refresh();
            if (!vGridMain.HasRowErrors)
                vGridMain.ClearRowErrors();
        }
        #endregion

        #region Các hàm định nghĩa
        /// <summary>
        /// Định nghĩa các rule trên field
        /// </summary>
        /// <returns>Danh sách các field đã được định nghĩa rule</returns>
        private FieldNameCheck[] GetRule()
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