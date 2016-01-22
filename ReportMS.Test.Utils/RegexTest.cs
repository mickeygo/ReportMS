using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class RegexTest
    {
        [TestMethod]
        public void ExtractSqlSelectClause_Test()
        {
            var sqlQuery = "SELECT AA, BB,  [cc], dd, [ee]  ee1, ff as ff1  FROM Test SELECT ddd, ffff FROM";

            // SELECT[a-zA-Z0-9\[\]._, ]+FROM
            // [a-zA-Z0-9_]+

            Func<Match, string> match = match1 =>
            {
                var len = match1.Groups.Count;
                return match1.Groups[len - 1].Value;
            };

            var selectClause = Regex.Match(sqlQuery, @"SELECT[a-zA-Z0-9\[\]._, ]+FROM", RegexOptions.IgnoreCase).Value;
            var removeSelectAndFromClause = Regex.Replace(selectClause, @"SELECT|FROM", "");

            var cols = (from column in removeSelectAndFromClause.Split(',')
                select match(Regex.Match(column, "[a-zA-Z0-9_]+")));

            var result = String.Join(",", cols);

            Assert.Fail(result);
        }

        [TestMethod]
        public void ExtractSqlSelectClause2_Test()
        {
            var sqlQuery = "SELECT AA, BB,  [cc], dd, [ee]  ee1, ff as ff_1  FROM Test SELECT ddd, ffff FROM";

            var fromPosition = sqlQuery.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);

            var selectClause = sqlQuery.Substring(0, fromPosition);
            var removeSelectAndFromClause = Regex.Replace(selectClause, @"SELECT|FROM", "");

            Func<MatchCollection, string> match = matchs =>
            {
                var len = matchs.Count;
                return matchs[len - 1].Value;
            };

            var cols = (from column in removeSelectAndFromClause.Split(',')
                        select match(Regex.Matches(column, "[a-zA-Z0-9_]+")));

            var result = String.Join(",", cols);

            Assert.Fail(result);
        }

        [TestMethod]
        public void Regex_Test()
        {
            var matchs = Regex.Matches("ff as ff_1", "[a-zA-Z0-9_]+");

            Assert.IsTrue(matchs[2].Value == "ff_1", matchs[2].Value);
        }
    }
}
