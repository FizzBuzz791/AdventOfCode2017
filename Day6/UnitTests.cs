using NUnit.Framework;

namespace Day6
{
	[TestFixture]
	internal class UnitTests
	{
		[TestCase(new[] {"1", "1"}, 2, TestName = "Basic")]
		[TestCase(new[] {"0", "2", "7", "0"}, 5, TestName = "Part1_Example")]
		public void RedistributeUntilDuplicateState_ReturnsExpectedResult(string[] memoryBanks, int expectedResult)
		{
			// Arrange
			Redistributer sut = new Redistributer();

			// Act
			int cycles = sut.RedistributeUntilDuplicateState(memoryBanks);

			// Assert
			Assert.That(cycles, Is.EqualTo(expectedResult));
		}

		[TestCase(new[] { "0", "2", "7", "0" }, 4, TestName = "Part2_Example")]
		public void FindCycles_ReturnsExpectedResult(string[] memoryBanks, int expectedResult)
		{
			// Arrange
			Redistributer sut = new Redistributer();

			// Act
			int cycles = sut.FindCycles(memoryBanks);

			// Assert
			Assert.That(cycles, Is.EqualTo(expectedResult));
		}
	}
}