using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NShogi;

namespace NShogi.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Game(new ConsoleGameUI()).Start(new Position());
        }
    }
}
