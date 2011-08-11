using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.IO;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Cho phép 1 form co thể hiển thị hoặc không hiện thị tùy vào người dùng.
    /// Su dung thong qua HelpXtraForm
    /// </summary>
    class UserForm
    {
        public UserForm(XtraForm mainform)
        {
            if (File.Exists(UserFormHelp.xml))
            {
                try
                {
                    _Refresh();
                    if (LoadCheckbox(mainform.GetType().FullName) == "")
                    {
                        if(UserFormHelp.AutoInsertIfNonExists(mainform) == false)
                            return;
                        Update_XML();
                    }
                }
                catch
                {
                    File.Delete(UserFormHelp.xml);
                    if (UserFormHelp.AutoInsertIfNonExists(mainform) == false)
                        return;
                    UserFormHelp.Create_XML_NULL();
                }
            }
            else
            {
                if (UserFormHelp.AutoInsertIfNonExists(mainform) == false) 
                    return;
                UserFormHelp.Create_XML_NULL();
            }
        }

        // Refresh lại file xml bằng cách loại bỏ các dirty item
        private void _Refresh()
        {
            List<DataRow> list = new List<DataRow>();

            DataSet ds = new DataSet();
            ConfigFile.ReadXML(UserFormHelp.xml, ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
                if (!UserFormHelp.CheckFormExist(dr["form"].ToString()))
                    list.Add(dr);

            // Loại bỏ các dirty DataRow từ DataTable
            foreach (DataRow dr in list)
                ds.Tables[0].Rows.Remove(dr);

            // Xây dựng lại file xml từ DataTable mới
            UserFormHelp.Create_XML_From_Source(ds.Tables[0]);
        }

        // Hàm update lại file xml do nó đã cũ so với database
        private void Update_XML()
        {
            DataSet ds = new DataSet();
            ConfigFile.ReadXML(UserFormHelp.xml, ds);
            UserFormHelp.Create_XML_Update(ds);
        }

        // Hàm kiểm tra xem có được show form hay không
        public bool IsShow(XtraForm mainform, XtraForm form)
        {
            bool flag = false;
            //if (LoadCheckbox(mainform.GetType().FullName) == "Y")
            if (LoadCheckbox(form.GetType().FullName) == "Y")
            {
                AddCheckbox(mainform, form);
                flag = true;
            }
            return flag;
        }

        // Hàm add checkbox vào form
        private void AddCheckbox(XtraForm mainform, XtraForm form)
        {
            int w = form.Width;
            int h = form.Height;
            form.Size = new System.Drawing.Size(w, h + 30);
            form.SuspendLayout();

            // Khởi tạo checkbox for add
            DevExpress.XtraEditors.CheckEdit checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            checkEdit1.Location = new System.Drawing.Point(13, h - 30);//subheader
            checkEdit1.Name = "_" + form.GetType().FullName;
            checkEdit1.Properties.Caption = "Hiện màn hình này ở các lần sau.";
            checkEdit1.Size = new System.Drawing.Size(w, 19);
            checkEdit1.TabIndex = 4;
            checkEdit1.Checked = true;
            checkEdit1.Click += new System.EventHandler(Checkbox_Click);
            form.Controls.Add(checkEdit1);

            form.ResumeLayout(false);
            form.PerformLayout();
        }

        private void Checkbox_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit checkbox = (DevExpress.XtraEditors.CheckEdit)sender;
            string name = checkbox.Name.Substring(1);
            SaveCheckbox(name, !checkbox.Checked);
        }

        // Hàm kiểm tra trạng thái show của form
        // 3 trạng thái: 'Y'=hiển thị; 'N'=ko hiển thị; ''=form ko tồn tại trong file xml
        public static string LoadCheckbox(string form_name)
        {
            DataSet ds = new DataSet();
            try
            {
                ConfigFile.ReadXML(UserFormHelp.xml, ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["form"].Equals(form_name))
                    {
                        if (dr["show"].Equals("Y"))
                            return "Y";
                        else
                            return "N";
                    }
                }
            }
            catch
            {
                File.Delete(UserFormHelp.xml);
                UserFormHelp.Create_XML_NULL();
            }
            return "";
        }

        // Hàm lưu trạng thái show vào file xml
        public static void SaveCheckbox(string key, bool value)
        {
            DataSet ds = new DataSet();
            try
            {
                ConfigFile.ReadXML(UserFormHelp.xml, ds);
                StringBuilder buider = new StringBuilder("<data>");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["form"].Equals(key))
                    {
                        if (value == true)
                            dr["show"] = "Y";
                        else
                            dr["show"] = "N";
                    }
                    buider.Append(UserFormHelp.DataRowToString(dr));
                }
                buider.Append("</data>");
                File.Delete(UserFormHelp.xml);
                ConfigFile.WriteXML(UserFormHelp.xml, buider.ToString());
            }
            catch
            {
                File.Delete(UserFormHelp.xml);
                UserFormHelp.Create_XML_NULL();
            }
        }
    }

    public class UserFormHelp
    {
        #region Biến dùng chung cho toàn bộ ứng dụng
        public static string xml = RadParams.RUNTIME_PATH + "/conf/" + FrameworkParams.currentUser.username + "_msg.cpl";
        #endregion

        // Hàm kiểm tra form có tồn tại trong db hay không
        public static bool CheckFormExist(string form)
        {
            string sql = "select name_form from fw_show_form where name_form='" + form + "'";
            DatabaseFB db = DABase.getDatabase();
            DbCommand check = db.GetSQLStringCommand(sql);
            if (db.ExecuteScalar(check) == null)
                return false;
            return true;
        }

        // Hàm xây dựng lại file xml từ DataTable
        public static void Create_XML_From_Source(DataTable dt)
        {
            StringBuilder buider = new StringBuilder("<data>");
            foreach (DataRow dr in dt.Rows)
                buider.Append(DataRowToString(dr));

            buider.Append("</data>");
            File.Delete(xml);
            ConfigFile.WriteXML(xml, buider.ToString());
        }

        // Hàm tạo file xml ban đầu từ db
        public static void Create_XML_NULL()
        {
            DataSet ds = DABase.getDatabase().LoadDataSet("select * from FW_SHOW_FORM where VISIBLE_BIT='Y'");
            if (ds == null) return;
            StringBuilder buider = new StringBuilder("<data>");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                buider.Append("<item>" +
                                 "<form>" + dr["NAME_FORM"].ToString() + "</form>" +
                                 "<show>Y</show>" +
                              "</item>");
            }
            buider.Append("</data>");
            ConfigFile.WriteXML(xml, buider.ToString());
        }

        // Hàm tạo file xml đồng bộ với db
        public static void Create_XML_Update(DataSet _ds)
        {
            DataSet ds = DABase.getDatabase().LoadDataSet("select * from FW_SHOW_FORM where VISIBLE_BIT='Y'");
            StringBuilder buider = new StringBuilder("<data>");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bool flag = false;
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    if (dr["NAME_FORM"].Equals(_dr["form"]))
                    {
                        buider.Append(DataRowToString(_dr));
                        flag = true;
                    }
                }
                if (!flag)
                    buider.Append("<item>" +
                                 "<form>" + dr["NAME_FORM"].ToString() + "</form>" +
                                 "<show>Y</show>" +
                                      "</item>");
            }
            buider.Append("</data>");
            File.Delete(xml);
            ConfigFile.WriteXML(xml, buider.ToString());
        }

        // Hàm lấy description của form thông qua name
        public static string GetDescription(string form)
        {
            string sql = "select description from FW_SHOW_FORM where name_form='" + form + "'";
            DatabaseFB db = DABase.getDatabase();
            DbCommand select = db.GetSQLStringCommand(sql);
            return db.ExecuteScalar(select).ToString();
        }

        public static string DataRowToString(DataRow dr)
        {
            return "<item><form>" + dr["form"] + "</form><show>" + dr["show"] + "</show></item>";
        }

        // Hàm tự động insert nếu không tồn tại record form trong db 
        public static bool AutoInsertIfNonExists(XtraForm form)
        {
            try
            {
                string sql = "select name_form from fw_show_form where name_form='" + form.GetType().FullName + "'";
                DatabaseFB db = DABase.getDatabase();
                DbCommand check = db.GetSQLStringCommand(sql);
                if (db.ExecuteScalar(check) == null)
                {
                    db = DABase.getDatabase();
                    long form_id = db.GetID(HelpGen.G_FW_ID);
                    sql = "insert into fw_show_form values('" + form_id + "','" + form.GetType().FullName + "','" + form.Text + "','Y')";
                    DbCommand insert = db.GetSQLStringCommand(sql);
                    db.ExecuteNonQuery(insert);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    class FW_SHOW_FORM : TableName
    {
        TableName table;

        public static FW_SHOW_FORM INSTANCE = new FW_SHOW_FORM();
        private FW_SHOW_FORM()
            : base()
        {
            table = new TableName("FW_SHOW_FORM", @"
                        /******************************************************************************/
                        /****                                Tables                                ****/
                        /******************************************************************************/



                        CREATE TABLE FW_SHOW_FORM (
                            ID           A_BIG_ID NOT NULL,
                            NAME_FORM    A_STR_MEDIUM,
                            DESCRIPTION  A_STR_VERY_LONG,
                            VISIBLE_BIT  A_BOOL
                        );



                        /******************************************************************************/
                        /****                          Unique Constraints                          ****/
                        /******************************************************************************/

                        ALTER TABLE FW_SHOW_FORM ADD CONSTRAINT UNQ1_FW_SHOW_FORM UNIQUE (NAME_FORM);


                        /******************************************************************************/
                        /****                             Primary Keys                             ****/
                        /******************************************************************************/

                        ALTER TABLE FW_SHOW_FORM ADD CONSTRAINT PK_FW_SHOW_FORM PRIMARY KEY (ID);

                ");
        }
    }
}
