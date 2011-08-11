using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Truy cập - READ
    /// Cập nhật - EDIT
    /// Duyệt - ADD
    /// </summary>
    public class frmUserManReadEditCommit : frmUserMan
    {
        public frmUserManReadEditCommit() : base()
        {
            HelpGridColumn.AnCot(colISDELETE);
            colDes.VisibleIndex = 1;   

            colISREAD.Caption = "Truy cập";
            colISREAD.VisibleIndex = 2;

            colISUPDATE.Caption = "Cập nhật";
            colISUPDATE.VisibleIndex = 3;

            colISINSERT.Caption = "Duyệt";
            colISINSERT.VisibleIndex = 4;
        }

        //public override List<Object> GetObjectItems()
        //{
        //    string featureName = "Quản lý người dùng REC";
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
