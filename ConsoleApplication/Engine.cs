using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NShogi;

namespace ConsoleApplication
{
    interface IEngine
    {
        Move WaitMove(Position position);
    }

    class RandomEngine : IEngine
    {
        public Move WaitMove(Position position)
        {
            var moves = position.GetLegalMovesAndDrops(position.Turn);
            return moves.ToList()[new Random().Next(moves.Count())];
        }
    }

    class HumanEngine : IEngine
    {
        public Move WaitMove(Position position)
        {
            string moveString = WaitInput(position);
            if (moveString.StartsWith("F"))
            {
                return new Move() { Finished = true };
            }
            else if (moveString.StartsWith("D"))
            {
                string[] drop = moveString.Split(' ');
                int dstIndex = Convert.ToInt32(drop[1]);
                Piece pieceType = Piece.Empty;
                switch(drop[2])
                {
                    case "R": pieceType = Piece.Rook; break;
                    case "B": pieceType = Piece.Bishop; break;
                    case "G": pieceType = Piece.Gold; break;
                    case "S": pieceType = Piece.Silver; break;
                    case "K": pieceType = Piece.Knight; break;
                    case "L": pieceType = Piece.Lance; break;
                    case "P": pieceType = Piece.Pawn; break;
                }
                return new Move()
                {
                    IsDrop = true,
                    DstIndex = dstIndex,
                    PieceType = pieceType
                };
            }
            else if (moveString.StartsWith("M"))
            {
                string[] move = moveString.Split(' ');
                int src = Convert.ToInt32(move[1]);
                int dst = Convert.ToInt32(move[2]);
                bool pro = move.Length > 3 ? Convert.ToBoolean(move[3]) : false;
                return new Move()
                {
                    SrcIndex = src,
                    DstIndex = dst,
                    Promote = pro
                };
            }
            return null;
        }

        private string WaitInput(Position position)
        {
            Console.WriteLine("Command: {M nn mm [true|false]|D nn {R|B|G|S|K|L|P}|F}");
            Console.Write(String.Format("{0}: ", position.Turn == Color.Black ? "先手" : "後手"));
            return Console.ReadLine();
        }
    }
}