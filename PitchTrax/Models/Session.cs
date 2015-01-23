using System;
using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    [Table("Sessions")]
    public class Session
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int SessionId { get; set; }

        public int PitcherId { get; set; }

        public DateTime SessionDate { get; set; }
    }
}
