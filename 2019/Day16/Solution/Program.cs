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
            var input = puzzle.GetInputAsync().Result.Trim('\n');

            Part1(input);
            Part2(input);
        }

        public static void Part1(string input)
        {
            var output = FlawedFrequencyTransmission(input, 100);
            Console.WriteLine($"Part 1: {string.Concat(output.Take(8))}");
        }

        public static void Part2(string input)
        {
            var message = DecodeSignal(input);
            Console.WriteLine($"Part 2: {message}");
        }

        public static string DecodeSignal(string input)
        {
            var signal = string.Concat(Enumerable.Repeat(input, 10000));
            var offset = int.Parse(string.Concat(input.Take(7)));

            var list = signal.Select(c => int.Parse(c.ToString())).ToList();
            for (int phase = 0; phase < 100; phase++)
            {
                for (int index = list.Count - 1; index >= offset; index--)
                {
                    list[index] = Math.Abs(index == list.Count - 1 ? list[index] : list[index] + list[index + 1]) % 10;
                }
            }

            return string.Concat(list.Skip(offset).Take(8));
        }

        public static List<int> FlawedFrequencyTransmission(string input, int phases)
        {
            var list = input.Select(c => int.Parse(c.ToString())).ToList();
            var basePattern = new int[] { 0, 1, 0, -1 };

            for (int p = 0; p < phases; p++)
            {
                var newList = new List<int>();
                var pattern = new List<int>();
                for (int i = 0; i < list.Count; i++)
                {
                    pattern.Clear();

                    // Generate pattern
                    foreach (var x in basePattern)
                    {
                        pattern.AddRange(Enumerable.Repeat(x, i + 1));
                    }

                    pattern = Enumerable.Repeat(pattern, (int)Math.Ceiling((double)list.Count / pattern.Count) + 1).SelectMany(x => x).ToList();

                    // "left shift"
                    pattern.RemoveAt(0);

                    newList.Add(Math.Abs(list.Select((element, index) => element * pattern[index]).Sum() % 10));
                }

                list = newList;
            }

            return list;
        }
    }
}
