using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace PitchTrax.Models
{
    public class Pitch
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitchId { get; set; }

        [ForeignKey(typeof(PitchType))]
        public int PitchTypeId { get; set; }

        [ForeignKey(typeof(Pitcher))]
        public int PitcherId { get; set; }

        [OneToOne]
        public Pitcher Pitcher { get; set; }

        [OneToOne]
        public PitchType PitchType { get; set; }

    }
}
