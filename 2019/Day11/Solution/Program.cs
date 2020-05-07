using System;
using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Day11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("a7105ed9-5988-4f41-b6e9-39d7a66ca1a3"));
            var puzzle = new Puzzle(user, 2019, 11);
            var input = puzzle.GetInputAsync().Result;
            var memory = input.Split(',').Select(BigInteger.Parse).ToArray();

            var paintedPanelCount = Part1(memory);

            Console.WriteLine($"The Robot painted {paintedPanelCount} panels at least once.");
        }

        public static int Part1(BigInteger[] memory)
        {
            var robot = new Robot(memory);
            robot.Run();
            return robot.PaintedPanels.Count;
        }
    }
}
