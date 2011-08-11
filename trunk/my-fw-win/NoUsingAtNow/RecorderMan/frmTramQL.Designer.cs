namespace ProtocolVN.Framework.Win
{
    partial class frmTramQL
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gridDSTram = new DevExpress.XtraGrid.GridControl();
            this.gridViewDSRecord = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.ColTram = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColNoiDung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColPlay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColStop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.play_item = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.stop_item = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.Tuyen = new ProtocolVN.Framework.Win.PLCombobox();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridDSTram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.play_item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stop_item)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDSTram
            // 
            this.gridDSTram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDSTram.Location = new System.Drawing.Point(12, 35);
            this.gridDSTram.MainView = this.gridViewDSRecord;
            this.gridDSTram.Name = "gridDSTram";
            this.gridDSTram.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.play_item,
            this.stop_item});
            this.gridDSTram.Size = new System.Drawing.Size(598, 396);
            this.gridDSTram.TabIndex = 8;
            this.gridDSTram.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDSRecord});
            // 
            // gridViewDSRecord
            // 
            this.gridViewDSRecord.ActiveFilterEnabled = false;
            this.gridViewDSRecord.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridViewDSRecord.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridViewDSRecord.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gridViewDSRecord.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridViewDSRecord.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewDSRecord.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewDSRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColTram,
            this.ColDiaChi,
            this.ColNoiDung,
            this.ColPlay,
            this.ColStop});
            this.gridViewDSRecord.GridControl = this.gridDSTram;
            this.gridViewDSRecord.GroupPanelText = "Danh sách trạm";
            this.gridViewDSRecord.IndicatorWidth = 40;
            this.gridViewDSRecord.Name = "gridViewDSRecord";
            this.gridViewDSRecord.OptionsCustomization.AllowFilter = false;
            this.gridViewDSRecord.OptionsCustomization.AllowGroup = false;
            this.gridViewDSRecord.OptionsFilter.UseNewCustomFilterDialog = true;
            this.gridViewDSRecord.OptionsLayout.Columns.AddNewColumns = false;
            this.gridViewDSRecord.OptionsNavigation.AutoFocusNewRow = true;
            this.gridViewDSRecord.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridViewDSRecord.OptionsSelection.MultiSelect = true;
            this.gridViewDSRecord.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewDSRecord.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewDSRecord.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewDSRecord.OptionsView.ShowFooter = true;
            this.gridViewDSRecord.OptionsView.ShowGroupedColumns = true;
            // 
            // ColTram
            // 
            this.ColTram.Caption = "Trạm";
            this.ColTram.Name = "ColTram";
            this.ColTram.OptionsColumn.AllowEdit = false;
            this.ColTram.Visible = true;
            this.ColTram.VisibleIndex = 0;
            this.ColTram.Width = 230;
            // 
            // ColDiaChi
            // 
            this.ColDiaChi.Caption = "Địa chỉ";
            this.ColDiaChi.Name = "ColDiaChi";
            this.ColDiaChi.OptionsColumn.AllowEdit = false;
            this.ColDiaChi.Visible = true;
            this.ColDiaChi.VisibleIndex = 1;
            // 
            // ColNoiDung
            // 
            this.ColNoiDung.Caption = "Nội dung";
            this.ColNoiDung.Name = "ColNoiDung";
            this.ColNoiDung.OptionsColumn.AllowEdit = false;
            this.ColNoiDung.Visible = true;
            this.ColNoiDung.VisibleIndex = 2;
            this.ColNoiDung.Width = 231;
            // 
            // ColPlay
            // 
            this.ColPlay.AppearanceCell.Options.UseTextOptions = true;
            this.ColPlay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColPlay.AppearanceHeader.Options.UseTextOptions = true;
            this.ColPlay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColPlay.Name = "ColPlay";
            this.ColPlay.OptionsColumn.AllowSize = false;
            this.ColPlay.OptionsColumn.ShowCaption = false;
            this.ColPlay.Visible = true;
            this.ColPlay.VisibleIndex = 3;
            this.ColPlay.Width = 20;
            // 
            // ColStop
            // 
            this.ColStop.AppearanceCell.Options.UseTextOptions = true;
            this.ColStop.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColStop.AppearanceHeader.Options.UseTextOptions = true;
            this.ColStop.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColStop.Name = "ColStop";
            this.ColStop.OptionsColumn.AllowSize = false;
            this.ColStop.OptionsColumn.ShowCaption = false;
            this.ColStop.Visible = true;
            this.ColStop.VisibleIndex = 4;
            this.ColStop.Width = 20;
            // 
            // play_item
            // 
            this.play_item.AutoHeight = false;
            this.play_item.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            serializableAppearanceObject1.Options.UseImage = true;
            this.play_item.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 10, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::ProtocolVN.Framework.Win.Dev.Properties.Resources.play16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Phát âm thanh")});
            this.play_item.Name = "play_item";
            this.play_item.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.play_item.Click += new System.EventHandler(this.play_item_Click);
            // 
            // stop_item
            // 
            this.stop_item.AutoHeight = false;
            this.stop_item.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.stop_item.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 10, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::ProtocolVN.Framework.Win.Dev.Properties.Resources.stop16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Ngừng phát âm thanh")});
            this.stop_item.Name = "stop_item";
            this.stop_item.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.stop_item.Click += new System.EventHandler(this.stop_item_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tuyến";
            // 
            // Tuyen
            // 
            this.Tuyen.DataSource = null;
            this.Tuyen.DisplayField = null;
            this.Tuyen.Location = new System.Drawing.Point(55, 9);
            this.Tuyen.Name = "Tuyen";
            this.Tuyen.Size = new System.Drawing.Size(290, 20);
            this.Tuyen.TabIndex = 10;
            this.Tuyen.ValueField = null;
            this.Tuyen.SelectedIndexChanged += new System.EventHandler(this.Tuyen_SelectedIndexChanged);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(12, 437);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 17;
            this.btnExport.Text = "&Export";
            // 
            // frmTramQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 468);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.Tuyen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridDSTram);
            this.Name = "frmTramQL";
            this.Text = "Quản lý trạm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTramQL_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridDSTram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.play_item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stop_item)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridDSTram;
        private DevExpress.XtraGrid.Views.Grid.PLGridView gridViewDSRecord;
        private System.Windows.Forms.Label label2;
        private PLCombobox Tuyen;
        public DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn ColTram;
        private DevExpress.XtraGrid.Columns.GridColumn ColNoiDung;
        private DevExpress.XtraGrid.Columns.GridColumn ColPlay;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit play_item;
        private DevExpress.XtraGrid.Columns.GridColumn ColStop;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit stop_item;
        private DevExpress.XtraGrid.Columns.GridColumn ColDiaChi;
    }
}