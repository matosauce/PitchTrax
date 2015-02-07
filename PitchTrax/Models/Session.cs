using System;
using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    [Table("Sessions")]
    public class Session
    {
        protected bool Equals(Session other)
        {
            return SessionId == other.SessionId
                && PitcherId == other.PitcherId
                && SessionDate.Date.ToString("yyyy-MM-dd hh:mm:ss") == other.SessionDate.Date.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SessionId;
                hashCode = (hashCode*397) ^ PitcherId;
                hashCode = (hashCode*397) ^ SessionDate.GetHashCode();
                return hashCode;
            }
        }

        [PrimaryKey, Unique, AutoIncrement]
        public int SessionId { get; set; }

        public int PitcherId { get; set; }

        public DateTime SessionDate { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Session) obj);
        }
    }
}
