using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public partial class frmTramQL : XtraFormPL
    {
        public frmTramQL()
        {
            InitializeComponent();

            gridViewDSRecord.OptionsView.NewItemRowPosition = 
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            InitColumn();
            InitDataCtrl();            
        }

        #region Các hàm xử lý khởi tạo
        /// <summary>
        /// Init the column.
        /// </summary>
        private void InitColumn()
        {
            XtraGridSupportExt.IDGridColumn(ColTram, "ID", "NAME", "DM_TRAM", "TRAM_ID");
            XtraGridSupportExt.TextLeftColumn(ColDiaChi, "ADDRESS");   
            XtraGridSupportExt.TextLeftColumn(ColNoiDung, "NOI_DUNG");            
            HelpGridColumn.CotRepository(ColPlay, "AM_THANH", play_item, HorzAlignment.Center);
            HelpGridColumn.CotRepository(ColStop, "", stop_item, HorzAlignment.Center);
        }

        /// <summary>
        /// Init the data control.
        /// </summary>
        private void InitDataCtrl()
        {
            RecorderManHelp.ChonTuyen(Tuyen, true);
        }
        #endregion

        /// <summary>
        /// Handles the SelectedIndexChanged event of the Tuyen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Tuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = RecorderManHelp.GetDSTram(Tuyen._getSelectedID());
            if (ds.Tables.Count > 0)
                gridDSTram.DataSource = ds.Tables[0];            
        }

        /// <summary>
        /// Handles the Click event of the play_item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void play_item_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridViewDSRecord.GetDataRow(gridViewDSRecord.FocusedRowHandle);
                byte[] amthanh_data = (byte[])dr["AM_THANH"];
                RecorderManHelp.m_Fifo.Write(amthanh_data, 0, amthanh_data.Length);

                RecorderManHelp.m_Player = new WaveOutPlayer(-1, RecorderManHelp.fmt, amthanh_data.Length, 
                    3, new BufferFillEventHandler(RecorderManHelp.Filler));
            }
            catch
            {
                RecorderManHelp.StopPlayer();
                PLMessageBoxDev.ShowMessage("Trạm thuộc tuyến này chưa có âm thanh.");
            }            
        }

        /// <summary>
        /// Handles the Click event of the stop_item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void stop_item_Click(object sender, EventArgs e)
        {
            RecorderManHelp.StopPlayer();
        }

        /// <summary>
        /// Handles the FormClosing event of the frmTramQL control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frmTramQL_FormClosing(object sender, FormClosingEventArgs e)
        {
            RecorderManHelp.StopPlayer();
        }        
    }
}