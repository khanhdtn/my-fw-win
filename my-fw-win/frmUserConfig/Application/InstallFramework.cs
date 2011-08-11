using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win
{
    public class InstallFramework : IPLInstall
    {
        #region IPLInstall Members

        public string GetInstallSQL()
        {
            return @"CREATE TABLE DEMO_INSTALL (
                    ID             A_BIG_ID NOT NULL /* A_BIG_ID = BIGINT */,
                    MA_NV          VARCHAR(10),
                    TEN_NV         VARCHAR(100) NOT NULL,
                    CMND           A_STR_SHORT /* A_STR_SHORT = VARCHAR(100) */,
                    NGAY_SINH      A_DATE_TIME /* A_DATE_TIME = TIMESTAMP */,
                    DIEN_THOAI     A_STR_SHORT /* A_STR_SHORT = VARCHAR(100) */,
                    DIA_CHI        A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */,
                    EMAIL          A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */,
                    VISIBLE_BIT    A_BOOL_NULL /* A_BOOL_NULL = CHAR(1) */,
                    DEPARTMENT_ID  A_BIG_ID /* A_BIG_ID = BIGINT */
                    );";                    
        }

        public string GetUnInstallSQL()
        {
            return "drop table DEMO_INSTALL";
        }

        public string CheckSQL()
        {
            return "select 1 from DEMO_INSTALL;";
        }

        #endregion
    }
}
