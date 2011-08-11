using System.Data;
using System.Drawing;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;

namespace ProtocolVN.Framework.Win
{
    public interface IChartXYZ
    {
        DataSet MasterData();//THANG, TENNHANVIEN, DOANHSO
        string[] MasterCaption();//Tháng, Tên nhân viên, Doanh số bán
        DataSet DetailData(string XFN_Value, string SerieFN_Value);//tháng đang chọn và Seria đang chọn
        string[] DetailCaption();//Tháng, Tên nhân viên, Mã phiếu bán hàng, Thành tiền
        string GetXFN();//TENNHANVIEN
        string GetYFN();//DOANHSO
        string GetSerieFN();//THANG
        string GetTitle();//BIEU DO DOANH SO Nam 2008
        string GetCaptionX();//Tên nhân viên
        string GetCaptionY();//Doanh số bán
        [Obsolete("Hiện tại không dùng")]
        string GetBeginTestSeries();
    }

    public interface IChartXY
    {
        DataSet MasterData();//TENNHANVIEN, DOANHSO
        string[] MasterCaption();//Tên nhân viên, Doanh số bán 
        DataSet DetailData(string XFN_Value);//NHANVIEN, PBH, ThanhTien
        string[] DetailCaption();//Tên nhân viên, Mã phiếu bán hàng, Thành tiền
        string GetXFN();//TENNHANVIEN
        string GetYFN();//DOANHSO
        string GetTitle();//BIEU DO DOANH SO 4/2008
        [Obsolete("Hiện tại không dùng")]
        string GetSeriesName();//Không dùng
        string GetCaptionX();
        string GetCaptionY();
    }

    public class PopularChartData
    {
        public static int TypeChart = 0;

        public static void SetBeginTextSeries(ChartControl chartControl, string text)
        {
            chartControl.SeriesNameTemplate.BeginText = text;
        }

        public static void DefineXYZ(ChartControl chartControl, DataSet ds, string valueX, string valueY, string valueSeries)
        {
            TypeChart = 2;
            chartControl.DataSource = ds.Tables[0];
            chartControl.SeriesDataMember = valueSeries;
            chartControl.SeriesTemplate.ArgumentDataMember = valueX;
            chartControl.SeriesTemplate.ValueDataMembers.AddRange(new string[] { valueY });
        }

        public static void DefineSeries(ChartControl chartControl, string name)
        {
            TypeChart = 1;
            Series series = new Series(name, ViewType.Bar);
            chartControl.Series.Add(series);
        }

        public static void DefineXY(ChartControl chartControl,DataSet ds, string valueX, string valueY)
        {
            chartControl.Series[0].DataSource = ds.Tables[0];
            chartControl.Series[0].ArgumentDataMember = valueX;
            chartControl.Series[0].ValueDataMembers.AddRange(new string[] {valueY});
        }

        public static void SetZoom(ChartControl chartControl,bool isZoom)
        {
            ((XYDiagram)chartControl.Diagram).EnableZooming = isZoom;
        }

        public static void SetScroll(ChartControl chartControl,bool isScroll)
        {
            ((XYDiagram)chartControl.Diagram).EnableScrolling = isScroll;
        }

        public static void SetSelectionRuntime(ChartControl chartControl, bool isSelection)
        {
            chartControl.RuntimeSelection = isSelection;
            chartControl.RuntimeSeriesSelectionMode = SeriesSelectionMode.Point;
        }

        public static void ChangeBarView(ChartControl chartControl)
        {
            foreach (Series series in chartControl.Series)
            {
                series.ChangeView(ViewType.Bar);
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.General;
                series.PointOptions.ValueNumericOptions.Precision = 2;
            }
        }

        public static void ChangeLineView(ChartControl chartControl)
        {
            foreach (Series series in chartControl.Series)
            {
                series.ChangeView(ViewType.Line);
                series.PointOptions.ValueNumericOptions.Format = NumericFormat.General;
                series.PointOptions.ValueNumericOptions.Precision = 2;
            }
        }

        public static void ChangePieView(ChartControl chartControl)
        {
            if (TypeChart == 1)
            {
                foreach (Series series in chartControl.Series)
                {
                    series.ChangeView(ViewType.Pie);
                    series.LegendPointOptions.PointView = PointView.Argument;
                    series.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    series.PointOptions.ValueNumericOptions.Precision = 2;
                }
            }
            else
            { 
                //danh cho truong hop di lieu 3 chieu XYZ 
            }
        }
       
        public static void SetAngleLabel_X(ChartControl chartControl, int angle)
        { 
            ((XYDiagram)chartControl.Diagram).AxisX.Label.Angle = angle;
        }

        public static void SetSmoothLabel_X(ChartControl chartControl, bool isSmooth)
        {
            ((XYDiagram)chartControl.Diagram).AxisX.Label.Antialiasing = isSmooth;
        }

        public static void DefineTitleChart(ChartControl chartControl, string title)
        {
            ChartTitle chartTitle = new ChartTitle();
            chartTitle.Text = title;
            chartControl.Titles.Add(chartTitle);
        }

        public static void DefineCaption_X(ChartControl chartControl, string caption)
        {
            ((XYDiagram)chartControl.Diagram).AxisX.Title.Text = caption;
            ((XYDiagram)chartControl.Diagram).AxisX.Title.Visible = true;
            ((XYDiagram)chartControl.Diagram).AxisX.Title.Alignment = System.Drawing.StringAlignment.Center;
            ((XYDiagram)chartControl.Diagram).AxisX.Title.Font = new System.Drawing.Font("Tahoma", 8);
        }

        public static void DefineCaption_Y(ChartControl chartControl, string caption)
        {
            ((XYDiagram)chartControl.Diagram).AxisY.Title.Text = caption;
            ((XYDiagram)chartControl.Diagram).AxisY.Title.Visible = true;
            ((XYDiagram)chartControl.Diagram).AxisY.Title.Alignment = System.Drawing.StringAlignment.Center;
            ((XYDiagram)chartControl.Diagram).AxisY.Title.Font = new System.Drawing.Font("Tahoma", 8);
        }

        public static void PrintPreview(ChartControl chartControl, GridControl grid)
        {
            chartControl.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom;
            CompositeLink composLink = new CompositeLink(new PrintingSystem());
            PrintableComponentLink pcLink1 = new PrintableComponentLink();
            PrintableComponentLink pcLink2 = new PrintableComponentLink();
            Link linkMainReport = new Link();

            linkMainReport.CreateDetailArea += new CreateAreaEventHandler(linkMainReport_CreateDetailArea);
            Link linkGrid1Report = new Link();
            linkGrid1Report.CreateDetailArea += new CreateAreaEventHandler(linkGrid1Report_CreateDetailArea);
            Link linkGrid2Report = new Link();
            linkGrid2Report.CreateDetailArea += new CreateAreaEventHandler(linkChartReport_CreateDetailArea);

            pcLink1.Component = grid;
            pcLink2.Component = chartControl;

            composLink.Links.Add(linkGrid2Report);
            composLink.Links.Add(pcLink2);
            composLink.Links.Add(linkGrid1Report);
            composLink.Links.Add(pcLink1);
            composLink.Links.Add(linkMainReport);
            composLink.ShowPreviewDialog();
        }

        private static void linkMainReport_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Rect = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            tb.BackColor = Color.White;
            e.Graph.DrawBrick(tb);
        }

        private static void linkGrid1Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "Dữ Liệu Chi Tiết";
            tb.Font = new Font("Arial", 12);
            tb.Rect = new RectangleF(0, 30, 600, 25);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            e.Graph.DrawBrick(tb);
        }
       
        private static void linkChartReport_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "";
            tb.Font = new Font("Tahoma", 12, FontStyle.Bold);
            tb.Rect = new RectangleF(0, 0, 600, 25);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            e.Graph.DrawBrick(tb);
        }

    }
}
