using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Cập nhật trực tiếp phiên bản mới vào máy đang chạy
    /// </summary>
    public class frmFWLiveUpdateDirect : frmFWLiveUpdate, IPublicForm
    {
        public frmFWLiveUpdateDirect() : base()
        {
            this.labelControl1.Text = "Chọn cách cập nhật phiên bản mới";
            this.Text = "Cập nhật phiên bản mới";
        }

        protected override bool checkAllowUpdate()
        {
            this.thongBaoHoiCoCapNhatMoi = "Có phiên bản phần mềm mới nhất từ PROTOCOL Software. \nBạn có muốn cập nhật về máy tính không?";
            this.thongBaoHoiCoCapNhatCu = "Phiên bản phần mềm đang sử dụng là mới hơn phiên bản bạn muốn cập nhật. \nBạn có muốn cập nhật về máy tính không ?";

            return true;
        }
    }
}
