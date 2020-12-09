using AdventOfCode.Year2020.Day1;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Tests.Year2020.Day1
{
    public class SolutionTests
    {
        [Test]
        public void Part1ReturnsExpectedResult()
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => "1721\n979\n366\n299\n675\n1456");

            var day1 = new Solution(puzzle);

            // Act
            string result = day1.SolvePart1();

            // Assert
            result.ShouldBe($"Part 1: {514579}");
        }
        
        [Test]
        public void Part2ReturnsExpectedResult()
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => "1721\n979\n366\n299\n675\n1456");

            var day1 = new Solution(puzzle);

            // Act
            string result = day1.SolvePart2();

            // Assert
            result.ShouldBe($"Part 2: {241861950}");
        }
    }
}