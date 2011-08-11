using System;
using System.Collections.Generic;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections;
using System.Data;
using ProtocolVN.Framework.Core;
using DevExpress.XtraGrid.Columns;

namespace ProtocolVN.Framework.Win
{
    public class PLReportData
    {
        public ReportDocument Report;
        public List<DataSet> DSList;

        public PLReportData()
        {
        }

        public PLReportData(ReportDocument report, List<DataSet> dsList)
        {
            Report = report;
            DSList = dsList;
        }
    }

    public class PLReportDataConfig
    {
        private List<DataSet> _DSList;
        public string[] DSListName
        {
            get
            {
                string[] list = new string[DataTables.Count];
                for (int i = 0; i < DataTables.Count; i++)
                {
                    list[i] = ((PLReportDataTable)DataTables[i]).Name;
                }
                return list;
            }
            set
            {
                if (value == null) return;
                int n = DataTables.Count;

                if (n > value.Length) n = value.Length;
                for (int i = 0; i < n; i++)
                {
                    ((PLReportDataTable)DataTables[i]).Name = value[i];
                }
            }
        }

        public ArrayList DataTables;
        public string ReportName;

        public PLReportDataConfig(string[] listtableName, List<DataSet> dsList)
        {
            ReportName = "";
            _DSList = dsList;
            DataTables = new ArrayList();
            Init();
            DSListName = listtableName;
        }

        public PLReportDataConfig(string Name, string[] listtableName, List<DataSet> dsList)
        {
            ReportName = Name;
            _DSList = dsList;
            DataTables = new ArrayList();
            Init();
            DSListName = listtableName;
        }

        private void Init()
        {
            if (_DSList != null)
            {
                for (int i = 0; i < _DSList.Count; i++)
                {
                    PLReportDataTable table = null;
                    if (_DSList[i] != null && ((DataSet)_DSList[i]).Tables.Count > 0)
                    {
                        if (DSListName != null && i < DSListName.Length)
                        {
                            table = new PLReportDataTable(DSListName[i], ((DataSet)_DSList[i]).Tables[0]);
                        }
                        else table = new PLReportDataTable("Bảng " + i, ((DataSet)_DSList[i]).Tables[0]);
                    }
                    else
                    {
                        if (DSListName != null && i < DSListName.Length)
                        {
                            table = new PLReportDataTable(DSListName[i], null);
                        }
                        else table = new PLReportDataTable("Bảng " + i, null);
                    }
                    if (table != null) DataTables.Add(table);
                }
            }
        }

        public PLReportDataTable GetTable(int index)
        {
            if (index < DataTables.Count)
            {
                return (PLReportDataTable)DataTables[index];
            }
            else return null;
        }

        public PLReportDataTable GetTable(string TabbleName)
        {
            for (int i = 0; i < DataTables.Count; i++)
            {
                PLReportDataTable table = (PLReportDataTable)DataTables[i];
                if (table.Name == TabbleName)
                    return table;
            }
            return null;
        }


    }

    public class PLReportDataField
    {
        public string Caption;
        public string FieldName;
        public int ID;

        public PLReportDataField(string caption, string fieldName)
        {
            Caption = caption;
            FieldName = fieldName;
            ID = 0;
        }

        public PLReportDataField(string caption, string fieldName, int id)
        {
            Caption = caption;
            FieldName = fieldName;
            ID = id;
        }
    }

    public class PLReportDataTable
    {
        private string _name;
        public string Name
        {
            get
            {
                if (DataSource != null)
                    return DataSource.TableName;
                else
                    return _name;
            }
            set
            {
                if (DataSource != null) DataSource.TableName = value;
                _name = value;
            }
        }
        public ArrayList Fields;
        public DataTable DataSource;

        public PLReportDataTable(string name, DataTable dataSource)
        {
            Name = name;
            Fields = new ArrayList();
            DataSource = dataSource;
        }

        public void AddField(PLReportDataField field)
        {
            field.ID = Fields.Count;
            Fields.Add(field);
        }

        public void AddField(string Caption, string FieldName)
        {
            PLReportDataField newField = new PLReportDataField(Caption, FieldName);
            newField.ID = Fields.Count;
            Fields.Add(newField);
        }

        /// <summary>
        /// Lấy Caption của Object trong reportDocument tương ứng
        /// </summary>
        /// <param name="ObjectName"></param>
        /// <param name="FieldName"></param>
        /// <param name="reportDocument"></param>
        public void AddField(string ObjectName, string FieldName, ReportDocument reportDocument)
        {
            try
            {
                TextObject obj = (TextObject)reportDocument.ReportDefinition.ReportObjects[ObjectName];
                string Caption = obj.Text;
                AddField(Caption, FieldName);

            }
            catch (Exception)
            {
                PLMessageBox.ShowErrorMessage("Lỗi - Không có object này - Method AddField()");
                return;
            }

        }

        public PLReportDataField GetField(int index)
        {
            if (index < Fields.Count)
            {
                return (PLReportDataField)Fields[index];
            }
            else return null;
        }

        public PLReportDataField GetField(string FieldName)
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                PLReportDataField field = (PLReportDataField)Fields[i];
                if (field.FieldName == FieldName)
                    return field;
            }
            return null;
        }

        /// <summary>
        /// Lấy các cột tương ứng có từ bảng dữ liệu
        /// Cột nào có trong bảng như chưa có Caption (chưa nằm trong Fields) thì tên cột sẽ là tên Field
        /// Lưu ý: chỉ xét các cột trong bảng dữ liệu, 
        /// dù cho add thêm nhiều Caption nhưng không có tên field tương ứng trong bảng thì sẽ không xuất ra giao diện
        /// </summary>
        /// <returns></returns>
        public ArrayList GetColumns()
        {
            ArrayList listCol = new ArrayList();
            if (DataSource != null)
            {
                for (int i = 0; i < DataSource.Columns.Count; i++)
                {
                    string fieldName = DataSource.Columns[i].ColumnName;
                    PLReportDataField field = GetField(fieldName);
                    GridColumn col = new GridColumn();

                    // set attribute for column
                    if (field != null)
                    {
                        col.Name = field.FieldName;
                        col.Caption = field.Caption;
                        col.FieldName = field.FieldName;
                        col.VisibleIndex = field.ID;
                    }
                    else
                    {
                        col.Name = DataSource.Columns[i].ColumnName;
                        col.Caption = DataSource.Columns[i].Caption;
                        col.FieldName = DataSource.Columns[i].ColumnName;
                        col.VisibleIndex = DataSource.Columns.Count;
                    }
                    col.Visible = true;
                    listCol.Add(col);
                }
            }
            return listCol;
        }
    }
}
