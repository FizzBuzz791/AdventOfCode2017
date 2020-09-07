using System;
using NAoCHelper;

namespace Solutions
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("1d35577e-659d-4010-861a-8d561da451fe"));

            int day = int.Parse(args[0]);
            var puzzle = new Puzzle(user, 2019, day);
            ISolvable? solution = day switch
            {
                1 => new Day1.Solution(puzzle),
                2 => new Day2.Solution(puzzle),
                3 => new Day3.Solution(puzzle),
                4 => new Day4.Solution(puzzle),
                5 => new Day5.Solution(puzzle),
                _ => null
            };

            if (solution != null)
            {
                Console.WriteLine(solution.SolvePart1());
                Console.WriteLine(solution.SolvePart2());
            }
            else
            {
                Console.WriteLine($"No solution available for Day {day}.");
            }
        }
    }
}
