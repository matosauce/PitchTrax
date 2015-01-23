using System.Collections.Generic;
using System.Linq;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    public class PitcherDAO
    {
        public List<Pitcher> GetAllPitchers(SQLiteConnection dbConnection)
        {
            return dbConnection.Table<Pitcher>().ToList();
        }

        public Pitcher GetPitcherById(SQLiteConnection dbConnection, int pitcherId)
        {
            return dbConnection.Table<Pitcher>().First(x => x.PitcherId == pitcherId);
        }

        public void InsertNewPitchers(SQLiteConnection dbConnection, Pitcher newPitcher)
        {
            dbConnection.Insert(newPitcher);
        }

        public void ModifyExistingPitcher(SQLiteConnection dbConnection, Pitcher newPitcher)
        {
            var oldPitcher = dbConnection.Table<Pitcher>().First(x => x.PitcherId == newPitcher.PitcherId);
            dbConnection.Update(oldPitcher);
        }

        public void DeleteExistingPitcher(SQLiteConnection dbConnection, int pitcherIdToBeDeleted)
        {
            var pitcherToBeDeleted = dbConnection.Table<Pitcher>().First(x => x.PitcherId == pitcherIdToBeDeleted);
            dbConnection.Delete(pitcherToBeDeleted);
        }
    }
}
