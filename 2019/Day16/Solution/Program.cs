using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace Day16
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("5755d29d-585d-46d4-ba5e-5eed30d1bcca"));
            var puzzle = new Puzzle(user, 2019, 16);
            var input = puzzle.GetInputAsync().Result;

            var output = Part1(input, 100);
            Console.WriteLine($"{string.Join(string.Empty, output.Take(8))}");
        }

        public static List<int> Part1(string input, int phases)
        {
            var list = input.Trim('\n').Select(c => int.Parse(c.ToString())).ToList();
            var basePattern = new int[] { 0, 1, 0, -1 };

            for (int p = 0; p < phases; p++)
            {
                var newList = new List<int>();
                for (int i = 0; i < list.Count; i++)
                {
                    var pattern = new List<int>();
                    var index = 0;

                    // Generate pattern
                    while (pattern.Count <= list.Count)
                    {
                        for (int j = 0; j < i + 1; j++)
                        {
                            pattern.Add(basePattern[index]);
                        }

                        // Increment or reset index
                        index = index + 1 == basePattern.Length ? 0 : index + 1;
                    }

                    // "left shift"
                    pattern.RemoveAt(0);

                    var newValue = 0;
                    for (int e = 0; e < list.Count; e++)
                    {
                        newValue += list[e] * pattern[e];
                    }

                    // Keep the ones digit
                    newList.Add(Math.Abs(newValue % 10));
                }

                list = newList;
            }

            return list;
        }
    }
}
