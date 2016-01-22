using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Test.Common;

namespace ReportMS.Test.Repositories
{
    
    internal class BootStrap
    {
        [ClassInitialize]
        static void StartUp()
        {
            Helper.Init();
        }
    }
}
