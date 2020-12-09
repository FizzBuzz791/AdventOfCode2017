using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using NAoCHelper;

namespace Solutions.Day5
{
    public class Solution : BaseSolution<List<char>>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').ToCharArray().ToList())
        {
        }

        public string SolvePart1()
        {
            IList<char> collapsedPolymer = ReactPolymer(Input);

            return $"Part 1: {collapsedPolymer.Count}";
        }

        public string SolvePart2()
        {
            IEnumerable<char> distinctUnitTypes = Input.Select(char.ToUpper).ToList().Distinct();

            var polymerOptions = new Dictionary<char, int>(); // Key: Removed Unit Type, Value: Collapsed Length
            foreach (char unitType in distinctUnitTypes)
            {
                List<char> improvedPolymer = Input.ToList(); // "Copy" the Input
                improvedPolymer.RemoveAll(i => char.ToUpper(i) == unitType);
                IList<char> collapsedPolymer = ReactPolymer(improvedPolymer);
                polymerOptions.Add(unitType, collapsedPolymer.Count);
            }

            KeyValuePair<char, int> optimalPolymer = polymerOptions.MinBy(o => o.Value).FirstOrDefault();
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