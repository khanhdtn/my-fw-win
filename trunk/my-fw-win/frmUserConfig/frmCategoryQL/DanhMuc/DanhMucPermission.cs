using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolVN.Framework.Core;
using ProtocolVN.DanhMuc;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public class DanhMucPermission
    {
        [Obsolete("...")]
        public static DanhMucPermType DMPermission = DanhMucPermType.NO;
        [Obsolete("...")]
        public static DelegationLib.DefinePermission GetPermission(XtraUserControl DM, String feature)
        {
            if (DMPermission == DanhMucPermType.READ_EDIT_COMMIT)
            {
                return HelpPermission.CategoryPermissionReadEditCommit(DM, feature);
            }
            else if (DMPermission == DanhMucPermType.READ_ADD_DELETE_EDIT)
            {
                return HelpPermission.CategoryPermission(DM, feature);
            }
            else if (DMPermission == DanhMucPermType.READ_EDIT)
            {
                return HelpPermission.CategoryPermissionReadEdit(DM, feature);
            }
            else
            {
                return null;
            }
        }

        public static DelegationLib.DefinePermission GetPermission(XtraUserControl control, String feature, String description)
        {
            return HelpPermission.CategoryPermission(control, feature, description);
        }
    }
}
