using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2017.Day2
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            var checksum = 0;
            
            foreach (var row in Input)
            {
                List<int> cells = row.Split('\t').Select(int.Parse).ToList();
                checksum += cells.Max() - cells.Min();
            }

            return $"Part 1: {checksum}";
        }

        public string SolvePart2() 
        {
            var checksum = 0;
            
            foreach (var row in Input)
            {
                List<int> cells = row.Split('\t').Select(int.Parse).ToList();
                
                var i = 0;
                var j = 1;

                while (i != j && cells[i] % cells[j] != 0)
                {
                    if (j < cells.Count - 1)
                    {
                        j++;
                    }
                    else
                    {
                        i++;
                        j = i + 1;
                    }
                }

                checksum += cells[i] / cells[j];
            }

            return $"Part 2: {checksum}";
        }
    }
}