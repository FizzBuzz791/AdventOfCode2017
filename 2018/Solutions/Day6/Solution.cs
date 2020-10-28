using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using NAoCHelper;

namespace Solutions.Day6
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        private List<Location> AllLocations { get; } = new List<Location>();
        private int XBoundary => AllLocations.Max(l => l.X);
        private int YBoundary => AllLocations.Max(l => l.Y);
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split("\n"))
        {
            foreach (var i in Input)
            {
                string[] split = i.Split(',');
                AllLocations.Add(new Location(int.Parse(split[0]), int.Parse(split[1])));
            }
        }

        public string SolvePart1()
        {
            var currentLocation = new Location(0, 0);
            while (currentLocation.X <= XBoundary && currentLocation.Y <= YBoundary)
            {
                Dictionary<Location, int> distances = AllLocations.ToDictionary(location => location,
                    location => CalculateManhattanDistance(currentLocation, location));

                IExtremaEnumerable<KeyValuePair<Location, int>>? sortedDistances = distances.MinBy(d => d.Value);
                KeyValuePair<Location, int> shortestDistance = sortedDistances.First();
                if (sortedDistances.Count() == 1 || sortedDistances.Count(s => s.Value == shortestDistance.Value) == 1)
                {
                    // Only add if the current location is closest to a single point.
                    var newLocation = new Location(currentLocation.X, currentLocation.Y)
                    {
                        IsInfinite = currentLocation.X == 0 || currentLocation.Y == 0 ||
                                     currentLocation.X == XBoundary || currentLocation.Y == YBoundary
                    };
                    AllLocations.SingleOrDefault(l => l == shortestDistance.Key)?.ClosestLocations.Add(newLocation);
                }

                // Move to the next location
                if (currentLocation.Y == YBoundary)
                {
                    currentLocation.X++;
                    currentLocation.Y = 0;
                }
                else
                {
                    currentLocation.Y++;
                }
            }

            List<Location> nonInfiniteLocations = AllLocations.Where(l => l.ClosestLocations.All(c => !c.IsInfinite)).ToList();
            Location? largestNonInfinite = nonInfiniteLocations.MaxBy(l => l.ClosestLocations.Count).First();

            return $"Part 1: {largestNonInfinite.ClosestLocations.Count}";
        }

        public string SolvePart2()
        {
            var locationsInSafeRegion = new List<Location>();
            var currentLocation = new Location(0, 0);
            while (currentLocation.X <= XBoundary && currentLocation.Y <= YBoundary)
            {
                Dictionary<Location, int> distances = AllLocations.ToDictionary(location => location,
                    location => CalculateManhattanDistance(currentLocation, location));

                int totalDistance = distances.Sum(d => d.Value);
                if (totalDistance < 10000)
                    locationsInSafeRegion.Add(new Location(currentLocation.X, currentLocation.Y));

                // Move to the next location
                if (currentLocation.Y == YBoundary)
                {
                    currentLocation.X++;
                    currentLocation.Y = 0;
                }
                else
                {
                    currentLocation.Y++;
                }
            }

            return $"Part 2: {locationsInSafeRegion.Count}";
        }

        private static int CalculateManhattanDistance(Location l1, Location l2)
        {
            return Math.Abs(l1.X - l2.X) + Math.Abs(l1.Y - l2.Y);
        }
    }
}