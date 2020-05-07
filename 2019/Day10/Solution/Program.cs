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

            var asteroids = FindAsteroids(input);

            var bestLocation = Part1(asteroids);
            Console.WriteLine($"Best is ({bestLocation.Item1.X},{bestLocation.Item1.Y}) with {bestLocation.Item2} other asteroids detected.");

            var targetAsteroid = Part2(bestLocation.Item1, asteroids);
            Console.WriteLine($"200th Asteroid Checksum: {targetAsteroid.X * 100 + targetAsteroid.Y}");
        }

        public static List<Point> FindAsteroids(string input)
        {
            var rows = input.Split("\n");

            // Loop through "grid" and find asteroids.
            var asteroids = new List<Point>();
            for (int y = 0; y < rows.Length; y++)
            {
                var row = rows[y].ToCharArray();
                for (int x = 0; x < row.Length; x++)
                {
                    if (row[x] == Asteriod)
                        asteroids.Add(new Point(x, y));
                }
            }

            return asteroids;
        }

        public static Tuple<Point, int> Part1(List<Point> asteroids)
        {
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

        public static Point Part2(Point laser, List<Point> asteroids)
        {
            // Remove the laser from the list of *all* asteroids.
            asteroids = asteroids.Where(a => a != laser).ToList();

            var angles = new SortedDictionary<double, SortedDictionary<double, Point>>();
            foreach (var asteroid in asteroids)
            {
                var xDiff = asteroid.X - laser.X;
                var yDiff = asteroid.Y - laser.Y;
                var angle = ConvertTo360(Math.Atan2(-yDiff, xDiff) * 180.0 / Math.PI);
                var distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

                if (angles.ContainsKey(angle))
                    angles[angle].Add(distance, asteroid);
                else
                    angles.Add(angle, new SortedDictionary<double, Point>() { { distance, asteroid } });
            }

            int counter = 0;
            Point targetAsteroid = Point.Empty;
            foreach (var key in angles.Keys)
            {
                if (counter == 199)
                    targetAsteroid = angles[key].First().Value;

                counter++;
            }

            return targetAsteroid;
        }

        public static double ConvertTo360(double deg)
        {
            if (deg <= 90 && deg >= 0)
            {
                deg = Math.Abs(deg - 90);
            }
            else if (deg < 0)
            {
                deg = Math.Abs(deg) + 90;
            }
            else
            {
                deg = 450 - deg;
            }
            return deg;
        }
    }
}
