using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public partial class frmTitleFilter : XtraForm, IPublicForm
    {
        
        public frmTitleFilter(string title)
        {
            InitializeComponent();
            this.txt_Title.Text = title;
        }

        XtraForm parentForm;
        public frmTitleFilter(string title,XtraForm parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.txt_Title.Text = title;
        }
        bool ChapNhan = true;
        delegate void dlgSendData(string s);
        delegate void dlgKhongLuu(bool b);
        private void btn_DongY_Click(object sender, EventArgs e)
        {
            if (this.txt_Title.Text.Trim().Length == 0)
                return;
            dlgSendData temp = new dlgSendData(((ISaveQuery)parentForm).getvalueFromChild);
            temp(this.txt_Title.Text);
            ChapNhan = true;
            this.Close();
        }

        private void frmTitileFilter_Load(object sender, EventArgs e)
        {

        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            dlgKhongLuu temp = new dlgKhongLuu(((ISaveQuery)parentForm).KhongLuu);
            temp(false);
            ChapNhan = false;
            this.Close();
        }

        private void frmTitileFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            dlgKhongLuu temp = new dlgKhongLuu(((ISaveQuery)parentForm).KhongLuu);
            temp(ChapNhan);
        }
    }
}