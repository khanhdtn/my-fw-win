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
using System.IO;
using ProtocolVN.Framework.Win.Database;

namespace ProtocolVN.Framework.Win
{
    public partial class frmFWUserFormMan : XtraForm, IRequiredDBObject, IPublicForm
    {
        #region IRequiredDBObject Members

        public void Requirement()
        {
            DatabaseMan.RequireDBObject(FW_SHOW_FORM.INSTANCE);
        }

        #endregion


        public frmFWUserFormMan()
        {            
            InitializeComponent();
            Init_Grid();
            btn_Luu.Click += new EventHandler(btn_Luu_Click);            
        }        

        private void Init_Grid()
        {            
            HelpGridColumn.CotTextLeft(CotTenForm, "form");
            HelpGridColumn.CotTextLeft(CotMoTa, "description");
            HelpGridColumn.CotPLHienThi(CotHienThi, "show");           
        }

        private void LoadAll()
        {            
            if (File.Exists(UserFormHelp.xml))
            {
                try
                {
                    Load_XML();
                }
                catch
                {
                    File.Delete(UserFormHelp.xml);
                    UserFormHelp.Create_XML_NULL();
                    Load_XML();
                }               
            }
            else
            {                
                UserFormHelp.Create_XML_NULL();               
                Load_XML();
            }
        }        

        // Hàm load file xml vào DataSource của grid, đồng thời cũng refresh lại file xml
        private void Load_XML()
        {            
            // List để lưu các dirty DataRow  
            List<DataRow> list = new List<DataRow>();

            // Đọc từ file xml đến DataSet
            DataSet ds = new DataSet();
            ConfigFile.ReadXML(UserFormHelp.xml, ds);

            // Thay đổi DataSet
            DataTable dt = ds.Tables[0];
            DataColumn c_mota = new DataColumn("description");
            dt.Columns.Add(c_mota);
            foreach (DataRow dr in dt.Rows)
            {                
                // Kiểm tra form có tồn tại trong db hay không
                if (UserFormHelp.CheckFormExist(dr["form"].ToString()))
                    dr["description"] = UserFormHelp.GetDescription(dr["form"].ToString());
                else
                    // Nếu ko --> đưa vào danh sách các dirty DataRow
                    list.Add(dr);
            }

            // Loại bỏ các dirty DataRow từ DataTable
            foreach (DataRow dr in list)
                dt.Rows.Remove(dr);
            
            ds.Tables.Clear();
            ds.Tables.Add(dt);
            
            // Load DataSet sau khi đã sửa đổi vào DataSource của grid
            gridControlForm.DataSource = ds.Tables[0];

            // Xây dựng lại file xml từ DataTable mới 
            UserFormHelp.Create_XML_From_Source(dt);
        }
        
        // Hàm xây dựng lại file xml khi đã sửa đổi trên grid
        private void Update_XML()
        {
            DataTable dt = (DataTable)gridControlForm.DataSource;
            StringBuilder buider = new StringBuilder("<data>");
            foreach (DataRow dr in dt.Rows)                  
                buider.Append(UserFormHelp.DataRowToString(dr));
            
            buider.Append("</data>");
            File.Delete(UserFormHelp.xml);
            ConfigFile.WriteXML(UserFormHelp.xml, buider.ToString());
        }

        #region Xử lý các sự kiện trên form
        void btn_Luu_Click(object sender, EventArgs e)
        {
            Update_XML();
            this.Close();
        }

        private void Manage_Show_Form_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion        

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try{
                DataRow row = this.gridViewForm.GetFocusedDataRow();
                if(row != null){
                    Object obj = HelpObject.CreateInstance(row[0].ToString());
                    if (obj is XtraFormPL)
                    {
                        ProtocolForm.ShowModalDialog(this, (XtraFormPL)obj);
                    }
                    else
                    {
                        ProtocolForm.ShowModalDialog(this, (XtraForm)obj);
                    }
                }
            }
            catch { }
        }
    }
}