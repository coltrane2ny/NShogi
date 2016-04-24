using System;
using System.Diagnostics;
using System.Text;
using NShogi;

namespace ConsoleApplication
{
    class ConsoleGameUI : IGameUI
    {
        IEngine black;
        IEngine white;
        
        public void Start()
        {
            Console.WriteLine("Start.");
            black = new HumanEngine();
            white = new RandomEngine();
        }

        public Move WaitMove(Position position)
        {
            PrintPosition(position);
            StringBuilder candidates = new StringBuilder("合法手: ");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var move in position.GetLegalMovesAndDrops(position.Turn))
            {
                candidates.AppendFormat("{0}{1} ", move.DstIndex, move.PieceType.ToPieceName());
            }
            sw.Stop();
            Console.WriteLine(candidates);
            Console.WriteLine(String.Format("Time to get legal moves: {0}", sw.Elapsed));
            
            return position.Turn == Color.Black ? black.WaitMove(position) : white.WaitMove(position);
        }

        private void PrintPosition(Position position)
        {
            StringBuilder sb = new StringBuilder()
                .AppendFormat("{0} 手目", position.CurrentMoveCount);
            if (position.CurrentMoveCount > 0)
                sb.AppendFormat("  {0} まで", position.LastMove);
            sb.Append("\n");

            for (int rank = 1; rank <= Board.RankNum; rank++)
            {
                sb.Append("+---+---+---+---+---+---+---+---+---+\n");
                sb.Append("|");
                for (int file = Board.FileNum; file > 0; file--)
                {
                    sb.AppendFormat("{0}|", ToDisplayName(position.Board[Board.GetIndex(file, rank)]));
                }
                sb.Append("\n");
            }
            sb.Append("+---+---+---+---+---+---+---+---+---+\n");
            sb.Append("持ち駒\n");
            sb.AppendFormat("先手: {0}\n", position.BlackHand.ToString());
            sb.AppendFormat("後手: {0}\n", position.WhiteHand.ToString());
            Console.WriteLine(sb.ToString());
        }

        private static string ToDisplayName(Piece piece)
        {
            return piece == Piece.Empty
                ? String.Format(" {0}", piece.ToPieceName())
                : String.Format("{0}{1}", piece.ToColor() == Color.Black ? "+" : "-", piece.ToPieceName());
        }
    }
}