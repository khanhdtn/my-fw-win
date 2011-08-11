using System.Drawing;
using System.IO;
using ProtocolVN.Framework.Core;
using System.Data.Common;
using System.Data;
namespace ProtocolVN.Framework.Win
{
    public class CompanyInfo{
        public string name;
        public string tradeName;
        public string representative;
        public string address;
        public string phone;
        public string fax;
        public string email;
        public string website;
        public byte[] logo;
        public string accountNo;
        public string bankName;
        public string taxCode;
        public byte[] headerletter;

        public CompanyInfo() { }
        public CompanyInfo(string name, string tradeName, string representative,
            string address,string phone,string fax,string email,string website,byte[] logo,
            string accountNo, string bankName, string taxCode, byte[] headerletter)
        {
            this.name = name;
            this.tradeName = tradeName;
            this.representative = representative;
            this.address = address;
            this.phone = phone;
            this.fax = fax;
            this.email = email;
            this.website = website;
            this.logo = logo;
            this.accountNo = accountNo;
            this.bankName = bankName;
            this.taxCode = taxCode;
            this.headerletter = headerletter;
        }

        public void update()
        {
            DACompanyInfo.Instance.updateCompanyInfo(this.name, this.tradeName, this.representative, 
                        this.address, this.phone, this.fax, this.email, this.website, this.logo, 
                        this.accountNo, this.bankName, this.taxCode, this.headerletter);
        }

        public void load()
        {
            CompanyInfo tmp = DACompanyInfo.Instance.load();

            this.name = tmp.name;
            this.tradeName = tmp.tradeName;
            this.representative = tmp.representative;
            this.address = tmp.address;
            this.phone = tmp.phone;
            this.fax = tmp.fax;
            this.email = tmp.email;
            this.website = tmp.website;
            this.logo = tmp.logo;
            this.accountNo = tmp.accountNo;
            this.bankName = tmp.bankName;
            this.taxCode = tmp.taxCode;
            this.headerletter = tmp.headerletter;
        }

        public static byte[] readBitmap2ByteArray(string fileName)
        {
            return HelpImage.GetImage(fileName);

            //using (Bitmap image = new Bitmap(fileName))
            //{
            //    MemoryStream stream = new MemoryStream();
            //    image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            //    return stream.ToArray();
            //}
        }

        public static Bitmap displayImageLogo(byte[] logo)
        {
            if (logo == null) return null;
            MemoryStream stream = new MemoryStream(logo);
            Bitmap image = new Bitmap(stream);
            return image;
        }
    }

    public class DACompanyInfo
    {
        public static readonly DACompanyInfo Instance = new DACompanyInfo();
        private DACompanyInfo() { }

        public CompanyInfo load()
        //PHUOC OK
        {
            CompanyInfo company = new CompanyInfo();
            DatabaseFB db = DABase.getDatabase();
            DbCommand dbSelect = db.GetSQLStringCommand("SELECT NAME,TRADENAME,REPRESENTATIVE,ADDRESS,PHONE,FAX,EMAIL,WEBSITE,LOGO,ACCOUNTNO,BANKNAME,TAXCODE,HEADER_LETTER" +
                                                        " FROM COMPANY_INFO WHERE ID=@ID");
            db.AddInParameter(dbSelect, "@ID", DbType.Int64, 1);
            IDataReader reader = db.ExecuteReader(dbSelect);
            if (reader.Read())
            {
                byte[] logo = null;
                byte[] headerletter = null;
                if (!reader["LOGO"].ToString().Equals(""))
                {
                    logo = (byte[])reader["LOGO"];
                }
                if (!reader["HEADER_LETTER"].ToString().Equals(""))
                {
                    headerletter = (byte[])reader["HEADER_LETTER"];
                }
                company = new CompanyInfo(reader["NAME"].ToString(), reader["TRADENAME"].ToString(), reader["REPRESENTATIVE"].ToString(),
                    reader["ADDRESS"].ToString(), reader["PHONE"].ToString(), reader["FAX"].ToString(), reader["EMAIL"].ToString(),
                    reader["WEBSITE"].ToString(), logo, reader["ACCOUNTNO"].ToString(), reader["BANKNAME"].ToString(), reader["TAXCODE"].ToString(), headerletter);
            }
            reader.Close();
            return company;
        }

        public void updateCompanyInfo(string name, string tradeName,
           string representative, string address, string phone, string fax,
           string email, string website, byte[] logo, string accountNo, string bankName, string taxCode, byte[] headerletter)
        //PHUOC OK
        {
            DatabaseFB db = DABase.getDatabase();            
            DataSet ds = db.LoadDataSet("select * from COMPANY_INFO WHERE ID=1", "COMPANY_INFO");
            ds.Tables[0].Rows[0]["name"] = name;
            ds.Tables[0].Rows[0]["tradename"] = tradeName;
            ds.Tables[0].Rows[0]["representative"] = representative;
            ds.Tables[0].Rows[0]["address"] = address;
            ds.Tables[0].Rows[0]["phone"] = phone;
            ds.Tables[0].Rows[0]["fax"] = fax;
            ds.Tables[0].Rows[0]["email"] = email;
            ds.Tables[0].Rows[0]["website"] = website;
            ds.Tables[0].Rows[0]["logo"] = logo;
            ds.Tables[0].Rows[0]["header_letter"] = headerletter;
            ds.Tables[0].Rows[0]["accountno"] = accountNo;
            ds.Tables[0].Rows[0]["bankname"] = bankName;
            ds.Tables[0].Rows[0]["taxcode"] = taxCode;
            DABase.getDatabase().UpdateDataSet(ds);

            //DbCommand dbUpdate = db.GetSQLStringCommand("UPDATE COMPANY_INFO SET NAME=@name,TRADENAME=@tradename" +
            //",REPRESENTATIVE=@representative,ADDRESS=@address,PHONE=@phone,FAX=@fax" +
            //",EMAIL=@email,WEBSITE=@website,LOGO=@logo,ACCOUNTNO=@accountno,BANKNAME=@bankname,TAXCODE=@taxcode WHERE ID=1");
            //db.AddInParameter(dbUpdate, "@name", DbType.String, name);
            //db.AddInParameter(dbUpdate, "@tradename", DbType.String, tradeName);
            //db.AddInParameter(dbUpdate, "@representative", DbType.String, representative);
            //db.AddInParameter(dbUpdate, "@address", DbType.String, address);
            //db.AddInParameter(dbUpdate, "@phone", DbType.String, phone);
            //db.AddInParameter(dbUpdate, "@fax", DbType.String, fax);
            //db.AddInParameter(dbUpdate, "@email", DbType.String, email);
            //db.AddInParameter(dbUpdate, "@website", DbType.String, website);
            //db.AddInParameter(dbUpdate, "@logo", DbType.Binary, logo);
            //db.AddInParameter(dbUpdate, "@accountno", DbType.String, accountNo);
            //db.AddInParameter(dbUpdate, "@bankname", DbType.String, bankName);
            //db.AddInParameter(dbUpdate, "@taxcode", DbType.String, taxCode);
            //db.ExecuteNonQuery(dbUpdate);
        }
    }
}