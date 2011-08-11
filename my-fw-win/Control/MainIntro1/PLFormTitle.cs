using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Khi dùng đặt chiều cao bằng 40.
    /// </summary>
    public class PLFormTitle : LabelControl
    {
        public PLFormTitle(): base()
        {
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseTextOptions = true;
            this.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Dock = System.Windows.Forms.DockStyle.Top;
        }

        public void _init(String Title, Form form)
        {
            this.Text = Title;
            form.Controls.Add(this);
            this.Size = new System.Drawing.Size(form.Size.Width, 35);
        }

        public static PLFormTitle CreateFormTitle(String Title, Form form)
        {
            PLFormTitle title = new PLFormTitle();
            title._init(Title, form);
            return title;
        }
    }
}
