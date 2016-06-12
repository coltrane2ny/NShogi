using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NShogi;
using NShogi.Algorithm;

namespace NShogi.UnitTest
{
    public class TestPosition : IPosition
    {
        private List<TestPosition> nextPositions = new List<TestPosition>();
        
        public Color Turn { get; private set; }
        public int Evaluation { get; private set; }
        public IEnumerable<IPosition> NextPositions
        {
            get 
            {
                return nextPositions;
            }
        }
        
        public TestPosition(Color turn, int evaluation)
        {
            Turn = turn;
            Evaluation = evaluation;
        }
        
        public TestPosition AddNextPosition(TestPosition p)
        {
            nextPositions.Add(p);
            return this;
        }
    }
}