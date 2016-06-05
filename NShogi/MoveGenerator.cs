using System;
using System.Collections.Generic;
using System.Linq;

namespace NShogi
{
    public class MoveGenerator
    {
        public static IEnumerable<Move> GetMoves(Position position)
        {
            foreach (var move in GetLegalMoves(position.Turn, position.Board))
                yield return move;
            foreach (var drop in GetLegalDrops(position.Turn,
                position.Turn == Color.Black ? position.BlackHand : position.WhiteHand,
                position.Board))
                yield return drop;
        }

        public static IEnumerable<Move> GetLegalMoves(Color color, Board board)
        {
            foreach (int index in Board.Indexes)
            {
                Piece piece = board[index];
                if (!piece.IsPiece())
                    continue;

                if (piece.ToColor() != color)
                    continue;

                IEnumerable<int> movableRange = GetMovableRange(index, color, board);
                foreach (int candidate in Move.GetMovableIndexes(piece, index))
                {
                    if (piece.ToPieceType() == Piece.Knight)
                    {
                        Piece dest = board[candidate];
                        if (dest != Piece.Empty && dest.ToColor() == color)
                            continue;
                    }
                    else if (!movableRange.Contains(candidate))
                        continue;

                    yield return new Move()
                    {
                        SrcIndex = index,
                        DstIndex = candidate,
                        PieceType = piece.ToPieceType(),
                        Promote = false
                    };

                    if ((color == Color.Black && Board.GetRank(candidate) <= 3)
                        || (color == Color.White && Board.GetRank(candidate) >= 7))
                    {
                        yield return new Move()
                        {
                            SrcIndex = index,
                            DstIndex = candidate,
                            PieceType = piece.ToPieceType(),
                            Promote = true
                        };
                    }
                }
            }
        }

        public static IEnumerable<Move> GetLegalDrops(Color color, Hand hand, Board board)
        {
            IEnumerable<int> dropableIndexes = GetDropableIndexes(board);
            foreach (Piece p in hand.Pieces)
            {
                foreach (int index in dropableIndexes)
                {
                    if (Move.CanDrop(color == Color.Black ? p : p | Piece.White, index))
                        yield return new Move()
                        {
                            IsDrop = true,
                            DstIndex = index,
                            PieceType = p,
                        };
                }
            }
        }

        // 現在の局面で、与えられたマス・手番に対して、縦横斜めの遠方へ移動可能な全マスを返す
        private static IEnumerable<int> GetMovableRange(int index, Color color, Board board)
        {
            foreach (int direction in new int[] { -11, -10, -9, -1, 1, 9, 10, 11 })
            {
                int i = index + direction;
                while (Board.IsInBoard(i))
                {
                    if (board[i] != Piece.Empty)
                    {
                        // 自分の駒ならそれ以上先に進めない
                        if (board[i].ToColor() == color)
                        {
                            break;
                        }
                        // 敵の駒なら駒を取ってその地点まで進める
                        if (board[i].ToColor() != color)
                        {
                            yield return i;
                            break;
                        }
                    }

                    yield return i;
                    i += direction;
                }
            }
        }

        private static IEnumerable<int> GetDropableIndexes(Board board)
        {
            foreach (int index in Board.Indexes)
            {
                Piece p = board[index];
                if (p == Piece.Empty)
                    yield return index;
            }
        }

    }
}