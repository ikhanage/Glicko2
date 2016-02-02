using Glicko2;
using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new GlickoPlayer("player 1");
            var player2 = new GlickoPlayer("player 2", 1800, 50);
            var player3 = new GlickoPlayer("player 3", 1300, 300);
            var player4 = new GlickoPlayer("player 4", 1650, 100);

            var player1Opponents = new List<GlickoOpponent>
            {
                new GlickoOpponent(player2, 0),
                new GlickoOpponent(player3, 1),
                new GlickoOpponent(player4, 1)
            };

            var variance = GlickoCalculator.ComputeVariance(player1, player1Opponents);
            var ratingImprovement = GlickoCalculator.RatingImprovement(player1, player1Opponents);

            Console.WriteLine(variance);
            Console.WriteLine(ratingImprovement);

            Console.ReadKey();
        }
    }
}
