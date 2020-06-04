using System.Drawing;

namespace Day15
{
    public class Node
    {
        public Point Location { get; }
        public int Distance { get; set; } = int.MaxValue;
        public bool Visited { get; set; } = false;

        public Node(Point location)
        {
            Location = location;
        }
    }
}