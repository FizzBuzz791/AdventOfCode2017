using System;
using NAoCHelper;

namespace Day12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("c1614296-0e85-46b7-aed7-a1c3d3891e37"));
            var puzzle = new Puzzle(user, 2019, 12);
            var input = puzzle.GetInputAsync().Result;

            Console.WriteLine(input);
        }
    }
}
