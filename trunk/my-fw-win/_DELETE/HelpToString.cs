using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    using System;
    using System.Data.Common;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;
    /// <summary>
    /// sealed Lớp hỗ trợ chuyển thông tin ngày, số thành dạng bằng chữ
    /// </summary>
    public class HelpToString
    {
        public static bool AllRights = false;
        public static string ClosedBracket = ")";
        public const short English = 1;
        public static string MinusSymbol = "-";
        public static string MinusSymbolWithSpace = " - ";
        public static string OpenedBracket = "(";
        public const short Other = 2;
        public const short Vietnamese = 0;

        private HelpToString()
        {
        }

        public static DateTime BeginningDayOfMonth(DateTime Date)
        {
            return BeginningDayOfMonth(Date.Year, Date.Month);
        }

        public static DateTime BeginningDayOfMonth(int Year, int Month)
        {
            return new DateTime(Year, Month, 1);
        }

        public static DateTime EndingDayOfMonth(DateTime Date)
        {
            return EndingDayOfMonth(Date.Year, Date.Month);
        }

        public static DateTime EndingDayOfMonth(int Year, int Month)
        {
            return new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
        }

        public static void GetNextMonth(int Year, int Month, out int NextYear, out int NextMonth)
        {
            if (Month == 12)
            {
                NextMonth = 1;
                NextYear = Year + 1;
            }
            else
            {
                NextMonth = Month + 1;
                NextYear = Year;
            }
        }

        public static int MonthToQuarter(int Month)
        {
            return ((Month + 2) / 3);
        }

        public static int MonthToSixMonth(int Month)
        {
            if (Month >= 7)
            {
                return 1;
            }
            return 0;
        }

        public static string NumberToString(double Value)
        {
            StringBuilder builder = new StringBuilder();
            if (Value == 0.0)
            {
                builder.Append("Không");
            }
            else if (Math.Abs(Value) > 999999999999)
            {
                builder.Append("Số quá lớn");
            }
            else
            {
                char ch;
                if (Value < 0.0)
                {
                    builder.Append("Trừ");
                    Value = Math.Abs(Value);
                }
                string[] strArray = new string[] { " tỷ", " triệu", " nghìn", "" };
                string[] strArray2 = new string[] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                string[] strArray3 = Value.ToString("#000000000000.0000000000", currentCulture).Split(new string[] { currentCulture.NumberFormat.NumberDecimalSeparator }, StringSplitOptions.None);
                for (int i = 0; i < 4; i++)
                {
                    string str = strArray3[0].Substring(i * 3, 3);
                    if (!(str != "000"))
                    {
                        goto Label_029A;
                    }
                    ch = str[0];
                    char ch2 = str[1];
                    char ch3 = str[2];
                    if ((ch != '0') || ((builder.Length != 0) && (ch == '0')))
                    {
                        builder.Append(strArray2[Convert.ToInt16(ch.ToString(currentCulture))]);
                        builder.Append(" trăm");
                    }
                    if (ch2 == '1')
                    {
                        builder.Append(" mười");
                    }
                    else if (ch2 != '0')
                    {
                        builder.Append(strArray2[Convert.ToInt16(ch2.ToString(currentCulture))]);
                        builder.Append(" mươi");
                    }
                    else if ((ch3 != '0') && (builder.Length != 0))
                    {
                        builder.Append(" lẻ");
                    }
                    if (ch3 == '1')
                    {
                        switch (ch2)
                        {
                            case '0':
                            case '1':
                                builder.Append(strArray2[Convert.ToInt16(ch3.ToString(currentCulture))]);
                                goto Label_028F;
                        }
                        builder.Append(" mốt");
                    }
                    else if (ch3 == '5')
                    {
                        if (ch2 == '0')
                        {
                            builder.Append(strArray2[Convert.ToInt16(ch3.ToString(currentCulture))]);
                        }
                        else
                        {
                            builder.Append(" lăm");
                        }
                    }
                    else if (ch3 != '0')
                    {
                        builder.Append(strArray2[Convert.ToInt16(ch3.ToString(currentCulture))]);
                    }
                Label_028F:
                    builder.Append(strArray[i]);
                Label_029A:;
                }
                if (strArray3.Length == 2)
                {
                    int index = -1;
                    for (int j = strArray3[1].Length - 1; j > -1; j--)
                    {
                        ch = strArray3[1][j];
                        if ((ch != '0') && (index == -1))
                        {
                            builder.Append(" phẩy");
                            index = builder.Length;
                        }
                        if (index != -1)
                        {
                            builder.Insert(index, strArray2[Convert.ToInt16(ch.ToString(currentCulture))]);
                        }
                    }
                }
            }
            string str2 = builder.ToString();
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = str2.Trim();
                char ch4 = str2[0];
                str2 = str2.Remove(0, 1);
                str2 = ch4.ToString().ToUpper(CultureInfo.CurrentCulture) + str2;
            }
            return str2;
        }

        public static string NumberToString(double Value, string Unit)
        {
            return (NumberToString(Value) + " " + Unit);
        }

        public static string NumberToStringVND(double Value)
        {
            return (NumberToString(Value) + " đồng.");
        }

        public static int QuarterToBeginningMonth(int Quarter)
        {
            return ((Quarter * 3) - 2);
        }

        public static int QuarterToEndingMonth(int Quarter)
        {
            return (Quarter * 3);
        }

        public static int SixMonthToBeginningMonth(int SixMonth)
        {
            if (SixMonth != 0)
            {
                return 7;
            }
            return 1;
        }

        public static int SixMonthToEndingMonth(int SixMonth)
        {
            if (SixMonth != 0)
            {
                return 12;
            }
            return 6;
        }

        public static string ToString(DateTime FromDate, DateTime ToDate)
        {
            int day = FromDate.Day;
            int month = FromDate.Month;
            int year = FromDate.Year;
            int num4 = ToDate.Day;
            int num5 = ToDate.Month;
            int num6 = ToDate.Year;
            if ((day == 1) && (num4 == DateTime.DaysInMonth(num6, num5)))
            {
                if ((month == num5) && (year == num6))
                {
                    return string.Concat(new object[] { "Tháng ", num5, "/", year });
                }
                if ((((month == 1) || (month == 4)) || ((month == 7) || (month == 10))) && (((month + 2) == num5) && (year == num6)))
                {
                    return string.Concat(new object[] { "Quý ", MonthToQuarter(month), " năm ", year });
                }
                if (((month == 1) || (month == 7)) && (((month + 5) == num5) && (year == num6)))
                {
                    return string.Concat(new object[] { "Sáu tháng ", (MonthToSixMonth(month) == 0) ? "đầu" : "cuối", " năm ", year });
                }
                if (((month == 1) && (num5 == 12)) && (year == num6))
                {
                    return ("Năm " + year);
                }
                if (((month == 1) && (num5 == 12)) && (year != num6))
                {
                    if ((year + 1) == num6)
                    {
                        return string.Concat(new object[] { "Năm ", year, " và ", num6 });
                    }
                    return string.Concat(new object[] { "Từ năm ", year, " đến năm ", num6 });
                }
                if ((((month == 1) || (month == 7)) && ((num5 == 6) || (num5 == 12))) && (year != num6))
                {
                    int num7 = MonthToSixMonth(month);
                    int num8 = MonthToSixMonth(num5);
                    if ((((year + 1) == num6) && (num7 == 1)) && (num8 == 0))
                    {
                        return string.Concat(new object[] { "Sáu tháng ", (num7 == 0) ? "đầu" : "cuối", " năm ", year, " và sáu tháng ", (num8 == 0) ? "đầu" : "cuối", " năm ", num6 });
                    }
                    return string.Concat(new object[] { "Từ sáu tháng ", (num7 == 0) ? "đầu" : "cuối", " năm ", year, " đến sáu ", (num8 == 0) ? "đầu" : "cuối", " năm ", num6 });
                }
                if ((((month == 1) || (month == 4)) || ((month == 7) || (month == 10))) && (((num5 == 3) || (num5 == 6)) || ((num5 == 9) || (num5 == 12))))
                {
                    int num9 = MonthToQuarter(month);
                    int num10 = MonthToQuarter(num5);
                    if (((year == num6) && ((num9 + 1) == num10)) || ((((year + 1) == num6) && (num9 == 4)) && (num10 == 1)))
                    {
                        return string.Concat(new object[] { "Quý ", num9, " năm ", year, " và quý ", num10, " năm ", num6 });
                    }
                    return string.Concat(new object[] { "Từ quý ", num9, " năm ", year, " đến quý ", num10, " năm ", num6 });
                }
                if (((year == num6) && ((month + 1) == num5)) || ((((year + 1) == num6) && (month == 12)) && (num5 == 1)))
                {
                    return string.Concat(new object[] { "Tháng ", month, "/", year, " và ", num5, "/", num6 });
                }
                return string.Concat(new object[] { "Từ tháng ", month, "/", year, " đến tháng ", num5, "/", num6 });
            }
            if (((day == num4) && (month == num5)) && (year == num6))
            {
                return ("Ngày " + FromDate.ToShortDateString());
            }
            if (FromDate.AddDays(1.0) == ToDate)
            {
                return ("Ngày " + FromDate.ToShortDateString() + " và " + ToDate.ToShortDateString());
            }
            return ("Từ ngày " + FromDate.ToShortDateString() + " đến ngày " + ToDate.ToShortDateString());
        }
    }    
}
