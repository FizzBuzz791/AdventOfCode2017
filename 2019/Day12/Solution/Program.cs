using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Combinatorics.Collections;
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

            // Strip the newline
            input = input.Substring(0, input.Length - 1);

            var sw = new Stopwatch();
            sw.Start();
            Part1(input, 1000);
            sw.Stop();
            Console.WriteLine($"1000 steps took {sw.ElapsedMilliseconds}ms");

            sw.Restart();
            Part2(input);
            sw.Stop();
            Console.WriteLine($"Calculating period took {sw.ElapsedMilliseconds}ms");
        }

        public static List<Moon> Part1(string input, int steps)
        {
            var moons = input.Split("\n").Select(m => new Moon(m)).ToList();
            var combinations = new Combinations<Moon>(moons, 2);

            for (int i = 0; i < steps; i++)
            {
                foreach (var combination in combinations)
                    ApplyGravity(combination.First(), combination.Last());

                foreach (var moon in moons)
                    moon.ApplyVelocity();
            }

            var totalEnergy = moons.Sum(m => m.PotentialEnergy * m.KineticEnergy);
            Console.WriteLine($"Sum of total energy after {steps} steps = {totalEnergy}");

            return moons;
        }

        public static void Part2(string input)
        {
            var moons = input.Split("\n").Select(m => new Moon(m)).ToArray();
            var original = moons.Select(m => new Moon(m)).ToArray();

            var xAxis = moons.Select(m => new int[] { m.Position.X, m.Velocity.X }).ToArray();
            var xAxisSteps = FindStepsToHalfway(xAxis);
            Console.WriteLine($"X axis took {xAxisSteps} steps to return to origin.");

            var yAxis = moons.Select(m => new int[] { m.Position.Y, m.Velocity.Y }).ToArray();
            var yAxisSteps = FindStepsToHalfway(yAxis);
            Console.WriteLine($"Y axis took {yAxisSteps} steps to return to origin.");

            var zAxis = moons.Select(m => new int[] { m.Position.Z, m.Velocity.Z }).ToArray();
            var zAxisSteps = FindStepsToHalfway(zAxis);
            Console.WriteLine($"Z axis took {zAxisSteps} steps to return to origin.");

            var lcm = LCM(new int[] { xAxisSteps * 2, yAxisSteps * 2, zAxisSteps * 2 });
            Console.WriteLine($"LCM of [{xAxisSteps * 2},{yAxisSteps * 2},{zAxisSteps * 2}] is {lcm}");
        }

        public static int FindStepsToHalfway(int[][] axis)
        {
            var original = axis.Select(x => new int[] { x[0], x[1] }).ToArray();
            var combinations = new Combinations<int[]>(axis, 2);
            var steps = 0;

            do
            {
                foreach (var combination in combinations)
                    ApplyGravity(combination.First(), combination.Last());

                foreach (var x in axis)
                    ApplyVelocity(x);

                steps++;
            } while (axis[0][1] != original[0][1] || axis[1][1] != original[1][1] || axis[2][1] != original[2][1] || axis[3][1] != original[3][1]);

            return steps;
        }

        public static BigInteger LCM(int[] array)
        {
            var lcm = BigInteger.One;
            int divisor = 2;

            while (true)
            {
                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == 0)
                        return 0;
                    else if (array[i] < 0)
                        array[i] = array[i] * -1;

                    if (array[i] == 1)
                        counter++;

                    if (array[i] % divisor == 0)
                    {
                        divisible = true;
                        array[i] = array[i] / divisor;
                    }
                }

                if (divisible)
                    lcm = lcm * divisor;
                else
                    divisor++;

                if (counter == array.Length)
                    return lcm;
            }
        }

        public static void ApplyGravity(Moon first, Moon second)
        {
            if (first.Position.X < second.Position.X)
            {
                first.Velocity.X++;
                second.Velocity.X--;
            }
            else if (first.Position.X > second.Position.X)
            {
                first.Velocity.X--;
                second.Velocity.X++;
            }

            if (first.Position.Y < second.Position.Y)
            {
                first.Velocity.Y++;
                second.Velocity.Y--;
            }
            else if (first.Position.Y > second.Position.Y)
            {
                first.Velocity.Y--;
                second.Velocity.Y++;
            }

            if (first.Position.Z < second.Position.Z)
            {
                first.Velocity.Z++;
                second.Velocity.Z--;
            }
            else if (first.Position.Z > second.Position.Z)
            {
                first.Velocity.Z--;
                second.Velocity.Z++;
            }
        }

        public static void ApplyGravity(int[] first, int[] second)
        {
            if (first[0] < second[0])
            {
                first[1]++;
                second[1]--;
            }
            else if (first[0] > second[0])
            {
                first[1]--;
                second[1]++;
            }
        }

        public static void ApplyVelocity(int[] axis)
        {
            axis[0] += axis[1];
        }
    }
}
