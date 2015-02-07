using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    [Table("Pitches")]
    public class Pitch
    {
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PitchId;
                hashCode = (hashCode*397) ^ PitchTypeId;
                hashCode = (hashCode*397) ^ PitcherId;
                hashCode = (hashCode*397) ^ SessionId;
                hashCode = (hashCode*397) ^ Velocity;
                hashCode = (hashCode*397) ^ Zone;
                hashCode = (hashCode*397) ^ Break;
                return hashCode;
            }
        }

        protected bool Equals(Pitch other)
        {
            return PitchId == other.PitchId && PitchTypeId == other.PitchTypeId && PitcherId == other.PitcherId && SessionId == other.SessionId && Velocity == other.Velocity && Zone == other.Zone && Break == other.Break;
        }

        [PrimaryKey, Unique, AutoIncrement]
        public int PitchId { get; set; }

        public int PitchTypeId { get; set; }

        public int PitcherId { get; set; }

        public int SessionId { get; set; }

        public int Velocity { get; set; }

        public int Zone { get; set; }

        public int Break { get; set; }

        public override bool Equals(object objPitch)
        {
            if (ReferenceEquals(null, objPitch)) return false;
            if (ReferenceEquals(this, objPitch)) return true;
            if (objPitch.GetType() != GetType()) return false;
            return Equals((Pitch) objPitch);
        }
    }
}
