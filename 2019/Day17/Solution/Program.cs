using System;

namespace Day17
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("1f58f4be-0537-4d02-b8cf-0df258e209bf"));
            var puzzle = new Puzzle(user, 2019, 17);
            var input = puzzle.GetInputAsync().Result.Trim('\n');

            Part1(input);
            Part2(input);
        }

        public static void Part1(string input)
        {
        }

        public static void Part2(string input)
        {
        }
    }
}
