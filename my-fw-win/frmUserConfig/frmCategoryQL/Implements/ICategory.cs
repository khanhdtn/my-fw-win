
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
namespace ProtocolVN.Framework.Win
{
    public enum GroupElementType
    {
        /// <summary>
        /// Chỉ chọn từ danh sách
        /// </summary>
        ONLY_CHOICE,
        /// <summary>
        /// Chọn và thêm vào danh sách
        /// </summary>
        CHOICE_N_ADD,   
        /// <summary>
        /// Chỉ cho phép thêm vào danh sách
        /// </summary>
        ONLY_INPUT      
    }
    //Xây dựng 1 danh mục mà chỉ cần truyền vào control không truyền vào các nút hành động thì cài đặt interface này
    public interface IPluginCategory : ICategory, IProtocolForm
    {
        /// <summary>Xử lý khi chọn Add
        /// </summary>
        object AddAction(object param);
        /// <summary>Xử lý khi chọn Add Child
        /// </summary>
        object AddChildAction(object param);
        /// <summary>Xử lý khi chọn Sửa
        /// </summary>
        object EditAction(object param);
        /// <summary>Xử lý khi chọn Xóa
        /// </summary>
        object DeleteAction(object param);
        /// <summary>Xử lý khi chọn In. Hiện tại chưa sử dụng
        /// </summary>
        object PrintAction(object param);
        /// <summary>Xử lý khi chọn Lưu. Hiện tại chưa sử dụng
        /// </summary>
        object SaveAction(object param);
        /// <summary>Xử lý khi chọn Không Lưu. Hiện tại chưa sử dụng
        /// </summary>
        object NoSaveAction(object param);
    }

    /// <summary>Cài đặt danh mục mà toàn bộ cài đặt nằm trong control này
    /// </summary>
    public interface IActionCategory : ICategory
    {
        ToolStrip GetMenuAction();
       
    }

    /// <summary>Cài đặt danh mục dạng nhúng control hiển thị thông tin vào frmCategory
    /// </summary>
    public interface ICategory
    {
        void SetOwner(IFormCategory owner);
        void UpdateGUI();
        GridView GetGridView();
    }
    
    /// <summary>Cài đặt danh mục dạng form riêng không nằm trong hệ thống danh mục
    /// </summary>
    public interface IFormCategory
    {
        ToolStripButton GetRemoveBtn();
        ToolStripButton GetAddBtn();
        ToolStripButton GetEditBtn();
        ToolStripButton GetSaveBtn();
        ToolStripButton GetNoSaveBtn();
        ToolStripDropDownButton GetPrintBtn();
        ToolStripDropDownButton GetExportBtn();
    }

    public interface ICatFunc
    {
        object init(params object[] inputs);
    }
    
    /// <summary>Hỗ trợ cài đặt chức năng Import cho danh mục
    /// Chèn đoạn code vào sau hàm InitDanhMuc
    ///     object o = pl.Tag;
    ///     TagPropertyMan.InsertOrUpdate(ref o, "frmCategory_IMPORT", DM_DoiTac.I);
    ///     pl.Tag = o;
    /// DM_XXX phải thừa kế ICatFuncImport và cài đặt code tựa như trên
    /// Demo trong lớp DM_DOI_TAC
    /// </summary>
    public interface ICatFuncImport : ICatFunc { }
}
