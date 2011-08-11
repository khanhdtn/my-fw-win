using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraVerticalGrid.Rows;
using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win.Database;

namespace ProtocolVN.Framework.Win.Test
{
    public class GenerateSQLMethodExec 
    {
        public void Show()
        {
            //Khai báo những DbObject cần có
            //DBObject obj = OBJ_REL.INSTANCE;
            //obj = GET_PHIEU_GOC.INSTANCE;
            //obj = FW_NGHIEP_VU_SYS.INSTANCE;


            frmGenerateSQL frm = new frmGenerateSQL();
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, frm);
        }               
    }
}
