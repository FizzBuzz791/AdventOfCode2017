using System;
using NAoCHelper;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("98fda5c2-62f1-4660-abe5-48b5d862e52f"));
            var puzzle = new Puzzle(user, 2019, 7);
            var input = puzzle.GetInputAsync().Result;

            Console.WriteLine("Hello World - Day 7");
        }
    }
}
