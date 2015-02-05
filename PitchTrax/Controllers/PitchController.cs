using System.Collections.Generic;
using PitchTrax.DAOs;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.Controllers
{
    public class PitchController
    {
        private readonly PitchDao _pitchDao ;

        public PitchController()
        {
            var connection = new PitchTraxDatabase().GetConnection();
            _pitchDao = new PitchDao(connection);
        }

        public IEnumerable<Pitch> GetAllPitchesByOnePitcher(int pitcherId)
        {
            return _pitchDao.GetAllPitchesThrownByPitcher(pitcherId);
        }

        public IEnumerable<Pitch> GetAllPitchesInOneSessionByOnePitcher(int sessionId)
        {
            return _pitchDao.GetAllPitchesThrownInOneSession(sessionId);
        }

        public IEnumerable<Pitch> GetAllPitchesOfPitchTypeThrownByPitcher(int pitcherId, int pitchTypeId)
        {
            return _pitchDao.GetAllPitchesThrownByPitcherOfOnePitchType(pitcherId, pitchTypeId);
        }

        public IEnumerable<Pitch> GetAllPitchesThrownByOnePitcherInOneSessionOfOnePitchType(int pitcherId, int pitchTypeId, int sessionId)
        {
            return _pitchDao.GetAllPitchesThrownByPitcherOfOnePitchTypeInOneSession(pitcherId, pitchTypeId, sessionId);
        } 

    }
}
