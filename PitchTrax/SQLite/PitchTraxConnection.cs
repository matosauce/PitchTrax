using System.Collections.Generic;
using PitchTrax.Models;

namespace PitchTrax.SQLite
{
    public class PitchTraxDatabase
    {

        private readonly SQLiteConnection _dbConnection;

        public PitchTraxDatabase()
        {
            _dbConnection = new SQLiteConnection(Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\PitchTrax.db");
            Initialize();
        }

        private void Initialize()
        {
            _dbConnection.CreateTable<Pitch>();
            _dbConnection.CreateTable<Pitcher>();
            _dbConnection.CreateTable<PitcherKnowsPitchType>();
            _dbConnection.CreateTable<PitcherStatistics>();
            _dbConnection.CreateTable<PitchStatistics>();
            _dbConnection.CreateTable<PitchType>();
            var types = new List<PitchType>
            {
                new PitchType()
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 1,
                    PitchTypeName = "4-Seam Fastball"
                },
                new PitchType()
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 2,
                    PitchTypeName = "2-Seam Fastball"
                },
                new PitchType()
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 3,
                    PitchTypeName = "Curveball"
                },
                new PitchType()
                {
                    PitchTypeColor = 0,
                    PitchTypeId = 1,
                    PitchTypeName = "Circle Change-up"
                }
            };
            _dbConnection.CreateTable<Session>();
        } 

        public SQLiteConnection GetAsyncConnection()
        {
            return _dbConnection;
        }

  

    }
}
