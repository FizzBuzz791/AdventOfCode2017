using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Day6
{
    public class Program
    {
        private static readonly string[] Input = new string[] { "227, 133", "140, 168", "99, 112", "318, 95", "219, 266", "134, 144", "306, 301",
            "189, 188", "58, 334", "337, 117", "255, 73", "245, 144", "102, 257", "255, 353", "303, 216", "141, 167", "40, 321", "201, 50", "60, 188",
            "132, 74", "125, 199", "176, 307", "204, 218", "338, 323", "276, 278", "292, 229", "109, 228", "85, 305", "86, 343", "97, 254", "182, 151",
            "110, 292", "285, 124", "43, 223", "153, 188", "285, 136", "334, 203", "84, 243", "92, 185", "330, 223", "259, 275", "106, 199", "183, 205",
            "188, 212", "231, 150", "158, 95", "174, 212", "279, 97", "172, 131", "247, 320" };

        public static void Main(string[] args)
        {
            Part1();
        }

        public static void Part1()
        {
            var AllLocations = new List<Location>();
            foreach (var i in Input)
            {
                var split = i.Split(',');
                AllLocations.Add(new Location(int.Parse(split[0]), int.Parse(split[1])));
            }

            int xBoundary = AllLocations.Max(l => l.X);
            int yBoundary = AllLocations.Max(l => l.Y);

            var currentLocation = new Location(0, 0);
            while (currentLocation.X <= xBoundary && currentLocation.Y <= yBoundary)
            {
                var distances = new Dictionary<Location, int>();
                foreach (var location in AllLocations)
                {
                    distances.Add(location, CalculateManhattanDistance(currentLocation, location));
                }

                var sortedDistances = distances.MinBy(d => d.Value);
                var shortestDistance = sortedDistances.First();
                if (sortedDistances.Count() == 1 || sortedDistances.Count(s => s.Value == shortestDistance.Value) == 1)
                {
                    // Only add if the current location is closest to a single point.
                    var newLocation = new Location(currentLocation.X, currentLocation.Y);
                    newLocation.IsInfinite = currentLocation.X == 0 || currentLocation.Y == 0 || currentLocation.X == xBoundary || currentLocation.Y == yBoundary;
                    AllLocations.SingleOrDefault(l => l == shortestDistance.Key).ClosestLocations.Add(newLocation);
                }

                if (currentLocation.Y == yBoundary)
                {
                    currentLocation.X++;
                    currentLocation.Y = 0;
                }
                else
                {
                    currentLocation.Y++;
                }
            }

            var nonInfiniteLocations = AllLocations.Where(l => l.ClosestLocations.All(c => !c.IsInfinite)).ToList();
            var largestNonInfinite = nonInfiniteLocations.MaxBy(l => l.ClosestLocations.Count).First();

            Console.WriteLine($"The largest non-infinite area is {largestNonInfinite.ClosestLocations.Count}");
        }

        public static int CalculateManhattanDistance(Location l1, Location l2)
        {
            return Math.Abs(l1.X - l2.X) + Math.Abs(l1.Y - l2.Y);
        }
    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public List<Location> ClosestLocations { get; } = new List<Location>();

        // Set this to true if any of the Closest Locations are at the boundary.
        public bool IsInfinite { get; set; } = false;

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
