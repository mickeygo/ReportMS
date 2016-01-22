using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Gear.Infrastructure;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// SQL Where 子句生成器
    /// </summary>
    public class SqlWhereClauseBuilder
    {
        private readonly char parameterChar = '@';
        private readonly List<Tuple<string, string, string, string>> parameterValues = new List<Tuple<string, string, string, string>>(); // name/operator/parameter/value
        private readonly StringBuilder whereBuilder = new StringBuilder();
        private readonly string[] operators = {"=", ">", ">=", "<", "<=", "<>"};
        private readonly string and = "AND";
        private readonly string where = "WHERE";

        private void Out(string s)
        {
            this.whereBuilder.Append(s);
        }

        private void ClearStringBuileder()
        {
            if (this.whereBuilder.Length > 0)
                this.whereBuilder.Clear();
        }

        /// <summary>
        /// 添加参数及操作符
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="operator">操作符 <c>[=, >, >=, &lt;, &lt;=, &lt;&gt;]</c> </param>
        /// <param name="value">参数值</param>
        public void AddParameterValue(string name, string @operator, string value)
        {
            if (!this.ValidateParameterNameOrAlias(name) || !this.ValidateParameterOperator(@operator)) 
                return;

            var paramName = this.GenerateUnqiueParameterName();  // 随机生成参数，唯一值
            this.parameterValues.Add(Tuple.Create(name, @operator, paramName, value));
        }

        private string GenerateUnqiueParameterName()
        {
            var parmLength = 10;
            return string.Format("{0}{1}", this.parameterChar, Utils.GetUniqueIdentifier(parmLength));
        }

        private bool ValidateParameterNameOrAlias(string name)
        {
            return !String.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[0-9a-zA-Z_]+$");
        }

        private bool ValidateParameterOperator(string @operator)
        {
            return !String.IsNullOrWhiteSpace(@operator) && this.operators.Contains(@operator);
        }

        private void GenerateWhereClause()
        {
            if (!this.parameterValues.Any()) return;

            this.Out(" ");
            this.Out(this.where);

            var index = 0;
            this.parameterValues.ForEach(parameter =>
            {
                if (index++ != 0)
                {
                    this.Out(" ");
                    this.Out(this.and);
                }

                this.Out(" ");
                this.Out("(");
                this.Out(parameter.Item1);
                this.Out(" ");
                this.Out(parameter.Item2);
                this.Out(" ");
                this.Out(parameter.Item3);
                this.Out(")");
            });
        }

        /// <summary>
        /// 获取参数及参数值
        /// </summary>
        /// <returns><c>Dictionary</c></returns>
        public IDictionary<string, string> GetParameterAndValues()
        {
            if (!this.parameterValues.Any()) 
                return new Dictionary<string, string>();

            return this.parameterValues.ToDictionary(p => p.Item3, p => p.Item4);
        }

        /// <summary>
        /// 生成 Where 子句
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            this.ClearStringBuileder();
            this.GenerateWhereClause();
            return this.whereBuilder.ToString();
        }
    }
}
