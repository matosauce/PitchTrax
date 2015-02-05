using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    public class PitcherKnowsPitchType
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int KnownPitchId { get; set; }

        public int PitchTypeId { get; set; }

        public int PitcherId { get; set; }
    }
}
