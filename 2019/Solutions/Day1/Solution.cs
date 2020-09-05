using System;
using NAoCHelper;

namespace Solutions.Day1
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split("\n"))
        {
        }
        
        public string SolvePart1()
        {
            var modules = Input;
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
            var modules = Input;
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