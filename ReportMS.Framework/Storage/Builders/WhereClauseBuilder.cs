using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ReportMS.Framework.Storage.Builders
{
    /// <summary>
    /// 建制 WHERE 子句的基类
    /// </summary>
    /// <typeparam name="TDataObject">能够对应到关系型数据库中具体表的对象类型</typeparam>
    public abstract class WhereClauseBuilder<TDataObject> : ExpressionVisitor, IWhereClauseBuilder<TDataObject>
        where TDataObject : class, new()
    {
        #region Private Fields
        private readonly StringBuilder sb = new StringBuilder();
        private readonly Dictionary<string, object> parameterValues = new Dictionary<string, object>();
        private readonly IStorageMappingResolver mappingResolver;
        private bool startsWith;
        private bool endsWith;
        private bool contains;
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>WhereClauseBuilderBase&lt;T&gt;</c> 实例. 
        /// </summary>
        /// <param name="mappingResolver">用于生成映射指定名存储器关系映射解析器实例<c>IStorageMappingResolver</c></param>
        protected WhereClauseBuilder(IStorageMappingResolver mappingResolver)
        {
            this.mappingResolver = mappingResolver;
        }

        #endregion

        #region Private Methods

        private void Out(string s)
        {
            sb.Append(s);
        }

        private void OutMember(Expression instance, MemberInfo member)
        {
            var mappedFieldName = mappingResolver.ResolveFieldName<TDataObject>(member.Name);
            Out(mappedFieldName);
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值将在 "子句" 中表示 "AND" 操作的字符串。
        /// </summary>
        protected virtual string And
        {
            get { return "AND"; }
        }

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值将在 "子句" 中表示 "OR" 操作的字符串。
        /// </summary>
        protected virtual string Or
        {
            get { return "OR"; }
        }

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值将在 "子句" 中表示 "=" 操作的字符串。
        /// </summary>
        protected virtual string Equal
        {
            get { return "="; }
        }

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值将在 "子句" 中表示 "NOT" 操作的字符串。
        /// </summary>
        protected virtual string Not
        {
            get { return "NOT"; }
        }

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值将在 "子句" 中表示 "不等于" 操作的字符串。
        /// </summary>
        protected virtual string NotEqual
        {
            get { return "<>"; }
        }

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值将在 "子句" 中表示 "LIKE" 操作的字符串。
        /// </summary>
        protected virtual string Like
        {
            get { return "LIKE"; }
        }

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值将在 "子句" 中表示 "LIKE" 操作的站位符 '%' 字符串。
        /// </summary>
        protected virtual char LikeSymbol
        {
            get { return '%'; }
        }

        /// <summary>
        /// 获取一个<c>System.String</c>值，该值是指数据库参数所使用的字符的字符 (如 '@' 或 '?' 或 ':')。
        /// </summary>
        protected internal abstract char ParameterChar { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.BinaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            string str;
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    str = "+";
                    break;
                case ExpressionType.AddChecked:
                    str = "+";
                    break;
                case ExpressionType.AndAlso:
                    str = this.And;
                    break;
                case ExpressionType.Divide:
                    str = "/";
                    break;
                case ExpressionType.Equal:
                    str = this.Equal;
                    break;
                case ExpressionType.GreaterThan:
                    str = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    str = ">=";
                    break;
                case ExpressionType.LessThan:
                    str = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    str = "<=";
                    break;
                case ExpressionType.Modulo:
                    str = "%";
                    break;
                case ExpressionType.Multiply:
                    str = "*";
                    break;
                case ExpressionType.MultiplyChecked:
                    str = "*";
                    break;
                case ExpressionType.Not:
                    str = this.Not;
                    break;
                case ExpressionType.NotEqual:
                    str = this.NotEqual;
                    break;
                case ExpressionType.OrElse:
                    str = this.Or;
                    break;
                case ExpressionType.Subtract:
                    str = "-";
                    break;
                case ExpressionType.SubtractChecked:
                    str = "-";
                    break;
                default:
                    throw new NotSupportedException(string.Format(Resources.EX_EXPRESSION_NODE_TYPE_NOT_SUPPORT, node.NodeType.ToString()));
            }

            this.Out("(");
            this.Visit(node.Left);
            this.Out(" ");
            this.Out(str);
            this.Out(" ");
            this.Visit(node.Right);
            this.Out(")");
            return node;
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TDataObject) ||
                typeof(TDataObject).IsSubclassOf(node.Member.DeclaringType))
            {
                var mappedFieldName = mappingResolver.ResolveFieldName<TDataObject>(node.Member.Name);
                Out(mappedFieldName);
            }
            else
            {
                var info = node.Member as FieldInfo;
                if (info != null)
                {
                    var ce = node.Expression as ConstantExpression;
                    var fi = info;
                    if (ce != null)
                    {
                        var fieldValue = fi.GetValue(ce.Value);
                        var constantExpr = Expression.Constant(fieldValue);
                        Visit(constantExpr);
                    }
                }
                else
                    throw new NotSupportedException(string.Format(Resources.EX_MEMBER_TYPE_NOT_SUPPORT, node.Member.GetType().FullName));
            }
            return node;
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ConstantExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            var paramName = string.Format("{0}{1}", ParameterChar, Utils.GetUniqueIdentifier(5));
            this.Out(paramName);
            if (parameterValues.ContainsKey(paramName)) return node;

            object v;
            if (this.startsWith && node.Value is string)
            {
                this.startsWith = false;
                v = node.Value.ToString() + this.LikeSymbol;
            }
            else if (this.endsWith && node.Value is string)
            {
                this.endsWith = false;
                v = this.LikeSymbol + node.Value.ToString();
            }
            else if (this.contains && node.Value is string)
            {
                this.contains = false;
                v = this.LikeSymbol + node.Value.ToString() + this.LikeSymbol;
            }
            else
            {
                v = node.Value;
            }
            this.parameterValues.Add(paramName, v);
            return node;
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MethodCallExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            this.Out("(");
            this.Visit(node.Object);
            if (node.Arguments == null || node.Arguments.Count != 1)
                throw new NotSupportedException(Resources.EX_INVALID_METHOD_CALL_ARGUMENT_NUMBER);
            var expr = node.Arguments[0];
            switch (node.Method.Name)
            {
                case "StartsWith":
                    this.startsWith = true;
                    this.Out(" ");
                    this.Out(this.Like);
                    this.Out(" ");
                    break;
                case "EndsWith":
                    this.endsWith = true;
                    this.Out(" ");
                    this.Out(this.Like);
                    this.Out(" ");
                    break;
                case "Equals":
                    this.Out(" ");
                    this.Out(Equal);
                    this.Out(" ");
                    break;
                case "Contains":
                    this.contains = true;
                    this.Out(" ");
                    this.Out(Like);
                    this.Out(" ");
                    break;
                default:
                    throw new NotSupportedException(string.Format(Resources.EX_METHOD_NOT_SUPPORT, node.Method.Name));
            }
            if (expr is ConstantExpression || expr is MemberExpression)
                this.Visit(expr);
            else
                throw new NotSupportedException(string.Format(Resources.EX_METHOD_CALL_ARGUMENT_TYPE_NOT_SUPPORT, expr.GetType().ToString()));
            this.Out(")");
            return node;
        }

        #region Protected NotSupported Methods

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.BlockExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitBlock(BlockExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.CatchBlock"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ConditionalExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DebugInfoExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DefaultExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.DynamicExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ElementInit"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.GotoExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitGoto(GotoExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.Expression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitExtension(Expression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.IndexExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitIndex(IndexExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.InvocationExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LabelExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLabel(LabelExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LabelTarget"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.Expression&lt;T&gt;"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ListInitExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.LoopExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitLoop(LoopExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberAssignment"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberInitExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberListBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.MemberMemberBinding"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.NewExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitNew(NewExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.NewArrayExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.ParameterExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.RuntimeVariablesExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.SwitchExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.SwitchCase"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.TryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitTry(TryExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.TypeBinaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        /// <summary>
        /// Visits the children of <see cref="System.Linq.Expressions.UnaryExpression"/>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise,
        /// returns the original expression.</returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            throw new NotSupportedException(string.Format(Resources.EX_PROCESS_NODE_NOT_SUPPORT, node.GetType().Name));
        }

        #endregion

        #endregion

        #region IWhereClauseBuilder<T> Members

        /// <summary>
        /// 用给定的表达式对象建制 "WHERE" 子句
        /// </summary>
        /// <param name="expression">表达式对象</param>
        /// <returns>包含有建制结果的<c>WhereClauseBuildResult</c> 实例</returns>
        public WhereClauseBuildResult BuildWhereClause(Expression<Func<TDataObject, bool>> expression)
        {
            this.sb.Clear();
            this.parameterValues.Clear();
            this.Visit(expression.Body);
            var result = new WhereClauseBuildResult
            {
                ParameterValues = parameterValues,
                WhereClause = sb.ToString()
            };
            return result;
        }

        #endregion
    }
}
