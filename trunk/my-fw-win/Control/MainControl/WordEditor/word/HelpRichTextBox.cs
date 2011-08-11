using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using RichTextFormat= System.Windows.Forms;
using System.Windows.Forms;
namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Lớp này hỗ trợ lưu dữ liệu từ RichTextBox xuống DB
    /// Để lưu dữ liệu này xuống DB dưới DB ta phải dùng kiểu BLOB
    /// còn trên đối tượng ta dùng kiểu dữ liệu Object.
    /// </summary>
    public class HelpRichTextBox
    {
        /// <summary>
        /// Hàm thực hiện chuyển đổi 1 đối tượng thành 1 mạng byte
        /// </summary>
        /// <param name="arraybyte">tham so truyen vao co kieu BLOB trong CSDL</param>
        /// <returns></returns>
        public static byte[] ToBytes(object arraybyte)
        {
            try
            {
                byte[] arrbyte;
                arrbyte = new byte[((byte[])arraybyte).Length];
                arrbyte = (byte[])arraybyte;
                return arrbyte;
            }
            catch { }
            return null;
        }   

        /// <summary>
        /// Hàm này đặt dữ liệu được lưu dạng BLOB lấy từ hàm GetData
        /// vào RichTextBox.
        /// </summary>
        public static void SetData(object arraybyte, RichTextFormat.RichTextBox rtb)
        {
            try
            {
                rtb.Rtf = Encoding.UTF8.GetString(ToBytes(arraybyte));
            }
            catch { }
        }

        
        /// <summary>
        /// Chuyển đổi dữ liệu có trong RichTextBox thành byte gán vào 
        /// đối tượng kiểu Object để có thể lưu vào db kiểu dữ liệu là BLOB
        /// </summary>        
        public static byte[] GetData(RichTextFormat.RichTextBox rtb)
        {
            return System.Text.Encoding.UTF8.GetBytes(rtb.Rtf);
        }


        /// <summary>
        /// Imports from word.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public static bool ImportFromWord(RichTextBox Rich, String filename)
        {
            try
            {
                Microsoft.Office.Interop.Word.ApplicationClass wordApp =
                                new Microsoft.Office.Interop.Word.ApplicationClass();

                object fileName = filename;
                object objFalse = false;
                object objTrue = true;
                object missing = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(
                ref fileName, ref objFalse, ref objTrue, ref objFalse, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref objTrue, ref missing, ref missing, ref missing, ref missing);

                doc.ActiveWindow.Selection.WholeStory();
                doc.ActiveWindow.Selection.Copy();

                IDataObject data = Clipboard.GetDataObject();
                Rich.Rtf = data.GetData(DataFormats.Rtf).ToString();
                wordApp.Quit(ref missing, ref missing, ref missing);

                return true;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Microsoft Word chưa cài đặt trên máy.");
                return false;
            }
        }

        public static bool InitRichTextBox(RichTextBox rtb, string templateFile)
        {
            return ImportFromWord(rtb, templateFile);
        }

        public static RightClickWord RightClick(RichTextBox rtb)
        {
            return RightClickWord.SupportRightClickWord(rtb);
        }
    }
}
