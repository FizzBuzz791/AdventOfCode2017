using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NAoCHelper;

namespace Day10
{
    public class Program
    {
        private const char Asteriod = '#';

        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("c617493e-f753-4626-bb52-9661e64c9878"));
            var puzzle = new Puzzle(user, 2019, 10);
            var input = puzzle.GetInputAsync().Result;

            var bestLocation = Part1(input);
            Console.WriteLine($"Best is ({bestLocation.Item1.X},{bestLocation.Item1.Y}) with {bestLocation.Item2} other asteroids detected.");
        }

        public static Tuple<Point, int> Part1(string input)
        {
            var rows = input.Split("\n");

            // Create grid & remember asteroid locations
            var grid = new char[rows[0].Length, rows.Length];
            var asteroids = new List<Point>();
            for (int y = 0; y < rows.Length; y++)
            {
                var row = rows[y].ToCharArray();
                for (int x = 0; x < row.Length; x++)
                {
                    grid[x, y] = row[x];
                    if (row[x] == Asteriod)
                        asteroids.Add(new Point(x, y));
                }
            }

            // Find best location
            var bestLocation = new Point(-1, -1);
            var highestAsteroidCount = 0;
            foreach (var asteroid in asteroids)
            {
                var otherAsteroids = asteroids.Where(a => a != asteroid).ToList();
                var uniqueAngles = new List<double>();
                foreach (var otherAsteroid in otherAsteroids)
                {
                    var xDiff = otherAsteroid.X - asteroid.X;
                    var yDiff = otherAsteroid.Y - asteroid.Y;
                    var angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
                    if (!uniqueAngles.Contains(angle))
                        uniqueAngles.Add(angle);
                }

                if (uniqueAngles.Count > highestAsteroidCount)
                {
                    bestLocation = asteroid;
                    highestAsteroidCount = uniqueAngles.Count;
                }
            }

            return new Tuple<Point, int>(bestLocation, highestAsteroidCount);
        }
    }
}
