namespace ProtocolVN.Framework.Win
{
    partial class PLDateTimeRange
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
            this.cbkybaocao = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cbkybaocao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbkybaocao
            // 
            this.cbkybaocao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbkybaocao.Location = new System.Drawing.Point(0, 0);
            this.cbkybaocao.Name = "cbkybaocao";
            this.cbkybaocao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbkybaocao.Properties.Items.AddRange(new object[] {
            "Ngày hiện tại",
            "Tuần hiện tại",
            "Tháng hiện tại",
            "Quý hiện tại",
            "Năm hiện tại",
            "---Ngày hôm qua",
            "---Tuần vừa rồi",
            "---Tháng trước",
            "---Quý trước",
            "---Năm trước",
            "Ngày đầu tuần đến nay",
            "Ngày đầu tháng đến nay",
            "Ngày đầu quý đến nay",
            "Ngày đầu năm đến nay",
            "---Tháng 1",
            "---Tháng 2",
            "---Tháng 3",
            "---Tháng 4",
            "---Tháng 5",
            "---Tháng 6",
            "---Tháng 7",
            "---Tháng 8",
            "---Tháng 9",
            "---Tháng 10",
            "---Tháng 11",
            "---Tháng 12"});
            this.cbkybaocao.Size = new System.Drawing.Size(184, 20);
            this.cbkybaocao.TabIndex = 0;
            this.cbkybaocao.SelectedIndexChanged += new System.EventHandler(this.cbkybaocao_SelectedIndexChanged);
            // 
            // PLDateTimeRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbkybaocao);
            this.Name = "PLDateTimeRange";
            this.Size = new System.Drawing.Size(184, 20);
            ((System.ComponentModel.ISupportInitialize)(this.cbkybaocao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.ComboBoxEdit cbkybaocao;

    }
}
