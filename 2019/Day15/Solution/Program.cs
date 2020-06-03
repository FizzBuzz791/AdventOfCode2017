using System;
using NAoCHelper;

namespace Day15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("f66a053b-e715-4168-b677-b51ee0115730"));
            var puzzle = new Puzzle(user, 2019, 15);
            var input = puzzle.GetInputAsync().Result;

            // Strip the newline
            input = input[0..^1];

            Console.WriteLine(input);
        }
    }
}
