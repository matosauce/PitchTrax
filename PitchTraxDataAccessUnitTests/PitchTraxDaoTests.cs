using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PitchTrax.DAOs;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTraxDataAccessUnitTests
{
    [TestClass]
    public class PitchTraxDaoTests
    {

        internal static SQLiteConnection _dbConnection;

        [ClassInitialize]
        public static void SetUpForTests(TestContext tc)
        {
            _dbConnection = new SQLiteConnection(Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\PitchTraxTest.db");
            _dbConnection.CreateTable<Pitch>();
            _dbConnection.CreateTable<Pitcher>();
            _dbConnection.CreateTable<PitcherKnowsPitchType>();
            var types = new List<PitchType>
            {
                new PitchType
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 1,
                    PitchTypeName = "4-Seam Fastball"
                },
                new PitchType
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 2,
                    PitchTypeName = "2-Seam Fastball"
                },
                new PitchType
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 3,
                    PitchTypeName = "Curveball"
                },
                new PitchType
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 4,
                    PitchTypeName = "Circle Change-up"
                },
                new PitchType
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 5,
                    PitchTypeName = "Splitter"
                },
                new PitchType
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 6,
                    PitchTypeName = "Slider"
                }
            };
            _dbConnection.CreateTable<PitchType>();
            if (_dbConnection.Table<PitchType>().Any())
                _dbConnection.DeleteAll<PitchType>();

            _dbConnection.InsertAll(types);

            _dbConnection.CreateTable<Session>();
        }

        [TestMethod]
        public void TestPitcher()
        {
            var pitcherDao = new PitcherDao(_dbConnection);
            var testPitcher = new Pitcher
            {
                FirstName = "John",
                LastName = "Smith",
                Handedness = "L",
                JerseyNumber = 1
            };

            var newPitcherId = pitcherDao.InsertNewPitcher(testPitcher);
            var pitcher = pitcherDao.GetPitcherById(newPitcherId);
            Assert.AreEqual(testPitcher, pitcher);

            testPitcher.FirstName = "Jack";
            testPitcher.LastName = "Johnson";
            testPitcher.Handedness = "R";
            testPitcher.JerseyNumber = 99;
            
            var modifiedPitcherId = pitcherDao.ModifyExistingPitcher(testPitcher);
            var testModifyPitcher = pitcherDao.GetPitcherById(modifiedPitcherId);
            Assert.AreEqual(testPitcher, testModifyPitcher);

            pitcherDao.DeleteExistingPitcher(modifiedPitcherId);
            //Assert.IsNull(pitcherDao.GetPitcherById(modifiedPitcherId));
        }
    }
}
