using System.Collections.Generic;
using System.Linq;
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
                    PitchTypeId = 1,
                    PitchTypeName = "Circle Change-up"
                }
            };
            _dbConnection.CreateTable<PitchType>();
            if (_dbConnection.Table<PitchType>().Any())
                _dbConnection.DeleteAll<PitchType>();

            _dbConnection.InsertAll(types);

            _dbConnection.CreateTable<Session>();
        } 

        public SQLiteConnection GetConnection()
        {
            return _dbConnection;
        }
    }
}
