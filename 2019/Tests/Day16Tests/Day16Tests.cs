using System.Collections.Generic;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day16;

namespace Tests.Day16Tests
{
	public class Day16Tests
	{
		[TestCaseSource(nameof(Part1Cases))]
		public void Part1FindsCorrectResult(string input, string expectedOutput)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(input);
			
			var day16 = new Solution(puzzle);

			// Act
			string result = day16.SolvePart1();

			// Assert
			result.ShouldBe($"Part 1: {expectedOutput}");
		}

		private static IEnumerable<object[]> Part1Cases()
		{
			yield return new object[] { "80871224585914546619083218645595", "24176176" };
			yield return new object[] { "19617804207202209144916044189917", "73745418" };
			yield return new object[] { "69317163492948606335995924319873", "52432133" };
		}
		
		[TestCaseSource(nameof(Part2Cases))]
		public void Part2FindsCorrectResult(string input, string expectedMessage)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(input);
			
			var day16 = new Solution(puzzle);

			// Act
			string result = day16.SolvePart2();

			// Assert
			result.ShouldBe($"Part 2: {expectedMessage}");
		}

		private static IEnumerable<object[]> Part2Cases()
		{
			yield return new object[] { "03036732577212944063491565474664", "84462026" };
			yield return new object[] { "02935109699940807407585447034323", "78725270" };
			yield return new object[] { "03081770884921959731165446850517", "53553731" };
		}
	}
}