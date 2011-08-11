using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using ProtocolVN.Plugin;
using DevExpress.XtraEditors;
using ProtocolVN.Framework.Core;
using System.Collections;
using System.IO;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    public class PLPlugin
    {
        public static string PLUGIN_FOLDER = Application.StartupPath + @"\plugins";
        public static string PLUGIN_CONF_FILE = Application.StartupPath + @"\conf\plugins.cpl";

        public static List<IPlugin> plugins = new List<IPlugin>();
        public static Dictionary<string, Assembly> lstAssembly = new Dictionary<string, Assembly>();

        public static bool ExistPlugin(string PluginName)
        {
            for (int i = 0; i < plugins.Count; i++)
            {
                if (plugins[i].Name == PluginName)
                {
                    return true;
                }
            }
            return false;
        }
        public static IPlugin GetPlugin(string PluginName)
        {
            for (int i = 0; i < plugins.Count; i++)
            {
                if (plugins[i].Name == PluginName)
                {
                    return plugins[i];
                }
            }
            return null;
        }
        public static void HookShowAllPlugin(XtraForm form)
        {
            if (PLPlugin.plugins == null) return;
            for (int i = 0; i < PLPlugin.plugins.Count; i++)
            {
                try
                {
                    PLPlugin.plugins[i].HookShowForm(form);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
        }
        public static void HookHideAllPlugin(XtraForm form)
        {
            if (PLPlugin.plugins == null) return;
            for (int i = 0; i < PLPlugin.plugins.Count; i++)
            {
                try
                {
                    PLPlugin.plugins[i].HookHideForm(form);
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
        }

        #region Không thấy dùng
        public static string GetStartupPath(string PluginName)
        {
            for (int i = 0; i < plugins.Count; i++)
            {
                if (plugins[i].Name == PluginName)
                {
                    string path = plugins[i].getAssembly().Location;
                    return path.Substring(0, path.LastIndexOf('\\'));
                }
            }
            return "";
        }
        #endregion
    }

    public class HelpPlugin
    {
        public static void AssembluResolve()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly ass;
            PLPlugin.lstAssembly.TryGetValue(args.Name, out ass);
            return ass;
        }

        public static bool DisposeMenuPlugin()
        {
            bool flag = true;
            if (PLPlugin.plugins != null && PLPlugin.plugins.Count > 0)
            {
                foreach (IPlugin i in PLPlugin.plugins)
                {
                    try
                    {
                        i.DisposePlugin();
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                        PLException.AddException(ex);
                    }
                }
            }
            return flag;
        }

        public static string CreateMenuPlugin()
        {
            //Nạp Plugin 
            List<IPlugin> ipi = LoadConfPlugins();
            //Dựng Menu Plugin
            string item = "";
            if (ipi != null)
            {
                foreach (IPlugin i in ipi)
                {
                    try
                    {
                        item += i.BuildMenu();
                        i.InitPlugin();
                    }
                    catch (Exception ex) { PLException.AddException(ex); }
                }
                PLPlugin.plugins = ipi;
            }
            return item;
        }

        public static List<IPlugin> LoadNotConfPlugins()
        {
            List<IPlugin> iplugin = null;
            string[] namePlugins = GetNamePlugin();
            if (namePlugins != null)
                iplugin = HelpPlugin.LoadPluginNotName(namePlugins);
            else
                iplugin = HelpPlugin.LoadAllPlugin();
            return iplugin;
        }

        public static List<IPlugin> LoadConfPlugins()
        {
            List<IPlugin> iplugin = null;
            string[] namePlugins = GetNamePlugin();
            if (namePlugins != null)
                iplugin = HelpPlugin.LoadPlugin(namePlugins);
            return iplugin;
        }

        public static List<IPlugin> LoadPlugin(string[] name)
        {
            //string path = Application.StartupPath + @"\plugins";
            string path = PLPlugin.PLUGIN_FOLDER;
            string[] pluginFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            IPlugin[] ipi = new IPlugin[pluginFiles.Length];
            List<IPlugin> getPlugin = new List<IPlugin>();
            ArrayList arrLst = new ArrayList();
            for (int i = 0; i < pluginFiles.Length; i++)
            {
                string args = pluginFiles[i].Substring(
                    pluginFiles[i].LastIndexOf("\\") + 1,
                    pluginFiles[i].IndexOf(".dll") -
                    pluginFiles[i].LastIndexOf("\\") - 1);
                Type ObjType = null;
                try
                {
                    Assembly ass = null;
                    //ass = Assembly.LoadFile(pluginFiles[i]);
                    ass = PLLoadAssembly(pluginFiles[i]);
                    if (!PLPlugin.lstAssembly.ContainsKey(ass.FullName))
                        PLPlugin.lstAssembly.Add(ass.FullName, ass);
                    if (ass != null)
                    {
                        ObjType = ass.GetType(args + ".PlugIn");
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
                try
                {
                    if (ObjType != null)
                    {
                        ipi[i] = (IPlugin)Activator.CreateInstance(ObjType);
                        foreach (string n in name)
                        {
                            if (ipi[i].Name == n && arrLst.IndexOf(ObjType.ToString()) == -1)
                            {
                                arrLst.Add(ObjType.ToString());
                                getPlugin.Add(ipi[i]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            return getPlugin;
        }

        public static List<IPlugin> LoadPluginNotName(string[] name)
        {
            //string path = Application.StartupPath + @"\plugins";
            string path = PLPlugin.PLUGIN_FOLDER;
            string[] pluginFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            IPlugin[] ipi = new IPlugin[pluginFiles.Length];
            List<IPlugin> getPlugin = new List<IPlugin>();
            ArrayList arrLst = new ArrayList();

            for (int i = 0; i < pluginFiles.Length; i++)
            {
                bool isExist = false;
                string args = pluginFiles[i].Substring(
                    pluginFiles[i].LastIndexOf("\\") + 1,
                    pluginFiles[i].IndexOf(".dll") -
                    pluginFiles[i].LastIndexOf("\\") - 1);
                Type ObjType = null;
                try
                {
                    Assembly ass = null;
                    ass = PLLoadAssembly(pluginFiles[i]);
                    //ass = Assembly.LoadFile(pluginFiles[i]);
                    if (ass != null) ObjType = ass.GetType(args + ".PlugIn");
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
                try
                {
                    if (ObjType != null)
                    {
                        ipi[i] = (IPlugin)Activator.CreateInstance(ObjType);
                        foreach (string n in name)
                        {
                            if (ipi[i].Name.Equals(n))
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist && arrLst.IndexOf(ObjType.ToString()) == -1)
                        {
                            getPlugin.Add(ipi[i]);
                            arrLst.Add(ObjType.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            return getPlugin;
        }

        public static List<IPlugin> LoadAllPlugin()
        {
            //string path = Application.StartupPath + @"\plugins";
            string path = PLPlugin.PLUGIN_FOLDER;
            string[] pluginFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            List<IPlugin> ipi = new List<IPlugin>();
            ArrayList arrlst = new ArrayList();

            for (int i = 0; i < pluginFiles.Length; i++)
            {
                string args = pluginFiles[i].Substring(
                    pluginFiles[i].LastIndexOf("\\") + 1,
                    pluginFiles[i].IndexOf(".dll") -
                    pluginFiles[i].LastIndexOf("\\") - 1);

                Type ObjType = null;
                Assembly ass = null;
                try
                {
                    //ass = Assembly.LoadFile(pluginFiles[i]);
                    ass = PLLoadAssembly(pluginFiles[i]);
                    if (ass != null) ObjType = ass.GetType(args + ".PlugIn");
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
                try
                {
                    if (ObjType != null && arrlst.IndexOf(ObjType.ToString()) == -1)
                    {
                        arrlst.Add(ObjType.ToString());
                        ipi.Add((IPlugin)Activator.CreateInstance(ObjType));
                    }
                }
                catch (Exception ex)
                {
                    PLException.AddException(ex);
                }
            }
            return ipi;
        }

        private static string[] GetNamePlugin()
        {
            string[] namePlugin = null;
            try
            {
                if (File.Exists(PLPlugin.PLUGIN_CONF_FILE) == true)
                {
                    DataSet ds = new DataSet();
                    ConfigFile.ReadXML(PLPlugin.PLUGIN_CONF_FILE, ds);
                    namePlugin = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        namePlugin[i] = dr["NAME"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                PLException.AddException(ex);
            }
            return namePlugin;
        }

        //HUYNC:Hàm Load một assembly và trả về kiểu object, object sẽ được ép về kiểu interface mà assembly được load lên
        //Khi unload assembly ta chỉ việc delete nó trong dictionary mà ta lưu trữ các assembly.
        //Muốn xóa hẳn thì ta delete file của assembly đó đi.
        public static Assembly PLLoadAssembly(string path)
        {
            //// Load dữ liệu nhị phân
            //byte[] fileContent;
            //using (System.IO.FileStream dll = System.IO.File.OpenRead(path))
            //{
            //    fileContent = new byte[dll.Length];
            //    dll.Read(fileContent, 0, (int)dll.Length);
            //}
            //AppDomain appDomain = AppDomain.CurrentDomain;
            //System.Reflection.Assembly assembly = appDomain.Load(fileContent);
            //return assembly;

            return Assembly.LoadFile(path);
        }
    }
}
