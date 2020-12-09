using AdventOfCode.Year2020.Day5;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Tests.Year2020.Day5
{
    public class SolutionTests
    {
        [TestCase("FBFBBFFRLR", 357)]
        [TestCase("BFFFBBFRRR", 567)]
        [TestCase("FFFBBBFRRR", 119)]
        [TestCase("BBFFBBFRLL", 820)]
        public void Part1ReturnsExpectedResult(string seat, int id)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => seat);

            var day5 = new Solution(puzzle);

            // Act
            string result = day5.SolvePart1();

            // Assert
            result.ShouldBe($"Part 1: {id}");
        }
    }
}