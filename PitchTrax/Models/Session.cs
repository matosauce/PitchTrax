using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace PitchTrax.Models
{
    public class Session
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int SessionId { get; set; }

        [ForeignKey(typeof(Pitcher))]
        public int PitcherId { get; set; }

        [OneToOne]
        public Pitcher Pitcher { get; set; }

        public DateTime SessionDate { get; set; }

        public List<Pitch> PitchesThrownInSession { get; set; }

    }
}
