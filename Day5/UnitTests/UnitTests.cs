using NUnit.Framework;

namespace Day5.UnitTests
{
	[TestFixture]
	internal class UnitTests
	{
		[TestCase(new[] {"1"}, 1, TestName = "One")]
		[TestCase(new[] {"0"}, 2, TestName = "Zero")]
		[TestCase(new[] {"1", "1"}, 2, TestName = "Double")]
		[TestCase(new[] {"2", "1"}, 1, TestName = "Two")]
		[TestCase(new[] {"1", "-1"}, 3, TestName = "Negative")]
		[TestCase(new[] {"0", "3", "0", "1", "-3"}, 5, TestName = "Example")]
		public void Part1ReturnsExpectedResult(string[] puzzleInput, int expectedResult)
		{
			// Arrange
			InstructionParser sut = new InstructionParser();

			// Act
			int stepsToEscape = sut.Part1(puzzleInput);

			// Assert
			Assert.That(stepsToEscape, Is.EqualTo(expectedResult));
		}

		[TestCase(new[] {"1"}, 1, TestName = "One")]
		[TestCase(new[] {"3"}, 1, TestName = "Three")]
		[TestCase(new[] {"3", "1", "1", "-3"}, 7, TestName = "OverThree")]
		[TestCase(new[] { "0", "3", "0", "1", "-3" }, 10, TestName = "Example")]
		public void Part2ReturnsExpectedResult(string[] puzzleInput, int expectedResult)
		{
			// Arrange
			InstructionParser sut = new InstructionParser();

			// Act
			int stepsToEscape = sut.Part2(puzzleInput);

			// Assert
			Assert.That(stepsToEscape, Is.EqualTo(expectedResult));
		}
	}
}