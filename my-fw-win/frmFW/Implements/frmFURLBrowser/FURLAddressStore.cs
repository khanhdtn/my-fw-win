using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class FURLAddressStore
    {
        public static List<FURLAddress> Store = new List<FURLAddress>();
        //PHUOCNC: Kiểm tra nếu trùng ko thêm
        public static void Add(FURLAddress Addr)
        {
            Store.Add(Addr);
        }

        private static string GetURL_FORM(string FURL, ref string[] Parameters)
        {
            string[] addresses = FURL.Split(new char[] { '?' });
            if (addresses != null)
            {
                Parameters = new string[addresses.Length - 1];
                for (int i = 0; i < Parameters.Length; i++)
                {
                    Parameters[i] = addresses[i + 1];
                }
                return addresses[0];
            }
            return null;
        }

        //PHUOCNC: Từ FURL lấy FURLAddress tương ứng dựa vào URL_FORM
        public static FURLAddress GetFURLAddress(string FURL)
        {
            //Từ FURL -> URL_FORM
            string[] Parameters = null;
            string URL_FORM = GetURL_FORM(FURL, ref Parameters);
            if (URL_FORM == null) return null;
            FURLAddress Result = null;
            //URL_FORM Lookup chính xác FURLAddress
            foreach (FURLAddress Address in Store)
            {
                if (Address.URL_FORM == URL_FORM)
                {
                    Result = Address;
                    break;
                }
            }
            if (Result != null)
                Result.PARAMS = Parameters;

            return Result;
        }

        //PHUOCNC: 
        public static List<FURLAddress> GetFURLAddresses(string FURL)
        {
            //FURL -> URL_FORM
            //Từ FURL -> URL_FORM
            string[] Parameters = null;
            List<FURLAddress> Result = new List<FURLAddress>();
            string URL_FORM = GetURL_FORM(FURL, ref Parameters);
            //Lookup gần đúng URL_FORM trong Store
            if (URL_FORM != null)
            {
                foreach (FURLAddress Address in Store)
                {
                    if (Address.URL_FORM.ToLower().Contains(URL_FORM.ToLower()))
                    {
                        Result.Add(Address);
                    }
                }
            }
            return Result;
        }
    }

    public class FURLAddress
    {
        public string NAME_FORM;
        public string URL_FORM;
        public IFURLType FURL_TYPE;

        //Using Runtime
        public string[] PARAMS;

        public FURLAddress(string NAME_FORM, string FURL_FORM, IFURLType FURL_TYPE)
        {
            this.NAME_FORM = NAME_FORM;
            this.URL_FORM = FURL_FORM;
            this.FURL_TYPE = FURL_TYPE;

            FURLAddressStore.Add(this);
        }
    }
}
