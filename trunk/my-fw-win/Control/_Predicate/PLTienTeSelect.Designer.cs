

namespace ProtocolVN.Framework.Win
{
    partial class PLTienTeSelect
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
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.rdotiente = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdotiente.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // calcEdit1
            // 
            this.calcEdit1.Dock = System.Windows.Forms.DockStyle.Right;
            this.calcEdit1.Location = new System.Drawing.Point(144, 0);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calcEdit1.Properties.Appearance.Options.UseFont = true;
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit1.Properties.Mask.EditMask = "c";
            this.calcEdit1.Size = new System.Drawing.Size(70, 20);
            this.calcEdit1.TabIndex = 1;
            this.calcEdit1.Leave += new System.EventHandler(this.calcEdit1_Leave);
            // 
            // rdotiente
            // 
            this.rdotiente.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdotiente.Location = new System.Drawing.Point(0, 0);
            this.rdotiente.Name = "rdotiente";
            this.rdotiente.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdotiente.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.rdotiente.Properties.Appearance.Options.UseBackColor = true;
            this.rdotiente.Properties.Appearance.Options.UseFont = true;
            this.rdotiente.Properties.Appearance.Options.UseTextOptions = true;
            this.rdotiente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rdotiente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdotiente.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "VNĐ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại Tệ")});
            this.rdotiente.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.rdotiente.Size = new System.Drawing.Size(138, 21);
            this.rdotiente.TabIndex = 2;
            this.rdotiente.SelectedIndexChanged += new System.EventHandler(this.rdotiente_SelectedIndexChanged);
            // 
            // PLTienTeSelect
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdotiente);
            this.Controls.Add(this.calcEdit1);
            this.Name = "PLTienTeSelect";
            this.Size = new System.Drawing.Size(214, 21);
            this.Load += new System.EventHandler(this.PLTienTeSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdotiente.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        //[DoubleRange(1,10000000000,ErrorMessage="Bạn
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private DevExpress.XtraEditors.RadioGroup rdotiente;
    }
}
