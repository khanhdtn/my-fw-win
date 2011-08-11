using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class HelpNghiepVu
    {
        //Còn vấn đề phân quyền
        public static List<Object> InitNghiepVu(ContextMenuStrip mnuNghiepVu, PhieuType PhieuFrom, object DOData)
        {
            List<Object> list = new List<Object>();

            List<PhieuType> Phieus = PhieuFrom.CanCreateList;
            ToolStripMenuItem[] Items = new ToolStripMenuItem[Phieus.Count];
            for (int i = 0; i < Phieus.Count; i++)
            {
                PhieuType PhieuTo = Phieus[i];
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = PhieuTo.GetDOName();
                //this.item.Size = new System.Drawing.Size(286, 22);
                item.Text = "Tạo " + PhieuTo.GetTitle();
                item.Click += delegate(object sender, EventArgs e)
                {
                    ProtocolForm.ShowModalForm((XtraForm)mnuNghiepVu.FindForm(), PhieuTo.GetFormClassName(), DOData);
                };                
                Items[i] = item;
                ApplyPermissionAction.ApplyPermissionObject(list, Items[i], PhieuTo.AllowAdd);
                //list.Add(PhieuTo.AllowAdd);
            }
            
            if (Items.Length == 0)
                mnuNghiepVu.Visible = false;
            else
                mnuNghiepVu.Items.AddRange(Items);

            return list;
        }

        public static _MenuItem GetBusinessMenuList(XtraForm FormQL, PhieuType Phieu, DelegationLib.CallFunction_MulIn_SinOut DOData)
        {
            List<PhieuType> Phieus = Phieu.CanCreateList;
            DelegationLib.CallFunction_MulIn_NoOut[] Actions = new DelegationLib.CallFunction_MulIn_NoOut[Phieus.Count];
            string[] Titles = new string[Phieus.Count];
            string[] ImageNames = new string[Phieus.Count];
            PermissionItem[] Permissions = new PermissionItem[Phieus.Count];
            for (int i = 0; i < Phieus.Count; i++)
            {
                PhieuType PhieuTo = Phieus[i];
                Titles[i] = "Tạo " + PhieuTo.GetTitle();
                ImageNames[i] = PhieuTo.GetImageName();
                Permissions[i] = PhieuTo.AllowAdd;
                Actions[i] = delegate(List<object> ids)
                {
                    if (ids != null && ids.Count > 0)
                    {
                        ProtocolForm.ShowModalForm(FormQL, PhieuTo.GetFormClassName(), DOData(ids));
                    }
                };
            }
            return new _MenuItem(Titles, ImageNames, Phieu.GetIDField(), Actions, Permissions);
        }
    }
}
