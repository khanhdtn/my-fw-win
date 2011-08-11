using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Đặc tả xử lý hiển thị hộp thoại trong hệ thống warning
    /// </summary>
    public interface PLOut
    {
        /// <summary>
        /// Mở thông báo Notify lên.
        /// </summary>
        object open(object param);
        /// <summary>
        /// Ghi nội dung vào họp thông báo
        /// </summary>
        object write(string title, string text);
        /// <summary>
        /// Đóng hộp thông báo
        /// </summary>
        object close(object param);
    }
}
