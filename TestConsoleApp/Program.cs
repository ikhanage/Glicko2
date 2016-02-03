using Glicko2;
using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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

            Console.WriteLine(String.Format("Player ranking: {0}", player1.Rating));
            Console.WriteLine(String.Format("Player ranking deviation: {0}", player1.RatingDeviation));

            player1 = GlickoCalculator.CalculateRanking(player1, player1Opponents);

            Console.WriteLine(String.Format("Player ranking: {0}", player1.Rating));
            Console.WriteLine(String.Format("Player ranking deviation: {0}", player1.RatingDeviation));

            Console.ReadKey();
        }
    }
}
