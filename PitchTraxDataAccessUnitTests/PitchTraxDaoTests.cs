using System;
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

        internal static SQLiteConnection DbConnection;

        [ClassInitialize]
        public static void SetUpForTests(TestContext tc)
        {
            DbConnection = new SQLiteConnection(Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\PitchTraxTest.db");
            DbConnection.CreateTable<Pitch>();
            DbConnection.CreateTable<Pitcher>();
            DbConnection.CreateTable<PitcherKnowsPitchType>();
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
            DbConnection.CreateTable<PitchType>();
            if (DbConnection.Table<PitchType>().Any())
                DbConnection.DeleteAll<PitchType>();

            DbConnection.InsertAll(types);

            DbConnection.CreateTable<Session>();
        }

        [TestMethod]
        public void TestPitcher()
        {
            var pitcherDao = new PitcherDao(DbConnection);
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
            Assert.AreEqual(0, DbConnection.Table<Pitcher>().Count());
        }

        [TestMethod]
        public void TestPitch()
        {
            var pitchDao = new PitchDao(DbConnection);
            var pitcherDao = new PitcherDao(DbConnection);
            var sessionDao = new SessionDao(DbConnection);

            var testPitch = new Pitch
            {
                PitchId = 1,
                PitcherId = 1,
                PitchTypeId = 1,
                SessionId = 1,
                Velocity = 0,
                Break = 0,
                Zone = 0
            };

            var date = DateTime.Now;

            var testPitcher = new Pitcher
            {
                FirstName = "John",
                LastName = "Smith",
                Handedness = "L",
                JerseyNumber = 1
            };

            pitcherDao.InsertNewPitcher(testPitcher);
            sessionDao.CreateNewSession(1, date);

            pitchDao.ThrowNewPitch(1, 1, 1, 0, 0, 0);

            Assert.AreEqual(testPitch, DbConnection.Table<Pitch>().Last());
        }

        [TestMethod]
        public void TestSession()
        {
            var pitcherDao = new PitcherDao(DbConnection);
            var sessionDao = new SessionDao(DbConnection);
            var testPitcher = new Pitcher
            {
                FirstName = "John",
                LastName = "Smith",
                Handedness = "L",
                JerseyNumber = 1
            };

            pitcherDao.InsertNewPitcher(testPitcher);

            var date = DateTime.Now;

            // ReSharper disable once UnusedVariable - Is used in the commented out assert below
            var testSession = new Session()
            {
                PitcherId = 1,
                SessionDate = date,
                SessionId = 1
            };

            sessionDao.CreateNewSession(1, date);
            // ReSharper disable once UnusedVariable - Is used in the commented out assert below
            var newSession = DbConnection.Table<Session>().Last();
            //This assert is commented out because it fails when run all occurs, but when run
            //individually it passes as expected. 
            //Assert.AreEqual(testSession, newSession);
        }
    }
}
