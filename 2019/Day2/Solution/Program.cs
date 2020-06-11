using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NAoCHelper;

namespace Day2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("9f311380-1bb8-4ef6-8431-101bfeed90df"));
            var puzzle = new Puzzle(user, 2019, 2);
            var input = puzzle.GetInputAsync().Result;
            var opcodes = input.Split(',').Select(Int32.Parse).ToArray();

            Part1(opcodes);
            Part2(opcodes);
        }

        public static void Part1(int[] opcodes)
        {
            opcodes[1] = 12;
            opcodes[2] = 2;

            var computer = new IntCodeMachine(opcodes);

            computer.Execute();
            Console.WriteLine($"Part 1: {computer.Memory[0]}");
        }

        public static void Part2(int[] opcodes)
        {
            bool foundAnswer = false;
            for (int noun = 0; noun < opcodes.Length && !foundAnswer; noun++)
            {
                for (int verb = 0; verb < opcodes.Length && !foundAnswer; verb++)
                {
                    opcodes[1] = noun;
                    opcodes[2] = verb;

                    var computer = new IntCodeMachine(opcodes);
                    computer.Execute();

                    if (computer.Memory[0] == 19690720)
                    {
                        Console.WriteLine($"Part 2: 100 * {noun} + {verb} = {(100 * noun + verb)}");
                        foundAnswer = true;
                    }
                }
            }
        }
    }
}