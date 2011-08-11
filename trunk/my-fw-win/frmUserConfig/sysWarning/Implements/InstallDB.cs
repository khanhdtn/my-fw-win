using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Win;

namespace ProtocolVN.Plugin.WarningSystem
{
    public class InstallDB : IPLInstall
    {
        #region IPLInstall Members

        public string CheckSQL()
        {
            return "";
        }

        public string GetInstallSQL()
        {
            return 
@"/******************************************************************************/
/***                               Generators                               ***/
/******************************************************************************/

CREATE GENERATOR G_WARNING;
SET GENERATOR G_WARNING TO 3;



/******************************************************************************/
/***                                 Tables                                 ***/
/******************************************************************************/



CREATE TABLE FW_DM_WARNING (
    ID           A_ID NOT NULL,
    NAME         A_STR_SHORT,
    VISIBLE_BIT  A_BOOL_NULL
);


CREATE TABLE FW_WARNING_PARAM (
    ID      A_ID NOT NULL,
    WAR_ID  A_ID,
    PARAM   A_STR_MEDIUM
);


CREATE TABLE FW_WARNINGINFO (
    ID           A_ID NOT NULL,
    USERID       A_ID,
    'TYPE'       A_ID,
    FORMNAME     A_STR_SHORT,
    DESCRIPTION  A_STR_MEDIUM,
    STATE        A_INTEGER,
    NAME         A_STR_MEDIUM,
    WARN_TYPE    A_ID
);


INSERT INTO FW_DM_WARNING (ID, NAME, VISIBLE_BIT) VALUES (1, 'Canh bao dinh ky', 'Y');
INSERT INTO FW_DM_WARNING (ID, NAME, VISIBLE_BIT) VALUES (2, 'Canh bao dau ky', 'Y');

COMMIT WORK;



/******************************************************************************/
/***                              Primary Keys                              ***/
/******************************************************************************/

ALTER TABLE FW_DM_WARNING ADD CONSTRAINT PK_FW__DM_WARNING PRIMARY KEY (ID);
ALTER TABLE FW_WARNINGINFO ADD CONSTRAINT PK_FW_WARNINGINFO PRIMARY KEY (ID);
ALTER TABLE FW_WARNING_PARAM ADD CONSTRAINT PK_FW_WARNING_PARAM PRIMARY KEY (ID);


/******************************************************************************/
/***                              Foreign Keys                              ***/
/******************************************************************************/

ALTER TABLE FW_WARNINGINFO ADD CONSTRAINT FK_FW_WARNINGINFO_2 FOREIGN KEY ('TYPE') REFERENCES FW_DM_WARNING (ID);
ALTER TABLE FW_WARNING_PARAM ADD CONSTRAINT FK_FW_WARNING_PARAM_1 FOREIGN KEY (WAR_ID) REFERENCES FW_WARNINGINFO (ID) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE FW_WARNING_PARAM ADD CONSTRAINT FK_WARNING_PARAM_1 FOREIGN KEY (WAR_ID) REFERENCES FW_WARNINGINFO (ID) ON DELETE CASCADE ON UPDATE CASCADE;";
        }

        public string GetUnInstallSQL()
        {
            return @"
                    DROP TABLE FW_WARNING_PARAM
                    DROP TABLE FW_WARNINGINFO;
                    DROP TABLE FW_DM_WARNING;
                    DROP GENERATOR G_WARNING;
            ";
        }

        #endregion
    }
}
