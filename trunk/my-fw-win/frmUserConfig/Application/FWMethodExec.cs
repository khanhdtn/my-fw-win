using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using ProtocolVN.DanhMuc;
using DevExpress.XtraVerticalGrid.Rows;

namespace ProtocolVN.Framework.Win
{
    public class FWMethodExec
    {
        #region frmAppParams
        public void ShowAppParamForm()
        {
            frmAppParams frm = new frmAppParams(CreateVGrid_Basic, GetRuleVGrid_Basic);
            ProtocolForm.ShowModalDialog(FrameworkParams.MainForm, frm, false);
        }

        public static EditorRow[] CreateVGrid_Basic()
        {
            EditorRow[] Rows = HelpEditorRow.CreateEditorRow(
                new string[]{   "VAT"
                            },
                new bool[]  {   true                                
                            },
                new int[]   {   20                                
                            });
            HelpEditorRow.DongSpinEdit(Rows[0], "VAT", 1);

            return Rows;
        }
        public static FieldNameCheck[] GetRuleVGrid_Basic(object param)
        {
            ////PHUOCNC: Không nhất thiết là ghi tất cả
            //return new FieldNameCheck[] { 
            //            new FieldNameCheck("MA_HDBHKH",
            //                new CheckType[]{ CheckType.Required , CheckType.RequireMaxLength },
            //                "Mã phiếu hóa đơn tài chính", 
            //                new object[]{ null, 100 })
            //};

            return null;
        }
        #endregion

        #region frmCategory
        public void ShowSystemCategory(){
            string xml =
            @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>
                <basiccats>
                  <group id ='1'>
                    <lang id='vn'>Sơ đồ tổ chức</lang>
                    <lang id='en'></lang>
                    " + DMFWNhanVien.I.Item() + @" 
                    " + DMFWPhongBan.I.Item() + @"
                  </group>
                </basiccats>
            ";
            frmCategory frm = new frmCategory(xml);
            ProtocolForm.ShowWindow(FrameworkParams.MainForm, frm, false);
        }
        #endregion
    }
}
