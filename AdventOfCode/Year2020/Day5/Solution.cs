using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2020.Day5
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            var highestId = int.MinValue;
            
            foreach (var seat in Input)
            {
                int id = ProcessBinarySpacePartition(seat);
                if (highestId < id)
                    highestId = id;
            }

            return $"Part 1: {highestId}";
        }

        public string SolvePart2()
        {
            List<int> ids = new();

            foreach (var seat in Input)
            {
                ids.Add(ProcessBinarySpacePartition(seat));
            }
            
            ids.Sort();

            var targetSeat = 0;
            for (int i = ids.First(); i <= ids.Last(); i++)
            {
                if (!ids.Contains(i) && ids.Contains(i - 1) && ids.Contains(i + 1))
                    targetSeat = i;
            }

            return $"Part 2: {targetSeat}";
        }

        private static int ProcessBinarySpacePartition(string seat)
        {
            var lowerRow = 0;
            var upperRow = 127;
            var lowerColumn = 0;
            var upperColumn = 7;

            while (seat.Length > 0)
            {
                char partition = seat.Take(1).First();
                seat = seat.Substring(1);
                switch (partition)
                {
                    case 'F':
                        upperRow = lowerRow + (int)Math.Floor((upperRow - lowerRow) / 2.0);
                        break;
                    case 'B':
                        lowerRow = upperRow - (int)Math.Floor((upperRow - lowerRow) / 2.0);
                        break;
                    case 'L':
                        upperColumn = lowerColumn + (int) Math.Floor((upperColumn - lowerColumn) / 2.0);
                        break;
                    case 'R':
                        lowerColumn = upperColumn - (int) Math.Floor((upperColumn - lowerColumn) / 2.0);
                        break;
                }
            }

            return lowerRow * 8 + lowerColumn;
        }
    }
}