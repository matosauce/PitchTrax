using System;
using System.Collections.Generic;
using System.Linq;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    public class PitcherDao
    {
        private readonly SQLiteConnection _dbConnection;
        private readonly TableQuery<Pitcher> _pitchers;
        private readonly TableQuery<Session> _sessions;
        private readonly TableQuery<Pitch> _pitches; 

        public PitcherDao(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _pitchers = _dbConnection.Table<Pitcher>();
            _sessions = _dbConnection.Table<Session>();
            _pitches = _dbConnection.Table<Pitch>();
        }

        public IEnumerable<Pitcher> GetAllPitchers()
        {
            return _pitchers
                .ToList();
        }

        public Pitcher GetPitcherById(int pitcherId)
        {
            return _pitchers
                .First(x => x.PitcherId == pitcherId);
        }

        public int InsertNewPitcher(Pitcher newPitcher)
        {
            _dbConnection.Insert(newPitcher);
            return _pitchers
                .Select(x => x.PitcherId)
                .Last();
        }

        public int ModifyExistingPitcher(Pitcher newPitcher)
        {
            var oldPitcher = _pitchers.First(x => x.PitcherId == newPitcher.PitcherId);
            if (oldPitcher != null)
                oldPitcher = newPitcher;
            _dbConnection.Update(oldPitcher);
            return _pitchers
                .First(x => x.PitcherId == newPitcher.PitcherId)
                .PitcherId;
        }

        public void DeleteExistingPitcher(int pitcherIdToBeDeleted)
        {
            _dbConnection.Delete(_pitchers.First(x => x.PitcherId == pitcherIdToBeDeleted));
        }

        public double GetAveragePitchesThrownPerSession(int pitcherId)
        {
            var totalPitchesThrown = _pitches
                .Count(x => x.PitcherId == pitcherId);

            var totalSessionsThrown = _sessions
                .Count(x => x.PitcherId == pitcherId);

            return Convert.ToDouble(totalPitchesThrown/totalSessionsThrown);
        }
    }
}
