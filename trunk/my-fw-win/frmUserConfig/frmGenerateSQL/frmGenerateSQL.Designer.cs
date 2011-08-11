using ProtocolVN.Framework.Win;
namespace ProtocolVN.Framework.Win
{
    partial class frmGenerateSQL
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
            ICSharpCode.TextEditor.Document.DefaultFormattingStrategy defaultFormattingStrategy64 = new ICSharpCode.TextEditor.Document.DefaultFormattingStrategy();
            ICSharpCode.TextEditor.Document.DefaultHighlightingStrategy defaultHighlightingStrategy64 = new ICSharpCode.TextEditor.Document.DefaultHighlightingStrategy();
            ICSharpCode.TextEditor.Document.GapTextBufferStrategy gapTextBufferStrategy64 = new ICSharpCode.TextEditor.Document.GapTextBufferStrategy();
            ICSharpCode.TextEditor.Document.DefaultTextEditorProperties defaultTextEditorProperties64 = new ICSharpCode.TextEditor.Document.DefaultTextEditorProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerateSQL));
            this.cbObjDb_main = new ProtocolVN.Framework.Win.PLCombobox();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_DoiTuong = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbObjDb_sub = new ProtocolVN.Framework.Win.PLCombobox();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.memo_script = new ICSharpCode.TextEditor.TextEditorControl();
            this.obj_lb = new DevExpress.XtraEditors.LabelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbObjDb_main
            // 
            this.cbObjDb_main.DataSource = null;
            this.cbObjDb_main.DisplayField = null;
            this.cbObjDb_main.Location = new System.Drawing.Point(101, 35);
            this.cbObjDb_main.Name = "cbObjDb_main";
            this.cbObjDb_main.Size = new System.Drawing.Size(180, 20);
            this.cbObjDb_main.TabIndex = 0;
            this.cbObjDb_main.ValueField = null;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopy.Location = new System.Drawing.Point(12, 451);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy Script";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lbl_DoiTuong
            // 
            this.lbl_DoiTuong.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DoiTuong.Appearance.Options.UseFont = true;
            this.lbl_DoiTuong.Location = new System.Drawing.Point(12, 62);
            this.lbl_DoiTuong.Name = "lbl_DoiTuong";
            this.lbl_DoiTuong.Size = new System.Drawing.Size(36, 13);
            this.lbl_DoiTuong.TabIndex = 46;
            this.lbl_DoiTuong.Text = "Script:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(201, 13);
            this.labelControl1.TabIndex = 47;
            this.labelControl1.Text = "Chọn đối tượng cần phát sinh script:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(287, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(108, 13);
            this.labelControl2.TabIndex = 49;
            this.labelControl2.Text = "Các đối tượng yêu cầu";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 38);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 13);
            this.labelControl3.TabIndex = 50;
            this.labelControl3.Text = "Đối tượng chính";
            // 
            // cbObjDb_sub
            // 
            this.cbObjDb_sub.DataSource = null;
            this.cbObjDb_sub.DisplayField = null;
            this.cbObjDb_sub.Location = new System.Drawing.Point(401, 35);
            this.cbObjDb_sub.Name = "cbObjDb_sub";
            this.cbObjDb_sub.Size = new System.Drawing.Size(180, 20);
            this.cbObjDb_sub.TabIndex = 51;
            this.cbObjDb_sub.ValueField = null;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 81);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.memo_script);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.obj_lb);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(662, 364);
            this.splitContainerControl1.SplitterPosition = 268;
            this.splitContainerControl1.TabIndex = 52;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // memo_script
            // 
            this.memo_script.Dock = System.Windows.Forms.DockStyle.Fill;            
            defaultHighlightingStrategy64.Extensions = new string[0];            
            defaultTextEditorProperties64.AllowCaretBeyondEOL = false;
            defaultTextEditorProperties64.AutoInsertCurlyBracket = true;
            defaultTextEditorProperties64.BracketMatchingStyle = ICSharpCode.TextEditor.Document.BracketMatchingStyle.After;
            defaultTextEditorProperties64.ConvertTabsToSpaces = false;
            defaultTextEditorProperties64.CreateBackupCopy = false;
            defaultTextEditorProperties64.DocumentSelectionMode = ICSharpCode.TextEditor.Document.DocumentSelectionMode.Normal;
            defaultTextEditorProperties64.EnableFolding = true;
            defaultTextEditorProperties64.Encoding = ((System.Text.Encoding)(resources.GetObject("defaultTextEditorProperties64.Encoding")));
            defaultTextEditorProperties64.Font = new System.Drawing.Font("Courier New", 10F);
            defaultTextEditorProperties64.HideMouseCursor = false;
            defaultTextEditorProperties64.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.Smart;
            defaultTextEditorProperties64.IsIconBarVisible = true;
            defaultTextEditorProperties64.LineTerminator = "\r\n";
            defaultTextEditorProperties64.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.None;
            defaultTextEditorProperties64.MouseWheelScrollDown = true;
            defaultTextEditorProperties64.MouseWheelTextZoom = true;
            defaultTextEditorProperties64.ShowEOLMarker = false;
            defaultTextEditorProperties64.ShowHorizontalRuler = false;
            defaultTextEditorProperties64.ShowInvalidLines = false;
            defaultTextEditorProperties64.ShowLineNumbers = true;
            defaultTextEditorProperties64.ShowMatchingBracket = true;
            defaultTextEditorProperties64.ShowSpaces = false;
            defaultTextEditorProperties64.ShowTabs = true;
            defaultTextEditorProperties64.ShowVerticalRuler = true;
            defaultTextEditorProperties64.TabIndent = 4;
            defaultTextEditorProperties64.UseAntiAliasedFont = false;
            defaultTextEditorProperties64.VerticalRulerRow = 80;            
            this.memo_script.Encoding = ((System.Text.Encoding)(resources.GetObject("memo_script.Encoding")));
            this.memo_script.Location = new System.Drawing.Point(0, 0);
            this.memo_script.Name = "memo_script";
            this.memo_script.ShowInvalidLines = false;
            this.memo_script.ShowTabs = true;
            this.memo_script.ShowVRuler = true;
            this.memo_script.Size = new System.Drawing.Size(658, 264);
            this.memo_script.TabIndex = 0;
            this.memo_script.TextEditorProperties = defaultTextEditorProperties64;
            this.memo_script.Load += new System.EventHandler(this.memo_script_Load);
            // 
            // obj_lb
            // 
            this.obj_lb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.obj_lb.Appearance.Options.UseFont = true;
            this.obj_lb.Location = new System.Drawing.Point(5, 6);
            this.obj_lb.Name = "obj_lb";
            this.obj_lb.Size = new System.Drawing.Size(0, 13);
            this.obj_lb.TabIndex = 51;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(93, 451);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 53;
            this.btnExport.Text = "Export Script";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmGenerateSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 481);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.cbObjDb_sub);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lbl_DoiTuong);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.cbObjDb_main);
            this.Name = "frmGenerateSQL";
            this.Text = "Generate SQL";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PLCombobox cbObjDb_main;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private DevExpress.XtraEditors.LabelControl lbl_DoiTuong;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private PLCombobox cbObjDb_sub;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.LabelControl obj_lb;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private ICSharpCode.TextEditor.TextEditorControl memo_script;

    }
}

