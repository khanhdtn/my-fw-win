using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using ProtocolVN.Plugin;
namespace ProtocolVN.Framework.Win
{
    //Màn hình quản lý các Plugin đã được thay thế bằng dạng nhúng vào trong frmXPOption
    [Obsolete("Không sử dụng")]
    public partial class frmPluginManager : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dtPlugin;
        private List<IPlugin> plugin;
        private bool isAccept = false;
        private string zipfileName = "";
        private bool napSuccess = false;

        public frmPluginManager()
        {
            InitializeComponent();
            HelpGridColumn.CotCheckEdit(this.gridColumn3, "CHOICE");
            this.FormClosed += delegate(object sender, FormClosedEventArgs e)
            {
                if (isAccept)
                    HelpMsgBox.ShowNotificationMessage("Để nhận được tác động. Xin vui lòng đăng xuất rồi đăng nhập trở lại.");
            };
        }
        private string fileName = FrameworkParams.CONF_FOLDER + "/plugins.cpl";

        private void InitDataSource()
        {
            dtPlugin = new DataTable("PLUGIN");
            DataColumn dcChoice = new DataColumn("CHOICE");
            dtPlugin.Columns.Add(dcChoice);
            DataColumn dcName = new DataColumn("NAME");
            dtPlugin.Columns.Add(dcName);
            DataColumn dcDes = new DataColumn("DESCRIPTION");
            dtPlugin.Columns.Add(dcDes);
            gridControl1.DataSource = dtPlugin;
        }

        private void frmPluginManager_Load(object sender, EventArgs e)
        {
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsMenu.EnableColumnMenu = false;
            gridView1.OptionsMenu.EnableFooterMenu = false;
            gridColumn3.Width = 35;
            gridColumn3.OptionsColumn.AllowSize = false;
            InitDataSource();
            barGoBo.Enabled = false;
            LoadPlugin();
            CreateFile();
        }

        private void LoadPlugin()
        {
            plugin = LoadPluginNotInstall();
            if (plugin != null)
                Register(plugin);
        }

        void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn.FieldName == "CHOICE")
                gridView1.OptionsBehavior.Editable = true;
            else
                gridView1.OptionsBehavior.Editable = false;
        }

        public bool Register(List<IPlugin> lstPlugin)
        {
            for (int i = 0; i < lstPlugin.Count; i++)
            {
                DataRow dr = dtPlugin.NewRow();
                dr["CHOICE"] = "N";
                dr["NAME"] = lstPlugin[i].Name;
                dr["DESCRIPTION"] = lstPlugin[i].Description;
                dtPlugin.Rows.Add(dr);
            }
            return true;
        }

        private void barDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void CreateFile()
        {
            if (!File.Exists(fileName))
                File.Create(fileName);
        }
        private void barChon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.MoveNext();
            try
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
            }
            DataTable dt = ((DataView)gridView1.DataSource).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["CHOICE"].ToString() == "Y")
                {
                    if (!plugin[i].CheckOK())
                    {
                        if (plugin[i].Install())
                            if (SaveFile(true))
                            {
                                Remove();
                                i--;
                            }
                    }
                    else
                    {
                        if (SaveFile(true))
                        {
                            Remove();
                            i--;
                        }
                    }
                }
            }
            isAccept = true;
        }

        private void Remove()
        {
            DataTable dt = ((DataView)gridView1.DataSource).Table;
            DataRow[] dr = dt.Select("CHOICE = 'Y'");
            foreach (DataRow r in dr)
                dt.Rows.Remove(r);
        }
        private bool SaveFile(bool install)
        {
            //HUYNC:
            StringBuilder contentFile = new StringBuilder();
            try
            {
                if (install)
                {
                    contentFile.Append(ConfigFile.Load(fileName));
                    if (contentFile.ToString() == "")
                        contentFile.Append("<?xml version='1.0' encoding='utf-8' standalone='yes'?><Plugins>");
                    else
                        contentFile.Replace("</Plugins>", "");
                    DataTable dt = ((DataView)gridView1.DataSource).Table;
                    DataRow[] drSelect = dt.Select("CHOICE = 'Y'");
                    foreach (DataRow dr in drSelect)
                          contentFile.Append("<Plugin><Name>"+ dr["NAME"].ToString() + "</Name><Description>" + dr["DESCRIPTION"].ToString() + "</Description></Plugin>");
                    contentFile.Append("</Plugins>");
                }
                else
                {
                    contentFile.Append("<?xml version='1.0' encoding='utf-8' standalone='yes'?><Plugins>");
                    DataTable dt = ((DataView)gridView1.DataSource).Table;
                    DataRow[] drSelect = dt.Select("CHOICE='N'");
                    foreach (DataRow dr in drSelect)
                            contentFile.Append("<Plugin><Name>" + dr["NAME"].ToString() + "</Name><Description>" + dr["DESCRIPTION"].ToString() + "</Description></Plugin>");
                    contentFile.Append("</Plugins>");
                }
                return ConfigFile.WriteXML(fileName, contentFile.ToString());
            }
            catch { return false; }

        }

        private void barDSPluginInstall_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<IPlugin> lstPlugin = LoadPluginInstall();
            if(lstPlugin!=null)
                Register(lstPlugin);
        }

        private List<IPlugin> LoadPluginInstall()
        {
            dtPlugin.Rows.Clear();
            gridControl1.DataSource = dtPlugin;
            barChon.Enabled = false;
            barGoBo.Enabled = true;
            return HelpPlugin.LoadConfPlugins();
        }

       
        private List<IPlugin> LoadPluginNotInstall()
        {
            dtPlugin.Rows.Clear();
            gridControl1.DataSource = dtPlugin;
            barGoBo.Enabled = false;
            barChon.Enabled = true;
            return HelpPlugin.LoadNotConfPlugins();
        }
        private void barPluginNotInstall_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<IPlugin>lstPlugin = LoadPluginNotInstall();
            if(lstPlugin!=null)
                Register(lstPlugin);
        }

        private void barGoBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
            }
            catch(Exception ex)
            {
                PLException.AddException(ex);
            }
            if (SaveFile(false))
            {
                Remove();
                isAccept = true;
            }
        }

       
        private void barNapCauKien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Tập tin bổ trợ (*.zip)|*.zip";
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            fileDialog.Title = "Bổ trợ mới";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                zipfileName = fileDialog.FileName;
                WaitingMsg.LongProcess(UnZipPlugin);
                if (napSuccess)
                {
                    HelpMsgBox.ShowNotificationMessage("Nạp bổ trợ thành công");
                    LoadPlugin();
                }
                else
                    PLMessageBox.ShowErrorMessage("Nạp bổ trợ không thành công");
            }
        }

        private void UnZipPlugin()
        {
            FileInfo info = new FileInfo(zipfileName);
            string desPath = Application.StartupPath + @"\plugins\" + info.Name.Replace(info.Extension, "");
            if (!Directory.Exists(desPath))
                Directory.CreateDirectory(desPath);
            if (ZipFile.UnZip(desPath, zipfileName))
                napSuccess = true;
            else
                napSuccess = false;
        }
    }
}