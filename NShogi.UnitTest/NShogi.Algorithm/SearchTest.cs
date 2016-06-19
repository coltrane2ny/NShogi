using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NShogi;
using NShogi.Algorithm;

namespace NShogi.UnitTest
{
    public class SearchTest
    {
        [Fact]
        public void NegaMaxSucceeds()
        {
            IPosition p = PositionMock.TestData;
            Assert.Equal(1, Search.NegaMax(p, 0));
            Assert.Equal(3, Search.NegaMax(p, 1));
            Assert.Equal(0, Search.NegaMax(p, 2));
            Assert.Equal(4, Search.NegaMax(p, 3));
        }

        [Fact]
        public void AlphaBetaSucceeds()
        {
            IPosition p = PositionMock.TestData;
            Assert.Equal(1, Search.AlphaBeta(p, 0));
            Assert.Equal(3, Search.AlphaBeta(p, 1));
            Assert.Equal(0, Search.AlphaBeta(p, 2));
            Assert.Equal(4, Search.AlphaBeta(p, 3));
        }
    }
}