using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using System.IO;

namespace ProtocolVN.Framework.Win
{
    public class RecorderManHelp
    {
        #region Các biến quan trọng sử dụng trong chương trình  
        public static WaveOutPlayer m_Player;
        private static WaveInRecorder m_Recorder;        
        private static byte[] m_PlayBuffer;
        public static byte[] m_RecBuffer;

        public static FifoStream m_Fifo = new FifoStream();
        public static WaveFormat fmt = new WaveFormat(44100, 16, 2);
        private static int bufferSize = 512 * 512;        
        #endregion

        /// <summary>
        /// Lấy DataSource cho Tuyến.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <param name="IsAdd">The is add.</param>
        public static void ChonTuyen(PLCombobox Input, bool? IsAdd)
        {
            DataSet dt;
            if (IsAdd != null && IsAdd == true)
            {
                dt = DABase.getDatabase().LoadDataSet("select * from DM_TUYEN where VISIBLE_BIT='Y'", "DM_TUYEN");
            }
            else
            {
                dt = DABase.getDatabase().LoadTable("DM_TUYEN");
            }

            Input._init(dt.Tables[0], "NAME", "ID");          
        }

        /// <summary>
        /// Lấy DataSource cho Trạm.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <param name="IsAdd">The is add.</param>
        public static void ChonTram(PLCombobox Input, bool? IsAdd)
        {
            DataSet dt;
            if (IsAdd != null && IsAdd == true)
            {
                dt = DABase.getDatabase().LoadDataSet("select * from DM_TRAM where VISIBLE_BIT='Y'", "DM_TRAM");
            }
            else
            {
                dt = DABase.getDatabase().LoadTable("DM_TRAM");
            }

            Input._init(dt.Tables[0], "NAME", "ID");
        }

        /// <summary>
        /// Lấy DS trạm thuộc tuyến.
        /// </summary>
        /// <param name="tuyen_id">The tuyen_id.</param>
        /// <returns></returns>
        public static DataSet GetDSTram(long tuyen_id)
        {
            QueryBuilder filter = new QueryBuilder("select tram_id, address, noi_dung, " + 
                "am_thanh from tuyen_ct inner join dm_tram on (tram_id=id) where 1=1");
            filter.addID("tuyen_id", tuyen_id);
            return DABase.getDatabase().LoadReadOnlyDataSet(filter);
        }

        /// <summary>
        /// Errors the save.
        /// </summary>
        /// <param name="a">A.</param>
        public static void ErrorSave(object a)
        {
            string msg = "Lưu thông tin không thành công. Vui lòng kiểm tra lại số liệu.";
            List<Exception> listExs = PLException.GetLastestExceptions();

            if (listExs.Count >= 0)
            {
                string tmp = PLDebug.GetUserErrorMsg(PLException.GetLastestExceptions());
                if (tmp != "" && tmp.IndexOf("PROTOCOL") == 0) msg = tmp;
            }
            PLMessageBox.ShowNotificationMessage(msg);
        }

        /// <summary>
        /// Inserts the specified tuyen.
        /// </summary>
        /// <param name="tuyen">The tuyen.</param>
        /// <param name="tram">The tram.</param>
        /// <param name="noi_dung">The noi_dung.</param>
        /// <param name="am_thanh">The am_thanh.</param>
        /// <returns></returns>
        public static bool Insert(long tuyen, long tram, string noi_dung, byte[] am_thanh)
        {
            try
            {
                DatabaseFB db = DABase.getDatabase();
                DbCommand dbInsert = null;

                dbInsert = db.GetSQLStringCommand("insert into tuyen_ct values(@tuyen,@tram,@noidung,@amthanh)");
                db.AddInParameter(dbInsert, "@tuyen", DbType.Int64, tuyen);
                db.AddInParameter(dbInsert, "@tram", DbType.Int64, tram);
                db.AddInParameter(dbInsert, "@noidung", DbType.String, noi_dung);
                db.AddInParameter(dbInsert, "@amthanh", DbType.Binary, am_thanh);
                
                db.ExecuteNonQuery(dbInsert);
                return true;
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                return false;
            }
        }

        /// <summary>
        /// Fillers the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="size">The size.</param>
        public static void Filler(IntPtr data, int size)
        {
            if (m_PlayBuffer == null || m_PlayBuffer.Length < size)
                m_PlayBuffer = new byte[size];
            if (m_Fifo.Length >= size)
                m_Fifo.Read(m_PlayBuffer, 0, size);
            else
                for (int i = 0; i < m_PlayBuffer.Length; i++)
                    m_PlayBuffer[i] = 0;
            System.Runtime.InteropServices.Marshal.Copy(m_PlayBuffer, 0, data, size);
        }

        /// <summary>
        /// Datas the arrived.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="size">The size.</param>
        private static void DataArrived(IntPtr data, int size)
        {
            if (m_RecBuffer == null || m_RecBuffer.Length < size)
                m_RecBuffer = new byte[size];
            System.Runtime.InteropServices.Marshal.Copy(data, m_RecBuffer, 0, size);
            m_Fifo.Write(m_RecBuffer, 0, m_RecBuffer.Length);
        }

        /// <summary>
        /// Stops the recorder.
        /// </summary>
        public static void StopRecorder()
        {
            StopPlayer();
            if (m_Recorder != null)
                try
                {
                    m_Recorder.Dispose();
                }
                finally
                {
                    m_Recorder = null;
                }
            m_Fifo.Flush();
        }

        /// <summary>
        /// Starts the recorder.
        /// </summary>
        public static void StartRecorder()
        {
            StopRecorder();
            try
            {                
                m_Recorder = new WaveInRecorder(-1, fmt, bufferSize, 3, 
                    new BufferDoneEventHandler(DataArrived));
            }
            catch
            {
                StopRecorder();
                throw;
            }
        }

        /// <summary>
        /// Plays the recorder.
        /// </summary>
        public static void PlayRecorder()
        {
            StopRecorder();
            try
            {
                if (m_RecBuffer != null)
                {
                    m_Fifo.Write(m_RecBuffer, 0, m_RecBuffer.Length);
                    m_Player = new WaveOutPlayer(-1, fmt, m_RecBuffer.Length, 3,
                        new BufferFillEventHandler(Filler));
                }
            }
            catch 
            {
                StopRecorder();
                throw;
            }
        }

        /// <summary>
        /// Stops the player.
        /// </summary>
        public static void StopPlayer()
        {
            if (m_Player != null)
                try
                {
                    m_Player.Dispose();
                }
                finally
                {
                    m_Player = null;
                }
            m_Fifo.Flush();            
        }        
    }
}
