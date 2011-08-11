using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win.Test
{
    public class FWMethodExec
    {
        public void KhoHangHoa()
        {
            FilterCase obj = new FilterCase(1, "1", "",
                new string[] { "MA_HANG", "TEN_HANG", "TEN_NHOM", "MA_PHIEU", "NGAY_PHAT_SINH", "NGUOI_PHAT_SINH", "DOI_TAC" },
                new string[] { "Mã hàng hóa", "Tên hàng hóa", "Tên nhóm hàng", "Số phiếu", "Ngày phát sinh", "Người phát sinh", "Đối tác" },
                "select * from F_HH1");
            PLFilter frm = new PLFilter(obj);
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, frm);
        }
    }
}
