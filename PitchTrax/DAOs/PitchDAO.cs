using System.Collections.Generic;
using System.Linq;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    public class PitchDao
    {

        private readonly SQLiteConnection _dbConnection;
        private readonly IEnumerable<Pitch> _pitches;
        private readonly IEnumerable<PitchType> _pitchTypes; 

        public PitchDao(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _pitches = _dbConnection.Table<Pitch>();
            _pitchTypes = _dbConnection.Table<PitchType>();
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

        public Pitch ThrowNewPitch(int pitcherId, int pitchTypeId, int sessionId, int velocity, int zone, int breakAmount)
        {
            _dbConnection.Insert(new Pitch
            {
                PitcherId = pitcherId,
                SessionId = sessionId,
                Velocity = velocity,
                Zone = zone,
                Break = breakAmount
            });
            return _pitches.Last();
        }

        public PitchType GetPitchTypeByPitchId(int pitchId)
        {
            var pitchTypeid =  _pitches
                .Where(x => x.PitchId == pitchId)
                .Select(x => x.PitchTypeId);

            return _pitchTypes
                .First(x => pitchTypeid.Contains(x.PitchTypeId));
        }

        public IEnumerable<Pitch> GetPitchesForStatisticsScreen(int pitcherId, int pitchTypeId)
        {
            return _pitches
                .Where(x => x.PitcherId == pitcherId)
                .Where(x => x.PitchTypeId == pitchTypeId);
        } 

    }
}
