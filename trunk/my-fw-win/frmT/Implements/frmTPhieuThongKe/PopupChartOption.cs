using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;

namespace ProtocolVN.Framework.Win
{
    public partial class PopupChartOption : XtraFormPL, IPublicForm
    {
        PivotGridControl pivotGridMaster;
        string chartTitle = "";
 
        public PopupChartOption(PivotGridControl _pivotGridMaster, string _chartTitle)
        {
            InitializeComponent();

            pivotGridMaster = _pivotGridMaster;
            chartTitle = _chartTitle.ToUpper();

            _initLoaiBieuDo();
            _initChart();
            
        }

        public static void _setScrollAndZoom(ChartControl chartControl)
        {
            if (chartControl.Diagram is Diagram3D)
            {
                Diagram3D diagram = (Diagram3D)chartControl.Diagram;
                diagram.RuntimeRotation = true;
                diagram.RuntimeZooming = true;
                diagram.RuntimeScrolling = true;
            }
            else if (chartControl.Diagram is XYDiagram)
            {
                XYDiagram diagram = (XYDiagram)chartControl.Diagram;
                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisYScrolling = true;
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYZooming = true;
                diagram.ZoomingOptions.UseKeyboard = true;
                diagram.ZoomingOptions.UseKeyboardWithMouse = true;
            }
            else if (chartControl.Diagram is XYDiagram2D)
            {
                XYDiagram2D diagram = (XYDiagram2D)chartControl.Diagram;
                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisYScrolling = true;
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYZooming = true;
                diagram.ZoomingOptions.UseKeyboard = true;
                diagram.ZoomingOptions.UseKeyboardWithMouse = true;
            }
            else if (chartControl.Diagram is XYDiagram3D)
            {
                XYDiagram3D diagram = (XYDiagram3D)chartControl.Diagram;
                diagram.RuntimeZooming = true;
                diagram.RuntimeScrolling = true;
                diagram.RuntimeRotation = true;
                diagram.ZoomingOptions.UseKeyboard = true;
                diagram.ZoomingOptions.UseKeyboardWithMouse = true;                
            }
        }

        private void _initChart()
        {
            checkShowPointLabels.Checked = true;
            chartControlMaster.SeriesTemplate.Label.Visible = true;

            if (pivotGridMaster.Cells.Selection.X == 0 &&
                pivotGridMaster.Cells.Selection.Y == 0 &&
                pivotGridMaster.Cells.Selection.Width == 0 &&
                pivotGridMaster.Cells.Selection.Height == 0)
            {
                ceSelectionOnly.Checked = false;
                ceSelectionFull.Checked = true;
                pivotGridMaster.OptionsChartDataSource.SelectionOnly = false;
            }
            else
            {
                ceSelectionOnly.Checked = true;
                ceSelectionFull.Checked = false;
                pivotGridMaster.OptionsChartDataSource.SelectionOnly = true;
            }

            ceChartDataVertical.Checked = true;
            pivotGridMaster.OptionsChartDataSource.ChartDataVertical = ceChartDataVertical.Checked;
            
            ChartTitle title = new ChartTitle();
            title.Text = chartTitle;
            chartControlMaster.Titles.Add(title);

            chartControlMaster.DataSource = pivotGridMaster;
            chartControlMaster.SeriesDataMember = "Series";
            chartControlMaster.SeriesTemplate.ArgumentDataMember = "Arguments";
            chartControlMaster.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Values" });
            chartControlMaster.SeriesTemplate.LegendPointOptions.PointView = PointView.ArgumentAndValues;

            _setScrollAndZoom(chartControlMaster);
        }

        private void _initLoaiBieuDo()
        {
            ViewType[] restrictedTypes = new ViewType[] { 
                ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt,
				ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt,
                ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick,
				ViewType.Bubble
			};

            foreach (ViewType type in Enum.GetValues(typeof(ViewType)))
            {
                if (Array.IndexOf<ViewType>(restrictedTypes, type) >= 0) continue;
                comboChartType.Properties.Items.Add(type);
            }
            //comboChartType.SelectedItem = ViewType.Line;
            comboChartType.SelectedItem = ViewType.Bar3D;
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            HelpWaiting.showMsgForm(this, drawGraph);            
        }

        private void drawGraph()
        {
            chartControlMaster.SeriesTemplate.ChangeView((ViewType)comboChartType.SelectedItem);
            _setScrollAndZoom(chartControlMaster);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            chartControlMaster.SeriesTemplate.Label.Visible = checkShowPointLabels.Checked;
        }

        private void ceChartDataVertical_CheckedChanged(object sender, EventArgs e)
        {
            pivotGridMaster.OptionsChartDataSource.ChartDataVertical = ceChartDataVertical.Checked;
        }

        private void ceSelectionOnly_CheckedChanged(object sender, EventArgs e)
        {
            pivotGridMaster.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            //HelpWaiting.showMsgForm(this, this.Close);
            this.Close();
        }
    }
}