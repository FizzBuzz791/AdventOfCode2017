using System;
using NAoCHelper;

namespace Solutions.Day1
{
    public class Solution
    {
        private readonly IPuzzle _puzzle;
        
        public Solution(IPuzzle puzzle)
        {
            _puzzle = puzzle;
        }
        
        public string SolvePart1()
        {
            string input = _puzzle.GetInputAsync().Result;
            var modules = input.Split("\n");
            var requiredFuel = 0L;
            foreach (string module in modules)
            {
                if (int.TryParse(module, out int moduleWeight))
                    requiredFuel += CalculateFuel(moduleWeight);
            }

            return $"Part 1: {requiredFuel}";
        }

        public string SolvePart2()
        {
            string input = _puzzle.GetInputAsync().Result;
            var modules = input.Split("\n");
            var requiredFuel = 0L;
            foreach (string module in modules)
            {
                if (int.TryParse(module, out int moduleWeight))
                {
                    requiredFuel += CalculateFuel(moduleWeight, true);
                }
            }

            return $"Part 2: {requiredFuel}";
        }

        private static int CalculateFuel(int module, bool part2 = false)
        {
            var totalFuel = 0;
            int requiredFuel = (int)Math.Floor(module / 3.0) - 2;

            do
            {
                totalFuel += requiredFuel;
                requiredFuel = (int)Math.Floor(requiredFuel / 3.0) - 2;
            } while (part2 && requiredFuel >= 0);

            return totalFuel;
        }
    }
}