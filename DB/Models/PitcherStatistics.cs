using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DB.Models
{
    public class PitcherStatistics
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitcherStatisticsId { get; set; }

        [ForeignKey(typeof(Pitcher))]
        public string PitcherId { get; set; }

        [OneToOne]
        public Pitcher Pitcher { get; set; }

        public int SessionsThrown { get; set; }

        public int PitchesThrown { get; set; }
    }
}
