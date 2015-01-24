using System.Collections.Generic;
using System.Linq;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    public class PitcherDao
    {
        private readonly SQLiteConnection _dbConnection;

        public PitcherDao(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Pitcher> GetAllPitchers()
        {
            return _dbConnection.Table<Pitcher>().ToList();
        }

        public Pitcher GetPitcherById(int pitcherId)
        {
            return _dbConnection.Table<Pitcher>().First(x => x.PitcherId == pitcherId);
        }

        public void InsertNewPitcher(Pitcher newPitcher)
        {
            _dbConnection.Insert(newPitcher);
        }

        public void ModifyExistingPitcher(Pitcher newPitcher)
        {
            var oldPitcher = _dbConnection.Table<Pitcher>().First(x => x.PitcherId == newPitcher.PitcherId);
            if (oldPitcher != null)
            {
                oldPitcher = newPitcher;
            }
            _dbConnection.Update(oldPitcher);
        }

        public void DeleteExistingPitcher(int pitcherIdToBeDeleted)
        {
            var pitcherToBeDeleted = _dbConnection.Table<Pitcher>().First(x => x.PitcherId == pitcherIdToBeDeleted);
            _dbConnection.Delete(pitcherToBeDeleted);
        }
    }
}
