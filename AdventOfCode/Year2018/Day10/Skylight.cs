using System.Drawing;

namespace AdventOfCode.Year2018.Day10
{
    public class Skylight
    {
        public Point Position { get; }
        public Point Velocity { get; }

        public Skylight(string input)
        {
            Position = new Point(int.Parse(input.Substring(10, 6)), int.Parse(input.Substring(18, 6)));
            Velocity = new Point(int.Parse(input.Substring(36, 2)), int.Parse(input.Substring(40, 2)));
        }

        public override string ToString() => $"position=<{Position.X}, {Position.Y}> velocity=<{Velocity.X}, {Velocity.Y}>";
    }
}