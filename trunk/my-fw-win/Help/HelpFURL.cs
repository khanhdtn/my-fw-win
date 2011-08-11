using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp hỗ trợ xử lý đường dẫn FURL
    /// </summary>
    public class HelpFURL
    {
        public static String FURL(Type type)
        {
            return type.FullName;
        }
        //Vấn đề đổi tên Function nữa
        public static String FURL(String Main, String Detail, String FunctionName)
        {
            return Main + "?param=" + Detail + "?param=" + FunctionName;
        }
        public static String FURL(String Main, String Value)
        {
            return Main + "?param=" + Value;
        }


        /// <summary>
        /// Dùng cho khai báo report
        /// </summary>
        public static String FURLReport(Type report)
        {
            return typeof(frmBaseReport).FullName + "?param=" + report.FullName;
        }

        /// <summary>
        /// Dùng cho khai báo dạng gọi hàm trong lớp methodExec.
        /// </summary>
        /// <param name="methodExec"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public static String FURLMethodExec(Type methodExec, String FunctionName){
            return methodExec.FullName + "?param=" + FunctionName;
        }
        
    }    
}
