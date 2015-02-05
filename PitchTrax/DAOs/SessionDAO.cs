using System;
using System.Collections.Generic;
using System.Linq;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    public class SessionDao
    {
        
        private readonly SQLiteConnection _dbConnection;
        private readonly TableQuery<Session> _sessions;

        public SessionDao(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _sessions = _dbConnection.Table<Session>();
        }

        public IEnumerable<Session> GetAllSessions()
        {
            return _sessions
                .ToList();
        }

        public Session GetSessionById(int sessionId)
        {
            return _sessions
                .First(x => x.SessionId == sessionId);
        }

        public IEnumerable<Session> GetSessionsThrownByPitcher(int pitcherId)
        {
            return _sessions
                .Where(x => x.PitcherId == pitcherId);
        }

        public IEnumerable<Session> GetAllSessionsInDateRange(DateTime startDateTime, DateTime endDateTime)
        {
            return _sessions
                .Where(x => DateTime.Compare(startDateTime, x.SessionDate) < 0
                            && DateTime.Compare(endDateTime, x.SessionDate) > 0);
        }

        public IEnumerable<Session> GetAllSessionsInDateRangeForSpecificPitcher(DateTime startDateTime, DateTime endDateTime, int pitcherId)
        {
            return _sessions
                .Where(x => DateTime.Compare(startDateTime, x.SessionDate) < 0)
                .Where(x => DateTime.Compare(endDateTime, x.SessionDate) > 0)
                .Where(x => x.PitcherId == pitcherId);
        }

        public void CreateNewSession(int pitcherId, DateTime date)
        {
            _dbConnection.Insert(new Session
            {
                PitcherId = pitcherId,
                SessionDate = date
            });
        }
    }
}
