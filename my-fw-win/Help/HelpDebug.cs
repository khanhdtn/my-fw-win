using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using ProtocolVN.Framework.Win;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ProtocolVN.Framework.Win
{
    

    /// <summary>
    /// Hiển thị 1 số thông tin giúp cho debug dễ hơn
    /// Không tham gia trong việc phát triển ứng dụng
    /// </summary>
    public class HelpDebug
    {
        /// <summary>Là tham số cho phép mình bật các chức năng hỗ trợ phát triển.
        /// </summary>
        //public static bool IsDebug = false;        
        public static IDBHelpDebug rdbms;

        #region 1. Xem thông tin trong DataSet
        public static void ShowData(DataSet ds)
        {
            ShowData(ds, "Debug");
        }
        /// <summary>Hiển thị một hộp thoại cho thấy nội dung của data set bao 
        /// gồm trạng thái của từng dòng. Dùng để kiểm tra khi có lỗi trong việc
        /// cập nhật DataSet
        /// </summary>
        public static void ShowData(DataSet ds, string Msg)
        {
            bool IsDebug = FrameworkParams.isSupportDeveloper;
            if (IsDebug)
            {
                frmGridInfo frm = new frmGridInfo();
                ds.Tables[0].Columns.Add("RowState", System.Type.GetType("System.String"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["RowState"] = ds.Tables[0].Rows[i].RowState.ToString();
                }
                frm.InitData(ds, Msg);
                frm.viewDebug.Columns["RowState"].VisibleIndex = 1;
                frm.TopLevel = true;
                //frm.TopMost = true;
                ProtocolForm.ShowDialog(FrameworkParams.MainForm, frm);
            }
        }

        /// <summary>Hiển thị một hộp thoại cho thấy nội dung của data set bao 
        /// gồm trạng thái của từng dòng. Dùng để kiểm tra khi có lỗi trong việc
        /// cập nhật DataSet và không làm biến đổi ds
        /// </summary>
        public static void ShowDataExt(DataSet ds, string Msg)
        {
            bool IsDebug = FrameworkParams.isSupportDeveloper;
            DataSet dsClone = ds.Clone();
            if (IsDebug)
            {
                frmGridInfo frm = new frmGridInfo();
                dsClone.Tables[0].Columns.Add("RowState", System.Type.GetType("System.String"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow newRow = dsClone.Tables[0].NewRow();
                    Object[] itemArray = ds.Tables[0].Rows[i].ItemArray;
                    for (int j = 0; j < itemArray.Length; j++)
                    {
                        newRow[j] = itemArray[j];
                    }
                    dsClone.Tables[0].Rows.Add(newRow);
                    newRow[newRow.ItemArray.Length-1] = ds.Tables[0].Rows[i].RowState.ToString();                    
                }
                frm.InitData(dsClone, Msg);
                frm.viewDebug.Columns["RowState"].VisibleIndex = 1;
                //frm.TopMost = true;
                frm.TopLevel = true;
                ProtocolForm.ShowDialog(FrameworkParams.MainForm, frm);
            }
        }
        #endregion

        #region 2. Xem thông tin trong IDataReader IDReader
        public static void ShowStructure(IDataReader IDReader)
        {
            ShowStructure(IDReader, "Debug");
        }
        /// <summary>Hiển thị lưới kiểu dữ liệu C# và DBType
        /// Dùng để kiểm tra xem 
        /// </summary>
        public static void ShowStructure(IDataReader IDReader, string Msg)
        {
            bool IsDebug = FrameworkParams.isSupportDeveloper;
            if (IsDebug)
            {
                frmGridInfo frm = new frmGridInfo();

                DataSet ds = new DataSet();
                DataTable table = new DataTable("reader");
                table.Columns.Add("ID", System.Type.GetType("System.String"));
                table.Columns.Add("Name", System.Type.GetType("System.String"));
                table.Columns.Add("DBType", System.Type.GetType("System.String"));
                table.Columns.Add("C#Type", System.Type.GetType("System.String"));
                table.Columns.Add("Value", System.Type.GetType("System.String"));
                ds.Tables.Add(table);

                int j = 1;
                while (IDReader.Read())
                {
                    for (int i = 0; i < IDReader.FieldCount; i++)
                    {
                        object[] row = { 
                            j.ToString() + "." + i.ToString(), 
                            IDReader.GetName(i), 
                            IDReader.GetDataTypeName(i), 
                            IDReader.GetValue(i).GetType().ToString(), 
                            IDReader.GetValue(i).ToString() 
                        };
                        ds.Tables[0].Rows.Add(row);
                    }
                    //BLANK ROW
                    object[] blankrow = { 
                            "-----", 
                            "-----", 
                            "-----", 
                            "-----", 
                            "-----" 
                    };
                    ds.Tables[0].Rows.Add(blankrow);

                    j++;
                }
                IDReader.Close();
                frm.InitData(ds, Msg);
                //frm.TopMost = true;
                frm.TopLevel = true;
                ProtocolForm.ShowDialog(FrameworkParams.MainForm, frm);
            }
        }
        #endregion

        #region 3. Xem thông tin khi thực hiện một QueryBuilder
        public static void ShowQueryBuilder(QueryBuilder filter)
        {
            ShowQueryBuilder(filter, "Debug");
        }
        public static void ShowQueryBuilder(QueryBuilder filter, string Msg)
        {
            bool IsDebug = FrameworkParams.isSupportDeveloper;
            if (IsDebug)
            {
                frmGridInfo frm = new frmGridInfo();
                DatabaseFB db = DABase.getDatabase();
                DataSet ds = db.LoadDataSet(filter, "AAA");

                frm.InitData(ds, Msg);
                ProtocolForm.ShowDialog(FrameworkParams.MainForm, frm);
            }
        }        
        #endregion

        #region 4.Xem nội dung một chuỗi
        public static void ShowString(string Content)
        {
            ShowString(Content, "");
        }
        public static void ShowString(string Content, string Msg)
        {
            bool IsDebug = FrameworkParams.isSupportDeveloper;
            if (IsDebug)
            {
                frmTextInfo frm = new frmTextInfo();
                frm.InitData(Content, Msg);
                frm.TopLevel = true;
                //frm.TopMost = true;
                ProtocolForm.ShowDialog(FrameworkParams.MainForm, frm);
            }
        }
        #endregion

        #region 5. Xem nội dung của exception

        public static string GetLastestFbException(List<Exception> listExp)
        {
            return rdbms.GetLastestFbException(listExp);
        }
        public static string GetUserErrorMsg(List<Exception> listExp){
            return rdbms.GetUserErrorMsg(listExp);
        }
        public static void ShowExceptionInfo(List<Exception> listExp, string Msg){
            rdbms.ShowExceptionInfo(listExp, Msg);
        }
        public static void ShowExceptionInfo(){
            rdbms.ShowExceptionInfo();
        }
        public static void ShowExceptionInfo(List<Exception> listExp)
        {
            rdbms.ShowExceptionInfo(listExp);
        }
        #endregion

        #region Hỗ trợ xem tình trạng trên tiêu đề form.
        public static void SetTitle(Form frm, FormStatus status)
        {
            bool IsDebug = FrameworkParams.isSupportDeveloper;
            if (IsDebug == false)
            {
                switch (status)
                {
                    case FormStatus.NO:
                        frm.Text += " NO ";//+ frm.Text;
                        return;
                    case FormStatus.NC:
                        frm.Text += " NC ";//+ frm.Text;
                        return;
                    case FormStatus.OK_DEV:
                        frm.Text += " OK_DEV ";//+ frm.Text;
                        return;
                    case FormStatus.OK_TEST:
                        frm.Text += " OK_TEST ";//+ frm.Text;
                        return;
                    case FormStatus.FIX:
                        frm.Text += " FIX ";//+ frm.Text;
                        return;
                    case FormStatus.FIXED:
                        frm.Text += " FIXED ";//+ frm.Text;
                        return;
                }
            }
        }
        public static String UpdateTitle(String title, FormStatus status)
        {
            bool IsDebug = FrameworkParams.isSupportDeveloper;
            if (IsDebug == false)
            {
                switch (status)
                {
                    case FormStatus.NO:
                        title += " NO ";//+ frm.Text;
                        break;
                    case FormStatus.NC:
                        title += " NC ";//+ frm.Text;
                        break;
                    case FormStatus.OK_DEV:
                        title += " OK_DEV ";//+ frm.Text;
                        break;
                    case FormStatus.OK_TEST:
                        title += " OK_TEST ";//+ frm.Text;
                        break;
                    case FormStatus.FIX:
                        title += " FIX ";//+ frm.Text;
                        break;
                    case FormStatus.FIXED:
                        title += " FIXED ";//+ frm.Text;
                        break;
                }
            }
            return title;
        }
        #endregion

        #region Mục đích show thông tin khác nhau tùy vào view là Release hay Trial
        public static void ShowMessage(string Info)
        {
            bool IsRelease = !FrameworkParams.IsTrial;
            if (IsRelease)
            {
                //PHUOCNC: Nên cấu hình thành Log File để theo dõi
                HelpMsgBox.ShowNotificationMessage("Vui lòng liên hệ bộ phận phát triển của PROTOCOLVN");
                return;
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage("Bạn đã cấu hình sai hoặc PL-FW chưa hỗ trợ. Xin liên hệ quản lý dự án.\nThông tin thêm: " + Info);
                return;
            }
        }
        public static void ShowMessage(object obj, string Info)
        {
            bool IsRelease = !FrameworkParams.IsTrial;
            if (IsRelease)
            {
                //PHUOCNC: Nên cấu hình thành Log File để theo dõi
                HelpMsgBox.ShowNotificationMessage("Vui lòng liên hệ bộ phận phát triển của PROTOCOLVN");
                return;
            }
            else
            {
                HelpMsgBox.ShowNotificationMessage(obj.GetType().Name + ". Bạn đã cấu hình sai hoặc PL-FW chưa hỗ trợ. Xin liên hệ quản lý dự án.\nThông tin thêm: " + Info);
                return;
            }
        }
        #endregion
    }

    class COMMENT
    {
        /*
         * TênDev + _NC : Xem cái gì ? Làm cái gì ?
         *      Ex: PHUOCNT_NC: Gắn Tiêu đề form vào form content như 1 label trên form ?
         * TênDev + _OK : Tình trạng sau thực hiện
         *      Ex: PHUOCNT_OK: Đã giải quyết nhưng các form khi thiết kế phải chừa 1 khoản trống là bao nhiêu đó.     
        */
    }

    /// <summary>
    /// NO_USE: Thông báo rằng form này ko có dùng trong dự án
    /// NO: Thông báo rằng form chưa được phân công thực hiện
    /// NC: Thông báo rằng form đã phân công và đang giai đoạn phát triển
    /// OK_DEV: Thông báo rằng form đã hoàn tất giai đoạn phát triển
    /// OK_TEST: Thông báo rằng form đã hoàn tất giai đoạn phát triển và đã test
    /// FIX: Thông báo rằng form đã hoàn tất giai đoạn phát triển và đang fix lỗi bộ phận test phát hiện
    /// FIXED: Thông báo rằng form đã hoàn tất giai đoạn phát triển và đã fix lỗi bộ phận test phát hiện
    /// </summary>
    public enum FormStatus
    {
        NO_USE,
        NO,
        NC,
        OK_DEV,
        OK_TEST,
        FIX,
        FIXED
    }

    [Obsolete("Sử dụng HelpDebug để thay thế")]
    public class PLDebug : HelpDebug
    {
    }
    /// <summary>
    /// Lớp được dùng để thông báo lỗi do cấu hình khi phát triển của Developer.
    /// </summary>
    [Obsolete("Dùng lớp HelpDebug để thay thê")]
    public class HelpDevError : HelpDebug
    {

    }

    [Obsolete("Dùng lớp HelpDebug để thay thê")]
    public class PLMessageBoxDev : HelpDebug
    {
    }

    [Obsolete("Dùng lớp HelpDebug để thay thê")]
    public class PMSupport : HelpDebug
    {
    }


}
