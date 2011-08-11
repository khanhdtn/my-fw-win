using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{
    public interface PLCPhieu
    {
        //Validate
        FieldNameCheck[] GetRule(Object DAModPhieu);

        [Obsolete("Không khuyên dùng. Nên trả về NULL")]
        List<Control> GetFormatControls(XtraForm formModPhieu);

        //Phan quyen
        List<object> GetObjectItems(XtraForm formModPhieu);

        //In phiếu
        _Print GetPrintObj(XtraForm frmMain, long[] IDs);

        PhieuType GetPhieuType();
    }
}
