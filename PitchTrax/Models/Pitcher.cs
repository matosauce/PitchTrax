using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

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

        public char Handedness { get; set; }

        [ManyToMany(typeof(PitchType))]
        public List<Pitcher> KnownPitches { get; set; }

        public List<Session> PreviousSessions { get; set; }

        public List<Pitch> PreviouslyThrownPitches { get; set; } 
    }
}
