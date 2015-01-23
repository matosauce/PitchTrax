using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    public class Pitcher
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitcherId { get; set; }

        [NotNull]
        public string FirstName { get; set; }

        [NotNull]
        public string LastName { get; set; }

        public int JerseyNumber { get; set; }

        [MaxLength(1)]
        public string Handedness { get; set; }
    }
}
