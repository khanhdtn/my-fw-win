using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using System.Data.Common;

namespace ProtocolVN.Plugin.WarningSystem
{
    public partial class frmWarningManager : DevExpress.XtraEditors.XtraForm
    {
        private Dictionary<string, IWarningDefine> dicWarning = null;
        public frmWarningManager()
        {
            InitializeComponent();
            dicWarning = new Dictionary<string, IWarningDefine>();
        }

        private void InitGrid()
        {
            this.MaximizeBox = false;
            XtraGridSupportExt.ComboboxGridColumn(gridColumn2, "FW_DM_WARNING", "ID", "NAME");
            gridColumn1.OptionsColumn.AllowEdit = false;
            gridColumn2.OptionsColumn.AllowEdit = false;
            gridColumn5.OptionsColumn.AllowEdit = false;
            gridView1.OptionsSelection.MultiSelect = false;
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                string name = gridView1.GetFocusedRowCellValue(gridColumn1).ToString();
                IWarningDefine war;
                dicWarning.TryGetValue(name, out war);
                if (war.CheckConfig())
                    btnConfig.Enabled = true;
                else
                    btnConfig.Enabled = false;
            }
            catch { }
        }
        private void frmWarningManager_Load(object sender, EventArgs e)
        {
            InitGrid();
            DbCommand command = DABase.getDatabase().GetSQLStringCommand("SELECT * FROM FW_WARNINGINFO WHERE USERID='"+ FrameworkParams.currentUser.id + "'");
            DataSet ds = DABase.getDatabase().LoadDataSet(command);
            gridControl1.DataSource = ds.Tables[0];
            dicWarning = WarningSupport.GetWarning();
            btnConfig.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataSet ds = ((DataView)gridView1.DataSource).Table.DataSet;
            ds.Tables[0].TableName = "FW_WARNINGINFO";
            if (DABase.getDatabase().UpdateTable(ds) != -1)
                this.Close();
            else
                PLMessageBox.ShowErrorMessage("Lưu dữ liệu không thành công");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnConfig_Click(object sender, EventArgs e)
        {
            int id = HelpNumber.ParseInt32(gridView1.GetFocusedRowCellValue(gridColumn4).ToString());
            string name = gridView1.GetFocusedRowCellValue(gridColumn1).ToString();
            IWarningDefine war;
            dicWarning.TryGetValue(name, out war);
            war.ShowConfig(id);
        }
    }
}