using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using DevExpress.Utils;

namespace ProtocolVN.Framework.Win
{
    public class CompanyInfoHeaderStartTitleGridEndFooter : IHeaderStartTitleGridEndFooter
    {
        private PrintableComponentLink printableComponentLink1;
        private String reportHeader;
        private Image ReportHeaderImage;
        private String rtfGridHeader;
        private int rtfGridHeaderHeight = 60;
        private String rtfGridFooter;
        private String reportFooter = "Trang [Trang #]";
        //LOGO lấy từ FrameworkParams.ReportHeaderImage

        public CompanyInfoHeaderStartTitleGridEndFooter()
        {
            CompanyInfo info = DACompanyInfo.Instance.load();

            //Lấy logo
            ImageConverter ic = new ImageConverter();
            Image img = (Image)ic.ConvertFrom(info.logo);

            ImageCollection images = new ImageCollection();
            images.ImageSize = new Size(48, 48);
            images.AddImage(img);

            ReportHeaderImage = images.Images[0];

            //Lay Info
            String blank = "                      ";
            StringBuilder str = new StringBuilder("");

            if (info.name != null) str.AppendLine(blank + info.name);
            if (info.address != null) str.AppendLine(blank + "Địa chỉ: " + info.address);
            if (info.phone != null) str.Append(blank + "Điện thoại: " + info.phone);
            if (info.fax != null) str.Append("   Fax: " + info.fax);
            str.AppendLine();
            if (info.email != null) str.Append(blank + "Email: " + info.email);
            if (info.website != null) str.Append("   Website: " + info.website);

            RichTextBox r = new RichTextBox();
            r.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            r.Text = str.ToString();
            rtfGridHeader = r.Rtf;
        }


        public PrintableComponentLink Draw(IPrintable gridControl, String mainTitle, String subTitle)
        {
            float height = 0;

            DevExpress.XtraPrinting.PrintingSystem printingSystem1;
            printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem();
            ((System.ComponentModel.ISupportInitialize)(printingSystem1)).BeginInit();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink1;
            printableComponentLink1 = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink1.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink1.Margins.Left = 50;
            printableComponentLink1.Margins.Right = 50;
            printableComponentLink1.Margins.Top = 50;
            printableComponentLink1.Margins.Bottom = 50;
            printingSystem1.Links.AddRange(new object[] { printableComponentLink1 });
            printableComponentLink1.Component = gridControl;
            printableComponentLink1.PrintingSystem = printingSystem1;

            DevExpress.XtraPrinting.PageHeaderArea headerArea;
            headerArea = new DevExpress.XtraPrinting.PageHeaderArea();
            headerArea.Content.Add(reportHeader);
            headerArea.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near;

            #region Đầu trang
            if (rtfGridHeader != null)
            {
                printableComponentLink1.RtfReportHeader = rtfGridHeader;
                height = rtfGridHeaderHeight;
            }
            printableComponentLink1.CreateReportHeaderArea += delegate(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
            {
                float currentHeight = height;

                #region Giải pháp 1
                Image headerImage = (this.ReportHeaderImage == null ?
                    FWImageDic.LOGO_IMAGE48 : this.ReportHeaderImage);

                DevExpress.XtraPrinting.ImageBrick logo;
                logo = e.Graph.DrawImage(headerImage,
                    new RectangleF(10, 5, headerImage.Width, headerImage.Height),
                    DevExpress.XtraPrinting.BorderSide.None, Color.Transparent);
                //currentHeight += headerImage.Height;
                #endregion

                if (mainTitle != null)
                {
                    DevExpress.XtraPrinting.TextBrick brick;
                    brick = e.Graph.DrawString(mainTitle, Color.Navy, new RectangleF(0, currentHeight, 620, 40), DevExpress.XtraPrinting.BorderSide.None);
                    currentHeight += 40;
                    brick.Font = new Font("Tahoma", 20);
                    brick.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);
                    brick.BackColor = Color.White;
                    brick.ForeColor = Color.Black;

                }

                if (subTitle != null)
                {
                    DevExpress.XtraPrinting.TextBrick brickDate;
                    brickDate = e.Graph.DrawString(subTitle, Color.Navy, new RectangleF(0, currentHeight, 620, 40), DevExpress.XtraPrinting.BorderSide.None);
                    currentHeight += 40;
                    brickDate.Font = new Font("Tahoma", 10);
                    brickDate.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);
                    brickDate.BackColor = Color.White;
                    brickDate.ForeColor = Color.Black;
                }
            };
            #endregion

            if (rtfGridFooter != null)
                printableComponentLink1.RtfReportFooter = rtfGridFooter;

            #region Header Footer
            DevExpress.XtraPrinting.PageFooterArea footerArea;
            footerArea = new DevExpress.XtraPrinting.PageFooterArea();
            footerArea.Content.Add(reportFooter);
            footerArea.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near;

            DevExpress.XtraPrinting.PageHeaderFooter pageHeaderFooter;
            pageHeaderFooter = new DevExpress.XtraPrinting.PageHeaderFooter(headerArea, footerArea);
            printableComponentLink1.PageHeaderFooter = pageHeaderFooter;
            #endregion

            ((System.ComponentModel.ISupportInitialize)(printingSystem1)).EndInit();

            printableComponentLink1.CreateDocument();

            return printableComponentLink1;
        }
    }
}
