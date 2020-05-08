using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
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

            Part2(memory);
        }

        public static int Part1(BigInteger[] memory)
        {
            var robot = new Robot(memory);
            robot.Run();
            return robot.PaintedPanels.Count;
        }

        public static void Part2(BigInteger[] memory)
        {
            var robot = new Robot(memory);
            robot.Run(Color.White);

            int minX = robot.PaintedPanels.Keys.Min(p => p.X);
            int minY = robot.PaintedPanels.Keys.Min(p => p.Y);
            int maxX = robot.PaintedPanels.Keys.Max(p => p.X);
            int maxY = robot.PaintedPanels.Keys.Max(p => p.Y);

            var output = new StringBuilder();
            var currentPoint = Point.Empty;
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    currentPoint = new Point(x, y);
                    if (robot.PaintedPanels.ContainsKey(currentPoint))
                        output.Append(robot.PaintedPanels[currentPoint] == Color.White ? '#' : ' ');
                    else
                        output.Append(' '); // Black
                }
                Console.WriteLine(output.ToString());
                output.Clear();
            }
        }
    }
}
