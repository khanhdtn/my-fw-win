using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class GDBException : Exception
    {
        public Exception ex;
        public GDBException() { }
        public GDBException(String msg){
            try
            {
                throw new Exception("Lỗi GDB. " + msg);
            }
            catch (Exception ex){
                this.ex = ex;
            }
        }
        public Exception GetEx()
        {
            return this.ex;
        }
    }

    public class GDBTbException : GDBException
    {
        public GDBTbException() { }
        public GDBTbException(String tableName)
        {
            try{
                throw new Exception("Lỗi GDB. " + "Thiếu bảng " + tableName);
            }
            catch(Exception ex){
                this.ex = ex;
            }
        }
    }

    public class GDBStoreException : GDBException
    {
        public GDBStoreException() { }
        public GDBStoreException(String storeName)
        {
            try
            {
                throw new Exception("Lỗi GDB. " + "Thiếu Store Procedure " + storeName);
            }
            catch (Exception ex) {
                this.ex = ex;
            }
        }
    }
}
