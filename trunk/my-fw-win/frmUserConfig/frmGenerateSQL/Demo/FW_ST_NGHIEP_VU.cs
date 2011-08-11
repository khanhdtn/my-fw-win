using System;
using System.Collections.Generic;
using System.Text;

namespace ProtocolVN.Framework.Win.Database
{
    public class FW_ST_NGHIEP_VU : StoreProcedure
    {
        public static FW_ST_NGHIEP_VU INSTANCE = init();
        private static FW_ST_NGHIEP_VU init()
        {
            if (INSTANCE == null) INSTANCE = new FW_ST_NGHIEP_VU();
            return INSTANCE;
        }
        private  FW_ST_NGHIEP_VU(): base()
        {
            this.NAME = "FW_ST_NGHIEP_VU";
            this.RequireObjectName.AddRange( new DBObject[]{FW_DM_PHIEU.INSTANCE, FW_NGHIEP_VU_SYS.INSTANCE});
            this.DDL = @"
                    CREATE OR ALTER PROCEDURE FW_ST_NGHIEP_VU (
                        table_name type of a_str_short)
                    returns (
                        type_des varchar(400),
                        id_src type of a_chung_tu,
                        id_des type of a_chung_tu)
                    as
                    begin
                      for
                    select p.name,f.type_src,f.type_des
                    from FW_DM_PHIEU p, FW_NGHIEP_VU_SYS f
                    where p.id=f.type_des and
                    f.type_src in (select id from dm_phieu p1
                    where p1.table_name=:table_name)
                         into
                         :type_des,:id_src,:id_des
                      do
                      begin
                      suspend;
                    end
                 ";
        }
    }
}
