using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class ctrlFURLResult : XtraUserControl
    {
        /// <summary>
        /// Occurs when [open URL].
        /// </summary>
        public event EventHandler OpenURL;

        /// <summary>
        /// Initializes a new instance of the <see cref="ctrlResult"/> class.
        /// </summary>
        public ctrlFURLResult()
        {
            InitializeComponent();
        }

        /// <summary>
        /// _inits the specified list_obj.
        /// </summary>
        /// <param name="list_obj">The list_obj.</param>
        public void _init(List<FURLAddress> list_obj)
        {
            result_list.Items.Clear();            
            foreach (FURLAddress obj in list_obj)
                result_list.Items.Add(obj.URL_FORM);                         
        }

        /// <summary>
        /// Does the open URL.
        /// </summary>
        private void DoOpenURL()
        {
            if (OpenURL != null && result_list.SelectedItem != null)
                OpenURL(result_list.SelectedItem.ToString(), EventArgs.Empty);
        }

        /// <summary>
        /// Handles the DoubleClick event of the result_list control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void result_list_DoubleClick(object sender, EventArgs e)
        {
            DoOpenURL();
        }
    }
}
