using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day8;

namespace Tests.Day8Tests
{
	public class Day8Tests
	{
		[TestCase("003006012012", 4)]
		public void FindsExpectedChecksum(string input, int expectedOutput)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(input);

			var day8 = new Solution(puzzle);
			Solution.OverrideDimensions(3,2);

			// Act
			string checksum = day8.SolvePart1();

			// Assert
			checksum.ShouldBe($"Part 1: {expectedOutput}");
		}
		
		[TestCase("0222112222120000", "Part 2:\n #\r\n# \r\n")]
		public void DecodesImageCorrectly(string input, string expectedResult)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(input);

			var day8 = new Solution(puzzle);
			Solution.OverrideDimensions(2,2);

			// Act
			string output = day8.SolvePart2();

			// Assert
			output.ShouldBe(expectedResult);
		}
	}
}