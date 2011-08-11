using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ProtocolVN.Framework.Win.Dev.Report;
using CrystalDecisions.Web;
using System.Drawing;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win.Report;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using ProtocolVN.Framework.Win.Report.TemplateReport;

namespace ProtocolVN.Framework.Win
{
    public enum PLDynRepType
    {
        HSheet,
        VSheet
    }

    public class DynamicSheetReport
    {
        private static int MAX_COL = 15;
        private static string Caption = "Caption";          
        private static string CaptionBox = "CaptionBox";    
        private static string Col = "Col";                  
        private static string ColBox = "FieldBox";          

        //PHUOCNC
        //Định dạng dữ liệu trong Report
        //Chưa thống nhất với toàn bộ hệ thống
        private static string Format(Object obj, out Alignment align)
        {
            align = Alignment.LeftAlign;
            if (obj is System.Int64 ||
                obj is System.Int32 ||
                obj is System.Int16)
            {
                Int64 tmp = Int64.Parse(obj.ToString());
                align = Alignment.RightAlign;
                return tmp.ToString("n");
            }
            else if (obj is DateTime)
            {
                align = Alignment.HorizontalCenterAlign;
                return ((DateTime)obj).ToString("d");
            }
            return obj.ToString();
        }

        //Hàm chuyển dữ liệu cần hiện thị trong report về định dạng mong muốn
        //Vì các field trong template mẫu là chuỗi không nên muốn chuyển cần phải định 
        //dạng dữ liệu lại và chú ý một mảng Align được trả về theo nguyên tắc định dạng dữ liệu của hệ thống
        private static DataSet ToSheetDataSet(DataSet Source, string[] FieldNames, out Alignment[] aligns)
        {
            aligns = new Alignment[FieldNames.Length];
            DataSet dsTempt = new DataSet("Schema");
            DataTable dt = new DataTable("SchemaTable");
            //Xây dựng cấu trúc bảng
            for (int i = 0; i < FieldNames.Length; i++)
                dt.Columns.Add(Col + i);
            dsTempt.Tables.Add(dt);
            //Đỗ dữ liệu bảng
            foreach (DataRow dr in Source.Tables[0].Rows)
            {
                DataRow tempt = dsTempt.Tables[0].NewRow();
                for (int i = 0; i < FieldNames.Length; i++)
                {
                    tempt[i] = Format(dr[FieldNames[i]], out aligns[i]);
                }
                dsTempt.Tables[0].Rows.Add(tempt);
            }
            return dsTempt;
        }

        //Tính toán kích thước của các đối tượng trên report
        private static void CalcSize(out int[] Lefts, out int[] Widths, int[] ExpectedWidths, int MaxWidth, int MaxHeight)
        {
            Lefts = new int[ExpectedWidths.Length];
            Widths = new int[ExpectedWidths.Length];
            Lefts[0] = 0;
            Widths[0] = ExpectedWidths[0];
            for (int i = 1; i < ExpectedWidths.Length; i++)
            {
                Lefts[i] = Lefts[i - 1] + Widths[i - 1] - 5;
                Widths[i] = ExpectedWidths[i];
            }
        }
        
        private static ParameterFields SetFormatPosition(ReportDocument Template, Alignment[] aligns, string[] FieldNames, 
            string[] Captions, int[] ExpectedWidths, string Title, string SubTitle)
        {
            //Kho gia A dung
            int MaxWidth = 1000;
            int MaxHeight = 1000;
            //Fix Number   
            int HeightBox = 300;
            int HeightText = 240;
            int Top = 0;
            int TopText = 30;
            int LeftText = 60;
            int RightText = 90;
            int[] Lefts;
            int[] Widths;
            ParameterFields Params;
            Params = HelpCrystalReport.CalcParameters(Captions);
            ParameterField paramField = null;
            ParameterDiscreteValue paramValue = null; 

            paramValue = new ParameterDiscreteValue();
            paramValue.Description = "Title";
            paramValue.Value = Title;

            paramField = HelpCrystalReport.CreateParameter("Title", Title);
            paramField.CurrentValues.Add(paramValue);
            paramField.AllowCustomValues = false;
            Params.Add(paramField);

            //Params.Add(HelpCrystalReport.CreateParameter("Title", Title));
            
            paramValue = new ParameterDiscreteValue();
            paramValue.Description = "SubTitle";
            paramValue.Value = SubTitle;

            paramField = HelpCrystalReport.CreateParameter("SubTitle", SubTitle);
            paramField.CurrentValues.Add(paramValue);
            paramField.AllowCustomValues = false;
            Params.Add(paramField);
            //Params.Add(HelpCrystalReport.CreateParameter("SubTitle", SubTitle));

            CalcSize(out Lefts, out Widths, ExpectedWidths, MaxWidth, MaxHeight);
            #region SetPosition
            for (int i = 0; i < FieldNames.Length; i++)
            {
                //CaptionBox
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).Top = Top;
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).Left = Lefts[i];
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).Right = Lefts[i] + Widths[i];
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).Bottom = Top + HeightBox;

                //Caption
                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Top = Top + TopText;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Left = Lefts[i] + LeftText;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Width = Widths[i] - RightText;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Height = HeightText;

                //ColBox
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).Top = Top;
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).Left = Lefts[i];
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).Right = Lefts[i] + Widths[i];
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).Bottom = Top + HeightBox;

                //Col
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Top = Top + TopText;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Left = Lefts[i] + LeftText;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Width = Widths[i] - RightText;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Height = HeightText;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).ObjectFormat.HorizontalAlignment = aligns[i];
            }
            for (int i = FieldNames.Length; i < MAX_COL; i++)
            {
                //Caption
                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Top = Top;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Left = 0;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Width = 0;
                //CaptionBox
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).Top = Top;
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).Left = 0;
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).Right = 0;
                //Col
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Top = Top;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Left = 0;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Width = 0;

                //ColBox
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).Top = Top;
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).Left = 0;
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).Right = 0;

                ((FieldObject)Template.ReportDefinition.ReportObjects[Caption + i]).Color = Color.White;
                ((BoxObject)Template.ReportDefinition.ReportObjects[CaptionBox + i]).LineColor = Color.White;
                ((FieldObject)Template.ReportDefinition.ReportObjects[Col + i]).Color = Color.White;
                ((BoxObject)Template.ReportDefinition.ReportObjects[ColBox + i]).LineColor = Color.White;
            }
            #endregion
            
            return Params;
        }

        public static void ToSheetReport(ReportDocument Template, out ParameterFields Params,
            DataSet Source, string[] FieldNames, string[] Captions, int[] ExpectedWidths,
            string Title, string SubTitle)
        {
            Alignment[] aligns;
            DataSet ReportSource = ToSheetDataSet(Source, FieldNames, out aligns);
            if (Template is IDynSheetReport)
            {
                Params = SetFormatPosition(Template, aligns, FieldNames, Captions, ExpectedWidths, Title, SubTitle);
                Template.SetDataSource(ReportSource);
            }
            else
            {
                Params = null;
                PLMessageBoxDev.ShowMessage("Template phải là 1 report thuộc template Report của cty");
            }
        }


        public static XtraForm Preview(XtraForm mainForm, PLDynRepType ReportType, 
            DataSet Source, string[] FieldNames, string[] Captions, int[] ExpectedWidths,
            string Title, string SubTitle)
        {
            ParameterFields Params = null;
            try
            {
                #region Mở rộng nếu có thêm Report mới
                PLBlankReport frm = new PLBlankReport();
                PLCrystalReportViewer view = new PLCrystalReportViewer();
                if (ReportType == PLDynRepType.HSheet)
                {
                    HSheetReport Report = new HSheetReport();
                    DynamicSheetReport.ToSheetReport(Report, out Params, Source,
                        FieldNames, Captions, ExpectedWidths, Title, SubTitle);
                    //view._I.ReportSource = Report;//phiên bản 12
                    view.ReportSource = Report;//phiên bản 10
                }
                else if (ReportType == PLDynRepType.VSheet)
                {
                    VSheetReport Report = new VSheetReport();
                    DynamicSheetReport.ToSheetReport(Report, out Params, Source,
                        FieldNames, Captions, ExpectedWidths, Title, SubTitle);
                    //view._I.ReportSource = Report;//phien bản 12
                    view.ReportSource = Report;//phien bản 10
                }
                #endregion

                //view._I.ParameterFieldInfo = Params;//phien bản 12
                //view.ParameterFieldInfo = Params;//phien bản 10
                frm.WindowState = FormWindowState.Maximized;
                view.Dock = DockStyle.Fill;
                frm.Controls.Add(view);

                return frm;
            }
            catch (Exception ex){
                PLException.AddException(ex);
                return null;
            }
        }
    }
}
