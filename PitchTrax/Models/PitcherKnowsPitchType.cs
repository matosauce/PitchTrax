using SQLiteNetExtensions.Attributes;

namespace PitchTrax.Models
{
    public class PitcherKnowsPitchType
    {
        [ForeignKey(typeof(PitchType))]
        public int PitchTypeId { get; set; }

        [ForeignKey(typeof(Pitcher))]
        public int PitcherId { get; set; }
    }
}
