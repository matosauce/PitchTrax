using System.Collections.Generic;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    public class PitchDao
    {

        private readonly SQLiteConnection _dbConnection;
        private readonly TableQuery<Pitch> _pitches;

        public PitchDao(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _pitches = _dbConnection.Table<Pitch>();
        }

        public IEnumerable<Pitch> GetAllPitchesThrownByPitcher(int pitcherId)
        {
            return _pitches
                .Where(x => x.PitcherId == pitcherId);
        }

        public IEnumerable<Pitch> GetAllPitchesThrownInOneSession(int sessionId)
        {
            return _pitches
                .Where(x => x.SessionId == sessionId);
        }

        public IEnumerable<Pitch> GetAllPitchesThrownByPitcherOfOnePitchType(int pitcherId, int pitchTypeId)
        {
            return _pitches
                .Where(x => x.PitcherId == pitchTypeId)
                .Where(x => x.PitchTypeId == pitchTypeId);
        }

        public IEnumerable<Pitch> GetAllPitchesThrownByPitcherOfOnePitchTypeInOneSession(int pitcherId, int pitchTypeId,
            int sessionId)
        {
            return _pitches
                .Where(x => x.PitcherId == pitchTypeId)
                .Where(x => x.PitchTypeId == pitchTypeId)
                .Where(x => x.SessionId == sessionId);
        }

        public void ThrowNewPitch(int pitcherId, int pitchTypeId, int sessionId, int velocity, int xCoord,
            int yCoord)
        {
            _dbConnection.Insert(new Pitch
            {
                PitcherId = pitcherId,
                SessionId = sessionId,
                Velocity = velocity,
                Zone = xCoord,
                YCoordinate = yCoord
            });
        }

    }
}
