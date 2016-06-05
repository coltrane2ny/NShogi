using System;
using System.Collections.Generic;
using System.Linq;

namespace NShogi
{
    // 局面
    public class Position
    {
        public Board Board { get; private set; }
        public int CurrentMoveCount { get; private set; }
        public Color Turn { get; private set; }
        public Hand BlackHand { get; private set; }
        public Hand WhiteHand { get; private set; }

        public Position()
        {
            Board = new Board();
            Turn = Color.Black;
            BlackHand = new Hand();
            WhiteHand = new Hand();
        }

        public Position(Position position)
        {
            Board = new Board(position.Board);
            CurrentMoveCount = position.CurrentMoveCount + 1;
            BlackHand = new Hand(position.BlackHand);
            WhiteHand = new Hand(position.WhiteHand);
        }

        // 盤上の駒を動かす
        public Position Move(int src, int dst, bool promote)
        {
            Piece srcPiece = Board[src];
            Piece dstPiece = Board[dst];

            Position next = new Position(this);
            next.Turn = TurnOver(Turn);
            next.Board[src] = Piece.Empty;
            next.Board[dst] = promote ? srcPiece.Promote() : srcPiece;

            if (Turn == Color.Black)
                next.BlackHand.Add(dstPiece);
            else
                next.WhiteHand.Add(dstPiece);
            return next;
        }

        // 持ち駒を打つ
        public Position Drop(int dst, Piece pieceType)
        {
            Piece piece = Turn == Color.Black ? pieceType : pieceType.Give();
            Position next = new Position(this);
            next.Turn = TurnOver(Turn);
            next.Board[dst] = piece;

            if (Turn == Color.Black)
                next.BlackHand.Remove(pieceType);
            else
                next.WhiteHand.Remove(pieceType);
            return next;
        }

        // 先後交代する
        private Color TurnOver(Color turn)
        {
            return (Color)((int)turn ^ 1);
        }
    }
}