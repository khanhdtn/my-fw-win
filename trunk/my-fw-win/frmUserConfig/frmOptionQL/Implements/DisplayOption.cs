using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Drawing.Printing;
using DevExpress.XtraEditors.Controls;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public partial class DisplayOption : DevExpress.XtraEditors.XtraUserControl,IConfigOption
    {
        private Option configOption;

        public DisplayOption()
        {
            InitializeComponent();
            //object obj = this.btnSave.Tag;
            //TagPropertyMan.InsertOrUpdate(ref obj, "SECURITY", new PermissionItem("ProtocolVN.Framework.Win.frmOption", PermissionType.EDIT));
            //this.btnSave.Tag = obj;
            loadPrinters();
            initData(); 
        }
       
        public bool SaveConfig()
        {
            try
            {
                getData();
                configOption.update();
                ApplyFormatAction.Culture = null;
                Application.CurrentCulture = ApplyFormatAction.GetCultureInfo();
                FrameworkParams.currentSkin.SelectSkin(HelpNumber.ParseInt32(configOption.Skin));
                //HelpMsgBox.ShowNotificationMessage("Lưu thông tin cấu hình thành công");

                UserForm.SaveCheckbox(suDungLanDau.Tag.ToString(), suDungLanDau.Checked);
                UserForm.SaveCheckbox(xacNhanKhiThoat.Tag.ToString(), xacNhanKhiThoat.Checked);

                return true;
            }
            catch
            {
                PLMessageBox.ShowErrorMessage("Lưu thông tin cấu hình thất bại");
                return false;
            }
        }

        private void loadPrinters()
        {
            //khong cho sua text
            cbInstalledPrinters.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            PopulateInstalledPrintersCombo();
            if (cbInstalledPrinters.Properties.Items.Count > -1)
            {
                // The combo box's Text property returns the selected item's text, which is the printer name.
                cbInstalledPrinters.SelectedIndex = 0;
            }
        }
        private void PopulateInstalledPrintersCombo()
        {
            // Add list of installed printers found to the combo box.
            // The pkInstalledPrinters string will be used to provide the display string.
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                cbInstalledPrinters.Properties.Items.Add(pkInstalledPrinters);
            }
        }
        public void initData()
        {
            
            if (FrameworkParams.option == null)
            {
                FrameworkParams.option = new Option();
                FrameworkParams.option.load();
            }
            configOption = FrameworkParams.option;

            seRound.Value = HelpNumber.ParseInt32(configOption.round);
            rgSperactorThousand.SelectedIndex = (configOption.thousandSeparator.Equals(".") ? 0 : 1);
            rgSperactorDec.SelectedIndex = (configOption.decSeparator.Equals(".") ? 1 : 0);
            cbFormatDay.EditValue = configOption.dateFormat;
            cbFormatHour.EditValue = configOption.timeFormat;
            if (FrameworkParams.currentSkin!=null)
                cbSkin.Properties.Items.AddRange(FrameworkParams.currentSkin.arrSkinName);
            cbSkin.SelectedIndex = HelpNumber.ParseInt32(configOption.Skin);

            for (int i = 0; i < cbInstalledPrinters.Properties.Items.Count; i++)
            {
                if (cbInstalledPrinters.Properties.Items[i].ToString() == configOption.printerName)
                    cbInstalledPrinters.SelectedItem = configOption.printerName;
                else
                {
                    cbInstalledPrinters.Text = "";
                }
            }

            this.suDungLanDau.Tag = typeof(frmFWRunFirst).FullName;            
            if (UserForm.LoadCheckbox(typeof(frmFWRunFirst).FullName) == "Y")
                suDungLanDau.Checked = true;
            else
                suDungLanDau.Checked = false;

            this.xacNhanKhiThoat.Tag = typeof(frmFWRunExit).FullName;
            if (UserForm.LoadCheckbox(typeof(frmFWRunExit).FullName) == "Y")
                xacNhanKhiThoat.Checked = true;
            else
                xacNhanKhiThoat.Checked = false;
        }

        public void getData()
        {
            configOption.numFormat = "";
            configOption.dateTimeFormat = "";
            configOption.round = "" + (HelpNumber.ParseInt32(seRound.Value.ToString()) > 5 ? 0 : HelpNumber.ParseInt32(seRound.Value.ToString()));
            configOption.thousandSeparator = rgSperactorThousand.Properties.Items[rgSperactorThousand.SelectedIndex].Description;
            configOption.decSeparator = rgSperactorDec.Properties.Items[rgSperactorDec.SelectedIndex].Description;
            configOption.dateFormat = cbFormatDay.EditValue.ToString();
            configOption.timeFormat = cbFormatHour.EditValue.ToString();
            configOption.Skin = cbSkin.SelectedIndex.ToString();
            configOption.printerName = cbInstalledPrinters.Text;
        }

        private void rgSperactorThousand_SelectedIndexChanged(object sender, EventArgs e)
        {
            rgSperactorDec.SelectedIndex = rgSperactorThousand.SelectedIndex;
        }

        private void rgSperactorDec_SelectedIndexChanged(object sender, EventArgs e)
        {
            rgSperactorThousand.SelectedIndex = rgSperactorDec.SelectedIndex;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(FrameworkParams.TEMP_FOLDER, true);

                try { HelpFile.NeedFolder("temp"); } catch { }
                
                HelpMsgBox.ShowNotificationMessage("Đã xóa các tập tin tạm.");
            }
            catch (Exception ex)
            {
                HelpMsgBox.ShowNotificationMessage("Xóa các tập tin tạm không thành công.");
                PLException.AddException(ex);
            }
        }

        private void btnCauHinhFTP_Click(object sender, EventArgs e)
        {
            ProtocolForm.ShowModalDialog((XtraForm)this.FindForm(), new frmConfigFTP());
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(FrameworkParams.LAYOUT_FOLDER, true);

                try { HelpFile.NeedFolder("layout"); } catch { }

                HelpMsgBox.ShowNotificationMessage("Đã phục hồi về giao diện gốc.");
            }
            catch (Exception ex)
            {
                HelpMsgBox.ShowNotificationMessage("Phục hồi về giao diện gốc không thành công.");
                PLException.AddException(ex);                
            }
        }

        private void btnAnHienManHinh_Click(object sender, EventArgs e)
        {
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, new frmFWUserFormMan());
        }

        #region IConfigOption Members


        public object runAfterShowControl(frmXPOption input)
        {
            return null;
        }

        #endregion
    }
}
