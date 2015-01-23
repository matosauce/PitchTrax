using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    public class PitcherStatistics
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitcherStatisticsId { get; set; }

        public string PitcherId { get; set; }

        public int SessionsThrown { get; set; }

        public int PitchesThrown { get; set; }
    }
}
