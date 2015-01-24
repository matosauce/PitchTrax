using System.Collections.Generic;
using System.Linq;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    class PitchTypeDao
    {

        private readonly SQLiteConnection _dbConnection;

        public PitchTypeDao(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<PitchType> GetAllPitchTypes()
        {
            return _dbConnection.Table<PitchType>().ToList();
        }

        public void TeachPitcherNewPitch(Pitcher pitcher, PitchType type)
        {
            var taughtPitch = new PitcherKnowsPitchType {PitchTypeId = type.PitchTypeId, PitcherId = pitcher.PitcherId};
            _dbConnection.Insert(taughtPitch);
        }

       //public List<PitchType> GetPitchesKnownByPitcher(Pitcher pitcher)
        //{
        //    //dbConnection.Table<PitcherKnowsPitchType>().Where(x => x.PitcherId == pitcher.PitcherId).Select("");
        //    throw new Exception();
        //}
    }
}
