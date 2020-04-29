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
            var user = new User(GetCookie());
            var puzzle = new Puzzle(user, 2019, 5);
            var input = puzzle.GetInputAsync().Result;

            var memory = input.Split(',').Select(Int32.Parse).ToArray();

            Part1(memory);
        }

        public static string GetCookie()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", false, true)
                            .AddUserSecrets("8730e162-85ce-40fe-bbfb-31292325b310");
            var config = builder.Build();

            var secretValues = config.GetSection("Secrets").GetChildren();
            return secretValues.FirstOrDefault(s => s.Key == "Cookie")?.Value ?? string.Empty;
        }

        public static void Part1(int[] memory)
        {
            var computer = new IntCodeMachine(memory);
            computer.Execute();

            Console.WriteLine("Part 1 Complete");
        }
    }
}
