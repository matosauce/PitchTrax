using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PitchTrax.DAOs;
using PitchTrax.Models;
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
            _dbConnection = new SQLiteConnection(Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\PitchTraxTest.db");
        }

        [TestMethod]
        public void TestInsertNewPitcher()
        {
            var pitcherDao = new PitcherDao(_dbConnection);
            var testPitcher = new Pitcher
            {
                FirstName = "John",
                LastName = "Smith",
                Handedness = "L",
                JerseyNumber = 1
            };
        }
    }
}
