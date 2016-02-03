using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glicko2
{
    public class GlickoOpponent : GlickoPlayer
    {
        public GlickoPlayer Opponent;
        public int Result;

        public GlickoOpponent(GlickoPlayer opponent, int result) : base(opponent.Rating, opponent.RatingDeviation, opponent.Volatility)
        {
            Result = result;
        }

        public GlickoOpponent(GlickoPlayer opponent, bool won) : base(opponent.Rating, opponent.RatingDeviation, opponent.Volatility)
        {
            Result = won ? 1 : 0;
        }
    }    
}