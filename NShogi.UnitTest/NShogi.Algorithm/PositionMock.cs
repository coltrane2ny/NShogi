using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NShogi;
using NShogi.Algorithm;

namespace NShogi.UnitTest
{
    public class PositionMock : IPosition
    {
        public static IPosition TestData = new PositionMock(Color.Black, 1)
            .AddNextPosition(new PositionMock(Color.White, 2)
                .AddNextPosition(new PositionMock(Color.Black, -4)
                    .AddNextPosition(new PositionMock(Color.White, -2))
                    .AddNextPosition(new PositionMock(Color.White, 5))
                    .AddNextPosition(new PositionMock(Color.White, 7))
                )
                .AddNextPosition(new PositionMock(Color.Black, 5)
                    .AddNextPosition(new PositionMock(Color.White, 4))
                    .AddNextPosition(new PositionMock(Color.White, 3))
                    .AddNextPosition(new PositionMock(Color.White, -5))
                )
            )
            .AddNextPosition(new PositionMock(Color.White, 3)
                .AddNextPosition(new PositionMock(Color.Black, 0)
                    .AddNextPosition(new PositionMock(Color.White, -6))
                    .AddNextPosition(new PositionMock(Color.White, -4))
                    .AddNextPosition(new PositionMock(Color.White, 0))
                )
                .AddNextPosition(new PositionMock(Color.Black, 1)
                    .AddNextPosition(new PositionMock(Color.White, 8))
                    .AddNextPosition(new PositionMock(Color.White, 1))
                    .AddNextPosition(new PositionMock(Color.White, 2))
                )
            );

        private List<PositionMock> nextPositions = new List<PositionMock>();

        public Color Turn { get; private set; }
        public int Evaluation { get; private set; }
        public IEnumerable<IPosition> NextPositions
        {
            get
            {
                return nextPositions;
            }
        }

        public PositionMock(Color turn, int evaluation)
        {
            Turn = turn;
            Evaluation = evaluation;
        }

        public PositionMock AddNextPosition(PositionMock p)
        {
            nextPositions.Add(p);
            return this;
        }
    }
}