namespace Glicko2
{
    public class GlickoPlayer
    {
        private double glickoConversion = 173.7178;

        public GlickoPlayer(string name, int rating = 1500, int ratingDeviation = 350, double volatility = 0.06)
        {
            Name = name;
            Rating = rating;
            RatingDeviation = ratingDeviation;
            Volatility = volatility;
        }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int RatingDeviation { get; set; }
        public double Volatility { get; set; }
        public double GlickoRating { get { return (Rating - 1500) / glickoConversion; } }
        public double GlickoRatingDeviation { get { return RatingDeviation / glickoConversion; } }
    }
}