using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win.Database
{
    public class EX_TABLE_NAME : TableName
    {
        public static EX_TABLE_NAME INSTANCE = init();
        private static EX_TABLE_NAME init()
        {
            if (INSTANCE == null) INSTANCE = new EX_TABLE_NAME();
            return INSTANCE;
        }
        private EX_TABLE_NAME()
            : base()
        {
            this.NAME = "FW_DM_PHIEU";
            this.DDL = @"
                        CREATE TABLE FW_DM_PHIEU (
                            ID          A_CHUNG_TU NOT NULL /* A_CHUNG_TU = SMALLINT */,
                            NAME        A_STR_SHORT /* A_STR_SHORT = VARCHAR(100) */,
                            TABLE_NAME  A_STR_SHORT /* A_STR_SHORT = VARCHAR(100) */,
                            FORM        A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */,
                            DIEU_KIEN   A_STR_LONG /* A_STR_LONG = VARCHAR(400) */
                        );         

                        ALTER TABLE FW_DM_PHIEU ADD CONSTRAINT PK_FW_DM_PHIEU PRIMARY KEY (ID);           
                ";
        }
    }
}
