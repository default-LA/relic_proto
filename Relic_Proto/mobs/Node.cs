using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relic_Proto
{
    public class Node
    {
        public int X;
        public int Y;
        public bool Walkable;
        public int PathLength;
        public int Heuristic;
        public Node parent;

        public Node()
        {
        }

        public int TotalCost()
        {
            return Heuristic + PathLength;
        }
    }
}
