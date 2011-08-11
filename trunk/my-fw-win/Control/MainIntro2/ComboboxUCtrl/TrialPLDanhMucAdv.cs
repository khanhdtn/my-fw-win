using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Data;
using System.ComponentModel;

using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    public class TrialPLDanhMucAdv : PopupContainerEdit, ISelectionControl, IIDValidation
    {
        private PopupContainerControl popupContainerControl;
        private TrialPLDanhMucAdvCtrl plDanhMuc;
        public static bool isClosePopup = false;

        static TrialPLDanhMucAdv()
        {
            RepositoryItemDanhMucAdv.Register();
        }

        public TrialPLDanhMucAdv()
            : base()
        {
            popupContainerControl = new PopupContainerControl();
            plDanhMuc = new TrialPLDanhMucAdvCtrl();
            this.Properties.PopupControl = popupContainerControl;
            popupContainerControl.Controls.Add(plDanhMuc);
            popupContainerControl.Size = plDanhMuc.Size;
            this.Properties.PopupSizeable = false;
            this.Properties.ShowPopupCloseButton = false;
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
        //Xu ly khi dong close popup
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
            TrialPLDanhMucAdvCtrl.valueFocus = this.EditValue;
            plDanhMuc._focusRow();
            base.ShowPopup();
        }
        public void _init(XtraForm frmDanhMuc,string tableName,string fieldId, string[] visibleField,string[] caption,string getfield )
        {
            plDanhMuc.PopupContainerEdit = this;
            plDanhMuc._init(frmDanhMuc,tableName,fieldId,visibleField,caption,getfield);
        }

        public int _getId()
        {
            return plDanhMuc._getId(this.EditValue);
        }

        #region ISelectionControl Members

        public long _getSelectedID()
        {
            throw new NotImplementedException();
        }

        public void _setSelectedID(long id)
        {
            throw new NotImplementedException();
        }

        public object _getSelectedValue()
        {
            throw new NotImplementedException();
        }

        public void _setSelectedValue(object data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPLControl Members

        public void _init()
        {
            throw new NotImplementedException();
        }

        public void _refresh()
        {
            throw new NotImplementedException();
        }

        public string _getValidateData()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IValidation Members

        public void SetError(DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider, string errorMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISelectionControl Members


        public void _refresh(object NewSrc)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
