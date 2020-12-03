using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2019.Day4
{
    public class Solution : BaseSolution<int[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split("-").Select(int.Parse).ToArray())
        {
        }

        public string SolvePart1()
        {
            var validNumberCount = 0;
            for (int i = Input[0]; i < Input[1]; i++)
            {
                if (NumberIsValid(i))
                    validNumberCount++;
            }

            return $"Part 1: {validNumberCount}";
        }

        public string SolvePart2()
        {
            var validNumberCount = 0;
            for (int i = Input[0]; i < Input[1]; i++)
            {
                if (NumberIsValid(i, true))
                    validNumberCount++;
            }

            return $"Part 2: {validNumberCount}";
        }

        private static bool NumberIsValid(int number, bool doPart2Checks = false)
        {
            char[] numberParts = number.ToString().ToArray();

            int firstPart = int.Parse(numberParts[0].ToString());
            int secondPart = int.Parse(numberParts[1].ToString());
            int thirdPart = int.Parse(numberParts[2].ToString());
            int fourthPart = int.Parse(numberParts[3].ToString());
            int fifthPart = int.Parse(numberParts[4].ToString());
            int sixthPart = int.Parse(numberParts[5].ToString());

            bool hasDouble = firstPart == secondPart || secondPart == thirdPart || thirdPart == fourthPart ||
                             fourthPart == fifthPart || fifthPart == sixthPart;
            bool onlyIncreases = firstPart <= secondPart && secondPart <= thirdPart && thirdPart <= fourthPart &&
                                 fourthPart <= fifthPart && fifthPart <= sixthPart;

            if (hasDouble && onlyIncreases && doPart2Checks)
            {
                List<IGrouping<int, char>> repeats = numberParts.GroupBy(p => numberParts.Count(n => n == p))
                    .Where(g => g.Key >= 2).ToList();
                List<IGrouping<int, char>> enumerable = repeats.ToList();
                if (enumerable.Count == 1 && enumerable.First().Key != 2)
                {
                    // x111xx
                    hasDouble = false;
                }
            }

            return hasDouble && onlyIncreases;
        }
    }
}