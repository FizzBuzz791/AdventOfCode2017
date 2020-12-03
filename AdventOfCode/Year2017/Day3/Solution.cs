using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2017.Day3
{
    public class Solution : BaseSolution<int>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, int.Parse)
        {
        }

        public string SolvePart1()
        {
            return "";
        }

        public string SolvePart2()
        {
            Point p = CalculateSpiralCoordinate(Input);
            return $"Part 1: {Math.Abs(p.X + p.Y) + 1}";
        }
        
        private static Point CalculateSpiralCoordinate(int index)
        {
            // (di, dj) is a vector - direction in which we move right now
            var di = 1;
            var dj = 0;
            // length of current segment
            var segmentLength = 1;

            // current position (i, j) and how much of current segment we passed
            var i = 0;
            var j = 0;
            var segmentPassed = 0;

            List<KeyValuePair<Point, int>> points =
                new() {new KeyValuePair<Point, int>(new Point(0, 0), 1)};
            var foundFirstLarger = false;

            for (var k = 1; k <= index; ++k)
            {
                // make a step, add 'direction' vector (di, dj) to current position (i, j)
                i += di;
                j += dj;
                ++segmentPassed;

                if (segmentPassed == segmentLength)
                {
                    // done with current segment
                    segmentPassed = 0;

                    // 'rotate' directions
                    int buffer = di;
                    di = -dj;
                    dj = buffer;

                    // increase segment length if necessary
                    if (dj == 0)
                        ++segmentLength;
                }

                if (!foundFirstLarger)
                {
                    var sum = 0;
                    if (points.Exists(p => p.Key == new Point(i - 1, j)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i - 1, j)).Value;

                    if (points.Exists(p => p.Key == new Point(i, j - 1)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i, j - 1)).Value;

                    if (points.Exists(p => p.Key == new Point(i - 1, j - 1)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i - 1, j - 1)).Value;

                    if (points.Exists(p => p.Key == new Point(i + 1, j)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i + 1, j)).Value;
                    if (points.Exists(p => p.Key == new Point(i, j + 1)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i, j + 1)).Value;

                    if (points.Exists(p => p.Key == new Point(i + 1, j + 1)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i + 1, j + 1)).Value;

                    if (points.Exists(p => p.Key == new Point(i - 1, j + 1)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i - 1, j + 1)).Value;

                    if (points.Exists(p => p.Key == new Point(i + 1, j - 1)))
                        sum += points.SingleOrDefault(p => p.Key == new Point(i + 1, j - 1)).Value;

                    points.Add(new KeyValuePair<Point, int>(new Point(i, j), sum));
                    if (sum > index)
                    {
                        foundFirstLarger = true;
                        Console.WriteLine($"Part 2: {sum}");
                    }
                }

                if (k == index)
                    return new Point(i, j); // Finally at the point we're after.
            }
            
            return new Point();
        }
    }
}