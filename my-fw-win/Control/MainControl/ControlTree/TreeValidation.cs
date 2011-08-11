using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using ProtocolVN.Framework.Win;
using System.Data;

using DevExpress.XtraEditors;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.ComponentModel;
using ProtocolVN.Framework.Core;

namespace ProtocolVN.Framework.Win
{
    public class TreeValidation
    {
        public static bool CheckDuplicateFieldNode(TreeList tree, DataSet TreeDataSet, ValidateNodeEventArgs e, string[] FieldChecks, string[] Subject)
        {
            TreeListNode node = e.Node;
            for (int j = 0; j < FieldChecks.Length; j++)
            {
                int count = 0;
                for (int i = 0; i < TreeDataSet.Tables[0].Rows.Count; i++)
                {
                    if (TreeDataSet.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (node[FieldChecks[j]] == null || node[FieldChecks[j]] == DBNull.Value)
                        {

                        }
                        else
                        {
                            string tmp = node[FieldChecks[j]].ToString().Trim();
                            if (TreeDataSet.Tables[0].Rows[i][FieldChecks[j]] != null &&
                            TreeDataSet.Tables[0].Rows[i][FieldChecks[j]] != DBNull.Value &&
                            tmp == TreeDataSet.Tables[0].Rows[i][FieldChecks[j]].ToString())
                            {
                                count++;
                                if (count >= 2) break;
                            }
                        }
                    }
                }
                if (tree.State == TreeListState.Editing && count == 1)
                {
                    tree.SetColumnError(tree.Columns[FieldChecks[j]], "Thông tin " + Subject[j] + " đã sử dụng!");
                    e.Valid = false;
                }
                if (count > 1)
                {
                    tree.SetColumnError(tree.Columns[FieldChecks[j]], "Thông tin " + Subject[j] + " đã sử dụng!");
                    e.Valid = false;
                }
                else
                {
                    tree.SetColumnError(tree.Columns[FieldChecks[j]], "");
                }
            }
            tree.InvalidNodeException += delegate(object sender, InvalidNodeExceptionEventArgs ex)
            {
                ex.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            };
            return e.Valid;
        }

        #region ValidateNode
        #region Hàm không dùng
        //HUYLX :Có thể bỏ đi
        //private static void SetError(DataRow Row, string FieldName, string ErrMsg, OutputError Output)
        //{
        //    switch (Output)
        //    {
        //        case OutputError.ERROR_PROVIDER:
        //            Row.SetColumnError(FieldName, ErrMsg);
        //            break;
        //    }
        //}
        //HUYLX: Có thể bỏ đi
        //private static void ClearError(DataRow Row, string FieldName, OutputError Output)
        //{
        //    switch (Output)
        //    {
        //        case OutputError.ERROR_PROVIDER:
        //            Row.SetColumnError(FieldName, "");
        //            break;
        //    }
        //}
        #endregion

        private static void SetError(DataRow Row, string FieldName, string ErrMsg)
        {
            Row.SetColumnError(FieldName , ErrMsg);
        }
        private static void ClearError(DataRow Row, string FieldName)
        {
            Row.SetColumnError(FieldName , "");
        }

        private static string GetErrMsg(CheckType Check, string Subject)
        {
            if (Subject == null) return "ERROR DEV";
            switch (Check)
            {
                case CheckType.IntALessB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.IntALessEqualB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.IntGreater0:
                    return ErrorMsgLib.errorGreater0(Subject);
                case CheckType.IntGreaterEqual0:
                    return ErrorMsgLib.errorGreaterEqual0(Subject);
                case CheckType.IntGreaterZero:
                    return ErrorMsgLib.errorGreater0(Subject);

                case CheckType.DecALessB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.DecALessEqualB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.DecGreater0:
                    return ErrorMsgLib.errorGreater0(Subject);
                case CheckType.DecGreaterEqual0:
                    return ErrorMsgLib.errorGreaterEqual0(Subject);
                case CheckType.DecGreaterZero:
                    return ErrorMsgLib.errorGreater0(Subject);

                case CheckType.DateALessB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.DateALessEqualB:
                    return ErrorMsgLib.errorAOpB(Subject);
                case CheckType.RequireDate:
                    return ErrorMsgLib.errorRequired(Subject);
                case CheckType.OptionDate:
                    return ErrorMsgLib.errorRequired(Subject);

                case CheckType.OptionEmail:
                    return ErrorMsgLib.errorEmail(Subject);
                case CheckType.RequireEmail:
                    return ErrorMsgLib.errorEmail(Subject);
                case CheckType.OptionMaxLength:
                    return ErrorMsgLib.errorMaxLength(Subject);
                case CheckType.RequireMaxLength:
                    return ErrorMsgLib.errorMaxLength(Subject);
                case CheckType.Required:
                    return ErrorMsgLib.errorRequired(Subject);
            }
            return Subject;
        }

        /// <summary>Kiểm tra và đánh dấu lỗi
        /// </summary>
        /// <returns></returns>
        public static bool ValidateNode(TreeList Tree, TreeListNode Node, FieldNameCheck[] CheckList)
        {
            if (CheckList == null) return true;
            bool isError = true;
        
            DataRow Row = ((DataRowView)Tree.GetDataRecordByNode(Node)).Row; 

            for (int i = 0; i < CheckList.Length; i++)
            {
                string FieldName = CheckList[i].FieldName;
                string Data = null;
                if (Row[FieldName] != null) Data = Row[FieldName].ToString();
                for (int j = 0; j < CheckList[i].Types.Length; j++)
                {
                    string ErrMsg = "";
                    if (CheckList[i].Subject != null)
                        ErrMsg = GetErrMsg(CheckList[i].Types[j], CheckList[i].Subject);
                    else
                        ErrMsg = CheckList[i].ErrMsgs[j];
                    switch (CheckList[i].Types[j])
                    {
                        #region 1. Số thực
                        case CheckType.DecGreaterZero:
                            if (HelpIsCheck.isDecGreater0(Data))
                                ClearError(Row , FieldName);
                            else
                            {
                                SetError(Row , FieldName , ErrMsg);
                                isError = false;
                            }
                            break;

                        case CheckType.DecGreater0:
                            if (HelpIsCheck.isDecGreater0(Data))
                                ClearError(Row , FieldName);
                            else
                            {
                                SetError(Row , FieldName , ErrMsg); 
                                isError = false;
                            }
                            break;

                        case CheckType.DecGreaterEqual0:
                            if (HelpIsCheck.isDecGreaterEqual0(Data))
                                ClearError(Row , FieldName);
                            else
                            {
                                SetError(Row , FieldName , ErrMsg);
                                isError = false;
                            }
                            break;
                        #endregion
                        #region 1. Số nguyên
                        case CheckType.IntGreaterZero:
                            if (HelpIsCheck.isIntGreater0(Data))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;

                        case CheckType.IntGreater0:
                            if (HelpIsCheck.isIntGreater0(Data))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;

                        case CheckType.IntGreaterEqual0:
                            if (HelpIsCheck.isIntGreaterEqual0(Data))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;
                        #endregion
                        #region 1. Email
                        case CheckType.RequireEmail:
                            if (Data != null && Data != "" && HelpIsCheck.isValidEmail(Data))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;
                        case CheckType.OptionEmail:
                            if (Data == null || Data == "" || HelpIsCheck.isValidEmail(Data))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;
                        #endregion
                        #region 1. Chuỗi
                        case CheckType.RequireMaxLength:
                            if (Data != null && Data != "" && HelpIsCheck.IsMaxLength(Data, (int)CheckList[i].Params[j]))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;
                        case CheckType.OptionMaxLength:
                            if (Data == null || Data == "" || HelpIsCheck.IsMaxLength(Data, (int)CheckList[i].Params[j]))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;
                        #endregion
                        #region 1. Date
                        case CheckType.RequireDate:
                            if (Data != null && Data != "" && Node[FieldName] is DateTime && !HelpIsCheck.isBlankDate((DateTime)Node[FieldName]))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;

                        case CheckType.OptionDate:
                            if (Data == null || Data == "" || Node[FieldName] is DateTime)
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;

                        case CheckType.DateALessB:
                            DateTime B;
                            if (CheckList[i].Params[j] is DateTime) 
                                B = (DateTime)CheckList[i].Params[j];
                            else 
                                B = (DateTime)Node[CheckList[i].Params[j].ToString()];

                            if (HelpIsCheck.IsALessB((DateTime)Node[FieldName], B))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;

                        case CheckType.DateALessEqualB:
                            DateTime C;
                            if (CheckList[i].Params[j] is DateTime) C = (DateTime)CheckList[i].Params[j];
                            else C = (DateTime)Row[CheckList[i].Params[j].ToString()];
                            if (HelpIsCheck.IsALessEqualB((DateTime)Node[FieldName], C))
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;
                        #endregion
                        #region 1. Other
                        case CheckType.Required:
                            if (Data != null && Data != "")
                                ClearError(Row, FieldName);
                            else
                            {
                                SetError(Row, FieldName, ErrMsg);
                                isError = false;
                            }
                            break;
                        #endregion
                    }

                    if (Row.HasErrors) break;
                }
            }
            return isError;
        }

        #endregion

        public static bool ValidateTree(TreeList Tree, FieldNameCheck[] CheckList)
        {
            bool flag = true;
            DataView view = (DataView)Tree.DataSource;
            ////PHUOCNC HUNG------------------------------------------------------
            //view.Table.AcceptChanges();//ngan chan truong hop row delete
            //// ---------------------------------------------------------   
            for (int i = 0; i < view.DataViewManager.DataSet.Tables[0].Rows.Count; i++)
            {
                if (view.DataViewManager.DataSet.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    if (!ValidateNode(Tree,Tree.Nodes[i],CheckList))
                        flag = false;
                }
            }
            return flag;
        }

    }
}
