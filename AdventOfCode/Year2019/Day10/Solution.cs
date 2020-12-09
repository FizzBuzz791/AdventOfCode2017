﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2019.Day10
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        private const char Asteroid = '#';
        private Point BestLocation { get; set; }
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split("\n"))
        {
        }

        public string SolvePart1()
        {
            // Find best location
            BestLocation = new Point(-1, -1);
            var highestAsteroidCount = 0;
            List<Point> asteroids = FindAsteroids();
            foreach (Point asteroid in asteroids)
            {
                List<Point> otherAsteroids = asteroids.Where(a => a != asteroid).ToList();
                var uniqueAngles = new List<double>();
                foreach (Point otherAsteroid in otherAsteroids)
                {
                    int xDiff = otherAsteroid.X - asteroid.X;
                    int yDiff = otherAsteroid.Y - asteroid.Y;
                    double angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
                    if (!uniqueAngles.Contains(angle))
                        uniqueAngles.Add(angle);
                }

                if (uniqueAngles.Count > highestAsteroidCount)
                {
                    BestLocation = asteroid;
                    highestAsteroidCount = uniqueAngles.Count;
                }
            }

            return $"Part 1: {highestAsteroidCount}";
        }

        public string SolvePart2()
        {
            List<Point> asteroids = FindAsteroids();

            // Remove the laser (BestLocation) from the list of *all* asteroids.
            asteroids = asteroids.Where(a => a != BestLocation).ToList();

            var angles = new SortedDictionary<double, SortedDictionary<double, Point>>();
            foreach (Point asteroid in asteroids)
            {
                int xDiff = asteroid.X - BestLocation.X;
                int yDiff = asteroid.Y - BestLocation.Y;
                double angle = ConvertTo360(Math.Atan2(-yDiff, xDiff) * 180.0 / Math.PI);
                double distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

                if (angles.ContainsKey(angle))
                    angles[angle].Add(distance, asteroid);
                else
                    angles.Add(angle, new SortedDictionary<double, Point> { { distance, asteroid } });
            }

            var counter = 0;
            var targetAsteroid = Point.Empty;
            foreach (double key in angles.Keys)
            {
                if (counter == 199)
                    targetAsteroid = angles[key].First().Value;

                counter++;
            }

            return $"Part 2: {targetAsteroid.X * 100 + targetAsteroid.Y}";
        }
        
        private List<Point> FindAsteroids()
        {
            // Loop through "grid" and find asteroids.
            var asteroids = new List<Point>();
            for (var y = 0; y < Input.Length; y++)
            {
                char[] row = Input[y].ToCharArray();
                for (var x = 0; x < row.Length; x++)
                {
                    if (row[x] == Asteroid)
                        asteroids.Add(new Point(x, y));
                }
            }

            return asteroids;
        }

        private static double ConvertTo360(double deg)
        {
            deg = deg switch
            {
                <= 90 and >= 0 => Math.Abs(deg - 90),
                < 0 => Math.Abs(deg) + 90,
                _ => 450 - deg
            };

            return deg;
        }
    }
}