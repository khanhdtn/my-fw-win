using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProtocolVN.Framework.Win
{
    public interface IDialogAction
    {
        void Action();
        string GetTitle();
    }

    public class NoAction : IDialogAction
    {
        private string Title;

        public NoAction(string Title)
        {
            this.Title = Title;
        }

        public void Action()
        {
            //NOOP
        }

        public string GetTitle()
        {
            return this.Title;
        }
    }

    public class RestartAction : IDialogAction
    {
        public void Action()
        {           
            Application.Exit();
            Process.Start(Application.ExecutablePath);          
        }

        public string GetTitle()
        {
            return "Khởi động chương trình";
        }
    }

    public class SendMailAction : IDialogAction
    {
        private string Title;

        public SendMailAction(string Title)
        {
            this.Title = Title;
        }

        public string GetTitle()
        {
            return this.Title;
        }

        public void Action()
        {            
            Process.Start("mailto:somemail[at]somedomain[dot]com");                 
        }        
    }

    public class OpenURLAction : IDialogAction
    {
        private string Title;
        private string URL;

        public OpenURLAction(string Title, string URL)
        {
            this.Title = Title;
            this.URL = URL;
        }

        public string GetTitle()
        {
            return this.Title;
        }

        public void Action()
        {              
            Process.Start(URL);            
        }      
    }
}
