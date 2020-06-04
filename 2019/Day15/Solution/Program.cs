using System;
using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Day15
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("f66a053b-e715-4168-b677-b51ee0115730"));
            var puzzle = new Puzzle(user, 2019, 15);
            var input = puzzle.GetInputAsync().Result;
            var memory = input.Split(',').Select(BigInteger.Parse).ToArray();

            Part1(memory);
        }

        public static void Part1(BigInteger[] memory)
        {
            var rdcs = new RepairDroidControlSystem(memory);
            rdcs.Explore();

            rdcs.DisplayGrid();

            var shortestPath = rdcs.FindPath(rdcs.Origin, rdcs.Destination);
            Console.WriteLine($"Shortest Path: {shortestPath}");
        }
    }
}
