using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.Common;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class PLDynamicField : XtraUserControl
    {
        private string tablename;       
        
        public PLDynamicField()
        {
            InitializeComponent();                        
        }

        public DevExpress.XtraVerticalGrid.VGridControl CustomizeFieldGrid
        {
            get { return this.customizeField_vgc; }
        }

        public string Tablename
        {
            get { return tablename; }
            set { tablename = value; }
        }

        #region Các hàm khởi tạo dữ liệu ban đầu
        // Hàm khởi tạo các field rỗng từ bảng chỉ định cho CustomField control
        /// <summary>
        /// Phiếu bán hàng mình có 1 số trường động nhưng và mình cho phép người dùng tự thêm vào
        /// do đó mình sẽ dùng control này với table là PhieuBanHang vậy khi
        /// Init mình sẽ _init(PhieuBanHang)
        /// </summary>
        /// <param name="table_dependency"></param>
        public void _init(string table_dependency)
        {
            this.tablename = table_dependency;
            DADynamicField.LoadFields(customizeField_vgc, table_dependency);
        }

        /// <summary>
        /// Thông tin của PBH có mã số là 15. Nó sẽ lấy danh sách cách field mở rộng của phiếu bán hàng
        /// 15 từ bảng chứa dữ liệu mở rộng
        /// </summary>
        /// <param name="id"></param>
        public void _setSelectedID(long id)
        {
            List<FieldData> array = this.LoadFields(this.tablename, id);
            this.InitBindingTable(array);
        }

        //Cập nhật nội dung mở rộng
        public bool _update(long id)
        {
            List<FieldData> array = this.GetData();
            return this.UpdateFields(array, id);    
        }

        //Delete nội dung mở rộng
        public bool _delete(long id)
        {
            return this.DeleteFields(id);
        }

        // Hàm lấy dữ liệu các field từ phiếu ID        
        public List<FieldData> LoadFields(string tablename_dependency, long phieu_id)
        {
            customizeField_vgc.Rows.Clear();
            return DADynamicField.Load(tablename_dependency, phieu_id);
        }

        // Hàm khởi tạo các field (bao gồm dữ liệu) từ danh sách các field cho CustomField control
        public void InitBindingTable(List<FieldData> fields_arr)
        {
            customizeField_vgc.Rows.Clear();
            foreach (FieldData field in fields_arr)
            {
                DevExpress.XtraVerticalGrid.Rows.EditorRow row = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                row.Name = "_" + field.FIELD_ID;                
                row.Properties.Caption = field.CAPTION;
                row.Properties.Value = field.CONTENT;
                if (field.DATA_TYPE == 1)
                    HelpEditorRow.DongTextLeft(row, null);
                else if (field.DATA_TYPE == 2)
                    HelpEditorRow.DongSpinEdit(row, null, 0);
                else if (field.DATA_TYPE == 3)
                    HelpEditorRow.DongCalcEdit(row, null, 3);
                else if (field.DATA_TYPE == 4)
                    HelpEditorRow.DongCheckEdit(row, null);
                else if (field.DATA_TYPE == 5)
                    HelpEditorRow.DongDateEdit(row, null);
                customizeField_vgc.Rows.Add(row);
            }
        }
        #endregion

        // Hàm lấy dữ liệu từ CustomField chuẩn bị cho việc update
        public List<FieldData> GetData()
        {
            List<FieldData> field_arr = new List<FieldData>();
            if (customizeField_vgc.Rows.Count > 0)
            {
                for (int i = 0; i < customizeField_vgc.Rows.Count; i++)
                {
                    DevExpress.XtraVerticalGrid.Rows.BaseRow row = customizeField_vgc.Rows[i];
                    FieldData field = new FieldData();
                    field.FIELD_ID = long.Parse(row.Name.Substring(1));
                    if (row.Properties.Value != null)
                        field.CONTENT = row.Properties.Value.ToString().Trim();
                    field_arr.Add(field);
                }
            }
            return field_arr;
        }

        
        // Hàm update dữ liệu trong CustomField
        public bool UpdateFields(List<FieldData> fields_arr, long phieu_id)
        {
            bool flag = true;
            foreach (FieldData field in fields_arr)
            {
                bool _flag = DADynamicField.UpdateContent(field, phieu_id);
                if (!_flag)
                    flag = false;
            }
            return flag;
        }

        // Hàm xóa các field của 1 phiếu
        private bool DeleteFields(long phieu_id)
        {
            return DADynamicField.Delete(phieu_id);
        }        

        // Hàm tạo chuỗi filter từ CustomField cho DataTable
        public string CreateFilterExpression()
        {
            _refreshDynamicField();
            StringBuilder builder = new StringBuilder();
            builder.Append("1=1");
            if (customizeField_vgc.Rows.Count > 0)
            {
                foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow br in customizeField_vgc.Rows)
                {                    
                    if (br.Properties.FieldName != "")
                        if (br.Properties.Value != null && br.Properties.Value.ToString() != "")
                            builder.Append(" and " + br.Properties.FieldName + "='" +
                                br.Properties.Value + "'");                    
                }
            }
            return builder.ToString();
        }

        private void _refreshDynamicField()
        {
            List<DevExpress.XtraVerticalGrid.Rows.BaseRow> fields_deleted = new List<DevExpress.XtraVerticalGrid.Rows.BaseRow>();
            foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow br in customizeField_vgc.Rows)
                if (!DADynamicField.CheckFieldIsExist(long.Parse(br.Name.Substring(1))))
                    fields_deleted.Add(br);
            foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow br in fields_deleted)
                customizeField_vgc.Rows.Remove(br);
        }        

        // Hàm trả về DataSet sau khi đã qua filter với các field trong CustomField
        public DataSet GetDataSetFiltered(DataSet Input, 
            DevExpress.XtraGrid.Views.Grid.GridView grid, bool displayFieldExt)
        {           
            DataTable Input_tb = Input.Tables[0];
            QueryBuilder filter = new QueryBuilder
            (
               @"select tf.caption, tf.field_id, tf.data_type" +
                    " from fw_table_field_ext tf inner join fw_table_object fo" +
                    " on (tf.table_id=fo.id) where fo.name='" + tablename +
                    "' and 1=1"
            );
            DataSet ds = DABase.getDatabase().LoadReadOnlyDataSet(filter);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string new_columnName = "_" + dr["FIELD_ID"];
                DataColumn new_c = new DataColumn(new_columnName);
                Input_tb.Columns.Add(new_c);
                if (customizeField_vgc.Rows.Count > 0)
                {
                    foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow br in customizeField_vgc.Rows)
                    {
                        if (br.Name == new_columnName)
                            br.Properties.FieldName = new_columnName;
                    }
                }
                for (int i = 0; i < Input_tb.Rows.Count; i++)
                {
                    string sql = "select content from fw_table_content_ext where field_id='" +
                        dr["FIELD_ID"] + "' and table_key='" + Input_tb.Rows[i][0] + "'";
                    DatabaseFB db = DABase.getDatabase();
                    DbCommand select = db.GetSQLStringCommand(sql);
                    object content = db.ExecuteScalar(select);
                    if (content != null)
                        Input_tb.Rows[i][new_columnName] = content.ToString();
                    else
                        Input_tb.Rows[i][new_columnName] = "";
                }
                if (displayFieldExt)
                    AddGridColumn(grid, dr["CAPTION"].ToString(), new_columnName, 
                        int.Parse(dr["DATA_TYPE"].ToString())); 
            }

            DataTable _dt = Input_tb.Copy();
            _dt.Clear();
            DataRow[] dr_arr = Input_tb.Copy().Select(CreateFilterExpression());
            for (int i = 0; i < dr_arr.Length; i++)
                _dt.ImportRow(dr_arr[i]);

            DataSet _ds = new DataSet();
            _ds.Tables.Add(_dt);
            return _ds;
        }

        private void AddGridColumn(DevExpress.XtraGrid.Views.Grid.GridView grid,
            string caption, string fieldname, int datatype)
        {
            DevExpress.XtraGrid.Columns.GridColumn c_grid =
                    new DevExpress.XtraGrid.Columns.GridColumn();
            c_grid.Caption = caption;
            c_grid.FieldName = fieldname;
            c_grid.VisibleIndex = grid.Columns.Count;
            if (datatype == 4)
                HelpGridColumn.CotCheckEdit(c_grid, fieldname);   

            bool flag = false;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].FieldName == fieldname)
                    flag = true;
            }
            if (!flag)
                grid.Columns.Add(c_grid);
        }

        private void customizeField_vgc_Leave(object sender, EventArgs e)
        {
            customizeField_vgc.Refresh();   
        }           
    }
}
