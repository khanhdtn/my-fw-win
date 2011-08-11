using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraCharts;
namespace ProtocolVN.Framework.Win
{
    public partial class frmChartXYZ : DevExpress.XtraEditors.XtraForm
    {
        private DataSet ds = null;
        private IChartXYZ ex;

        public frmChartXYZ(IChartXYZ ex)
        {
            InitializeComponent();
            this.ex = ex;
        }

        private void frmTestChartXYZ_Load(object sender, EventArgs e)
        {
            ds = ex.MasterData();
            PopularChartData.DefineXYZ(chartControl1, ds, ex.GetXFN(), ex.GetYFN(), ex.GetSerieFN());
            PopularChartData.DefineTitleChart(chartControl1, ex.GetTitle());
            PopularChartData.DefineCaption_X(chartControl1, ex.GetCaptionX());
            PopularChartData.DefineCaption_Y(chartControl1, ex.GetCaptionY());
            PopularChartData.SetBeginTextSeries(chartControl1, ex.GetBeginTestSeries());
            PopularChartData.SetAngleLabel_X(chartControl1, 0);
            PopularChartData.SetSmoothLabel_X(chartControl1, true);
            PopularChartData.SetSelectionRuntime(chartControl1, true);
            PopularChartData.SetScroll(chartControl1, true);
            PopularChartData.SetZoom(chartControl1, true);

            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            gridView1.Columns[0].Group();
            gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            gridView1.OptionsBehavior.Editable = false;
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl1.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;

            string type = hi.HitObject.GetType().ToString();

            if(hi.HitObject.GetType().ToString().Equals("DevExpress.XtraCharts.Series"))
            {
                Series series = (Series)hi.HitObject;
                if (point != null)
                {
                    DataSet ds = new DataSet();
                    //ds = ex.DetailData(point.Argument.ToString(), series.Name.Remove(0, ex.GetBeginTestSeries().Length - 1));
                    ds = ex.DetailData(point.Argument.ToString(), series.Name);
                    gridControl1.DataSource = ds.Tables[0].DefaultView;
                    gridView1.PopulateColumns();
                    string[] caption = ex.DetailCaption();
                    for (int i = 0; i < caption.Length; i++)
                    {
                        gridView1.Columns[i].Caption = caption[i];
                    }
                    gridView1.Columns[0].Group();
                    gridView1.Columns[1].Group();
                }
            }
        }

        private void btnBarChart_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            gridView1.Columns[0].Group();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i];
            }
            PopularChartData.ChangeBarView(chartControl1);
        }

        private void btnLineChart_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            gridView1.Columns[0].Group();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i];
            }
            PopularChartData.ChangeLineView(chartControl1);
        }

        private void btnPieChart_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.PopulateColumns();
            //-------------------------------//
            // PopularChartData.ChangePieView(chartControl1,ds,ex.GetXFN(),ex.GetYFN());
        }

        private void btnShowAllData_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = ds.Tables[0].DefaultView;
            gridView1.Columns[0].Group();
            gridView1.PopulateColumns();
            string[] caption = ex.MasterCaption();
            for (int i = 0; i < caption.Length; i++)
            {
                gridView1.Columns[i].Caption = caption[i];
            }
            gridView1.Columns[0].Group();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
           // ProtocolForm.ShowDialog(this, new frmPrintXYZ(ex));
            PopularChartData.PrintPreview(chartControl1, gridControl1);
        }

       
    }
}