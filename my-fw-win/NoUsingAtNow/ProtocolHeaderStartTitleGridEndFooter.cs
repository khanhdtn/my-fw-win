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
    public class ProtocolHeaderStartTitleGridEndFooter : IHeaderStartTitleGridEndFooter
    {
        private PrintableComponentLink printableComponentLink1;
        private String reportHeader;
        private Image ReportHeaderImage;
        private String rtfGridHeader = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Arial;}{\f1\fswiss\fcharset238{\*\fname Arial;}Arial CE;}}
{\*\generator Msftedit 5.41.15.1515;}\viewkind4\uc1\pard\f0\fs16                      129 Th\'edch Qu\u7843?ng \f1\'d0\u7913?c, Ph\u432?\u7901?ng 4, Qu\u7853?n Ph\'fa Nhu\u7853?n, TP. HCM\par
\f0                      \f1 Tel: (08) 38 42 38 38    -    Fax: (08) 38 42 38 39\par
\f0                      \f1 Email: info@protocolvn.com\par
\f0                      Website: www.protocolvn.com\fs20\par
}";
        private int rtfGridHeaderHeight = 60;
        private String rtfGridFooter;
        private String reportFooter = "Trang [Trang #]";
        //LOGO lấy từ FrameworkParams.ReportHeaderImage

        public ProtocolHeaderStartTitleGridEndFooter()
        {
        }

        //public FWHeaderGrid(            
        //    String reportHeader,
        //    Image ReportHeaderImage,
        //    String rtfGridHeader, 
        //    int rtfGridHeaderHeight, 
        //    String rtfGridFooter,
        //    String reportFooter)
        //{
        //    this.reportHeader = reportHeader;
        //    this.ReportHeaderImage = ReportHeaderImage;
        //    this.rtfGridHeader = rtfGridHeader;
        //    this.rtfGridHeaderHeight = rtfGridHeaderHeight;
        //    this.rtfGridFooter = rtfGridFooter;
        //    this.reportFooter = reportFooter;
        //}


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
