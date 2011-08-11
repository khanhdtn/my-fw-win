using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public class TrialPLDataTreeNewExt:PopupContainerEdit
    {
        private PopupContainerControl popupContainerControl;
        private UserControlDataTreeExt dataTree;
        public static bool isClosePopup = false;
        static TrialPLDataTreeNewExt()
        {
            RepositoryItemDataTreeNew.Register();
        }
        public TrialPLDataTreeNewExt()
        {
            popupContainerControl = new PopupContainerControl();
            dataTree = new UserControlDataTreeExt();
            popupContainerControl.Controls.Add(dataTree);
            this.Properties.PopupControl = popupContainerControl;
            popupContainerControl.Size = dataTree.Size;
            this.Properties.PopupSizeable = false;
            this.Properties.ShowPopupCloseButton = false;
            dataTree.PopupContainerEdit = this;
            this.Properties.NullText = GlobalConst.NULL_TEXT;
        }

        public void _init(XtraForm danhMucForm , string TableName , int[] RootID , string IDField , string IDParentField , string[] VisibleFields , string[] Captions , string getField)
        {
            dataTree._BuildTree(danhMucForm , TableName , RootID , IDField , IDParentField , VisibleFields , Captions , getField);
        }

        public void _init(XtraForm danhMucForm, string btnCaption, UserControlDataTreeExt.GetValue function,  string TableName, int[] RootID, string IDField, string IDParentField, string[] VisibleFields, string[] Captions, string getField)
        {
            dataTree._BuildTree(danhMucForm,btnCaption,function, TableName, RootID, IDField, IDParentField, VisibleFields, Captions, getField);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            isClosePopup = true;
        }
        protected override void OnGotFocus(EventArgs e)
        {
            isClosePopup = true;
            base.OnGotFocus(e);
        }
        protected override void DoClosePopup(PopupCloseMode closeMode)
        {
            if (closeMode != PopupCloseMode.Immediate || isClosePopup)
            {
                this.DestroyPopupForm();
                isClosePopup = false;
            }
        }
        public override void ShowPopup()
        {
            UserControlDataTreeExt.valueFocus = this.EditValue;
            dataTree._focusRow();
            base.ShowPopup();
        }
        public int _getId()
        {
            return dataTree._getId(this.EditValue);            
        }
    }
}
