using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraEditors.Filtering;

namespace ProtocolVN.Framework.Win
{
    public struct SQLDATA
    {
        public string Filters;
        public Dictionary<string, string> Parameters;
        public Dictionary<string, object> ParameterDataTypes;
    }

    /// <summary>
    /// Lớp này mới xử lý 1 vài tình huống query thôi chứ chưa đầu đủ    
    /// </summary>
    public class FilterControlHelper
    {
        protected CriteriaOperator _criteria;
        protected DevExpress.XtraEditors.Filtering.Node _head;
        protected Dictionary<string, string> _params = null;
        protected Dictionary<string, object> _paramDataTypes = null;
        protected bool _useParams = false;
        protected Stack<string> _groupClauseStack = null;

        public FilterControlHelper(DevExpress.XtraEditors.FilterControl filterControl)
        {
            _criteria = filterControl.FilterCriteria;
            _head = (DevExpress.XtraEditors.Filtering.Node)CriteriaToTreeProcessor.GetTree(new FilterControlNodesFactory(), _criteria, null);
        }

        /// <summary>
        /// If filters are missing this will throw an argument exception
        /// </summary>
        public bool ErrorOnMissingFilters { get; set; }
        /// <summary>
        /// Set to true to use a null check on the Is Blank operation
        ///
        /// Set to false (default) to check both empty string and null on the Is Blank operation.
        /// </summary>
        public bool BlankAsNullOnly { get; set; }

        /// <summary>
        /// Gets the SQL filter compatible with the SQL database engine without parameters
        /// </summary>
        /// <returns>The SQL filter string</returns>
        public string GetSQLFilter()
        {
            SQLDATA ret = GetSQLFilter(false);
            return ret.Filters;
        }
        /// <summary>
        /// Gets the SQL filter compatible with the SQL database engine.
        /// </summary>
        /// <param name="UseParameters">Replaces strings entered with parameters (Use GetParameters() to get the filled parameters)</param>
        /// <returns>The SQL filter string</returns>
        public SQLDATA GetSQLFilter(bool UseParameters)
        {
            _useParams = UseParameters;
            _params = new Dictionary<string, string>();
            _paramDataTypes = new Dictionary<string, object>();
            _groupClauseStack = new Stack<string>();
            StringBuilder sqlQuery = new StringBuilder();
            BuildSQL(sqlQuery, _head);
            SQLDATA ret = new SQLDATA();
            ret.Filters = sqlQuery.ToString();
            // Make a copy so the internal reference isnt tainted
            if (_useParams)
            {
                ret.Parameters = new Dictionary<string, string>(_params);
                ret.ParameterDataTypes = new Dictionary<string, object>(_paramDataTypes);
            }
            return ret;
        }

        private void BuildSQL(StringBuilder SQLQuery, DevExpress.XtraEditors.Filtering.Node Node)
        {
            if (Node is GroupNode)
            {
                // Group nodes
                BuildGroupSQL(SQLQuery, (GroupNode)Node);
            }
            else if (Node is ClauseNode)
            {
                // Single leaf node
                BuildClauseSQL(SQLQuery, (ClauseNode)Node);
            }
        }

        private void BuildClauseSQL(StringBuilder SQLQuery, ClauseNode ClauseNode)
        {
            // Non-group node
            var operation = ClauseNode.Operation;
            // Check for values that arent filled in.
            if (ClauseNode.AdditionalOperands.Count > 0
                && ClauseNode.AdditionalOperands[0] is DevExpress.Data.Filtering.OperandValue
                && ((DevExpress.Data.Filtering.OperandValue)ClauseNode.AdditionalOperands[0]).Value == null)
            {
                if (ErrorOnMissingFilters)
                    throw new ArgumentException("Not all filters are properly filled in. If you need to search for blanks or nulls please use the is blank operator.");
                else
                    operation = ClauseType.IsNull;
            }
            
            CriteriaOperator[] criterias = new CriteriaOperator[ClauseNode.AdditionalOperands.Count];
            for (int k = 0; k < ClauseNode.AdditionalOperands.Count; k++)
            {
                criterias[k] = ClauseNode.AdditionalOperands[k];
            }
            
            //string[] addlOper = Array.ConvertAll<object, string>(ClauseNode.AdditionalOperands.ToArray(), 
            //    delegate(object o) { return o.ToString(); }
            //);

            string[] addlOper = Array.ConvertAll<object, string>(criterias,
                delegate(object o) { return o.ToString(); }
            );
            string oper = "";
            // Hook into parameter system
            if (_useParams)
                ConvertDataToParams(addlOper, operation, ClauseNode);
            if (addlOper.Length > 0)
            {
                oper = addlOper[0];
            }
            string stmt = "";
            string field = ClauseNode.FirstOperand.PropertyName;
            
            switch (operation)
            {
                case ClauseType.AnyOf:
                    stmt = GetAnyOf(field, addlOper);
                    break;
                case ClauseType.BeginsWith:
                    stmt = GetBeginWith(field, oper);
                    break;
                case ClauseType.Between:
                    stmt = GetBetween(field, addlOper[0], addlOper[1]);
                    break;
                case ClauseType.Contains:
                    stmt = GetContain(field, oper);
                    break;
                case ClauseType.DoesNotContain:
                    stmt = GetNotContain(field, oper);
                    break;
                case ClauseType.DoesNotEqual:
                    stmt = GetNotEqual(field, oper);
                    break;
                case ClauseType.EndsWith:
                    stmt = GetEndsWith(field, oper);
                    break;
                case ClauseType.Equals:
                    stmt = GetEqual(field, oper);
                    break;
                case ClauseType.Greater:
                    stmt = GetGreaterThan(field, oper);
                    break;
                case ClauseType.GreaterOrEqual:
                    stmt = GetGreaterThanOrEqual(field, oper);
                    break;
                case ClauseType.IsNotNull:
                    stmt = GetNotBlank(field);
                    break;
                case ClauseType.IsNull:
                    stmt = GetBlank(field);
                    break;
                case ClauseType.Less:
                    stmt = GetLessThan(field, oper);
                    break;
                case ClauseType.LessOrEqual:
                    stmt = GetLessThanOrEqual(field, oper);
                    break;
                case ClauseType.Like:
                    stmt = GetLike(field, oper);
                    break;
                case ClauseType.NoneOf:
                    stmt = GetNotAnyOf(field, oper);
                    break;
                case ClauseType.NotBetween:
                    stmt = GetNotBetween(field, addlOper[0], addlOper[1]);
                    break;
                case ClauseType.NotLike:
                    stmt = GetNotLike(field, oper);
                    break;
                default:
                    throw new NotImplementedException("Unknown enum for the node's operation type.");
            }
            if (SQLQuery.Length > 0 && !SQLQuery.ToString().EndsWith(GetStartGroup()))
                SQLQuery.AppendFormat(" {0} ", _groupClauseStack.Peek());
            SQLQuery.Append(stmt);
        }



        private void BuildGroupSQL(StringBuilder SQLQuery, GroupNode GroupNode)
        {
            if (_groupClauseStack.Count > 0 && SQLQuery.Length > 0 && !SQLQuery.ToString().EndsWith(GetStartGroup()))
                SQLQuery.AppendFormat(" {0} ", _groupClauseStack.Peek());
            _groupClauseStack.Push(GetGroupOperator(GroupNode.NodeType));
            // Start the group.
            SQLQuery.Append(GetStartGroup());
            // Build sub-node information
            foreach (object subNode in GroupNode.SubNodes)
            {
                BuildSQL(SQLQuery, (DevExpress.XtraEditors.Filtering.Node)subNode);
            }
            _groupClauseStack.Pop();
            SQLQuery.Append(GetEndGroup());
        }

        /// <summary>
        /// Converts data into parameters and adds to the dictionary collection
        /// </summary>
        /// <param name="Data"></param>
        private void ConvertDataToParams(string[] Data, ClauseType operation, ClauseNode ClauseNode)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                string s = Data[i];
                string paramName = string.Format("@PM{0}", _params.Count + 1);
                // Strip the single quotes when adding parameters.
                if (s.StartsWith("'") || s.EndsWith("'"))
                    _params.Add(paramName, s.Substring(1, s.Length - 2));
                else
                    _params.Add(paramName, s);

                object value = ((DevExpress.Data.Filtering.OperandValue)ClauseNode.AdditionalOperands[0]).Value;
                _paramDataTypes.Add(paramName, value);

                if (HelpDBExt.GetDbType(value) == System.Data.DbType.String)
                {
                    switch (operation)
                    {
                        case ClauseType.BeginsWith:
                            _params[paramName] = string.Format("{0}%", _params[paramName]);
                            _paramDataTypes[paramName] = string.Format("{0}%", _paramDataTypes[paramName].ToString());
                            break;
                        case ClauseType.EndsWith:
                            _params[paramName] = string.Format("%{0}", _params[paramName]);
                            _paramDataTypes[paramName] = string.Format("%{0}", _paramDataTypes[paramName].ToString());
                            break;
                        case ClauseType.Contains:
                            _params[paramName] = string.Format("%{0}%", _params[paramName]);
                            _paramDataTypes[paramName] = string.Format("%{0}%", _paramDataTypes[paramName].ToString());
                            break;
                        case ClauseType.DoesNotContain:
                            _params[paramName] = string.Format("%{0}%", _params[paramName]);
                            _paramDataTypes[paramName] = string.Format("%{0}%", _paramDataTypes[paramName].ToString());
                            break;
                        case ClauseType.Like:
                            break;
                        case ClauseType.NotLike:                            
                            break;
                    }
                }

                Data[i] = paramName;
            }
        }

        // Override these virtual methods if you need to adjust how the system formats SQL.

        // This is intended for use with other databases that have different syntax.

        #region Virtual Methods
        public virtual string GetStartGroup()
        {
            return "(";
        }
        public virtual string GetEndGroup()
        {
            return ")";
        }
        public virtual string GetGroupOperator(GroupType GroupOperator)
        {
            switch (GroupOperator)
            {
                case GroupType.And:
                    return "AND";
                case GroupType.NotAnd:
                    return "NOT AND";
                case GroupType.NotOr:
                    return "NOT OR";
                case GroupType.Or:
                    return "OR";
                default:
                    throw new ArgumentException("Group operator not supported");
            }
        }

        public virtual string FormatField(string Field)
        {
            // Zayeem -- To support field names qualified with respective table names
            // i.e. field names in format TableName.FieldName
            // rbarone -- Added as virtual method to allow for override
            return string.Format("[{0}]", Field.Replace(".", "].["));
        }

        public virtual string GetEqual(string Field, string Data)
        {
            return string.Format("{0} = {1}", FormatField(Field), Data);
        }

        public virtual string GetNotEqual(string Field, string Data)
        {
            return string.Format("{0} <> {1}", FormatField(Field), Data);
        }

        public virtual string GetGreaterThan(string Field, string Data)
        {
            return string.Format("{0} > {1}", FormatField(Field), Data);
        }

        public virtual string GetGreaterThanOrEqual(string Field, string Data)
        {
            return string.Format("{0} >= {1}", FormatField(Field), Data);
        }

        public virtual string GetLessThan(string Field, string Data)
        {
            return string.Format("{0} < {1}", FormatField(Field), Data);
        }
        public virtual string GetLessThanOrEqual(string Field, string Data)
        {
            return string.Format("{0} <= {1}", FormatField(Field), Data);
        }
        public virtual string GetBetween(string Field, string Data1, string Data2)
        {
            return string.Format("{0} BETWEEN {1} AND {2}", FormatField(Field), Data1, Data2);
        }
        public virtual string GetNotBetween(string Field, string Data1, string Data2)
        {
            return string.Format("{0} NOT BETWEEN {1} AND {2}", FormatField(Field), Data1, Data2);
        }
        public virtual string GetContain(string Field, string Data)
        {            
            return GetLike(Field, Data);
        }

        public virtual string GetNotContain(string Field, string Data)
        {
            return GetNotLike(Field, Data);
        }

        public virtual string GetBeginWith(string Field, string Data)
        {
            return GetLike(Field, Data);
        }

        public virtual string GetEndsWith(string Field, string Data)
        {
            return GetLike(Field, Data);
        }

        public virtual string GetLike(string Field, string Data)
        {
            return string.Format("{0} LIKE {1}", FormatField(Field), Data);
        }

        public virtual string GetNotLike(string Field, string Data)
        {
            return string.Format("{0} NOT LIKE {1}", FormatField(Field), Data);
        }

        public virtual string GetBlank(string Field)
        {
            if (BlankAsNullOnly)
                return string.Format("{0} IS NULL", FormatField(Field));
            else
                return string.Format("({0} IS NULL OR {0} = '')", FormatField(Field));
        }

        public virtual string GetNotBlank(string Field)
        {
            if (BlankAsNullOnly)
                return string.Format("{0} IS NOT NULL", FormatField(Field));
            else
                return string.Format("{1}{0} IS NOT NULL AND {0} <> ''{2}", FormatField(Field), GetStartGroup(), GetEndGroup());
        }
        public virtual string GetAnyOf(string Field, params string[] Data)
        {
            return string.Format("{0} IN {2}{1}{3}", FormatField(Field), string.Join(", ", Data), GetStartGroup(), GetEndGroup());
        }
        public virtual string GetNotAnyOf(string Field, params string[] Data)
        {
            return string.Format("{0} NOT IN {2}{1}{3}", FormatField(Field), string.Join(", ", Data), GetStartGroup(), GetEndGroup());
        }
        #endregion

    }
}
