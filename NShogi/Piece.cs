using System;
using System.Collections.Generic;

namespace NShogi
{
    // 00000000 00000000
    //    |   | ||||||||   PieceType
    //    |   | |||||||`--   Pawn
    //    |   | ||||||`---   Lance
    //    |   | |||||`----   Knight
    //    |   | ||||`-----   Silver
    //    |   | |||`------   Gold
    //    |   | ||`-------   Bishop
    //    |   | |`--------   Rook
    //    |   | `---------   King
    //    |   `----------- Promoted flag
    //    `--------------- Black or White (@see Color)
    [Flags]
    public enum Piece
    {
        Empty    = 0x0000,
        Pawn     = 0x0001,
        Lance    = 0x0002,
        Knight   = 0x0004,
        Silver   = 0x0008,
        Gold     = 0x0010,
        Bishop   = 0x0020,
        Rook     = 0x0040,
        King     = 0x0080,
        Promoted = 0x0100,
        White    = 0x1000,
        OutOfBoard = int.MinValue,
    }

    public static class PieceExt
    {
        private static readonly int pieceTypeMask = 0x00FF;
        private static readonly int pieceNameMask = 0x01FF;
        private static readonly Dictionary<Piece, string> pieceNameMap = new Dictionary<Piece, string>();

        static PieceExt()
        {
            pieceNameMap.Add(Piece.Empty, "　");
            pieceNameMap.Add(Piece.Pawn, "歩");
            pieceNameMap.Add(Piece.Lance, "香");
            pieceNameMap.Add(Piece.Knight, "桂");
            pieceNameMap.Add(Piece.Silver, "銀");
            pieceNameMap.Add(Piece.Gold, "金");
            pieceNameMap.Add(Piece.Bishop, "角");
            pieceNameMap.Add(Piece.Rook, "飛");
            pieceNameMap.Add(Piece.King, "玉");
            pieceNameMap.Add(Piece.Pawn | Piece.Promoted, "と");
            pieceNameMap.Add(Piece.Lance | Piece.Promoted, "杏");
            pieceNameMap.Add(Piece.Knight | Piece.Promoted, "圭");
            pieceNameMap.Add(Piece.Silver | Piece.Promoted, "全");
            pieceNameMap.Add(Piece.Bishop | Piece.Promoted, "馬");
            pieceNameMap.Add(Piece.Rook | Piece.Promoted, "龍");
        }

        public static bool IsPiece(this Piece piece)
        {
            return piece != Piece.OutOfBoard
                && piece != Piece.Empty;
        }

        // 裏返した駒（玉、金の場合はそのまま）を返す
        public static Piece Turn(this Piece piece)
        {
            Piece type = piece.ToPieceType();
            if (type == Piece.King || type == Piece.Gold)
                return piece;
            return piece ^ Piece.Promoted;
        }

        public static Piece Promote(this Piece piece)
        {
            return piece.Promoted() ? piece : piece.Turn();
        }

        // 駒の先後を入れ替える
        public static Piece Give(this Piece piece)
        {
            return piece ^ Piece.White;
        }

        public static bool Promoted(this Piece piece)
        {
            return (piece & Piece.Promoted) > 0;
        }

        public static Piece ToPieceType(this Piece piece)
        {
            return (Piece)((int)piece & pieceTypeMask);
        }

        public static Color ToColor(this Piece piece)
        {
            return (Color)((int)(piece & Piece.White) >> 12);
        }

        public static string ToPieceName(this Piece piece)
        {
            return pieceNameMap[(Piece)((int)piece & pieceNameMask)];
        }

    }
}