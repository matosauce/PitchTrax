using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PitchTrax.Models;
using PitchTrax.SQLite;

namespace PitchTrax.DAOs
{
    class PitchTypeDAO
    {
        public List<PitchType> GetAllPitchTypes(SQLiteConnection dbConnection)
        {
            return dbConnection.Table<PitchType>().ToList();
        }

        public void TeachPitcherNewPitch(SQLiteConnection dbConnection, Pitcher pitcher, PitchType type)
        {
            var taughtPitch = new PitcherKnowsPitchType {PitchTypeId = type.PitchTypeId, PitcherId = pitcher.PitcherId};
            dbConnection.Insert(taughtPitch);
        }

        public List<PitchType> GetPitchesKnownByPitcher(SQLiteConnection dbConnection, Pitcher pitcher)
        {
            //dbConnection.Table<PitcherKnowsPitchType>().Where(x => x.PitcherId == pitcher.PitcherId).Select("");
            throw new Exception();
        }
    }
}
