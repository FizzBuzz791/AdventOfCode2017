using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NAoCHelper;

namespace Day4
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(GetCookie());
            var puzzle = new Puzzle(user, 2019, 4);
            var input = puzzle.GetInputAsync().Result;

            Part1(input);
        }

        public static void Part1(string input)
        {
            var range = input.Split("-");
            var lowerLimit = Int32.Parse(range[0]);
            var upperLimit = Int32.Parse(range[1]);

            var validNumberCount = 0;
            for (int i = lowerLimit; i < upperLimit; i++)
            {
                if (NumberIsValid(i))
                    validNumberCount++;
            }

            Console.WriteLine($"Part 1: {validNumberCount}");
        }

        public static bool NumberIsValid(int number)
        {
            var hasDouble = false;
            var onlyIncreases = true;
            var numberParts = number.ToString().ToArray();
            for (int i = 0; i < numberParts.Length - 1; i++)
            {
                var firstPart = Convert.ToInt32(numberParts[i]);
                var secondPart = Convert.ToInt32(numberParts[i + 1]);
                if (firstPart == secondPart)
                    hasDouble = true;

                if (secondPart < firstPart)
                    onlyIncreases = false;
            }

            return hasDouble && onlyIncreases;
        }

        public static string GetCookie()
        {
            var builder =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .AddUserSecrets("1964f53f-6f5d-4b42-b635-fadef0a0d1f3");
            var config = builder.Build();

            var secretValues = config.GetSection("Secrets").GetChildren();
            return secretValues.FirstOrDefault(s => s.Key == "Cookie")?.Value
                ?? string.Empty;
        }
    }
}
