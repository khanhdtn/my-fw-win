using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
namespace ProtocolVN.Framework.Win
{
    public class PLCCapNhatTiGia:PLCPhieu
    {
        public static PLCCapNhatTiGia I = new PLCCapNhatTiGia();
        public static String frmCapNhatTiGia = typeof(frmCapNhatTiGia).FullName;
        #region PLCPhieu Members

        List<System.Windows.Forms.Control> PLCPhieu.GetFormatControls(DevExpress.XtraEditors.XtraForm formModPhieu)
        {
            throw new NotImplementedException();
        }

        List<object> PLCPhieu.GetObjectItems(DevExpress.XtraEditors.XtraForm formModPhieu)
        {
            throw new NotImplementedException();
        }

        PhieuType PLCPhieu.GetPhieuType()
        {
            throw new NotImplementedException();
        }

        _Print PLCPhieu.GetPrintObj(DevExpress.XtraEditors.XtraForm frmMain, long[] IDs)
        {
            throw new NotImplementedException();
        }

        FieldNameCheck[] PLCPhieu.GetRule(object DAModPhieu)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Phan quyen
        public static Permission perCapNhatTiGia = new Permission("OCapNhatTiGia", "Cáº­p nháº­t tá»‰ giÃ¡");        

        public bool Contains(XtraForm form)
        {
            if (form is frmCapNhatTiGia)
                return true;
            return false;
        }

        public List<object> GetObjectItems(DevExpress.XtraEditors.XtraForm formModPhieu)
        {
            List<Object> items = new List<Object>();
            if (formModPhieu is frmCapNhatTiGia)
            {
                frmCapNhatTiGia that = (frmCapNhatTiGia)formModPhieu;
                ApplyPermissionAction.ApplyPermissionObject(items,
                        new object[]{ that.btnSave,
                            that.btnAddNew,
                            that.CotTiGiaMoi},
                    new PermissionItem[]{ perCapNhatTiGia.GetPermissionItem(PermissionType.ADD_EDIT),
                    perCapNhatTiGia.GetPermissionItem(PermissionType.ADD_EDIT),
                    perCapNhatTiGia.GetPermissionItem(PermissionType.ADD_EDIT)                    
                    });
                ApplyPermissionAction.ApplyPermissionObject(items,
                    that.CotDong,
                    perCapNhatTiGia.GetPermissionItem(PermissionType.DELETE));
               
            }           
            
            return items;
        }
        #endregion
    }
}
