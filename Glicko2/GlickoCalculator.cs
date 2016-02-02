using System;
using System.Collections.Generic;

namespace Glicko2
{    
    public static class GlickoCalculator
    {
        public static double VolatilityChange = 0.8;
        public static double ConvergenceTolerance = 0.000001;
        private static double glickoConversion = 173.7178;

        public static double CalculateRanking(GlickoPlayer competitor, List<GlickoOpponent> opponents)
        {


            throw new NotImplementedException();
        }

        private static double ConvertRatingToOriginal(double glickoRating)
        {
            return (glickoConversion * glickoRating) + 1500;
        }

        private static double CalculateNewRatingDeviation(double preratingDeviation, double variance)
        {
            return 1 / Math.Sqrt((1 / Math.Pow(preratingDeviation, 2)) + (1 / variance));
        }

        private static double CaluculateNewRating(GlickoPlayer competitor, List<GlickoOpponent> opponents, double newRatingDeviation)
        {
            var sum = 0.0;

            foreach(var opponent in opponents)
            {
                sum += Gphi(opponent.Opponent) * (opponent.Result - Edeltaphi(competitor.Rating, opponent.Opponent));
            }

            return competitor.Rating + ((Math.Pow(newRatingDeviation, 2)) * sum);
        }

        public static double CalculatePreRatingDeviation(double ratingDeviation, double updatedVolatility)
        {
            return Math.Sqrt(Math.Pow(ratingDeviation, 2) + Math.Pow(updatedVolatility, 2));
        }

        public static double CalulateNewVolatility(GlickoPlayer competitor, List<GlickoOpponent> opponents)
        {
            var variance = ComputeVariance(competitor, opponents);
            var rankingChange = RatingImprovement(competitor, opponents, variance);
            var rankDeviation = competitor.GlickoRatingDeviation;            

            var A = VolatilityTransform(competitor.Volatility);
            var a = VolatilityTransform(competitor.Volatility);
            double B = 0.0;

            if (Math.Pow(rankingChange, 2) > (Math.Pow(competitor.GlickoRatingDeviation, 2) + variance))
            {
                B = Math.Log(Math.Pow(rankingChange, 2) - Math.Pow(competitor.GlickoRatingDeviation, 2) - variance);
            }

            if (Math.Pow(rankingChange, 2) <= (Math.Pow(competitor.GlickoRatingDeviation, 2) + variance))
            {
                var k = 1;
                var x = Math.Log(competitor.Volatility - (k * VolatilityChange));

                while(VolatilityFunction(x, rankingChange, rankDeviation, variance, competitor.Volatility) < 0)
                {
                    k++;
                }

                B = VolatilityTransform(competitor.Volatility) - (k * VolatilityChange);
            }

            var fA = VolatilityFunction(A, rankingChange, rankDeviation, variance, competitor.Volatility);
            var fB = VolatilityFunction(B, rankingChange, rankDeviation, variance, competitor.Volatility);

            while (Math.Abs(B - A) > ConvergenceTolerance)
            {
                var C = A + ((A - B) * fA / (fB - fA));
                var fC = VolatilityFunction(C, rankingChange, rankDeviation, variance, competitor.Volatility);

                if ((fC * fB) < 0)
                {
                    A = B;
                    fA = fB;
                }
                else
                {
                    fA = fA / 2;
                }

                B = C;
                fB = fC;
            }

            return Math.Exp(A / 2);
        }

        private static double VolatilityTransform(double volatility)
        {
            return Math.Log(Math.Pow(volatility, 2));
        }

        private static double VolatilityFunction(double x, double rankingChange, double rankDeviation, double variance, double volatility)
        {
            var leftNumerater = Math.Exp(x) * (Math.Pow(rankingChange, 2) - Math.Pow(rankDeviation, 2) - variance - Math.Exp(x));
            var leftDenominator = 2 * Math.Pow(Math.Pow(rankDeviation, 2) + variance + Math.Exp(x), 2);

            var rightNumerater = x - VolatilityTransform(volatility);
            var rightDenomintor = Math.Pow(VolatilityChange, 2);

            return leftNumerater / leftDenominator - rightNumerater / rightDenomintor;
        }

        public static double RatingImprovement(GlickoPlayer competitor, List<GlickoOpponent> opponents, double variance)
        {
            double sum = 0;

            foreach (var opponent in opponents)
            {
                sum += Gphi(opponent.Opponent) * (opponent.Result - Edeltaphi(competitor.GlickoRating, opponent.Opponent));
            }

            return variance * sum;
        }

        public static double ComputeVariance(GlickoPlayer competitor, List<GlickoOpponent> opponents)
        {
            double sum = 0;
            foreach (var opponent in opponents)
            {
                var opponentsGphi = Gphi(opponent.Opponent);

                var eDeltaPhi = Edeltaphi(competitor.GlickoRating, opponent.Opponent);

                sum += Math.Pow(opponentsGphi, 2) * eDeltaPhi * (1 - eDeltaPhi);
            }

            return sum;
        }

        private static double Gphi(GlickoPlayer opponent)
        {
            return 1 / (Math.Sqrt(1 + (3 * Math.Pow(opponent.GlickoRatingDeviation, 2) / Math.Pow(Math.PI, 2))));
        }

        private static double Edeltaphi(double playerRating, GlickoPlayer opponent)
        {
            return 1 / (1 + (Math.Exp(-Gphi(opponent)) * (playerRating - opponent.GlickoRating)));
        }
    }
}
