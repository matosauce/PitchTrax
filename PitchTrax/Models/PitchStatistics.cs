using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    class PitchStatistics
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitchStatisticsId { get; set; }

        public int PitchId { get; set; }

        public int Velocity { get; set; }

        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }
    }
}
