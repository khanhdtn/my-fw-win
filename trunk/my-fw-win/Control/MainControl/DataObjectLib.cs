using System;
using System.Collections.Generic;
using System.Text;

using ProtocolVN.Framework.Win;
using ProtocolVN.Framework.Core;

//
//Dùng để chứa các Lớp đóng vai trò là DataObject
//
namespace ProtocolVN.Framework.Win
{
    /// <summary>Các thao tác trên control chọn dữ liệu từ danh mục    
    /// </summary>
    public interface ISelectionControl
    {
        /// <summary>Trả về ID tương ứng với phần từ đang chọn        
        /// ID = -1 : chưa chọn phần tử nào
        /// </summary>
        long _getSelectedID();

        /// <summary>Chọn phần tử có id bằng với id chỉ định
        /// ID = -1 : không chọn phần tử nào
        /// </summary>
        /// <param name="id"></param>
        void _setSelectedID(long id);

        /// <summary>Làm mới dữ liệu
        /// </summary>
        void _refresh(object NewSrc);
    }

    public class ItemInfo
    {
        private string caption;
        private string image;
        private DelegationLib.CallFunction_MulIn_NoOut delegates;
        private PermissionItem per;

        public ItemInfo(string caption, string image, DelegationLib.CallFunction_MulIn_NoOut delegates, PermissionItem per)
        {
            this.caption = caption;
            this.image = image;
            this.delegates = delegates;
            this.per = per;

        }

        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                caption = value;
            }
        }

        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        public DelegationLib.CallFunction_MulIn_NoOut Delegates
        {
            get
            {
                return delegates;
            }
            set
            {
                delegates = value;
            }
        }

        public PermissionItem Per
        {
            get
            {
                return per;
            }
            set
            {
                per = value;
            }
        }

    }

    /// <summary>Được dùng để xây dựng các Combobox control.
    /// </summary>
    public class ItemData : IConvertible
    {
        public long ID;
        public string Name;
        public ItemData(long ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public override String ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == DBNull.Value) return false;
            if (obj is String)
            {
                String s = (String)obj;
                s = s.Trim();
                return this.Name == s;
            }
            else
            {
                ItemData that = (ItemData)obj;
                if (that.ID == this.ID)
                {
                    that.Name = this.Name;
                    return true;
                }
                return false;
            }
        }

        #region IConvertible Members

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            return Name;
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
