using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Win.frmFW.Implements.frmStikiesMain;

namespace ProtocolVN.Plugin.NoteBook
{
    class Config
    {

        public static dsConfig.dtNotesRow LoadNode(int id)
        {
            dsConfig ds = new dsConfig();
            //HUYNC:Đọc từ database lên.
            LoadTableNoteBook(ds);
            //ds.ReadXml(GlobalVars.ConfigFile);

            dsConfig.dtNotesRow dr = ds.dtNotes.FindByIdMsg(id);

            return dr;
        }

        public static int AddNode(dsConfig.dtNotesRow dr)
        {
            dsConfig ds = new dsConfig();
            //HUYNC:Đọc từ database lên.
            LoadTableNoteBook(ds);
            //ds.ReadXml(GlobalVars.ConfigFile);

            dsConfig.dtNotesRow dr_new = ds.dtNotes.NewdtNotesRow();
            foreach (DataColumn col in ds.dtNotes.Columns)
                if(!col.AutoIncrement) dr_new[col.ColumnName] = dr[col.ColumnName];

            ds.dtNotes.Rows.Add(dr_new);
            //HUYNC:Lưu xuống database
            SaveTableNoteBook(ds);
            //ds.WriteXml(GlobalVars.ConfigFile, System.Data.XmlWriteMode.WriteSchema);

            return dr_new.IdMsg;
        }

        public static void SaveNode(dsConfig.dtNotesRow dr)
        {
            dsConfig ds = new dsConfig();
            //HUYNC:Đọc từ database lên.
            LoadTableNoteBook(ds);
            //ds.ReadXml(GlobalVars.ConfigFile);

            dsConfig.dtNotesRow dr_orig = ds.dtNotes.FindByIdMsg(dr.IdMsg);

            if (dr_orig != null)
            {
                foreach ( DataColumn col in ds.dtNotes.Columns)
                    dr_orig[col.ColumnName] = dr[col.ColumnName];
                //HUYNC:Lưu xuông database
                SaveTableNoteBook(ds);
                //ds.WriteXml(GlobalVars.ConfigFile, System.Data.XmlWriteMode.WriteSchema);
            }
        }

        public static List<int> GetNoteIDs()
        {
            dsConfig ds = new dsConfig();
            //HUYNC:Đọc từ database lên.
            LoadTableNoteBook(ds);
            //ds.ReadXml(GlobalVars.ConfigFile);

            List<int> ret = new List<int>();

            foreach (dsConfig.dtNotesRow dr in ds.dtNotes.Rows)
                ret.Add(dr.IdMsg);

            return ret;
        }

        public static void Delete(int id)
        {
            dsConfig ds = new dsConfig();
            //HUYNC:Đọc từ database lên.
            LoadTableNoteBook(ds);
            //ds.ReadXml(GlobalVars.ConfigFile);

            dsConfig.dtNotesRow dr = ds.dtNotes.FindByIdMsg(id);

            if (dr != null)
            {
                dr.Delete();
                //HUYNC:Lưu xuống database
                SaveTableNoteBook(ds);
                //ds.WriteXml(GlobalVars.ConfigFile, System.Data.XmlWriteMode.WriteSchema);
            }
        }

        private static void LoadTableNoteBook(dsConfig config)
        {            
            DbCommand command = DABase.getDatabase().GetSQLStringCommand("SELECT * FROM FW_NOTE_BOOK WHERE USERID=" + FrameworkParams.currentUser.id);
            DataSet ds = DABase.getDatabase().LoadDataSet(command, "FW_NOTE_BOOK");
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                    config.Tables[0].ImportRow(dr);
            }
            else
            {
                throw new Exception("Thiếu bảng FW_NOTE_BOOK");
            }
        }

        private static void SaveTableNoteBook(dsConfig config)
        {
            config.Tables[0].TableName = "FW_NOTE_BOOK";
            DABase.getDatabase().UpdateTable(config.Tables[0].DataSet, "FW_NOTE_BOOK");
        }

    }
}
