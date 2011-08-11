using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// DUYVT_31.03.10
    /// </summary>    
    public interface IPivotChart
    {
        FieldPivot _getRowField();
        FieldPivot _getColumnField();
        FieldPivot _getDataField();
        QueryBuilder PLBuildQueryFilter();
    }

    public interface IPivotOLAP
    {
        FieldPivot[] _getRowFields();
        FieldPivot[] _getColumnFields();
        FieldPivot[] _getDataFields();
        QueryBuilder PLBuildQueryFilter();
    }

    public interface IPivotProductReport
    {
        FieldPivot _getRowField_HANG_HOA();
        FieldPivot _getRowField_HANG_HOA_NHOM();
        FieldPivot _getColumnField_NGAY();
        FieldPivot _getDataField_TONG_TIEN();
        QueryBuilder PLBuildQueryFilter();
    }

    public interface IPivotOrderReport
    {
        FieldPivot _getRowField_MA_DON_HANG();
        FieldPivot _getRowField_HANG_HOA();
        FieldPivot _getDataField_DON_GIA();
        FieldPivot _getDataField_SO_LUONG();
        FieldPivot _getDataField_GIAM_GIA();
        FieldPivot _getDataField_TONG_TIEN();
        QueryBuilder PLBuildQueryFilter();
    }





    public interface IPivotSummary
    {
        FieldPivot[] _getRowFields();
        FieldPivot[] _getColumnFields();
        FieldPivot _getDataField();
        QueryBuilder PLBuildQueryFilter();
    }



    public enum TypeField
    {   
        VanBan,
        So,
        NgayThang,
        VND,
        USD
    }

    public enum FollowGroupField
    {
        Ngay,
        Thang,
        Quy,
        Nam
    }

    public class FieldPivot
    {
        string fieldName = "";
        string caption = "";        
        TypeField typeField;
        int visibleIndex;
        int width;
        string formatString = "";
        FollowGroupField followGroupField;

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        public TypeField TypeField
        {
            get { return typeField; }
            set { typeField = value; }
        }

        public int VisibleIndex
        {
            get { return visibleIndex; }
            set { visibleIndex = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        
        public string FormatString
        {
            get { return formatString; }
            set { formatString = value; }
        }

        public FollowGroupField FollowGroupField
        {
            get { return followGroupField; }
            set { followGroupField = value; }
        }

        public FieldPivot() { }

        public FieldPivot(string fieldName, string caption, TypeField typeField, 
            int visibleIndex, int width)
        {
            this.fieldName = fieldName;
            this.caption = caption != "" ? caption : fieldName;
            this.typeField = typeField;
            this.visibleIndex = visibleIndex;
            this.width = width;
        }

        public FieldPivot(string fieldName, string caption, TypeField typeField, string formatString,
            int visibleIndex, int width)
        {
            this.fieldName = fieldName;
            this.caption = caption != "" ? caption : fieldName;
            this.typeField = typeField;
            this.formatString = formatString;
            this.visibleIndex = visibleIndex;
            this.width = width;
        }

        public FieldPivot(string fieldName, string caption, TypeField typeField, string formatString,
            int visibleIndex, int width, FollowGroupField followGroupField)
        {
            this.fieldName = fieldName;
            this.caption = caption != "" ? caption : fieldName;
            this.typeField = typeField;
            this.formatString = formatString;
            this.visibleIndex = visibleIndex;
            this.width = width;
            this.followGroupField = followGroupField;
        }

        public FieldPivot(string fieldName, string caption, TypeField typeField,
            int visibleIndex, int width, FollowGroupField followGroupField)
        {
            this.fieldName = fieldName;
            this.caption = caption != "" ? caption : fieldName;
            this.typeField = typeField;
            this.visibleIndex = visibleIndex;
            this.width = width;
            this.followGroupField = followGroupField;
        }

        public FieldPivot _set(string caption)
        {
            this.caption = caption;
            return this;
        }

        public FieldPivot _set(string caption, FollowGroupField followGroupField)
        {
            this.followGroupField = followGroupField;
            this.caption = caption;
            return this;
        }
    }
}