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

        public Piece this[int index]
        {
            get { return pieces[index]; }
            set { pieces[index] = value; }
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