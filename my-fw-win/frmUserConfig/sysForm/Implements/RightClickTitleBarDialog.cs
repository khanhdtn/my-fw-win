using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public class RightClickTitleBarDialog
    {
        private SystemMenu m_SystemMenu = null;
        private Form form;
        // ID's of the entries
        private const int m_FormInfo = 0x100;
        private const int m_RefreshForm = 0x101;
        private const int m_FURL = 0x102;

        public const string MENU_TITLE_FORM_INFO_TEXT = "Thông tin màn hình";
        public const string MENU_TITLE_FORM_REFRESH_TEXT = "Lấy về thông tin mới";
        public const string MENU_TITLE_FORM_FURL_TEXT = "Lấy về thông tin mới";

        public RightClickTitleBarDialog(Form frm)
        {
            this.form = frm;
            try
            {
                m_SystemMenu = SystemMenu.FromForm(frm);

                m_SystemMenu.AppendSeparator();

                if (FrameworkParams.isSupportDeveloper)
                    m_SystemMenu.AppendMenu(m_FormInfo, MENU_TITLE_FORM_INFO_TEXT);
                if (this is IFormRefresh)
                {
                    m_SystemMenu.AppendMenu(m_RefreshForm, MENU_TITLE_FORM_REFRESH_TEXT);
                }
                if (this is IFormFURL)
                {
                    m_SystemMenu.AppendMenu(m_FURL, MENU_TITLE_FORM_FURL_TEXT);
                }

                m_SystemMenu.AppendSeparator();
            }
            catch (NoSystemMenuException /* err */ )
            {
                // Do some error handling
            }
        }

        public void execMenuItem(ref Message msg)
        {
            if (msg.Msg == (int)WindowMessages.wmSysCommand)
            {
                switch (msg.WParam.ToInt32())
                {
                    case m_FormInfo:
                        {
                            RightClickTitleBarHelper.showFormInfo(this.form);
                            break;
                        }

                    case m_RefreshForm:
                        {
                            RightClickTitleBarHelper.refreshForm((IFormRefresh)this.form);
                            break;
                        }
                    case m_FURL:
                        {
                            RightClickTitleBarHelper.showFURL((IFormFURL)this.form);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

    }
}
