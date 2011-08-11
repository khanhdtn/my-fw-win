using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    /*  d - Numeric day of the month without a leading zero.
        dd - Numeric day of the month with a leading zero.
        ddd - Abbreviated name of the day of the week.
        dddd - Full name of the day of the week.

        f,ff,fff,ffff,fffff,ffffff,fffffff - 
	        Fraction of a second. The more Fs the higher the precision.

        h - 12 Hour clock, no leading zero.
        hh - 12 Hour clock with leading zero.
        H - 24 Hour clock, no leading zero.
        HH - 24 Hour clock with leading zero.

        m - Minutes with no leading zero.
        mm - Minutes with leading zero.

        M - Numeric month with no leading zero.
        MM - Numeric month with a leading zero.
        MMM - Abbreviated name of month.
        MMMM - Full month name.

        s - Seconds with no leading zero.
        ss - Seconds with leading zero.

        t - AM/PM but only the first letter. 
        tt - AM/PM ( a.m. / p.m.)

        y - Year with out century and leading zero.
        yy - Year with out century, with leading zero.
        yyyy - Year with century.

        zz - Time zone off set with +/-.
     */
    public class HelpDateExt02 : HelpDate
    {
        #region Chuyển đồi chuỗi thành Date/Time/DateTime
        /// <summary>Chuyển chuỗi ngày thành DataTime. DisplayDate chính là ngày tuân theo chuỗi định
        /// dạng do người dùng cấu hình
        /// </summary>
        public static DateTime ParseDisplayDate(string input)
        {
            return DateTime.ParseExact(input, FrameworkParams.option.dateFormat, null);
        }

        /// <summary>Chuyển chuỗi ngày giờ thành DateTime . DisplayDate chính là ngày tuân theo chuỗi định
        /// dạng do người dùng cấu hình
        /// </summary>
        public static DateTime ParseDisplayDateTime(string input)
        {
            return DateTime.ParseExact(input, FrameworkParams.option.DateTimeFomat, null);
        }

        /// <summary>Chuyển chuỗi giờ thành DateTime . DisplayDate chính là ngày tuân theo chuỗi định
        /// dạng do người dùng cấu hình
        /// </summary>
        public static DateTime ParseDisplayTime(string input)
        {
            return DateTime.ParseExact(input, FrameworkParams.option.timeFormat, null);
        }


        /// <summary>Chuyển chuỗi HH:mm thành kiểu DateTime.
        /// </summary>
        public static DateTime ParseShortTime(string input)
        {
            return DateTime.ParseExact(input, "HH:mm", null);
        }

        /// <summary>Chuyển chuỗi HH:mm:ss thành kiểu DateTime.
        /// </summary>
        public static DateTime ParseLongTime(string input)
        {
            return DateTime.ParseExact(input, "HH:mm:ss", null);
        }
        #endregion


        #region Ngày
        /// <summary>Chuyển DateTime thành chuỗi ngày hiển thị do người dùng cấu hình
        /// </summary>
        public static string ToDisplayDateString(DateTime input){
            return input.ToString(FrameworkParams.option.dateFormat);
        }

        /// <summary>Chuyển DateTime thành chuỗi ngày giờ hiển thị do người dùng cấu hình
        /// </summary>
        public static string ToDisplayDateTimeString(DateTime input)
        {
            return input.ToString(FrameworkParams.option.DateTimeFomat);
        }
        #endregion


        #region Thời gian
        /// <summary>Chuyển DateTime thành chuỗi giờ hiển thị do người dùng cấu hình
        /// </summary>
        public static string ToDisplayTimeString(DateTime input)
        {
            return input.ToString(FrameworkParams.option.timeFormat);
        }

        /// <summary>Chuyển DateTime thành chuỗi thời gian theo định dạng HH:mm
        /// </summary>
        public static string ToShortTimeString(DateTime input)
        {
            return input.ToString("HH:mm");
        }

        /// <summary>Chuyển DateTime thành chuỗi thời gian theo định dạng HH:mm:ss
        /// </summary>
        public static string ToLongTimeString(DateTime input)
        {
            return input.ToString("HH:mm:ss");
        }
        #endregion
    }
}