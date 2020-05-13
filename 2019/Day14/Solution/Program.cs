using System;
using NAoCHelper;

namespace Day14
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("f66a053b-e715-4168-b677-b51ee0115730"));
            var puzzle = new Puzzle(user, 2019, 14);
            var input = puzzle.GetInputAsync().Result;

            Console.WriteLine(input);
        }
    }
}
