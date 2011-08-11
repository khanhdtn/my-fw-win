using DevExpress.XtraEditors;
using ProtocolVN.Framework.Win;
namespace ProtocolVN.Plugin
{
    public interface IPlugin
    {
        System.Reflection.Assembly getAssembly();

        string Name { get; set; }   //Tên của plugins        
        string Description { get; set; }    //Mô tả chức năng của plugins    
        bool CheckOK();     //Kiểm tra điều kiện bắt buộc để hệ thống thực hiện
        bool Install();     //Tiến hành cài đặt nếu hệ thống không đủ điều kiện
        string BuildMenu(); //Hệ thống menu để gắn vào menu chính của ứng dụng
        bool UnInstall();   //Tiến hành uninstall khi không dùng hệ thống nữa

        bool InitPlugin();      //Sẽ được gọi khi khởi tạo menu cho Plugin
        bool DisposePlugin();   //Sẽ được gọi khi hủy Plugin

        bool HookShowForm(XtraForm frm);    // Can thiệp vào các form của ứng dụng mẹ khi mở form.
        bool HookHideForm(XtraForm frm);    // Can thiệp vào các form của ứng dụng mẹ khi đóng form.

        IPermission GetPermission();
        IFormat GetFormat();
    }
}
