using System;
using NAoCHelper;

namespace Solutions
{
    public static class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("1d35577e-659d-4010-861a-8d561da451fe"));
            var day1Puzzle = new Puzzle(user, 2019, 1);
            
            var day1 = new Day1.Solution(day1Puzzle);
            Console.WriteLine(day1.SolvePart1());
            Console.WriteLine(day1.SolvePart2());
        }
    }
}
