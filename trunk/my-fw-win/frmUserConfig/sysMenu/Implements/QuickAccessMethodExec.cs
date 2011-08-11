using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ProtocolVN.Framework.Win
{
    public class QuickAccessMethodExec
    {
        public static void Run(string fileName)
        {
            Process pro = new Process();
            pro.StartInfo.FileName = fileName;
            pro.Start();
        }

        public void RunNotePad(){
            Run("notepad.exe");                
        }

        public void RunWordPad(){
            Run("wordpad.exe");
        }

        public void RunWord(){
            Run("winword.exe");
        }

        public void RunExcel(){
            Run("excel.exe");
        }

        public void RunCalc(){
            Run("calc.exe");
        }

        public void RunExplorer()
        {
            Run("explorer.exe");
        }
    }
}
