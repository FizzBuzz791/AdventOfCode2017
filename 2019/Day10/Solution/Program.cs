using System;
using NAoCHelper;

namespace Day10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("c617493e-f753-4626-bb52-9661e64c9878"));
            var puzzle = new Puzzle(user, 2019, 10);
            var input = puzzle.GetInputAsync().Result;

            Console.WriteLine(input);
        }
    }
}
