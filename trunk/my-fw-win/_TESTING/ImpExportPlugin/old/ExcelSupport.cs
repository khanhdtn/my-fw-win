using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Plugin.OldImpExp
{
    class ExcelSupport
    {
        public static string constring = @"Provider=Microsoft.Jet.OLEDB.4.0" + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";Data Source=";
        public static string filenamepath = "";
        public static OleDbConnection Con;
        public static OleDbDataAdapter Adapter;
        public static DataSet DS;
        // MỞ KẾT NỐI TỚI FILE CSDL
        public static void open()
        {
            Con = new OleDbConnection(constring + filenamepath);
            try
            {
                Con.Open();
            }
            catch (Exception e) {PLMessageBox.ShowErrorMessage("Loi ket noi: " + e.ToString()); }
        }
        public static void close()
        {
            Con.Dispose();
        }
        // THỰC HIỆN TRUY VẤN LẤY VỀ 1 DATASET TỪ SHEETNAME ĐƯỢC LỰA CHỌN
        public static DataSet dataSet(string sheetname)
        {

            Adapter = new OleDbDataAdapter("select * from [" + sheetname + "$]", Con);
            DS = new DataSet();
            try
            {
                Adapter.Fill(DS, sheetname);

                return DS;
            }
            catch (Exception e) { PLMessageBox.ShowErrorMessage("Khong load duoc vao DataSet: " + e.ToString()); }
            return null;
        }
        public static List<string> GetSheetNames(string fileName)
        {
            ApplicationClass excelapp = new ApplicationClass();
            RecentFile re = excelapp.RecentFiles.Add(fileName);// nhan ve file vua chon de xu ly
            List<string> lstSheet = new List<string>();
            try
            {
                excelapp.Visible = false;
                Workbook excelWorkbook = re.Open();// mở ra và có thể truy xuất các sheet
                if (excelWorkbook.Sheets.Count > 0)
                {
                    Worksheet excelWorkSheet;
                    for (int i = 1; i <= excelWorkbook.Sheets.Count; i++)
                    {
                        excelWorkSheet = (Worksheet)excelWorkbook.Sheets[i];
                        lstSheet.Add(excelWorkSheet.Name);
                    }
                    excelWorkSheet = null;
                }
                excelWorkbook = null;
                re.Application.Quit();
                excelapp.Quit();// thoat khoi chuong trinh excel.exe trong he thong             

            }
            catch (Exception) { }
            return lstSheet;

        }
    }
}
