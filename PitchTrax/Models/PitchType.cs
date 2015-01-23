using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace PitchTrax.Models
{
    public class PitchType
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitchTypeId { get; set; }

        [NotNull]
        public string PitchTypeName { get; set; }

        public int PitchTypeColor { get; set; }

        [ManyToMany(typeof(PitcherKnowsPitchType))]
        public List<Pitcher> Pitchers { get; set; }

        public List<Pitch> PreviouslyThrownPitchesOfThisType { get; set; }
    }
}
