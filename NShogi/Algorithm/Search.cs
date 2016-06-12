using System;
using System.Collections.Generic;
using System.Linq;

namespace NShogi.Algorithm
{
    public class Search
    {
        public static int NegaMax(IPosition p, int depth)
        {
            if (depth < 0) throw new ArgumentException(String.Format("depth は　0 以上でなければなりません。depth: {0}", depth));
            if (depth == 0) return p.Turn == Color.Black ? p.Evaluation : -p.Evaluation;

            int max = int.MinValue;
            foreach (var np in p.NextPositions)
            {
                int e = -NegaMax(np, depth - 1);
                if (e > max) max = e;
            }
            return max;
        }
    }
}