using System.Drawing;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using System;
namespace ProtocolVN.Framework.Win
{
    public class FWImageDic
    {
        internal static Image LOGO_IMAGE16 = ResourceMan.getImage16("logo.gif");
        internal static Image LOGO_IMAGE20 = ResourceMan.getImage20("logo.gif");
        internal static Image LOGO_IMAGE48 = ImageCollectionMan.Instance.GetImage4848("logo.gif");
        internal static Image LOGO_IMAGE32 = ImageCollectionMan.Instance.GetImage3232("logo.gif");

        #region ResourceMan Các Image ở đây là ko thay đổi được
        //public static Image SAVE_ALL_IMAGE48 = ResourceMan.getImage48("fwSaveAll.png");
        //public static Image ADD_ALL_IMAGE48 = ResourceMan.getImage48("fwAddAll.png");
        //public static Image REMOVE_IMAGE48 = ResourceMan.getImage48("fwRemove.png");
        //public static Image REMOVE_ALL_IMAGE48 = ResourceMan.getImage48("fwRemoveAll.png");
        //public static Image LOGIN_IMAGE48 = ResourceMan.getImage48("fwLogin.png");
        //public static Image CONNECT_IMAGE48 = ResourceMan.getImage48("fwConnect.png");
        //public static Image CONFIG_IMAGE48 = ResourceMan.getImage48("fwConfig.png");
        //public static Image CHOICE_POPUP_IMAGE48 = ResourceMan.getImage48("fwChoicePopup.png");
        //public static Image CHOICE_IMAGE48 = ResourceMan.getImage48("fwChoice.png");
        //public static Image NO_CHOICE_IMAGE48 = ResourceMan.getImage48("fwNoChoice.png");
        //public static Image DETAIL_IMAGE48 = ResourceMan.getImage48("fwDetail.png");
        //public static Image PREVIEW_IMAGE48 = ResourceMan.getImage48("fwPreview.png");
        //public static Image FILTER_IMAGE48 = ResourceMan.getImage48("fwFilter.png");
        //public static Image FIND_IMAGE48 = ResourceMan.getImage48("fwFind.png");


        public static Image SAVE_ALL_IMAGE20 = HelpImage.getImage2020("fwSaveAll.png");
        public static Image EXIT_IMAGE20 = HelpImage.getImage2020("fwExit.png");
        public static Image ADD_ALL_IMAGE20 = HelpImage.getImage2020("fwAddAll.png");
        public static Image REMOVE_IMAGE20 = HelpImage.getImage2020("fwRemove.png");
        public static Image REMOVE_ALL_IMAGE20 = HelpImage.getImage2020("fwRemoveAll.png");
        public static Image LOGIN_IMAGE20 = HelpImage.getImage2020("fwLogin.png");
        public static Image CONNECT_IMAGE20 = HelpImage.getImage2020("fwConnect.png");
        public static Image CONFIG_IMAGE20 = HelpImage.getImage2020("fwConfig.png");
        public static Image CHOICE_POPUP_IMAGE20 = HelpImage.getImage2020("fwChoicePopup.png");
        
        
        public static Image DETAIL_IMAGE20 = HelpImage.getImage2020("fwDetail.png");
        public static Image PREVIEW_IMAGE20 = HelpImage.getImage2020("fwPreview.png");
        
        /// <summary>Khi chọn vào nó cho phép cập nhật 1 ptu
        /// </summary>
        public static Image EDIT_IMAGE16 = HelpImage.getImage1616("fwEdit.png");
        /// <summary>Khi chọn vào nó sẽ thêm mới một đối tương con -> giống như New Child
        /// </summary>
        public static Image ADD_CHILD_IMAGE16 = HelpImage.getImage1616("fwAddChild.png");
        public static Image ADD_ALL_IMAGE16 = HelpImage.getImage1616("fwAddAll.png");
        /// <summary>Khi chọn vào nó sẽ loại bỏ liên kết với ptu chứ không xóa hẵn xóa dùng Delete
        /// </summary>
        public static Image REMOVE_IMAGE16 = HelpImage.getImage1616("fwRemove.png");
        public static Image REMOVE_ALL_IMAGE16 = HelpImage.getImage1616("fwRemoveAll.png");

        /// <summary>Khi chọn vào nó sẽ hiển thị popup cho chọn thông tin       
        /// </summary>
        public static Image CHOICE_POPUP_IMAGE16 = HelpImage.getImage1616("fwChoicePopup.png");

        /// <summary>Khi chọn vào nó sẽ hiển thị các Nghiệp vụ liên quan
        /// </summary>
        public static Image BUSINESS_IMAGE16 = HelpImage.getImage1616("fwBusiness.png");
        /// <summary>Khi chọn vào nó sẽ duyệt / chấp nhận
        /// </summary>
        public static Image COMMIT_IMAGE16 = HelpImage.getImage1616("fwCommit.png");
        /// <summary>Khi chọn vào nó sẽ không duyệt / không chấp nhận
        /// </summary>
        public static Image UNCOMMIT_IMAGE16 = HelpImage.getImage1616("fwUnCommit.png");
        /// <summary>Hình biểu diễn trạng thái DUYET       
        /// </summary>

        public static Image WARN_IMAGE16 = HelpImage.getImage1616("tbsWarning.png");
        public static Image TOOLTIP_WARN_IMAGE16 = HelpImage.getImage1616("tbsToolTipWarning.png");
        #endregion

        #region HelpImage

        /// <summary>Thực hiện công việc xem chi tiết
        /// </summary>       
        public static Image DETAIL_IMAGE16 = HelpImage.getImage1616("fwDetail.png");
        
        
        /// <summary>Thực hiện công việc liên quan đến điều kiện tìm
        /// </summary>
        public static Image FILTER_IMAGE16 = HelpImage.getImage1616("fwFilter.png");
        public static Image FILTER_IMAGE20 = HelpImage.getImage2020("fwFilter.png");


        /// <summary>Thực hiện công việc liên quan đến tìm kiếm
        /// </summary>
        public static Image FIND_IMAGE16 = HelpImage.getImage1616("fwFind.png");
        public static Image FIND_IMAGE20 = HelpImage.getImage2020("fwFind.png");


        /// <summary>Thực hiện công việc liên quan đến in
        /// </summary>
        public static Image PRINT_IMAGE20 = HelpImage.getImage2020("fwPrint.png");

        public static Image CHART_IMAGE20 = HelpImage.getImage2020("chart.png");

        /// <summary>Thực hiện công việc xem trước khi in
        /// </summary>
        public static Image PRINT_PREVIEW_IMAGE20 = HelpImage.getImage2020("fwPrintPreview.png");
        
        
        /// <summary>Thực hiện công việc chọn các nút chức năng
        /// </summary>
        public static Image BUSINESS_IMAGE20 = HelpImage.getImage2020("fwBusiness.png");


        /// <summary>Thực hiện khi chọn
        /// </summary>
        public static Image CHOICE_IMAGE16 = HelpImage.getImage1616("fwChoice.png");
        public static Image CHOICE_IMAGE20 = HelpImage.getImage2020("fwChoice.png");


        
        /// <summary>Thực hiện khi bỏ chọn
        /// </summary>
        public static Image NO_CHOICE_IMAGE16 = HelpImage.getImage1616("fwNoChoice.png");
        public static Image NO_CHOICE_IMAGE20 = HelpImage.getImage2020("fwNoChoice.png");


        public static Image COMMIT_IMAGE20 = HelpImage.getImage2020("fwCommit.png");        
        public static Image UNCOMMIT_IMAGE20 = HelpImage.getImage2020("fwUnCommit.png");

        
        public static Image REFRESH_IMAGE20 = HelpImage.getImage2020("fwRefresh.png");
        
        public static Image ADD_CHILD_IMAGE48 = HelpImage.getImage4848("fwAddChild.png");        
        public static Image EXPORT_TO_FILE_IMAGE20 = HelpImage.getImage2020("fwExportToFile.png");
        
        #endregion

        #region Global
        public static Image NO_SAVE_IMAGE16 = global::ProtocolVN.Framework.Win.Properties.Resources.fwNoSave;
        public static Image DUYET_IMAGE16 = global::ProtocolVN.Framework.Win.Properties.Resources.fwDuyet;
        public static Image CHUA_DUYET_IMAGE16 = global::ProtocolVN.Framework.Win.Properties.Resources.fwChuaDuyet.ToBitmap();
        public static Image KHONG_DUYET_IMAGE16 = global::ProtocolVN.Framework.Win.Properties.Resources.fwKhongDuyet;
        public static Icon FAX_16 = global::ProtocolVN.Framework.Win.Properties.Resources.fwFax;
        public static Icon DATA_REPORT_16 = global::ProtocolVN.Framework.Win.Properties.Resources.fwDataReport;
        public static Icon CONFIG_REPORT_16 = global::ProtocolVN.Framework.Win.Properties.Resources.fwConfigReport;
        #endregion

        #region Win Images
        public static Image SAVE_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwSave.png");
        public static Image SAVE_ALL_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwSaveAll.png");
        public static Image CLOSE_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwClose.PNG");
        public static Image EXIT_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwExit.png");
        public static Image DELETE_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwDelete.png");
        public static Image ADD_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("Add.png");
        public static Image LOGIN_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwLogin.png");
        public static Image CONNECT_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwConnect.png");
        public static Image CONFIG_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwConfig.png");
        public static Image PRINT_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwPrint.png");
        public static Image REFRESH_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwRefresh.png");
        public static Image INFO_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwInfo.png");
        public static Image PRINT_PREVIEW_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwPrintPreview.png");
        public static Image PREVIEW_IMAGE16 = ImageCollectionMan.Instance.GetImage1616("fwPreview.png");

        public static Image SAVE_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("fwSave.png");
        public static Image NO_SAVE_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("fwDelete.png");
        public static Image CLOSE_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("fwClose.PNG");
        public static Image DELETE_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("fwDelete.png");
        public static Image EDIT_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("fwEdit.png");
        public static Image ADD_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("add.png");
        public static Image VIEW_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("fwPreview.png");
        public static Image ADD_CHILD_IMAGE20 = ImageCollectionMan.Instance.GetImage2020("fwAddChild.png");
        
        
        
        //public static Image SAVE_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(11);
        //public static Image NO_SAVE_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(12);
        //public static Image CLOSE_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(13);
        //public static Image DELETE_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(14);
        //public static Image EDIT_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(15);
        //public static Image ADD_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(10);
        //public static Image PRINT_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(16);
        //public static Image PRINT_PREVIEW_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(17);
        //public static Image BUSINESS_IMAGE48 = ResourceMan.getImage48("fwBusiness.png");
        //public static Image COMMIT_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(18);
        //public static Image UNCOMMIT_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(19);
        //public static Image EXIT_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(13);
        //public static Image VIEW_IMAGE48 = ImageCollectionMan.Instance.GetImage4848(20);
        
        #endregion


        












        private static Image GROUP_OPEN = HelpImage.getImage4848("fwGroupOpen.png");
        private static Image GROUP_CLOSE = HelpImage.getImage4848("fwGroupClose.png");
        private static Image ELEMENT = HelpImage.getImage4848("fwElement.png");
        public static void GET_GROUP_ELEM_IMAGE16(ImageList images)
        {
            images.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            images.ImageSize = new System.Drawing.Size(16, 16);
            images.TransparentColor = System.Drawing.Color.Transparent;
            images.Images.Add("GROUP_OPEN", GROUP_OPEN);
            images.Images.Add("GROUP_CLOSE", GROUP_CLOSE);
            images.Images.Add("ELEMENT", ELEMENT);
        }



        public static void GET_DUYET_STATUS16(DevExpress.Utils.ImageCollection images)
        {
            images.Images.Add(CHUA_DUYET_IMAGE16);
            images.Images.Add(DUYET_IMAGE16);
            images.Images.Add(KHONG_DUYET_IMAGE16);
        }




        #region Di chuyển HelpImage
        //public static Image getImage1616(String name)
        //{
        //    int num = HelpNumber.ParseInt32(name);
        //    Image image = null;
        //    if (num < 0)
        //    {
        //        if (FrameworkParams.imageStore != null)
        //        {
        //            image = FrameworkParams.imageStore.GetImage1616(name);
        //            if (image == null) image = ImageCollectionMan.Instance.GetImage1616(name);
        //        }
        //        else
        //        {
        //            image = ResourceMan.getImage16(name);
        //        }
        //    }
        //    else
        //    {
        //        image = ImageCollectionMan.Instance.GetImage1616(num);
        //    }
        //    if (image == null) image = FWImageDic.LOGO_IMAGE16;

        //    return image;
        //}

        //public static Image getImage2020(String name)
        //{
        //    int num = HelpNumber.ParseInt32(name);
        //    Image image = null;
        //    if (num < 0)
        //    {
        //        if (FrameworkParams.imageStore != null)
        //        {
        //            image = FrameworkParams.imageStore.GetImage2020(name);
        //            if (image == null) image = ImageCollectionMan.Instance.GetImage2020(name);
        //        }
        //        else
        //        {
        //            image = ResourceMan.getImage20(name);
        //        }
        //    }
        //    else
        //    {
        //        image = ImageCollectionMan.Instance.GetImage2020(num);
        //    }
        //    if (image == null) image = FWImageDic.LOGO_IMAGE20;

        //    return image;
        //}

        //public static Image getImage3232(String name)
        //{
        //    int num = HelpNumber.ParseInt32(name);
        //    Image image = null;
        //    if (num < 0)
        //    {
        //        if (FrameworkParams.imageStore != null)
        //        {
        //            image = FrameworkParams.imageStore.GetImage3232(name);
        //            if (image == null) image = ImageCollectionMan.Instance.GetImage3232(name);
        //        }
        //        else
        //        {
        //            image = ResourceMan.getImage32(name);
        //        }
        //    }
        //    else
        //    {
        //        image = ImageCollectionMan.Instance.GetImage3232(num);
        //    }
        //    if (image == null) image = FWImageDic.LOGO_IMAGE32;

        //    return image;
        //}

        //public static Image getImage4848(String name)
        //{
        //    int num = HelpNumber.ParseInt32(name);
        //    Image image = null;
        //    if (num < 0)
        //    {
        //        if (FrameworkParams.imageStore != null)
        //        {
        //            image = FrameworkParams.imageStore.GetImage4848(name);
        //            if (image == null) image = ImageCollectionMan.Instance.GetImage4848(name);
        //        }
        //        else
        //        {
        //            image = ResourceMan.getImage48(name);
        //        }
        //    }
        //    else
        //    {
        //        image = ImageCollectionMan.Instance.GetImage4848(num);
        //    }
        //    if (image == null) image = FWImageDic.LOGO_IMAGE48;

        //    return image;
        //}
        
        //public static Image ADD_CHILD_IMAGE20 = ResourceMan.getImage20("fwAddChild.png");
        //public static Image VIEW_IMAGE16 = ResourceMan.getImage16("fwView.png");
        //public static Image VIEW_IMAGE20 = ResourceMan.getImage20("fwView.png");
        //public static Image SAVE_IMAGE48 = ResourceMan.getImage48("fwSave.png");
        //public static Image NO_SAVE_IMAGE48 = ResourceMan.getImage48("fwNoSave.png");
        //public static Image CLOSE_IMAGE48 = ResourceMan.getImage48("fwClose.png");
        //public static Image DELETE_IMAGE48 = ResourceMan.getImage48("fwDelete.png");
        //public static Image EDIT_IMAGE48 = ResourceMan.getImage48("fwEdit.png");
        //public static Image ADD_IMAGE48 = ResourceMan.getImage48("fwAdd.png");
        //public static Image PRINT_IMAGE48 = ResourceMan.getImage48("fwPrint.png");
        //public static Image PRINT_PREVIEW_IMAGE48 = ResourceMan.getImage48("fwPrintPreview.png");
        //public static Image BUSINESS_IMAGE48 = ResourceMan.getImage48("fwBusiness.png");
        //public static Image COMMIT_IMAGE48 = ResourceMan.getImage48("fwCommit.png");
        //public static Image UNCOMMIT_IMAGE48 = ResourceMan.getImage48("fwUnCommit.png");
        //public static Image EXIT_IMAGE48 = ResourceMan.getImage48("fwExit.png");
        //public static Image VIEW_IMAGE48 = ResourceMan.getImage48("fwView.png");
        //public static Image ADD_IMAGE20 = ResourceMan.getImage20("fwAdd.png");
        //public static Image EDIT_IMAGE20 = ResourceMan.getImage20("fwEdit.png");
        //public static Image DELETE_IMAGE20 = ResourceMan.getImage20("fwDelete.png");
        //public static Image CLOSE_IMAGE20 = ResourceMan.getImage20("fwClose.png");        
        //public static Image NO_SAVE_IMAGE20 = ResourceMan.getImage20("fwNoSave.png");
        //public static Image SAVE_IMAGE20 = ResourceMan.getImage20("fwSave.png");
        //public static Image SAVE_IMAGE16 = ResourceMan.getImage16("fwSave.png");        
        //public static Image NO_SAVE_IMAGE16 = ResourceMan.getImage16("fwNoSave.png");
        //public static Image SAVE_ALL_IMAGE16 = ResourceMan.getImage16("fwSaveAll.png");
        //public static Image CLOSE_IMAGE16 = ResourceMan.getImage16("fwClose.png");
        //public static Image EXIT_IMAGE16 = ResourceMan.getImage16("fwExit.png");
        //public static Image DELETE_IMAGE16 = ResourceMan.getImage16("fwDelete.png");
        //public static Image ADD_IMAGE16 = ResourceMan.getImage16("fwAdd.png");
        //public static Image LOGIN_IMAGE16 = ResourceMan.getImage16("fwLogin.png");
        //public static Image CONNECT_IMAGE16 = ResourceMan.getImage16("fwConnect.png");
        //public static Image CONFIG_IMAGE16 = ResourceMan.getImage16("fwConfig.png");
        //public static Image PRINT_IMAGE16 = ResourceMan.getImage16("fwPrint.png");
        //public static Image PRINT_PREVIEW_IMAGE16 = ResourceMan.getImage16("fwPrintPreview.png");
        //public static Image PREVIEW_IMAGE16 = ResourceMan.getImage16("fwPreview.png");
        //global::ProtocolVN.Framework.Win.Properties.Resources.logo;
        //public static Image LOGO_IMAGE48 = ResourceMan.getImage48("logo.gif");
        #endregion
    }
}