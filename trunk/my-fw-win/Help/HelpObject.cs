using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Reflection;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// Hỗ trợ làm việc với HelpObject
    /// </summary>
    public class HelpObject : GenerateClass
    {
        public static XtraForm CreateXtraFormInstance(String FormName)
        {
            try
            {
                Object a = GenerateClass.initObject(FormName);
                return (XtraForm)a;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp form tên " + FormName + " không thành công.");
                return null;
            }
        }
        public static XtraForm CreateXtraFormInstance(String FormName, Int64 ID)
        {
            try
            {
                Object a = GenerateClass.initObject(FormName, ID);
                return (XtraForm)a;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp form tên " + FormName + " không thành công.");
                return null;
            }
        }
        public static XtraForm CreateXtraFormInstance(String FormName, object ID)
        {
            try
            {
                Object a = GenerateClass.initObject(FormName, ID);
                return (XtraForm)a;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp form tên " + FormName + " không thành công.");
                return null;
            }
        }        
        public static XtraForm CreateXtraFormInstance(String FormName, List<Object> InitParams)
        {
            try
            {
                Object form = GenerateClass.initObject(FormName, InitParams);
                return (XtraForm)form;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp form tên " + FormName + " không thành công.");
                return null;
            }
        }
        public static XtraForm CreateXtraFormInstance(String FormName, params object[] InitParams)
        {
            try
            {
                Object form = GenerateClass.initObject(FormName, InitParams);
                return (XtraForm)form;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp form tên " + FormName + " không thành công.");
                return null;
            }
        }

        public static Object CreateInstance(String ClassName)
        {
            try
            {
                Object obj = GenerateClass.initObject(ClassName);
                return obj;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp một lớp " + ClassName + "không thành công.");
                return null;
            }
        }
        public static Object CreateInstance(String ClassName, Int64 ID)
        {
            try
            {
                Object obj = GenerateClass.initObject(ClassName, ID);
                return obj;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp một lớp " + ClassName + "không thành công.");
                return null;
            }
        }
        public static Object CreateInstance(String ClassName, object ID)
        {
            try
            {
                Object obj = GenerateClass.initObject(ClassName, ID);
                return obj;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp một lớp " + ClassName + "không thành công.");
                return null;
            }
        }
        public static Object CreateInstance(String ClassName, List<Object> initParams)
        {
            try
            {
                Object obj = GenerateClass.initObject(ClassName, initParams);
                return obj;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp một lớp "+ ClassName+"không thành công.");
                return null;
            }
        }
        public static Object CreateInstance(String ClassName, params object[] initParams)
        {
            try
            {
                Object obj = GenerateClass.initObject(ClassName, initParams);
                return obj;
            }
            catch
            {
                PLMessageBoxDev.ShowMessage("Nạp một lớp " + ClassName + "không thành công.");
                return null;
            }
        }
        

        public static Object GetProperty(Object Obj, String PropertyName)
        {
            try
            {
                PropertyInfo prop = Obj.GetType().GetProperty(PropertyName);
                return prop.GetValue(Obj, null);
            }
            catch
            {
                PLMessageBoxDev.ShowMessage(Obj, "Thuộc tính " + PropertyName + " không tồn tại");
                return null;
            }
        }
        public static void SetProperty(Object Obj, String PropertyName, String Value)
        {
            try
            {
                PropertyInfo prop = Obj.GetType().GetProperty(PropertyName);
                prop.SetValue(Obj, Value, null);
            }
            catch
            {
                PLMessageBoxDev.ShowMessage(Obj, "Thuộc tính " + PropertyName + " không tồn tại");
            }
        }
        
        #region PHUOCNT NC Giải quyết vụ Unplug plugin và xoá DLL
        //HUYNC:Hàm Load một assembly và trả về kiểu object, object sẽ được ép về kiểu interface mà assembly được load lên
        //Khi unload assembly ta chỉ việc delete nó trong dictionary mà ta lưu trữ các assembly.
        //Muốn xóa hẳn thì ta delete file của assembly đó đi.
        private Object LoadAssembly(string path, string typeName)
        {
            byte[] fileContent;
            using (System.IO.FileStream dll = System.IO.File.OpenRead(path))
            {
                fileContent = new byte[dll.Length];
                dll.Read(fileContent, 0, (int)dll.Length);
            }
            AppDomain appDomain = AppDomain.CurrentDomain;
            System.Reflection.Assembly assembly = appDomain.Load(fileContent);
            Type type = assembly.GetType(typeName);
            return Activator.CreateInstance(type);
        }
        #endregion
    }
}
