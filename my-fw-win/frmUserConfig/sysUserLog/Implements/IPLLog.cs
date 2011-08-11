using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Khai báo Form ở dạng không cần phải LOG vào trong nhật ký sử dụng
    /// </summary>
    public interface INonLog
    {
    }

    /// <summary>Chỉ định các vấn đề cần LOG trên tất cả các FORM trong ứng dụng
    /// </summary>
    public interface IPLLog
    {
        List<LogItem> Log(Object main);
    }

    public class LogItem
    {
        public object Item;
        public string ContentLog;

        public LogItem(object Item, string ContentLog)
        {
            this.Item = Item;
            this.ContentLog = ContentLog;
        }
    }
}