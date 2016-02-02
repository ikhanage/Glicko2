using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glicko2
{
    public class GlickoOpponent
    {
        public GlickoPlayer Opponent;
        public int Result;

        public GlickoOpponent(GlickoPlayer opponent, int result)
        {
            Opponent = opponent;
            Result = result;
        }

        public GlickoOpponent(GlickoPlayer opponent, bool won)
        {
            Opponent = opponent;
            Result = won ? 1 : 0;
        }
    }    
}