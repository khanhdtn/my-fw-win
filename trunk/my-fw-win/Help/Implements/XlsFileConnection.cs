using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public class XlsFileConnection
    {
        public  string constringNew = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";Data Source={0}";
       private  string constringOld = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1""";
       public string constring = "";
        private const int MAX_EXCEL_ROWS_COUNT = 65536;
        private XlsFileErrorCode excelErr;
        private OleDbConnection Con;
        private ApplicationClass app;
        string fileName = "";
        public XlsFileConnection(String FileName)
        {
            this.fileName = FileName;
            constring = string.Format(constringNew, FileName);
        }
        public XlsFileConnection()
        {
            app = new ApplicationClass();
        }
        public XlsFileErrorCode ConnectionError
        {
            get
            {
                return this.excelErr;
            }

        }
        public OleDbConnection Connection
        {
            get
            {
                return this.Con;
            }
        }
        public string DecimalSeparator
        {
            get
            {
                if (app == null) return null;
                if (app.UseSystemSeparators == true)
                {
                    return System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
                }
                else
                    return app.DecimalSeparator;
            }
        }
        public string ThousandSeparator
        {
            get
            {
                if (app == null) return null;
                if (app.UseSystemSeparators == true)
                {
                    return System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSeparator;
                }
                else
                    return app.ThousandsSeparator;
            }
        }

        // MỞ KẾT NỐI TỚI FILE CSDL
        int i = 1;
        public OleDbConnection Open()
        {
            Con = new OleDbConnection(constring);
            try
            {
                Con.Open();
            }
            catch (OleDbException e)
            {
                i++;
                if (i <= 2)
                {
                    constring = string.Format(constringOld, fileName);
                    Open();

                }
                else
                {
                    Con.Close();
                    excelErr = XlsFileErrorCode.OPEN_ERROR;
                    if (e.Errors.Count == 0) return null;
                    if (e.Errors[0].NativeError == XlsFileErrorCode.NOT_EXCEL_FORMATED.GetHashCode())
                        excelErr = XlsFileErrorCode.NOT_EXCEL_FORMATED;
                    else if (e.Errors[0].NativeError == XlsFileErrorCode.PASSWORD_PROTECTED.GetHashCode())
                        excelErr = XlsFileErrorCode.PASSWORD_PROTECTED;
                    return null;
                }
            }
            catch (Exception)
            {
                Con.Close();
                excelErr = XlsFileErrorCode.OPEN_ERROR;
                return null;
            }
            excelErr = XlsFileErrorCode.NONE;
            return Con;
        }

        public void Close()
        {
            if (Con != null) Con.Dispose();
         
        }

        /// <summary>
        /// Load dataset từ excel
        /// </summary>
        /// <param name="sheetname"></param>
        /// <param name="defaultColAndValue"></param>
        /// <param name="fieldNameAutoIncrement">Tên cột tạo số tự tăng, bắt đầu từ 2. Bỏ trống thì không tạo.</param>
        /// <returns></returns>
        public DataSet LoadDataSet(string sheetname, object[] defaultColAndValue)
        {
            OleDbDataAdapter Adapter;
            DataSet DS;

            string moreSelect = "";
            if (defaultColAndValue != null)
            {
                string fieldName = "";
                object defaultValue = null;
                for (int i = 0; i < defaultColAndValue.Length - 1; i += 2)
                {
                    fieldName = defaultColAndValue[i].ToString();
                    defaultValue = defaultColAndValue[i + 1];
                    if (defaultValue is String || defaultValue is DateTime)
                    {
                        moreSelect += ",'" + defaultValue + "' as [" + fieldName + "]";
                    }
                    else
                    {
                        moreSelect += "," + defaultValue + " as [" + fieldName + "]";
                    }
                }
            }
            string sql = "select ex.*" + moreSelect + " from [" + sheetname + "$] ex";

            Adapter = new OleDbDataAdapter(sql, Con);
            DS = new DataSet();

            try
            {

                DS.Tables.Add(new System.Data.DataTable(sheetname));
                DataColumn colIndex = new DataColumn(HelpExcel.xlsRowIndexField, typeof(System.Int32));
                colIndex.AutoIncrement = true;
                colIndex.AutoIncrementStep = 1;
                colIndex.AutoIncrementSeed = 2;
                DS.Tables[0].Columns.Add(colIndex);

                Adapter.Fill(DS, sheetname);
                return DS;
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static void ExportToExcel(DataSet dataSet, string FullFileName, Dictionary<string, XlsCellCheck> listValidation)
        {
            int sheetIndex = 0;
            ApplicationClass excelApp = new ApplicationClass();
            Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
            Worksheet excelSheet = null;
            Worksheet sheetDanhMuc = null;
            foreach (System.Data.DataTable dt in dataSet.Tables)
            {
                sheetDanhMuc = (Worksheet)excelWorkbook.Sheets.Add(
                  excelWorkbook.Sheets.get_Item(++sheetIndex),
                  Type.Missing, 1, XlSheetType.xlWorksheet);
                excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, XlSheetType.xlWorksheet);

                excelSheet.Name = dt.TableName;
                sheetDanhMuc.Name = "DM_KHONG_XOA_SUA";
                ((Range)excelSheet.Cells[1, 1]).EntireRow.Font.Bold = true;
                ((Range)excelSheet.Cells[2, 1]).EntireRow.Font.Bold = true;
                //((Range)excelSheet.Cells[2, 1]).EntireRow.Font.FontStyle = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold);

                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    excelSheet.Cells[1, col + 1] = dt.Columns[col].ColumnName;
                    excelSheet.Cells[2, col + 1] = dt.Rows[0][col].ToString();
                    if (listValidation.ContainsKey(dt.Columns[col].ColumnName))
                    {
                        XlsCellCheck validate = listValidation[dt.Columns[col].ColumnName];
                        Range xRange = excelSheet.get_Range(excelSheet.Cells[3, col + 1], excelSheet.Cells[MAX_EXCEL_ROWS_COUNT, col + 1]);
                        if (validate.ValidationType == XlDVType.xlValidateList &&
                            (validate.Value2 == null ||
                           (bool)validate.Value2 == false))
                        {
                            sheetDanhMuc.Cells[1, col + 1] = "Danh mục " + dt.Rows[0][col].ToString();
                            string[] names = validate.Value1.ToString().Split(',');
                            for (int i = 0; i < names.Length; i++)
                            {
                                sheetDanhMuc.Cells[i + 2, col + 1] = names[i];
                            }
                            Range rangeDM = sheetDanhMuc.get_Range(sheetDanhMuc.Cells[2, col + 1], sheetDanhMuc.Cells[names.Length + 2, col + 1]);
                            validate.Value1 = "=" + sheetDanhMuc.Name + "!" + rangeDM.get_Address(Type.Missing, Type.Missing, XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
                        }
                        xRange.Columns.Validation.Add(validate.ValidationType,
                            (validate.AlertType == null ? Type.Missing : validate.AlertType.Value),
                            (validate.OperatorValid == null ? Type.Missing : validate.OperatorValid.Value),
                            validate.Value1, validate.Value2);
                        xRange.Columns.Validation.ErrorTitle = validate.ErrTitle;
                        xRange.Columns.Validation.ErrorMessage = validate.ErrMesage;
                        xRange.Columns.Validation.IgnoreBlank = !validate.Require;
                        xRange.Columns.Validation.ShowError = validate.ShowError;
                    }
                }

                ((Range)excelSheet.Rows[1, Type.Missing]).Hidden = true;
                //((Range)excelSheet.Columns[Type.Missing, 10]).Validation.


                dt.Rows[0].Delete();
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        excelSheet.Cells[row + 3, col + 1] = dt.Rows[row].ItemArray[col];
                        ((Range)excelSheet.Rows[row + 3, Type.Missing]).Columns.EntireColumn.AutoFit();
                    }
                }
                sheetDanhMuc.Protect("protocolVN220987", true, true, false, false, false, true, false, false, false, false, false, false, false, false, false);


            }
            sheetDanhMuc.Visible = XlSheetVisibility.xlSheetHidden;
            excelSheet.Columns.AutoFit();
            XlFileFormat xlFormat = XlFileFormat.xlWorkbookNormal;

            System.IO.FileInfo fi = new System.IO.FileInfo(FullFileName);
            if (fi.Extension.ToLower() == ".xlsx") xlFormat = XlFileFormat.xlOpenXMLWorkbook;
            excelWorkbook.SaveAs(FullFileName, xlFormat, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
               true, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelWorkbook.Close(true, Type.Missing, Type.Missing);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheetDanhMuc);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelSheet = null;
            excelWorkbook = null;
            excelApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private static void ExportToExcel(DataSet dataSet, string FullFileName)
        {
            int sheetIndex = 0;
            ApplicationClass excelApp = new ApplicationClass();
            Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
            Worksheet excelSheet = null;
            foreach (System.Data.DataTable dt in dataSet.Tables)
            {
                excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, XlSheetType.xlWorksheet);

                excelSheet.Name = dt.TableName;
                ((Range)excelSheet.Cells[2, 1]).EntireRow.Font.Bold = true;
                ((Range)excelSheet.Rows[2, Type.Missing]).Font.FontStyle = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold);
                for (int col = 0; col < dt.Columns.Count; col++)
                {

                    excelSheet.Cells[1, col + 1] = dt.Columns[col].ColumnName;
                    excelSheet.Cells[2, col + 1] = dt.Rows[0][col].ToString();
                }

                ((Range)excelSheet.Rows[1, Type.Missing]).Hidden = true;


                dt.Rows[0].Delete();
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        excelSheet.Cells[row + 3, col + 1] = dt.Rows[row].ItemArray[col];
                        ((Range)excelSheet.Rows[row + 3, Type.Missing]).Columns.EntireColumn.AutoFit();
                    }
                }

            }
            XlFileFormat xlFormat = XlFileFormat.xlWorkbookNormal;

            System.IO.FileInfo fi = new System.IO.FileInfo(FullFileName);
            if (fi.Extension.ToLower() == ".xlsx") xlFormat = XlFileFormat.xlOpenXMLWorkbook;
            excelWorkbook.SaveAs(FullFileName, xlFormat, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
               true, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelWorkbook.Close(true, Type.Missing, Type.Missing);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelSheet = null;
            excelWorkbook = null;
            excelApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public static bool CreateExcelFile(DataSet dataSet, string FullFileName, Dictionary<string, XlsCellCheck> listValidation)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                DataSet demoDataSet = dataSet;
                if (listValidation != null)
                {
                    XlsFileConnection.ExportToExcel(demoDataSet, FullFileName, listValidation);
                }
                else
                {
                    XlsFileConnection.ExportToExcel(demoDataSet, FullFileName);
                }
                stopwatch.Stop();
                stopwatch.Reset();
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                if (ex.Message.Contains("00024500-0000-0000-C000-000000000046"))
                {
                    HelpMsgBox.ShowNotificationMessage("Máy chưa cài Microsoft Excel!");
                }
                else
                    HelpMsgBox.ShowNotificationMessage("Tạo file Excel bị lỗi");
                if (System.IO.File.Exists(FullFileName))
                {
                    System.IO.File.Delete(FullFileName);
                }
                return false;
            }
        }

        public static string SetStateOnExcel(string FileNameImport, string sheetName, DataSet dsError,
            int xlsRowCount, int xlsColumnCount)
        {
            string FullFileName="";
            ApplicationClass excelApp = null;
            Workbook excelWorkbook = null;
            Worksheet excelSheet = null;
            try
            {
             FullFileName = Path.GetDirectoryName(FileNameImport) + "//" +
                    Path.GetFileNameWithoutExtension(FileNameImport) + "_RESULT_" + DateTime.Now.ToString("yyMMddHHmmss") + Path.GetExtension(FileNameImport);

                File.Copy(FileNameImport, FullFileName, true);
                excelApp = new ApplicationClass();
                excelWorkbook = excelApp.Workbooks.Open(FullFileName, Type.Missing, false, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
                excelSheet = (Worksheet)excelWorkbook.Sheets[sheetName];
                excelWorkbook.CheckCompatibility = false;
                excelSheet.Cells[1, xlsColumnCount] = "TRANG_THAI_IMPORT";
                excelSheet.Cells[2, xlsColumnCount] = "Trạng thái import";
                Range sheet = excelSheet.get_Range(excelSheet.Cells[3, 1], excelSheet.Cells[xlsRowCount, xlsColumnCount]);
                //sheet.ClearComments();
                sheet.ClearNotes();
                sheet.ClearFormats();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                sheet = null;
                for (int index = 3; index <= xlsRowCount; index++)
                {
                    if (index == 2000)
                    {
                        int a = index;
                    }
                    Range cellErr = (Range)excelSheet.Cells[index, xlsColumnCount];
                    DataRow[] rows = dsError.Tables[0].Select(HelpExcel.xlsRowIndexField + "=" + index, HelpExcel.xlsColumnIndexField + " ASC");
                    if (rows.Length == 0)
                    {
                        cellErr.set_Value(Type.Missing, "Thành công");
                        continue;
                    }
                    else
                    {
                        cellErr.set_Value(Type.Missing, "Lỗi");
                        cellErr.Font.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    foreach (DataRow r in rows)
                    {
                        bool allrow = r[HelpExcel.xlsColumnIndexField] == DBNull.Value;
                        Range cell = (Range)excelSheet.Cells[index, (allrow ? Type.Missing : r[HelpExcel.xlsColumnIndexField])];
                        if (allrow)
                        {
                            cellErr.NoteText(r[HelpExcel.xlsErrorField], Type.Missing, Type.Missing);
                            excelSheet.get_Range(excelSheet.Cells[index, 1], excelSheet.Cells[index, xlsColumnCount]).Interior.Color =
                               ColorTranslator.ToOle(Color.FromArgb(250, 225, 170));


                        }
                        else
                        {
                            cell.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(250, 225, 170));
                            cell.NoteText(r[HelpExcel.xlsErrorField], Type.Missing, Type.Missing);
                        }
                    }
                }
                //Ko cho sửa trên file lỗi
                excelSheet.Protect("protocolVN220987", true, true, false, false, false, 
                    true, false, false, false, false, false, false, false, false, false);
                excelWorkbook.Save();
            }
            catch
            {
                try
                {
                    if (excelWorkbook != null)
                    {
                        excelWorkbook.Save();
                    }
                }
                catch
                {
                    
                }
               
            }          
            if (excelApp != null)
                excelApp.Quit();
            if (excelSheet != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
            if (excelWorkbook != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
            if (excelApp != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelSheet = null;
            excelWorkbook = null;
            excelApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return FullFileName;
        }
    }

    public enum XlsFileErrorCode
    {
        NONE = 0,
        NOT_EXCEL_FORMATED = -328602519,
        PASSWORD_PROTECTED = -327947149,
        OPEN_ERROR = 1
    }

    /// <summary>
    /// Các ràng buộc dữ liệu trên excel
    /// </summary>
    public class XlsCellCheck
    {
        private XlDVType validationType;
        private XlDVAlertStyle? alertType;
        private XlFormatConditionOperator? operatorValid;
        private string errTitle;
        private string errMesage;
        private Object value1;
        private Object value2;
        private Object value3;
        private bool require;
        private bool showError;

        /// <summary>
        /// Định nghĩa ràng buộc trên excel
        /// </summary>
        /// <param name="validationType">Loại ràng buộc hỗ trợ của excel</param>
        /// <param name="alertType">Loại thông báo ràng buộc (Warning: cảnh báo; Stop: bắt buộc, Information: Thông tin)</param>
        /// <param name="operatorValid">Phép toán kiểm tra (dạng 'List', 'Custom', 'AnyValue' để là null)</param>
        /// <param name="errTitle">Tiêu đề thông báo</param>
        /// <param name="errMesage">Nội dung câu thông báo nếu vi phạm ràng buộc</param>
        /// <param name="value1">Dữ liệu mốc1 để kiểm tra</param>
        /// <param name="value2">Dữ liệu mốc2 để kiểm tra, thường dùng trong loại ràng buộc so sánh, phạm vi 
        /// (riêng dạng 'List', để là fasle, hoặc null thì dữ liệu mốc 1 lấy từ db, là true thì các giá trị nhập cố định)  </param>
        /// <param name="value3">Dữ liệu mốc3, thường là số thập phân </param>
        /// <param name="require">Có kiểm tra nếu dữ liệu trống hay không?</param>
        /// <param name="showError">Có hiện thông báo lỗi trên excel hay không?</param>        
        public XlsCellCheck(XlDVType validationType, XlDVAlertStyle? alertType,
            XlFormatConditionOperator? operatorValid, string errTitle, string errMesage,
            Object value1, Object value2, Object value3, bool require, bool showError)
        {

            this.validationType = validationType;
            this.alertType = alertType;
            this.operatorValid = operatorValid;
            this.errTitle = errTitle;
            this.errMesage = errMesage;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.require = require;
            this.showError = showError;
        }
        public XlsCellCheck()
        {

        }
        public bool Require
        {
            get { return require; }
            set { require = value; }
        }
        public XlDVType ValidationType
        {
            get { return validationType; }
            set { validationType = value; }
        }
        public XlDVAlertStyle? AlertType
        {
            get { return alertType; }
            set { alertType = value; }
        }

        public XlFormatConditionOperator? OperatorValid
        {
            get { return operatorValid; }
            set { operatorValid = value; }
        }
        public string ErrTitle
        {
            get { return errTitle; }
            set { errTitle = value; }
        }

        public string ErrMesage
        {
            get { return errMesage; }
            set { errMesage = value; }
        }

        public Object Value1
        {
            get { return value1; }
            set { value1 = value; }
        }
        public Object Value2
        {
            get { return value2; }
            set { value2 = value; }
        }
        public Object Value3
        {
            get { return value3; }
            set { value3 = value; }
        }
        public bool ShowError
        {
            get { return showError; }
            set { showError = value; }
        }

        //PHUOCNT TODO
        public static void add(Dictionary<string, XlsCellCheck> xlsCellChecks, FieldNameCheck[] fieldNameChecks)
        {
            try
            {


                //Hàm này sẽ chuyển các FieldNameCheck thành dạng XlsCellCheck.

                //Khanhdtn: Hàm này chỉ chuyển tương đối, vì trong excel mỗi field chỉ được kiểm tra theo một nội dung kiểm tra (ko tính require),
                //còn FieldNameCheck thì có thể kiểm tra nhiều kiểu --> Lấy cái CheckType (ko tính Require) cuối cùng để chuyển";

                if (fieldNameChecks != null)
                {
                    string mss = "";
                    for (int i = 0; i < fieldNameChecks.Length; i++)
                    {
                        if (xlsCellChecks.ContainsKey(fieldNameChecks[i].FieldName)) continue;
                        //Chuyển qua được 
                        XlsCellCheck xlsCheck = new XlsCellCheck();
                        xlsCheck.AlertType = XlDVAlertStyle.xlValidAlertStop;
                        xlsCheck.ErrTitle = "Thông báo";
                        xlsCheck.ShowError = false;
                        xlsCheck.Require = false;
                        xlsCheck.ValidationType = XlDVType.xlValidateInputOnly;//Giá trị mặc định
                        for (int j = 0; j < fieldNameChecks[i].Types.Length; j++)
                        {
                            #region
                            mss = "";
                            switch (fieldNameChecks[i].Types[j])
                            {
                                case CheckType.DateALessB:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDate;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLess;
                                    mss = string.Format("ngày nhỏ hơn {1}", fieldNameChecks[i].Params[j]);
                                    xlsCheck.Value1 = fieldNameChecks[i].Params[j];
                                    break;
                                case CheckType.DateALessEqualB:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDate;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLessEqual;
                                    mss = string.Format("ngày nhỏ hơn hoặc bằng {1}", fieldNameChecks[i].Params[j]);
                                    xlsCheck.Value1 = fieldNameChecks[i].Params[j];
                                    break;
                                case CheckType.DecALessB:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLess;
                                    mss = string.Format("là số nhỏ hơn {1}", fieldNameChecks[i].Params[j]);
                                    xlsCheck.Value1 = fieldNameChecks[i].Params[j];
                                    //xlsCheck.Value3=? (số thập phân)
                                    break;
                                case CheckType.DecALessEqualB:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLessEqual;
                                    mss = string.Format("là số nhỏ hơn hoặc bằng {1}", fieldNameChecks[i].Params[j]);
                                    xlsCheck.Value1 = fieldNameChecks[i].Params[j];
                                    //xlsCheck.Value3=? (số thập phân)
                                    break;
                                case CheckType.DecGreater0:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreater;
                                    mss = "là số lớn hơn 0";
                                    xlsCheck.Require = true;
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = (fieldNameChecks[i].Params[j] != null ? HelpNumber.ParseInt32(fieldNameChecks[i].Params[j]) : 0);

                                    break;
                                case CheckType.DecGreaterEqual0:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreaterEqual;
                                    mss = "là số lớn hơn hoặc bằng 0";
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = (fieldNameChecks[i].Params[j] != null ? HelpNumber.ParseInt32(fieldNameChecks[i].Params[j]) : 0);
                                    

                                    break;
                                case CheckType.DecGreaterZero:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreater;
                                    mss = "là số lớn hơn 0";
                                    xlsCheck.Require = true;
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = (fieldNameChecks[i].Params[j] != null ? HelpNumber.ParseInt32(fieldNameChecks[i].Params[j]) : 0);
                                    

                                    break;
                                case CheckType.IntALessB:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLess;
                                    mss = "là số nhỏ hơn 0";
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = 0;
                                    break;
                                case CheckType.IntALessEqualB:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLessEqual;
                                    mss = "là số nhỏ hơn hoặc bằng 0";
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = 0;
                                    break;
                                case CheckType.IntGreater0:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreater;
                                    mss = "là số lớn hơn 0";
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = 0;
                                    xlsCheck.Require = true;
                                    break;
                                case CheckType.IntGreaterEqual0:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreaterEqual;
                                    mss = "là số lớn hơn hoặc bằng 0";
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = 0;
                                    break;
                                case CheckType.IntGreaterZero:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreater;
                                    mss = "là số lớn hơn 0";
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = 0;
                                    xlsCheck.Require = true;
                                    break;
                                case CheckType.OptionDate:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDate;
                                    xlsCheck.OperatorValid = null;
                                    mss = "là dạng ngày";
                                    break;
                                case CheckType.OptionEmail:                                  
                                    xlsCheck.ValidationType = XlDVType.xlValidateCustom;
                                    xlsCheck.OperatorValid = null;
                                    mss = "đúng dạng email";
                                    xlsCheck.Value2 = @"\S+@\S+\.\S+";
                                    //Chưa chuyển được kiểu tương ứng
                                    break;
                                case CheckType.OptionMaxLength:
                                    xlsCheck.ValidationType = xlsCheck.ValidationType | XlDVType.xlValidateTextLength;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLessEqual;
                                    mss = string.Format("có số ký tự nhỏ hơn hoặc bằng {0}", fieldNameChecks[i].Params[j]);
                                    xlsCheck.Value1 = fieldNameChecks[i].Params[j];
                                    break;
                                case CheckType.Required:
                                    xlsCheck.Require = true;
                                    mss = "";
                                    break;
                                case CheckType.RequireDate:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDate;
                                    xlsCheck.OperatorValid = null;
                                    mss = "là dạng ngày";
                                    xlsCheck.Require = true;
                                    break;
                                case CheckType.RequiredID:
                                    xlsCheck.ValidationType = XlDVType.xlValidateDecimal;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreater;
                                    mss = "là số lớn hơn 0";
                                    xlsCheck.Value1 = 0;
                                    xlsCheck.Value3 = 0;
                                    xlsCheck.Require = true;
                                    break;
                                case CheckType.RequireEmail:
                                    xlsCheck.Require = true;
                                    xlsCheck.ValidationType = XlDVType.xlValidateCustom;
                                    xlsCheck.OperatorValid = null;
                                    mss = "đúng dạng email";
                                    xlsCheck.Value2 = @"\S+@\S+\.\S+";
                                    break;
                                case CheckType.RequireMaxLength:
                                    xlsCheck.ValidationType = XlDVType.xlValidateTextLength;
                                    xlsCheck.OperatorValid = XlFormatConditionOperator.xlLessEqual;
                                    mss = string.Format("có số ký tự nhỏ hơn hoặc bằng {0}", fieldNameChecks[i].Params[j]);
                                    xlsCheck.Value1 = fieldNameChecks[i].Params[j];
                                    break;
                            }
                            #endregion

                            if (fieldNameChecks[i].Subject != null)
                            {
                                xlsCheck.ErrMesage = (string.Format("Vui lòng vào thông tin \"{0}\"", fieldNameChecks[i].Subject) + (xlsCheck.Require ? " và " : " ") + mss).Trim();
                            }
                            else
                            {
                                xlsCheck.ErrMesage = fieldNameChecks[i].ErrMsgs[j];
                            }
                        }
                        if (xlsCheck.Require && xlsCheck.ValidationType == XlDVType.xlValidateInputOnly)
                        {
                            xlsCheck.ValidationType = XlDVType.xlValidateTextLength;
                            xlsCheck.OperatorValid = XlFormatConditionOperator.xlGreater;
                            xlsCheck.Value1 = 0;
                        }
                        xlsCellChecks.Add(fieldNameChecks[i].FieldName, xlsCheck);
                    }
                }
            }
            catch (Exception ex)
            {

                PLException.AddException(ex);
                HelpMsgBox.ShowNotificationMessage("Thông tin cấu hình FieldNamCheck không đúng!");
            }
        }


    }
}
