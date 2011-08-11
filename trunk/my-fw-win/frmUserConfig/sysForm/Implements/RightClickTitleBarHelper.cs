using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    


    public class RightClickTitleBarHelper
    {
        #region Danh sách các xử lý trên form
        public static void showFormInfo(Form form)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Tên lớp: " + form.GetType().FullName);
            str.AppendLine();
            str.Append("Màn hình public: " + (form is IPublicForm ? "Có" : "Không"));
            HelpDebug.ShowString(str.ToString());
        }
        public static void refreshForm(IFormRefresh form)
        {
            form._RefreshAction(null);
        }
        public static void showFURL(IFormFURL frm)
        {
            if (frm != null && frm is XtraForm)
            {
                HelpCommonDialog.showTextInfo((XtraForm)frm, frm._FURLAction().ToString(), "Địa chỉ FURL", true);
            }
        }
        public static bool isDesktopForm(Form form)
        {
            if ((form.Tag != null && form.Tag.ToString() == FrameworkParams.desktopForm) || form.Text == "Giới thiệu")
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
