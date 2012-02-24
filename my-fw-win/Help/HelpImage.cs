using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Drawing;
using System.IO;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ xử lý trên IMAGE
    /// </summary>
    public class HelpImage //: ResourceMan
    {
        public static byte[] GetImage(string fileName)
        {
            try
            {
                using (Bitmap image = new Bitmap(fileName))
                {
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    return stream.ToArray();
                }
            }
            catch (Exception e) 
            {
                PLException.AddException(e);
            }

            return null;
        }

        public static void LoadImage(PictureEdit pictureEdit1, byte[] logo)
        {
            try
            {
                if (logo == null) return;
                MemoryStream stream = new MemoryStream(logo);
                Bitmap image = new Bitmap(stream);

                pictureEdit1.Image = image;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
        }
        public static Image LoadImage( byte[] logo)
        {
            try
            {
                if (logo == null) return null;
                MemoryStream stream = new MemoryStream(logo);
                Bitmap image = new Bitmap(stream);

                return image;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return null;
        }

        public static byte[] GetBytes(Image jpegImg)
        {
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            jpegImg.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] ret = mStream.ToArray();
            mStream.Close();
            return ret;
        }

        /// <summary>Hàm trả về 1 Image.
        /// </summary>
        /// <param name="name">
        /// name: là số nó sẽ lookup ID trong kho hình của FW.
        /// name: là tên nếu dùng imageStore nó sẽ lookup name từ kho ứng dụng.
        ///              nếu không thấy nó sẽ lookup name từ kho FW.
        ///              nếu không dùng imageStore nó sẽ lookup từ kho hình thư mục images.
        ///              nếu lookup không ra nó sẽ lấy hình logo.
        /// </param>
        public static Image getImage1616(String name)
        {
            int num = HelpNumber.ParseInt32(name);
            Image image = null;
            if (num < 0)
            {
                if (FrameworkParams.imageStore != null)
                {
                    image = FrameworkParams.imageStore.GetImage1616(name);
                    if (image == null) image = ImageCollectionMan.Instance.GetImage1616(name);
                }
                else
                {
                    image = ResourceMan.getImage16(name);
                }
            }
            else
            {
                image = ImageCollectionMan.Instance.GetImage1616(num);
            }
            
            if (image == null) image = FWImageDic.LOGO_IMAGE16;

            return image;
        }

        /// <summary>Hàm trả về 1 Image.
        /// </summary>
        /// <param name="name">
        /// name: là số nó sẽ lookup ID trong kho hình của FW.
        /// name: là tên nếu dùng imageStore nó sẽ lookup name từ kho ứng dụng.
        ///              nếu không thấy nó sẽ lookup name từ kho FW.
        ///              nếu không dùng imageStore nó sẽ lookup từ kho hình thư mục images.
        ///              nếu lookup không ra nó sẽ lấy hình logo.
        /// </param>
        public static Image getImage2020(String name)
        {
            int num = HelpNumber.ParseInt32(name);
            Image image = null;
            if (num < 0)
            {
                if (FrameworkParams.imageStore != null)
                {
                    image = FrameworkParams.imageStore.GetImage2020(name);
                    if (image == null) image = ImageCollectionMan.Instance.GetImage2020(name);
                }
                else
                {
                    image = ResourceMan.getImage20(name);
                }
            }
            else
            {
                image = ImageCollectionMan.Instance.GetImage2020(num);
            }
            if (image == null) image = FWImageDic.LOGO_IMAGE20;

            return image;
        }

        /// <summary>Hàm trả về 1 Image.
        /// </summary>
        /// <param name="name">
        /// name: là số nó sẽ lookup ID trong kho hình của FW.
        /// name: là tên nếu dùng imageStore nó sẽ lookup name từ kho ứng dụng.
        ///              nếu không thấy nó sẽ lookup name từ kho FW.
        ///              nếu không dùng imageStore nó sẽ lookup từ kho hình thư mục images.
        ///              nếu lookup không ra nó sẽ lấy hình logo.
        /// </param>
        public static Image getImage3232(String name)
        {
            int num = HelpNumber.ParseInt32(name);
            Image image = null;
            if (num < 0)
            {
                if (FrameworkParams.imageStore != null)
                {
                    image = FrameworkParams.imageStore.GetImage3232(name);
                    if (image == null) image = ImageCollectionMan.Instance.GetImage3232(name);
                }
                else
                {
                    image = ResourceMan.getImage32(name);
                }
            }
            else
            {
                image = ImageCollectionMan.Instance.GetImage3232(num);
            }
            if (image == null) image = FWImageDic.LOGO_IMAGE32;

            return image;
        }

        /// <summary>Hàm trả về 1 Image.
        /// </summary>
        /// <param name="name">
        /// name: là số nó sẽ lookup ID trong kho hình của FW.
        /// name: là tên nếu dùng imageStore nó sẽ lookup name từ kho ứng dụng.
        ///              nếu không thấy nó sẽ lookup name từ kho FW.
        ///              nếu không dùng imageStore nó sẽ lookup từ kho hình thư mục images.
        ///              nếu lookup không ra nó sẽ lấy hình logo.
        /// </param>
        public static Image getImage4848(String name)
        {
            int num = HelpNumber.ParseInt32(name);
            Image image = null;
            if (num < 0)
            {
                if (FrameworkParams.imageStore != null)
                {
                    image = FrameworkParams.imageStore.GetImage4848(name);
                    if (image == null) image = ImageCollectionMan.Instance.GetImage4848(name);
                }
                else
                {
                    image = ResourceMan.getImage48(name);
                }
            }
            else
            {
                image = ImageCollectionMan.Instance.GetImage4848(num);
            }
            if (image == null) image = FWImageDic.LOGO_IMAGE48;

            return image;
        }
    }
}
