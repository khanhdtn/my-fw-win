using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProtocolVN.Plugin.VietInput
{
    public class VietKeyHandler
    {
        //private InputType inputtype;
        private char[] lastkey = new char[] { ' ' , ' ' , ' ' };
        private char newchar = ' ';
        public enum InputType { Telex , Vni , Auto , Off };
        public VietKeyHandler()
        {
            //this.inputtype = InputType.Telex;
        }
        //public VietKeyHandler(InputType i)
        //{
        //    this.inputtype = i;
        //}
        //public InputType InputMethods
        //{
        //    get
        //    {
        //        return inputtype;
        //    }
        //    set
        //    {
        //        inputtype = value;
        //    }
        //}
        private char[] chukep = new char[] { 'A' , 'a' , 'D' , 'd' , 'E' , 'e' , 'O' , 'o' , 'W' , 'w' };
        private char[] bodau = new char[] { 'f' , 'F' , 's' , 'S' , 'r' , 'R' , 'x' , 'X' , 'j' , 'J' , 'z' , 'Z' };
        public void Process(char KeyChar)
        {
            newchar = KeyChar;
            if (PLVietKey.KieuGo == InputType.Telex)
            {
                if (inchar(newchar , chukep)) ChuKep();
                else
                    if (inchar(newchar , bodau)) BoDau();
            }
            else
                if (PLVietKey.KieuGo == InputType.Vni)
                    VNI();
                else
                    if (PLVietKey.KieuGo == InputType.Auto)
                    {
                        if (inchar(newchar , chukep)) ChuKep();
                        else
                            if (inchar(newchar , bodau)) BoDau();
                        VNI();
                    }
            lastkey[2] = lastkey[1];
            lastkey[1] = lastkey[0];
            lastkey[0] = newchar;
        }
        char[] PhuAmCuoi = new Char[] { 'c' , 'C' , 'g' , 'G' , 'h' , 'H' , 'm' , 'M' , 'n' , 'N' , 'p' , 'P' , 't' , 'T' };
        char[] NguyenAm = new char[]
		{
			'a','á','à','ã','ả','ạ','â','ấ','ầ','ẫ','ẩ','ậ','ă','ắ','ằ','ẵ','ẳ','ặ',
			'A','Á','À','Ã','Ả','Ạ','Â','Ấ','Ầ','Ẫ','Ẩ','Ậ','Ă','Ắ','Ằ','Ẵ','Ẳ','Ặ',
			'e','é','è','ẽ','ẻ','ẹ','ê','ế','ề','ễ','ể','ệ',
			'E','É','È','Ẽ','Ẻ','Ẹ','Ê','Ế','Ề','Ễ','Ể','Ệ',
			'i','í','ì','ĩ','ỉ','ị',
			'I','Í','Ì','Ĩ','Ỉ','Ị',
			'o','ó','ò','õ','ỏ','ọ','ô','ố','ồ','ỗ','ổ','ộ','ơ','ớ','ờ','ỡ','ở','ợ',
			'O','Ó','Ò','Õ','Ỏ','Ọ','Ô','Ố','Ồ','Ỗ','Ổ','Ộ','Ơ','Ớ','Ờ','Ỡ','Ở','Ợ',
			'u','ú','ù','ũ','ủ','ụ','ư','ứ','ừ','ữ','ử','ự',
			'U','Ú','Ù','Ũ','Ủ','Ụ','Ư','Ứ','Ừ','Ữ','Ử','Ự',
			'y','ý','ỳ','ỹ','ỷ','ỵ',
			'Y','Ý','Ỳ','Ỹ','Ỷ','Ỵ'};

        char[] ChuCoMu = new char[] {'â','ấ','ầ','ẫ','ẩ','ậ','ă','ắ','ằ','ẵ','ẳ','ặ',
										'Â','Ấ','Ầ','Ẫ','Ẩ','Ậ','Ă','Ắ','Ằ','Ẵ','Ẳ','Ặ',
										'ê','ế','ề','ễ','ể','ệ',
										'Ê','Ế','Ề','Ễ','Ể','Ệ',
										'ô','ố','ồ','ỗ','ổ','ộ','ơ','ớ','ờ','ỡ','ở','ợ',
										'Ô','Ố','Ồ','Ỗ','Ổ','Ộ','Ơ','Ớ','Ờ','Ỡ','Ở','Ợ',
										'ư','ứ','ừ','ữ','ử','ự',
										'Ư','Ứ','Ừ','Ữ','Ử','Ự'};
        char[] ChuCai = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','w','u','x','y','z',
									   'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','W','U','X','Y','Z'};
        char[] achar = new char[] { 'a' , 'á' , 'à' , 'ã' , 'ả' , 'ạ' };
        char[] Achar = new char[] { 'A' , 'Á' , 'À' , 'Ã' , 'Ả' , 'Ạ' };
        char[] âchar = new char[] { 'â' , 'ấ' , 'ầ' , 'ẫ' , 'ẩ' , 'ậ' };
        char[] Âchar = new char[] { 'Â' , 'Ấ' , 'Ầ' , 'Ẫ' , 'Ẩ' , 'Ậ' };
        char[] ăchar = new char[] { 'ă' , 'ắ' , 'ằ' , 'ẵ' , 'ẳ' , 'ặ' };
        char[] Ăchar = new char[] { 'Ă' , 'Ắ' , 'Ằ' , 'Ẵ' , 'Ẳ' , 'Ặ' };
        char[] echar = new char[] { 'e' , 'é' , 'è' , 'ẽ' , 'ẻ' , 'ẹ' };
        char[] Echar = new char[] { 'E' , 'É' , 'È' , 'Ẽ' , 'Ẻ' , 'Ẹ' };
        char[] êchar = new char[] { 'ê' , 'ế' , 'ề' , 'ễ' , 'ể' , 'ệ' };
        char[] Êchar = new char[] { 'Ê' , 'Ế' , 'Ề' , 'Ễ' , 'Ể' , 'Ệ' };
        char[] ichar = new char[] { 'i' , 'í' , 'ì' , 'ĩ' , 'ỉ' , 'ị' };
        char[] Ichar = new char[] { 'I' , 'Í' , 'Ì' , 'Ĩ' , 'Ỉ' , 'Ị' };
        char[] ochar = new char[] { 'o' , 'ó' , 'ò' , 'õ' , 'ỏ' , 'ọ' };
        char[] Ochar = new char[] { 'O' , 'Ó' , 'Ò' , 'Õ' , 'Ỏ' , 'Ọ' };
        char[] ôchar = new char[] { 'ô' , 'ố' , 'ồ' , 'ỗ' , 'ổ' , 'ộ' };
        char[] Ôchar = new char[] { 'Ô' , 'Ố' , 'Ồ' , 'Ỗ' , 'Ổ' , 'Ộ' };
        char[] ơchar = new char[] { 'ơ' , 'ớ' , 'ờ' , 'ỡ' , 'ở' , 'ợ' };
        char[] Ơchar = new char[] { 'Ơ' , 'Ớ' , 'Ờ' , 'Ỡ' , 'Ở' , 'Ợ' };
        char[] uchar = new char[] { 'u' , 'ú' , 'ù' , 'ũ' , 'ủ' , 'ụ' };
        char[] Uchar = new char[] { 'U' , 'Ú' , 'Ù' , 'Ũ' , 'Ủ' , 'Ụ' };
        char[] ưchar = new char[] { 'ư' , 'ứ' , 'ừ' , 'ữ' , 'ử' , 'ự' };
        char[] Ưchar = new char[] { 'Ư' , 'Ứ' , 'Ừ' , 'Ữ' , 'Ử' , 'Ự' };
        char[] ychar = new char[] { 'y' , 'ý' , 'ỳ' , 'ỹ' , 'ỷ' , 'ỵ' };
        char[] Ychar = new char[] { 'Y' , 'Ý' , 'Ỳ' , 'Ỹ' , 'Ỷ' , 'Ỵ' };

        [DllImport("User32.dll")]
        private static extern int GetKeyState(int nVirtKey);
        byte VK_CAPTION = 20;

        private bool inchar(char c , char[] ach)
        {
            foreach (char ch in ach)
            {
                if (c == ch)
                    return true;
            }
            return false;
        }
        private void Huyen(ref char ch)
        {
            if (inchar(ch , achar)) { ch = 'à'; return; }
            if (inchar(ch , Achar)) { ch = 'À'; return; }
            if (inchar(ch , âchar)) { ch = 'ầ'; return; }
            if (inchar(ch , Âchar)) { ch = 'Ầ'; return; }
            if (inchar(ch , ăchar)) { ch = 'ằ'; return; }
            if (inchar(ch , Ăchar)) { ch = 'Ằ'; return; }
            if (inchar(ch , echar)) { ch = 'è'; return; }
            if (inchar(ch , Echar)) { ch = 'È'; return; }
            if (inchar(ch , êchar)) { ch = 'ề'; return; }
            if (inchar(ch , Êchar)) { ch = 'Ề'; return; }
            if (inchar(ch , ichar)) { ch = 'ì'; return; }
            if (inchar(ch , Ichar)) { ch = 'Ì'; return; }
            if (inchar(ch , ochar)) { ch = 'ò'; return; }
            if (inchar(ch , Ochar)) { ch = 'Ò'; return; }
            if (inchar(ch , ôchar)) { ch = 'ồ'; return; }
            if (inchar(ch , Ôchar)) { ch = 'Ồ'; return; }
            if (inchar(ch , ơchar)) { ch = 'ờ'; return; }
            if (inchar(ch , Ơchar)) { ch = 'Ờ'; return; }
            if (inchar(ch , uchar)) { ch = 'ù'; return; }
            if (inchar(ch , Uchar)) { ch = 'Ù'; return; }
            if (inchar(ch , ưchar)) { ch = 'ừ'; return; }
            if (inchar(ch , Ưchar)) { ch = 'Ừ'; return; }
            if (inchar(ch , ychar)) { ch = 'ỳ'; return; }
            if (inchar(ch , Ychar)) { ch = 'Ỳ'; return; }
        }
        private void Hoi(ref char ch)
        {
            if (inchar(ch , achar)) { ch = 'ả'; return; }
            if (inchar(ch , Achar)) { ch = 'Ả'; return; }
            if (inchar(ch , âchar)) { ch = 'ẩ'; return; }
            if (inchar(ch , Âchar)) { ch = 'Ẩ'; return; }
            if (inchar(ch , ăchar)) { ch = 'ẳ'; return; }
            if (inchar(ch , Ăchar)) { ch = 'Ẳ'; return; }
            if (inchar(ch , echar)) { ch = 'ẻ'; return; }
            if (inchar(ch , Echar)) { ch = 'Ẻ'; return; }
            if (inchar(ch , êchar)) { ch = 'ể'; return; }
            if (inchar(ch , Êchar)) { ch = 'Ể'; return; }
            if (inchar(ch , ichar)) { ch = 'ỉ'; return; }
            if (inchar(ch , Ichar)) { ch = 'Ỉ'; return; }
            if (inchar(ch , ochar)) { ch = 'ỏ'; return; }
            if (inchar(ch , Ochar)) { ch = 'Ỏ'; return; }
            if (inchar(ch , ôchar)) { ch = 'ổ'; return; }
            if (inchar(ch , Ôchar)) { ch = 'Ổ'; return; }
            if (inchar(ch , ơchar)) { ch = 'ở'; return; }
            if (inchar(ch , Ơchar)) { ch = 'Ở'; return; }
            if (inchar(ch , uchar)) { ch = 'ủ'; return; }
            if (inchar(ch , Uchar)) { ch = 'Ủ'; return; }
            if (inchar(ch , ưchar)) { ch = 'ử'; return; }
            if (inchar(ch , Ưchar)) { ch = 'Ử'; return; }
            if (inchar(ch , ychar)) { ch = 'ỷ'; return; }
            if (inchar(ch , Ychar)) { ch = 'Ỷ'; return; }
        }
        private void Nga(ref char ch)
        {
            if (inchar(ch , achar)) { ch = 'ã'; return; }
            if (inchar(ch , Achar)) { ch = 'Ã'; return; }
            if (inchar(ch , âchar)) { ch = 'ẫ'; return; }
            if (inchar(ch , Âchar)) { ch = 'Ẫ'; return; }
            if (inchar(ch , ăchar)) { ch = 'ẵ'; return; }
            if (inchar(ch , Ăchar)) { ch = 'Ẵ'; return; }
            if (inchar(ch , echar)) { ch = 'ẽ'; return; }
            if (inchar(ch , Echar)) { ch = 'Ẽ'; return; }
            if (inchar(ch , êchar)) { ch = 'ễ'; return; }
            if (inchar(ch , Êchar)) { ch = 'Ễ'; return; }
            if (inchar(ch , ichar)) { ch = 'ĩ'; return; }
            if (inchar(ch , Ichar)) { ch = 'Ĩ'; return; }
            if (inchar(ch , ochar)) { ch = 'õ'; return; }
            if (inchar(ch , Ochar)) { ch = 'Õ'; return; }
            if (inchar(ch , ôchar)) { ch = 'ỗ'; return; }
            if (inchar(ch , Ôchar)) { ch = 'Ỗ'; return; }
            if (inchar(ch , ơchar)) { ch = 'ỡ'; return; }
            if (inchar(ch , Ơchar)) { ch = 'Ỡ'; return; }
            if (inchar(ch , uchar)) { ch = 'ũ'; return; }
            if (inchar(ch , Uchar)) { ch = 'Ũ'; return; }
            if (inchar(ch , ưchar)) { ch = 'ữ'; return; }
            if (inchar(ch , Ưchar)) { ch = 'Ữ'; return; }
            if (inchar(ch , ychar)) { ch = 'ỹ'; return; }
            if (inchar(ch , Ychar)) { ch = 'Ỹ'; return; }
        }
        private void Sac(ref char ch)
        {
            if (inchar(ch , achar)) { ch = 'á'; return; }
            if (inchar(ch , Achar)) { ch = 'Á'; return; }
            if (inchar(ch , âchar)) { ch = 'ấ'; return; }
            if (inchar(ch , Âchar)) { ch = 'Ấ'; return; }
            if (inchar(ch , ăchar)) { ch = 'ắ'; return; }
            if (inchar(ch , Ăchar)) { ch = 'Ắ'; return; }
            if (inchar(ch , echar)) { ch = 'é'; return; }
            if (inchar(ch , Echar)) { ch = 'É'; return; }
            if (inchar(ch , êchar)) { ch = 'ế'; return; }
            if (inchar(ch , Êchar)) { ch = 'Ế'; return; }
            if (inchar(ch , ichar)) { ch = 'í'; return; }
            if (inchar(ch , Ichar)) { ch = 'Í'; return; }
            if (inchar(ch , ochar)) { ch = 'ó'; return; }
            if (inchar(ch , Ochar)) { ch = 'Ó'; return; }
            if (inchar(ch , ôchar)) { ch = 'ố'; return; }
            if (inchar(ch , Ôchar)) { ch = 'Ố'; return; }
            if (inchar(ch , ơchar)) { ch = 'ớ'; return; }
            if (inchar(ch , Ơchar)) { ch = 'Ớ'; return; }
            if (inchar(ch , uchar)) { ch = 'ú'; return; }
            if (inchar(ch , Uchar)) { ch = 'Ú'; return; }
            if (inchar(ch , ưchar)) { ch = 'ứ'; return; }
            if (inchar(ch , Ưchar)) { ch = 'Ứ'; return; }
            if (inchar(ch , ychar)) { ch = 'ý'; return; }
            if (inchar(ch , Ychar)) { ch = 'Ý'; return; }
        }
        private void Nang(ref char ch)
        {
            if (inchar(ch , achar)) { ch = 'ạ'; return; }
            if (inchar(ch , Achar)) { ch = 'Ạ'; return; }
            if (inchar(ch , âchar)) { ch = 'ậ'; return; }
            if (inchar(ch , Âchar)) { ch = 'Ậ'; return; }
            if (inchar(ch , ăchar)) { ch = 'ặ'; return; }
            if (inchar(ch , Ăchar)) { ch = 'Ặ'; return; }
            if (inchar(ch , echar)) { ch = 'ẹ'; return; }
            if (inchar(ch , Echar)) { ch = 'Ẹ'; return; }
            if (inchar(ch , êchar)) { ch = 'ệ'; return; }
            if (inchar(ch , Êchar)) { ch = 'Ệ'; return; }
            if (inchar(ch , ichar)) { ch = 'ị'; return; }
            if (inchar(ch , Ichar)) { ch = 'Ị'; return; }
            if (inchar(ch , ochar)) { ch = 'ọ'; return; }
            if (inchar(ch , Ochar)) { ch = 'Ọ'; return; }
            if (inchar(ch , ôchar)) { ch = 'ộ'; return; }
            if (inchar(ch , Ôchar)) { ch = 'Ộ'; return; }
            if (inchar(ch , ơchar)) { ch = 'ợ'; return; }
            if (inchar(ch , Ơchar)) { ch = 'Ợ'; return; }
            if (inchar(ch , uchar)) { ch = 'ụ'; return; }
            if (inchar(ch , Uchar)) { ch = 'Ụ'; return; }
            if (inchar(ch , ưchar)) { ch = 'ự'; return; }
            if (inchar(ch , Ưchar)) { ch = 'Ự'; return; }
            if (inchar(ch , ychar)) { ch = 'ỵ'; return; }
            if (inchar(ch , Ychar)) { ch = 'Ỵ'; return; }
        }
        private void XoaDau(ref char ch)
        {
            if (inchar(ch , achar)) { ch = 'a'; return; }
            if (inchar(ch , Achar)) { ch = 'A'; return; }
            if (inchar(ch , âchar)) { ch = 'â'; return; }
            if (inchar(ch , Âchar)) { ch = 'Â'; return; }
            if (inchar(ch , ăchar)) { ch = 'ã'; return; }
            if (inchar(ch , Ăchar)) { ch = 'Ã'; return; }
            if (inchar(ch , echar)) { ch = 'e'; return; }
            if (inchar(ch , Echar)) { ch = 'E'; return; }
            if (inchar(ch , êchar)) { ch = 'ê'; return; }
            if (inchar(ch , Êchar)) { ch = 'Ê'; return; }
            if (inchar(ch , ichar)) { ch = 'i'; return; }
            if (inchar(ch , Ichar)) { ch = 'I'; return; }
            if (inchar(ch , ochar)) { ch = 'o'; return; }
            if (inchar(ch , Ochar)) { ch = 'O'; return; }
            if (inchar(ch , ôchar)) { ch = 'ô'; return; }
            if (inchar(ch , Ôchar)) { ch = 'Ô'; return; }
            if (inchar(ch , ơchar)) { ch = 'õ'; return; }
            if (inchar(ch , Ơchar)) { ch = 'Õ'; return; }
            if (inchar(ch , uchar)) { ch = 'u'; return; }
            if (inchar(ch , Uchar)) { ch = 'U'; return; }
            if (inchar(ch , ưchar)) { ch = 'ư'; return; }
            if (inchar(ch , Ưchar)) { ch = 'Ư'; return; }
            if (inchar(ch , ychar)) { ch = 'y'; return; }
            if (inchar(ch , Ychar)) { ch = 'Y'; return; }
        }
        private void DauMu(ref char ch)
        {
            switch (ch)
            {
                case 'o': ch = 'ô'; break;
                case 'O': ch = 'Ô'; break;
                case 'e': ch = 'ê'; break;
                case 'E': ch = 'Ê'; break;
                case 'a': ch = 'â'; break;
                case 'A': ch = 'Â'; break;
            }
        }
        private void DauMoc(ref char ch)
        {
            switch (ch)
            {
                case 'o': ch = 'ơ'; break;
                case 'O': ch = 'Ơ'; break;
                case 'u': ch = 'ư'; break;
                case 'U': ch = 'Ư'; break;
            }
        }
        private void DauAS(ref char ch)
        {
            switch (ch)
            {
                case 'a': ch = 'ă'; break;
                case 'A': ch = 'Ă'; break;
            }
        }
        private void ChuDD(ref char ch)
        {
            switch (ch)
            {
                case 'd': ch = 'đ'; break;
                case 'D': ch = 'Đ'; break;
            }
        }

        private void SuaChu(char ch , byte position)
        {
            if (position == 0)
            {
                SendKeys.Send("{BACKSPACE}");
                if ((GetKeyState(20) & 0x0001) != 0)
                    SendKeys.Send(ch.ToString().ToUpper());
                else
                    SendKeys.Send(ch.ToString());
                lastkey[0] = ch;
            }
            else
            {
                SendKeys.Send("{BACKSPACE}");
                for (int i = 1 ; i <= position ; i++)
                    SendKeys.Send("{BACKSPACE}");
                lastkey[position - 1] = ch;

                if ((GetKeyState(VK_CAPTION) & 0x0001) != 0)
                {
                    if (newchar != 'Z')
                        SendKeys.Send(lastkey[position - 1].ToString());
                    else
                        if (inchar(lastkey[position - 1] , ChuCai))
                            SendKeys.Send(lastkey[position - 1].ToString().ToLower());
                        else
                            SendKeys.Send(lastkey[position - 1].ToString());

                    if (position > 1)
                        for (int j = position - 1 ; j >= 1 ; j--)
                            SendKeys.Send(lastkey[j - 1].ToString().ToLower());
                }
                else
                    for (int j = position ; j >= 1 ; j--)
                        SendKeys.Send(lastkey[j - 1].ToString());
            }
        }

        private void VNI()
        {
            char ch = lastkey[0];
            switch (newchar)
            {
                case '1': Sac(ref ch); break;
                case '2': Huyen(ref ch); break;
                case '3': Hoi(ref ch); break;
                case '4': Nga(ref ch); break;
                case '5': Nang(ref ch); break;
                case '6': DauMu(ref ch); break;
                case '7': DauMoc(ref ch); break;
                case '8': DauAS(ref ch); break;
                case '9': ChuDD(ref ch); break;
                case '0': XoaDau(ref ch); break;
            }
            if (ch != lastkey[0])
                SuaChu(ch , 1);
        }
        private void ChuKep()
        {
            switch (newchar)
            {
                case 'a':
                case 'A':
                    switch (lastkey[0])
                    {
                        case 'a': if (lastkey[1] != 'a') SuaChu('â' , 1); break;
                        case 'A': if (lastkey[1] != 'A') SuaChu('Â' , 1); break;
                    }
                    break;

                case 'e':
                case 'E':
                    switch (lastkey[0])
                    {
                        case 'e': if (lastkey[1] != 'e') SuaChu('ê' , 1); break;
                        case 'E': if (lastkey[1] != 'E') SuaChu('Ê' , 1); break;
                    }
                    break;
                case 'o':
                case 'O':
                    switch (lastkey[0])
                    {
                        case 'o': if (lastkey[1] != 'o') SuaChu('ô' , 1); break;
                        case 'O': if (lastkey[1] != 'O') SuaChu('Ô' , 1); break;
                    }
                    break;
                case 'd':
                case 'D':
                    switch (lastkey[0])
                    {
                        case 'd': if (lastkey[1] != 'd') SuaChu('đ' , 1); break;
                        case 'D': if (lastkey[1] != 'D') SuaChu('Đ' , 1); break;
                    }
                    break;
                case 'w':
                    switch (lastkey[0])
                    {
                        case 'A': SuaChu('Ă' , 1); break;
                        case 'a': SuaChu('ă' , 1); break;
                        case 'O': SuaChu('Ơ' , 1); break;
                        case 'o': SuaChu('ơ' , 1); break;
                        case 'U': SuaChu('Ư' , 1); break;
                        case 'u': SuaChu('ư' , 1); break;
                        case 'ư': SuaChu('w' , 1); break;
                        case 'Ư': SuaChu('W' , 1); break;
                        default: SuaChu('ư' , 0); break;
                    }
                    break;
                case 'W':
                    switch (lastkey[0])
                    {
                        case 'A': SuaChu('Ă' , 1); break;
                        case 'a': SuaChu('ă' , 1); break;
                        case 'O': SuaChu('Ơ' , 1); break;
                        case 'o': SuaChu('ơ' , 1); break;
                        case 'U': SuaChu('Ư' , 1); break;
                        case 'u': SuaChu('ư' , 1); break;
                        case 'ư': SuaChu('w' , 1); break;
                        case 'Ư': SuaChu('W' , 1); break;
                        default: SuaChu('Ư' , 0); break;
                    }
                    break;
            }
        }
        private void BoDauChu(char ch , byte position)
        {
            char old = ch;
            switch (newchar)
            {
                case 'f':
                case 'F': Huyen(ref ch); break;
                case 'r':
                case 'R': Hoi(ref ch); break;
                case 'x':
                case 'X': Nga(ref ch); break;
                case 'j':
                case 'J': Nang(ref ch); break;
                case 's':
                case 'S': Sac(ref ch); break;
                case 'z':
                case 'Z': XoaDau(ref ch); break;
            }
            if (ch != old) SuaChu(ch , position);
        }
        private void BoDau()
        {
            if (inchar(lastkey[0] , PhuAmCuoi))
            {
                char[] nc = new char[] { 'n' , 'N' , 'c' , 'C' };
                if (inchar(lastkey[1] , nc))
                {
                    if (inchar(lastkey[2] , NguyenAm))
                        BoDauChu(lastkey[2] , 3);
                }
                else
                    if (inchar(lastkey[1] , NguyenAm))
                        BoDauChu(lastkey[1] , 2);
            }
            else
                if (inchar(lastkey[0] , ChuCoMu))
                    BoDauChu(lastkey[0] , 1);
                else
                    if (inchar(lastkey[0] , NguyenAm))
                    {
                        char[] uU = new char[] { 'u' , 'U' };
                        char[] qQ = new char[] { 'q' , 'Q' };
                        char[] iI = new char[] { 'i' , 'I' };
                        char[] gG = new char[] { 'g' , 'G' };
                        bool QUGI = (inchar(lastkey[1] , uU) && inchar(lastkey[2] , qQ))
                            || (inchar(lastkey[1] , iI) && inchar(lastkey[2] , gG));
                        if (QUGI)
                            BoDauChu(lastkey[0] , 1);
                        else
                        {
                            char[] aAeE = new char[] { 'a' , 'á' , 'à' , 'ã' , 'ả' , 'ạ' , 'A' , 'Á' , 'À' , 'Ã' , 'Ả' , 'Ạ' , 'e' , 'é' , 'è' , 'ẽ' , 'ẻ' , 'ẹ' , 'E' , 'É' , 'È' , 'Ẽ' , 'Ẻ' , 'Ẹ' };
                            char[] yY = new char[] { 'y' , 'ý' , 'ỳ' , 'ỹ' , 'ý' , 'ỵ' , 'Y' , 'Ý' , 'Ỳ' , 'Ỹ' , 'Ỷ' , 'Ỵ' };
                            bool OAOEUY = (((lastkey[1] == 'o') || (lastkey[1] == 'O')) && (inchar(lastkey[0] , aAeE))
                                || (((lastkey[1] == 'u') || (lastkey[1] == 'U')) && (inchar(lastkey[0] , yY))));
                            if ((OAOEUY) || (!(inchar(lastkey[1] , NguyenAm)))) BoDauChu(lastkey[0] , 1);
                            else
                                if (inchar(lastkey[1] , NguyenAm)) BoDauChu(lastkey[1] , 2);
                        }
                    }
        }
    }
}
