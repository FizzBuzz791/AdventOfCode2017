using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day5
{
    public class Solution : BaseSolution<string>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x)
        {
        }

        public string SolvePart1()
        {
            List<char> fullPolymer = Input.ToCharArray().ToList();
            IList<char> collapsedPolymer = ReactPolymer(fullPolymer);

            return $"Part 1: {collapsedPolymer.Count}";
        }

        public string SolvePart2()
        {
            IEnumerable<char> distinctUnitTypes = Input.ToUpper().ToCharArray().ToList().Distinct();

            var polymerOptions = new Dictionary<string, int>(); // Key: Removed Unit Type, Value: Collapsed Length
            foreach (char unitType in distinctUnitTypes)
            {
                var unit = unitType.ToString();
                List<char> improvedPolymer = Input.Replace(unit.ToUpper(), string.Empty).Replace(unit.ToLower(), string.Empty).ToCharArray().ToList();
                IList<char> collapsedPolymer = ReactPolymer(improvedPolymer);
                polymerOptions.Add(unit, collapsedPolymer.Count);
            }

            KeyValuePair<string, int> optimalPolymer = polymerOptions.MinBy(o => o.Value).FirstOrDefault();
            return $"Part 2: {optimalPolymer.Value}";
        }
        
        private static IList<char> ReactPolymer(List<char> polymer)
        {
            var index = 0;

            while (index + 1 < polymer.Count)
            {
                // Lowercase and Uppercase comparison will return a non-zero result, this is the only time we want to remove.
                char firstTarget = polymer[index];
                char secondTarget = polymer[index + 1];
                if (char.ToLower(firstTarget) == char.ToLower(secondTarget) && polymer[index].CompareTo(polymer[index + 1]) != 0)
                {
                    polymer.RemoveRange(index, 2);
                    if (index > 0)
                        index--; // Move back one to check if a new pair has resulted from the removal.
                }
                else
                {
                    index++; // Nothing here, next!
                }
            }

            return polymer;
        }
    }
}