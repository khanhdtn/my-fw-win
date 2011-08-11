
/******************************************************************************/
/***                                 Tables                                 ***/
/******************************************************************************/



CREATE TABLE FW_REPORT_PARAMS (
    ID                  BIGINT NOT NULL,
    TEN_KE_TOAN_TRUONG  A_STR_SHORT
);


INSERT INTO FW_REPORT_PARAMS (ID, TEN_KE_TOAN_TRUONG) VALUES (1, 'Nguyá»…n Thanh PhÆ°á»›c');

COMMIT WORK;



/******************************************************************************/
/***                              Primary Keys                              ***/
/******************************************************************************/

ALTER TABLE FW_REPORT_PARAMS ADD CONSTRAINT PK_FW_REPORT_PARAMS PRIMARY KEY (ID);


/* Fields descriptions */

DESCRIBE FIELD ID TABLE FW_REPORT_PARAMS
'Integer column';

DESCRIBE FIELD TEN_KE_TOAN_TRUONG TABLE FW_REPORT_PARAMS
'Káº¿ toÃ¡n trÆ°á»Ÿng;TÃªn káº¿ toÃ¡n trÆ°á»Ÿng Ä‘Æ°á»£c dÃ¹ng Ä‘á»ƒ hiá»‡n thá»‹ trong bÃ¡o cÃ¡o';