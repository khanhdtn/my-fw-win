using System;
using System.Collections.Generic;
using System.Text;
using DevExpress;
using System.Data;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp cho phép tạo một lưới mà PL đang dùng.
    /// Nó giúp ta việc hóa các menuItem và cho phép chuyển tự động 'Y' -> Check box
    /// </summary>
    public class PLDynamicGrid
    {
        public PLGridView gv;     //Đối tượng lưới có thể dùng nó để mở rộng chức năng của lưới
        private DataSet ds;     //Để cập nhật CSDL
        private GridControl gridCtrl;

        #region Nạp dữ liệu vào Lưới dùng DataSet
        /// <summary>
        /// Hạn chế sử dụng
        /// </summary>
        public PLDynamicGrid(DevExpress.XtraGrid.GridControl gridCtrl, DataSet dataset, bool isUpdate)//isUpdate = true  : Lưới cho phép sửa = false : Lưới chỉ đọc 
        {
            init(gridCtrl, dataset, null, isUpdate, null);
            this.DisplayData();
        }
        public PLDynamicGrid(GridControl gridCtrl, DataSet dataset, bool isUpdate, PLGridView gv)
        {
            init(gridCtrl, dataset, gv, isUpdate, null);
            DisplayData();
        }
        #endregion

        #region Nạp dữ liệu vào Lưới dùng DataSet cho phép đặt caption cho các cột hiện thị
        /// <summary>
        /// Tạo ra một PL Grid với số field trong dataset tương ứng với các phần tử trong mảng listCaption
        /// isUpdate : true -> Cho phép cập nhật trên lưới
        ///          : false -> Không cho phép cập nhật trên lưới
        /// Sẽ tạo mới 1 GridView
        /// </summary>
        public PLDynamicGrid(GridControl gridCtrl, DataSet dataset, bool isUpdate, string[] listCaption)
        {
            init(gridCtrl, dataset, null, isUpdate, listCaption);            
            DisplayData();
        }

        /// <summary>
        /// Tạo ra một PL Grid với số field trong dataset tương ứng với các phần tử trong mảng listCaption
        /// isUpdate : true -> Cho phép cập nhật trên lưới
        ///          : false -> Không cho phép cập nhật trên lưới
        /// Không tạo mới GridView
        /// </summary>
        public PLDynamicGrid(GridControl gridCtrl, DataSet dataset, bool isUpdate, string[] listCaption, PLGridView gridview)
        {
            init(gridCtrl, dataset, gridview, isUpdate, listCaption);
            DisplayData();
        }
        #endregion
        
        public PLDynamicGrid(DevExpress.XtraGrid.GridControl gridCtrl, String tableName, bool isUpdate)
        {
            this.ds = DABase.getDatabase().LoadTable(tableName);
            init(gridCtrl, null, null, isUpdate, null);
            DisplayData();
        }

        #region Nạp dữ liệu vào Lưới dùng TableName
        /// <summary>
        /// Tạo ra một PL Grid với dữ liệu là trong tableName
        /// Các field trong table phải map với tên caption trong mảng ListCaption
        /// Tạo GridView mới
        /// </summary>
        public PLDynamicGrid(DevExpress.XtraGrid.GridControl gridCtrl, String tableName, bool isUpdate, string[] ListCaption)
        {
            this.ds = DABase.getDatabase().LoadTable(tableName);
            init(gridCtrl, null, null, isUpdate, ListCaption);
            DisplayData();
        }

        /// <summary>
        /// Tạo ra một PL Grid với dữ liệu là trong tableName
        /// Các field trong table phải map với tên caption trong mảng ListCaption
        /// Không Tạo GridView mới
        /// </summary>
        public PLDynamicGrid(GridControl gridCtrl, String tableName, bool isUpdate, string[] ListCaption, PLGridView gridview)
        {
            this.ds = DABase.getDatabase().LoadTable(tableName);
            init(gridCtrl, null, gridview, isUpdate, ListCaption);
            DisplayData();
        }
        #endregion

        #region Hàm hỗ trợ
        private void init(GridControl gridCtrl, DataSet dataset, PLGridView gridview, bool isUpdate, string[] ListCaption)
        {
            this.gridCtrl = gridCtrl;
            if (dataset!=null) this.ds = dataset;
            if (gridview != null) 
                this.gv = gridview;
            else
                this.gv = new PLGridView();
            this.gv.OptionsBehavior.Editable = isUpdate;
            this.LoadColumnFromDataSet(isUpdate);            
            if (ListCaption != null) setCaption(ListCaption);

            //XtraGridSupport.BitCBBToCheckImageCombo(this.gv);
        }
        //PHUOC? Xem xét lại danh sách các thuộc tính
        private void LoadColumnFromDataSet(bool isUpdate)
        {
            DevExpress.XtraGrid.Columns.GridColumn[] listColumn = new DevExpress.XtraGrid.Columns.GridColumn[ds.Tables[0].Columns.Count];
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                DevExpress.XtraGrid.Columns.GridColumn gc = new DevExpress.XtraGrid.Columns.GridColumn();
                DataColumn dc = ds.Tables[0].Columns[i];                
                gc.Name = "Col" + i;                                     
                gc.FieldName = dc.ColumnName;                                
                //TODO : Ten cot
                gc.AppearanceHeader.Options.UseTextOptions = true;
                gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gc.Caption = dc.Caption;                
                gc.Visible = true;
                gc.VisibleIndex = i;
                //gc.OptionsColumn.ReadOnly = !isUpdate;
                //gc.OptionsFilter.AllowFilter = false;
                //gc.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
                gv.Columns.Add(gc);   
                //gv.ValidateRow +=new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gv_ValidateRow);             
            }
        }
        private void setCaption(string[] listCaption)
        {
            for (int i = 0; i < listCaption.Length; i++)
            {
                gv.Columns[i].Caption = listCaption[i];
            }
        }
        private void DisplayData()
        {
            gridCtrl.MainView = gv;
            gridCtrl.DataSource = ds.Tables[0];
            gv.GridControl = gridCtrl;
        }

        public void ChangeProperty(string key, object value, int indexCol)
        {
            DevExpress.XtraGrid.Columns.GridColumn col = gv.Columns[indexCol];
            Type type = col.GetType();
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                if (property.Name.Equals(key))
                    if (property.CanWrite)
                    {
                        property.SetValue(col, value, null);
                    }
            }
        }

        #endregion
    }
}