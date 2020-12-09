using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using NAoCHelper;

namespace AdventOfCode.Year2020.Day3
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        private readonly int _patternWidth;
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
            _patternWidth = Input[0].Length;
        }

        public string SolvePart1() => $"Part 1: {GetTreeCountForSlope(new Point(3, 1))}";

        public string SolvePart2()
        {
            BigInteger multipliedTreeCount = BigInteger.One;
            List<Point> possibleSlopes = new()
                { new Point(1, 1), new Point(3, 1), new Point(5, 1), new Point(7, 1), new Point(1, 2) };
            
            foreach (Point slope in possibleSlopes)
            {
                multipliedTreeCount *= GetTreeCountForSlope(slope);
            }

            return $"Part 2: {multipliedTreeCount}";
        }

        private int GetTreeCountForSlope(Point slope)
        {
            var treeCount = 0;
            var currentPosition = new Point(0, 0);
            while (currentPosition.Y + 1 < Input.Length)
            {
                currentPosition.X += slope.X;
                currentPosition.Y += slope.Y;

                if (currentPosition.X >= _patternWidth)
                    currentPosition.X -= _patternWidth;

                if (Input[currentPosition.Y][currentPosition.X] == '#')
                    treeCount++;
            }

            return treeCount;
        }
    }
}