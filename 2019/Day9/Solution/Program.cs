using System;
using NAoCHelper;

namespace Day9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("fe89d886-33f1-4478-8a4e-bc48a031be8c"));
            var puzzle = new Puzzle(user, 2019, 9);
            var input = puzzle.GetInputAsync().Result;

            Console.WriteLine(input);
        }
    }
}
