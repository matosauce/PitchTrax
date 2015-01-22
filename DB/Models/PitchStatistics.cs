using SQLite;
using SQLiteNetExtensions.Attributes;

namespace DB.Models
{
    class PitchStatistics
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int PitchStatisticsId { get; set; }

        [ForeignKey(typeof(Pitch))]
        public int PitchId { get; set; }
        
        [OneToOne]
        public Pitch Pitch { get; set; }

        public int Velocity { get; set; }

        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }
    }
}
