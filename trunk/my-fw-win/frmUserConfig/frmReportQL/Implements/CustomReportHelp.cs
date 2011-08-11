using System;
using System.Collections.Generic;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    public class PLReportCaption
    {
        private ReportDocument reportDocument;

        public string Caption
        {
            get
            {
                try
                {
                    TextObject reportObj = (TextObject)reportDocument.ReportDefinition.ReportObjects[ReportObjectName];
                    return reportObj.Text;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            set
            {

                ChangeCaption(value);
            }
        }
        public string ReportObjectName;
        public string FieldName;

        public PLReportCaption(string _ReportObjectName, string _FieldName, ReportDocument _report)
        {
            ReportObjectName = _ReportObjectName;
            FieldName = _FieldName;
            reportDocument = _report;
        }

        private void ChangeCaption(string newCaption)
        {
            try
            {
                TextObject reportObj = (TextObject)reportDocument.ReportDefinition.ReportObjects[ReportObjectName];
                reportObj.Text = newCaption;
            }
            catch (Exception)
            {
                return;
            }
        }
    }

    public class PLReportColumn
    {
        public string HeaderName
        {
            get
            {
                try
                {
                    TextObject reportObj = (TextObject)reportDocument.ReportDefinition.ReportObjects[ObjectName];
                    return reportObj.Text;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            set
            {
                ChangeHeaderName(value);
            }
        }
        public string FieldName;
        public string ObjectName;
        public string LineAffectName;
        private ReportDocument reportDocument;

        private ArrayList ListSubObjectsName;// danh sách các đối tượng sẽ thay đổi theo nếu cột thay đổi

        /// <summary>
        /// Constructor
        /// <param name="_HeaderName">The header name (used for showing in the grid</param>
        /// <param name="_FieldName">The field name of the database</param>
        /// <param name="_ObjectName">The name of the object</param>
        /// <param name="_LineAffectName">The line name</param>
        /// <param name="_report">The report Document object</param>
        /// </summary>
        public PLReportColumn(string _FieldName, string _ObjectName, string _LineAffectName, string[] listSubObjects, ReportDocument _report)
        {
            // HeaderName = _HeaderName;
            FieldName = _FieldName;
            ObjectName = _ObjectName;
            LineAffectName = _LineAffectName;
            reportDocument = _report;

            ListSubObjectsName = new ArrayList();
            if (listSubObjects != null)
            {
                for (int i = 0; i < listSubObjects.Length; i++)
                {
                    ListSubObjectsName.Add(listSubObjects[i]);
                }
            }
        }

        public string[] GetListSubObjectsName()
        {
            if (ListSubObjectsName.Count == 0) return null;
            string[] listName = new string[ListSubObjectsName.Count];
            for (int i = 0; i < ListSubObjectsName.Count; i++)
            {
                listName[i] = (string)ListSubObjectsName[i];
            }
            return listName;
        }

        /// <summary>
        /// Get the width of the column (unit: pixel)
        /// 1 pixel = 0.1mm
        /// </summary>
        /// <returns>The column's width (pixel)</returns>
        public int GetWidth()
        {
            if (reportDocument != null && reportDocument.ReportDefinition.ReportObjects[ObjectName] != null)
            {
                return reportDocument.ReportDefinition.ReportObjects[ObjectName].Width / 10;
            }
            return 0;
        }

        public void ChangeWidth(int deltaWidth)
        {
            if (reportDocument == null) return;

            try
            {

                ReportObject obj = reportDocument.ReportDefinition.ReportObjects[ObjectName];
                if (obj != null) obj.Width += deltaWidth;

                if (LineAffectName != null)
                {
                    BoxObject line = (BoxObject)reportDocument.ReportDefinition.ReportObjects[LineAffectName];
                    line.Left += deltaWidth;
                    line.Right += deltaWidth;
                }

                if (ListSubObjectsName != null)
                {
                    for (int i = 0; i < ListSubObjectsName.Count; i++)
                    {
                        ReportObject subObj = reportDocument.ReportDefinition.ReportObjects[(string)ListSubObjectsName[i]];
                        if (subObj != null)
                            subObj.Width += deltaWidth;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void Move(int delta)
        {
            if (reportDocument == null) return;
            try
            {
                ReportObject obj = reportDocument.ReportDefinition.ReportObjects[ObjectName];
                if (obj != null) obj.Left += delta;
                // obj.Width += delta;

                if (LineAffectName != null)
                {
                    BoxObject line = (BoxObject)reportDocument.ReportDefinition.ReportObjects[LineAffectName];
                    line.Left += delta;
                    line.Right += delta;
                }

                if (ListSubObjectsName != null)
                {
                    for (int i = 0; i < ListSubObjectsName.Count; i++)
                    {
                        ReportObject subObj = reportDocument.ReportDefinition.ReportObjects[(string)ListSubObjectsName[i]];
                        if (subObj != null)
                            subObj.Left += delta;
                    }
                }
            }
            catch (Exception)
            {
                // this col or affected lines have wrong name
                return;
            }
        }

        private void ChangeHeaderName(string newHeaderName)
        {
            try
            {
                TextObject reportObj = (TextObject)reportDocument.ReportDefinition.ReportObjects[ObjectName];
                reportObj.Text = newHeaderName;
            }
            catch (Exception)
            {
                return;
            }
        }

    }

    public class PLReportRow
    {
        private ReportDocument reportDocument;

        public string ObjectName;
        public ArrayList Columns;
        public bool isChangeWidth;

        public PLReportRow(string _ObjectName, ReportDocument _report)
        {
            ObjectName = _ObjectName;
            reportDocument = _report;
            Columns = new ArrayList();
            isChangeWidth = false;
        }

        public PLReportRow(string _Name, ReportDocument _report, ArrayList _ListColumns)
        {
            ObjectName = _Name;
            reportDocument = _report;
            Columns = _ListColumns;
            isChangeWidth = false;
        }

        /// <summary>
        /// Add new column to row       
        /// </summary>
        /// <param name="_HeaderName">Column's header name</param>
        /// <param name="_FieldName">Column's field name</param>
        /// <param name="_ObjectName">Column's name</param>
        /// <param name="_LineAffectName">The affected line if the column is changed</param>
        /// <param name="_listSubObjects">List names of the sub objects which are changed when the column is changed</param>
        public void AddColumn(string _FieldName, string _ObjectName, string _LineAffectName, string[] _listSubObjects)
        {
            PLReportColumn newCol = new PLReportColumn(_FieldName, _ObjectName, _LineAffectName, _listSubObjects, reportDocument);

            Columns.Add(newCol);
        }

        private GridColumn CreateGridColumn(PLReportColumn colObject, int visibleIndex)
        {
            GridColumn col = new GridColumn();
            col.Name = colObject.ObjectName;
            col.Caption = colObject.HeaderName;
            col.Visible = true;
            col.VisibleIndex = visibleIndex;
            // convert to pixel
            col.Width = colObject.GetWidth();
            if (colObject.FieldName != null)
                col.FieldName = colObject.FieldName;
            else col.FieldName = colObject.ObjectName;

            return col;
        }

        public PLReportColumn GetColumn(int index)
        {
            if (index < Columns.Count)
                return (PLReportColumn)Columns[index];
            return null;
        }

        public PLReportColumn GetColumn(string ColumnName)
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                PLReportColumn col = (PLReportColumn)Columns[i];
                if (col.ObjectName == ColumnName)
                    return col;
            }
            return null;
        }

        public ArrayList GetPLReportColumns()
        {
            ArrayList listCol = new ArrayList();
            if (reportDocument == null || Columns == null) return listCol;
            for (int i = 0; i < Columns.Count; i++)
            {
                PLReportColumn colObject = (PLReportColumn)Columns[i];
                GridColumn col = CreateGridColumn(colObject, i);
                listCol.Add(col);
            }
            return listCol;
        }

        public void ApplyChanges(GridColumnCollection gridColumn)
        {
            if (Columns == null || Columns.Count == 0) return;

            isChangeWidth = false;
            for (int i = 0; i < Columns.Count; i++)
            {
                PLReportColumn colObj = (PLReportColumn)Columns[i];
                string colName = colObj.ObjectName;
                GridColumn col = gridColumn.ColumnByName(colName);
                // delta tính theo đơn vị mm
                int delta = (col.VisibleWidth - colObj.GetWidth()) * 10;

                colObj.ChangeWidth(delta);

                for (int j = i + 1; j < Columns.Count; j++)
                {
                    ((PLReportColumn)Columns[j]).Move(delta);
                }
            }
        }

        /// <summary>
        /// Move row Object and all of its columns
        /// </summary>
        /// <param name="delta">Độ dài tính theo mm</param>
        public void Move(int delta)
        {
            // move all row's columns
            if (Columns != null)
            {
                for (int i = 0; i < Columns.Count; i++)
                {
                    ((PLReportColumn)Columns[i]).Move(delta);
                }
            }
            // move row
            try
            {
                ReportObject RowObj = reportDocument.ReportDefinition.ReportObjects[ObjectName];
                RowObj.Left += delta;
            }
            catch (Exception)
            {
                // row is not existed or there are some errors when moving row obj
                return;
            }
        }
        /// <summary>
        /// Get the width of the row
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            int lastIndex = Columns.Count;
            if (lastIndex > 0)
            {
                lastIndex = lastIndex - 1;
                int colWidth = 0;
                //  int rowWidth = 0;
                PLReportColumn lastColObj = (PLReportColumn)Columns[lastIndex];
                string LastColObjName = lastColObj.ObjectName;
                try
                {
                    ReportObject lastObj = reportDocument.ReportDefinition.ReportObjects[LastColObjName];

                    //       ReportObject RowObj = (ReportObject)reportDocument.ReportDefinition.ReportObjects[ObjectName];

                    if (lastObj != null) colWidth = lastObj.Left + lastObj.Width;
                    //      if (RowObj != null) rowWidth = RowObj.Left + RowObj.Width;
                    //      if (colWidth > rowWidth) return colWidth;
                    //      else return rowWidth;
                    return colWidth;
                }
                catch (Exception)
                {
                    // trường hợp obj cuối không tồn tại
                    return 0;
                }
            }
            else return 0;
        }
    }

    public class ReportConfig
    {
        public ReportDocument reportDocument;

        public const string Caption_CurrentValue = "CurrentValue";
        public const string Caption_NewValue = "NewValue";


        public string Name;
        public ArrayList Rows;
        public ArrayList Captions;

        public ReportConfig(ReportDocument _report)
        {
            reportDocument = _report;
            Rows = new ArrayList();
            Captions = new ArrayList();
        }

        /// <summary>
        /// Add new row to Report Config
        /// </summary>
        /// <param name="_BoxObjectName">Name of the box object in the main report</param>
        public void AddPLReportRow(string _RowObjectName)
        {
            PLReportRow newRow = new PLReportRow(_RowObjectName, reportDocument);
            Rows.Add(newRow);
        }

        public PLReportRow GetPLReportRow(int index)
        {
            if (index < Rows.Count)
            {
                return (PLReportRow)Rows[index];
            }
            return null;
        }

        public PLReportRow GetPLReportRow(string RowName)
        {
            for (int i = 0; i < Rows.Count; i++)
            {
                PLReportRow row = (PLReportRow)Rows[i];
                if (row.ObjectName == RowName)
                    return row;
            }
            return null;
        }

        private string[] GetListAllCaptions()
        {
            if (reportDocument != null)
            {
                List<string> listCaptions = new List<string>();
                for (int i = 0; i < reportDocument.ReportDefinition.ReportObjects.Count; i++)
                {
                    if (reportDocument.ReportDefinition.ReportObjects[i] is TextObject)
                    {
                        listCaptions.Add(reportDocument.ReportDefinition.ReportObjects[i].Name);
                    }
                }

                string[] list = new string[listCaptions.Count];
                for (int i = 0; i < listCaptions.Count; i++)
                    list[i] = listCaptions[i];
                return list;
            }
            return null;
        }

        public void LoadListAllCaptions()
        {
            string[] listCaptions = GetListAllCaptions();
            if (listCaptions != null)
            {
                if (Captions == null) Captions = new ArrayList();
                else Captions.Clear();
                AddListPLReportCaptions(listCaptions);
            }
        }

        /// <summary>
        /// nếu đối tượng không tồn tại, Caption này sẽ không được thêm
        /// </summary>
        /// <param name="ObjectName">Tên đối tượng cần thay đổi Caption</param>
        public void AddPLReportCaption(string ObjectName)
        {
            PLReportCaption newCaption = new PLReportCaption(ObjectName, null, reportDocument);
            if (newCaption.Caption == null) return;
            Captions.Add(newCaption);
        }

        public void AddListPLReportCaptions(string[] ListCaptions)
        {
            for (int i = 0; i < ListCaptions.Length; i++)
            {
                AddPLReportCaption(ListCaptions[i]);
            }
        }

        public PLReportCaption GetPLReportCaption(int index)
        {
            if (index < Captions.Count)
                return (PLReportCaption)Captions[index];
            return null;
        }

        public PLReportCaption GetPLReportCaption(string CaptionName)
        {
            for (int i = 0; i < Captions.Count; i++)
            {
                PLReportCaption caption = (PLReportCaption)Captions[i];
                if (caption.Caption == CaptionName) return caption;
            }
            return null;
        }

        public void ChangeCaption(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                try
                {
                    string currentValue = table.Rows[i][Caption_CurrentValue].ToString();
                    string newValue = table.Rows[i][Caption_NewValue].ToString();

                    PLReportCaption caption = GetPLReportCaption(currentValue);
                    caption.Caption = newValue;
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// PHUOCNC: Nếu nhiều row thì làm như thế này ko hợp lý.
        /// Nó phải trả về ArrayList<ArrayList<GridColumn>>
        /// </summary>
        /// <returns></returns>
        public ArrayList GetGridColumn()
        {
            ArrayList listCol = new ArrayList();
            if (reportDocument == null || Rows == null) return listCol;

            for (int i = 0; i < Rows.Count; i++)
            {
                ArrayList listColFromRow = ((PLReportRow)Rows[i]).GetPLReportColumns();

                /*    
                for (int j = 0; j < listColFromBox.Count; j++)
                {
                    listCol.Add(listColFromBox[j]);
                    count++;
                }*/
                listCol.Add(listColFromRow);

            }
            return listCol;
        }

        public void ApplyChanges(GridColumnCollection gridColumns)
        {
            if (Rows != null)
            {
                for (int i = 0; i < Rows.Count; i++)
                {
                    ((PLReportRow)Rows[i]).ApplyChanges(gridColumns);
                }
            }
        }

        public void SaveChanges()
        {
            reportDocument.SaveAs(reportDocument.FileName);

        }

        public void Refresh()
        {
            reportDocument.Refresh();
        }

        public void ChangeHeaderName(DataTable table)
        {
            for (int iRow = 0; iRow < Rows.Count; iRow++)
            {
                PLReportRow row = GetPLReportRow(iRow);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    try
                    {
                        string colName = table.Columns[i].ColumnName;

                        string newValue = table.Rows[0][colName].ToString();

                        PLReportColumn col = row.GetColumn(colName);
                        if (col != null)
                            col.HeaderName = newValue;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
