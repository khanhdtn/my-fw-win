using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;
using System.Reflection;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win
{
    public class ObjectManagerHelp
    {
        public static List<ObjectInfo> _initAll()
        {
            List<ObjectInfo> list = new List<ObjectInfo>();            
            DataSet ds = DABase.getDatabase().LoadTable("FW_OBJ");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ObjectInfo obj = new ObjectInfo();
                obj.Id = (long)dr["ID"];
                obj.Title = dr["CAPTION"].ToString();
                obj.Key_field = dr["KEY_FIELD"].ToString();
                obj.TableName = dr["TABLE_NAME"].ToString();
                obj.ClassName = dr["CLASS_NAME"].ToString();                
                obj.DsPhieu = obj.getAllPhieu();                
                list.Add(obj);
            }
            return list;
        }

        public static DataSet find_DSObject(QueryBuilder filter)
        {            
            return DABase.getDatabase().LoadDataSet(filter);
        }

        public static QueryBuilder find_DSPhieu_Object(PhieuInfo phieu, string obj_field, string obj_key)
        {
            QueryBuilder filter = new QueryBuilder("select * from " + phieu.TableName + " where 1=1");
            filter.addCondition(obj_field + "='" + obj_key + "'");
            return filter;
        }

        public static bool UpdateDataRow(DataRow row, VGridControl vgrid)
        {
            try
            {
                foreach (BaseRow br in vgrid.Rows)
                    row[br.Properties.FieldName] = br.Properties.Value == null ? DBNull.Value : br.Properties.Value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void ChonDoiTuong(PLCombobox Input)
        {
            DataSet ds;      
            ds = DABase.getDatabase().LoadDataSet(
                "select id, caption from fw_obj where visible_bit='Y'");            
            Input._init(ds.Tables[0], "CAPTION", "ID");
        }

        public static void ChonPhieu(PLCombobox Input, long obj_id)
        {
            DataSet ds;
            ds = DABase.getDatabase().LoadDataSet("select fp.id, fp.caption from fw_phieu fp " +
                "inner join fw_obj_phieu fo on (fp.id=fo.phieu_id) " +
                "where obj_id='" + obj_id + "'");            
            Input._init(ds.Tables[0], "CAPTION", "ID");
        }        

        public static List<Field> BuildFieldsFromGrid(GridView grid)
        {
            List<Field> list = new List<Field>();
            foreach(GridColumn gc in grid.Columns)
            {
                if (gc.Visible)
                {
                    Field field = new Field();
                    field.CAPTION = gc.Caption;
                    field.NAME = gc.FieldName;
                    field.DATA_TYPE = gc.ColumnType.Name;
                    field.TAG = gc.Tag != null ? gc.Tag.ToString() : "";
                    list.Add(field);
                }
            }
            return list;
        }        
    }
}
