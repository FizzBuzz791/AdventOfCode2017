using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode.Year2018.Day11
{
    public class FuelGrid
    {
        private FuelCell[,] Grid { get; }

        private Size Size { get; }

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

        private int? GetTotalPowerLevelForSquare(FuelCell cell, int squareSize)
        {
            int? totalPower = null;
            if (cell.Coordinate.X + squareSize - 1 < Size.Width && cell.Coordinate.Y + squareSize - 1 < Size.Height)
            {
                totalPower = 0;
                for (var i = 0; i < squareSize; i++)
                {
                    for (var j = 0; j < squareSize; j++)
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
            var target = new KeyValuePair<Point, int>();
            int currentSize = squareSize ?? 1;

            while (currentSize <= Size.Width)
            {
                for (var i = 0; i + currentSize < Size.Width; i++)
                {
                    for (var j = 0; j + currentSize < Size.Height; j++)
                    {
                        FuelCell fuelCell = Grid[i, j];
                        int? totalPowerLevel = GetTotalPowerLevelForSquare(fuelCell, currentSize);
                        if (totalPowerLevel > highestPower)
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
}