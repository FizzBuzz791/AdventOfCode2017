using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace Solutions.Day16
{
	public class Solution : BaseSolution<string>, ISolvable
	{
		public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n'))
		{
		}

		public string SolvePart1()
		{
			var output = FlawedFrequencyTransmission(Input);
			return $"Part 1: {string.Concat(output.Take(8))}";
		}

		public string SolvePart2()
		{
			string message = DecodeSignal(Input);
			return $"Part 2: {message}";
		}

		private static string DecodeSignal(string input)
		{
			string signal = string.Concat(Enumerable.Repeat(input, 10000));
			int offset = int.Parse(string.Concat(input.Take(7)));

			var list = signal.Select(c => int.Parse(c.ToString())).ToList();
			for (var phase = 0; phase < 100; phase++)
			{
				for (int index = list.Count - 1; index >= offset; index--)
				{
					list[index] = Math.Abs(index == list.Count - 1 ? list[index] : list[index] + list[index + 1]) % 10;
				}
			}

			return string.Concat(list.Skip(offset).Take(8));
		}

		private static IEnumerable<int> FlawedFrequencyTransmission(string input)
		{
			const int phases = 100;
			
			var list = input.Select(c => int.Parse(c.ToString())).ToList();
			var basePattern = new[] { 0, 1, 0, -1 };

			for (var p = 0; p < phases; p++)
			{
				var newList = new List<int>();
				var pattern = new List<int>();
				for (var i = 0; i < list.Count; i++)
				{
					pattern.Clear();

					// Generate pattern
					foreach (int x in basePattern)
					{
						pattern.AddRange(Enumerable.Repeat(x, i + 1));
					}

					pattern = Enumerable.Repeat(pattern, (int) Math.Ceiling((double) list.Count / pattern.Count) + 1)
					                    .SelectMany(x => x)
					                    .ToList();

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