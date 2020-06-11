using System;
using System.IO;
using System.Linq;
using NAoCHelper;

namespace Day1
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("9f311380-1bb8-4ef6-8431-101bfeed90df"));
            var puzzle = new Puzzle(user, 2019, 1);
            var input = puzzle.GetInputAsync().Result;

            Part1(input);
            Part2(input);
        }

        public static void Part1(string puzzleInput)
        {
            var modules = puzzleInput.Split("\n");
            var requiredFuel = 0L;
            foreach (var module in modules)
            {
                if (Int32.TryParse(module, out int moduleWeight))
                {
                    requiredFuel += CalculateFuel(moduleWeight);
                }
            }

            Console.WriteLine($"Part 1: {requiredFuel}");
        }

        public static void Part2(string puzzleInput)
        {
            var modules = puzzleInput.Split("\n");
            var requiredFuel = 0L;
            foreach (var module in modules)
            {
                if (Int32.TryParse(module, out int moduleWeight))
                {
                    requiredFuel += CalculateFuel(moduleWeight, true);
                }
            }

            Console.WriteLine($"Part 2: {requiredFuel}");
        }

        public static int CalculateFuel(int module, bool part2 = false)
        {
            var totalFuel = 0;

            var requiredFuel = (int)Math.Floor(module / 3.0) - 2;

            do
            {
                totalFuel += requiredFuel;
                requiredFuel = (int)Math.Floor(requiredFuel / 3.0) - 2;
            } while (part2 && requiredFuel >= 0);

            return totalFuel;
        }
    }
}
