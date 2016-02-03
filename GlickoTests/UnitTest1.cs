using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Glicko2;
using System.Collections.Generic;

namespace GlickoTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GlickoUpdateCorrect()
        {
            var player1 = new GlickoPlayer("player 1", ratingDeviation: 200);
            var player2 = new GlickoPlayer("player 2", 1400, 30);
            var player3 = new GlickoPlayer("player 3", 1550, 100);
            var player4 = new GlickoPlayer("player 4", 1700, 300);

            var player1Opponents = new List<GlickoOpponent>
            {
                new GlickoOpponent(player2, 1),
                new GlickoOpponent(player3, 0),
                new GlickoOpponent(player4, 0)
            };

            player1 = GlickoCalculator.CalculateRanking(player1, player1Opponents);

            var playerRatingCorrect = player1.Rating == 1464.06;
            var playerRatingDeviationCorrect = player1.RatingDeviation == 151.52;
            var playerVolatilityCorrect = player1.Volatility == 0.05999;

            Assert.IsTrue(playerRatingCorrect && playerRatingDeviationCorrect && playerVolatilityCorrect);
        }
    }
}
