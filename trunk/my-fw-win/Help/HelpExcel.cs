using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using ProtocolVN.Framework.Core;
using System.Data;
using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Win;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using DevExpress.Utils;
using System.Drawing;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    public class HelpExcel
    {
        public const string FILTER_FILE = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx|All files (*.*)|*.*";
        public static string xlsRowIndexField = "INDEX_ROW_XLS";
        public static string xlsColumnIndexField = "INDEX_COLUMN_XLS";
        public static string xlsErrorField = "ERR_MESSAGE";     
        public static string errorField = "IS_ERROR";
        private static int xlsColumnCount;
        private static int xlsRowCount;


        #region Export
        public static string GetNameString(string tableName, string fieldName)
        {
            DataSet ds = HelpDB.getDatabase().LoadDataSet(string.Format("SELECT LIST(A.{0},',') FROM (SELECT {0} FROM {1} ORDER BY {0}) A", fieldName, tableName), tableName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// Tạo file excel mẫu cho import
        /// </summary>
        /// <param name="columns">Các cột có trong file excel</param>
        /// <param name="sheetName">Tên Sheet trên Excel, thường là tên bảng danh mục</param>
        /// <param name="listValidations">Các ràng buộc trên excel</param>
        /// <returns></returns>        
        public static bool ExportTemplateExcel(GridColumn[] columns, string tableNameDMMaster, Dictionary<string, XlsCellCheck> listExcelrules)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add(CreatDataTableToGrid(tableNameDMMaster, columns));
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = "My Documents://";
            save.Filter = FILTER_FILE;
            save.Title = HelpApplication.getTitleForm("Xuất tập tin excel mẫu");
            string fileBK = "";
            if (save.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.Application.DoEvents();
                if (ds != null)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            fileBK = Path.GetTempPath() + "\\" + Path.GetFileName(save.FileName);
                            File.Copy(save.FileName, fileBK,true);
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("The process cannot access the file"))//Tương đối
                            {
                                HelpMsgBox.ShowErrorMessage("Tập tin \"" + save.FileName + "\" đang mở \nhoặc đang được sử dụng bởi chương trình khác, không thể lưu chồng lên được!");
                            }
                            else
                            {
                                HelpMsgBox.ShowNotificationMessage("Tạo file excel bị lỗi!");
                            }
                            return false;
                        }
                    }
                    string fileName = save.Filter.Split('|')[save.FilterIndex * 2 - 1].Replace("*.*", "").TrimStart('*');
                    if (Path.GetExtension(save.FileName) == fileName)
                        fileName = save.FileName;
                    else
                        fileName = save.FileName + fileName;



                    if (XlsFileConnection.CreateExcelFile(ds, fileName, listExcelrules))
                    {
                        try
                        {
                            File.Delete(fileBK);
                        }
                        catch
                        {

                        }
                        HelpExeExt.OpenFile(HelpExeExt.ProcessName.EXCEL, fileName);
                    }
                    else if (File.Exists(fileBK))
                    {
                        File.Copy(fileBK, Path.GetDirectoryName(save.FileName) + "\\" + Path.GetFileName(fileBK));
                        try
                        {
                            File.Delete(fileBK);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Tạo file excel mẫu cho import
        /// </summary>
        /// <param name="columns">Các cột có trong file excel</param>
        /// <param name="sheetName">Tên Sheet trên Excel, thường là tên bảng danh mục</param>
        public static bool ExportTemplateExcel(GridColumn[] columns, string tableNameDMMaster)
        {
            Dictionary<string, XlsCellCheck> defaultRules = new Dictionary<string, XlsCellCheck>();
            defaultRules.Add("VISIBLE_BIT", new XlsCellCheck(XlDVType.xlValidateList,
              XlDVAlertStyle.xlValidAlertStop, null, "Thông báo", "Vui lòng chọn dữ liệu trong danh sách",
              "Y,N", true, null, false, false));
            return ExportTemplateExcel(columns, tableNameDMMaster, defaultRules);
        }
        /// <summary>
        /// Tạo file excel mẫu cho import
        /// </summary>
        /// <param name="columns">Các cột có trong file excel</param>
        /// <param name="sheetName">Tên Sheet trên Excel, thường là tên bảng danh mục</param>
        /// <param name="fieldDMs">Các field mà dữ liệu dạng danh mục</param>
        /// <param name="tableNameDMs">Các tên bảng danh mục tương ứng với tên field danh mục</param>       
        public static bool ExportTemplateExcel(GridColumn[] columns, string tableNameDMMaster, string[] fieldDMs, string[] tableNameDMs, string[] getFieldDMs)
        {
            if (fieldDMs == null && tableNameDMs == null) return ExportTemplateExcel(columns, tableNameDMMaster);
            Dictionary<string, XlsCellCheck> defaultRules = new Dictionary<string, XlsCellCheck>();
            for (int i = 0; i < fieldDMs.Length; i++)
            {
                if (fieldDMs.Length <= i) break;
                defaultRules.Add(fieldDMs[i], new XlsCellCheck(XlDVType.xlValidateList,
                    XlDVAlertStyle.xlValidAlertStop, null, "Thông báo", "Vui lòng chọn dữ liệu trong danh sách",
                    GetNameString(tableNameDMs[i], getFieldDMs[i]), null, null, false, false));
            }
            defaultRules.Add("VISIBLE_BIT", new XlsCellCheck(XlDVType.xlValidateList,
                XlDVAlertStyle.xlValidAlertStop, null, "Thông báo", "Vui lòng chọn dữ liệu trong danh sách",
                "Y,N", true, null, false, false));
            return ExportTemplateExcel(columns, tableNameDMMaster, defaultRules);
        }
        /// <summary>
        /// Tạo file excel mẫu cho import, (Phải bảo đảm các FieldName của các cột có trong bảng danh mục)
        /// </summary>
        /// <param name="gridView">GridView, tất cả các cột của gridview sẽ được export ngoại trừ các cột trong exceptColumns</param>
        /// <param name="sheetName">Tên Sheet trên Excel, thường là tên bảng danh mục</param>
        /// <param name="fieldDMs">Các field mà dữ liệu dạng danh mục, bằng null sẽ không có</param>
        /// <param name="tableNameDMs">Các tên bảng danh mục tương ứng với tên field danh mục</param> 
        /// <param name="exceptColumns">Các cột sẽ không import</param>
        public static bool ExportTemplateExcel(GridView gridView,
            string tableNameDMMaster,  string[] fieldDMs,
            string[] tableNameDMs, string[] getFieldDMs, 
            params GridColumn[] exceptColumns)
        {
            if (exceptColumns == null || exceptColumns.Length == 0)
            {
                return ExportTemplateExcel(GetColumArray(gridView), tableNameDMMaster, fieldDMs, tableNameDMs, getFieldDMs);

            }
            else
            {
                List<GridColumn> cols = new List<GridColumn>();
                List<GridColumn> exceptCols = new List<GridColumn>(exceptColumns);
                foreach (GridColumn col in gridView.Columns)
                {
                    if (!exceptCols.Contains(col)) cols.Add(col);
                }
                return ExportTemplateExcel(cols.ToArray(), tableNameDMMaster, fieldDMs, tableNameDMs, getFieldDMs);
            }
        }

        private static string GetNameString(string tableName)
        {
            return GetNameString(tableName, "NAME");
        }
        private static GridColumn[] GetColumArray(GridView grid)
        {
            if (grid.Columns == null) return null;
            GridColumn[] cols = new GridColumn[grid.Columns.Count];
            System.Collections.ArrayList.Adapter(grid.Columns).CopyTo(cols);
            return cols;
        }
        private static System.Data.DataTable CreatDataTableToGrid(string tableNameDMMaster, GridColumn[] columns)
        {
            System.Data.DataTable dt = new System.Data.DataTable(tableNameDMMaster);
            DataRow drCaption = dt.NewRow();
            GridColumn cotVisible = null;
            foreach (GridColumn col in columns)
            {
                if (col.FieldName.Trim().Length == 0) continue;
                if (col.FieldName == "VISIBLE_BIT")
                {
                    cotVisible = col;
                    continue;
                }
                dt.Columns.Add(col.FieldName);
                drCaption[col.FieldName] = col.Caption;
            }
            if (cotVisible != null)
            {
                dt.Columns.Add(cotVisible.FieldName);
                drCaption[cotVisible.FieldName] = cotVisible.Caption;
            }
            dt.Rows.Add(drCaption);
            return dt;
        }
        #endregion

        #region Import
        private static decimal ToDecimalFromExcel(DataRow Row, string Field, int soThapPhan)
        {
            decimal result = 0;
            if (Row[Field].GetType() != typeof(System.String))
            {
                result = HelpNumber.RoundDecimal(HelpNumber.ParseDecimal(Row[Field]), soThapPhan);
            }
            else
            {
                string values = Row[Field].ToString().Replace(".", "<@>!").Replace(",",
                    FrameworkParams.option.thousandSeparator).Replace("<@>!", FrameworkParams.option.decSeparator);
                result = HelpNumber.RoundDecimal(HelpNumber.ParseDecimal(values), soThapPhan);
            }
            Row[Field] = result;
            return result;
        }
        private static List<int> CheckIsDuplicate(DataSet dsError, System.Data.DataTable dtCheck, params string[] fieldPrimary)
        {
            List<int> duplicateIndex = new List<int>();
            if (fieldPrimary == null || fieldPrimary.Length == 0) return duplicateIndex;
            DataView view = dtCheck.Copy().DefaultView;
            view.Sort = string.Join(",", fieldPrimary);

            bool equal = false;
            for (int i = 1; i < view.Count; i++)
            {
                for (int j = 0; j < fieldPrimary.Length; j++)
                {
                    if (view[i][fieldPrimary[j]].Equals(view[i - 1][fieldPrimary[j]]))
                    {
                        equal = true;
                    }
                    else { equal = false; break; }
                }
                if (equal == true)
                {
                    duplicateIndex.Add(HelpNumber.ParseInt32(view[i][xlsRowIndexField]));
                    AddErrorRow(dsError, view[i][xlsRowIndexField], DBNull.Value, "Trùng dữ liệu với dòng " + view[i - 1][xlsRowIndexField]);
                }
            }
            return duplicateIndex;

        }
        private static bool UpdateGen(string genName, object GenID)
        {

            DbTransaction dbTrans = null;
            DatabaseFB db = null;
            string str = "set generator " + genName + " to " + GenID + " ";
            try
            {
                db = HelpDB.getDatabase();
                dbTrans = PLTransaction.BeginTrans(db);
                DbCommand cmd = db.GetSQLStringCommand(str);
                db.ExecuteNonQuery(cmd, dbTrans);
                db.CommitTransaction(dbTrans);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                db.RollbackTransaction(dbTrans);
                return false;
            }
            return true;
        }
        private static DataSet CreatDsErrorImport()
        {
            DataSet ds = new DataSet("Importing");
            System.Data.DataTable dt = new System.Data.DataTable("Error");
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn(xlsRowIndexField,typeof(Int32)),               
                new DataColumn(xlsColumnIndexField,typeof(Int32)),
                new DataColumn(xlsErrorField)             
            });
            ds.Tables.Add(dt);
            return ds;
        }
        private static void AddErrorRow(DataSet dsErrorImport, object rowIndex, object columnName, object message)
        {
            dsErrorImport.Tables[0].Rows.Add(rowIndex, columnName, message);
        }
        private static bool ValidateConnection(XlsFileConnection conn, string fileNamePath)
        {
            if (conn.Open() == null)
            {
                if (conn.ConnectionError == XlsFileErrorCode.NOT_EXCEL_FORMATED)
                {
                    HelpMsgBox.ShowNotificationMessage("Tập tin \"" + fileNamePath + "\" không phải định dạng excel!");
                }
                else if (conn.ConnectionError == XlsFileErrorCode.PASSWORD_PROTECTED)
                {
                    HelpMsgBox.ShowNotificationMessage("Tập tin \"" + new FileInfo(fileNamePath).Name + "\" đã được bảo vệ bằng mật khẩu!");
                }
                else
                {
                    HelpMsgBox.ShowNotificationMessage("Truy cập vào tập tin \"" + fileNamePath + "\" không thành công!");
                }
                return false;
            }
            return true;
        }        
     
        public static DataSet GetDataSetFromExcel(GridView grid, string IDField, 
            string[] fieldDMs, string[] tableNameDMs, string[] idFieldDMs,string[] getFieldDMs,
            Dictionary<string, XlsCellCheck> excelRules, string[] fieldsToCheckDuplicate, bool writeErorToFile, 
            out int tongSoDongExcel, out DataSet datasetError, out string fileError)
        {
            WaitDialogForm wait = null;
            datasetError = null;
            fileError = "";
            tongSoDongExcel = 0;
            try
            {

                OpenFileDialog oP = new OpenFileDialog();
                oP.InitialDirectory = "My Documents://";
                oP.Filter = FILTER_FILE;
                oP.Title = HelpApplication.getTitleForm("Import dữ liệu từ tập tin excel");
                if (oP.ShowDialog() == DialogResult.Cancel) return new DataSet();
                System.Windows.Forms.Application.DoEvents();
                string filenamepath = oP.FileName;
                wait = new WaitDialogForm("Đang xử lý..", "Import dữ liệu từ excel!", new Size(250, 50));

                System.Data.DataTable dtDMMaster = grid.GridControl.DataSource as System.Data.DataTable;
                DataSet dsError = CreatDsErrorImport();
                // DataSet ds = ExcuteImport(oP.FileName, dsErrorr, dtSource, excelRules, fieldsAndCaptionToCheckDuplicate);

                #region Excute Import
                string tempFilePath = Path.GetTempFileName();
                File.Copy(filenamepath, tempFilePath, true);
                FileInfo inf = new FileInfo(tempFilePath);
                if (inf.IsReadOnly) inf.IsReadOnly = false;

                wait.SetCaption("Đang kết nối tập tin excel...");
                XlsFileConnection conn = new XlsFileConnection(tempFilePath);
                if (ValidateConnection(conn, filenamepath) == false) return null;
                DataSet ds = new DataSet();

                ds = conn.LoadDataSet(dtDMMaster.TableName, null);
                if (ds == null)
                {
                    wait.Close();
                    HelpMsgBox.ShowNotificationMessage("Tập tin excel có cấu trúc không đúng mẫu, vui lòng kiểm tra lại");
                    return null;
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//Loại bỏ dòng rỗng
                {
                    xlsRowCount = ds.Tables[0].Rows.Count + 1;
                    string conditon = "";
                    List<DataColumn> colDelete = new List<DataColumn>();
                    System.Data.DataTable dtStruct = ds.Tables[0].Clone();
                    foreach (DataColumn col in dtStruct.Columns)
                    {
                        if (col.ColumnName == xlsRowIndexField) continue;
                        if (!dtDMMaster.Columns.Contains(col.ColumnName))
                        {
                            ds.Tables[0].Columns.Remove(col.ColumnName);
                            continue;
                        }
                        conditon += col.ColumnName + " IS NOT NULL OR ";
                    }
                    xlsColumnCount = ds.Tables[0].Columns.Count;
                    conditon = conditon.Substring(0, conditon.LastIndexOf("OR"));
                    ds.Tables[0].DefaultView.RowFilter = conditon;
                    System.Data.DataTable dt = ds.Tables[0].DefaultView.ToTable();
                    ds.Tables.Clear();
                    ds.Tables.Add(dt);
                }
                conn.Close();
                System.Windows.Forms.Application.DoEvents();


                #region GetValidDatset
                System.Data.DataTable dtExcel;
                dtExcel = ds.Tables[0];//.Copy();
                dtExcel.Rows.RemoveAt(0);
                if (dtExcel.Rows.Count <= 0)
                {
                    wait.Close();
                    HelpMsgBox.ShowNotificationMessage("Tập tin import \"" + filenamepath + "\" chưa được nhập liệu!");
                    return null;
                }
                DataSet sourceDataSet = HelpDB.getDatabase().LoadDataSet("SELECT * FROM " + dtDMMaster.TableName, dtDMMaster.TableName);
                DataSet newDataSet = sourceDataSet.Clone();
                #region Check dubpllicate
                List<int> dupplicateIndexs = CheckIsDuplicate(dsError, dtExcel, fieldsToCheckDuplicate);
                #endregion




                Dictionary<string, System.Data.DataTable> listDMTables = new Dictionary<string, System.Data.DataTable>();
                Dictionary<string, string> listDMIDFields = new Dictionary<string, string>();
                if (fieldDMs != null && tableNameDMs != null && idFieldDMs != null && getFieldDMs != null)
                {
                    for (int x = 0; x < fieldDMs.Length; x++)
                    {
                        DataSet dsDM = HelpDB.getDatabase().LoadDataSet("SELECT * FROM " + tableNameDMs[x], tableNameDMs[x]);
                        if (dsDM != null && dsDM.Tables.Count > 0)
                        {
                            dsDM.Tables[0].PrimaryKey = new DataColumn[] { dsDM.Tables[0].Columns[getFieldDMs[x]] };
                            listDMTables.Add(fieldDMs[x], dsDM.Tables[0]);
                        }
                        listDMIDFields.Add(fieldDMs[x], idFieldDMs[x]);

                    }
                }


                List<long> ids = HelpDB.getDatabase().GetID(HelpGen.G_FW_DM_ID, dtExcel.Rows.Count);
                //long id = HelpDB.getDatabase().GetID(HelpGen.G_FW_DM_ID);
                //UpdateGen(HelpGen.G_FW_DM_ID, id + dtExcel.Rows.Count);
                wait.SetCaption("Đang xử lý dữ liệu...");
                if (excelRules == null)
                    excelRules = new Dictionary<string, XlsCellCheck>();
                if (excelRules.ContainsKey("VISIBLE_BIT") == false)
                {
                    excelRules.Add("VISIBLE_BIT", new XlsCellCheck(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop, null, "Thông báo", "Vui lòng nhập dữ liệu có trong danh sách!",
                        "Y,N", true, null, false, false));
                }
                int idStep = 0;
                foreach (DataRow row in dtExcel.Rows)
                {
                    System.Windows.Forms.Application.DoEvents();
                    if (fieldsToCheckDuplicate != null && fieldsToCheckDuplicate.Length > 0)
                    {
                        if (dupplicateIndexs.Contains(HelpNumber.ParseInt32(row[xlsRowIndexField])))
                        {
                            continue;//Dòng trùng trên excel

                        }
                        if (HelpDataSet.FindRow(fieldsToCheckDuplicate, row, sourceDataSet.Tables[0]) != null)
                        {
                            AddErrorRow(dsError, row[xlsRowIndexField], DBNull.Value, "Đã tồn tại trong cơ sở dữ liệu!");
                            continue;
                        }
                    }
                    DataRow newRow = newDataSet.Tables[0].NewRow();
                    bool isRowError = false;
                    bool isCellError = false;
                    foreach (DataColumn col in newDataSet.Tables[0].Columns)
                    {
                        isCellError = false;
                        if (dtExcel.Columns.Contains(col.ColumnName) == false) continue;
                        if (col.ColumnName == IDField)
                            continue;
                        if (excelRules != null && excelRules.ContainsKey(col.ColumnName))
                        {

                            XlsCellCheck rule = excelRules[col.ColumnName];
                            if (row[col.ColumnName].ToString().Trim() == "") row[col.ColumnName] = DBNull.Value;
                            if (row[col.ColumnName] == DBNull.Value)
                            {
                                if (rule.Require)
                                {
                                    AddErrorRow(dsError, row[xlsRowIndexField], dtExcel.Columns.IndexOf(col.ColumnName), rule.ErrMesage);
                                    isRowError = true;
                                    isCellError = true;
                                }
                                continue;
                            }
                            #region Check rule
                            switch (rule.ValidationType)
                            {
                                case XlDVType.xlValidateDate:
                                    #region
                                    DateTime testDt = new DateTime();
                                    if (!DateTime.TryParse(row[col.ColumnName].ToString(), out testDt))
                                    {
                                        isCellError = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (rule.OperatorValid != null)
                                        {
                                            switch (rule.OperatorValid.Value)
                                            {
                                                case XlFormatConditionOperator.xlBetween:
                                                    if (HelpIsCheck.IsALessB(testDt, DateTime.Parse(rule.Value1.ToString()))
                                                        || HelpIsCheck.IsALessB(DateTime.Parse(rule.Value2.ToString()), testDt))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlEqual:
                                                    if (HelpIsCheck.IsALessB(testDt, DateTime.Parse(rule.Value1.ToString())) ||
                                                       HelpIsCheck.IsALessB(DateTime.Parse(rule.Value1.ToString()), testDt))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlGreater:
                                                    if (HelpIsCheck.IsALessEqualB(testDt, DateTime.Parse(rule.Value1.ToString())))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlGreaterEqual:
                                                    if (HelpIsCheck.IsALessB(testDt, DateTime.Parse(rule.Value1.ToString())))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlLess:
                                                    if (HelpIsCheck.IsALessB(testDt, DateTime.Parse(rule.Value1.ToString())) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlLessEqual:
                                                    if (HelpIsCheck.IsALessEqualB(testDt, DateTime.Parse(rule.Value1.ToString())) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlNotBetween:
                                                    if (HelpIsCheck.IsALessB(testDt, DateTime.Parse(rule.Value1.ToString())) == false
                                                        && HelpIsCheck.IsALessB(DateTime.Parse(rule.Value2.ToString()), testDt) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlNotEqual:
                                                    if (HelpIsCheck.IsALessB(testDt, DateTime.Parse(rule.Value1.ToString())) == false &&
                                                      HelpIsCheck.IsALessB(DateTime.Parse(rule.Value1.ToString()), testDt) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;

                                            }
                                        }
                                        if (isCellError == false) row[col.ColumnName] = testDt;
                                    }
                                    #endregion
                                    break;
                                case XlDVType.xlValidateDecimal:
                                    #region
                                    decimal testDecimal = 0;
                                    if (!Decimal.TryParse(row[col.ColumnName].ToString(), out testDecimal))
                                    {
                                        isCellError = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (rule.OperatorValid != null)
                                        {
                                            decimal checkD = ToDecimalFromExcel(row, col.ColumnName, HelpNumber.ParseInt32(rule.Value3));
                                            switch (rule.OperatorValid.Value)
                                            {
                                                case XlFormatConditionOperator.xlBetween:
                                                    if (HelpIsCheck.IsALessB(checkD, HelpNumber.ParseDecimal(rule.Value1))
                                                        || HelpIsCheck.IsALessB(HelpNumber.ParseDecimal(rule.Value2), checkD))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlEqual:
                                                    if (HelpIsCheck.IsALessB(checkD, HelpNumber.ParseDecimal(rule.Value1)) ||
                                                       HelpIsCheck.IsALessB(HelpNumber.ParseDecimal(rule.Value1), checkD))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlGreater:
                                                    if (HelpIsCheck.IsALessEqualB(checkD, HelpNumber.ParseDecimal(rule.Value1)))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlGreaterEqual:
                                                    if (HelpIsCheck.IsALessB(checkD, HelpNumber.ParseDecimal(rule.Value1)))
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlLess:
                                                    if (HelpIsCheck.IsALessB(checkD, HelpNumber.ParseDecimal(rule.Value1)) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlLessEqual:
                                                    if (HelpIsCheck.IsALessEqualB(checkD, HelpNumber.ParseDecimal(rule.Value1)) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlNotBetween:
                                                    if (HelpIsCheck.IsALessB(checkD, HelpNumber.ParseDecimal(rule.Value1)) == false
                                                        && HelpIsCheck.IsALessB(HelpNumber.ParseDecimal(rule.Value2), checkD) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                                case XlFormatConditionOperator.xlNotEqual:
                                                    if (HelpIsCheck.IsALessB(checkD, HelpNumber.ParseDecimal(rule.Value1)) == false &&
                                                      HelpIsCheck.IsALessB(HelpNumber.ParseDecimal(rule.Value1), checkD) == false)
                                                    {
                                                        isCellError = true;
                                                    }
                                                    break;
                                            }
                                        }

                                    }
                                    #endregion
                                    break;
                                case XlDVType.xlValidateList:
                                    List<string> listData = new List<string>(rule.Value1.ToString().Split(','));
                                    if (!listData.Contains(row[col.ColumnName].ToString()))
                                    {
                                        isCellError = true;
                                    }
                                    break;
                                case XlDVType.xlValidateTextLength:
                                    #region
                                    if (rule.OperatorValid != null)
                                    {
                                        int testLength = row[col.ColumnName].ToString().Trim().Length;
                                        switch (rule.OperatorValid.Value)
                                        {
                                            case XlFormatConditionOperator.xlBetween:
                                                if (HelpIsCheck.IsALessB(testLength, HelpNumber.ParseInt32(rule.Value1))
                                                    || HelpIsCheck.IsALessB(HelpNumber.ParseInt32(rule.Value2), testLength))
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                            case XlFormatConditionOperator.xlEqual:
                                                if (HelpIsCheck.IsALessB(testLength, HelpNumber.ParseInt32(rule.Value1)) ||
                                                   HelpIsCheck.IsALessB(HelpNumber.ParseInt32(rule.Value1), testLength))
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                            case XlFormatConditionOperator.xlGreater:
                                                if (HelpIsCheck.IsALessEqualB(testLength, HelpNumber.ParseInt32(rule.Value1)))
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                            case XlFormatConditionOperator.xlGreaterEqual:
                                                if (HelpIsCheck.IsALessB(testLength, HelpNumber.ParseInt32(rule.Value1)))
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                            case XlFormatConditionOperator.xlLess:
                                                if (HelpIsCheck.IsALessB(testLength, HelpNumber.ParseInt32(rule.Value1)) == false)
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                            case XlFormatConditionOperator.xlLessEqual:
                                                if (HelpIsCheck.IsALessEqualB(testLength, HelpNumber.ParseInt32(rule.Value1)) == false)
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                            case XlFormatConditionOperator.xlNotBetween:
                                                if (HelpIsCheck.IsALessB(testLength, HelpNumber.ParseInt32(rule.Value1)) == false
                                                    && HelpIsCheck.IsALessB(HelpNumber.ParseInt32(rule.Value2), testLength) == false)
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                            case XlFormatConditionOperator.xlNotEqual:
                                                if (HelpIsCheck.IsALessB(testLength, HelpNumber.ParseInt32(rule.Value1)) == false &&
                                                  HelpIsCheck.IsALessB(HelpNumber.ParseInt32(rule.Value1), testLength) == false)
                                                {
                                                    isCellError = true;
                                                }
                                                break;
                                        }
                                    }
                                    if (isCellError == false) row[col.ColumnName] = row[col.ColumnName].ToString().Trim();
                                    break;
                                    #endregion
                                case XlDVType.xlValidateCustom:
                                    if (rule.Value2 != null && rule.Value2.ToString() != "")
                                    {
                                        System.Text.RegularExpressions.Regex regex =
                                        new System.Text.RegularExpressions.Regex(rule.Value2.ToString());
                                        if (!regex.IsMatch(row[col.ColumnName].ToString()))
                                        {
                                            isCellError = true;
                                        }
                                    }
                                    //Chừa chỗ
                                    break;
                                case XlDVType.xlValidateTime:
                                    //Chừa chỗ
                                    break;
                                case XlDVType.xlValidateWholeNumber:
                                    //Chừa chỗ
                                    break;
                                case XlDVType.xlValidateInputOnly:
                                    //Chừa chỗ
                                    break;
                            }
                            #endregion
                            if (isCellError == true)
                            {
                                AddErrorRow(dsError, row[xlsRowIndexField], dtExcel.Columns.IndexOf(col.ColumnName), rule.ErrMesage);
                                isRowError = true;
                                continue;
                            }
                        }
                        //Xác định đây có phải là cột DM, nếu phải thì lấy ID tương ứng với giá trị đó
                        if (listDMTables.ContainsKey(col.ColumnName))
                        {
                            System.Data.DataTable dtDM = listDMTables[col.ColumnName];
                            DataRow rowDM = dtDM.Rows.Find(row[col.ColumnName]);
                            if (rowDM != null)
                            {
                                newRow[col.ColumnName] = HelpNumber.ParseInt64(rowDM[listDMIDFields[col.ColumnName]]);
                            }
                        }
                        else
                        {
                            newRow[col.ColumnName] = Convert.ChangeType(row[col.ColumnName], col.DataType);
                        }
                    }
                    if (isRowError == false)
                    {
                        newRow[IDField] = ids[idStep];
                        idStep++;
                        newDataSet.Tables[0].Rows.Add(newRow);
                    }
                }
                if (File.Exists(tempFilePath))
                {
                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch
                    {
                    }
                }

                #endregion

                #endregion

                if (writeErorToFile)
                {
                    wait.SetCaption("Đang kiểm tra kết quả import...");
                    fileError = XlsFileConnection.SetStateOnExcel(oP.FileName, dtDMMaster.TableName, dsError, xlsRowCount, xlsColumnCount);
                }
                wait.Close();
                tongSoDongExcel = dtExcel.Rows.Count;
                datasetError = dsError;
                return newDataSet;
            }
            catch (Exception ex)
            {
                if (wait != null) wait.Close();
                HelpMsgBox.ShowNotificationMessage("Tập tin excel có cấu trúc không đúng mẫu, vui lòng kiểm tra lại.");
                PLException.AddException(ex);
                return null;
            }
            finally
            {
                if (wait != null) wait.Close();
            }

        }

        public static bool ImportToDanhMuc(GridView grid, string IDField,
            string[] fieldDMs, string[] tableNameDMs, string[] idFieldDMs, string[] getFieldDMs,
            Dictionary<string, XlsCellCheck> excelRules, string[] fieldsToCheckDuplicate)
        {
            try
            {
                int tongSoDong=0;
                DataSet dsError=null;
                string fileError="";
                DataSet newDs = GetDataSetFromExcel(grid, IDField, fieldDMs,
                    tableNameDMs, idFieldDMs, getFieldDMs, excelRules, fieldsToCheckDuplicate, true, out tongSoDong, out dsError, out fileError);
                if (newDs != null && newDs.Tables.Count > 0 )
                {

                    if (HelpDB.getDatabase().UpdateDataSet(newDs))
                    {
                        DialogResult dr = HelpMsgBox.ShowConfirmMessage("Import được " + newDs.Tables[0].Rows.Count + " trên tổng số " + tongSoDong+ " dòng có dữ liệu! \n Bạn có muốn xem chi tiết kết quả import ?");
                        if (dr == DialogResult.Yes)
                        {
                            HelpFile.OpenFile(fileError);
                        }
                        return true;
                    }
                    else
                    {
                        HelpMsgBox.ShowNotificationMessage("Lưu dữ liệu không thàng công!");
                        try
                        {
                            File.Delete(fileError);
                        }
                        catch
                        {
                        }
                    }                  

                }

            }
            catch
            {
               
            }
            return false;
        }
            
        #endregion
          
    
    }
}
