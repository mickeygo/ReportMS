using System;

namespace ReportMS.Test.Storage
{
    public class SqlServerTempTest
    {
        public Guid ID { get; private set; }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public double Weight { get; private set; }

        public string Address { get; private set; }

        public bool Enabled { get; private set; }

        public DateTime? CreatedDate { get; private set; }
    }
}
