using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    public interface IFURLType {
        object CreateInstance(FURLAddress Owner);
    }

    //Q: là Phiếu quản lý ( như thiết kế hiện tại ): không tham số
    public class QFURLType : IFURLType{
        #region IFURLType Members

        public object CreateInstance(FURLAddress Owner)
        {
            return GenerateClass.initObject(Owner.NAME_FORM);
        }

        #endregion
    }

    //A: là Phiếu thêm : ko tham số, hoặc 1 tham số là ID ( Hơi vô lý ko ai biết ID chỉ biết SoPhieu
    public class AFURLType : IFURLType{
        public string KEY_FIELD;
        public string MA_FIELD_NAME;
        public string TABLE_NAME;
        public AFURLType(string KEY_FIELD, string MA_FIELD_NAME, string TABLE_NAME)
        {
            this.KEY_FIELD = KEY_FIELD;
            this.MA_FIELD_NAME = MA_FIELD_NAME;
            this.TABLE_NAME = TABLE_NAME;
        }

        #region IFURLType Members

        private long GetID(String SoPhieu)
        {
            string sql = "select " + this.KEY_FIELD + " from " +
                this.TABLE_NAME + " where " + this.MA_FIELD_NAME +
                "='" + SoPhieu + "'";
            DbCommand select = DABase.getDatabase().GetSQLStringCommand(sql);
            object ret = DABase.getDatabase().ExecuteScalar(select);
            if(ret != null) return HelpNumber.ParseInt64(ret);
            else return -1;
        }
        
        public object CreateInstance(FURLAddress Owner)
        {
            object instance = null;
            try
            {
                if (Owner.PARAMS != null && Owner.PARAMS.Length > 0)
                {
                    instance = GenerateClass.initObject(Owner.NAME_FORM, this.GetID(Owner.PARAMS[0]));
                }
                else
                {
                    instance = GenerateClass.initObject(Owner.NAME_FORM);
                }
            }
            catch { }
            return instance;
        }

        #endregion
    }

    //PHUOCNC-TODO FURLAddressFactory
    /*
     * Danh mục?Nhân viên
     * Danh mục?Khách hàng
     *
     * A
     * Phiếu bán hàng
     * Phiếu bán hàng?123456
     * 
     * Q
     * Phiếu bán hàng QL     
     * FURL (cái này sẽ hiện thị trên từng form)
     */
    public class FURLAddressFactory
    {
        public static FURLAddress GenFURLAddress()
        {
            return null;
        }
    }
}
