using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day1
{
    public class Solution : BaseSolution<int[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n').Select(int.Parse).ToArray())
        {
        }

        public string SolvePart1()
        {
            return $"Part 1: {Input.Sum()}";
        }

        public string SolvePart2()
        {
            var final = 0;
            var count = 0;
            var duplicateFound = false;
            HashSet<int> frequencies = new() { final };

            do
            {
                final += Input[count];
                if (frequencies.Contains(final))
                    duplicateFound = true;
                else
                    frequencies.Add(final);

                if (count < Input.Length - 1)
                    count++;
                else
                    count = 0;
            } while (!duplicateFound);

            return $"Part 2: {final}";
        }
    }
}