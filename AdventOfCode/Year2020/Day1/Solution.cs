using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2020.Day1
{
    public class Solution : BaseSolution<int[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split("\n").Select(int.Parse).ToArray())
        {
        }

        public string SolvePart1()
        {
            var firstEntry = 0;
            var secondEntry = 0;
            
            for (var firstIndex = 0; firstIndex < Input.Length - 1; firstIndex++)
            {
                for (int secondIndex = firstIndex + 1; secondIndex < Input.Length; secondIndex++)
                {
                    if (Input[firstIndex] + Input[secondIndex] == 2020)
                    {
                        firstEntry = Input[firstIndex];
                        secondEntry = Input[secondIndex];
                        break;
                    }
                }
            }
            
            return $"Part 1: {firstEntry * secondEntry}";
        }

        public string SolvePart2()
        {
            var firstEntry = 0;
            var secondEntry = 0;
            var thirdEntry = 0;
            
            for (var firstIndex = 0; firstIndex < Input.Length - 2; firstIndex++)
            {
                for (int secondIndex = firstIndex + 1; secondIndex < Input.Length - 1; secondIndex++)
                {
                    for (int thirdIndex = secondIndex + 1; thirdIndex < Input.Length; thirdIndex++)
                    {
                        if (Input[firstIndex] + Input[secondIndex] + Input[thirdIndex] == 2020)
                        {
                            firstEntry = Input[firstIndex];
                            secondEntry = Input[secondIndex];
                            thirdEntry = Input[thirdIndex];
                            break;
                        }
                    }
                }
            }
            
            return $"Part 2: {firstEntry * secondEntry * thirdEntry}";
        }
    }
}