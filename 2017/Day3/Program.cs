using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        private static void Main()
        {
            const int CELL = 325489;
            Point p = CalculateSpiralCoordinate(CELL);

            Console.WriteLine($"X: {p.X},Y: {p.Y}");
            // +1 for the final move.
            Console.WriteLine($"Manhattan Distance: {Math.Abs(p.X + p.Y) + 1}");
            Console.ReadKey();
        }

        private static Point CalculateSpiralCoordinate(int index)
        {
            // (di, dj) is a vector - direction in which we move right now
            int di = 1;
            int dj = 0;
            // length of current segment
            int segmentLength = 1;

            // current position (i, j) and how much of current segment we passed
            int i = 0;
            int j = 0;
            int segmentPassed = 0;

            List<KeyValuePair<Point, int>> points =
                new List<KeyValuePair<Point, int>> {new KeyValuePair<Point, int>(new Point(0, 0), 1)};
            bool foundFirstLarger = false;

            for (int k = 1; k <= index; ++k)
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
                    {
                        ++segmentLength;
                    }
                }

                if (!foundFirstLarger)
                {
                    int sum = 0;
                    if (points.Exists(p => p.Key == new Point(i - 1, j)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i - 1, j)).Value;
                    }
                    if (points.Exists(p => p.Key == new Point(i, j - 1)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i, j - 1)).Value;
                    }
                    if (points.Exists(p => p.Key == new Point(i - 1, j - 1)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i - 1, j - 1)).Value;
                    }

                    if (points.Exists(p => p.Key == new Point(i + 1, j)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i + 1, j)).Value;
                    }
                    if (points.Exists(p => p.Key == new Point(i, j + 1)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i, j + 1)).Value;
                    }
                    if (points.Exists(p => p.Key == new Point(i + 1, j + 1)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i + 1, j + 1)).Value;
                    }

                    if (points.Exists(p => p.Key == new Point(i - 1, j + 1)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i - 1, j + 1)).Value;
                    }
                    if (points.Exists(p => p.Key == new Point(i + 1, j - 1)))
                    {
                        sum += points.SingleOrDefault(p => p.Key == new Point(i + 1, j - 1)).Value;
                    }

                    points.Add(new KeyValuePair<Point, int>(new Point(i, j), sum));
                    if (sum > index)
                    {
                        foundFirstLarger = true;
                        Console.WriteLine($"Part 2: {sum}");
                    }
                }

                if (k == index)
                {
                    // Finally at the point we're after.
                    return new Point(i, j);
                }
            }
            return new Point();
        }
    }
}