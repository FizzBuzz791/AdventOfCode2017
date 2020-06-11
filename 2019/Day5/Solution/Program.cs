using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NAoCHelper;

namespace Day5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("8730e162-85ce-40fe-bbfb-31292325b310"));
            var puzzle = new Puzzle(user, 2019, 5);
            var input = puzzle.GetInputAsync().Result;

            var memory = input.Split(',').Select(Int32.Parse).ToArray();

            Part1(memory);
            Part2(memory);
        }

        public static void Part1(int[] memory)
        {
            var computer = new IntCodeMachine(memory, 1);
            computer.Execute();

            Console.WriteLine("Part 1 Complete");
        }

        public static void Part2(int[] memory)
        {
            var computer = new IntCodeMachine(memory, 5);
            computer.Execute();

            Console.WriteLine("Part 2 Complete");
        }
    }
}
