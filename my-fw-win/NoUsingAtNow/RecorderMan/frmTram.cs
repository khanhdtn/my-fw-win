using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;

namespace ProtocolVN.Framework.Win
{
    public partial class frmTram : XtraFormPL
    {
        private DXErrorProvider Error;

        public frmTram()
        {
            InitializeComponent();
            
            InitValidation();
            InitDataCtrl();
        }

        #region Các hàm xử lý khởi tạo
        /// <summary>
        /// Init the data Control.
        /// </summary>
        private void InitDataCtrl()
        {
            HelpXtraForm.SetCloseForm(this, this.btnThoat, true);
            RecorderManHelp.ChonTuyen(Tuyen, true);
            RecorderManHelp.ChonTram(Tram, true);
        }

        /// <summary>
        /// Init the validation.
        /// </summary>
        private void InitValidation()
        {
            this.Error = GUIValidation.GetErrorProvider(this);
            GUIValidation.SetMaxLength(new object[] { Noidung, 1000 });
        }
        #endregion

        #region Các hàm xử lý sự kiện Button
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            GUIValidation.TrimAllData(new object[] { Noidung });
            if (ValidateData())
            {
                RecorderManHelp.StopRecorder();

                long tuyen = Tuyen._getSelectedID();
                long tram = Tram._getSelectedID();
                string noi_dung = Noidung.Text;
                byte[] am_thanh = RecorderManHelp.m_RecBuffer;
                if (RecorderManHelp.Insert(tuyen, tram, noi_dung, am_thanh))
                    HelpXtraForm.CloseFormNoConfirm(this);
                else
                    RecorderManHelp.ErrorSave(this);
            }
        }      

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            RecorderManHelp.StartRecorder();
        }

        /// <summary>
        /// Handles the Click event of the btnStop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            RecorderManHelp.StopRecorder();
        }

        /// <summary>
        /// Handles the Click event of the btnPlay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            RecorderManHelp.PlayRecorder();
        }     
        #endregion

        /// <summary>
        /// Validates the data.
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            Error.ClearErrors();
            bool flag = true;
            flag = GUIValidation.ShowRequiredError(Error,
                new object[]{                    
                    Tuyen, "Tuyến",
                    Tram, "Trạm",
                }
            );            
            return flag;
        }           
    }
}