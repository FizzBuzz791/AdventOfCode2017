using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace Day8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("f4e71f6f-ce28-49d3-8515-f4e51a2ddfd7"));
            var puzzle = new Puzzle(user, 2019, 8);
            var input = puzzle.GetInputAsync().Result;

            // Strip the newline
            input = input.Substring(0, input.Length - 1);

            var checksum = Part1(input, 25, 6);
            Console.WriteLine($"Checksum is {checksum}.");

        }

        public static int Part1(string input, int width, int height)
        {
            var imageData = new Queue<int>(input.ToCharArray().Select(c => Int32.Parse(c.ToString())).ToArray());

            var layers = new Dictionary<int, List<int>>();
            var layerCount = 0;
            var indexOfLeastZeroes = -1;
            var leastZeroes = Int32.MaxValue;
            while (imageData.Count > 0)
            {
                var layer = new List<int>();
                var zeroCount = 0;
                for (int i = 0; i < width * height; i++)
                {
                    var digit = imageData.Dequeue();
                    if (digit == 0)
                        zeroCount++;

                    layer.Add(digit);
                }

                layerCount++;
                layers.Add(layerCount, layer);

                if (zeroCount < leastZeroes)
                {
                    leastZeroes = zeroCount;
                    indexOfLeastZeroes = layerCount;
                }
            }

            var onesCount = layers[indexOfLeastZeroes].Count(d => d == 1);
            var twosCount = layers[indexOfLeastZeroes].Count(d => d == 2);

            return onesCount * twosCount;
        }
    }
}
