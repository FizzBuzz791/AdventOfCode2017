using System.Drawing;

namespace Solutions.Day3
{
    public class Step
    {
        public bool Wire1Touched { get; set; }
        public bool Wire2Touched { get; set; }
        public bool Intersection => Wire1Touched && Wire2Touched;

        public int Wire1Steps { get; set; }
        public int Wire2Steps { get; set; }
        public Point Location { get; }

        public Step(Point location)
        {
            Location = location;
        }
    }
}