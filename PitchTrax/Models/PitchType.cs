﻿using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    [Table("PitchTypes")]
    public class PitchType
    {
        [PrimaryKey, Unique]
        public int PitchTypeId { get; set; }

        [NotNull]
        public string PitchTypeName { get; set; }

        public int PitchTypeColor { get; set; }
    }
}
