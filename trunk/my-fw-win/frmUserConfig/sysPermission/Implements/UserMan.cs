using System.Data;
using DevExpress.XtraGrid;
using ProtocolVN.Framework.Core;
using System.Collections.Generic;
using System;
using DevExpress.XtraEditors;
namespace ProtocolVN.Framework.Win
{
    public class UserManUtil
    {
        public static BaoBieu getReport(DataRow row)
        {
            BaoBieu report = new BaoBieu();

            report.id = HelpNumber.ParseInt64(row["ID"]);
            report.keyid = row["KEYID"].ToString().Trim();
            report.reportName = row["NAME"].ToString().Trim();
            report.isRead = (row["ISREAD_BIT"].ToString().Equals("Y") ? true : false);

            return report;
        }

        public static Feature getFeature(DataRow row)
        {
            Feature feature = new Feature();

            feature.id = HelpNumber.ParseInt64(row["ID"]);
            feature.featureName = row["NAME"].ToString().Trim();
            feature.isRead = (row["isread_bit"].ToString().Equals("Y") ? true : false);
            feature.isInsert = (row["isinsert_bit"].ToString().Equals("Y") ? true : false);
            feature.isUpdate = (row["isupdate_bit"].ToString().Equals("Y") ? true : false);
            feature.isDelete = (row["isdelete_bit"].ToString().Equals("Y") ? true : false);
            feature.isPrint = (row["isprint_bit"].ToString().Equals("Y") ? true : false);
            feature.isExport = (row["isexport_bit"].ToString().Equals("Y") ? true : false);
            return feature;
        }

        public static void getAllUserByGroupId(GridControl gridControlThanhPhanGroupUser, ref DataSet ds, long groupid, bool isNew)
        {
            string select = "select group_cat.groupid, group_cat.groupname, user_cat.userid, user_cat.username, DM_NHAN_VIEN.name as employee_name ,department.name as department_name  from group_cat " +
            "inner join group_user_rel on group_cat.groupid=group_user_rel.groupid " +
            "join user_cat on user_cat.userid=group_user_rel.userid " +
            "join DM_NHAN_VIEN  on DM_NHAN_VIEN.id=  user_cat.employee_id " +
            "join department on department.id=DM_NHAN_VIEN.department_id where 1=1";
            //if (ds == null)
            //{
            //    ds = new DataSet();
            //    QueryBuilder query = new QueryBuilder(select);
            //    query.addCondition("1=1");
            //    ds = DABase.getDatabase().LoadDataSet(query, "tblUSER");
            //}
            //if (isNew == true)
            //{
            //    ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblUSER");
            //}
            ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblUSER");
            gridControlThanhPhanGroupUser.DataSource = ds.Tables[0].DefaultView;
            ds.Tables[0].DefaultView.RowFilter = "groupid =" + groupid;
        }

        public static void getAllGroupByUserId(GridControl gridControlThanhPhanGroupUser,ref DataSet ds, long Userid, bool isNew)
        {
            string select = "select  group_cat.groupid, groupname, user_cat.userid from group_cat " +
                            "inner join group_user_rel  on group_cat.groupid=group_user_rel.groupid " +
                            "join user_cat on user_cat.userid = group_user_rel.userid where 1=1";
            //if (ds == null)
            //{
            //    ds = new DataSet();
            //    ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblGROUP");
            //}
            //if (isNew == true)
            //{
            //    ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblGROUP");
            //}
            ds = DABase.getDatabase().LoadDataSet(new QueryBuilder(select), "tblGROUP");
            gridControlThanhPhanGroupUser.DataSource = ds.Tables[0].DefaultView;
            ds.Tables[0].DefaultView.RowFilter = "userid =" + Userid;
        }
    }    
}
