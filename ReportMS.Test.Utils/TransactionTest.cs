using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TransactionScop_Test()
        {
            var transResult0 = new Transaction0();
            var transResult1 = new Transaction0();
            //var transResult2 = new Transaction0();
            
            var transOption = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, transOption))
            {
                transResult0.Value = 1;

                using (var trans1 = new TransactionScope())
                {
                    transResult1.Value = 1;

                    //trans1.Dispose();
                }

                //using (var trans2 = new TransactionScope())
                //{
                //    transResult2 = 1;

                //    //throw new InvalidOperationException();

                //    trans2.Complete();
                //}

                //trans.Complete();
            }

            Assert.IsTrue(transResult0.Value == 1);
            Assert.IsTrue(transResult1.Value == 1);
            //Assert.IsTrue(transResult2 == 1);
        }

        class Transaction0
        {
            public int Value { get; set; }
        }
    }

}
