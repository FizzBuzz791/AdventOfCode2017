using System.Drawing;

namespace AdventOfCode.Year2018.Day10
{
    public static class SkylightExtensions
    {
        public static Point CalculatePositionAtTime(this Skylight skyLight, int time) => new(
            skyLight.Position.X + skyLight.Velocity.X * time, skyLight.Position.Y + skyLight.Velocity.Y * time);
    }
}