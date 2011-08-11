using System;
using System.Collections.Generic;
using System.Text;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Lớp hỗ trợ làm việc với Crystall Report
    /// </summary>
    public class HelpCrystalReport
    {
        public static ParameterField CreateParameter(string ParamName, object Value)
        {
            ParameterField paramField = new ParameterField();
            paramField.Name = ParamName;
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = Value;
            paramField.CurrentValues.Add(paramDiscreteValue);

            return paramField;
        }

        //PHUOCNC Sửa lại cho tổng quát hơn
        //FieldName = Caption + index
        public static ParameterFields CalcParameters(string[] Captions)
        {
            //Không được thay đổi vì Template Report mình dùng những Field có tên là vậy          
            string Caption = "Caption";
            int MAX_COL = 15;

            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField = null;
            ParameterDiscreteValue paramValue = null; 
            for (int i = 0; i < Captions.Length; i++)
            {
                paramValue = new ParameterDiscreteValue();
                paramValue.Description = "Caption" + i;
                paramValue.Value = Captions[i];

                paramField = HelpCrystalReport.CreateParameter(Caption + i, Captions[i]);
                paramField.CurrentValues.Add(paramValue);                
                paramFields.Add(paramField);

                //paramFields.Add(HelpCrystalReport.CreateParameter(Caption + i, Captions[i]));
            }

            for (int i = Captions.Length; i < MAX_COL; i++)
            {
                paramValue = new ParameterDiscreteValue();
                paramValue.Description = "Caption" + i;
                paramValue.Value = "";

                paramField = HelpCrystalReport.CreateParameter(Caption + i, "");
                paramField.CurrentValues.Add(paramValue);
                paramFields.Add(paramField);

                //paramFields.Add(HelpCrystalReport.CreateParameter(Caption + i, ""));
            }
            //Set value for first parameter
            

            
            
            

            return paramFields;
        }

        /// <summary>Hàm kiểm tra xem máy đã cài máy in chưa
        /// </summary>
        public static bool HasPrinter()
        {
            if (PrinterSettings.InstalledPrinters.Count == 0)
                return false;
            return true;
        }

        private PrinterSettings.PaperSizeCollection PopulatePaperSizesOfPrinter(string PrinterName)
        {
            try
            {
                PrinterSettings printSet = new PrinterSettings();
                printSet.PrinterName = PrinterName;
                return printSet.PaperSizes;
            }
            catch
            {
                return null;
            }          
        }
    }
}
