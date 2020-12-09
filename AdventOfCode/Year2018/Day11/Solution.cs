using System.Drawing;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day11
{
    public class Solution : BaseSolution<int>, ISolvable
    {
        private readonly FuelGrid _grid;
        
        public Solution(IPuzzle puzzle) : base(puzzle, int.Parse)
        {
            _grid = new FuelGrid(new Size(300, 300), Input);
        }

        public string SolvePart1()
        {
            (Point coordinate, int squareSize) = _grid.GetHighestPowerSquare(3);
            return $"Part 1: ({squareSize}x{squareSize}) is {coordinate.X},{coordinate.Y}";
        }

        public string SolvePart2()
        {
            (Point coordinate, int squareSize) = _grid.GetHighestPowerSquare(null);
            return $"Part 2: ({squareSize}x{squareSize}) is {coordinate.X},{coordinate.Y}";
        }
    }
}