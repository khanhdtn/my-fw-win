using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Data;
using System.IO;
using ProtocolVN.Framework.Core;
using System.Windows.Forms;
using System.Drawing;

namespace ProtocolVN.Framework.Win
{
    public class PLFormLayout
    {
        private static void LoadSizeForm(XtraForm form)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + form.Name + @".xml");
                string[] sizeForm = ds.Tables[0].Rows[0][form.Name].ToString().Split(',');
                HelpXtraForm.SetLargeSize(form, HelpNumber.ParseInt32(sizeForm[0]), HelpNumber.ParseInt32(sizeForm[1]));
                //SetLocation(form, HelpNumber.ParseInt32(sizeForm[2]), HelpNumber.ParseInt32(sizeForm[3]));
                PLFormLayout.SetCenterLocation(form);
            }
            catch { }
        }
        private static void SetCenterLocation(XtraForm form)
        {
            Size screenSize = SystemInformation.PrimaryMonitorSize;
            int x = (screenSize.Width - form.Width) / 2;
            int y = (screenSize.Height - form.Height) / 2;
            form.Location = new Point(x, y);
        }
        private static void SaveSizeForm(XtraForm form)
        {
            try
            {
                string path = FrameworkParams.LAYOUT_FOLDER + @"\" + FrameworkParams.currentUser.username + form.Name + @".xml";
                CreateFileStroreSize(path);
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                if (ds.Tables.Count == 0)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add(form.Name);
                    ds.Tables.Add(table);
                }
                if (!ds.Tables[0].Columns.Contains(form.Name))
                    ds.Tables[0].Columns.Add(form.Name);
                if (ds.Tables[0].Rows.Count == 0)
                    ds.Tables[0].Rows.Add(form.Width + "," + form.Height + "," + form.Location.X + "," + form.Location.Y);
                else
                    ds.Tables[0].Rows[0][form.Name] = form.Width + "," + form.Height + "," + form.Location.X + "," + form.Location.Y;

                ds.WriteXml(path);
            }
            catch { }
        }
        private static void CreateFileStroreSize(string filePath)
        {
            if (!File.Exists(filePath))
            {
                StreamWriter writer = new StreamWriter(filePath);
                writer.Write("<?xml version='1.0' encoding='utf-8' ?><Size></Size>");
                writer.Flush();
                writer.Close();
            }
        }

        public static void SetSaveLayout(XtraForm form)
        {
            form.Load += delegate(object sender, EventArgs e)
            {
                LoadSizeForm(form);
            };
            form.FormClosed += delegate(object sender, FormClosedEventArgs e)
            {
                SaveSizeForm(form);
            };
        }
    }
}
