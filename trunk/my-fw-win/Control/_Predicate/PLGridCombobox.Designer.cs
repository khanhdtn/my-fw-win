namespace ProtocolVN.Framework.Win
{
    partial class PLGridCombobox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridData = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.PLGridView();
            this.gcValueMember = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDisplayMember = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDM = new DevExpress.XtraEditors.SimpleButton();
            this.EditText = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.popupContainerControl1.AutoSize = true;
            this.popupContainerControl1.Controls.Add(this.gridData);
            this.popupContainerControl1.Controls.Add(this.btnSelect);
            this.popupContainerControl1.Controls.Add(this.btnCancel);
            this.popupContainerControl1.Controls.Add(this.btnDM);
            this.popupContainerControl1.Location = new System.Drawing.Point(3, 26);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(289, 188);
            this.popupContainerControl1.TabIndex = 0;
            this.popupContainerControl1.Text = "popupContainerControl1";
            // 
            // gridData
            // 
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.EmbeddedNavigator.Name = "";
            this.gridData.Location = new System.Drawing.Point(3, 2);
            this.gridData.MainView = this.gridView1;
            this.gridData.Name = "gridData";
            this.gridData.Size = new System.Drawing.Size(283, 159);
            this.gridData.TabIndex = 0;
            this.gridData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridData_KeyDown);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcValueMember,
            this.gcDisplayMember});
            this.gridView1.GridControl = this.gridData;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gcValueMember
            // 
            this.gcValueMember.Caption = "gridColumn1";
            this.gcValueMember.Name = "gcValueMember";
            this.gcValueMember.OptionsColumn.AllowEdit = false;
            this.gcValueMember.OptionsColumn.ReadOnly = true;
            this.gcValueMember.Visible = true;
            this.gcValueMember.VisibleIndex = 0;
            // 
            // gcDisplayMember
            // 
            this.gcDisplayMember.Caption = "gridColumn2";
            this.gcDisplayMember.Name = "gcDisplayMember";
            this.gcDisplayMember.OptionsColumn.AllowEdit = false;
            this.gcDisplayMember.OptionsColumn.ReadOnly = true;
            this.gcDisplayMember.Visible = true;
            this.gcDisplayMember.VisibleIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Appearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnSelect.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSelect.Appearance.Options.UseBorderColor = true;
            this.btnSelect.Appearance.Options.UseForeColor = true;
            this.btnSelect.Location = new System.Drawing.Point(168, 163);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(55, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "&Chọn";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Appearance.Options.UseBorderColor = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.Location = new System.Drawing.Point(229, 163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Bỏ chọn";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDM
            // 
            this.btnDM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDM.Appearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnDM.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDM.Appearance.Options.UseBorderColor = true;
            this.btnDM.Appearance.Options.UseForeColor = true;
            this.btnDM.Location = new System.Drawing.Point(3, 163);
            this.btnDM.Name = "btnDM";
            this.btnDM.Size = new System.Drawing.Size(55, 23);
            this.btnDM.TabIndex = 2;
            this.btnDM.Text = "&Thêm";
            this.btnDM.Click += new System.EventHandler(this.btnDM_Click);
            // 
            // EditText
            // 
            this.EditText.Dock = System.Windows.Forms.DockStyle.Top;
            this.EditText.EditValue = "";
            this.EditText.Location = new System.Drawing.Point(0, 0);
            this.EditText.Name = "EditText";
            this.EditText.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EditText.Properties.CloseOnLostFocus = false;
            this.EditText.Properties.CloseOnOuterMouseClick = false;
            this.EditText.Properties.PopupControl = this.popupContainerControl1;
            this.EditText.Properties.ShowPopupShadow = false;
            this.EditText.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.EditText.Size = new System.Drawing.Size(295, 20);
            this.EditText.TabIndex = 1;
            this.EditText.EditValueChanged += new System.EventHandler(this.EditText_EditValueChanged_1);
            this.EditText.Popup += new System.EventHandler(this.EditText_Popup);
            // 
            // PLGridCombobox
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.EditText);
            this.Controls.Add(this.popupContainerControl1);
            this.Name = "PLGridCombobox";
            this.Size = new System.Drawing.Size(295, 218);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        public DevExpress.XtraEditors.SimpleButton btnSelect;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraEditors.SimpleButton btnDM;
        public DevExpress.XtraGrid.GridControl gridData;
        public DevExpress.XtraGrid.Views.Grid.PLGridView gridView1;
        public DevExpress.XtraGrid.Columns.GridColumn gcValueMember;
        public DevExpress.XtraGrid.Columns.GridColumn gcDisplayMember;
        public DevExpress.XtraEditors.PopupContainerEdit EditText;


    }
}
