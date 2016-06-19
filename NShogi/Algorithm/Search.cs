using System;
using System.Collections.Generic;
using System.Linq;

namespace NShogi.Algorithm
{
    public class Search
    {
        public static int NegaMax(IPosition p, uint depth)
        {
            if (depth == 0) return Evaluate(p);

            depth--;
            int max = int.MinValue;
            foreach (var np in p.NextPositions)
            {
                max = Math.Max(max, -NegaMax(np, depth));
            }
            return max;
        }

        public static int AlphaBeta(IPosition p, uint depth, int alpha = -9999, int beta = 9999)
        {
            if (depth == 0) return Evaluate(p);

            depth--;
            foreach (var np in p.NextPositions)
            {
                alpha = Math.Max(alpha, -AlphaBeta(np, depth, -beta, -alpha));
                if (alpha >= beta) return alpha;
            }
            return alpha;
        }

        private static int Evaluate(IPosition p)
        {
            return p.Turn == Color.Black ? p.Evaluation : -p.Evaluation;
        }
    }
}