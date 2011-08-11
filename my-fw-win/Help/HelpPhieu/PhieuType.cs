using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class PhieuTypeStore
    {
        public static List<PhieuType> Store = new List<PhieuType>();

        public static PhieuType GetPhieuType(Int64 ID_TYPE)
        {
            foreach (PhieuType phieu in Store)
                if (phieu.ID == ID_TYPE) return phieu;
            throw new Exception("Không tồn tại phiếu đó");
        }

        public static bool Add(PhieuType type)
        {
            foreach (PhieuType phieu in Store)
            {
                if (phieu.ID == type.ID)
                    throw new Exception("Trùng phiếu");                    
            }

            Store.Add(type);
            return true;
        }

        public static List<PhieuType> GetAllPhieuType(long nhom)
        {
            List<PhieuType> list = new List<PhieuType>();
            foreach (PhieuType phieu in Store)
            {
                if (phieu.GetNhomPhieu() == nhom)
                    list.Add(phieu);
            }
            return list;
        }
    }

    public class PhieuType
    {   
        public long ID;
        public long NHOM_PHIEU;             //
        private string TABLE_NAME;          //Tên table của Phiếu
        public string PLC_PHIEU_NAME;       //Tên của lớp PLC của phiếu
        private string DO_NAME;             //Tên DO của Phiếu
        private string FORM_CLASS_NAME="";  //Tên form hiển thị phiếu
        private string TITLE;               //Tiêu đề
        private string IMAGE_NAME;          //Tên hình

        private string ID_FIELD;            //IDPhieuFdName
        private string SO_LG_FIELD;         //??
        private string TR_LG_FIELD;         //??
        private string STT_FIELD;           //??

        public PermissionItem AllowAdd;
        public PermissionItem AllowIn;

        public List<PhieuType> CanCreateList = new List<PhieuType>();

        public PhieuType(long ID, string TABLE_NAME, string DO_NAME, string TITLE, PermissionItem AllowAdd, 
            string FORM_CLASS_NAME, string IMAGE_NAME, string ID_FIELD)
        {
            this.ID = ID;
            this.NHOM_PHIEU = ID;
            this.TABLE_NAME = TABLE_NAME;
            this.DO_NAME = DO_NAME;
            this.TITLE = TITLE;
            this.AllowAdd = AllowAdd;
            this.FORM_CLASS_NAME = FORM_CLASS_NAME;
            this.IMAGE_NAME = IMAGE_NAME;
            this.ID_FIELD = ID_FIELD;
            this.SO_LG_FIELD = "SO_LUONG";
            this.TR_LG_FIELD = "TRONG_LUONG";
            PhieuTypeStore.Add(this);
        }
        public PhieuType(long ID, string TABLE_NAME, string DO_NAME, string TITLE, PermissionItem AllowAdd,
            string FORM_CLASS_NAME, string IMAGE_NAME, string ID_FIELD, string SO_LG_FIELD, string TR_LG_FIELD)
        {
            this.ID = ID;
            this.NHOM_PHIEU = ID;
            this.TABLE_NAME = TABLE_NAME;
            this.DO_NAME = DO_NAME;
            this.TITLE = TITLE;
            this.AllowAdd = AllowAdd;
            this.FORM_CLASS_NAME = FORM_CLASS_NAME;
            this.IMAGE_NAME = IMAGE_NAME;
            this.ID_FIELD = ID_FIELD;
            this.SO_LG_FIELD = SO_LG_FIELD;
            this.TR_LG_FIELD = TR_LG_FIELD;

            PhieuTypeStore.Add(this);
        }
        public PhieuType(long ID, string TABLE_NAME, string DO_NAME, string TITLE, PermissionItem AllowAdd,
            string FORM_CLASS_NAME, string IMAGE_NAME, string ID_FIELD, string SO_LG_FIELD, string TR_LG_FIELD, string STT_FIELD)
        {
            this.ID = ID;
            this.NHOM_PHIEU = ID;
            this.TABLE_NAME = TABLE_NAME;
            this.DO_NAME = DO_NAME;
            this.TITLE = TITLE;
            this.AllowAdd = AllowAdd;
            this.FORM_CLASS_NAME = FORM_CLASS_NAME;
            this.IMAGE_NAME = IMAGE_NAME;
            this.ID_FIELD = ID_FIELD;
            this.SO_LG_FIELD = SO_LG_FIELD;
            this.TR_LG_FIELD = TR_LG_FIELD;
            this.STT_FIELD = STT_FIELD; 

            PhieuTypeStore.Add(this);
        }        
        public PhieuType():this(-1,"","", "", null,"", "", "" ) { }
        public PhieuType(long ID):this(ID, "", "","", null,"", "", ""){}
        public PhieuType(long ID, string TABLE_NAME):this(ID, TABLE_NAME, "","", null,"","","") { }


        public long GetTypeID()
        {
            return ID;
        }
        public long GetNhomPhieu()
        {
            return NHOM_PHIEU;
        }
        public string GetTableName()
        {
            return TABLE_NAME;
        }
        public string GetDOName()
        {
            return DO_NAME;
        }
        public string GetTitle()
        {
            return TITLE;
        }
        public string GetFormClassName()
        {
            return FORM_CLASS_NAME;
        }
        public string GetImageName() {
            return IMAGE_NAME;
        }
        public string GetIDField()
        {
            return ID_FIELD;
        }
        public string GetSOLGField()
        {
            return SO_LG_FIELD;
        }
        public string GetTRLGField()
        {
            return TR_LG_FIELD;
        }
        public string GetSTTField()
        {
            return STT_FIELD;
        }
        public void AllowCreatePhieu(params PhieuType[] Phieu)
        {
            CanCreateList.AddRange(Phieu);
        }
        public bool CanCreatePhieu(PhieuType Phieu)
        {
            return CanCreateList.Contains(Phieu);
        }

        //public bool CanCreatePhieu(object DO)
        //{
        //    foreach (PhieuType phieu in CanCreateList)
        //    {
        //        if (phieu.GetDOName().Equals(DO.GetType().FullName))
        //            return true;
        //    }
        //    return false;
        //}
    }


    /// <summary>
    /// Được dùng để lấy ID khi In Phiếu
    /// </summary>
    public interface IDDOPhieu
    {
        Int64 GetID();
    }

}
