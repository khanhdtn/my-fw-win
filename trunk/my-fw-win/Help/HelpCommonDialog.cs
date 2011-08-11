using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Data;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp hỗ trợ cho phép mở một số hộp thoại thường dùng và
    /// Hộp thoại của công ty.
    /// </summary>
    public class HelpCommonDialog : HelpMsgBox
    {
        /// <summary>
        /// Hộp thoại nói lên chức năng này đang xây dựng.
        /// </summary>
        public static void showDangXayDung(XtraForm form)
        {
            XtraForm owner = (form == null ? FrameworkParams.MainForm : form);
            frmDangXayDung frm = new frmDangXayDung();
            ProtocolForm.ShowModalDialog(owner, frm);
        }

        /// <summary>
        /// Hộp thoại chứa thông tin có trong DataSet giúp cho người dùng có thể muốn copy số liệu và in số liệu 
        /// </summary>
        public static void showGridInfo(XtraForm form, DataSet data, String title, bool isModal)
        {
            frmGridInfo frm = new frmGridInfo();
            frm.InitData(data, title);

            XtraForm owner = (form == null ? FrameworkParams.MainForm : form);
            if (isModal)
                ProtocolForm.ShowModalDialog(owner, frm);
            else
                ProtocolForm.ShowDialog(owner, frm);
        }


        /// <summary>
        /// Hiển thị hộp thoại truy vấn nâng cao và cho phép lưu câu truy vấn
        /// </summary>
        public static void showSaveQueryDialog(XtraForm form, String dataSetID, String masterQueryNoCondition, GridControl gridCtrl, bool isModal)
        {
            FilterCase obj = new FilterCase(FrameworkParams.currentUser.id, dataSetID, "Save Query Dialog", masterQueryNoCondition);
            SaveQueryDialog q = new SaveQueryDialog(obj, gridCtrl);
            XtraForm owner = (form == null ? FrameworkParams.MainForm : form);
            q.Owner = owner;
            if (isModal)
            {
                q.ShowDialog();
            }
            else
            {
                q.Show();
            }
        }

        /// <summary>
        /// Hộp thoại hiện thị dữ liệu văn bản nó cho phép người dùng có thể sao chép dữ liệu
        /// </summary>
        public static void showTextInfo(XtraForm form, String text, String title, bool isModal)
        {
            frmTextInfo frm = new frmTextInfo();
            frm.InitData(text, title);
            frm.Text = title;
            frm.TopLevel = true;
            //frm.TopMost = true;
            XtraForm owner = (form == null ? FrameworkParams.MainForm : form);
            if(isModal)
                ProtocolForm.ShowModalDialog(owner, frm);
            else
                ProtocolForm.ShowDialog(owner, frm);
        }

        /// <summary>
        /// Hộp thoại thống báo vi phạm phân quyền
        /// </summary>
        public static void showPermissionFail(XtraForm form)
        {
            XtraForm owner = (form == null ? FrameworkParams.MainForm : form);
            frmPermissionFail frm = new frmPermissionFail();

            ProtocolForm.ShowModalDialog(owner, frm);
        }

        /// <summary>Chọn tập tin trả về đường dẫn đến tập tin đó
        /// </summary>
        /// <param name="Filter">
        ///     "GIF|*.gif|BMP|*.bmp|JPEG|*.jpg;*.jpeg|Tất cả|*.*"
        ///     "Tập tin bổ trợ (*.zip)|*.zip"
        ///     "Excel files (*.xls,*.xlsx)|*.xls;*.xlsx|Tất cả (*.*)|*.*"
        ///     "Tập tin dự phòng (*.fbk)|*.fbk|Tất cả (*.*)|*.*"
        ///     "Tập tin cấu hình kết nối (*.cpl)|*.cpl|Tất cả (*.*)|*.*"
        ///     "Tập tin cơ sở dữ liệu (*.gdb)|*.gdb|Tập tin cơ sở dữ liệu (*.fdb)|*.fdb|Tất cả (*.*)|*.*"
        ///     "Image Files(*.bmp;*.jpg;*.gif;*.jpeg;*.png)|*.bmp;*.jpg;*.gif;*.jpeg;*.png"
        ///     "SQL Script File(*.sql)|*.sql"
        ///     "Hình ảnh(*.bmp,*.icon,*.ico,*.jpg,*.jpeg,*.gif,*.png)|*.bmp;*.icon;*.ico;*.jpg;*.jpeg;*.gif;*.png"
        /// </param>
        /// <param name="Title"></param>
        /// <param name="maxSize">
        ///     -1: Không giới hạn kích thước
        ///     >0: Giới hạn kích thước
        /// </param>
        public static string showChooseFileByOpenFileDialog(string Filter, string Title, int MaxSize)
        {
            String title = (Title == null ? HelpApplication.getTitleForm("Chọn tập tin"): HelpApplication.getTitleForm(Title));
            String filter = (Filter == null ? "Tất cả (*.*)|*.*" : Filter); 
        OpenFile:
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = Filter;
            open.Title = title;
            open.DereferenceLinks = false;
            DialogResult dr = open.ShowDialog();
            if (dr == DialogResult.Cancel) 
                return String.Empty;

            if (MaxSize <= 0)//Không hạn chế kích thước
            {
                return open.FileName;
            }
            else//Hạn chế kích thước
            {
                if (HelpFile.CheckFileSize(open.FileName, MaxSize))//Nhỏ hơn
                    return open.FileName;
                else//Chọn lại
                {
                    HelpMsgBox.ShowNotificationMessage(string.Format("Kích thước tập tin không được vượt quá {0} MB!", MaxSize));
                    goto OpenFile;
                }
            }
        }

        public static string showChooseFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            else
                return String.Empty;
        }
    }
}