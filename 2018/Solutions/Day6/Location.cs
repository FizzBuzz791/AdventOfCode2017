using System.Collections.Generic;

namespace Solutions.Day6
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public List<Location> ClosestLocations { get; } = new List<Location>();

        // Set this to true if any of the Closest Locations are at the boundary.
        public bool IsInfinite { get; set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}