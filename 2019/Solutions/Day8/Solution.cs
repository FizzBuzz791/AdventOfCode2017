using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;
using NAoCHelper;

namespace Solutions.Day8
{
	public class Solution : BaseSolution<int[]>, ISolvable
	{
		private static int Width { get; set; } = 25;
		private static int Height { get; set; } = 6;

		public Solution(IPuzzle puzzle) : base(
			puzzle, x => x.Trim('\n').ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
		{
		}

		public string SolvePart1()
		{
			var layers = CalculateLayers(Input);
			int indexOfLeastZeroes = layers.MinBy(l => l.Value.Count(d => d == 0)).First().Key;
			var layerWithLeastZeroes = layers[indexOfLeastZeroes];

			int onesCount = layerWithLeastZeroes.Count(d => d == 1);
			int twosCount = layerWithLeastZeroes.Count(d => d == 2);

			return $"Part 1: {onesCount * twosCount}";
		}

		public string SolvePart2()
		{
			var layers = CalculateLayers(Input);
			var output = new StringBuilder();
			for (var h = 0; h < Height; h++)
			{
				for (var w = 0; w < Width; w++)
				{
					var pixels = layers.Select(layer => layer.Value[w + h * Width]).ToList();
					int pixel = pixels.FirstOrDefault(p => p == 0 || p == 1);
					output.Append(pixel == 0 ? " " : "#");
				}

				output.AppendLine();
			}

			return $"Part 2:\n{output}";
		}

		// Dirty, dirty hack to allow for testing.
		public static void OverrideDimensions(int width, int height)
		{
			Width = width;
			Height = height;
		}

		private static Dictionary<int, List<int>> CalculateLayers(IEnumerable<int> input)
		{
			var imageData = new Queue<int>(input);

			var layers = new Dictionary<int, List<int>>();
			var layerCount = 0;

			while (imageData.Count > 0)
			{
				var layer = new List<int>();
				for (var i = 0; i < Width * Height; i++)
				{
					layer.Add(imageData.Dequeue());
				}

				layerCount++;
				layers.Add(layerCount, layer);
			}

			return layers;
		}
	}
}