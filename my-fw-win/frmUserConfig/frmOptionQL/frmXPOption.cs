using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Interface cho phép nhúng control vào màn hình tùy chọn
    /// </summary>
    public interface IConfigOption
    {
        /// <summary>
        /// Gọi thực hiện khi nhấn nút lưu
        /// </summary>
        bool SaveConfig();
        /// <summary>
        /// Gọi thực hiện sao khi UserControl được load lên
        /// </summary>
        /// <param name="input"></param>
        object runAfterShowControl(frmXPOption input);
    }

    public partial class frmXPOption : XtraForm
    {
        private static DataTable dtApp = null;
        public static void addOption(object nameOption, string className, string title)
        {
            if (dtApp == null)
            {
                dtApp = new DataTable("OPTION");
                dtApp.Columns.Add("OPTION_NAME");
                dtApp.Columns.Add("CLASS_NAME");
                dtApp.Columns.Add("TITLE");
            }
            dtApp.Rows.Add(nameOption, className, title);
        }
        private DataTable dt;
        private Control activeControl = null;
        private IConfigOption actionControl = null;

        public frmXPOption()
        {
            InitializeComponent();
            InitDataTable();
            listBoxControl1.DataSource = dt;
            listBoxControl1.ValueMember = "CLASS_NAME";
            listBoxControl1.DisplayMember = "OPTION_NAME";
            
            //AddOption("Hồ sơ công ty", typeof(CompanyInfoOption).FullName, "Hồ sơ công ty");
            AddOption("Hiển thị", typeof(DisplayOption).FullName, "Tùy chọn hiển thị");
            AddOption("Bổ trợ", typeof(PluginManagerOption).FullName, "Quản lý bổ trợ");
            
            if( dtApp != null)
            {
                for(int i = 0; i < dtApp.Rows.Count; i++){
                    this.AddOption(
                        dtApp.Rows[i]["OPTION_NAME"], 
                        dtApp.Rows[i]["CLASS_NAME"].ToString(),
                        dtApp.Rows[i]["TITLE"].ToString());                        
                }
            }
        }

        private void InitDataTable()
        {
            dt = new DataTable("OPTION");
            dt.Columns.Add("OPTION_NAME");
            dt.Columns.Add("CLASS_NAME");
            dt.Columns.Add("TITLE");
        }
        public bool AddOption(object nameOption, string className, string title)
        {
            dt.Rows.Add(nameOption, className,title);
            return true;
        }

        private void listBoxControl1_SelectedIndexChanged()
        {
            try
            {
                btnSave.Visible = true;
                btnDong.Visible = true;

                string value = listBoxControl1.SelectedValue.ToString();
                DataRow[] dr = dt.Select("CLASS_NAME='" + value + "'");
                //Chỉ thực hiện trên cùng 1 Assembly thôi
                //object instance = Activator.CreateInstance(Type.GetType(value));
                object instance = HelpObject.CreateInstance(value);                
                Control control = (Control)instance;
                if (instance is IConfigOption)
                {
                    actionControl = (IConfigOption)instance;
                    ((IConfigOption)instance).runAfterShowControl(this);
                }
                else
                {
                    btnSave.Visible = false;
                }

                if (activeControl != null && control != null)
                {
                    activeControl.Visible = false;
                    if (pcContent.Contains(control))
                    {
                        control.Visible = true;
                    }
                    else
                    {
                        pcContent.Controls.Add(control);
                    }
                    activeControl = control;
                    lblTitle.Text = dr[0]["TITLE"].ToString();
                }
                else
                {
                    pcContent.Controls.Add(control);
                    activeControl = control;
                }

                if (control != null)
                {
                    control.Dock = DockStyle.Fill;
                    control.Focus();                    
                }

            }
            catch (Exception ex){
                PLException.AddException(ex);
            }
        }
        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            WaitingMsg.LongProcess(listBoxControl1_SelectedIndexChanged);           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(actionControl.SaveConfig())
                this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}