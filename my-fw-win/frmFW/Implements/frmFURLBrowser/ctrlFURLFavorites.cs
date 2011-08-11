using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
	public partial class ctrlFURLFavorites : XtraUserControl {
        /// <summary>
        /// Occurs when [add new favorite].
        /// </summary>
		public event EventHandler AddNewFavorite;
        /// <summary>
        /// Occurs when [edit favorite].
        /// </summary>
		public event EventHandler EditFavorite;
        /// <summary>
        /// Occurs when [delete favorite].
        /// </summary>
		public event EventHandler DeleteFavorite;
        /// <summary>
        /// Occurs when [open favorite].
        /// </summary>
		public event EventHandler OpenFavorite;

        /// <summary>
        /// Initializes a new instance of the <see cref="ctrlFavorites"/> class.
        /// </summary>
		public ctrlFURLFavorites() {
			InitializeComponent();
			ItemsEnabled();
		}

        /// <summary>
        /// Items the enabled.
        /// </summary>
		private void ItemsEnabled() {
			iOpen.Enabled = iDelete.Enabled = iEdit.Enabled = listBox1.SelectedIndex >= 0;
		}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e) {
			ItemsEnabled();
		}

        /// <summary>
        /// Deletes the items.
        /// </summary>
		public void DeleteItems() {
			listBox1.Items.Clear();
		}

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="init">if set to <c>true</c> [init].</param>
		public void AddItem(BarItem item, bool init) {
			listBox1.Items.Add(item.Caption);
			if(!init) listBox1.SelectedIndex = listBox1.Items.Count - 1;
		}

        /// <summary>
        /// Handles the ItemClick event of the iAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
		private void iAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			if(AddNewFavorite != null) AddNewFavorite(this, EventArgs.Empty);
		}

        /// <summary>
        /// Handles the ItemClick event of the iEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
		private void iEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			int i = listBox1.SelectedIndex;
			if(EditFavorite != null && listBox1.SelectedItem != null) 
				EditFavorite(listBox1.SelectedItem.ToString(), EventArgs.Empty);
			listBox1.SelectedIndex = i;	
		}

        /// <summary>
        /// Handles the ItemClick event of the iDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
		private void iDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			int i = listBox1.SelectedIndex;
			if(DeleteFavorite != null && listBox1.SelectedItem != null) 
				DeleteFavorite(listBox1.SelectedItem.ToString(), EventArgs.Empty);
			try {
				listBox1.SelectedIndex = i;	
			} catch {}
			ItemsEnabled();
		}

        /// <summary>
        /// Does the open favorite.
        /// </summary>
		private void DoOpenFavorite() {
			if(OpenFavorite != null && listBox1.SelectedItem != null) 
				OpenFavorite(listBox1.SelectedItem.ToString(), EventArgs.Empty);
		}

        /// <summary>
        /// Handles the ItemClick event of the iOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
		private void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			DoOpenFavorite();
		}

        /// <summary>
        /// Handles the DoubleClick event of the listBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void listBox1_DoubleClick(object sender, System.EventArgs e) {
			DoOpenFavorite();
		}
	}
}
