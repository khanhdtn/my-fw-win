using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win.Database
{
    public class FW_NGHIEP_VU_SYS : TableName
    {
        public static FW_NGHIEP_VU_SYS INSTANCE = init();
        private static FW_NGHIEP_VU_SYS init()
        {
            if (INSTANCE == null) INSTANCE = new FW_NGHIEP_VU_SYS();
            return INSTANCE;
        }

        private FW_NGHIEP_VU_SYS()
            : base()
        {
            this.NAME = "FW_NGHIEP_VU_SYS";
            this.RequireObjectName.Add(FW_DM_PHIEU.INSTANCE);
            this.DDL =  @"
                        /******************************************************************************/
                        /***                              Tables                                    ***/
                        /******************************************************************************/

                        CREATE TABLE FW_NGHIEP_VU_SYS (
                            TYPE_SRC  A_CHUNG_TU NOT NULL /* A_CHUNG_TU = SMALLINT */,
                            TYPE_DES  A_CHUNG_TU NOT NULL /* A_CHUNG_TU = SMALLINT */
                        );

                        /******************************************************************************/
                        /***                              Primary Keys                              ***/
                        /******************************************************************************/

                        ALTER TABLE FW_NGHIEP_VU_SYS ADD CONSTRAINT PK_FW_NGHIEP_VU_SYS PRIMARY KEY (TYPE_SRC, TYPE_DES);


                        /******************************************************************************/
                        /***                              Foreign Keys                              ***/
                        /******************************************************************************/

                        ALTER TABLE FW_NGHIEP_VU_SYS ADD CONSTRAINT FK_FW_NGHIEP_VU_SYS_1 FOREIGN KEY (TYPE_SRC) REFERENCES FW_DM_PHIEU (ID);
                        ALTER TABLE FW_NGHIEP_VU_SYS ADD CONSTRAINT FK_FW_NGHIEP_VU_SYS_2 FOREIGN KEY (TYPE_DES) REFERENCES FW_DM_PHIEU (ID);

                ";
        }
    }
}
