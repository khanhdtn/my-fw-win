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
    public partial class frmAssignUser : DevExpress.XtraEditors.XtraForm
    {
        private List<IWarning> lstAssWarning;
        private List<IWarningDefine> lstWarning;
        private DataTable dtWarning;
        private DataSet dsWarInfo;
        private Dictionary<string, DataTable> dicWarOfUser;
        private string activeUser = "";
        public frmAssignUser()
        {
            InitializeComponent();
            lstAssWarning = new List<IWarning>();
            lstWarning = new List<IWarningDefine>();
            dicWarOfUser = new Dictionary<string, DataTable>();
        }

        private void InitGrid()
        {
            gridView1.GroupPanelText = "Danh sách cảnh báo";
            gridColumn3.OptionsColumn.AllowEdit =false;
            gridColumn5.OptionsColumn.AllowEdit = false;
            XtraGridSupportExt.ComboboxGridColumn(gridColumn3, "FW_DM_WARNING", "ID", "NAME");
            gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
        }

        
        private void Init()
        {
            // dw.name typename
            DbCommand command = DABase.getDatabase().GetSQLStringCommand(@"SELECT wi.id,wi.userid,wi.description, wi.warn_type, wi.name,wi.state 
                                                                          FROM fw_warninginfo wi, fw_dm_warning dw
                                                                          WHERE wi.warn_type = dw.id");
            dsWarInfo = DABase.getDatabase().LoadDataSet(command,"FW_WARNINGINFO");
            //HelpGridColumn.CotCheckEdit(gridColumn2, "STATE");
            AddUser();
            InitGrid();
            lstBoxUser.SelectedIndex = 0;
        }
        private void frmAssignUser_Load(object sender, EventArgs e)
        {
            Init();
            lstAssWarning = WarningSupport.LoadAllWarning();
            GetAllWarning();

        }
        void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == gridColumn1)
            {
                DataRow[] row = dtWarning.Select("NAME='" + e.Value + "'");
                DataTable dt = ((DataView)(gridView1.DataSource)).Table;
                gridView1.SetFocusedRowCellValue(gridColumn3, row[0]["WARN_TYPE"]);
                gridView1.SetFocusedRowCellValue(gridColumn5,row[0]["DESCRIPTION"]);
                gridView1.SetFocusedRowCellValue(gridColumn2, 0);
                gridView1.SetFocusedRowCellValue(gridColumn4, activeUser);
            }
        }
        private void AddUser()
        {
            DataSet dsUser = DABase.getDatabase().LoadTable("USER_CAT", new string[] { "USERID", "USERNAME" });
            lstBoxUser.DataSource = dsUser.Tables[0];
            lstBoxUser.DisplayMember = "USERNAME";
            lstBoxUser.ValueMember = "USERID";
        }
        private void GetAllWarning()
        {
            dtWarning = new DataTable();
            DataColumn dcName = new DataColumn("NAME");
            dcName.Caption = "Tên cảnh báo";

            DataColumn dcType = new DataColumn("WARN_TYPE");
            dcType.Caption = "ID";
            
            DataColumn dcTypeName = new DataColumn("TYPENAME");            
            dcTypeName.Caption = "Loại cảnh báo";
            
            DataColumn dcDescription = new DataColumn("DESCRIPTION");            
            dcDescription.Caption = "Mô tả";
            
            dtWarning.Columns.AddRange(new DataColumn[] {dcName, dcDescription, dcTypeName, dcType });
            
            DataSet dsDmWarning = DABase.getDatabase().LoadTable("FW_DM_WARNING");
            foreach (IWarning warning in lstAssWarning)
            {
                foreach (IWarningDefine warningdefine in warning.GetWarning())
                {
                    lstWarning.Add(warningdefine);
                    DataRow[] row = dsDmWarning.Tables[0].Select("ID='" + (int)warningdefine.Type + "'");
                    dtWarning.Rows.Add(warningdefine.Name, warningdefine.Description, row[0][1].ToString(), (int)warningdefine.Type);
                }
            }
            repositoryItemGridLookUpEdit3.DataSource = dtWarning;
            repositoryItemGridLookUpEdit3.DisplayMember = "NAME";
            repositoryItemGridLookUpEdit3.ValueMember = "NAME";
            repositoryItemGridLookUpEdit3.View.Columns["WARN_TYPE"].Visible = false;
        }

        private void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.FocusedRowHandle += -1;
                //gridView1.MovePrev();
            }
            catch { gridView1.FocusedRowHandle += 1; }
            DataSet dsCollect = DABase.getDatabase().LoadTable("FW_WARNINGINFO");
            //DataTable dtCollect = dsWarInfo.Tables[0];
            DataTable dtCollect = dsCollect.Tables[0];
            //dsCollect.Tables.Add(dtCollect);
            foreach (DataTable dt in dicWarOfUser.Values)
            {
                dtCollect.Merge(dt);
            }

            foreach (DataRow row in dtCollect.Rows)
            {
                if (row.RowState!= DataRowState.Deleted && row["ID"].ToString() == "" )
                    row["ID"] = DABase.getDatabase().GetID("G_WARNING");
            }
            if (DABase.getDatabase().UpdateTable(dsCollect,"FW_WARNINGINFO") == -1)
            {
                PLDebug.ShowExceptionInfo(PLException.GetLastestExceptions());                
                PLMessageBox.ShowErrorMessage("Lưu không thành công");
            }
            else
                HelpMsgBox.ShowNotificationMessage("Lưu thành công");
        }

        private void lstBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstBoxUser.SelectedValue is DataRowView)
                    activeUser = ((DataRowView)lstBoxUser.SelectedValue).Row[0].ToString();
                else
                    activeUser = lstBoxUser.SelectedValue.ToString();

                DataTable dt = dsWarInfo.Tables[0].Clone();
                dt.Columns["NAME"].Unique = true;
                //string  id = ((DataRowView)lstBoxUser.SelectedValue).Row[0].ToString();
                if (!dicWarOfUser.ContainsKey(activeUser))
                {
                    DataRow[] dr = dsWarInfo.Tables[0].Select("USERID=" + activeUser);
                    foreach (DataRow row in dr)
                        dt.ImportRow(row);
                    dicWarOfUser.Add(activeUser, dt);
                }
                else
                    dicWarOfUser.TryGetValue(activeUser, out dt);
                gridControl1.DataSource = dt;
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
            }
        }

        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.DeleteSelectedRows();
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }

        private void barBtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}