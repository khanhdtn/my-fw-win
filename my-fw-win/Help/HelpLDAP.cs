using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ProtocolVN.Framework.Core;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ làm việc với LDAP
    /// </summary>
    public class HelpLDAP
    {
        public static void SetProperty(DirectoryEntry de, string PropertyName, string PropertyValue)
        {
            if (PropertyValue != null)
            {
                if (de.Properties.Contains(PropertyName))
                {
                    de.Properties[PropertyName][0] = PropertyValue;
                }
                else
                {
                    de.Properties[PropertyName].Add(PropertyValue);
                }
            }
        }
        public static DirectoryEntry GetDirectoryEntry(string user, string password, string LDAPPath)
        {
            DirectoryEntry entry = null;
            try
            {
                entry = new DirectoryEntry(LDAPPath, user, password);
            }
            catch (COMException) { }
            return entry;
        }
        public static SearchResult GetSearchResult(string user, string password, string LDAPPath)
        {
            SearchResult sRsResult = null;
            DirectoryEntry entry = null;
            DirectorySearcher mySearcher = null;
            entry = GetDirectoryEntry(user, password, LDAPPath);
            if (entry != null)
            {
                try
                {
                    mySearcher = new DirectorySearcher(entry);
                }
                catch (COMException) { };
                if (mySearcher != null)
                {
                    try
                    {
                        string strFilter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + user + "))";
                        mySearcher.Filter = strFilter;
                        sRsResult = mySearcher.FindOne();
                    }
                    catch (COMException) { };
                }
                else { };
            }
            else { };
            return sRsResult;
        }
        public static string GetInfo(string user, string pass, string pathLDAP, string attribute)
        {
            SearchResult result = null;
            string strResult = "";
            string path = pathLDAP;
            try
            {
                result = GetSearchResult(user, pass, pathLDAP);
                if (result != null) strResult += result.GetDirectoryEntry().Properties[attribute].Value;
                else { };

            }
            catch (COMException) { };
            return strResult;
        }

        #region Kiểm tra Users
        public static bool Verify(string user, string password, string pathLDAP)
        {
            SearchResult result = GetSearchResult(user, password, pathLDAP);
            if (result != null)
                return true;
            return false;
        }
        #region Thay đổi Password của Users: chưa chạy 
        //public static bool ChangePassword(string user, string pass, string passNew)
        //{
        //    bool blResult = false;
        //    DirectoryEntry objLoginEntry = null;
        //    objLoginEntry = GetDirectoryEntry(user, pass);
        //    if (objLoginEntry != null)
        //    {
        //        try
        //        {
        //            objLoginEntry.Invoke("ChangePassword", new object[] { pass, passNew });
        //            objLoginEntry.CommitChanges();
        //            blResult = true;
        //        }
        //        catch (COMException ex)
        //        {
        //            MessageBox.Show("Loi: " + ex.Message.ToString());
        //        }
        //    }
        //    return blResult;
        //}
        //public static bool ChangePassword1(string user, string pass, string passNew)
        //{
        //    bool blResult = false;
        //    DirectoryEntry objLoginEntry = null;
        //    objLoginEntry = GetDirectoryEntry(user, pass);

        //    if (objLoginEntry != null)
        //    {
        //        try
        //        {
        //            //objLoginEntry.Properties[].Value = passNew;
        //            //object ob = objLoginEntry.NativeObject;
        //            objLoginEntry.CommitChanges();
        //        }
        //        catch (COMException ex)
        //        {
        //            MessageBox.Show("Loi: " + ex.Message.ToString());
        //        }
        //    }
        //    return blResult;
        //}
        #endregion
        #endregion
    }
}
