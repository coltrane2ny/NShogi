using System;
using System.Collections.Generic;
using System.Linq;

namespace NShogi
{
    // 棋譜
    public class Score
    {
        private List<Move> moves = new List<Move>();

        public Position InitialPosition { get; private set; }
        public int Count { get { return moves.Count; } }
        public Move LastMove { get { return moves[moves.Count - 1]; } }
        public Move[] Moves { get { return moves.ToArray(); } }

        public Score(Position initial)
        {
            InitialPosition = initial;
        }

        public void AddMove(Move move)
        {
            moves.Add(move);
        }
    }
}