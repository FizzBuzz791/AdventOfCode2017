using AdventOfCode.Year2020.Day2;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Tests.Year2020.Day2
{
    public class SolutionTests
    {
        [Test]
        public void Part1ReturnsExpectedResult()
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => "1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc");

            var day2 = new Solution(puzzle);

            // Act
            string result = day2.SolvePart1();

            // Assert
            result.ShouldBe($"Part 1: {2}");
        }

        [Test]
        public void Part2ReturnsExpectedResult()
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => "1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc");

            var day2 = new Solution(puzzle);

            // Act
            string result = day2.SolvePart2();

            // Assert
            result.ShouldBe($"Part 2: {1}");
        }
    }
}