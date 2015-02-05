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

        public void UpdateKnownPitchTypes(int pitcherId, IEnumerable<int> types)
        {
            _dbConnection.InsertAll(KnownPitchFactory(types, pitcherId));
        }

       public IEnumerable<PitchType> GetPitchesKnownByPitcher(int pitcherId)
       {
           var knownPitchTypeIds = _knownPitchTypes
                .Where(x => x.PitcherId == pitcherId)
                .ToList();
           //We had to break this into two parts because somehow we were getting back zeros for data.
           var ids = knownPitchTypeIds.Select(x => x.PitchTypeId);
           var stuff = _pitchTypes
               .Where(x => ids
                   .Contains(x.PitchTypeId));
           return stuff;
       }

        private static IEnumerable<PitcherKnowsPitchType> KnownPitchFactory(IEnumerable<int> pitchTypeIds, int pitcherId)
        {
            return pitchTypeIds.Select(typeId => new PitcherKnowsPitchType
            {
                PitcherId = pitcherId,
                PitchTypeId = typeId
            });
        }
    }
}
