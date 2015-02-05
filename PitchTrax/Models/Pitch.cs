using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    [Table("Pitches")]
    public class Pitch
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitchId { get; set; }

        public int PitchTypeId { get; set; }

        public int PitcherId { get; set; }

        public int SessionId { get; set; }

        public int Velocity { get; set; }

        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }
    }
}
