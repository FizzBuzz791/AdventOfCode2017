using System;
using NAoCHelper;

namespace Day17
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("5755d29d-585d-46d4-ba5e-5eed30d1bcca"));
            var puzzle = new Puzzle(user, 2019, 17);
            var input = puzzle.GetInputAsync().Result.Trim('\n');

            Console.WriteLine(input);
        }
    }
}
