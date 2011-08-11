using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win.Database
{
    public class FW_DM_PHIEU : TableName
    {
        public static FW_DM_PHIEU INSTANCE = init();
        private static FW_DM_PHIEU init()
        {
            if (INSTANCE == null) INSTANCE = new FW_DM_PHIEU();
            return INSTANCE;
        }

        private FW_DM_PHIEU()
            : base()
        {
            this.NAME = "FW_DM_PHIEU";
            this.DDL =  @"
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
