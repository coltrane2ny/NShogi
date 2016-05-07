using System;
using System.Collections.Generic;

namespace NShogi
{
    public class Move
    {
        public bool Finished { get; set; }
        public bool IsDrop { get; set; }
        public int SrcIndex { get; set; }
        public int DstIndex { get; set; }
        public Piece PieceType { get; set; }
        public bool Promote { get; set; }

        private delegate IEnumerable<int> MovableIndexesFactory(int index);
        private static Dictionary<Piece, MovableIndexesFactory> _movableIndexesFactories = new Dictionary<Piece, MovableIndexesFactory>()
        {
            { Piece.Pawn, GetBlackPawnMoves },
            { Piece.Pawn | Piece.White, GetWhitePawnMoves },
            { Piece.Lance, GetBlackLanceMoves },
            { Piece.Lance | Piece.White, GetWhiteLanceMoves },
            { Piece.Knight, GetBlackKnightMoves },
            { Piece.Knight | Piece.White, GetWhiteKnightMoves },
            { Piece.Silver, GetBlackSilverMoves },
            { Piece.Silver | Piece.White, GetWhiteSilverMoves },
            { Piece.Gold, GetBlackGoldMoves },
            { Piece.Gold | Piece.White, GetWhiteGoldMoves },
            { Piece.Bishop, GetBishopMoves },
            { Piece.Bishop | Piece.White, GetBishopMoves },
            { Piece.Rook, GetRookMoves },
            { Piece.Rook | Piece.White, GetRookMoves },
            { Piece.King, GetKingMoves },
            { Piece.King | Piece.White, GetKingMoves },
            { Piece.Pawn | Piece.Promoted, GetBlackGoldMoves },
            { Piece.Pawn | Piece.White | Piece.Promoted, GetWhiteGoldMoves },
            { Piece.Lance | Piece.Promoted, GetBlackGoldMoves },
            { Piece.Lance | Piece.White | Piece.Promoted, GetWhiteGoldMoves },
            { Piece.Knight | Piece.Promoted, GetBlackGoldMoves },
            { Piece.Knight | Piece.White | Piece.Promoted, GetWhiteGoldMoves },
            { Piece.Silver | Piece.Promoted, GetBlackGoldMoves },
            { Piece.Silver | Piece.White | Piece.Promoted, GetWhiteGoldMoves },
            { Piece.Bishop | Piece.Promoted, GetHorceMoves },
            { Piece.Bishop | Piece.White | Piece.Promoted, GetHorceMoves },
            { Piece.Rook | Piece.Promoted, GetDragonMoves },
            { Piece.Rook | Piece.White | Piece.Promoted, GetDragonMoves },
        };
        private delegate bool DropChecker(int index);
        private static Dictionary<Piece, DropChecker> _dropCheckers = new Dictionary<Piece, DropChecker>()
        {
            { Piece.Pawn, CheckBlackPawnLanceDrop },
            { Piece.Pawn | Piece.White, CheckWhitePawnLanceDrop },
            { Piece.Lance, CheckBlackPawnLanceDrop },
            { Piece.Lance | Piece.White, CheckWhitePawnLanceDrop },
            { Piece.Knight, CheckBlackKnightDrop },
            { Piece.Knight | Piece.White, CheckWhiteKnightDrop },
        };

        // 非合法手を含めた移動可能なすべてのマスを返す。
        public static IEnumerable<int> GetMovableIndexes(Piece p, int index)
        {
            foreach(int candidate in _movableIndexesFactories[p](index))
            {
                if (Board.IsInBoard(candidate))
                    yield return candidate;
            }
        }

        // 持ち駒を打てるかどうかの単純チェック
        public static bool CanDrop(Piece p, int index)
        {
            return _dropCheckers.ContainsKey(p)
                ? _dropCheckers[p](index)
                : true;
        }

        private static IEnumerable<int> GetBlackPawnMoves(int index)
        {
            yield return index - 1;
        }

        private static IEnumerable<int> GetWhitePawnMoves(int index)
        {
            yield return index + 1;
        }

        private static IEnumerable<int> GetBlackLanceMoves(int index)
        {
            while (Board.IsInBoard(--index))
            {
                yield return index;
            }
        }

        private static IEnumerable<int> GetWhiteLanceMoves(int index)
        {
            while (Board.IsInBoard(++index))
            {
                yield return index;
            }
        }

        private static IEnumerable<int> GetBlackKnightMoves(int index)
        {
            yield return index - 12;
            yield return index + 8;
        }

        private static IEnumerable<int> GetWhiteKnightMoves(int index)
        {
            yield return index - 8;
            yield return index + 12;
        }

        private static IEnumerable<int> GetBlackSilverMoves(int index)
        {
            yield return index - 11;
            yield return index - 9;
            yield return index - 1;
            yield return index + 9;
            yield return index + 11;
        }

        private static IEnumerable<int> GetWhiteSilverMoves(int index)
        {
            yield return index - 11;
            yield return index - 9;
            yield return index + 1;
            yield return index + 9;
            yield return index + 11;
        }

        private static IEnumerable<int> GetBlackGoldMoves(int index)
        {
            yield return index - 11;
            yield return index - 10;
            yield return index - 1;
            yield return index + 1;
            yield return index + 9;
            yield return index + 10;
        }

        private static IEnumerable<int> GetWhiteGoldMoves(int index)
        {
            yield return index - 10;
            yield return index - 9;
            yield return index - 1;
            yield return index + 1;
            yield return index + 10;
            yield return index + 11;
        }

        private static IEnumerable<int> GetBishopMoves(int index)
        {
            foreach (int direction in new int[] { -11, -9, 9, 11 })
            {
                int pos = index + direction;
                while (Board.IsInBoard(pos))
                {
                    yield return pos;
                    pos += direction;
                }
            }
        }

        private static IEnumerable<int> GetRookMoves(int index)
        {
            foreach (int direction in new int[] { -10, -1, 1, 10 })
            {
                int pos = index + direction;
                while (Board.IsInBoard(pos))
                {
                    yield return pos;
                    pos += direction;
                }
            }
        }

        private static IEnumerable<int> GetKingMoves(int index)
        {
            yield return index - 11;
            yield return index - 10;
            yield return index - 9;
            yield return index - 1;
            yield return index + 1;
            yield return index + 9;
            yield return index + 10;
            yield return index + 11;
        }

        private static IEnumerable<int> GetHorceMoves(int index)
        {
            foreach (var i in GetBishopMoves(index))
            {
                yield return i;
            }
            yield return index - 10;
            yield return index - 1;
            yield return index + 1;
            yield return index + 10;
        }

        private static IEnumerable<int> GetDragonMoves(int index)
        {
            foreach (var i in GetRookMoves(index))
            {
                yield return i;
            }
            yield return index - 11;
            yield return index - 9;
            yield return index + 9;
            yield return index + 11;
        }

        private static bool CheckBlackPawnLanceDrop(int index)
        {
            return Board.GetRank(index) > 1;
        }

        private static bool CheckWhitePawnLanceDrop(int index)
        {
            return Board.GetRank(index) < 9;
        }

        private static bool CheckBlackKnightDrop(int index)
        {
            return Board.GetRank(index) > 2;
        }

        private static bool CheckWhiteKnightDrop(int index)
        {
            return Board.GetRank(index) < 8;
        }
    }
}