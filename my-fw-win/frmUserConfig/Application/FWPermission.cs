using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing.Printing;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public class FWPermission
    {
        private static Permission ONguoiDungNhomNguoiDung = new Permission("ONguoiDungNhomNguoiDung", "NgÆ°á»i dÃ¹ng / NhÃ³m ngÆ°á»i dÃ¹ng");
        private static Permission FSaoLuuPhucHoi = new Permission("FSaoluuphuchoi", "Sao lÆ°u & Phá»¥c há»“i");
        private static Permission FTaiChuongTrinhVeServer = new Permission("FTaiChuongTrinhVeServer", "Táº¡i chÆ°Æ¡ng trÃ¬nh vá» Server");

        public static void Init()
        {
            Permission per = ONguoiDungNhomNguoiDung;
        }

        public static List<string> GetPublicForm()
        {
            List<string> publicForms = new List<string>();

            publicForms.Add(FWFormName.frmLogin);
            publicForms.Add(FWFormName.frmChangePwd);
            publicForms.Add(FWFormName.frmConfigDB);            
            publicForms.Add(FWFormName.frmXPOption);

            publicForms.Add(FWFormName.frmPLAbout);
            publicForms.Add(FWFormName.frmLockApplication);
            publicForms.Add(FWFormName.frmLicence);

            publicForms.Add(FWFormName.frmBaseReport);
            publicForms.Add(FWFormName.PLBlankReport);

            publicForms.Add(FWFormName.frmCategory);
            publicForms.Add(FWFormName.frmPermissionFail);

            publicForms.Add(FWFormName.frmGridInfo);

            publicForms.Add(FWFormName.noFRMQuickAccessMethodExec);

            publicForms.Add(FWFormName.frmIntro);

            #region PREDICATE
            publicForms.Add(FWFormName.frmOption);
            #endregion

            return publicForms;
        }

        public static Dictionary<string, string> GetFormFeatureMap()
        {
            Dictionary<string, string> mitemPermissionMap = new Dictionary<string, string>();

            mitemPermissionMap.Add(FWFormName.frmUserManExt, ONguoiDungNhomNguoiDung.GetPermissionStr(PermissionType.VIEW));
            mitemPermissionMap.Add(FWFormName.frmTreeUserManExt, ONguoiDungNhomNguoiDung.GetPermissionStr(PermissionType.VIEW));
            mitemPermissionMap.Add(FWFormName.frmBackupRestore, FSaoLuuPhucHoi.GetPermissionStr(PermissionType.VIEW));
            mitemPermissionMap.Add(FWFormName.frmLiveUpdate, FTaiChuongTrinhVeServer.GetPermissionStr(PermissionType.VIEW));

            return mitemPermissionMap;
        }

        public static void GetObjectItems(XtraForm frmCommon, List<Object> items)
        {
            if (frmCommon is frmUserMan)
            {
                frmUserMan frm = (frmUserMan)frmCommon;
                string featureName = "Quản lý người dùng";

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    new PermissionItem(featureName, PermissionType.ADD));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    new PermissionItem(featureName, PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    new PermissionItem(featureName, PermissionType.DELETE));
            }

            if (frmCommon is frmUserManReadEdit)
            {
                frmUserManReadEdit frm = (frmUserManReadEdit)frmCommon;
                string featureName = "Quản lý người dùng RE";
                
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    new PermissionItem(featureName, PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    new PermissionItem(featureName, PermissionType.EDIT));
            }

            if (frmCommon is frmUserManReadEditCommit)
            {
                frmUserManReadEditCommit frm = (frmUserManReadEditCommit)frmCommon;
                string featureName = "Quản lý người dùng REC";

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    new PermissionItem(featureName, PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    new PermissionItem(featureName, PermissionType.EDIT));
            }

            if (frmCommon is frmUserManExt)
            {
                frmUserManExt frm = (frmUserManExt)frmCommon;

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.ADD));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.DELETE));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.EDIT));
            }

            if (frmCommon is frmTreeUserMan)
            {
                string featureName = "Quản lý người dùng";
                frmTreeUserMan frm = (frmTreeUserMan)frmCommon;

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    new PermissionItem(featureName, PermissionType.ADD));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    new PermissionItem(featureName, PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    new PermissionItem(featureName, PermissionType.DELETE));
            }

            if (frmCommon is frmTreeUserManReadEdit)
            {
                string featureName = "Quản lý người dùng RE";
                frmTreeUserManReadEdit frm = (frmTreeUserManReadEdit)frmCommon;

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    new PermissionItem(featureName, PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    new PermissionItem(featureName, PermissionType.EDIT));
            }

            if (frmCommon is frmTreeUserManReadEditCommit)
            {
                string featureName = "Quản lý người dùng REC";
                frmTreeUserManReadEditCommit frm = (frmTreeUserManReadEditCommit)frmCommon;

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    new PermissionItem(featureName, PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    new PermissionItem(featureName, PermissionType.EDIT));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    new PermissionItem(featureName, PermissionType.EDIT));
            }

            if (frmCommon is frmTreeUserManExt)
            {
                frmTreeUserManExt frm = (frmTreeUserManExt)frmCommon;

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnInsert,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.ADD));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDelete,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.DELETE));

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnDontSave,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.EDIT));
                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnEdit,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.EDIT));
            }

            else if (frmCommon is frmUserChild)
            {
                frmUserChild frm = (frmUserChild)frmCommon;

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.ADD_EDIT));
            }

            else if (frmCommon is frmGroupChild)
            {
                frmGroupChild frm = (frmGroupChild)frmCommon;

                ApplyPermissionAction.ApplyPermissionObject(items, frm.btnSave,
                    ONguoiDungNhomNguoiDung.GetPermissionItem(PermissionType.ADD_EDIT));
            }
        }
    }
}
