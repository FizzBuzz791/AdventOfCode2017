using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day6
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            var allLocations = new List<Location>();
            foreach (var i in Input)
            {
                string[] split = i.Split(',');
                allLocations.Add(new Location(int.Parse(split[0]), int.Parse(split[1])));
            }

            int xBoundary = allLocations.Max(l => l.X);
            int yBoundary = allLocations.Max(l => l.Y);

            var currentLocation = new Location(0, 0);
            while (currentLocation.X <= xBoundary && currentLocation.Y <= yBoundary)
            {
                Dictionary<Location, int> distances = allLocations.ToDictionary(location => location,
                    location => CalculateManhattanDistance(currentLocation, location));

                var sortedDistances = distances.MinBy(d => d.Value);
                KeyValuePair<Location, int> shortestDistance = sortedDistances.First();
                if (sortedDistances.Count() == 1 || sortedDistances.Count(s => s.Value == shortestDistance.Value) == 1)
                {
                    // Only add if the current location is closest to a single point.
                    var newLocation = new Location(currentLocation.X, currentLocation.Y)
                    {
                        IsInfinite = currentLocation.X == 0 || currentLocation.Y == 0 ||
                                     currentLocation.X == xBoundary || currentLocation.Y == yBoundary
                    };
                    allLocations.SingleOrDefault(l => l == shortestDistance.Key)?.ClosestLocations.Add(newLocation);
                }

                // Move to the next location
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

            List<Location> nonInfiniteLocations = allLocations.Where(l => l.ClosestLocations.All(c => !c.IsInfinite)).ToList();
            Location? largestNonInfinite = nonInfiniteLocations.MaxBy(l => l.ClosestLocations.Count).First();

            return $"The largest non-infinite area is {largestNonInfinite.ClosestLocations.Count}";
        }

        public string SolvePart2()
        {
            var allLocations = new List<Location>();
            foreach (var i in Input)
            {
                string[] split = i.Split(',');
                allLocations.Add(new Location(int.Parse(split[0]), int.Parse(split[1])));
            }

            int xBoundary = allLocations.Max(l => l.X);
            int yBoundary = allLocations.Max(l => l.Y);

            var locationsInSafeRegion = new List<Location>();
            var currentLocation = new Location(0, 0);
            while (currentLocation.X <= xBoundary && currentLocation.Y <= yBoundary)
            {
                Dictionary<Location, int> distances = allLocations.ToDictionary(location => location,
                    location => CalculateManhattanDistance(currentLocation, location));

                int totalDistance = distances.Sum(d => d.Value);
                if (totalDistance < 10000)
                    locationsInSafeRegion.Add(new Location(currentLocation.X, currentLocation.Y));

                // Move to the next location
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

            return $"The size of the safe region is {locationsInSafeRegion.Count}";
        }
        
        private static int CalculateManhattanDistance(Location l1, Location l2) => Math.Abs(l1.X - l2.X) + Math.Abs(l1.Y - l2.Y);
    }
}