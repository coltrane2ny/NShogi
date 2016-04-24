using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NShogi;

namespace NShogi.UnitTest
{
    public class MoveTest
    {
        [Fact]
        public void BlackPawn()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Pawn, 17),
                item => Assert.Equal(16, item)
            ); 
        }
        [Fact]
        public void WhitePawn()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Pawn | Piece.White, 13),
                item => Assert.Equal(14, item)
            );
        }
        [Fact]
        public void BlackLance()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Lance, 19),
                item => Assert.Equal(18, item),
                item => Assert.Equal(17, item),
                item => Assert.Equal(16, item),
                item => Assert.Equal(15, item),
                item => Assert.Equal(14, item),
                item => Assert.Equal(13, item),
                item => Assert.Equal(12, item),
                item => Assert.Equal(11, item)
            );
        }
        [Fact]
        public void WhiteLance()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Lance | Piece.White, 11),
                item => Assert.Equal(12, item),
                item => Assert.Equal(13, item),
                item => Assert.Equal(14, item),
                item => Assert.Equal(15, item),
                item => Assert.Equal(16, item),
                item => Assert.Equal(17, item),
                item => Assert.Equal(18, item),
                item => Assert.Equal(19, item)
            );
        }
        [Fact]
        public void BlackKnight()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Knight, 29),
                item => Assert.Equal(17, item),
                item => Assert.Equal(37, item)
            );
        }
        [Fact]
        public void WhiteKnight()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Knight | Piece.White, 21),
                item => Assert.Equal(13, item),
                item => Assert.Equal(33, item)
            );
        }
        [Fact]
        public void BlackSilver()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Silver, 39),
                item => Assert.Equal(28, item),
                item => Assert.Equal(38, item),
                item => Assert.Equal(48, item)
            );
        }
        [Fact]
        public void WhiteSilver()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Silver | Piece.White, 31),
                item => Assert.Equal(22, item),
                item => Assert.Equal(32, item),
                item => Assert.Equal(42, item)
            );
        }
        [Fact]
        public void BlackGold()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Gold, 49),
                item => Assert.Equal(38, item),
                item => Assert.Equal(39, item),
                item => Assert.Equal(48, item),
                item => Assert.Equal(58, item),
                item => Assert.Equal(59, item)
            );
        }
        [Fact]
        public void WhiteGold()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Gold | Piece.White, 41),
                item => Assert.Equal(31, item),
                item => Assert.Equal(32, item),
                item => Assert.Equal(42, item),
                item => Assert.Equal(51, item),
                item => Assert.Equal(52, item)
            );
        }
        [Fact]
        public void Bishop()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Bishop, 88),
                item => Assert.Equal(77, item),
                item => Assert.Equal(66, item),
                item => Assert.Equal(55, item),
                item => Assert.Equal(44, item),
                item => Assert.Equal(33, item),
                item => Assert.Equal(22, item),
                item => Assert.Equal(11, item),
                item => Assert.Equal(79, item),
                item => Assert.Equal(97, item),
                item => Assert.Equal(99, item)
            );
        }
        [Fact]
        public void Rook()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.Rook, 28),
                item => Assert.Equal(18, item),
                item => Assert.Equal(27, item),
                item => Assert.Equal(26, item),
                item => Assert.Equal(25, item),
                item => Assert.Equal(24, item),
                item => Assert.Equal(23, item),
                item => Assert.Equal(22, item),
                item => Assert.Equal(21, item),
                item => Assert.Equal(29, item),
                item => Assert.Equal(38, item),
                item => Assert.Equal(48, item),
                item => Assert.Equal(58, item),
                item => Assert.Equal(68, item),
                item => Assert.Equal(78, item),
                item => Assert.Equal(88, item),
                item => Assert.Equal(98, item)
            );
        }
        [Fact]
        public void King()
        {
            Assert.Collection(Move.GetMovableIndexes(Piece.King, 59),
                item => Assert.Equal(48, item),
                item => Assert.Equal(49, item),
                item => Assert.Equal(58, item),
                item => Assert.Equal(68, item),
                item => Assert.Equal(69, item)
            );
        }
    }
}
