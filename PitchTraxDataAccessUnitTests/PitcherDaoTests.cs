using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PitchTrax.SQLite;

namespace PitchTraxDataAccessUnitTests
{
    [TestClass]
    public class PitcherDaoTests
    {

        internal SQLiteConnection _dbConnection;
        [ClassInitialize]
        public void SetUpForTests()
        {
            _dbConnection = new SQLiteConnection(Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\PitchTrax.db");
        }

        [TestMethod]
        public void TestInsertNewPitcher()
        {

        }
    }
}
