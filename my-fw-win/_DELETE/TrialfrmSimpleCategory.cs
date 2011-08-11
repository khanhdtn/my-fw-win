using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    //PHUOCNC : Thay bằng cách dùng frmItemPluginCat
    public partial class TrialfrmSimpleCategory : XtraForm
    {
        public PLDynamicGrid grid;
        
        public DataSet ds;
        private bool isChange = false;
        public bool IsChange
        {
            get
            {
                return isChange;
            }
        }
        public TrialfrmSimpleCategory(){
            InitializeComponent();
        }

        public TrialfrmSimpleCategory(DataSet ds, string[] listCaption)
        {
            InitializeComponent();
            this.ds = ds;
            this.grid = new PLDynamicGrid(gridData, ds, true, listCaption);
        }

        private void SimpleCategory_Load(object sender, EventArgs e)
        {          
            btnNotSave.Enabled = false;

            
            object obj = TagPropertyMan.Get(this.Tag, "FORM_PARAM");
            if (obj != null && obj.ToString() != "")
            {
                //PHUOC 
                string[] tokens = obj.ToString().Split(",".ToCharArray());
                this.ds = DABase.getDatabase().LoadTable(tokens[0]);
                this.Text = tokens[1];
                string[] caption = new string[tokens.Length - 2];
                for (int i = 2; i < tokens.Length; i++)
                {
                    caption[i - 2] = tokens[i];
                }
                this.grid = new PLDynamicGrid(gridData, ds, true, caption);            
                //Custom validation
                grid.gv.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridview_CellValueChanging);
            }
            else{
                //NOOP
            }            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (ds.HasChanges())
            {
                DialogResult kq = PLMessageBox.ShowConfirmMessage("Bạn có muốn lưu lại dữ liệu vừa thay đổi không ?");
                if (kq == DialogResult.Yes)
                {
                    // save data and exit
                    btnSave_Click(null, null);
                    this.Close();
                }
                else if (kq == DialogResult.No)
                {
                    //exit not save   
                    ds.RejectChanges();
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DABase.getDatabase().UpdateTable(ds) == -1)
            {
                PLMessageBox.ShowErrorMessage("Quá trình lưu xuất hiện lỗi! Vui lòng kiểm tra lại dữ liệu.");
                //ds.RejectChanges();
            }
            else
            {
                ds.AcceptChanges();
                isChange = true;
                btnNotSave.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (PLMessageBox.ShowConfirmMessage("Bạn có chắn chắn muốn xóa dữ liệu này không ?") == DialogResult.Yes)
            {
                DevExpress.XtraGrid.Views.Grid.GridViewPL gv = (DevExpress.XtraGrid.Views.Grid.GridViewPL)gridData.MainView;
                if (gv.SelectedRowsCount <= 0) return;
                int index = gv.GetSelectedRows()[0];
                if (index >= 0)
                    ((DataTable)gridData.DataSource).DefaultView.Delete(index);
                btnNotSave.Enabled = true;
            }
        }
      
        private void btnNotSave_Click(object sender, EventArgs e)
        {
            ds.RejectChanges();
            btnNotSave.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            grid.gv.FocusedRowHandle = -999998;
            grid.gv.FocusedColumn = grid.gv.VisibleColumns[0];
            grid.gv.OptionsSelection.EnableAppearanceHideSelection = false;
            grid.gv.ShowEditor();
        }

        private void gridview_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //TODO            
        }  
    }
}