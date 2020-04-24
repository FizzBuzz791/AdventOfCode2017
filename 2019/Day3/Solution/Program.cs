using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NAoCHelper;

namespace Day3
{
    public class Program
    {
        private static int OriginX { get; set; }
        private static int OriginY { get; set; }

        public static void Main()
        {
            var user = new User(GetCookie());
            var puzzle = new Puzzle(user, 2019, 3);
            var input = puzzle.GetInputAsync().Result;

            var crossovers = FindCrossovers(input);

            var minDistance = Part1(crossovers);
            Console.WriteLine($"Part 1: {minDistance}");

            var minSteps = Part2(crossovers);
            Console.WriteLine($"Part 2: {minSteps}");
        }

        public static string GetCookie()
        {
            var builder =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .AddUserSecrets("846e2ffd-4fb8-4da4-9405-d0f80b0ed87e");
            var config = builder.Build();

            var secretValues = config.GetSection("Secrets").GetChildren();
            return secretValues.FirstOrDefault(s => s.Key == "Cookie")?.Value
                ?? string.Empty;
        }

        public static List<Step> FindCrossovers(string input)
        {
            var wire1instructions = input.Split("\n")[0].Split(",");
            var wire2instructions = input.Split("\n")[1].Split(",");

            var wire1Dimensions = ProcessInstructions(wire1instructions);
            var wire2Dimensions = ProcessInstructions(wire2instructions);

            var xDist = Math.Abs(Math.Min(wire1Dimensions.MinX, wire2Dimensions.MinX) - Math.Max(wire1Dimensions.MaxX, wire2Dimensions.MaxX));
            var yDist = Math.Abs(Math.Min(wire1Dimensions.MinY, wire2Dimensions.MinY) - Math.Max(wire1Dimensions.MaxY, wire2Dimensions.MaxY));

            var grid = new Step[xDist + 1, yDist + 1];
            OriginX = Math.Abs(Math.Min(wire1Dimensions.MinX, wire2Dimensions.MinX));
            OriginY = Math.Abs(Math.Min(wire1Dimensions.MinY, wire2Dimensions.MinY));

            LayWires(wire1instructions, grid, true);
            LayWires(wire2instructions, grid, false);

            var crossovers = new List<Step>();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] != null && grid[x, y].Intersection)
                        crossovers.Add(grid[x, y]);
                }
            }

            return crossovers;
        }

        public static int Part1(List<Step> crossovers)
        {
            // Find the closest cross-over point to origin.
            var minDistance = Int32.MaxValue;
            foreach (var crossover in crossovers)
            {
                var distance = Math.Abs(OriginX - crossover.Location.X) + Math.Abs(OriginY - crossover.Location.Y);
                if (distance < minDistance)
                    minDistance = distance;
            }
            return minDistance;
        }

        public static int Part2(List<Step> crossovers)
        {
            var minSteps = Int32.MaxValue;
            foreach (var crossover in crossovers)
            {
                var combinedSteps = crossover.Wire1Steps + crossover.Wire2Steps;
                if (combinedSteps < minSteps)
                    minSteps = combinedSteps;
            }
            return minSteps;
        }

        public static Dimensions ProcessInstructions(string[] wireInstructions)
        {
            var currentX = 0;
            var currentY = 0;

            var minX = 0;
            var maxX = 0;
            var minY = 0;
            var maxY = 0;

            foreach (var instruction in wireInstructions)
            {
                var direction = instruction[0];
                var distance = Int32.Parse(instruction.Substring(1));

                switch (direction)
                {
                    case 'L':
                        currentX -= distance;
                        if (currentX < minX)
                            minX = currentX;
                        break;
                    case 'R':
                        currentX += distance;
                        if (currentX > maxX)
                            maxX = currentX;
                        break;
                    case 'U':
                        currentY -= distance;
                        if (currentY < minY)
                            minY = currentY;
                        break;
                    case 'D':
                        currentY += distance;
                        if (currentY > maxY)
                            maxY = currentY;
                        break;
                    default:
                        Console.WriteLine($"Encountered unknown instruction: {instruction}");
                        break;
                }
            }

            return new Dimensions(minX, minY, maxX, maxY);
        }

        public static void LayWires(string[] wireInstructions, Step[,] grid, bool isWire1)
        {
            var currentX = OriginX;
            var currentY = OriginY;
            var lastValue = 0;

            // Don't initialise the origin, it doesn't count as a cross-over point.
            // grid[currentX, currentY]++;

            foreach (var instruction in wireInstructions)
            {
                var direction = instruction[0];
                var distance = Int32.Parse(instruction.Substring(1));

                var xIncrement = 0;
                var yIncrement = 0;

                switch (direction)
                {
                    case 'L':
                        xIncrement = -1;
                        break;
                    case 'R':
                        xIncrement = 1;
                        break;
                    case 'U':
                        yIncrement = -1;
                        break;
                    case 'D':
                        yIncrement = 1;
                        break;
                }

                while (distance > 0)
                {
                    currentX += xIncrement;
                    currentY += yIncrement;

                    if (grid[currentX, currentY] == null)
                        grid[currentX, currentY] = new Step(new Point(currentX, currentY));

                    if (isWire1)
                    {
                        grid[currentX, currentY].Wire1Touched = true;
                        grid[currentX, currentY].Wire1Steps = lastValue + 1;
                    }
                    else
                    {
                        grid[currentX, currentY].Wire2Touched = true;
                        grid[currentX, currentY].Wire2Steps = lastValue + 1;
                    }

                    lastValue += 1;
                    distance--;
                }
            }
        }

        public class Dimensions
        {
            public int MinX { get; }
            public int MinY { get; }
            public int MaxX { get; }
            public int MaxY { get; }

            public Dimensions(int minX, int minY, int maxX, int maxY)
            {
                MinX = minX;
                MinY = minY;
                MaxX = maxX;
                MaxY = maxY;
            }
        }

        public class Step
        {
            public bool Wire1Touched { get; set; }
            public bool Wire2Touched { get; set; }
            public bool Intersection { get { return Wire1Touched && Wire2Touched; } }

            public int Wire1Steps { get; set; }
            public int Wire2Steps { get; set; }
            public Point Location { get; }

            public Step(Point location)
            {
                Location = location;
            }
        }
    }
}
