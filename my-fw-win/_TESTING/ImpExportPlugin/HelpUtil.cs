using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Plugin.ImpExp
{
    public class HelpUtil
    {
        public static void CreateGridColumn(GridView gridView, DataTable dt, string tableName)
        {
            DataColumn colError = new DataColumn("ERROR");
            dt.Columns.Add(colError);
            DataColumn colNewRow = new DataColumn("NEW_ROW");
            dt.Columns.Add(colNewRow);

            GridColumn cotLoi = new GridColumn();
            cotLoi.Caption = "Dòng lỗi";
            cotLoi.FieldName = "ERROR";
            cotLoi.OptionsColumn.AllowEdit = false;
            RepositoryItemCheckEdit chkEditErr = new RepositoryItemCheckEdit();
            chkEditErr.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            chkEditErr.ValueChecked = "1";
            chkEditErr.ValueUnchecked = null;
            cotLoi.ColumnEdit = chkEditErr;
            cotLoi.VisibleIndex = 0;
            cotLoi.Visible = true;
            gridView.Columns.Add(cotLoi);
            GridColumn cotNewRow = new GridColumn();
            cotNewRow.Caption = "Dòng mới";
            cotNewRow.FieldName = "NEW_ROW";
            cotNewRow.OptionsColumn.AllowEdit = false;
            RepositoryItemCheckEdit chkEditNew = new RepositoryItemCheckEdit();
            chkEditNew.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            chkEditNew.ValueChecked = "1";
            chkEditNew.ValueUnchecked = null;
            cotNewRow.ColumnEdit = chkEditNew;
            cotNewRow.VisibleIndex = 1;
            cotNewRow.Visible = true;
            gridView.Columns.Add(cotNewRow);

            DataSet ds = DABase.getDatabase().LoadTable("FW_MAP_FIELD_CAPTION");
            DataRow[] drows = ds.Tables[0].Select("TABLE_NAME='" + tableName + "'");
            if (drows.Length > 0)
            {
                for (int i = 0; i < drows.Length; i++)
                {
                    DataRow dr = drows[i];
                    GridColumn col = new GridColumn();
                    col.Caption = dr["CAPTION"].ToString();
                    col.FieldName = dr["FIELD_NAME"].ToString();
                    col.VisibleIndex = i + 2;
                    col.Visible = true;
                    gridView.Columns.Add(col);
                }
            }
            else
            {
                for (int i = 0; i < dt.Columns.Count - 2; i++)
                {
                    DataColumn dc = dt.Columns[i];
                    GridColumn col = new GridColumn();
                    col.Caption = dc.Caption;
                    col.FieldName = dc.ColumnName;
                    col.VisibleIndex = i + 2;
                    col.Visible = true;
                    gridView.Columns.Add(col);
                }
            }

        }
        public static DataTable SetDataSourceFromExcel()
        {
            DataTable dt = null;
            if (ExcelSupport.filenamepath != "")
            {
                DialogResult result = PLMessageBox.ShowConfirmMessage("Bạn có chắc chọn tập tin khác không?");
                if (result == DialogResult.Yes)
                {
                    ExcelSupport.filenamepath = OpenFileName();
                    List<string> lstSheet = ExcelSupport.GetSheetNames(ExcelSupport.filenamepath);
                    if (lstSheet.Count > 0)
                        dt = AddDataSheet(lstSheet[0]);
                }
            }
            else
            {
                ExcelSupport.filenamepath = OpenFileName();
                List<string> lstSheet = ExcelSupport.GetSheetNames(ExcelSupport.filenamepath);
                if (lstSheet.Count > 0)
                    dt = AddDataSheet(lstSheet[0]);
            }
            return dt;
        }
        private static DataTable AddDataSheet(string sheetName)
        {
            DataTable dt = new DataTable(sheetName);
            ExcelSupport.open();
            DB xuly = new DB();
            xuly._DataSet = ExcelSupport.dataSet(sheetName);
            dt = xuly.xulydataset().Tables[0];
            ExcelSupport.close();
            return dt;
        }
        private static string OpenFileName()
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Excel files (*.xls,*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
                open.FilterIndex = 2;
                if (open.ShowDialog() == DialogResult.OK)
                    return open.FileName;
                return "";
            }
            catch { }
            return "";
        }
        public static void CreateComboItemDM(GridColumn column, string tenDM, string value, string display, string field)
        {
            try
            {
                if (tenDM != "")
                {
                    DataSet ds = DABase.getDatabase().LoadTable(tenDM);
                    HelpGridColumn.CotCombobox(column, ds, value, display, field);
                }
                else
                    column.ColumnEdit = null;
            }
            catch { }
        }
    }
}
