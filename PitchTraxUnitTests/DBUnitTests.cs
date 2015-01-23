using System.IO;
using DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace PitchTraxUnitTests
{
    [TestClass]
    public class DBUnitTests
    {
        [TestMethod]
        public void TestDatabaseConnection()
        {
            var conn = new SQLiteAsyncConnection("entries");    
        }
    }
}
