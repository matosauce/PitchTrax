using System;

namespace Statistics
{
    public static class Metrics
    {
        public static double DefenseIndependentEra(int hr, int bb, int hbp, int k, int ip)
        {
            var defenseIndependentRuns = 13*hr + 3*(bb + hbp) - 2*k;
            return 3 + (defenseIndependentRuns/ip);
        }

        public static double ExpectedWinPercentage(double teamRunAverage, double era)
        {
            return 1/(1 + Math.Pow((teamRunAverage/era), y: 2));
        }

        public static double Era(double ip, double earnedRuns)
        {
            return earnedRuns/ip;
        }

        public static double ComponentEra(int hits, int hrs, int walks, int battersFaced, int ip)
        {
            var ptb = 0.89*(1.255*(hits - hrs) + 4*hrs) + 0.56*walks;
            return (((hits + walks)*ptb)/(battersFaced*ip))*9 - 0.56;
        }

        public static double Whip(double hits, double walks, double ip)
        {
            return (hits + walks)/ip;
        }
    }
}
