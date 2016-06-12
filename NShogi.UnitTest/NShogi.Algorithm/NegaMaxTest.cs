using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NShogi;
using NShogi.Algorithm;

namespace NShogi.UnitTest
{
    public class NegaMaxTest
    {
        [Fact]
        public void NegaMaxSucceeds()
        {
            IPosition p = new TestPosition(Color.Black, 1)
                .AddNextPosition(new TestPosition(Color.White, 2)
                    .AddNextPosition(new TestPosition(Color.Black, 4)
                        .AddNextPosition(new TestPosition(Color.White, -2))
                        .AddNextPosition(new TestPosition(Color.White, 5))
                        .AddNextPosition(new TestPosition(Color.White, 7))
                    )
                    .AddNextPosition(new TestPosition(Color.Black, 5)
                        .AddNextPosition(new TestPosition(Color.White, 4))
                        .AddNextPosition(new TestPosition(Color.White, 3))
                        .AddNextPosition(new TestPosition(Color.White, -5))
                    )
                )
                .AddNextPosition(new TestPosition(Color.White, 3)
                    .AddNextPosition(new TestPosition(Color.Black, 0)
                        .AddNextPosition(new TestPosition(Color.White, -6))
                        .AddNextPosition(new TestPosition(Color.White, -4))
                        .AddNextPosition(new TestPosition(Color.White, 0))
                    )
                    .AddNextPosition(new TestPosition(Color.Black, 1)
                        .AddNextPosition(new TestPosition(Color.White, 8))
                        .AddNextPosition(new TestPosition(Color.White, 1))
                        .AddNextPosition(new TestPosition(Color.White, 2))
                    )
                );
            Assert.Equal(1, Search.NegaMax(p, 0));
            Assert.Equal(3, Search.NegaMax(p, 1));
            Assert.Equal(4, Search.NegaMax(p, 2));
            Assert.Equal(4, Search.NegaMax(p, 3));
        }
    }
}