using System;
using System.Collections.Generic;
using System.Linq;

namespace NShogi
{
    public class Board
    {
        public const int RankNum = 9; // 段
        public const int FileNum = 9; // 筋

        private const int boardSize = 10 * 11;
        private static Piece[] initialPieces = new Piece[]
        {
            Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard,
            Piece.OutOfBoard, Piece.Lance.Give(), Piece.Empty, Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Empty, Piece.Lance,
            Piece.OutOfBoard, Piece.Knight.Give(), Piece.Bishop.Give(), Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Rook, Piece.Knight,
            Piece.OutOfBoard, Piece.Silver.Give(), Piece.Empty, Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Empty, Piece.Silver,
            Piece.OutOfBoard, Piece.Gold.Give(), Piece.Empty, Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Empty, Piece.Gold,
            Piece.OutOfBoard, Piece.King.Give(), Piece.Empty, Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Empty, Piece.King,
            Piece.OutOfBoard, Piece.Gold.Give(), Piece.Empty, Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Empty, Piece.Gold,
            Piece.OutOfBoard, Piece.Silver.Give(), Piece.Empty, Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Empty, Piece.Silver,
            Piece.OutOfBoard, Piece.Knight.Give(), Piece.Rook.Give(), Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Bishop, Piece.Knight,
            Piece.OutOfBoard, Piece.Lance.Give(), Piece.Empty, Piece.Pawn.Give(), Piece.Empty, Piece.Empty, Piece.Empty, Piece.Pawn, Piece.Empty, Piece.Lance,
            Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard, Piece.OutOfBoard
        };

        private Piece[] pieces;

        public Board()
        {
            pieces = initialPieces;
        }

        public Board(Board board)
        {
            pieces = new Piece[boardSize];
            Array.Copy(board.pieces, 0, pieces, 0, boardSize);
        }

        public static Board FromSfen(string sfen)
        {
            return new Board();
        }

        public string ToSfen()
        {
            return "";
        }

        public Piece this[int index]
        {
            get { return pieces[index]; }
            set { pieces[index] = value; }
        }

        public IEnumerable<Move> GetLegalMoves(Color color)
        {
            foreach (int index in Indexes)
            {
                Piece piece = pieces[index];
                if (!piece.IsPiece())
                    continue;

                if (piece.ToColor() != color)
                    continue;

                IEnumerable<int> movableRange = GetMovableRange(index, color);
                foreach (int candidate in Move.GetMovableIndexes(piece, index))
                {
                    if (piece.ToPieceType() == Piece.Knight)
                    {
                        Piece dest = pieces[candidate];
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
                }
            }
        }

        public IEnumerable<Move> GetLegalDrops(Color color, Hand hand)
        {
            IEnumerable<int> dropableIndexes = GetDropableIndexes();
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
        private IEnumerable<int> GetMovableRange(int index, Color color)
        {
            foreach (int direction in new int[] { -11, -10, -9, -1, 1, 9, 10, 11 })
            {
                int i = index + direction;
                while (IsInBoard(i))
                {
                    if (pieces[i] != Piece.Empty)
                    {
                        // 自分の駒ならそれ以上先に進めない
                        if (pieces[i].ToColor() == color)
                        {
                            break;
                        }
                        // 敵の駒なら駒を取ってその地点まで進める
                        if (pieces[i].ToColor() != color)
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

        private IEnumerable<int> GetDropableIndexes()
        {
            foreach (int index in Indexes)
            {
                Piece p = pieces[index];
                if (p == Piece.Empty)
                    yield return index;
            }
        }

        public static int GetIndex(int file, int rank)
        {
            return file * 10 + rank;
        }

        public static int GetFile(int index)
        {
            return index / 10;
        }

        public static int GetRank(int index)
        {
            return index % 10;
        }

        public static bool IsInBoard(int index)
        {
            return GetRank(index) > 0 && GetFile(index) > 0 && GetFile(index) < 10;
        }

        public static IEnumerable<int> Indexes
        {
            get
            {
                for (int index = 11; index < 100; index++)
                {
                    if (IsInBoard(index))
                        yield return index;
                }
            }
        }
    }
}