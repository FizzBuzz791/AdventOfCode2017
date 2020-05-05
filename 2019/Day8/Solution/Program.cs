using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var layers = CalculateLayers(input, 25, 6);

            var checksum = Part1(layers);
            Console.WriteLine($"Checksum is {checksum}.");

            Console.WriteLine(Part2(layers, 25, 6));
        }

        public static Dictionary<int, List<int>> CalculateLayers(string input, int width, int height)
        {
            var imageData = new Queue<int>(input.ToCharArray().Select(c => Int32.Parse(c.ToString())).ToArray());

            var layers = new Dictionary<int, List<int>>();
            var layerCount = 0;

            while (imageData.Count > 0)
            {
                var layer = new List<int>();
                for (int i = 0; i < width * height; i++)
                {
                    layer.Add(imageData.Dequeue());
                }

                layerCount++;
                layers.Add(layerCount, layer);
            }

            return layers;
        }

        public static int Part1(Dictionary<int, List<int>> layers)
        {
            var indexOfLeastZeroes = layers.Min(l => l.Value.Count(d => d == 0));
            var layerWithLeastZeroes = layers[indexOfLeastZeroes];

            var onesCount = layerWithLeastZeroes.Count(d => d == 1);
            var twosCount = layerWithLeastZeroes.Count(d => d == 2);

            return onesCount * twosCount;
        }

        public static string Part2(Dictionary<int, List<int>> layers, int width, int height)
        {
            var output = new StringBuilder();
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    var pixels = new List<int>();
                    foreach (var layer in layers)
                    {
                        pixels.Add(layer.Value[w + (h * width)]);
                    }

                    var pixel = pixels.FirstOrDefault(p => p == 0 || p == 1);
                    output.Append(pixel == 0 ? " " : "#");
                }
                output.AppendLine();
            }
            return output.ToString();
        }
    }
}
