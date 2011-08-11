using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraEditors.DXErrorProvider;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Interface cho report bình thường
    /// </summary>
    public interface IReportFilter
    {
        DataSet getDataSet();
        Dictionary<string, Object> GetParamFieldValue();
        DataSet[] getSubReports();
        void ValidateFilter(DXErrorProvider Error);
    }

    /// <summary>
    /// Cho phép tùy biến file report lúc runtime.
    /// </summary>
    public interface IAdvanceReportFilter : IReportFilter
    {
        string getReportFile();
    }

    //Dùng cho những báo biểu có thể thấy được data trong Form
    public interface IDataReport
    {
        Object Define(Object main);
    }

    /// <summary>
    /// Dùng cho tuỳ biến chiều rộng cột và tiêu đề cột
    /// </summary>
    public interface ICustomReport
    {
        Object Define(Object main);
    }

    public interface IDynSheetReportFilter : IReportFilter
    {
        void GetParam(out string Title, out string SubTitle, out string[] FieldNames,
            out string[] Captions, out int[] Widths, out bool IsVertical);
    }

    public interface IProtocolXtraReport
    {
        DataSet getDataSet();
    }
}