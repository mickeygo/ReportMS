using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// SQL SELECT 子句生成器
    /// </summary>
    public class SqlSelectClauseBuilder
    {
        private readonly List<Tuple<string, string>> selectValues = new List<Tuple<string, string>>();  // field/alias
        private readonly StringBuilder selectBuilder = new StringBuilder();
        private readonly Tuple<string, string> tableName;  // name/alias
        private readonly SelectClauseBuildMode selectClauseBuildMode;

        /// <summary>
        /// 初始化<c>SqlSelectClauseBuilder</c>实例
        /// </summary>
        /// <param name="tableName">表/视图名称</param>
        /// <param name="model">生成 SELECT 子句的模式 [Raw, WithAlias, StringWithAlias]</param>
        public SqlSelectClauseBuilder(string tableName, SelectClauseBuildMode model)
            : this(tableName, null, model)
        { }

        /// <summary>
        /// 初始化<c>SqlSelectClauseBuilder</c>实例
        /// </summary>
        /// <param name="tableName">表/视图名称</param>
        /// <param name="tableAlias">表/视图的匿名</param>
        /// <param name="model">生成 SELECT 子句的模式 [Raw, WithAlias, StringWithAlias]</param>
        public SqlSelectClauseBuilder(string tableName, string tableAlias, SelectClauseBuildMode model)
        {
            if (!this.ValidateTableViewNameOrAlias(tableName))
                throw new ArgumentException(tableName);

            if (tableAlias != null && !this.ValidateTableViewNameOrAlias(tableAlias))
                throw new ArgumentException(tableAlias);

            this.tableName = Tuple.Create(tableName, tableAlias);
            this.selectClauseBuildMode = model;
        }

        private void Out(string s)
        {
            this.selectBuilder.Append(s);
        }

        private void ClearStringBuileder()
        {
            if (this.selectBuilder.Length > 0)
                this.selectBuilder.Clear();
        }

        /// <summary>
        /// 添加 Select 的字段
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="alias">字段匿名</param>
        public void AddField(string field, string alias)
        {
            if (!this.ValidateFieldNameOrAlias(field))
                return;
            if (this.IsRepeatFieldName(field))
                return;

            if (String.IsNullOrEmpty(alias))
            {
                this.selectValues.Add(Tuple.Create(field, (string) null));
            }
            else
            {
                if (this.ValidateFieldNameOrAlias(alias) && !this.IsRepeatAlias(alias))
                    this.selectValues.Add(Tuple.Create(field, alias));
            }
        }

        private bool IsRepeatFieldName(string name)
        {
            return this.selectValues.Any(f => f.Item1.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsRepeatAlias(string alias)
        {
            return this.selectValues.Any(f => f.Item2.Equals(alias, StringComparison.OrdinalIgnoreCase));
        }

        private bool ValidateFieldNameOrAlias(string name)
        {
            return !String.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[0-9a-zA-Z_]+$");
        }

        private bool ValidateTableViewNameOrAlias(string table)
        {
            return !String.IsNullOrWhiteSpace(table) && Regex.IsMatch(table, @"^[0-9a-zA-Z_]+$");
        }

        /// <summary>
        /// 生成 SELECT 子句，若存在匿名，则使用匿名
        /// </summary>
        private void GenerateSelectClause()
        {
            if (!this.selectValues.Any())
                return;

            var index = 0;
            this.Out("SELECT");
            this.selectValues.ForEach(s =>
            {
                if (index++ != 0)
                    this.Out(",");

                this.Out(" ");
                this.Out(s.Item1);
                
                if (!String.IsNullOrWhiteSpace(s.Item2))
                {
                    this.Out(" ");
                    this.Out("AS");
                    this.Out(" ");
                    this.Out(s.Item2);
                }
            });

            this.Out(" ");
            this.Out("FROM");
            this.Out(" ");
            this.Out(this.tableName.Item1);
            if (!String.IsNullOrWhiteSpace(this.tableName.Item2))
            {
                this.Out(" ");
                this.Out("AS");
                this.Out(" ");
                this.Out(this.tableName.Item2);
            }
        }

        /// <summary>
        /// 生成 SELECT 子句，将类型转换为 NVARCHAR 类型，若存在匿名，则使用匿名
        /// </summary>
        private void GenerateConvertToStringSelectClause()
        {
            if (!this.selectValues.Any())
                return;

            var index = 0;
            this.Out("SELECT");
            this.selectValues.ForEach(s =>
            {
                if (index++ != 0)
                    this.Out(",");

                this.Out(" ");
                this.Out(string.Format("CAST({0} AS NVARCHAR)", s.Item1));

                this.Out(" ");
                this.Out("AS");
                this.Out(" ");
                this.Out(!String.IsNullOrWhiteSpace(s.Item2) ? s.Item2 : s.Item1);
            });

            this.Out(" ");
            this.Out("FROM");
            this.Out(" ");
            this.Out(this.tableName.Item1);
            if (!String.IsNullOrWhiteSpace(this.tableName.Item2))
            {
                this.Out(" ");
                this.Out("AS");
                this.Out(" ");
                this.Out(this.tableName.Item2);
            }
        }

        /// <summary>
        /// 生成原始的 SELECT 子句（不转换类型）
        /// </summary>
        private void GenerateRawSelectClause()
        {
            if (!this.selectValues.Any())
                return;

            var index = 0;
            this.Out("SELECT");
            this.selectValues.ForEach(s =>
            {
                if (index++ != 0)
                    this.Out(",");

                this.Out(" ");
                this.Out(s.Item1);
            });

            this.Out(" ");
            this.Out("FROM");
            this.Out(" ");
            this.Out(this.tableName.Item1);
            if (!String.IsNullOrWhiteSpace(this.tableName.Item2))
            {
                this.Out(" ");
                this.Out("AS");
                this.Out(" ");
                this.Out(this.tableName.Item2);
            }
        }

        public override string ToString()
        {
            this.ClearStringBuileder();

            switch (selectClauseBuildMode)
            {
                case SelectClauseBuildMode.Raw: 
                    this.GenerateRawSelectClause();
                    break;
                case SelectClauseBuildMode.StringWithAlias: 
                    this.GenerateConvertToStringSelectClause();
                    break;
                case SelectClauseBuildMode.WithAlias: 
                    this.GenerateSelectClause();
                    break;
            }

            return this.selectBuilder.ToString();
        }
    }
}
