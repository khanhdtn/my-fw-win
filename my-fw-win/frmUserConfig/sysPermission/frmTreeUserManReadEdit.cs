using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// (Truy Cập - VIEW)
    /// (Cập nhật - EDIT)
    /// Đã sử dụng trong các dự án nên không xóa
    [Obsolete("Không sử dụng")]
    public class frmTreeUserManReadEdit : frmTreeUserMan
    {
        public frmTreeUserManReadEdit() : base()
        {
            HelpTreeColumn.AnCot(colTreeListISINSERT_BIT);
            HelpTreeColumn.AnCot(colTreeListISDELETE_BIT);
            colTreeListDESCRIPTION.VisibleIndex = 1;

            colTreeListISREAD_BIT.VisibleIndex = 2;
            colTreeListISREAD_BIT.Caption = "Truy cập";

            colTreeListISUPDATE_BIT.VisibleIndex = 3;
            colTreeListISUPDATE_BIT.Caption = "Cập nhật";
        }

        //public override List<Object> GetObjectItems()
        //{
        //    string featureName = "Quản lý người dùng RE";

        //    List<Object> items = new List<Object>();
        //    ApplyPermissionAction.ApplyPermissionObject(items, this.btnInsert,
        //        new PermissionItem(featureName, PermissionType.EDIT));

        //    ApplyPermissionAction.ApplyPermissionObject(items, this.btnSave,
        //        new PermissionItem(featureName, PermissionType.EDIT));
        //    ApplyPermissionAction.ApplyPermissionObject(items, this.btnDontSave,
        //        new PermissionItem(featureName, PermissionType.EDIT));

        //    ApplyPermissionAction.ApplyPermissionObject(items, this.btnEdit,
        //        new PermissionItem(featureName, PermissionType.EDIT));

        //    ApplyPermissionAction.ApplyPermissionObject(items, this.btnDelete,
        //        new PermissionItem(featureName, PermissionType.EDIT));

        //    return items;
        //}
    }
}