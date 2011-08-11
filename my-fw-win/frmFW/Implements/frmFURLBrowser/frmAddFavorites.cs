using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
	/// <summary>
	/// Summary description for frmAddFavorites.
	/// </summary>
	public partial class frmAddFavorites : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Gets the name of the location.
        /// </summary>
        /// <value>The name of the location.</value>
		public string LocationName {
			get { return textBox1.Text; }
		}

        /// <summary>
        /// Gets the location URL.
        /// </summary>
        /// <value>The location URL.</value>
		public string LocationURL {
			get { return textBox2.Text; }
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="frmAddFavorites"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="URL">The URL.</param>
        /// <param name="img">The img.</param>
		public frmAddFavorites(string name, string URL, Image img) : this(name, URL, img, true) {}
        /// <summary>
        /// Initializes a new instance of the <see cref="frmAddFavorites"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="URL">The URL.</param>
        /// <param name="img">The img.</param>
        /// <param name="isAdd">if set to <c>true</c> [is add].</param>
		public frmAddFavorites(string name, string URL, Image img, bool isAdd) {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			textBox1.Text = name;
			textBox2.Text = URL;
			pictureBox1.Image = img;
			if(!isAdd) {
                Text = "Sửa FURL";
                label1.Text = "Thay đổi thông tin mô tả FURL.";
			}
		}

		#region Windows Form Designer generated code
		
		#endregion

	}
}
