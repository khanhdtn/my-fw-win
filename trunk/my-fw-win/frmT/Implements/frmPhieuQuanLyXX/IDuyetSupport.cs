using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public interface IDuyetSupport
    {
        /// <summary>Cài đặt chức năng Không duyệt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool KhongDuyetAction(long ID, long NguoiDuyetID, DateTime NgayDuyet);
        /// <summary>Duyệt qua phần tử có ID là ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="NguoiDuyetID"></param>
        /// <param name="NgayDuyet"></param>
        /// <returns></returns>
        bool DuyetAction(long ID, long NguoiDuyetID, DateTime NgayDuyet);        
    }
}
