using System;
using System.Collections.Generic;
using System.Drawing;
using MoreLinq;

namespace Day11
{
    public class Program
    {
        public static void Main()
        {
            var grid = new FuelGrid(new Size(300, 300), 2694);
            (Point coordinate, int powerLevel) = grid.GetHighestPowerSquare();
            Console.WriteLine($"Top-left coordinate for highest power square ({powerLevel}) is {coordinate.X},{coordinate.Y}");
        }
    }

    public class FuelGrid
    {
        public FuelCell[,] Grid { get; }

        public Size Size { get; }

        public FuelGrid(Size size, int gridSerialNumber)
        {
            Size = size;

            Grid = new FuelCell[size.Width, size.Height];

            for (var i = 0; i < Size.Width; i++)
            {
                for (var j = 0; j < Size.Height; j++)
                {
                    Grid[i, j] = new FuelCell(new Point(i, j), gridSerialNumber);
                }
            }
        }

        public int? GetTotalPowerLevelForSquare(FuelCell cell)
        {
            int? totalPower = null;
            if (cell.Coordinate.X + 2 < Size.Width && cell.Coordinate.Y + 2 < Size.Height)
            {
                int topLeftPower = cell.PowerLevel;
                int topMiddlePower = Grid[cell.Coordinate.X + 1, cell.Coordinate.Y].PowerLevel;
                int topRightPower = Grid[cell.Coordinate.X + 2, cell.Coordinate.Y].PowerLevel;

                int middleLeftPower = Grid[cell.Coordinate.X, cell.Coordinate.Y + 1].PowerLevel;
                int middleMiddlePower = Grid[cell.Coordinate.X + 1, cell.Coordinate.Y + 1].PowerLevel;
                int middleRightPower = Grid[cell.Coordinate.X + 2, cell.Coordinate.Y + 1].PowerLevel;

                int bottomLeftPower = Grid[cell.Coordinate.X, cell.Coordinate.Y + 2].PowerLevel;
                int bottomMiddlePower = Grid[cell.Coordinate.X + 1, cell.Coordinate.Y + 2].PowerLevel;
                int bottomRightPower = Grid[cell.Coordinate.X + 2, cell.Coordinate.Y + 2].PowerLevel;

                totalPower = topLeftPower + topMiddlePower + topRightPower +
                    middleLeftPower + middleMiddlePower + middleRightPower +
                    bottomLeftPower + bottomMiddlePower + bottomRightPower;
            }
            return totalPower;
        }

        public KeyValuePair<Point, int> GetHighestPowerSquare()
        {
            var powerSquares = new Dictionary<Point, int>();
            for (var i = 0; i < Size.Width; i++)
            {
                for (var j = 0; j < Size.Height; j++)
                {
                    FuelCell fuelCell = Grid[i, j];
                    var totalPowerLevel = GetTotalPowerLevelForSquare(fuelCell);
                    if (totalPowerLevel != null)
                        powerSquares.Add(fuelCell.Coordinate, (int)totalPowerLevel);
                }
            }

            (Point coordinate, int powerLevel) = powerSquares.MaxBy(p => p.Value).First();
            // Grid is 0-indexed but the question expects 1-indexed, so add 1 here where it's inconsequential.
            return new KeyValuePair<Point, int>(new Point(coordinate.X + 1, coordinate.Y + 1), powerLevel);
        }
    }

    public class FuelCell
    {
        public Point Coordinate { get; }
        public int RackId => Coordinate.X + CoordinateOffset + RackOffset;

        private const int CoordinateOffset = 1;
        private const int RackOffset = 10;

        public int PowerLevel
        {
            get
            {
                int powerLevel = (RackId * (Coordinate.Y + CoordinateOffset) + GridSerialNumber) * RackId;

                // Discard everything except the 100's digit (will be 0 if less than 100)
                powerLevel = powerLevel / (int)Math.Pow(10, 3 - 1) % 10;

                return powerLevel - 5;
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
