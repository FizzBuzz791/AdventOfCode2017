using System;
using System.Collections.Generic;
using System.Drawing;

namespace Day11
{
    public class Program
    {
        public static void Main()
        {
            var grid = new FuelGrid(new Size(300, 300), 2694);
            Part1(grid);
            Part2(grid);
        }

        public static void Part1(FuelGrid grid)
        {
            (Point coordinate, int squareSize) = grid.GetHighestPowerSquare(3);
            Console.WriteLine($"Top-left coordinate for highest power square ({squareSize}x{squareSize}) is {coordinate.X},{coordinate.Y}");
        }

        public static void Part2(FuelGrid grid)
        {
            (Point coordinate, int squareSize) = grid.GetHighestPowerSquare(null);
            Console.WriteLine($"Top-left coordinate for highest power square ({squareSize}x{squareSize}) is {coordinate.X},{coordinate.Y}");
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

        public int? GetTotalPowerLevelForSquare(FuelCell cell, int squareSize)
        {
            int? totalPower = null;
            if (cell.Coordinate.X + squareSize - 1 < Size.Width && cell.Coordinate.Y + squareSize - 1 < Size.Height)
            {
                totalPower = 0;
                for (int i = 0; i < squareSize; i++)
                {
                    for (int j = 0; j < squareSize; j++)
                    {
                        totalPower += Grid[cell.Coordinate.X + i, cell.Coordinate.Y + j].PowerLevel;
                    }
                }
            }
            return totalPower;
        }

        public KeyValuePair<Point, int> GetHighestPowerSquare(int? squareSize)
        {
            var highestPower = 0;
            KeyValuePair<Point, int> target = new KeyValuePair<Point, int>();
            var currentSize = squareSize.HasValue ? squareSize.Value : 1;

            while (currentSize <= Size.Width)
            {
                Console.WriteLine($"Calculating for {currentSize}x{currentSize}");
                for (var i = 0; i + currentSize < Size.Width; i++)
                {
                    for (var j = 0; j + currentSize < Size.Height; j++)
                    {
                        FuelCell fuelCell = Grid[i, j];
                        var totalPowerLevel = GetTotalPowerLevelForSquare(fuelCell, currentSize);
                        if (totalPowerLevel != null && totalPowerLevel > highestPower)
                        {
                            target = new KeyValuePair<Point, int>(fuelCell.Coordinate, currentSize);
                            highestPower = (int)totalPowerLevel;
                        }
                    }
                }

                // Break the loop if squareSize has a value.
                currentSize += squareSize.HasValue ? Size.Width : 1;
            }

            // Grid is 0-indexed but the question expects 1-indexed, so add 1 here where it's inconsequential.
            return new KeyValuePair<Point, int>(new Point(target.Key.X + 1, target.Key.Y + 1), target.Value);
        }
    }

    public class FuelCell
    {
        public Point Coordinate { get; }
        public int RackId => Coordinate.X + CoordinateOffset + RackOffset;

        private int? CachedPowerLevel;

        private const int CoordinateOffset = 1;
        private const int RackOffset = 10;

        public int PowerLevel
        {
            get
            {
                if (CachedPowerLevel.HasValue)
                {
                    // Not sure if this will help, the math is pretty basic, but let's try.
                    return CachedPowerLevel.Value;
                }
                else
                {
                    int powerLevel = (RackId * (Coordinate.Y + CoordinateOffset) + GridSerialNumber) * RackId;

                    // Discard everything except the 100's digit (will be 0 if less than 100)
                    powerLevel = powerLevel / (int)Math.Pow(10, 3 - 1) % 10;

                    powerLevel -= 5;

                    CachedPowerLevel = powerLevel;

                    return powerLevel;
                }
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
