using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Chỉ định danh sách CTRL | ITEM phải kiểm tra phân quyền.
    /// </summary>
    public interface IPermisionable
    {
        List<Control> GetPermisionableControls();
        List<Object> GetObjectItems();
    }

    /// <summary>Khai báo FORM ở dạng Public
    /// </summary>
    public interface IPublicForm
    {
    }

    /// <summary>Một ứng dụng cần phân quyền cần phải xây dựng 1 IPermission.
    /// Mục đích của IPermission là cho phép dev xây dựng 1 giải pháp permission
    /// cho toàn ứng dụng ( không tập trung trên từng form ) giúp linh động giải pháp
    /// phân quyền.
    /// </summary>
    public interface IPermission
    {
        /// <summary>Chỉ định các tên các Public Form.
        /// Public Form là Form mà mọi account đều có thể thực hiện.
        /// Có thể dùng IPublicForm trên Form đó (nhưng nếu màn hình có khi Public khi Private thì không thể dùng).
        /// Phạm vi toàn bộ ứng dụng.
        /// </summary>
        List<string> getPublicForm();

        /// <summary>Khai báo ánh xạ các item trong MENU với các FEATURE_CAT tương ứng. 
        /// Bắt buộc. Phạm vi toàn bộ ứng dụng.
        /// </summary>
        Dictionary<string, string> getFormFeatureMap();

        /// <summary>Chỉ định CONTROL & ITEM cần phải kiểm tra Permission trên toàn bộ ứng dụng.
        /// Bắt buộc. Phạm vi toàn bộ ứng dụng.
        /// </summary>        
        List<Object> GetObjectItems(XtraForm form);


        /// <summary>Chỉ định CONTROL cần phải kiểm tra Permission. Hiện tại mình có thể chỉ định thông qua GetObjectItems luôn.
        /// Không bắt buộc. Phạm vi toàn bộ ứng dụng.
        /// </summary>
        [Obsolete("Dùng GetObjectItems")]
        List<Control> GetPermisionableControls(XtraForm form);
    }
}
