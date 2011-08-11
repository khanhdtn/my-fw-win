using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Ribbon;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;

namespace ProtocolVN.Framework.Win
{
    public class RibbonStatusOut : PLOut
    {
        private RibbonStatusBar ribStaBar;
        private ToolTipItem toolTip;
        private BarStaticItem staticItem = null;

        #region PLOut Members
        private static RibbonStatusOut status;
        public static RibbonStatusOut Instance()
        {
            if (status == null)
            {
                status = new RibbonStatusOut((RibbonForm)FrameworkParams.MainForm);
            }
            return status;
        }
        public static void Dispose()
        {
            if (status != null) status.close(null);
        }

        private RibbonStatusOut(RibbonForm form)
        {
            ribStaBar = form.StatusBar;
        }

        public object open(object param)
        {
            if (staticItem == null)
            {
                staticItem = new BarStaticItem();
                staticItem.Glyph = FWImageDic.WARN_IMAGE16;
                toolTip = new ToolTipItem();
                SuperToolTip superToltip = new SuperToolTip();
                superToltip.Items.Add(toolTip);
                staticItem.SuperTip = superToltip;
                ribStaBar.ItemLinks.Add(staticItem);
                staticItem.Alignment = BarItemLinkAlignment.Right;
            }
            else
            {
                //PHUOCNC Index = 3
                ribStaBar.ItemLinks[3].Visible = true;
            }
            return "NOTHING";
        }

        public object write(string title, string text)
        {
            open(null);
            toolTip.Text = text;
            return "NOTHING";
        }

        public object close(object param)
        {
            //PHUOCNC Index = 3
            if (ribStaBar.ItemLinks.Count == 4)//Đã tạo Item
            {
                ribStaBar.ItemLinks[3].Visible = false;
            }
            return "NOTHING";
        }

        #endregion
    }
}
