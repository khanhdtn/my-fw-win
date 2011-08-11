using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Data.Common;
using ProtocolVN.Framework.Core;
using System.Data;

namespace ProtocolVN.Plugin.WarningSystem
{
    public class WarningSupport
    {
        static string WarningFolder = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + "\\warningsystem";
        public static List<IWarning> LoadAllWarning()
        {            
            string[] strArray = Directory.GetFiles(WarningFolder, "*.dll", SearchOption.AllDirectories);
            List<IWarning> list = new List<IWarning>();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < strArray.Length; i++)
            {
                string str2 = strArray[i].Substring(strArray[i].LastIndexOf("\\") + 1, (strArray[i].IndexOf(".dll")) - strArray[i].LastIndexOf("\\") - 1);
                Type type = null;
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.LoadFile(strArray[i]);
                    if (assembly != null)
                    {
                        type = assembly.GetType(str2 + ".Warning");
                    }
                }
                catch { }
                try
                {
                    if ((type != null) && (list2.IndexOf(type.ToString()) == -1))
                    {
                        list2.Add(type.ToString());
                        list.Add((IWarning)Activator.CreateInstance(type));
                    }
                }
                catch { }
            }
            return list;
        }

        public static List<IWarningDefine> LoadWarning(params string[] names)
        {
            List<IWarningDefine> lstWarning = new List<IWarningDefine>();
            if (names.Length > 0)
            {
                List<IWarning> lstAssWarning = LoadAllWarning();
                foreach (IWarning warning in lstAssWarning)
                {
                    foreach (IWarningDefine warningdefine in warning.GetWarning())
                    {
                        if(CheckName(warningdefine,names))
                            lstWarning.Add(warningdefine);
                    }
                }
            }
            return lstWarning;
        }
        private static bool CheckName(IWarningDefine warDefine,params string[] names)
        {
            foreach (string name in names)
                if (warDefine.Name.Equals(name))
                    return true;
            return false;
        }

        public static string[] GetNameWarningFromDB()
        {
            DataSet ds = DABase.getDatabase().LoadDataSet("SELECT NAME FROM FW_WARNINGINFO WHERE USERID=" + FrameworkParams.currentUser.id + " AND STATE=1 AND FORMNAME is NULL");
            string[] names = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                names.SetValue(dr["NAME"].ToString(),i);
            }
            return names;
            
        }

        public static string[] GetNameWarningFromDB(string frmName)
        {
            DataSet ds = DABase.getDatabase().LoadDataSet("SELECT NAME FROM FW_WARNINGINFO WHERE USERID=" + FrameworkParams.currentUser.id + " AND STATE=1 AND FORMNAME='" + frmName + "'");
            string[] names = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                names.SetValue(dr["NAME"].ToString(), i);
            }
            return names;
        }
        //Kích hoạt warning
        public static void StartWarning()
        {
            List<IWarningDefine> lstWarning = null;
            try
            {
                string[] names = WarningSupport.GetNameWarningFromDB();
                lstWarning = WarningSupport.LoadWarning(names);
                
                foreach (IWarningDefine define in lstWarning)
                {
                    QueryBuilder builder = new QueryBuilder("SELECT PARAM FROM fw_warning_param wp,fw_warninginfo wi WHERE wp.war_id = wi.id and 1=1");
                    builder.add("wi.name", Operator.Equal, define.Name, DbType.String);
                    DataSet dsParam = DABase.getDatabase().LoadDataSet(builder);
                    if (dsParam.Tables.Count > 0 && dsParam.Tables[0].Rows.Count > 0)
                    {
                        string lstParam = dsParam.Tables[0].Rows[0][0].ToString();
                        string[] param = lstParam.Split(';');
                        List<Object> list = new List<object>();
                        foreach (object p in param)
                            list.Add(p);
                        define.SetParams(list);
                    }
                }
                WarningSystemPluginEx warning = new WarningSystemPluginEx(lstWarning);

                warning.Start();
            }
            catch (Exception ex) { PLException.AddException(ex); }
        }
        //Load tat ca Warning thuoc form name
        public static WarningSystemPluginEx StartWarning(string frmName)
        {
            List<IWarningDefine> lstWarning = null;
            WarningSystemPluginEx warning = null;
            try
            {
                if (frmName!="")
                {
                    string[] names = WarningSupport.GetNameWarningFromDB(frmName);
                    lstWarning = WarningSupport.LoadWarning(names);

                    foreach (IWarningDefine define in lstWarning)
                    {
                        QueryBuilder builder = new QueryBuilder("SELECT PARAM FROM fw_warning_param wp,fw_warninginfo wi WHERE wp.war_id = wi.id and 1=1");
                        builder.add("wi.name", Operator.Equal, define.Name, DbType.String);
                        DataSet dsParam = DABase.getDatabase().LoadDataSet(builder);
                        if (dsParam.Tables.Count > 0 && dsParam.Tables[0].Rows.Count > 0)
                        {
                            string lstParam = dsParam.Tables[0].Rows[0][0].ToString();
                            string[] param = lstParam.Split(';');
                            List<Object> list = new List<object>();
                            foreach (object p in param)
                                list.Add(p);
                            define.SetParams(list);
                        }
                    }
                    warning = new WarningSystemPluginEx(lstWarning);
                    warning.Start();
                }
            }
            catch (Exception ex) { PLException.AddException(ex); }
            return warning;
        }

        public static Dictionary<string,IWarningDefine> GetWarning()
        {
            string[] names = WarningSupport.GetNameWarningFromDB();
            List<IWarningDefine> lstWarning = WarningSupport.LoadWarning(names);
            return AddWarToDic(lstWarning);
        }

        private static Dictionary<string, IWarningDefine> AddWarToDic(List<IWarningDefine> warnings)
        {
            Dictionary<string, IWarningDefine> dicWar = null;
            if (warnings != null)
            {
                dicWar = new Dictionary<string, IWarningDefine>();
                foreach (IWarningDefine war in warnings)
                    dicWar.Add(war.Name, war);
            }
            return dicWar;
        }
    }
}
