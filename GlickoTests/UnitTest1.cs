using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Glicko2;
using System.Collections.Generic;

namespace GlickoTests
{
    [TestClass]
    public class UnitTest1
    {
        GlickoPlayer player1 = new GlickoPlayer(ratingDeviation: 200);
        GlickoPlayer player2 = new GlickoPlayer(1400, 30);
        GlickoPlayer player3 = new GlickoPlayer(1550, 100);
        GlickoPlayer player4 = new GlickoPlayer(1700, 300);

        [TestMethod]
        public void GlickoUpdateCorrect()
        {
            var player1Opponents = new List<GlickoOpponent>
            {
                new GlickoOpponent(player2, 1),
                new GlickoOpponent(player3, 0),
                new GlickoOpponent(player4, 0)
            };

            player1 = GlickoCalculator.CalculateRanking(player1, player1Opponents);

            Assert.AreEqual(0.05999, player1.Volatility);
        }

        [TestMethod]
        public void GlickoUpdateRankingCorrect()
        {
            var player1Opponents = new List<GlickoOpponent>
            {
                new GlickoOpponent(player2, 1),
                new GlickoOpponent(player3, 0),
                new GlickoOpponent(player4, 0)
            };

            player1 = GlickoCalculator.CalculateRanking(player1, player1Opponents);

            Assert.AreEqual(1464.06, player1.Rating);
        }

        [TestMethod]
        public void GlickoUpdateRatingDeviationCorrect()
        {
            var player1Opponents = new List<GlickoOpponent>
            {
                new GlickoOpponent(player2, 1),
                new GlickoOpponent(player3, 0),
                new GlickoOpponent(player4, 0)
            };

            player1 = GlickoCalculator.CalculateRanking(player1, player1Opponents);

            Assert.AreEqual(151.52, player1.RatingDeviation);
        }
    }
}
