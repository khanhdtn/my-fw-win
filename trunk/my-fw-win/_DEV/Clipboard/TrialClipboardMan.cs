using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class ClipboardMan
    {
        public Dictionary<string, ClipboardItem> clipboard;
        public static ClipboardMan Instance = new ClipboardMan();
        
        private ClipboardMan()
        {
            this.clipboard = new Dictionary<string, ClipboardItem>();
            
        }

        public void InitClipboardMan(ClipboardItem[] Item)
        {            
            for (int i = 0; i < Item.Length; i++)
            {
                if(!this.clipboard.ContainsKey(Item[i].EntityName))
                    this.clipboard.Add(Item[i].EntityName, Item[i]);
            }
            
        }

        #region 1.Hàm copy dữ liệu vào ClipboardMan
        public bool Copy(string entity, string []keys,DataSet src, bool Append)
        {
            String er = string.Empty;
            try
            {
                if(keys == null) //Neu null tuc la su dung keys da dinh nghia
                    HelpDataSet.MergeDataSet(clipboard[entity].Keys, this.clipboard[entity].Data, src, Append);
                else //nguoc lai merge theo keys moi
                    HelpDataSet.MergeDataSet(keys, this.clipboard[entity].Data, src, Append);
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                throw ex;
            }
            if (er == string.Empty)
                return true;
            return false;
        }
        #endregion

        #region 2.Hàm Paste dữ liệu từ Clipboard xuống Dataset
        public bool Paste(string entity, string []keys,DataSet dest, bool Append, bool removeData)
        {
            String er = string.Empty;
            try
            {
                //Nếu không có dữ liệu trên Clipboard có entity này thì thoát
                if (this.clipboard[entity].Data.Tables[0].Rows.Count == 0)
                {
                    return true;
                }
                if(keys==null)
                    HelpDataSet.MergeDataSet(this.clipboard[entity].Keys, dest, this.clipboard[entity].Data, Append);
                else
                    HelpDataSet.MergeDataSet(keys, dest, this.clipboard[entity].Data, Append);

                //Thông báo xóa sau khi paste
                if (removeData)
                {
                    if (PLMessageBox.ShowConfirmMessage("Bạn có muốn xóa dữ liệu trên clipboard\nsau khi dán không ?") == DialogResult.Yes)
                    {
                        this.clipboard[entity].Data.Tables[0].Rows.Clear();
                    }
                }
                
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                throw ex;
            }
            if (er == string.Empty)
                return true;
            return false;
        }
        #endregion

        #region 3.Clear entity
        public bool Clear(string entity)
        {
            return this.clipboard.Remove(entity);
        }
        #endregion

        #region 4.Xóa các dòng trên Clipboard tương ứng với entity
        public bool ClearRows(string entity, int[] rowsSelected)
        {
            string err = string.Empty;
            try
            {
               
                for (int i = rowsSelected.Length - 1; i >= 0; i--)
                {
                    this.clipboard[entity].Data.Tables[0].Rows.RemoveAt(rowsSelected[i]);
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                throw ex;

            }
            if (err == string.Empty)
                return true;
            return false;
        }
        #endregion

        #region 5.Hàm lấy danh sách các entity
        public string[] GetEntitys()
        {
            string[] rs = new string[clipboard.Count];
            clipboard.Keys.CopyTo(rs, 0);
            return rs;
        }
        public string[] GetTitiles()
        {
            string []entitys = GetEntitys();
            string []rs = new string[clipboard.Count];
            for (int i = 0; i < clipboard.Count; i++)
            {
                rs[i] = clipboard[entitys[i]].Title;
            }
            return rs;
        }
        #endregion

        #region 6.Kiểm tra trong danh sách còn tồn tại entity không
        public bool CheckEntity(string entity)
        {
            foreach (string var in this.clipboard.Keys)
            {
                if (entity == var)
                    return true;
            }
            return false;
        }
        #endregion

        #region 11.Hàm giải phóng data cho ClipboardItem
        public bool DisData(string entity)
        {
            string er = string.Empty;
            try
            {
                if (PLMessageBox.ShowConfirmMessage("Bạn có chắc chắn muốn xóa dữ liệu của đối tượng " + this.clipboard[entity].Title + " không ?") == DialogResult.Yes)
                {
                    this.clipboard[entity].Data.Tables[0].Rows.Clear();
                }

            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
                throw ex;
            }
            if (er == string.Empty)
                return true;
            return false;
        }
        #endregion

        #region 12.Lay ten hinh trong clipboardItem
        public string GetImange(string entity)
        {
            return this.clipboard[entity].ImageName;
        }
        #endregion

        #region 13.Lấy cấu trúc dataset của ClipboardItem
        public DataSet GetDataSetContructor(string entity)
        {
            DataSet Kq = new DataSet();
            DataTable Table = new DataTable();
            for (int i = 0; i < this.clipboard[entity].Data.Tables[0].Columns.Count; i++)
            {
                DataColumn col = new DataColumn(
                    this.clipboard[entity].Data.Tables[0].Columns[i].ColumnName,
                    this.clipboard[entity].Data.Tables[0].Columns[i].DataType
                    );
                Table.Columns.Add(col);
            }
            Kq.Tables.Add(Table);
            return Kq;
        }
        #endregion

        #region 14.Lấy Title của ClipboardItem
        public string GetTitle(string entity)
        {
            return this.clipboard[entity].Title;
        }
                
        #endregion

        #region 15. Kiểm tra một ClipboardItem có dữ liệu chưa
        public bool CheckDataSet(string entity)
        {
            if (ClipboardMan.Instance.clipboard[entity].Data.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }
         
        #endregion
    }
    public class ClipboardItem
    {
        public string Title; //Ten hien thi cua doi tuong
        public string EntityName;   //Tên của đối tượng
        public string ImageName; //Ten hinh cho doi tuong
        public DataSet Data;        //Dữ liệu
        public string[] Keys;       //Tên field cần để Merge
        public string[] Captions;   //Các caption tương ứng trong lưới

        public ClipboardItem(string title,string entityname,string imagename, DataSet data, string[] keys, string[] captions)
        {
            this.Title = title;
            this.EntityName = entityname;
            this.Data = data;
            this.Keys = keys;
            this.Captions = captions;
            this.ImageName = imagename;
        }
    }

    public class IClipboardDataSet
    {
        //Tao cau truc dataset tu mot cau truy van
        public static DataSet IDataSet(string sql,string TableName)
        {
            QueryBuilder query = new QueryBuilder(sql);
            DataSet ds = DABase.getDatabase().LoadDataSet(query, TableName);
            return ds;

        }
        //Tao cau truc dataset
        public static DataSet IDataSet(string []Fields,string []StringType,string TableName)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(TableName);
            for (int i = 0; i < Fields.Length; i++)
			{
			    ds.Tables[0].Columns.Add(new DataColumn(Fields[i],Type.GetType(StringType[i])));
			}

            return ds;
        }
    }
}
