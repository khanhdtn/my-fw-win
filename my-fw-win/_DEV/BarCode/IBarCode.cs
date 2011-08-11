using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    public interface IBarCode
    {        
        DataSet GetDataSource();
        Dictionary<string, string> GetFieldMap();
    }

    public interface IInputBarcode
    {
        /// <summary>Phương thức được gọi khi Mã vạch hợp lệ.        
        /// Thông tin mã vạch được lấy thông qua data._MaVach và 
        /// số lượng là data._So.
        /// </summary>
        bool chonSanPham(frmInputBarcode data);
        
        /// <summary>Cập nhật tên sản phẩm khi mã vạch hợp lệ.
        /// Thông tin mã vạch được lấy thông qua data._MaVach
        /// Thông tin tên sản phẩm được lấy thông qua data._MoTa
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool capNhatTenSanPham(frmInputBarcode data);
        
        /// <summary>Phương thức kiểm tra xem barcode đó có hợp lệ hay không.
        /// Nếu hợp lệ trả về giá trị != ""
        /// Nếu không hợp lệ trả về giá trị là = ""
        /// </summary>
        String kiemTraMaVach(String barCode);
    }
}
