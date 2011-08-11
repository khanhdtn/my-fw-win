using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Plugin;
using ProtocolVN.Framework.Core;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public partial class PluginManagerOption : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dtPlugin;
        private List<IPlugin> plugin;
        private bool isAccept = false;
        private string zipfileName = "";
        private bool napSuccess = false;
        private string fileName = FrameworkParams.CONF_FOLDER + "/plugins.cpl";

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (isAccept)
            {
                PLMessageBoxExt.ShowNotificationMessage("Để nhận được kết quả sau khi cài bổ trợ.\nVui lòng khởi động lại chương trình.", true);
            }
        }
        public PluginManagerOption()
        {
            InitializeComponent();
            HelpGridColumn.CotCheckEdit(this.gridColumn3, "CHOICE");
            InitGrid();
        }
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
        private void InitGrid()
        {
            gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsView.ShowIndicator = false;
            //gridView1.OptionsView.ShowGroupPanel = false;
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
                    //Kiem tra dieu kien can de plugin hoat dong
                    if (!plugin[i].CheckOK())
                    {
                        //Cài đặt plugin
                        if (plugin[i].Install())
                        {
                            //Kích hoạt plugins
                            if (SaveFile(true))
                            {
                                Remove();
                                i--;
                            }
                        }
                        else
                        {
                            //Kích hoạt plugins ko quan tâm đến cài đặt
                            if (PLMessageBox.ShowConfirmMessage("Cài đặt Plugin không thành công. Bạn vẫn muốn kích hoạt Plugin?") == DialogResult.Yes)
                            {
                                if (SaveFile(true))
                                {
                                    Remove();
                                    i--;
                                }
                            }
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
            PLMessageBoxExt.ShowNotificationMessage("Vui lòng khởi động lại chương trình.", true);
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
            //PHUOCNC: Hiện tại không lưu được thông tin XML File có dấu tiếng việt
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
            this.gridView1.GroupPanelText = "Danh sách các bỗ trợ đang sử dụng";
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
            this.gridView1.GroupPanelText = "Danh sách các bỗ trợ sẵn sàng";
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
            fileDialog.Title = "Bổ trợ";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                zipfileName = fileDialog.FileName;
                WaitingMsg.LongProcess(UnZipPlugin);
                if (napSuccess)
                {
                    HelpMsgBox.ShowNotificationMessage("Nạp bổ trợ thành công.");
                    LoadPlugin();
                }
                else
                {
                    PLMessageBox.ShowErrorMessage("Nạp bổ trợ không thành công.");
                }
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

        private void GoBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Gọi UnInstall của Plugins

            //Unregister DLL của Plugins

            HelpMsgBox.ShowNotificationMessage("Đang nâng cấp.");
        }
    }
}
   