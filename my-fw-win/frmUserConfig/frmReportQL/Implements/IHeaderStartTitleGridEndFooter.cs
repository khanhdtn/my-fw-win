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
    public interface IHeaderStartTitleGridEndFooter
    {
        PrintableComponentLink Draw(IPrintable gridControl, String mainTitle, String subTitle);
    }


    public class ImageHeaderStartTitleGridEndFooter : IHeaderStartTitleGridEndFooter
    {
        private PrintableComponentLink printableComponentLink1;
        private String reportHeader;
        private Image ReportHeaderImage;
        private String reportFooter = "Trang [Trang #]";
        //LOGO lấy từ FrameworkParams.ReportHeaderImage

        public ImageHeaderStartTitleGridEndFooter(Image ReportHeaderImage)
        {
            this.ReportHeaderImage = ReportHeaderImage;
        }


        public PrintableComponentLink Draw(IPrintable gridControl, String mainTitle, String subTitle)
        {
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
            printableComponentLink1.CreateReportHeaderArea += delegate(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
            {
                float currentHeight = 0;
                #region Giải pháp 1
                if (ReportHeaderImage != null)
                {
                    DevExpress.XtraPrinting.ImageBrick headerImage;
                    headerImage = e.Graph.DrawImage(ReportHeaderImage,
                        new RectangleF(5, 5, ReportHeaderImage.Width, ReportHeaderImage.Height),
                            BorderSide.None, Color.Transparent);
                    currentHeight += ReportHeaderImage.Height + 5;
                }
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
