using System;
using System.Collections.Generic;

namespace NShogi
{
    // 持ち駒
    public class Hand
    {
        private int[] counts = new int[7];
        private static readonly Dictionary<Piece, int> pieceMap = new Dictionary<Piece, int>();

        static Hand()
        {
            pieceMap.Add(Piece.Pawn, 0);
            pieceMap.Add(Piece.Lance, 1);
            pieceMap.Add(Piece.Knight, 2);
            pieceMap.Add(Piece.Silver, 3);
            pieceMap.Add(Piece.Gold, 4);
            pieceMap.Add(Piece.Bishop, 5);
            pieceMap.Add(Piece.Rook, 6);
        }
        public Hand() {}
        public Hand(Hand hand)
        {
            Array.Copy(hand.counts, 0, counts, 0, counts.Length);
        }

        public int Add(Piece piece)
        {
            if (piece.ToPieceType() == Piece.Empty)
                return 0;
            return ++counts[pieceMap[piece.ToPieceType()]];
        }

        public int Remove(Piece piece)
        {
            if (piece.ToPieceType() == Piece.Empty)
                return 0;
            return --counts[pieceMap[piece.ToPieceType()]];
        }

        public int Count(Piece piece)
        {
            if (piece.ToPieceType() == Piece.Empty)
                return 0;
            return counts[pieceMap[piece.ToPieceType()]];
        }

        public int this[Piece piece]
        {
            get { return Count(piece.ToPieceType()); }
        }
        
        public IEnumerable<Piece> Pieces
        {
            get
            {
                foreach (var p in pieceMap)
                {
                    if (counts[p.Value] > 0)
                        yield return p.Key;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("飛:{0}  角:{1}  金:{2}  銀:{3}  桂:{4}  香:{5}  歩:{6}",
                counts[6],
                counts[5],
                counts[4],
                counts[3],
                counts[2],
                counts[1],
                counts[0]);
        }
    }
}