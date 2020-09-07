using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day6;

namespace Tests.Day6Tests
{
	public class Day6Tests
	{
		[Test]
		public void Part2FindsJumpCountCorrectly()
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync()
			      .Returns(x => "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN");

			var day6 = new Solution(puzzle);

			// Act
			string jumpCount = day6.SolvePart2();

			// Assert
			jumpCount.ShouldBe("Part 2: 4");
		}
	}
}