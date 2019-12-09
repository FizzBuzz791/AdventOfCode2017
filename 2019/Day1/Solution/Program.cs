using System;
using System.IO;
using System.Linq;
using AoCHelper;
using Microsoft.Extensions.Configuration;

namespace Day1
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(GetCookie());
            var puzzle = new Puzzle(user, 2019, 1);
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
            var modules = puzzleInput.Split("\n");
            var requiredFuel = 0L;
            foreach (var module in modules)
            {
                if (Int32.TryParse(module, out int moduleWeight))
                {
                    requiredFuel += CalculateFuelForModule(moduleWeight);
                }
            }

            Console.WriteLine($"Part 1: {requiredFuel}");
        }

        public static int CalculateFuelForModule(int module)
        {
            return (int)Math.Floor(module / 3.0) - 2;
        }
    }
}
