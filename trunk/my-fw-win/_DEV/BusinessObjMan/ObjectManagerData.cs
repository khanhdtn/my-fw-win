using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraVerticalGrid;
using ProtocolVN.Framework.Core;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace ProtocolVN.Framework.Win
{
    public class Field
    {
        private long _ID;

        public long ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _NAME;

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        private string _CAPTION;

        public string CAPTION
        {
            get { return _CAPTION; }
            set { _CAPTION = value; }
        }
        private string _DATA_TYPE;

        public string DATA_TYPE
        {
            get { return _DATA_TYPE; }
            set { _DATA_TYPE = value; }
        }
        private string _TAG;

        public string TAG
        {
            get { return _TAG; }
            set { _TAG = value; }
        }
    }


    public abstract class ObjectBase
    {
        private long _Id;
        private string _Title;
        private string _Key_field;
        private string _TableName;
        private string _ClassName;        
        private GridView _View;

        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Key_field
        {
            get { return _Key_field; }
            set { _Key_field = value; }
        }

        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }

        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }        

        public GridView View
        {
            get { return _View; }
            set { _View = value; }
        }
    }


    public interface IObject
    {
        void CreateInfoGrid(GridView gridView);
        void CreateVGrid(VGridControl vgrid);
        FieldNameCheck[] GetRuleVGrid(object param);
    }

    public class ObjectInfo : ObjectBase
    {
        private List<PhieuInfo> _DsPhieu;
        private VGridControl _Vgrid;
        private InitInfoGridColumns InitInfoGridCol;
        public delegate void InitInfoGridColumns(GridView gridView);

        private InitVGridColumns InitVGridCol;
        public delegate void InitVGridColumns(VGridControl vgridControl);

        private GetRule Rule;
        public delegate FieldNameCheck[] GetRule(object param);

        public ObjectInfo() { }

        public void _init(bool OnlyVGrid)
        {
            object instance = GenerateClass.initObject(ClassName);

            if(instance != null && instance is IObject){
                IObject obj = (IObject)instance;
                if (OnlyVGrid)
                    this.InitInfoGridCol = null;
                else
                    this.InitInfoGridCol = obj.CreateInfoGrid;

                this.InitVGridCol = obj.CreateVGrid;
                this.Rule = obj.GetRuleVGrid;
                
                InitGrid();                
            }
            else
            {
                PLMessageBoxDev.ShowMessage("Cấu hình sai vì " + ClassName + " không tồn tại");
            }
        }             

        private void InitGrid()
        {            
            this.Vgrid.Rows.Clear();

            if (InitInfoGridCol != null)
            {
                InitInfoGridCol(this.View);
                LoadSchema();
            }
            if (InitVGridCol != null)
                InitVGridCol(this.Vgrid);            
        }

        private void LoadSchema()
        {            
            QueryBuilder query = new QueryBuilder(
                "select * from " + TableName + " where 1=1");
            query.addCondition("1=-1");
            View.GridControl.DataSource =
                DABase.getDatabase().LoadReadOnlyDataSet(query).Tables[0];           
        }

        public List<PhieuInfo> DsPhieu
        {
            get { return _DsPhieu; }
            set { _DsPhieu = value; }
        }

        public VGridControl Vgrid
        {
            get { return _Vgrid; }
            set { _Vgrid = value; }
        }

        public List<PhieuInfo> getAllPhieu()
        {
            List<PhieuInfo> list = new List<PhieuInfo>();
            QueryBuilder query = new QueryBuilder("select fp.form_class_name, fp.id, fp.caption, " +
                "fp.key_field, fp.obj_field, fp.ma_field_name, fp.ngay_tao_fn, " +
                "fp.ngay_cn_fn, fp.table_name, fp.class_name from fw_phieu fp " +
                "inner join fw_obj_phieu fo on (fp.id=fo.phieu_id) " +
                "where obj_id='" + Id + "'" + " and 1=1");
            DataSet ds = DABase.getDatabase().LoadDataSet(query);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PhieuInfo phieu = new PhieuInfo();
                phieu.Id = (long)dr["ID"];
                phieu.Title = dr["CAPTION"].ToString();
                phieu.Key_field = dr["KEY_FIELD"].ToString();
                phieu.Obj_field = dr["OBJ_FIELD"].ToString();
                phieu.Ma_field_name = dr["MA_FIELD_NAME"].ToString();
                phieu.Ngay_tao_fn = dr["NGAY_TAO_FN"].ToString();
                phieu.Ngay_cn_fn = dr["NGAY_CN_FN"].ToString();
                phieu.TableName = dr["TABLE_NAME"].ToString();
                phieu.ClassName = dr["CLASS_NAME"].ToString();
                phieu.Form_Class_Name = dr["FORM_CLASS_NAME"].ToString();
                list.Add(phieu);
            }
            return list;
        }        

        public bool delete(long IdKey)
        {
            return DatabaseFB.DeleteRecord(TableName, Key_field, IdKey);
        }       

        public bool saveOrUpdate()
        {
            bool flag = false;
            if (VGridValidation.ValidateRecord(Vgrid, Rule(null)))
            {
                DataSet MainDS = DatabaseFB.LoadDataSet(TableName, Key_field, getVGridID());
                if (MainDS.Tables[0].Rows.Count == 1)
                {
                    HelpDataSet.MergeTable(new string[] { Key_field }, MainDS.Tables[0], (DataTable)Vgrid.DataSource, true, true);
                    flag = DatabaseFB.Update2DataSet(HelpGen.G_FW_ID, MainDS, null, false);
                }
                else
                {
                    DataRow row = MainDS.Tables[0].NewRow();
                    flag = ObjectManagerHelp.UpdateDataRow(row, Vgrid);
                    MainDS.Tables[0].Rows.Add(row);
                    flag = DatabaseFB.Update2DataSet(HelpGen.G_FW_ID, MainDS, null, true);
                }
            }            
            return flag;
        }                             

        private long getVGridID()
        {
            try
            {
                object ID = ((DataTable)Vgrid.DataSource).Rows[0][0];
                if (ID != null && ID.ToString() != "")
                    return (long)ID;
            }
            catch
            {
                return -2;
            }
            return -2;
        }
    }


    public interface IPhieu
    {
        void CreateResultGrid(GridView gridView);
    }

    public class PhieuInfo : ObjectBase
    {
        private string _Obj_field;
        private string _Ma_field_name;
        private string _Ngay_tao_fn;
        private string _Ngay_cn_fn;
        private string _Form_Class_Name;        
        
        private InitResultGridColumns InitResultGridCol;
        public delegate void InitResultGridColumns(GridView gridView);

        private static List<string> key_fields = new List<string>();

        public PhieuInfo() { }

        public void _init()
        {            
            object instance = GenerateClass.initObject(ClassName);
            if (instance != null && instance is IPhieu)
            {
                IPhieu obj = (IPhieu)instance;
                this.InitResultGridCol = obj.CreateResultGrid;

                if (!key_fields.Contains(this.Key_field))
                {
                    this.View.DoubleClick += new EventHandler(View_DoubleClick);
                    key_fields.Add(this.Key_field);
                }

                InitGrid();                
            }
            else
            {
                PLMessageBoxDev.ShowMessage("Cấu hình sai vì " + ClassName + " không tồn tại");
            }
        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (this.View.Columns.Contains(this.View.Columns.ColumnByFieldName(this.Key_field)))
            {
                if (this.View.FocusedRowHandle >= 0)
                {
                    Int64 id = HelpNumber.DecimalToInt64(this.View.GetDataRow
                                        (this.View.FocusedRowHandle)[this.Key_field]);
                    ProtocolForm.ShowModalForm(FrameworkParams.MainForm, this.Form_Class_Name, id);
                }                
            }
        }

        private void InitGrid()
        {            
            View.GridControl.DataSource = null;
            View.Columns.Clear();     
            
            if (InitResultGridCol != null)
                InitResultGridCol(this.View);
        }

        public string Obj_field
        {
            get { return _Obj_field; }
            set { _Obj_field = value; }
        }

        public string Ma_field_name
        {
            get { return _Ma_field_name; }
            set { _Ma_field_name = value; }
        }

        public string Ngay_tao_fn
        {
            get { return _Ngay_tao_fn; }
            set { _Ngay_tao_fn = value; }
        }

        public string Ngay_cn_fn
        {
            get { return _Ngay_cn_fn; }
            set { _Ngay_cn_fn = value; }
        }

        public string Form_Class_Name
        {
            get { return _Form_Class_Name; }
            set { _Form_Class_Name = value; }
        }
    }
}
