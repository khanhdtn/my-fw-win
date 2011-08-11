using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class DADynamicField
    {
        /// <summary>
        /// Cập nhật dữ liệu của 1 fields
        /// </summary>
        /// <param name="field"></param>
        /// <param name="phieu_id"></param>
        /// <returns></returns>
        public static bool UpdateContent(FieldData field, long phieu_id)
        {
            DataSet ds = DABase.getDatabase().LoadTable("FW_TABLE_CONTENT_EXT",
                new string[] { "FIELD_ID", "TABLE_KEY", "CONTENT" },
                "TABLE_KEY='" + phieu_id + "' and FIELD_ID='" + field.FIELD_ID + "'");
            DatabaseFB db = DABase.getDatabase();
            if (ds.Tables[0].Rows.Count == 1)
            {
                string sql = "update FW_TABLE_CONTENT_EXT set CONTENT='" + field.CONTENT +
                "' where TABLE_KEY='" + phieu_id + "' and FIELD_ID='" + field.FIELD_ID + "'";
                DbCommand update = db.GetSQLStringCommand(sql);
                db.ExecuteNonQuery(update);
            }
            else
            {
                string sql = "insert into FW_TABLE_CONTENT_EXT values('" + field.FIELD_ID +
                    "','" + phieu_id + "','" + field.CONTENT + "')";
                DbCommand insert = db.GetSQLStringCommand(sql);
                db.ExecuteNonQuery(insert);
            }
            return true;
        }


        /// <summary>
        /// Load dữ liệu của 1 mẫu tin từ table nào 
        /// ví dụ
        /// PBH có số mẫu tinh mở rộng là A, B, C -> Table PBH
        /// thì table dependency làm Table Phieu Ban Hang
        /// </summary>
        /// <param name="tablename_dependency"></param>
        /// <param name="phieu_id"></param>
        /// <returns></returns>
        public static List<FieldData> Load(string tablename_dependency, long phieu_id)
        {
            List<FieldData> fields_arr = new List<FieldData>();
            QueryBuilder filter = new QueryBuilder
            (
               @"select tc.content, tf.caption, tf.field_id, tf.data_type" +
                    " from fw_table_content_ext tc inner join fw_table_field_ext tf" +
                    " on (tc.field_id=tf.field_id) where tc.table_key='" + phieu_id + "' and 1=1"               
            );
            DataSet ds = DABase.getDatabase().LoadReadOnlyDataSet(filter);
            DataTable dt = ds.Tables[0];              
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FieldData field = new FieldData();
                field.FIELD_ID = (long)dt.Rows[i]["FIELD_ID"];
                field.CAPTION = dt.Rows[i]["CAPTION"].ToString();
                field.DATA_TYPE = (long)dt.Rows[i]["DATA_TYPE"];
                field.CONTENT = dt.Rows[i]["CONTENT"].ToString();
                fields_arr.Add(field);
            }

            filter = new QueryBuilder
            (
               @"select tf.caption, tf.field_id, tf.data_type" +
                    " from fw_table_field_ext tf inner join fw_table_object fo" +
                    " on (tf.table_id=fo.id) where fo.name='" + tablename_dependency + 
                    "' and tf.visible_bit='Y' and tf.field_id not in " + 
                    "(select field_id from fw_table_content_ext where table_key='" + phieu_id + "') and 1=1"
            );
            ds = DABase.getDatabase().LoadReadOnlyDataSet(filter);
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FieldData field = new FieldData();
                field.FIELD_ID = (long)dt.Rows[i]["FIELD_ID"];
                field.CAPTION = dt.Rows[i]["CAPTION"].ToString();
                field.DATA_TYPE = (long)dt.Rows[i]["DATA_TYPE"];                
                fields_arr.Add(field);
            } 
            
            return fields_arr;
        }
       
        /// <summary>
        /// Xóa tất cả dữ liệu liên quan đến mẫu tin gốc.
        /// </summary>
        /// <param name="phieu_id"></param>
        /// <returns></returns>
        public static bool Delete(long phieu_id)
        {              
            return DatabaseFB.DeleteRecord("FW_TABLE_CONTENT_EXT", "TABLE_KEY", phieu_id);            
        }

        /// <summary>
        /// Lấy danh sách các field mở rộng của một table. Mỗi field có các thông tin
        /// tiêu đề, Id của field, và kiểu dữ liệu của Field.
        /// </summary>
        /// <param name="vgrid"></param>
        /// <param name="tablename"></param>
        public static void LoadFields(DevExpress.XtraVerticalGrid.VGridControl vgrid, string tablename)
        {
            QueryBuilder filter = new QueryBuilder
            (
               @"select tf.caption, tf.field_id, tf.data_type" +
                    " from fw_table_field_ext tf inner join fw_table_object fo" +
                    " on (tf.table_id=fo.id) where fo.name='" + tablename + "' and tf.visible_bit='Y' and 1=1"
            );
            DataSet ds = DABase.getDatabase().LoadReadOnlyDataSet(filter);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    DevExpress.XtraVerticalGrid.Rows.EditorRow row = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                    row.Name = "_" + dr["FIELD_ID"];
                    row.Properties.Caption = dr["CAPTION"].ToString();
                    if (int.Parse(dr["DATA_TYPE"].ToString()) == 1)
                        HelpEditorRow.DongTextLeft(row, null);
                    else if (int.Parse(dr["DATA_TYPE"].ToString()) == 2)
                        HelpEditorRow.DongSpinEdit(row, null, 0);
                    else if (int.Parse(dr["DATA_TYPE"].ToString()) == 3)
                        HelpEditorRow.DongCalcEdit(row, null, 3);
                    else if (int.Parse(dr["DATA_TYPE"].ToString()) == 4)
                        HelpEditorRow.DongCheckEdit(row, null);
                    else if (int.Parse(dr["DATA_TYPE"].ToString()) == 5)
                        HelpEditorRow.DongDateEdit(row, null);
                    vgrid.Rows.Add(row);
                }
            }
        }

        public static bool CheckFieldIsExist(long field_id)
        {
            string sql = "select count(*) from fw_table_field_ext where field_id='" + 
                field_id + "' and visible_bit<>'N'";
            DatabaseFB db = DABase.getDatabase();
            DbCommand check = db.GetSQLStringCommand(sql);
            int count = int.Parse(db.ExecuteScalar(check).ToString());
            if (count > 0)
                return true;
            return false;
        }
    }
}
