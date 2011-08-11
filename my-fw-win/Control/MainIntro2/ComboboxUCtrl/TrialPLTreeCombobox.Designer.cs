
namespace ProtocolVN.Framework.Win
{
    partial class TrialPLTreeCombobox
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
            this.plDataTree1 = new PLDataTree();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.EditText = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plDataTree1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.popupContainerControl1.AutoSize = true;
            this.popupContainerControl1.Controls.Add(this.plDataTree1);
            this.popupContainerControl1.Controls.Add(this.btnSelect);
            this.popupContainerControl1.Controls.Add(this.btnCancel);
            this.popupContainerControl1.Location = new System.Drawing.Point(2, 22);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(264, 234);
            this.popupContainerControl1.TabIndex = 0;
            this.popupContainerControl1.Text = "popupContainerControl1";
            // 
            // plDataTree1
            // 
            this.plDataTree1.Location = new System.Drawing.Point(3, 0);
            this.plDataTree1.Name = "plDataTree1";
            this.plDataTree1.OptionsBehavior.Editable = false;
            this.plDataTree1.OptionsBehavior.UseTabKey = true;
            this.plDataTree1.OptionsView.AutoCalcPreviewLineCount = true;
            this.plDataTree1.Size = new System.Drawing.Size(258, 202);
            this.plDataTree1.TabIndex = 0;
            this.plDataTree1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.plDataTree1_KeyDown);
            this.plDataTree1.DoubleClick += new System.EventHandler(this.dataTree_DoubleClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Appearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnSelect.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSelect.Appearance.Options.UseBorderColor = true;
            this.btnSelect.Appearance.Options.UseForeColor = true;
            this.btnSelect.Location = new System.Drawing.Point(138, 206);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(55, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Chọn";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Appearance.Options.UseBorderColor = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.Location = new System.Drawing.Point(199, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Bỏ chọn";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.EditText.Size = new System.Drawing.Size(267, 20);
            this.EditText.TabIndex = 1;
            this.EditText.EditValueChanged += new System.EventHandler(this.EditText_EditValueChanged_1);
            this.EditText.Popup += new System.EventHandler(this.EditText_Popup);
            // 
            // PLTreeCombobox
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.EditText);
            this.Controls.Add(this.popupContainerControl1);
            this.Name = "PLTreeCombobox";
            this.Size = new System.Drawing.Size(267, 257);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plDataTree1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        public DevExpress.XtraEditors.SimpleButton btnSelect;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraEditors.PopupContainerEdit EditText;
        private PLDataTree plDataTree1;


    }
}
