using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class ReportItemPermission {
        public string filterClassName;//KEY

        public string reportName;

        public ReportItemPermission(string filterControlName, string report){
            this.filterClassName = filterControlName;
            this.reportName = report;
        }

        public override bool Equals(object obj)
        {
            ReportItemPermission report = (ReportItemPermission)obj;
            return report.filterClassName.Equals(this.filterClassName);
        }
    }

    public class ReportStore
    {
        public static List<ReportItemPermission> reportItems = new List<ReportItemPermission>();

        public static void addReport(ReportItemPermission report){
            if (FrameworkParams.isSupportDeveloper == true)
            {
                if (!reportItems.Contains(report))
                {
                    reportItems.Add(report);
                }
                else
                {
                    PLException.AddException(new Exception(report.filterClassName + " đã khai báo 2 lần.\nVui lòng xem lại."));
                }
            }
        }

        public static string ToSQL()
        {
            StringBuilder builder = new StringBuilder();
            foreach (ReportItemPermission reportItem in reportItems)
            {
                builder.AppendLine("INSERT INTO REPORT_CAT (ID, KEYID, NAME, VISIBLE_BIT) VALUES (gen_id(G_FW_ID, 1), '"+
                    reportItem.filterClassName + "', '" + reportItem.reportName + "', 'Y')");
            }
            return builder.ToString();
        }
    }
}
