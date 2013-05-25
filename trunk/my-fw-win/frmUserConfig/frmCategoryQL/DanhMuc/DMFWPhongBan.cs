using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.DanhMuc
{
    #region SQL

    #endregion
    public class DMFWPhongBan:IDanhMuc
    {
        public static DMFWPhongBan I = new DMFWPhongBan();
        public static String N = typeof(DMFWPhongBan).FullName;
        public static bool isPermission = false;
        #region IDanhMuc Members       

        public string Item()
        {
            return
            @"<cat table='" + HelpFURL.FURL(N, "Init") + @"' type='action' picindex='navPhongBan.png'>
              <lang id='vn'>Phòng ban</lang>
              <lang id='en'></lang>
            </cat>";
        }

        public string MenuItem(string mainCat, string parentID, bool isSep)
        {
            return MenuItem(mainCat, "Phòng ban", parentID, isSep);
        }

        public string MenuItem(string mainCat, string title, string parentID, bool isSep)
        {
            return MenuBuilder.CreateItem(N, title, parentID, true,
                           HelpFURL.FURL(mainCat, N, "Init"), true, isSep, "navPhongBan.png", false, "", "");
        }

        #region Init      

        public DevExpress.XtraEditors.XtraUserControl Init()
        {
            DMTreeGroup dmTree = new DMTreeGroup();
            dmTree.Init(GroupElementType.ONLY_INPUT, "DEPARTMENT", "ID", "PARENT_ID",
                new string[] { "NAME" }, new String[] { "Tên phòng ban" }, HelpGen.G_FW_DM_ID,
                null,
                new FieldNameCheck[] { 
                    new FieldNameCheck("NAME",
                        new CheckType[]{ CheckType.Required, CheckType.RequireMaxLength },
                        "Tên phòng ban", 
                    new object[]{ null, 200 })
                }
            );            
            if(isPermission) dmTree.DefinePermission(DanhMucParams.GetPermission(dmTree, N, "Danh má»¥c phÃ²ng ban"));
            return dmTree;
        }
        #endregion

        #endregion

       
    }
}
