using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Plugin.ImpExp
{
    public partial class frmMinImport : DevExpress.XtraEditors.XtraForm
    {
        public frmMinImport()
        {
            InitializeComponent();
            gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            DataSet ds = DABase.getDatabase().LoadTable("DM_HANG_HOA");
            ImportData(gridView1, ds.Tables[0], "DM_HANG_HOA",new string[]{"CLOAI_ID"},new string[]{"DM_CHUNG_LOAI"},new string[]{"ID"},new string[]{"NAME"});
            gridView1.BestFitColumns();
        }

        void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            gridView1.BestFitColumns();
        }
        private void ImportData(GridView gridView,DataTable dt , string tableName, string[] fieldName,string[] tenDM, string[] valueFieldDM,string[] displayFieldDM)
        {
            HelpUtil.CreateGridColumn(gridView, dt, tableName);
            for (int i = 0; i < fieldName.Length; i++)
            {
                GridColumn column = FindColumn(gridView,fieldName[i]);
                HelpUtil.CreateComboItemDM(column, tenDM[i], valueFieldDM[i], displayFieldDM[i], fieldName[i]);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = HelpUtil.SetDataSourceFromExcel();
        }
        private GridColumn FindColumn(GridView gridView, string field)
        {
            foreach (GridColumn column in gridView.Columns)
            {
                if (column.FieldName.Equals(field))
                    return column;
            }
            return null;
        }
    }
}