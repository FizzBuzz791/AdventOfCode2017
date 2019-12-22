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
            var user = new User(GetCookie());
            var puzzle = new Puzzle(user, 2019, 2);
            var input = puzzle.GetInputAsync().Result;

            Part1(input);
        }

        public static string GetCookie()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", false, true)
                            .AddUserSecrets("9f311380-1bb8-4ef6-8431-101bfeed90df");
            var config = builder.Build();

            var secretValues = config.GetSection("Secrets").GetChildren();
            return secretValues.FirstOrDefault(s => s.Key == "Cookie")?.Value ?? string.Empty;
        }

        public static void Part1(string puzzleInput)
        {
            var opcodes = puzzleInput.Split(',').Select(Int32.Parse).ToArray();

            opcodes[1] = 12;
            opcodes[2] = 2;

            var result = ProcessOpCodes(opcodes);
            Console.WriteLine($"Part 1: {result[0]}");
        }

        public static int[] ProcessOpCodes(int[] opcodes)
        {
            int instructionPosition = 0;
            int operation = opcodes[instructionPosition];
            while (operation != 99)
            {
                switch (operation)
                {
                    case 1:
                        opcodes = Add(opcodes, instructionPosition);
                        break;
                    case 2:
                        opcodes = Multiply(opcodes, instructionPosition);
                        break;
                }

                instructionPosition += 4;
                operation = opcodes[instructionPosition];
            }
            return opcodes;
        }

        public static int[] Add(int[] opcodes, int instructionPosition)
        {
            int firstTermIndex = opcodes[instructionPosition + 1];
            int secondTermIndex = opcodes[instructionPosition + 2];
            int resultIndex = opcodes[instructionPosition + 3];

            int firstTerm = opcodes[firstTermIndex];
            int secondTerm = opcodes[secondTermIndex];

            int result = firstTerm + secondTerm;
            opcodes[resultIndex] = result;

            return opcodes;
        }

        public static int[] Multiply(int[] opcodes, int instructionPosition)
        {
            int firstTermIndex = opcodes[instructionPosition + 1];
            int secondTermIndex = opcodes[instructionPosition + 2];
            int resultIndex = opcodes[instructionPosition + 3];

            int firstTerm = opcodes[firstTermIndex];
            int secondTerm = opcodes[secondTermIndex];

            int result = firstTerm * secondTerm;
            opcodes[resultIndex] = result;

            return opcodes;
        }
    }
}