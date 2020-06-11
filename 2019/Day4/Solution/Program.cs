using System;
using System.IO;
using System.Linq;
using NAoCHelper;

namespace Day4
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("1964f53f-6f5d-4b42-b635-fadef0a0d1f3"));
            var puzzle = new Puzzle(user, 2019, 4);
            var input = puzzle.GetInputAsync().Result;

            var range = input.Split("-");
            var lowerLimit = Int32.Parse(range[0]);
            var upperLimit = Int32.Parse(range[1]);

            Part1(lowerLimit, upperLimit);
            Part2(lowerLimit, upperLimit);
        }

        public static void Part1(int lowerLimit, int upperLimit)
        {
            var validNumberCount = 0;
            for (int i = lowerLimit; i < upperLimit; i++)
            {
                if (NumberIsValid(i))
                    validNumberCount++;
            }

            Console.WriteLine($"Part 1: {validNumberCount}");
        }

        public static void Part2(int lowerLimit, int upperLimit)
        {
            var validNumberCount = 0;
            for (int i = lowerLimit; i < upperLimit; i++)
            {
                if (NumberIsValid(i, true))
                    validNumberCount++;
            }

            Console.WriteLine($"Part 2: {validNumberCount}");
        }

        public static bool NumberIsValid(int number, bool doPart2Checks = false)
        {
            var numberParts = number.ToString().ToArray();

            var firstPart = Int32.Parse(numberParts[0].ToString());
            var secondPart = Int32.Parse(numberParts[1].ToString());
            var thirdPart = Int32.Parse(numberParts[2].ToString());
            var fourthPart = Int32.Parse(numberParts[3].ToString());
            var fifthPart = Int32.Parse(numberParts[4].ToString());
            var sixthPart = Int32.Parse(numberParts[5].ToString());

            bool hasDouble = firstPart == secondPart || secondPart == thirdPart || thirdPart == fourthPart || fourthPart == fifthPart || fifthPart == sixthPart;
            bool onlyIncreases = firstPart <= secondPart && secondPart <= thirdPart && thirdPart <= fourthPart && fourthPart <= fifthPart && fifthPart <= sixthPart;

            if (hasDouble && onlyIncreases && doPart2Checks)
            {
                var repeats = numberParts.GroupBy(p => numberParts.Count(n => n == p)).Where(g => g.Key >= 2);
                if (repeats.Count() == 1 && repeats.First().Key != 2)
                {
                    // x111xx
                    hasDouble = false;
                }
            }

            return hasDouble && onlyIncreases;
        }
    }
}
