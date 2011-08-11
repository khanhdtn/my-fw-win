using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public class TrialPLDataTreeNew:PopupContainerEdit
    {
        private PopupContainerControl popupContainerControl;
        private UserControlDataTree dataTree;
        public static bool isClosePopup = false;
        static TrialPLDataTreeNew()
        {
            RepositoryItemDataTreeNew.Register();
        }
        public TrialPLDataTreeNew()
        {
            popupContainerControl = new PopupContainerControl();
            dataTree = new UserControlDataTree();
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
            UserControlDataTree.valueFocus = this.EditValue;
            dataTree._focusRow();
            base.ShowPopup();
        }
        public int _getId()
        {
            return dataTree._getId(this.EditValue);
        }
    }
}
