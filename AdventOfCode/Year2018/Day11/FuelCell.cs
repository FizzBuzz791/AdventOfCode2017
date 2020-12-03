using System;
using System.Drawing;

namespace AdventOfCode.Year2018.Day11
{
    public class FuelCell
    {
        public Point Coordinate { get; }
        private int RackId => Coordinate.X + CoordinateOffset + RackOffset;

        private int? _cachedPowerLevel;

        private const int CoordinateOffset = 1;
        private const int RackOffset = 10;

        public int PowerLevel
        {
            get
            {
                if (_cachedPowerLevel.HasValue)
                    return _cachedPowerLevel.Value;

                int powerLevel = (RackId * (Coordinate.Y + CoordinateOffset) + GridSerialNumber) * RackId;

                // Discard everything except the 100's digit (will be 0 if less than 100)
                powerLevel = powerLevel / (int)Math.Pow(10, 3 - 1) % 10;

                powerLevel -= 5;

                _cachedPowerLevel = powerLevel;

                return powerLevel;
            }
        }

        private int GridSerialNumber { get; }

        public FuelCell(Point coordinate, int gridSerialNumber)
        {
            Coordinate = coordinate;
            GridSerialNumber = gridSerialNumber;
        }
    }
}