namespace ProtocolVN.Framework.Win
{
    partial class PopupChartOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.comboChartType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ceChartDataRow = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ceChartDataVertical = new DevExpress.XtraEditors.CheckEdit();
            this.checkShowPointLabels = new DevExpress.XtraEditors.CheckEdit();
            this.ceSelectionFull = new DevExpress.XtraEditors.CheckEdit();
            this.ceSelectionOnly = new DevExpress.XtraEditors.CheckEdit();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.chartControlMaster = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboChartType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceChartDataRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceChartDataVertical.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkShowPointLabels.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelectionFull.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelectionOnly.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.comboChartType);
            this.panelControl1.Controls.Add(this.ceChartDataRow);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.ceChartDataVertical);
            this.panelControl1.Controls.Add(this.checkShowPointLabels);
            this.panelControl1.Controls.Add(this.ceSelectionFull);
            this.panelControl1.Controls.Add(this.ceSelectionOnly);
            this.panelControl1.Controls.Add(this.btnDong);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(730, 67);
            this.panelControl1.TabIndex = 0;
            // 
            // comboChartType
            // 
            this.comboChartType.EditValue = "Line";
            this.comboChartType.Location = new System.Drawing.Point(75, 12);
            this.comboChartType.Name = "comboChartType";
            this.comboChartType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboChartType.Size = new System.Drawing.Size(145, 20);
            this.comboChartType.TabIndex = 1;
            this.comboChartType.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit2_SelectedIndexChanged);
            // 
            // ceChartDataRow
            // 
            this.ceChartDataRow.Location = new System.Drawing.Point(367, 38);
            this.ceChartDataRow.Name = "ceChartDataRow";
            this.ceChartDataRow.Properties.Caption = "Biểu đồ theo dữ liệu dòng";
            this.ceChartDataRow.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.ceChartDataRow.Properties.RadioGroupIndex = 1;
            this.ceChartDataRow.Size = new System.Drawing.Size(152, 19);
            this.ceChartDataRow.TabIndex = 6;
            this.ceChartDataRow.TabStop = false;
            this.ceChartDataRow.CheckedChanged += new System.EventHandler(this.ceChartDataVertical_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Loại biểu đồ";
            // 
            // ceChartDataVertical
            // 
            this.ceChartDataVertical.Location = new System.Drawing.Point(367, 13);
            this.ceChartDataVertical.Name = "ceChartDataVertical";
            this.ceChartDataVertical.Properties.Caption = "Biểu đồ theo dữ liệu cột";
            this.ceChartDataVertical.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.ceChartDataVertical.Properties.RadioGroupIndex = 1;
            this.ceChartDataVertical.Size = new System.Drawing.Size(142, 19);
            this.ceChartDataVertical.TabIndex = 5;
            this.ceChartDataVertical.TabStop = false;
            this.ceChartDataVertical.CheckedChanged += new System.EventHandler(this.ceChartDataVertical_CheckedChanged);
            // 
            // checkShowPointLabels
            // 
            this.checkShowPointLabels.Location = new System.Drawing.Point(10, 38);
            this.checkShowPointLabels.Name = "checkShowPointLabels";
            this.checkShowPointLabels.Properties.Caption = "Hiện dữ liệu trên biểu đồ";
            this.checkShowPointLabels.Size = new System.Drawing.Size(178, 19);
            this.checkShowPointLabels.TabIndex = 2;
            this.checkShowPointLabels.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // ceSelectionFull
            // 
            this.ceSelectionFull.Location = new System.Drawing.Point(229, 38);
            this.ceSelectionFull.Name = "ceSelectionFull";
            this.ceSelectionFull.Properties.Caption = "Xem tất cả dữ liệu";
            this.ceSelectionFull.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.ceSelectionFull.Properties.RadioGroupIndex = 2;
            this.ceSelectionFull.Size = new System.Drawing.Size(145, 19);
            this.ceSelectionFull.TabIndex = 4;
            this.ceSelectionFull.TabStop = false;
            this.ceSelectionFull.CheckedChanged += new System.EventHandler(this.ceSelectionOnly_CheckedChanged);
            // 
            // ceSelectionOnly
            // 
            this.ceSelectionOnly.Location = new System.Drawing.Point(229, 13);
            this.ceSelectionOnly.Name = "ceSelectionOnly";
            this.ceSelectionOnly.Properties.Caption = "Xem dữ liệu đang chọn";
            this.ceSelectionOnly.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.ceSelectionOnly.Properties.RadioGroupIndex = 2;
            this.ceSelectionOnly.Size = new System.Drawing.Size(145, 19);
            this.ceSelectionOnly.TabIndex = 3;
            this.ceSelectionOnly.TabStop = false;
            this.ceSelectionOnly.CheckedChanged += new System.EventHandler(this.ceSelectionOnly_CheckedChanged);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(525, 12);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(60, 23);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đón&g";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // chartControlMaster
            // 
            this.chartControlMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlMaster.Location = new System.Drawing.Point(0, 67);
            this.chartControlMaster.Name = "chartControlMaster";
            this.chartControlMaster.RuntimeHitTesting = false;
            this.chartControlMaster.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            pointSeriesLabel1.LineVisible = true;
            pointSeriesLabel1.Visible = false;
            this.chartControlMaster.SeriesTemplate.Label = pointSeriesLabel1;
            this.chartControlMaster.SeriesTemplate.View = lineSeriesView1;
            this.chartControlMaster.Size = new System.Drawing.Size(730, 449);
            this.chartControlMaster.TabIndex = 1;
            // 
            // PopupChartOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 516);
            this.Controls.Add(this.chartControlMaster);
            this.Controls.Add(this.panelControl1);
            this.Name = "PopupChartOption";
            this.Text = "Xem biểu đồ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboChartType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceChartDataRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceChartDataVertical.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkShowPointLabels.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelectionFull.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelectionOnly.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlMaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit checkShowPointLabels;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraCharts.ChartControl chartControlMaster;
        private DevExpress.XtraEditors.CheckEdit ceSelectionOnly;
        private DevExpress.XtraEditors.CheckEdit ceChartDataVertical;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.CheckEdit ceChartDataRow;
        private DevExpress.XtraEditors.CheckEdit ceSelectionFull;
        private DevExpress.XtraEditors.ComboBoxEdit comboChartType;
    }
}