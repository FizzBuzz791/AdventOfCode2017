using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day10
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            List<Skylight> originalSkyLights = Input.Select(skylight => new Skylight(skylight)).ToList();
            
            var iterations = new Dictionary<int, int>();
            for (var i = 0; i < 20000; i++)
            {
                (Point min, Point max) = CalculateBoundingBoxAtTime(CalculateNewPositionsAtTime(originalSkyLights, i));
                iterations.Add(i, max.X - min.X + max.Y - min.Y);
            }

            (int time, int _) = iterations.OrderBy(i => i.Value).First();

            return $"Part 1: {DrawPositionsAtTime(originalSkyLights, time)}";
        }

        public string SolvePart2()
        {
            List<Skylight> originalSkyLights = Input.Select(skylight => new Skylight(skylight)).ToList();
            
            var iterations = new Dictionary<int, int>();
            for (var i = 0; i < 20000; i++)
            {
                (Point min, Point max) = CalculateBoundingBoxAtTime(CalculateNewPositionsAtTime(originalSkyLights, i));
                iterations.Add(i, max.X - min.X + max.Y - min.Y);
            }

            (int time, int _) = iterations.OrderBy(i => i.Value).First();
            return $"Part 2: {time}";
        }

        private static List<Point> CalculateNewPositionsAtTime(IEnumerable<Skylight> skyLights, int time) =>
            skyLights.Select(skylight => skylight.CalculatePositionAtTime(time)).ToList();

        private static Tuple<Point, Point> CalculateBoundingBoxAtTime(IReadOnlyCollection<Point> positions)
        {
            int minX = positions.Min(p => p.X);
            int maxX = positions.Max(p => p.X);
            int minY = positions.Min(p => p.Y);
            int maxY = positions.Max(p => p.Y);

            return new Tuple<Point, Point>(new Point(minX, minY), new Point(maxX, maxY));
        }

        private static string DrawPositionsAtTime(IEnumerable<Skylight> skyLights, int time)
        {
            List<Point> newPositions = CalculateNewPositionsAtTime(skyLights, time);
            (Point min, Point max) = CalculateBoundingBoxAtTime(newPositions);

            var grid = new List<char[]>();
            int lineLength = max.X - min.X + 1;
            int lineCount = max.Y - min.Y + 1;
            for (var i = 0; i < lineCount; i++)
            {
                grid.Add(new string(' ', lineLength).ToCharArray());
            }

            foreach (Point skyLight in newPositions)
            {
                grid[skyLight.Y - min.Y][skyLight.X - min.X] = '#';
            }

            StringBuilder builder = new();
            foreach (var row in grid)
            {
                builder.AppendLine(string.Join(string.Empty, row));
            }

            return builder.ToString();
        }
    }
}