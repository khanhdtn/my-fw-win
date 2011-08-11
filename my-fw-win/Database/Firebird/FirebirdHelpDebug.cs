using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using ProtocolVN.Framework.Core;
using System.Data;
using System.Windows.Forms;

namespace ProtocolVN.Framework.Win
{
    public class FirebirdHelpDebug : IDBHelpDebug
    {
        public string GetLastestFbException(List<Exception> listExp)
        {
            string ret = "";
            if (listExp != null)
            {
                for (int i = listExp.Count - 1; i <= 0; i--)
                {
                    if (listExp[i] is FbException)
                    {
                        FbException FbExp = (FbException)listExp[i];
                        if (FbExp.ErrorCode > 0 && FbExp.ErrorCode < 100)
                        {
                            return DABase.getDatabase().GetException(FbExp.ErrorCode);
                        }
                        else
                        {
                            ret = DBError.GetError("" + FbExp.ErrorCode);
                            if (ret != "" + FbExp.ErrorCode)
                                return ret;
                        }

                        for (int j = FbExp.Errors.Count - 1; j >= 0; j--)
                        {
                            if (FbExp.Errors[j].Number > 0 && FbExp.Errors[j].Number < 100)
                            {
                                return DABase.getDatabase().GetException(FbExp.Errors[j].Number);
                            }
                            else
                            {
                                ret = DBError.GetError("" + FbExp.Errors[j].Number);
                                if (ret != "" + FbExp.Errors[j].Number)
                                {
                                    return ret;
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public string GetUserErrorMsg(List<Exception> listExp)
        {
            string ret = "";
            if (listExp != null)
            {
                for (int i = listExp.Count - 1; i >= 0; i--)
                {
                    if (listExp[i] is FbException)
                    {
                        FbException FbExp = (FbException)listExp[i];
                        if (FbExp.ErrorCode > 0 && FbExp.ErrorCode < 100)
                        {
                            return DABase.getDatabase().GetException(FbExp.ErrorCode);
                        }
                    }
                }
            }
            return ret;
        }

        public void ShowExceptionInfo(List<Exception> listExp, string Msg)
        {
            try
            {
                if (FrameworkParams.isSupportDeveloper)
                {
                    DataSet DataInfo = new DataSet("ExceptionInfo");
                    DataTable tb = new DataTable("Info");
                    tb.Columns.AddRange(new DataColumn[] { 
                            new DataColumn("SOURCE",Type.GetType("System.String")),
                            new DataColumn("MESSAGE",Type.GetType("System.String")),
                            new DataColumn("NUMBER",Type.GetType("System.String")),
                            new DataColumn("DETAIL",Type.GetType("System.String")),
                            new DataColumn("STACK_TRACE", Type.GetType("System.String"))
                        }
                    );
                    DataInfo.Tables.Add(tb);
                    int k = 1;
                    for (int i = listExp.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            if (listExp[i] is FbException)
                            {
                                DataRow dr = null;

                                FbException FbExp = (FbException)listExp[i];
                                dr = DataInfo.Tables[0].NewRow();
                                dr["SOURCE"] = "+" + FbExp.Source + "--" + FbExp.TargetSite.ToString();
                                dr["MESSAGE"] = "" + (k++) + " " + FbExp.Message + listExp[i].ToString();
                                dr["NUMBER"] = FbExp.ErrorCode;
                                dr["DETAIL"] = DBError.GetError("" + FbExp.ErrorCode);
                                dr["STACK_TRACE"] = FbExp.StackTrace;
                                DataInfo.Tables[0].Rows.Add(dr);
                                for (int j = FbExp.Errors.Count - 1; j >= 0; j--)
                                {
                                    dr = DataInfo.Tables[0].NewRow();
                                    dr["SOURCE"] = "";// FbExp.Source + "--" + FbExp.TargetSite.ToString();
                                    dr["MESSAGE"] = "" + (k - 1) + "." + (FbExp.Errors.Count - j + 1) + " " + FbExp.Errors[j].Message;
                                    dr["NUMBER"] = FbExp.Errors[j].Number;
                                    dr["DETAIL"] = DBError.GetError("" + FbExp.Errors[j].Number);
                                    dr["STACK_TRACE"] = DBNull.Value;
                                    DataInfo.Tables[0].Rows.Add(dr);
                                }
                            }
                            else if (listExp[i] is GDBException)
                            {
                                GDBException Ex1 = (GDBException)listExp[i];
                                Exception Ex = Ex1.ex;
                                DataRow dr = DataInfo.Tables[0].NewRow();
                                dr["SOURCE"] = "+" + Ex.Source + "--" + Ex.TargetSite.ToString();
                                dr["MESSAGE"] = "" + (k++) + " " + Ex.Message + Ex.ToString();
                                dr["NUMBER"] = "---";
                                dr["DETAIL"] = "---";
                                dr["STACK_TRACE"] = Ex.StackTrace;
                                DataInfo.Tables[0].Rows.Add(dr);
                            }
                            else if (listExp[i] is PLException)
                            {
                                PLException ex1 = (PLException)listExp[i];
                                Exception Ex = ex1.origin;
                                DataRow dr = DataInfo.Tables[0].NewRow();
                                dr["SOURCE"] = Ex.Source + "--" + Ex.TargetSite.ToString() + "+ ClassName.Function:" + ex1.className + "." + ex1.functionName;
                                dr["MESSAGE"] = "" + (k++) + " " + Ex.Message + Ex.ToString() + "+ MsgInfo: " + ex1.msgInfo;
                                dr["NUMBER"] = "---";
                                dr["DETAIL"] = "---";
                                dr["STACK_TRACE"] = Ex.StackTrace + "+ " + ex1.sqlStatement;
                                DataInfo.Tables[0].Rows.Add(dr);
                            }
                            else
                            {
                                Exception Ex = listExp[i];
                                DataRow dr = DataInfo.Tables[0].NewRow();
                                dr["SOURCE"] = "+" + Ex.Source + "--" + Ex.TargetSite.ToString();
                                dr["MESSAGE"] = "" + (k++) + " " + Ex.Message + Ex.ToString();
                                dr["NUMBER"] = "---";
                                dr["DETAIL"] = "---";
                                dr["STACK_TRACE"] = Ex.StackTrace;
                                DataInfo.Tables[0].Rows.Add(dr);
                            }
                        }
                        catch
                        {
                        }
                    }
                    frmGridInfo frm = new frmGridInfo();
                    frm.InitData(DataInfo, Msg);
                    HelpGridColumn.CotMemoExEdit(HelpGridColumn.ThemCot(frm.viewDebug, "_StackTrace Detail", 0, 250), "STACK_TRACE");
                    HelpGridColumn.CotMemoExEdit(HelpGridColumn.ThemCot(frm.viewDebug, "_Source Detail", 1, 250), "SOURCE");
                    frm.viewDebug.OptionsBehavior.Editable = true;
                    frm.WindowState = FormWindowState.Maximized;
                    //frm.TopMost = true;
                    frm.cmdXoa.Visible = true;
                    //frm.ShowDialog();
                    frm.Show();
                }
            }
            catch
            {

            }
        }

        public void ShowExceptionInfo()
        {
            ShowExceptionInfo(PLException.GetLastestExceptions(), "Nguyên nhân vấn đề");
        }

        public void ShowExceptionInfo(List<Exception> listExp)
        {
            ShowExceptionInfo(listExp, "Debug");
        }
    }
}
