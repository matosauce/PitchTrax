using System.Threading.Tasks;
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
            _dbConnection.CreateTable<Session>();
        } 

        public SQLiteConnection GetAsyncConnection()
        {
            return _dbConnection;
        }

  

    }
}
