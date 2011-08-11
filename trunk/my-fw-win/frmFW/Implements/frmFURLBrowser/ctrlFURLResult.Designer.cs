namespace ProtocolVN.Framework.Win
{
    public partial class ctrlFURLResult
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
            this.result_list = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.result_list)).BeginInit();
            this.SuspendLayout();
            // 
            // result_list
            // 
            this.result_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result_list.ItemHeight = 15;
            this.result_list.Location = new System.Drawing.Point(0, 0);
            this.result_list.Name = "result_list";
            this.result_list.Size = new System.Drawing.Size(256, 283);
            this.result_list.TabIndex = 5;
            this.result_list.DoubleClick += new System.EventHandler(this.result_list_DoubleClick);
            // 
            // ctrlResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.result_list);
            this.Name = "ctrlResult";
            this.Size = new System.Drawing.Size(256, 283);
            ((System.ComponentModel.ISupportInitialize)(this.result_list)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl result_list;

    }
}
