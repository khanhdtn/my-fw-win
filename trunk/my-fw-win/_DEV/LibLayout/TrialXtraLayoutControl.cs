using System;
using System.Collections.Generic;

using System.Text;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    public class TrialXtraLayoutControl
    {
        /// <summary>Hàm dựng thực thi LayoutControl
        /// </summary>
        /// <param name="xtraForm">form cần thực thi (form chứa layout control)</param>
        /// <param name="NamespaceForm">Tên namespace của form</param>
        /// <param name="layOutControl">layout control cần thực thi</param>
        /// <param name="PathFoder">Thư mục nơi chứa file xml của layout</param>
        public static void InitLayoutControlXtraForm(XtraForm xtraForm,string NamespaceForm, LayoutControl layOutControl,string PathFoder)
        {
            string filenameActice = FrameworkParams.currentUser.username + "_" + NamespaceForm + "." + xtraForm.Name;
            ShowButtonDesignLayout(layOutControl, xtraForm);
            SetDefaultIntoMemberBuff(layOutControl);
            LoadFromXml(layOutControl, PathFoder + "\\" + filenameActice);

            xtraForm.FormClosing += delegate(object sender, System.Windows.Forms.FormClosingEventArgs e)
            {
                SaveXmlToDr(layOutControl, PathFoder , "\\" + filenameActice );
            };
        }

        public static void InitLayoutControlUserCtrl(XtraUserControl xtraUserControl, string NamespaceUserCtrl, LayoutControl layOutControl, string PathFolder)
        {
            Form parentForm = xtraUserControl.ParentForm;
            if (parentForm != null)
                MessageBox.Show("sahgh");
            string filenameActice = FrameworkParams.currentUser.username + "_" + NamespaceUserCtrl + "." + xtraUserControl.Name ;
            SetDefaultIntoMemberBuff(layOutControl);
            LoadFromXml(layOutControl, PathFolder + "\\" + filenameActice);
               
            layOutControl.HideCustomization += delegate(object sender, EventArgs e)
            {
                SaveXmlToDr(layOutControl, PathFolder, "\\" + filenameActice);
            };
        
        }

        #region CHAUTV : Các hàm dựng hỗ trợ layout trên form
        //Tạm thời để chế độ (protected) để không gây khó khăn cho người dùng

        /// <summary>
        /// Hàm gắn hai nút chức năng hỗ trợ thiết kế giao diện lên form
        /// </summary>
        /// <param name="_LayoutControl">XtraLayoutControl</param>
        /// <param name="frm"></param>
        protected static void ShowButtonDesignLayout(LayoutControl _LayoutControl, XtraForm frm)
        {
            SimpleButton Design = new SimpleButton();
            Design.Name = "btnDesign";
            Design.Text = "..";
            Design.Click += delegate(object sender, EventArgs e)
            {
                _LayoutControl.ShowCustomizationForm();
            };
            Design.ToolTipTitle = "Thông báo";
            Design.ToolTip = "Thiết kế giao diện";
            Design.Location = new System.Drawing.Point(frm.Width - 75, 0);
            Design.Size = new System.Drawing.Size(30, 23);

            SimpleButton Default = new SimpleButton();
            Default.Name = "btnDefault";
            Default.Text = "..";
            Default.Click += delegate(object sender, EventArgs e)
            {
                _LayoutControl.RestoreDefaultLayout();
            };
            Default.ToolTipTitle = "Thông báo";
            Default.ToolTip = "Giao diện mặc định";
            Default.Location = new System.Drawing.Point(frm.Width - 45, 0);
            Default.Size = new System.Drawing.Size(30, 23);

            frm.Controls.Add(Design);
            frm.Controls.Add(Default);

            frm.SizeChanged += delegate(object sender, EventArgs e)
            {
                frm.Controls.Remove(Design);
                frm.Controls.Remove(Default);
                Default.Location = new System.Drawing.Point(frm.Width - 45, 0);
                Design.Location = new System.Drawing.Point(frm.Width - 75, 0);
                frm.Controls.Add(Design);
                frm.Controls.Add(Default);

            };
        }

        /// <summary>
        /// Hàm lưu giao diện xuống file xml
        /// </summary>
        /// <param name="_LayoutControl">XtraLayoutControl nguồn</param>
        /// <param name="pathFile">Đường dẫn thư mục</param>
        /// <param name="FileName">Tên file (Có thể có phần [.xml] hoặc không)</param>
        /// <returns></returns>
        public static bool SaveXmlToDr(LayoutControl _LayoutControl,string pathFile,string FileNameXml)
        {
            string err = string.Empty;
            pathFile = pathFile.Trim();
            try
            {
                if (pathFile.Trim() == string.Empty)
                {
                    _LayoutControl.SaveLayoutToXml(FileNameXml);
                    return true;
                }

                if (System.IO.Directory.Exists(pathFile))
                {
                    if(FileNameXml.Contains(".xml"))
                        _LayoutControl.SaveLayoutToXml(pathFile + "\\" + FileNameXml);
                    else
                        _LayoutControl.SaveLayoutToXml(pathFile + "\\" + FileNameXml + ".xml");
                }

            }
            catch (Exception ex)
            {
                err = ex.Message;
                throw new Exception(ex.Message);
            }
            if (err == string.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// Ghi nhận giao diện thiết kế trên form ban đầu vào vùng nhớ tạm 
        /// </summary>
        /// <param name="_LayoutControl">XtraLayoutControl</param>
        protected static void SetDefaultIntoMemberBuff(LayoutControl _LayoutControl)
        {
            _LayoutControl.SetDefaultLayout();
        }

        /// <summary>
        /// Lấy lại giao diện thiết kế ban đầu trên form, do người lập trình thiết kế
        /// </summary>
        /// <param name="_LayoutControl">XtraLayoutControl</param>
        protected static void ResetDefault(LayoutControl _LayoutControl)
        {
            _LayoutControl.RestoreDefaultLayout();
        }

        /// <summary>
        /// Mở và hiển thị giao diện được lấy từ file xml
        /// </summary>
        /// <param name="_LayoutControl"></param>
        /// <param name="filename">Tên file (có thể có .xml hoặc không)</param>
        /// <returns></returns>
        public static bool LoadFromXml(LayoutControl _LayoutControl, string filenameXml)
        {
            string err = string.Empty;
            try
            {
                if (filenameXml.Contains(".xml") == false)
                    filenameXml += ".xml";
                if (System.IO.File.Exists(filenameXml))
                    _LayoutControl.RestoreLayoutFromXml(filenameXml);
            }
            catch ( Exception ex)
            {
                err = ex.Message;
                throw new Exception(ex.Message) ;
            }
            if (err == string.Empty)
                return true;
            return false;
        }

        #endregion

        #region CHAUTV : Các hàm dựng hỗ trợ UserControl (Dang xây dựng)
        
        #endregion
    }
}
