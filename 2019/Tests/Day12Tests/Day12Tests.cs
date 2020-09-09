using System.Collections.Generic;
using System.Numerics;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day12;

namespace Tests.Day12Tests
{
	public class Day12Tests
	{
		[TestCaseSource(nameof(Part1Cases))]
		public void Part1CalculatesResultCorrectly(string input, int steps, int totalEnergy)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(input);

			var day12 = new Solution(puzzle);
			day12.OverrideSteps(steps);

			// Act
			string result = day12.SolvePart1();

			// Assert
			result.ShouldBe($"Part 1: {totalEnergy}");
		}

		private static IEnumerable<object[]> Part1Cases()
		{
			yield return new object[]
			{
				"<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>", 10, 179
			};
			yield return new object[]
			{
				"<x=-8, y=-10, z=0>\n<x=5, y=5, z=10>\n<x=2, y=-7, z=3>\n<x=9, y=-8, z=-3>", 100, 1940
			};
		}
		
		[TestCaseSource(nameof(Part2Cases))]
		public void Part2CalculatesResultCorrectly(string input, BigInteger expectedSteps)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(input);

			var day12 = new Solution(puzzle);

			// Act
			string result = day12.SolvePart2();

			// Assert
			result.ShouldBe($"Part 2: {expectedSteps}");
		}

		private static IEnumerable<object[]> Part2Cases()
		{
			yield return new object[]
			{
				"<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>", new BigInteger(2772), 
			};
			yield return new object[]
			{
				"<x=-8, y=-10, z=0>\n<x=5, y=5, z=10>\n<x=2, y=-7, z=3>\n<x=9, y=-8, z=-3>", new BigInteger(4686774924), 
			};
		}
	}
}