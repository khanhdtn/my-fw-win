using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    /// <summary>Chỉ định các CTRL sẽ phải định dạng. Và hệ thống sẽ định dạng tự động dựa vào 
    /// dạng của CTRL.
    /// </summary>
    public interface IFormatable
    {
        //Chỉ ra danh sách các control cần định dạng.
        //Hệ thống sẽ tự động tìm đến các control để tiến hình định dạng
        List<Control> GetFormatControls();
    }

    /// <summary>Chỉ định các CTRL cần được phân quyền và định dạng 
    /// </summary>
    public interface IProtocolForm : IPermisionable, IFormatable
    {
    }

    /// <summary>Xử lý nạp lại dữ liệu.
    /// </summary>
    public interface IFormRefresh
    {
        object _RefreshAction(params object[] input);
    }


    /// <summary>Xử lý lấy địa chỉ FURL.
    /// </summary>
    public interface IFormFURL
    {
        object _FURLAction(params object[] input);
    }

    /// <summary>Chỉ định Form có kích thước cố định
    /// 
    /// </summary>
    public interface IFormFixSize
    {

    }
}
