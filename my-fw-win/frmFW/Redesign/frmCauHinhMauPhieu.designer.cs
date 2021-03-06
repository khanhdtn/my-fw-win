using ProtocolVN.Framework.Win;
namespace ProtocolVN.Framework.Win
{
    abstract partial class frmCauHinhMauPhieu
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnXemTruoc = new DevExpress.XtraEditors.SimpleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControlConfig = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControlConfig = new DevExpress.XtraEditors.XtraScrollableControl();
            this.flowLayoutPanelLabel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelPattern = new System.Windows.Forms.FlowLayoutPanel();
            this.groupControlExample = new DevExpress.XtraEditors.GroupControl();
            this.xtraScrollableControlExample = new DevExpress.XtraEditors.XtraScrollableControl();
            this.flowLayoutPanelDemo = new System.Windows.Forms.FlowLayoutPanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlConfig)).BeginInit();
            this.groupControlConfig.SuspendLayout();
            this.xtraScrollableControlConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlExample)).BeginInit();
            this.groupControlExample.SuspendLayout();
            this.xtraScrollableControlExample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(100, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(181, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Đón&g";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnXemTruoc
            // 
            this.btnXemTruoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemTruoc.Location = new System.Drawing.Point(3, 3);
            this.btnXemTruoc.Name = "btnXemTruoc";
            this.btnXemTruoc.Size = new System.Drawing.Size(91, 23);
            this.btnXemTruoc.TabIndex = 1;
            this.btnXemTruoc.Text = "Xem t&rước";
            this.btnXemTruoc.Click += new System.EventHandler(this.btnXemTruoc_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnXemTruoc);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(392, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(263, 31);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.flowLayoutPanel1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 65);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(657, 37);
            this.panelControl1.TabIndex = 6;
            // 
            // groupControlConfig
            // 
            this.groupControlConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlConfig.Controls.Add(this.xtraScrollableControlConfig);
            this.groupControlConfig.Location = new System.Drawing.Point(3, 3);
            this.groupControlConfig.Name = "groupControlConfig";
            this.groupControlConfig.Size = new System.Drawing.Size(473, 56);
            this.groupControlConfig.TabIndex = 0;
            this.groupControlConfig.Text = "Định dạng phát sinh số phiếu";
            // 
            // xtraScrollableControlConfig
            // 
            this.xtraScrollableControlConfig.Controls.Add(this.flowLayoutPanelLabel);
            this.xtraScrollableControlConfig.Controls.Add(this.flowLayoutPanelPattern);
            this.xtraScrollableControlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControlConfig.FireScrollEventOnMouseWheel = true;
            this.xtraScrollableControlConfig.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControlConfig.Name = "xtraScrollableControlConfig";
            this.xtraScrollableControlConfig.Size = new System.Drawing.Size(469, 32);
            this.xtraScrollableControlConfig.TabIndex = 2;
            this.xtraScrollableControlConfig.Scroll += new DevExpress.XtraEditors.XtraScrollEventHandler(this.xtraScrollableControlConfig_Scroll);
            // 
            // flowLayoutPanelLabel
            // 
            this.flowLayoutPanelLabel.AutoSize = true;
            this.flowLayoutPanelLabel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelLabel.Location = new System.Drawing.Point(7, 3);
            this.flowLayoutPanelLabel.Name = "flowLayoutPanelLabel";
            this.flowLayoutPanelLabel.Size = new System.Drawing.Size(183, 25);
            this.flowLayoutPanelLabel.TabIndex = 0;
            // 
            // flowLayoutPanelPattern
            // 
            this.flowLayoutPanelPattern.AutoSize = true;
            this.flowLayoutPanelPattern.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPattern.Location = new System.Drawing.Point(196, 3);
            this.flowLayoutPanelPattern.Name = "flowLayoutPanelPattern";
            this.flowLayoutPanelPattern.Size = new System.Drawing.Size(270, 25);
            this.flowLayoutPanelPattern.TabIndex = 1;
            // 
            // groupControlExample
            // 
            this.groupControlExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlExample.Controls.Add(this.xtraScrollableControlExample);
            this.groupControlExample.Location = new System.Drawing.Point(482, 3);
            this.groupControlExample.Name = "groupControlExample";
            this.groupControlExample.Size = new System.Drawing.Size(173, 56);
            this.groupControlExample.TabIndex = 7;
            this.groupControlExample.Text = "Minh họa";
            // 
            // xtraScrollableControlExample
            // 
            this.xtraScrollableControlExample.Controls.Add(this.flowLayoutPanelDemo);
            this.xtraScrollableControlExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControlExample.FireScrollEventOnMouseWheel = true;
            this.xtraScrollableControlExample.Location = new System.Drawing.Point(2, 22);
            this.xtraScrollableControlExample.Name = "xtraScrollableControlExample";
            this.xtraScrollableControlExample.Size = new System.Drawing.Size(169, 32);
            this.xtraScrollableControlExample.TabIndex = 1;
            this.xtraScrollableControlExample.Scroll += new DevExpress.XtraEditors.XtraScrollEventHandler(this.xtraScrollableControlExample_Scroll);
            // 
            // flowLayoutPanelDemo
            // 
            this.flowLayoutPanelDemo.AutoSize = true;
            this.flowLayoutPanelDemo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelDemo.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelDemo.Name = "flowLayoutPanelDemo";
            this.flowLayoutPanelDemo.Size = new System.Drawing.Size(158, 25);
            this.flowLayoutPanelDemo.TabIndex = 2;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.groupControlExample);
            this.panelControl2.Controls.Add(this.groupControlConfig);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(657, 65);
            this.panelControl2.TabIndex = 11;
            // 
            // frmCauHinhMauPhieu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(657, 102);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCauHinhMauPhieu";
            this.Text = "Thiết lập định dạng số phiếu";
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlConfig)).EndInit();
            this.groupControlConfig.ResumeLayout(false);
            this.xtraScrollableControlConfig.ResumeLayout(false);
            this.xtraScrollableControlConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlExample)).EndInit();
            this.groupControlExample.ResumeLayout(false);
            this.xtraScrollableControlExample.ResumeLayout(false);
            this.xtraScrollableControlExample.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnXemTruoc;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControlConfig;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPattern;
        private DevExpress.XtraEditors.GroupControl groupControlExample;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDemo;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlConfig;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlExample;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLabel;
    }
}