using System;
using System.Collections.Generic;
using PitchTrax.DAOs;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.Controllers
{
    public class SessionController
    {

        private readonly SessionDao _sessionDao;
        private readonly PitchDao _pitchDao;

        public SessionController()
        {
            var connection = new PitchTraxDatabase().GetConnection();
            _sessionDao = new SessionDao(connection);
            _pitchDao = new PitchDao(connection);
        }

        public IEnumerable<Session> GetAllSessions()
        {
            return _sessionDao.GetAllSessions();
        }

        public IEnumerable<Session> GetAllSessionsInDateRange(DateTime startDate, DateTime endDate)
        {
            return _sessionDao.GetAllSessionsInDateRange(startDate, endDate);
        }

        public IEnumerable<Session> GetAllSessionsInDateRangeForPitcher(int pitcherId, DateTime startDate,
            DateTime endDate)
        {
            return _sessionDao.GetAllSessionsInDateRangeForSpecificPitcher(startDate, endDate, pitcherId);
        } 

        public IEnumerable<Session> GetAllSessionsForPitcher(int pitcherId)
        {
            return _sessionDao.GetSessionsThrownByPitcher(pitcherId);
        }

        public Session GetSessionById(int sessionId)
        {
            return _sessionDao.GetSessionById(sessionId);
        }

        public void CreateNewSession(int pitcherId, DateTime date)
        {
            _sessionDao.CreateNewSession(pitcherId, date);
        }

    }
}
