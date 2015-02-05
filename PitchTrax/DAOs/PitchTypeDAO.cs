using System.Collections.Generic;
using System.Linq;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    public class PitchTypeDao
    {
        private readonly SQLiteConnection _dbConnection;
        private readonly TableQuery<PitchType> _pitchTypes;
        private readonly TableQuery<PitcherKnowsPitchType> _knownPitchTypes; 

        public PitchTypeDao(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _pitchTypes = dbConnection.Table<PitchType>();
            _knownPitchTypes = dbConnection.Table<PitcherKnowsPitchType>();
        }

        public IEnumerable<PitchType> GetAllPitchTypes()
        {
            return _pitchTypes
                .ToList();
        }

        public void TeachPitcherNewPitch(Pitcher pitcher, PitchType type)
        {
            _dbConnection.Insert(KnownPitchFactory(type.PitchTypeId, pitcher.PitcherId));
        }

        public void RemovePitchFromPitcher(Pitcher pitcher, PitchType type)
        {
            _dbConnection.Delete(KnownPitchFactory(type.PitchTypeId, pitcher.PitcherId));
        }

       public IEnumerable<PitchType> GetPitchesKnownByPitcher(Pitcher pitcher)
       {
           var knownPitchTypeIds = _knownPitchTypes
                .Where(x => x.PitcherId == pitcher.PitcherId)
                .Select(x => x.PitchTypeId);

           return _pitchTypes
               .Where(x => knownPitchTypeIds
                   .Contains(x.PitchTypeId));
       }

        private static PitcherKnowsPitchType KnownPitchFactory(int pitchTypeId, int pitcherId)
        {
            return new PitcherKnowsPitchType
            {
                PitchTypeId = pitchTypeId,
                PitcherId = pitcherId
            };
        }
    }
}
