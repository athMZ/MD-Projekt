using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMatrix
{
    public class Edge
    {
        private Node Start { get; set; }
        private Node End { get; set; }

        private double DistanceX { get; set; }
        private double DistanceY { get; set; }

        public Edge(Node start, Node end)
        {
            Start = start;
            End = end;
            UpdateDistance();
        }

        public void UpdateDistance()
        {
            DistanceX = double.Abs(Start.PositionX - End.PositionX);
            DistanceY = double.Abs(Start.PositionY - End.PositionY);
        }
    }
}
