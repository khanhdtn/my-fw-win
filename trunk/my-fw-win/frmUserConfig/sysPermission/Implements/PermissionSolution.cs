using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Win.Dev;

namespace ProtocolVN.Framework.Win
{
    #region Data Object
    /// <summary>Các loại phân quyền đang hỗ trợ
    /// </summary>
    public enum PermissionType
    {
        VIEW,   //Truy xuất, Xem mở không thì đóng
        ADD,    //Thêm | Duyệt
        DELETE, //Xóa
        EDIT,   //Sửa | Cập nhật
        ADD_EDIT,   //Thêm | Sửa đều có quyền
        ADD_EDIT_DELETE, //Thêm | Sửa | Xóa đều có quyền
        EDIT_DELETE, //Sửa | Xóa đều có quyền
        VIEW_ADD_DELETE_EDIT, //Xem | Them | Xoa | Sua
        VIEW_HIDE,//Xem không xem ẩn,
        PRINT,//In
        EXPORT//Xuất file
    }
    /// <summary>Cách xử lý khi vi phạm phân quyền
    /// </summary>
    public enum PermissionHow
    {
        HIDE,
        DISABLE
    }
    /// <summary>Một mục phân quyền
    /// </summary>
    public class PermissionItem
    {
        public string featureName;              //Tên chức năng
        public PermissionType permissionType;   //Loại phân quyền
        public DelegationLib.CallFunction_NoIn_NoOut failAction;    //Vi phạm điều kiện phân quyền sẽ gọi hàm này 
        public PermissionHow permissionHow = PermissionHow.HIDE;    //Vi phạm điều kiện sẽ thực hiện hành động này đối với control.

        public List<PermissionItem> OROtherItem = new List<PermissionItem>();

        public PermissionItem(string featureName, PermissionType permissionType,
            PermissionHow permissionHow, DelegationLib.CallFunction_NoIn_NoOut failAction)
        {
            this.featureName = featureName;
            this.permissionType = permissionType;
            this.permissionHow = permissionHow;
            this.failAction = failAction;
        }

        public PermissionItem(string featureName, PermissionType permissionType,
            DelegationLib.CallFunction_NoIn_NoOut failAction) :
            this(featureName, permissionType, PermissionHow.HIDE, failAction)
        { }

        public PermissionItem(string featureName, PermissionType permissionType) :
            this(featureName, permissionType, PermissionHow.HIDE, null) { }        
    }
    #endregion

    #region Xây dựng FEATURE_CAT Table dùng Store Pattern
    public class PermissionStore
    {                       
        public static List<Permission> MissFeatures = new List<Permission>();
        public static void AddMissing(String FeatureName)
        {
            foreach (Permission feature in MissFeatures)
            {
                if (feature.featureName.Equals(FeatureName))
                {
                    return;
                }
            }
            Permission per = PermissionStore.Exist(FeatureName); 
            if(per!=null) 
                MissFeatures.Add(per);                        
            else{
                HelpDevError.ShowMessage(FeatureName + " này ko được tạo qua new Permission");
            }
            return;
        }

        public static List<Permission> AppFeatures = new List<Permission>();
        
        public static Permission Exist(String FeatureName)
        {
            foreach (Permission feature in AppFeatures)
            {
                if (feature.featureName.Equals(FeatureName))
                {
                    return feature;
                }
            }
            return null;
        }
       
        public static string ToSQL()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Permission feature in AppFeatures)
            {
                builder.AppendLine(feature.SQL());
            }
            return builder.ToString();
        }
    }

    public class Permission : Feature
    {
        private static int ID = 100;
        public Permission(String FeatureName, String Description)
        {
            this.featureName = FeatureName;
            this.description = Description;
            this.isRead = false;
            this.isInsert = false;
            this.isDelete = false;
            this.isUpdate = false;
            this.isPrint = false;
            this.isExport = false;

            if (FrameworkParams.isSupportDeveloper == true)
            {
                Permission per = PermissionStore.Exist(FeatureName);
                if (per == null)
                {
                    PermissionStore.AppFeatures.Add(this);
                }
            }
        }

        public override bool Equals(object obj)
        {
            Permission feature = (Permission)obj;
            return feature.featureName.Equals(this.featureName);
        }

        public string SQL()
        {
            if (featureName.StartsWith("F"))
            {
                return @"INSERT INTO FEATURE_CAT (ID, NAME, DESCRIPTION, VISIBLE_BIT, ISREAD, ISINSERT, ISUPDATE, ISDELETE, ISPRINT, ISEXPORT) 
                VALUES (" + "gen_id(G_FW_ID, 1)" + @", '" + this.featureName + @"', '" + this.description + @"', 'Y', '"
                          + "Y" + "','" + "N" + "','" + "N" + "','" + "N" + "','" + "N" + "','" + "N" + "');";
            }
            else if (featureName.StartsWith("O"))
            {
                return @"INSERT INTO FEATURE_CAT (ID, NAME, DESCRIPTION, VISIBLE_BIT, ISREAD, ISINSERT, ISUPDATE, ISDELETE, ISPRINT, ISEXPORT) 
                VALUES (" + "gen_id(G_FW_ID, 1)" + @", '" + this.featureName + @"', '" + this.description + @"', 'Y', '"
                          + "Y" + "','" + "Y" + "','" + "Y" + "','" + "Y" + "','" + "Y" + "','" + "Y" + "');";    
            }
            else if (featureName.IndexOf(".DM") > 0)//Danh muc
            {
                return @"INSERT INTO FEATURE_CAT (ID, NAME, DESCRIPTION, VISIBLE_BIT, ISREAD, ISINSERT, ISUPDATE, ISDELETE, ISPRINT, ISEXPORT)
                VALUES (" + "gen_id(G_FW_ID, 1)" + @", '" + this.featureName + @"', '" + this.description + @"', 'Y', '"
                          + "Y" + "','" + "Y" + "','" + "Y" + "','" + "Y" + "','" + "Y" + "','" + "Y" + "');";    
            }
            return featureName + " đặt không đúng chuẩn";
        }

        public PermissionItem GetPermissionItem(PermissionType type)
        {
            return new PermissionItem(this.featureName, type);
        }

        public PermissionItem GetPermissionItem(PermissionType type, PermissionHow how)
        {
            PermissionItem item = GetPermissionItem(type);
            item.permissionHow = how;
            return item;
        }

        public String GetPermissionStr(PermissionType type)
        {            
            String ret = featureName;
            switch (type)
            {
                case PermissionType.VIEW:
                    ret = featureName + ";VIEW";
                    break;
                case PermissionType.EDIT:
                    ret = featureName + ";EDIT";
                    break;
                case PermissionType.ADD:
                    ret = featureName + ";ADD";
                    break;
                case PermissionType.DELETE:
                    ret = featureName + ";DELETE";
                    break;
                case PermissionType.PRINT:
                    ret = featureName + ";PRINT";
                    break;
                case PermissionType.EXPORT:
                    ret = featureName + ";EXPORT";
                    break;
            }
            return ret;
        }
    }
    #endregion


    /// <summary>Lớp áp dụng phân quyền
    /// </summary>
    public class ApplyPermissionAction : ApplyAction
    {
        private User user;

        public ApplyPermissionAction(string formName)
        {
            this.user = FrameworkParams.currentUser;
        }

        #region Hướng Element : Đang cập nhật các Element hỗ trợ
        /// <summary>Hàm cho phép áp dụng phân quyền trên 1 Item
        /// </summary>
        public override bool applyElement(Object element, XtraForm form)
        {
            if (!User.isAdmin(user.username) && FrameworkParams.isPermision!=null)
            {
                PermissionItem tagValue = null;

                #region Phân quyền trên các control hành động
                #region ToolStripMenuItem nằm trên Menu
                if ((element as ToolStripMenuItem) != null)
                {
                    tagValue = null;
                    if (TagPropertyMan.Get(((ToolStripMenuItem)element).Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(((ToolStripMenuItem)element).Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null) return false;
                    if (result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE)
                            ((ToolStripMenuItem)element).Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE)
                            ((ToolStripMenuItem)element).Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region ToolStripButton nằm trên Bar
                else if ((element as ToolStripButton) != null)
                {
                    ToolStripButton item = (ToolStripButton)element;

                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null) return false;
                    if (result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region ToolStripButton nằm trên Bar
                else if ((element as ToolStripDropDownButton) != null)
                {
                    ToolStripDropDownButton item = (ToolStripDropDownButton)element;

                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null) return false;
                    if (result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region BarItem nằm trên Bars, BarManager
                else if ((element as BarItem) != null)
                {
                    BarItem barItem = (BarItem)element;

                    if (TagPropertyMan.Get(barItem.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(barItem.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null) return false;
                    if (result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) barItem.Visibility = BarItemVisibility.Never;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) barItem.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region SimpleButton
                else if ((element as SimpleButton) != null)
                {
                    SimpleButton item = (SimpleButton)element;

                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null) return false;
                    if (result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region DropDownButton
                else if ((element as DropDownButton) != null)
                {
                    DropDownButton item = (DropDownButton)element;

                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null) return false;
                    if (result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #endregion

                #region Phân quyền trên các control thông tin
                #region CalcEdit
                else if ((element as CalcEdit) != null)
                {
                    CalcEdit item = (CalcEdit)element;
                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null || result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region SpinEdit
                else if ((element as SpinEdit) != null)
                {
                    SpinEdit item = (SpinEdit)element;
                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null || result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region TextEdit
                else if ((element as TextEdit) != null)
                {
                    TextEdit item = (TextEdit)element;
                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null || result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region GridColumn
                else if ((element as GridColumn) != null)
                {
                    GridColumn item = (GridColumn)element;
                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null || result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) HelpGridColumn.HideGridColumn(item);
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.OptionsColumn.ReadOnly = true;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #region XtraUserControl
                else if ((element as XtraUserControl) != null)
                {
                    XtraUserControl item = (XtraUserControl)element;

                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null || result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }                    
                }
                #endregion
                #region Control
                else if ((element as Control) != null)
                {
                    Control item = (Control)element;

                    if (TagPropertyMan.Get(item.Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(item.Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null || result == false)
                    {
                        if (tagValue.permissionHow == PermissionHow.HIDE) item.Visible = false;
                        else if (tagValue.permissionHow == PermissionHow.DISABLE) item.Enabled = false;
                        if (tagValue.failAction != null) tagValue.failAction();
                    }
                }
                #endregion
                #endregion
                else
                {
                    PLMessageBoxDev.ShowMessage("applyElement : Item: " + element.GetType().Name + " chưa được hỗ trợ phân quyền.");
                }

            }
            return true;
        }

        #region Hàm hỗ trợ khai báo phân quyền
        public static void ApplyPermissionObject(Object item, PermissionItem permission)
        {
            if (permission == null) return;
            #region Phân quyền trên các control hành động
            if ((item as ToolStripMenuItem) != null)
            {
                object obj = ((ToolStripMenuItem)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((ToolStripMenuItem)item).Tag = obj;
            }
            else if ((item as ToolStripButton) != null)
            {
                object obj = ((ToolStripButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((ToolStripButton)item).Tag = obj;
            } else if ((item as ToolStripDropDownButton) != null)
            {
                object obj = ((ToolStripDropDownButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((ToolStripDropDown)item).Tag = obj;
            }
            else if ((item as BarItem) != null)
            {
                object obj = ((BarItem)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((BarItem)item).Tag = obj;
            }
            else if ((item as SimpleButton) != null)
            {
                object obj = ((SimpleButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((SimpleButton)item).Tag = obj;
            }
            else if ((item as DropDownButton) != null)
            {
                object obj = ((DropDownButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((DropDownButton)item).Tag = obj;
            }
            #endregion
            #region Phân quyền trên các control thông tin
            else if ((item as CalcEdit) != null)
            {
                object obj = ((CalcEdit)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((CalcEdit)item).Tag = obj;
            }
            else if ((item as SpinEdit) != null)
            {
                object obj = ((SpinEdit)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((SpinEdit)item).Tag = obj;
            }
            else if ((item as TextEdit) != null)
            {
                object obj = ((TextEdit)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((TextEdit)item).Tag = obj;
            }
            else if ((item as GridColumn) != null)
            {
                object obj = ((GridColumn)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((GridColumn)item).Tag = obj;
            }
            else if ((item as XtraUserControl) != null)
            {
                object obj = ((XtraUserControl)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((XtraUserControl)item).Tag = obj;
            }
            else if ((item as Control) != null)
            {
                object obj = ((Control)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((Control)item).Tag = obj;
            }
            #endregion
            else
            {
                PLMessageBoxDev.ShowMessage("ApplyPermissionObject : Item: " + item.GetType().Name + " chưa được hỗ trợ phân quyền.");
            }
        }
        public static void ApplyPermissionObject(List<Object> list, Object item, PermissionItem permission)
        {
            if (permission == null) return;
            #region Phân quyền trên các control hành động
            if ((item as ToolStripMenuItem) != null)
            {
                object obj = ((ToolStripMenuItem)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((ToolStripMenuItem)item).Tag = obj;
            }
            else if ((item as ToolStripButton) != null)
            {
                object obj = ((ToolStripButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((ToolStripButton)item).Tag = obj;
            } 
            else if ((item as ToolStripDropDownButton) != null)
            {
                object obj = ((ToolStripDropDownButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((ToolStripDropDownButton)item).Tag = obj;
            }
            else if ((item as BarItem) != null)
            {
                object obj = ((BarItem)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((BarItem)item).Tag = obj;
            }
            else if ((item as SimpleButton) != null)
            {
                object obj = ((SimpleButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((SimpleButton)item).Tag = obj;
            }
            else if ((item as DropDownButton) != null)
            {
                object obj = ((DropDownButton)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((DropDownButton)item).Tag = obj;
            }
            #endregion
            #region Phân quyền trên các control thông tin
            else if ((item as CalcEdit) != null)
            {
                object obj = ((CalcEdit)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((CalcEdit)item).Tag = obj;
            }
            else if ((item as SpinEdit) != null)
            {
                object obj = ((SpinEdit)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((SpinEdit)item).Tag = obj;
            }
            else if ((item as TextEdit) != null)
            {
                object obj = ((TextEdit)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((TextEdit)item).Tag = obj;
            }
            else if ((item as GridColumn) != null)
            {
                object obj = ((GridColumn)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((GridColumn)item).Tag = obj;
            }
            else if ((item as XtraUserControl) != null)
            {
                object obj = ((XtraUserControl)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((XtraUserControl)item).Tag = obj;
            }
            else if ((item as Control) != null)
            {
                object obj = ((Control)item).Tag;
                TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", permission);
                ((Control)item).Tag = obj;
            }
            #endregion
            else
            {
                PLMessageBoxDev.ShowMessage("ApplyPermissionObject : Item: " + item.GetType().Name + " chưa được hỗ trợ phân quyền.");
            }
            list.Add(item);
        }
        public static void ApplyPermissionObject(List<Object> list, Object[] items, PermissionItem permission)
        {
            if (items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    ApplyPermissionAction.ApplyPermissionObject(list, items[i], permission);
                }
            }
        }
        public static void ApplyPermissionObject(List<Object> list, Object[] items, PermissionItem[] permission)
        {
            if (items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    ApplyPermissionAction.ApplyPermissionObject(list, items[i], permission[i]);
                }
            }
        }
        #endregion

        #endregion

        #region Hàm tiện ích trong hệ thống phân quyền
        /// <summary> Hàm kiểm tra quyền đối với một mục phân quyền  
        ///     False   Không có quyền trên nó, 
        ///     True    Có quyền trên nó
        ///     null    Không có quyền Read trên nó
        /// </summary>
        public static bool? checkPermission(PermissionItem item)
        {
            bool? result = null;
            if (!User.isAdmin(FrameworkParams.currentUser.username) && FrameworkParams.isPermision!=null)
            {
                if (item == null) return true;
                List<Feature> features = ApplyPermissionAction.GetPermissionFeatures();
                foreach (Feature feature in features)
                {
                    if (feature == null) 
                        return false;
                    if (feature.featureName.Equals(item.featureName))
                    {
                        switch (item.permissionType)
                        {
                            case PermissionType.ADD:
                                result = feature.isInsert;
                                break;
                            case PermissionType.DELETE:
                                result = feature.isDelete;
                                break;
                            case PermissionType.EDIT:
                                result = feature.isUpdate;
                                break;
                            case PermissionType.ADD_EDIT:
                                if (feature.isInsert || feature.isUpdate) 
                                    result = true;
                                else 
                                    result = false;
                                break;
                            case PermissionType.ADD_EDIT_DELETE:
                                if (feature.isInsert || feature.isUpdate || feature.isDelete)
                                    result = true;
                                else
                                    result = false;
                                break;
                            case PermissionType.VIEW_ADD_DELETE_EDIT:
                                if (feature.isInsert || feature.isUpdate || feature.isDelete || feature.isRead)
                                    result = true;
                                else
                                    result = false;
                                break;
                            case PermissionType.EDIT_DELETE:
                                if (feature.isUpdate || feature.isDelete)
                                    result = true;
                                else
                                    result = false;
                                break;
                            case PermissionType.VIEW_HIDE:
                                result = feature.isRead;
                                break;
                            case PermissionType.VIEW:
                                if (feature.isRead == false)
                                   result = null;
                                else 
                                   result = true;
                               break;       
                            case PermissionType.PRINT:
                               result = feature.isPrint;
                               break;
                            case PermissionType.EXPORT:
                               result = feature.isExport;
                               break;
                        }

                        //Tùy vào kết quả sẽ xử lý tiếp.
                        if (result == false)
                        {
                            //Xử lý 1 item chấp nhận nhiều feature khác nhau.
                            foreach(PermissionItem orChildItem in item.OROtherItem){
                                result = checkPermission(orChildItem);
                                if (result == true){
                                    break;
                                }
                            }
                        }
                        else if (result == true) { }
                        else { }
                        return result;
                    }
                }
                PermissionStore.AddMissing(item.featureName);
                return false;
            }
            return true;
        }

        //Danh sách các form không cần kiểm Permission nghĩa là trong form này ko có phân quyền.
        //Có thể tùy biến trong Project thông qua hàm getPublicForm.
        private static List<string> PUBLIC_FORMS = null;
        public static List<string> getPublicForm()
        {
            if (PUBLIC_FORMS == null)
            {
                //Framework
                List<string> publicForms = FWPermission.GetPublicForm();
                if (FrameworkParams.isPermision != null)
                {
                    List<string> forms = FrameworkParams.isPermision.getPublicForm();
                    if (forms != null) publicForms.AddRange(forms);
                }
                //DEVEXPRESS
                publicForms.AddRange(DevPermission.GetPublicForm());

                //Public Form của Plugin
                for (int i = 0; i < PLPlugin.plugins.Count; i++)
                {
                    if (PLPlugin.plugins[i].GetPermission() != null)
                    {
                        List<string> publics = PLPlugin.plugins[i].GetPermission().getPublicForm();
                        if (publics != null) publicForms.AddRange(publics);
                    }
                }
                PUBLIC_FORMS = publicForms;
            }
            return PUBLIC_FORMS;
        }

        //Danh sách các form được map đến chức năng tương ứng trong hệ thống
        public static Dictionary<string, string> FEATURE_MAP = null;
        public static Dictionary<string, string> getFormFeatureMap()
        {
            if (FEATURE_MAP == null)
            {
                //FWPermission
                Dictionary<string, string> project = new Dictionary<string,string>(FWPermission.GetFormFeatureMap());
                //DEVEXPRESS
                Dictionary<string, string> tmp = DevPermission.GetFormFeatureMap();
                foreach (string key in tmp.Keys)
                {
                    if (project.ContainsKey(key) == false)
                        project.Add(key, tmp[key]);
                }
                //Project
                if (FrameworkParams.isPermision != null){
                    tmp = FrameworkParams.isPermision.getFormFeatureMap();
                    foreach (string key in tmp.Keys)
                    {
                        if(project.ContainsKey(key)==false) 
                            project.Add(key, tmp[key]);
                    }
                }                                    
                if (PLPlugin.plugins != null)
                {
                    for (int i = 0; i < PLPlugin.plugins.Count; i++)
                    {
                        try
                        {
                            Dictionary<string, string> dic = PLPlugin.plugins[i].GetPermission().getFormFeatureMap();
                            if (dic != null)
                            {
                                foreach (string key in dic.Keys)
                                {
                                    string value = "";
                                    dic.TryGetValue(key, out value);
                                    if(project.ContainsKey(key)==false) project.Add(key, value);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            PLException.AddException(ex);
                        }
                    }
                }
                FEATURE_MAP = project;
            }
            return FEATURE_MAP;
        }

        //Bản quyền của người dùng đối với các chức năng của chương trình.
        //Không bao gồm quyền đối với report
        private static List<Feature> PermissionFeatures = null;
        public static void ClearPermissionFeatures()
        {
            PermissionFeatures = null;
        }
        public static List<Feature> GetPermissionFeatures()
        {
            if (PermissionFeatures == null)
            {
                PermissionFeatures = Feature.loadAllFeatureByUser(FrameworkParams.currentUser.username);
            }
            return PermissionFeatures;
        }

        //Form thông báo tương ứng khi truy cập vào tài nguyên không được phép
        public static frmPermissionFail getPermissionFormFail()
        {
            if (FrameworkParams.wait != null) FrameworkParams.wait.Finish();
            return new frmPermissionFail();
        }
        #endregion

        //????
        public static List<object> GetGlobalObjectItems(XtraForm form)
        {
            List<object> objs = FrameworkParams.isPermision.GetObjectItems(form);
            //Framework
            FWPermission.GetObjectItems(form, objs);
            //DEVEXPRESS
            DevPermission.GetObjectItems(form, objs);

            if (objs == null) objs = new List<object>();
            //Public Form của Plugin
            for (int i = 0; i < PLPlugin.plugins.Count; i++)
            {
                if (PLPlugin.plugins[i].GetPermission() != null)
                {
                    List<object> publics = PLPlugin.plugins[i].GetPermission().GetObjectItems(form);
                    if (publics != null) objs.AddRange(publics);
                }
            }
            return objs;
        }



        #region Hướng Control : Default dành cho tất cả các FORM.
        /// <summary>Hàm cho phép áp dụng phân quyền trên Ctrl
        /// </summary>
        [Obsolete("Dùng applyElement")]
        public override bool applyControl(Control Ctrl, XtraForm form)
        {
            if (!User.isAdmin(user.username))
            {
                PermissionItem tagValue = null;

                #region Phân quyền trên SimpleButton
                if ((Ctrl as DevExpress.XtraEditors.SimpleButton) != null)
                {
                    tagValue = null;
                    if (TagPropertyMan.Get(((DevExpress.XtraEditors.SimpleButton)Ctrl).Tag, "SECURITY") != null)
                        tagValue = (PermissionItem)TagPropertyMan.Get(((DevExpress.XtraEditors.SimpleButton)Ctrl).Tag, "SECURITY");
                    bool? result = checkPermission(tagValue);
                    if (result == null) return false;
                    if (result == false)
                    {
                        ((DevExpress.XtraEditors.SimpleButton)Ctrl).Visible = false;
                        if (tagValue.failAction != null)
                        {
                            tagValue.failAction();
                        }
                    }
                }
                #endregion

                #region Phân quyền trên ToolStrip Item
                else if ((Ctrl as System.Windows.Forms.ToolStrip) != null)
                {
                    foreach (System.Windows.Forms.ToolStripItem ctrlItem in ((System.Windows.Forms.ToolStrip)Ctrl).Items)
                    {
                        tagValue = null;
                        if (TagPropertyMan.Get(ctrlItem.Tag, "SECURITY") != null)
                            tagValue = (PermissionItem)TagPropertyMan.Get(ctrlItem.Tag, "SECURITY");
                        bool? result = checkPermission(tagValue);
                        if (result == null) return false;
                        if (result == false)
                        {
                            ctrlItem.Visible = false;
                            if (tagValue.failAction != null)
                            {
                                tagValue.failAction();
                            }
                        }
                    }
                }
                #endregion

                #region Phân quyền trên Bar thông qua BarDockControl
                else if ((Ctrl as BarDockControl) != null)
                {
                    BarManager barMan = ((BarDockControl)Ctrl).Manager;
                    if (barMan != null)
                    {
                        foreach (Bar bar in barMan.Bars)
                        {
                            foreach (LinkPersistInfo linkInfo in bar.LinksPersistInfo)
                            {
                                tagValue = null;
                                BarItem barItem = linkInfo.Item;

                                if (TagPropertyMan.Get(barItem.Tag, "SECURITY") != null)
                                    tagValue = (PermissionItem)TagPropertyMan.Get(barItem.Tag, "SECURITY");
                                bool? result = checkPermission(tagValue);
                                if (result == null) return false;
                                if (result == false)
                                {
                                    barItem.Visibility = BarItemVisibility.Never;
                                    if (tagValue.failAction != null)
                                    {
                                        tagValue.failAction();
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                else
                {
                    PLMessageBoxDev.ShowMessage(form.Name + @" cài đặt XtraFormPL và tiến hành phân quyền hoặc đưa nó vào Public Form!");
                }
            }
            return true;
        }

        /// <summary>Khai báo quyền đòi hỏi trên một Control
        /// </summary>
        public static void ApplyPermissionCtrl(List<Control> list, Control ctrl, PermissionItem item)
        {
            object obj = ctrl.Tag;
            TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", item);
            ctrl.Tag = obj;

            list.Add(ctrl);
        }
        #endregion
    }

    public class HelpPermission
    {
        #region Giải pháp phân quyền trên Danh mục
        [Obsolete("Dùng CategoryPermission")]
        public static DelegationLib.DefinePermission CategoryPermissionReadEditCommit(XtraUserControl DanhMuc, string mainFeature)
        {
            if (DanhMuc is DMGrid)
            {
                DMGrid basic = (DMGrid)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    return items;
                };
            }
            else if (DanhMuc is DMTreeGroup)
            {
                DMTreeGroup basic = (DMTreeGroup)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.addSameLevel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnLuu,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnKhongLuu,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    return items;
                };
            }
            else if (DanhMuc is DMTreeGroupElement)
            {
                DMTreeGroupElement basic = (DMTreeGroupElement)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    return items;
                };
            }

            PLMessageBoxDev.ShowMessage("Loại control danh mục " + DanhMuc.GetType().Name + " chưa được hỗ trợ");
            return null;
        }
        [Obsolete("Dùng CategoryPermission")]
        public static DelegationLib.DefinePermission CategoryPermissionReadEdit(XtraUserControl DanhMuc, string mainFeature)
        {
            if (DanhMuc is DMGrid)
            {
                DMGrid basic = (DMGrid)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    return items;
                };
            }
            else if (DanhMuc is DMTreeGroup)
            {
                DMTreeGroup basic = (DMTreeGroup)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.addSameLevel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnLuu,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnKhongLuu,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    return items;
                };
            }
            else if (DanhMuc is DMTreeGroupElement)
            {
                DMTreeGroupElement basic = (DMTreeGroupElement)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    return items;
                };
            }

            PLMessageBoxDev.ShowMessage("Loại control danh mục " + DanhMuc.GetType().Name + " chưa được hỗ trợ");
            return null;
        }

        [Obsolete("Dùng CategoryPermission")]
        public static DelegationLib.DefinePermission CategoryPermission(XtraUserControl DanhMuc, string mainFeature)
        {
            #region DMGrid
            if (DanhMuc is DMGrid)
            {
                DMGrid basic = (DMGrid)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.DELETE));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        new PermissionItem(mainFeature, PermissionType.ADD_EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        new PermissionItem(mainFeature, PermissionType.ADD_EDIT));

                    return items;
                };
            }
            #endregion
            #region DMTreeGroup
            else if (DanhMuc is DMTreeGroup)
            {
                DMTreeGroup basic = (DMTreeGroup)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.addSameLevel,
                        new PermissionItem(mainFeature, PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.DELETE));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnLuu,
                        new PermissionItem(mainFeature, PermissionType.ADD_EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnKhongLuu,
                        new PermissionItem(mainFeature, PermissionType.ADD_EDIT));

                    return items;
                };
            }
            #endregion
            #region DMTreeGroupElement
            else if (DanhMuc is DMTreeGroupElement)
            {
                DMTreeGroupElement basic = (DMTreeGroupElement)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        new PermissionItem(mainFeature, PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        new PermissionItem(mainFeature, PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        new PermissionItem(mainFeature, PermissionType.DELETE));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        new PermissionItem(mainFeature, PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        new PermissionItem(mainFeature, PermissionType.ADD_EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        new PermissionItem(mainFeature, PermissionType.ADD_EDIT));

                    return items;
                };
            }
            #endregion

            PLMessageBoxDev.ShowMessage("Loại danh mục mới " + DanhMuc.GetType().Name + " chưa được hỗ trợ");
            return null;
        }

        public static DelegationLib.DefinePermission CategoryPermission(XtraUserControl DanhMuc, string mainFeature, string description)
        {
            Permission per = new Permission(mainFeature, description);
                        
            if (DanhMuc is PLDMGrid)
            {
                PLDMGrid basic1 = (PLDMGrid)DanhMuc;
                DMGrid basic = basic1.GetDMGrid;

                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    //Lap vo tan xem lai tai sao ???
                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        per.GetPermissionItem(PermissionType.VIEW));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        per.GetPermissionItem(PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        per.GetPermissionItem(PermissionType.DELETE));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        per.GetPermissionItem(PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSelect,
                        per.GetPermissionItem(PermissionType.VIEW));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSelect,
                        per.GetPermissionItem(PermissionType.VIEW));

                    return items;
                };
            }
            #region DMGrid
            else if (DanhMuc is DMGrid)
            {
                DMGrid basic = (DMGrid)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        per.GetPermissionItem(PermissionType.VIEW));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        per.GetPermissionItem(PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        per.GetPermissionItem(PermissionType.DELETE));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        per.GetPermissionItem(PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));

                    return items;
                };
            }
            #endregion
            #region DMTreeGroup
            else if (DanhMuc is DMTreeGroup)
            {
                DMTreeGroup basic = (DMTreeGroup)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        per.GetPermissionItem(PermissionType.VIEW));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        per.GetPermissionItem(PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.addSameLevel,
                        per.GetPermissionItem(PermissionType.ADD));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        per.GetPermissionItem(PermissionType.DELETE));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        per.GetPermissionItem(PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnLuu,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnKhongLuu,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));

                    return items;
                };
            }
            #endregion
            #region DMTreeGroupElement
            else if (DanhMuc is DMTreeGroupElement)
            {
                DMTreeGroupElement basic = (DMTreeGroupElement)DanhMuc;
                return delegate(object formCategory)
                {
                    List<Object> items = new List<Object>();

                    ApplyPermissionAction.ApplyPermissionObject(items, basic,
                        per.GetPermissionItem(PermissionType.VIEW));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnAdd,
                        per.GetPermissionItem(PermissionType.ADD));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnDel,
                        per.GetPermissionItem(PermissionType.DELETE));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnUpdate,
                        per.GetPermissionItem(PermissionType.EDIT));

                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnSave,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));
                    ApplyPermissionAction.ApplyPermissionObject(items, basic.btnNoSave,
                        per.GetPermissionItem(PermissionType.ADD_EDIT));

                    return items;
                };
            }
            #endregion

            PLMessageBoxDev.ShowMessage("Loại danh mục " + DanhMuc.GetType().Name + " chưa được hỗ trợ");

            return null;
        }

        
        #endregion

        public static bool CheckCtrl(XtraUserControl control)
        /// <summary>Cập nhập giao diện ứng với phân quyền của control      
        /// True : Cho phép hiện thị control đó
        /// False: Không cho phép hiển thị control đó
        /// </summary>
        {
            if (FrameworkParams.isPermision == null) return true;

            try
            {
                if (control is IPermisionable)
                {
                    IPermisionable per = (IPermisionable)control;
                    List<Object> objs = per.GetObjectItems();
                    if (objs != null)
                    {
                        ApplyPermissionAction a = new ApplyPermissionAction(null);
                        //Cập nhật control tương ứng phân quyền
                        bool flag = true;
                        for (int i = 0; i < objs.Count; i++)
                        {
                            flag = flag && a.applyElement(objs[i], null);
                            //if (objs[i] != control)
                            //    flag = flag && a.applyElement(objs[i], null);
                            //else
                            //{
                            //    if (a.applyElement(objs[i], null) == false)
                            //        return false;
                            //}
                        }
                        if (flag == true)
                        {
                            if (control is DMGrid)
                            //Trường hợp đặc biệt nên bỏ sự kiện double click
                            {
                                DMGrid c = (DMGrid)control;
                                c.BasicTemplate.SupportDoubleClick = c.btnUpdate.Visible;
                            }
                            else if (control is DMTreeGroupElement)
                            {
                                DMTreeGroupElement c = (DMTreeGroupElement)control;
                                c._IsDrapDrop = c.btnUpdate.Visible;
                            }
                            //return true;
                            return control.Visible;
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                PLMessageBoxDev.ShowMessage("Lỗi cấu hình phân quyền");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiem tra xem formName co nam trong danh sach Public Form khong.
        /// No khong bao gom cac Form la IPublic.
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public static bool CanShowForm(String formName)
        {
            if (FrameworkParams.isPermision == null) return true;

            List<string> publicForms = ApplyPermissionAction.getPublicForm();
            if (publicForms.Contains(formName))
            {
                return true;
            }
            
            //Check Permission cho truy cập
            Dictionary<string, string> formFeatureMap = ApplyPermissionAction.getFormFeatureMap();
            string featureName = "";
            if (formFeatureMap.ContainsKey(formName))
            {
                featureName = formFeatureMap[formName];
                if (featureName == null)
                {
                    PLMessageBoxDev.ShowMessage("Lập danh sách featureMap bị sai tại form " + formName);
                }
                string per = "";
                if (featureName.Contains(";"))
                {
                    per = featureName.Substring(featureName.IndexOf(';') + 1).Trim();
                    featureName = featureName.Substring(0, featureName.IndexOf(';')).Trim();
                }

                PermissionType PerType = PermissionType.VIEW;
                if (per.Equals("VIEW"))
                {
                    PerType = PermissionType.VIEW;
                }
                else if (per.Equals("ADD"))
                {
                    PerType = PermissionType.ADD;
                }
                else if (per.Equals("DELETE"))
                {
                    PerType = PermissionType.DELETE;
                }
                else if (per.Equals("EDIT"))
                {
                    PerType = PermissionType.EDIT;
                }
                else if (per.Equals("PRINT"))
                {
                    PerType = PermissionType.PRINT;
                }
                else if (per.Equals("EXPORT"))
                {
                    PerType = PermissionType.EXPORT;
                }

                PermissionItem item = new PermissionItem(featureName, PerType);
                bool? flag = ApplyPermissionAction.checkPermission(item);
                if (flag == null || flag == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            PLException.AddException(new Exception("Tên " + formName + " không có trong featureMap. Thêm form này vào trong getFormFeatureMap của PLPermission"));
            return true;
        }
        /// <summary>
        /// Kiem tra 1 form co phai la public form
        /// trong toan bo he thong dua vao danh sach public form
        /// va loai form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static bool CanShowForm(XtraForm form)
        {
            if (form != null)
            {
                if (form is IPublicForm) return true;
                string formName = form.GetType().FullName;
                return CanShowForm(formName);
            }
            else
            {
                return false;
            }
        }

        public static bool? Check(PermissionItem item)
        /// <summary> Hàm kiểm tra quyền đối với một mục phân quyền  
        ///     False   Không có quyền trên nó, 
        ///     True    Có quyền trên nó
        ///     Null    Không có quyền Read trên nó
        /// </summary>
        {
            return ApplyPermissionAction.checkPermission(item);
        }
    }
}