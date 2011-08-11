using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win.Database;
using System.IO;
using ICSharpCode.TextEditor.Document;
using DevExpress.XtraEditors;

//Xay dung chuc nang cho phep tu dong sinh ra SQL String.
//Giup cho viec dong goi DB
namespace ProtocolVN.Framework.Win
{
    public partial class frmGenerateSQL : XtraForm
    {
        #region Các biến quan trọng sử dụng trong chương trình
        List<DBObject> db_obj_mainlist;
        List<DBObject> db_obj_sublist;
        Stack<DBObject> db_obj_stack;
        Stack<DBObject> db_obj_stack_temp;
        #endregion

        public frmGenerateSQL()
        {
            InitializeComponent();
            RequireDB();

            db_obj_mainlist = DatabaseMan.DBObjectList;

            InitCtrl();
            InitEvent();            
            InitData();
        }

        #region Các hàm khởi tạo
        private void InitEvent()
        {            
            cbObjDb_main.SelectedIndexChanged += new EventHandler(cbObjDb_SelectedIndexChanged);            
        }        
    
        private void InitCtrl()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col =
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME");

            cbObjDb_main._lookUpEdit.Properties.Columns.Add(col);
            cbObjDb_main._lookUpEdit.Properties.DisplayMember = "NAME";
            cbObjDb_main._lookUpEdit.Properties.ValueMember = "NAME";

            cbObjDb_sub._lookUpEdit.Properties.Columns.Add(col);
            cbObjDb_sub._lookUpEdit.Properties.DisplayMember = "NAME";
            cbObjDb_sub._lookUpEdit.Properties.ValueMember = "NAME";

            memo_script.ForeColor = Color.Blue;            
        }

        private void InitData()
        {
            DataTable dbObj_dt = CreateDataTableFromListObj(db_obj_mainlist);            
            cbObjDb_main._lookUpEdit.Properties.DataSource = dbObj_dt;            
        }        
        #endregion

        #region Các hàm trợ giúp
        public void RequireDB()
        {
            //DBObject obj = OBJ_REL.INSTANCE;
            //obj = GET_PHIEU_GOC.INSTANCE;
            //obj = FW_NGHIEP_VU_SYS.INSTANCE;
            //obj = FW_ST_NGHIEP_VU.INSTANCE;
        }

        private DataTable CreateDataTableFromListObj(List<DBObject> list)
        {
            DataTable dt = new DataTable();
            DataColumn name_col = new DataColumn("NAME");
            dt.Columns.Add(name_col);

            try
            {
                foreach (DBObject obj in list)
                {
                    DataRow dr = dt.NewRow();
                    dr["NAME"] = obj.NAME;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }        

        private DBObject getDbObject(string name)
        {
            foreach (DBObject obj in db_obj_mainlist)
                if (obj.NAME.Equals(name))
                    return obj;
            return null;
        }

        private void Load_RelativeObj()
        {
            DataTable dbObj_dt = CreateDataTableFromListObj(db_obj_sublist);                
            cbObjDb_sub._lookUpEdit.Properties.DataSource = dbObj_dt; 
        }

        private void SetRequiredObjectRecursive(DBObject obj)
        {               
            foreach (DBObject _obj in obj.RequireObjectName)
            {
                if (!db_obj_stack.Contains(_obj))
                    db_obj_stack.Push(_obj);
                else
                {
                    DBObject obj_temp = db_obj_stack.Pop();
                    db_obj_stack_temp = new Stack<DBObject>();
                    DBObject obj1;
                    do{
                        obj1 = db_obj_stack.Pop();
                        db_obj_stack_temp.Push(obj1);
                    }while(obj1.NAME != _obj.NAME);
                    db_obj_stack.Push(obj_temp);
                    while (db_obj_stack_temp.Count > 0)
                        db_obj_stack.Push(db_obj_stack_temp.Pop());
                }
                SetRequiredObjectRecursive(_obj);
            }
        }

        String GetSQLScript(DBObject obj)
        {
            db_obj_sublist = new List<DBObject>();
            db_obj_stack = new Stack<DBObject>();

            db_obj_stack.Push(obj);

            SetRequiredObjectRecursive(obj);

            StringBuilder builder = new StringBuilder();
            while (db_obj_stack.Count > 0)
            {
                DBObject _obj = db_obj_stack.Pop();
                builder.Append(_obj.DDL.Trim() + " ");
                db_obj_sublist.Add(_obj);
            }            
            return builder.ToString().Trim();
        }
        #endregion

        #region Các sự kiện Button
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (memo_script.Text != "")
            {
                try
                {
                    Clipboard.SetText(memo_script.Text);
                    PLMessageBoxExt.ShowNotificationMessage(
                        "Nội dung script đã được copy đến clipboard", false);
                }
                catch {                  
                }
            }
            else
                PLMessageBoxExt.ShowNotificationMessage(
                    "Script không tồn tại", false);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog ofd = new SaveFileDialog())
            {
                ofd.Title = "Chọn tập tin SQL Script";
                ofd.Filter = "SQL Script File(*.sql)|*.sql";
                if (ofd.ShowDialog() == DialogResult.OK)
                {                    
                    StreamWriter s = File.CreateText(ofd.FileName);
                    s.Write(memo_script.Text.Trim());
                    s.Flush();
                }
            }
        } 
        #endregion

        #region Các sự kiện khác
        void cbObjDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbObjDb_main._lookUpEdit.EditValue != null)
            {                
                memo_script.Text = GetSQLScript(
                    getDbObject(cbObjDb_main._lookUpEdit.EditValue.ToString()));
                obj_lb.Text = cbObjDb_main._lookUpEdit.EditValue.ToString();

                Load_RelativeObj();
                btnCopy.Focus();
            }
        }        

        private void memo_script_Load(object sender, EventArgs e)
        {            
            String strPathXSHD = RadParams.RUNTIME_PATH + "/conf/res/";
            FileSyntaxModeProvider provider = new FileSyntaxModeProvider(strPathXSHD);
            HighlightingManager manager = HighlightingManager.Manager;
            manager.AddSyntaxModeFileProvider(provider);
            memo_script.Document.HighlightingStrategy =
                manager.FindHighlighter("SQL");            
        }
        #endregion        
    }
}
