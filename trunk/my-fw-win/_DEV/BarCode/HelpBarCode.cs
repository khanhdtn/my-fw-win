using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocolVN.Framework.Core;
using DevExpress.XtraPrinting.BarCode;

namespace ProtocolVN.Framework.Win
{
    #region Sử dụng CheckDigit của DevExpress
    class EAN13CheckDigit : EAN13Generator
    {
        public static char checkDigit(String text)
        {
            return EAN13Generator.CalcCheckDigit(text);
        }
    }
    class EAN8CheckDigit : EAN8Generator
    {
        public static char checkDigit(String text)
        {
            return EAN8Generator.CalcCheckDigit(text);
        }
    }
    class Industrial2of5CheckDigit : Industrial2of5Generator
    {
        public static char checkDigit(String text)
        {
            return Industrial2of5Generator.CalcCheckDigit(text);
        }
    }
    

    #endregion
    public class HelpBarCode
    {
        public static String generateBarcode(String ma)
        {
            DOBarcodeOption bc = DOBarcodeOption.load();
            #region BarCodeType.EAN13
            if (bc.SYM_BARCODE == (int)BarCodeType.EAN13)
            {
                int lengthMa = 12 - bc.COUNTRY.Length - bc.PROVIDER.Length;
                String maMoi = HelpBarCode.check(ma, "0123456789", lengthMa);
                if (maMoi == "") return "";                
                String results = bc.COUNTRY + bc.PROVIDER + maMoi;
                ///*
                //* Lấy tổng tất cả các số ở vị trí lẻ (1,3,5,7,9,11) được một số A.
                //* Lấy tổng tất cả các số ở vị trí chẵn (2,4,6,8,10,12). Tổng này nhân với 3 được một số (B).
                //* Lấy tổng của A và B được số A+B.
                //* Lấy phần dư trong phép chia của A+B cho 10, gọi là số x. 
                //* Nếu số dư này bằng 0 thì số kiểm tra bằng 0, nếu nó khác 0 thì số kiểm tra là phần bù (10-x) của số dư đó.
                //*/
                //int A = HelpNumber.ParseInt32(results[0]) + HelpNumber.ParseInt32(results[2]) + HelpNumber.ParseInt32(results[4]) +
                //        HelpNumber.ParseInt32(results[6]) + HelpNumber.ParseInt32(results[8]) + HelpNumber.ParseInt32(results[10]);
                //int B = HelpNumber.ParseInt32(results[1]) + HelpNumber.ParseInt32(results[3]) + HelpNumber.ParseInt32(results[5]) +
                //        HelpNumber.ParseInt32(results[7]) + HelpNumber.ParseInt32(results[9]) + HelpNumber.ParseInt32(results[11]);

                //int check = (A + B * 3) % 10;
                //if (check != 0) check = 10 - check;
                //return results + check;
                return results + EAN13CheckDigit.checkDigit(results);
            }

            #endregion
            #region BarCodeType.EAN8
            else if (bc.SYM_BARCODE == (int)BarCodeType.EAN8)
            {
                String maMoi = HelpBarCode.check(ma, "0123456789", 7);
                if (maMoi == "") return "";                                
                String results = maMoi;
                /*
                 *  20046132
                    (3+6+0+2)*3 = 33
                    (1+4+0) = 5
                    33 + 5 = 38               10 - (38 % 10) = 2
                 * 
                 *  Lấy tổng tất cả các số ở vị trí (3,6,0,2). Tổng này nhân với 3 được một số A.
                 *  Lấy tổng tất cả các số ở vị trí chẵn (1,4,0). Được một số (B).
                 *  Lấy tổng của A và B được số A+B.
                 *  Lấy phần dư trong phép chia của A+B cho 10, gọi là số x. 
                 *  Nếu số dư này bằng 0 thì số kiểm tra bằng 0, nếu nó khác 0 thì số kiểm tra là phần bù (10-x) của số dư đó.
                */
                //int A = HelpNumber.ParseInt32(results[0]) + HelpNumber.ParseInt32(results[2]) + HelpNumber.ParseInt32(results[4]) +
                //        HelpNumber.ParseInt32(results[6]);
                //int B = HelpNumber.ParseInt32(results[5]) + HelpNumber.ParseInt32(results[3]) + HelpNumber.ParseInt32(results[1]);

                //int check = (A * 3 + B) % 10;
                //if (check != 0) check = 10 - check;
                //return results + check;
                return maMoi + EAN8CheckDigit.checkDigit(maMoi);
            }
            #endregion
            else if(bc.SYM_BARCODE == (int)BarCodeType.CODE25_INDUSTRIAL){//Mod10
                String maMoi =HelpBarCode.check(ma, "0123456789", bc.CHAR_NUMBER);
                if (maMoi == "") return "";
                //int A = 0;
                //int B = 0;
                //for (int i = 0; i < maMoi.Length; i += 2)
                //    A += HelpNumber.ParseInt32(maMoi[i]) * 3;
                //for (int j = 1; j < maMoi.Length; j += 2)
                //    B += HelpNumber.ParseInt32(maMoi[j]);
                //int check = (A + B) % 10;
                //if (check != 0) check = 10 - check;

                //Tính check digit dùm anh
                //return maMoi + check;
                return maMoi;
                //return maMoi + Industrial2of5CheckDigit.checkDigit(maMoi);
            }
            else if( bc.SYM_BARCODE == (int)BarCodeType.CODE25_INTERLEAVED)//Mod10
            {                
                String maMoi =HelpBarCode.check(ma, "0123456789", bc.CHAR_NUMBER);
                if (maMoi == "") return "";
                return maMoi;
                //return maMoi + Industrial2of5CheckDigit.checkDigit(maMoi);                
            }
            else if (bc.SYM_BARCODE == (int)BarCodeType.CODABAR)
            {
                String maMoi = check(ma, "01234567890-$:/.+", bc.CHAR_NUMBER);
                if (maMoi == "") return "";
                //Không có check digit
                return maMoi;
            }
            else if (bc.SYM_BARCODE == (int)BarCodeType.CODE_39)//Mod43
            {
                String maMoi = check(ma, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%", bc.CHAR_NUMBER);
                if (maMoi == "") return "";

                //Có tính check digit
                return maMoi;
                //return maMoi + checkDigitMod43(maMoi);
            }
            else if (bc.SYM_BARCODE == (int)BarCodeType.CODE_39_EXT)//Mod43
            {
                String maMoi = check(ma, @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%!#&'()*,:;<=>?@[\]^_` abcdefghijklmnopqrstuvwxyz{|}", bc.CHAR_NUMBER);
                if (maMoi == "") return "";
                //Có tính check digit
                return maMoi;
                //return maMoi + checkDigitMod43(maMoi);
            }
            else if(bc.SYM_BARCODE == (int)BarCodeType.CODE_93){
                String maMoi = check(ma, "0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ-.$/+%", bc.CHAR_NUMBER);
                if (maMoi == "") return "";
                //Không tính check digit
                return maMoi;
            }
            else if (bc.SYM_BARCODE == (int)BarCodeType.CODE_93_EXT)
            {
                String maMoi = check(ma, @"0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZ-.$/+%!#&'()*,:;<=>?@[\]^_` abcdefghijklmnopqrstuvwxyz{|}~", bc.CHAR_NUMBER);
                if (maMoi == "") return "";
                //Không tính check digit
                return maMoi;
            }
            return "";
        }

        private static char checkDigitMod43(string text)
        {
            String dict = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";
            int result = 0;
            int pos = -1;
            for (int i = 0; i < text.Length; i++)
            {
                pos = dict.IndexOf(text[i]);
                if ( pos>=0 ) result += dict.IndexOf(text[i]);
            }
            result = result % 43;
            return dict[result];
        }

        private static String check(String ma, String ok, int length)
        {
            if (ma.Length > length) return "";

            for (int i = 0; i < ma.Length; i++)
            {
                if (!ok.Contains(ma[i]))
                    return "";
            }

            if (ma.Length < length)
            {
                string blank = "";
                for (int i = ma.Length; i < length; i++)
                    blank += "0";

                return blank + ma;
            }

            return ma;
        }        
    }
}
