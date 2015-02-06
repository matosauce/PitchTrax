using PitchTrax.SQLite;

namespace PitchTrax.Models
{
    public class Pitcher
    {
        protected bool Equals(Pitcher other)
        {
            return PitcherId == other.PitcherId 
                && string.Equals(FirstName, other.FirstName) 
                && string.Equals(LastName, other.LastName) 
                && JerseyNumber == other.JerseyNumber 
                && string.Equals(Handedness, other.Handedness);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PitcherId;
                hashCode = (hashCode*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ JerseyNumber;
                hashCode = (hashCode*397) ^ (Handedness != null ? Handedness.GetHashCode() : 0);
                return hashCode;
            }
        }

        [PrimaryKey, Unique, AutoIncrement]
        public int PitcherId { get; set; }

        [NotNull]
        public string FirstName { get; set; }

        [NotNull]
        public string LastName { get; set; }

        public int JerseyNumber { get; set; }

        [MaxLength(1)]
        public string Handedness { get; set; }

        public override bool Equals(object objPitcher)
        {
            if (ReferenceEquals(null, objPitcher)) return false;
            if (ReferenceEquals(this, objPitcher)) return true;
            return objPitcher.GetType() == GetType() && Equals((Pitcher) objPitcher);
        }
    }
}
