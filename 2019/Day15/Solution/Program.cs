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

            var rdcs = new RepairDroidControlSystem(memory);
            rdcs.Explore();
            rdcs.DisplayGrid();

            Part1(rdcs);
            Part2(rdcs);
        }

        public static void Part1(RepairDroidControlSystem rdcs)
        {
            var shortestPath = rdcs.FindPath(rdcs.Origin).SingleOrDefault(n => n.Location == rdcs.Destination).Distance;
            Console.WriteLine($"Shortest Path: {shortestPath}");
        }

        public static void Part2(RepairDroidControlSystem rdcs)
        {
            var longestPath = rdcs.FindPath(rdcs.Destination).Max(x => x.Distance);
            Console.WriteLine($"Minutes: {longestPath}");
        }
    }
}
