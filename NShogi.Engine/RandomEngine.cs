using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NShogi;

namespace NShogi.Engine
{
    public class RandomEngine : IEngine
    {
        public Move WaitMove(Position position)
        {
            var moves = position.GetLegalMovesAndDrops(position.Turn);
            return moves.ToList()[new Random().Next(moves.Count())];
        }
    }
}