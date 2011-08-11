using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    /// <summary>
    /// FWPLDataType Hỗ trợ là
    /// 1: Text -> Kiểu chuỗi
    /// 2: Number -> Kiểu số thực
    /// 3: Date -> Kiểu chỉ ngày.
    /// 4: Bool -> Kiểm Y | N        
    /// 5: Time -> Kiểu chỉ thời gian HH:mm:ss
    /// </summary>
    public enum FWPLDataType
    {
        NONE = 0,
        TEXT = 1,
        DOUBLE_NUMBER = 2,//Số thực
        DISPLAY_DATE = 3,//Chính là Display Date
        BOOL = 4,
        SHORT_TIME = 5,
        DISPLAY_TIME = 6,
        DISPLAY_DATE_TIME = 7,
        LONG_TIME = 8,
        INT_NUMBER = 9
    }

    /// <summary>Lớp hỗ trợ lưu dùng chuỗi để lưu nhiều kiểu dữ liệu.
    /// Các kiểu dữ liệu hỗ trợ nằm trong FWPLDataType. Hiện tại lớp chưa cài đặt hoàn chỉnh
    /// nếu ko đáp ứng vui lòng yêu cầu nâng cấp.
    /// </summary>
    public class HelpMultiDataTypeField
    {
        #region String -> Object; Object -> String; Dua vao DataType

        /// <summary>Hàm chuyển từ datatype kiểu số thành kiểu Enum
        /// </summary>
        public static FWPLDataType ToFWDatType(int dataType)
        {
            if (dataType == 2)
                return FWPLDataType.DOUBLE_NUMBER;
            else if (dataType == 3)
                return FWPLDataType.DISPLAY_DATE;
            else if (dataType == 4)
                return FWPLDataType.BOOL;
            else if (dataType == 5)
                return FWPLDataType.SHORT_TIME;
            else if (dataType == 6)
                return FWPLDataType.DISPLAY_TIME;
            else if (dataType == 7)
                return FWPLDataType.DISPLAY_DATE_TIME;
            else if (dataType == 8)
                return FWPLDataType.LONG_TIME;
            else if (dataType == 9)
                return FWPLDataType.INT_NUMBER;
            else
                return FWPLDataType.TEXT;
        }

        /// <summary>Hàm lấy Object từ dữ liệu kiểu chuỗi.
        /// </summary>
        public static object GetObjectFromPLString(string data, FWPLDataType dataType)
        {
            switch (dataType)
            {
                case FWPLDataType.BOOL:
                    if (data == String.Empty)
                        return null;
                    else if (data == "Y")
                        return true;
                    else if (data == "N")
                        return false;
                    break;
                case FWPLDataType.DOUBLE_NUMBER:
                    return double.Parse(data);
                case FWPLDataType.INT_NUMBER:
                    return long.Parse(data);
                case FWPLDataType.TEXT:
                    return data;
                case FWPLDataType.DISPLAY_DATE:
                    return HelpDateExt02.ParseDisplayDate(data);
                case FWPLDataType.LONG_TIME:
                    return HelpDateExt02.ParseLongTime(data);
                case FWPLDataType.SHORT_TIME:
                    return HelpDateExt02.ParseShortTime(data);
                default:
                    break;
            }
            return null;
        }

        /// <summary>Hàm lấy chuỗi tương ứng dataType để lưu
        /// </summary>
        public static string GetPLStringFromObject(object data, FWPLDataType dataType)
        {
            if (data == null) return "";
            switch (dataType)
            {
                case FWPLDataType.BOOL:
                    if (data.ToString()=="True" || data.ToString() == "Y" )
                        return "Y";
                    else if (data.ToString() == "False" || data.ToString() == "N")
                        return "N";
                    break;
                case FWPLDataType.DISPLAY_DATE:
                    return HelpDateExt02.ToDisplayDateString((DateTime)data);
                case FWPLDataType.DOUBLE_NUMBER:
                    return "" + data;
                case FWPLDataType.TEXT:
                    return data.ToString();
                case FWPLDataType.SHORT_TIME:
                    return HelpDateExt02.ToShortTimeString((DateTime)data);
                default:
                    break;
            }
            return "";
        }
        #endregion
    }
}
