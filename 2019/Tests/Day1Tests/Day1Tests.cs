using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day1;

namespace Tests.Day1Tests
{
    public class Day1Tests
    {
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void Part1ReturnsExpectedResult(int moduleWeight, int expectedFuel)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(x => moduleWeight.ToString());
            
            var day1 = new Solution(puzzle);

            // Act
            string result = day1.SolvePart1();

            // Assert
            result.ShouldBe($"Part 1: {expectedFuel}");
        }

        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void Part2ReturnsExpectedResult(int moduleWeight, int expectedFuel)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(x => moduleWeight.ToString());
            
            var day1 = new Solution(puzzle);
            
            // Act
            string result = day1.SolvePart2();

            // Assert
            result.ShouldBe($"Part 2: {expectedFuel}");
        }
    }
}