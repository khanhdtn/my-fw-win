using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Columns;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Plugin.ImpExp
{
    public partial class frmResolveDM : DevExpress.XtraEditors.XtraForm
    {
        private Dictionary<string, DataTable> lstDataSource;
        private Dictionary<string, DataTable> lstItemDM;
        private string itemCreateNew;
        private string IdDM;
        private string NameDM;
        public frmResolveDM()
        {
            InitializeComponent();
            IdDM = "ID";
            NameDM = "NAME";
            itemCreateNew = "[Thêm mới]";
            lstDataSource = new Dictionary<string, DataTable>();
            lstItemDM = new Dictionary<string, DataTable>();
        }

        private DataTable dtValueInfo;
        private static DataTable dtOutput= null;
        public DataTable OutputTable
        {
            get { return dtOutput; }
        }

        public frmResolveDM(DataTable dt):this()
        {
            dtValueInfo = dt;
            AddDM(dt);
            Init();
            try {
                lstDanhMuc.SelectedIndex = 0;
                CreateValue(gridColumn2, lstDanhMuc.SelectedValue.ToString());
                AddDataSourceDM();
                
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }

        private void Init()
        {
            lstDanhMuc.SelectedIndexChanged += new EventHandler(lstDanhMuc_SelectedIndexChanged);
        }
        
        private void SetIdForDMSource(string TenDM, ref DataTable dt)
        {
            DataTable dtDM;
            lstItemDM.TryGetValue(TenDM, out dtDM);
            //Thay doi lai NAME, ID lay field so sanh va field gia tri
            foreach(DataRow row in dt.Rows)
            {
                DataRow[] dr = dtDM.Select(NameDM + "='" + row["LIKE_VALUE"] + "'");
                if (dr.Length > 0)
                    row["ID"] = dr[0][IdDM];
                else
                    row["ID"] = "-1";
            }
        }

        private void lstDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddDataSourceDM();
            CreateValue(gridColumn2, lstDanhMuc.SelectedValue.ToString());
            gridView1.FocusedRowHandle = 0;
            //gridView1_FocusedRowChanged(null, null);
        }

        //Gan datatable tuong ung cua tung danh muc vao trong lstDataSource
        private void AddDataSourceDM()
        {
            string tenDM = lstDanhMuc.SelectedValue.ToString();
            if (!lstDataSource.ContainsKey(tenDM))
            {
                DataTable dt = DataSourceDM(dtValueInfo, tenDM);
                lstDataSource.Add(tenDM, dt);
                gridControl1.DataSource = dt;
            }
            else
            {
                DataTable dt;
                lstDataSource.TryGetValue(tenDM, out dt);
                gridControl1.DataSource = dt;
            }
        }
        //Tao bang datasource cho tung loai danh muc
        private DataTable CreateSourceDM()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ORG_VALUE"));
            dt.Columns.Add(new DataColumn("LIKE_VALUE"));
            //dt.Columns.Add(new DataColumn("CHOICE_VALUE"));
            dt.Columns.Add(new DataColumn("ID"));
            return dt;
        }

        //Add cac danh muc vao list box
        private List<string> AddDM(DataTable dt)
        {
            List<string> lstDM = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string name = dr["TEN_DM"].ToString();
                if (!lstDM.Contains(name))
                {
                    lstDM.Add(name);
                    lstDanhMuc.Items.Add(name);
                    if (!lstItemDM.ContainsKey(name))
                    {
                        DataSet ds = DABase.getDatabase().LoadTable(name);
                        lstItemDM.Add(name, ds.Tables[0]);
                    }
                }
            }
            return lstDM;
        }
        //Tao datasource cho tung loai danh muc
        private DataTable DataSourceDM(DataTable dt, string tenDM)
        {
            DataRow[] dr = dt.Select("TEN_DM = '" + tenDM + "'");
            DataTable source = CreateSourceDM();
            List<string> lstValue = new List<string>();
            foreach (DataRow row in dr)
            {
                string value = row["ORG_VALUE"].ToString();
                if (!lstValue.Contains(value))
                {
                    DataRow newRow = source.NewRow();
                    newRow["ORG_VALUE"] = value;
                    DataTable dtItemDM;
                    lstItemDM.TryGetValue(tenDM, out dtItemDM);
                    //Thay doi gia tri NAME bang field so sanh gia tri
                    try
                    {
                        DataRow[] likeValue = dtItemDM.Select(NameDM +" like '%" + value + "%'");
                        newRow["LIKE_VALUE"] = likeValue[0][NameDM];
                    }
                    catch { }
                    lstValue.Add(value);
                    source.Rows.Add(newRow);
                }
              
            }
            return source;
        }
        //Tung ung voi tung gia tri , tao mot combobox chua cac gia tri gan chinh xac
        private void CreateValue(GridColumn column,string tenDM)
        {
            //RepositoryItemImageComboBox itemComboBox = new RepositoryItemImageComboBox();
            //DevExpress.XtraEditors.Controls.ImageComboBoxItem item = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
            //itemComboBox.Items.Add("");
            DataSet dsDM = DABase.getDatabase().LoadTable(tenDM);
            //DataRow[] row = dtValueInfo.Select("ORG_VALUE='" + orgValue + "'");
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            ////dt.Columns.Add(new DataColumn("ID"));
            //dt.Columns.Add(new DataColumn("NAME"));

            //DataRow newEmptyRow = dt.NewRow();
            //newEmptyRow["ID"] = "";
            //newEmptyRow["NAME"] = "[Thêm mới]";
            //dt.Rows.Add(newEmptyRow);
            RepositoryItemComboBox comboBox = new RepositoryItemComboBox();
            comboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            comboBox.Items.Add(itemCreateNew);
            foreach (DataRow dr in dsDM.Tables[0].Rows)
            {
                //DataRow newRow = dt.NewRow();
                //newRow["ID"] = dr["ID"];
                //newRow["NAME"] = dr["NAME"];
                //dt.Rows.Add(newRow);
                comboBox.Items.Add(dr[NameDM].ToString());
            }
            column.ColumnEdit = comboBox;
            column.FieldName = "LIKE_VALUE";
            //ds.Tables.Add(dt);

            //HUYNC:Thay the giá trị so sanh với giá trị cần lấy ở đây.
            //DataRow newRow = dsDM.Tables[0].NewRow();
            //newRow["ID"] = "";
            //newRow["NAME"] = "[Thêm mới]";
            //dsDM.Tables[0].Rows.InsertAt(newRow, 0);
         
            //return HelpGridColumn.CotCombobox(column, ds, "ID", "NAME", "LIKE_VALUE");
            //column.ColumnEdit = comboBox;
            //imageComboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            //column.ColumnEdit = imageComboBox;
        }

        private DataTable CreateTableOutput()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("FIELD"));
            dt.Columns.Add(new DataColumn("INDEX"));
            return dt;
        }
        private void CommitData()
        {
            try
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
            }
            catch { }
        }
        private void ThucHien()
        {
            CommitData();           
            dtOutput = CreateTableOutput();
            foreach (string key in lstDataSource.Keys)
            {
                DataTable source;
                lstDataSource.TryGetValue(key, out source);
                SetIdForDMSource(key, ref source);
                foreach (DataRow row in source.Rows)
                {
                    string orgvalue = row["ORG_VALUE"].ToString();
                    DataRow[] dr = dtValueInfo.Select("ORG_VALUE ='" + orgvalue + "'");
                    List<object> lstIndex = new List<object>();
                    foreach (DataRow r in dr)
                    {
                        if (!lstIndex.Contains(r["INDEX"]))
                            lstIndex.Add(r["INDEX"]);
                    }
                    foreach (object o in lstIndex)
                    {
                        DataRow newRow = dtOutput.NewRow();
                        if (row["LIKE_VALUE"].ToString().Equals("") || row["LIKE_VALUE"].ToString().Equals(itemCreateNew))
                        {
                            DataSet ds = DABase.getDatabase().LoadTable(dr[0]["TEN_DM"].ToString());
                            DataRow updateRow = ds.Tables[0].NewRow();
                            long id = DABase.getDatabase().GetID("G_DANH_MUC");
                            updateRow[IdDM] = id;
                            updateRow[NameDM] = dr[0]["ORG_VALUE"];
                            ds.Tables[0].Rows.Add(updateRow);
                            if (DABase.getDatabase().UpdateTable(ds) != -1)
                                newRow["ID"] = id;
                            else
                                newRow["ID"] = "";
                        }
                        else if (row["ID"].ToString().Equals("-1"))
                        {
                            DataSet ds = DABase.getDatabase().LoadTable(dr[0]["TEN_DM"].ToString());
                            DataRow updateRow = ds.Tables[0].NewRow();
                            long id = DABase.getDatabase().GetID("G_DANH_MUC");
                            updateRow["ID"] = id;
                            updateRow["NAME"] = row["LIKE_VALUE"];
                            ds.Tables[0].Rows.Add(updateRow);
                            if (DABase.getDatabase().UpdateTable(ds) != -1)
                            {
                                newRow["ID"] = id;
                            }
                            else
                                newRow["ID"] = "-1";
                        }
                        else
                            newRow["ID"] = row["ID"];
                        newRow["FIELD"] = dr[0]["COLUMNFIELD"];
                        newRow["INDEX"] = o;
                        dtOutput.Rows.Add(newRow);
                    }
                }
            }
            this.Close();
        }
        private void barBtnOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PLMessageBox.ShowConfirmMessage(MessageNewRow()) == DialogResult.Yes)
            {
                WaitingMsg.LongProcess(ThucHien);
            }
        }
        private string MessageNewRow()
        {
            CommitData();
            string msg = "";
            foreach (string key in lstDataSource.Keys)
            {
                DataTable dt;
                lstDataSource.TryGetValue(key, out dt);
                int newline = 0;
                DataSet ds = DABase.getDatabase().LoadTable(key);
                foreach (DataRow row in dt.Rows)
                    if (row["LIKE_VALUE"].ToString().Equals("")|| row["LIKE_VALUE"].ToString().Equals(itemCreateNew)
                        ||!isExist(row["LIKE_VALUE"].ToString(),ds.Tables[0]))
                        newline++;
                if(newline >0)
                    msg += key + " số dòng dữ liệu thêm mới " + newline +"\n";
                
            }
            msg += "Bạn có chắc muốn thực hiện không?";
            return msg;
        }
        private bool isExist(string name,DataTable dtDM)
        {
            DataRow[] dr = dtDM.Select(NameDM + "='"+ name +"'");
            if (dr.Length < 1)
                return false;
            return true;
        }
        private void barBtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtOutput = null;
            this.Close();
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.DeleteSelectedRows();
            DataTable dt = ((DataView)gridView1.DataSource).Table;
            dt.AcceptChanges();
        }
    }
}