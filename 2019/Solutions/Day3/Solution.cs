using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NAoCHelper;

namespace Solutions.Day3
{
    public class Solution : BaseSolution<string>, ISolvable
    {
        private static int OriginX { get; set; }
        private static int OriginY { get; set; }

        public Solution(IPuzzle puzzle) : base(puzzle, x => x)
        {
        }

        public string SolvePart1()
        {
            var crossovers = FindCrossovers(Input);

            // Find the closest cross-over point to origin.
            int minDistance = crossovers
                .Select(crossover =>
                    Math.Abs(OriginX - crossover.Location.X) + Math.Abs(OriginY - crossover.Location.Y))
                .Prepend(int.MaxValue).Min();

            return $"Part 1: {minDistance}";
        }

        public string SolvePart2()
        {
            var crossovers = FindCrossovers(Input);

            int minSteps = crossovers.Select(crossover => crossover.Wire1Steps + crossover.Wire2Steps)
                .Prepend(int.MaxValue).Min();

            return $"Part 2: {minSteps}";
        }

        private static IEnumerable<Step> FindCrossovers(string input)
        {
            var wire1Instructions = input.Split("\n")[0].Split(",");
            var wire2Instructions = input.Split("\n")[1].Split(",");

            var wire1Dimensions = ProcessInstructions(wire1Instructions);
            var wire2Dimensions = ProcessInstructions(wire2Instructions);

            int xDist = Math.Abs(Math.Min(wire1Dimensions.MinX, wire2Dimensions.MinX) -
                                 Math.Max(wire1Dimensions.MaxX, wire2Dimensions.MaxX));

            int yDist = Math.Abs(Math.Min(wire1Dimensions.MinY, wire2Dimensions.MinY) -
                                 Math.Max(wire1Dimensions.MaxY, wire2Dimensions.MaxY));


            var grid = new Step?[xDist + 1, yDist + 1];
            OriginX = Math.Abs(Math.Min(wire1Dimensions.MinX, wire2Dimensions.MinX));
            OriginY = Math.Abs(Math.Min(wire1Dimensions.MinY, wire2Dimensions.MinY));

            LayWires(wire1Instructions, grid, true);
            LayWires(wire2Instructions, grid, false);

            var crossovers = new List<Step>();
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] != null && grid[x, y]!.Intersection)
                        crossovers.Add(grid[x, y]!);
                }
            }

            return crossovers;
        }

        private static Dimensions ProcessInstructions(IEnumerable<string> wireInstructions)
        {
            var currentX = 0;
            var currentY = 0;

            var minX = 0;
            var maxX = 0;
            var minY = 0;
            var maxY = 0;

            foreach (var instruction in wireInstructions)
            {
                char direction = instruction[0];
                int distance = int.Parse(instruction.Substring(1));

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

        private static void LayWires(IEnumerable<string> wireInstructions, Step?[,] grid, bool isWire1)
        {
            int currentX = OriginX;
            int currentY = OriginY;
            var lastValue = 0;

            // Don't initialise the origin, it doesn't count as a cross-over point.
            foreach (var instruction in wireInstructions)
            {
                char direction = instruction[0];
                int distance = int.Parse(instruction.Substring(1));

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

                    grid[currentX, currentY] ??= new Step(new Point(currentX, currentY));

                    if (isWire1)
                    {
                        grid[currentX, currentY]!.Wire1Touched = true;
                        grid[currentX, currentY]!.Wire1Steps = lastValue + 1;
                    }
                    else
                    {
                        grid[currentX, currentY]!.Wire2Touched = true;
                        grid[currentX, currentY]!.Wire2Steps = lastValue + 1;
                    }

                    lastValue += 1;
                    distance--;
                }
            }
        }
    }
}