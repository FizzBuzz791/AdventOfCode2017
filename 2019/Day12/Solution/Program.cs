using System;
using System.Collections.Generic;
using System.Linq;
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

            Part1(input, 1000);
        }

        public static List<Moon> Part1(string input, int steps)
        {
            var moons = input.Split("\n").Select(m => new Moon(m)).ToList();
            var combinations = new Combinations<Moon>(moons, 2);

            for (int i = 0; i < steps; i++)
            {
                foreach (var permutation in combinations)
                    ApplyGravity(permutation.First(), permutation.Last());

                foreach (var moon in moons)
                    moon.ApplyVelocity();
            }

            var totalEnergy = moons.Sum(m => m.PotentialEnergy * m.KineticEnergy);
            Console.WriteLine($"Sum of total energy after {steps} steps = {totalEnergy}");

            return moons;
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
    }
}
