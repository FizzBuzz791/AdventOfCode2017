using System.Collections.Generic;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day6;

namespace Tests.Day6Tests
{
    public class Day6Tests
    {
        [TestCaseSource(nameof(Part1Cases))]
        public void FindsLargestFiniteArea(string input, int expectedSize)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(input);

            var day6 = new Solution(puzzle);

            // Act
            string largestFiniteArea = day6.SolvePart1();

            // Assert
            largestFiniteArea.ShouldBe($"Part 1: {expectedSize}");
        }

        private static IEnumerable<object[]> Part1Cases()
        {
            yield return new object[] { "1, 1\n1, 6\n8, 3\n3, 4\n5, 5\n8, 9", 17 };
        }

        [Ignore("Not sure how to do this with the current infra (example uses 32 instead of 10000).")]
        [TestCaseSource(nameof(Part2Cases))]
        public void FindsRegionClosestToAllCoordinates(string input, int expectedSize)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(input);
            
            var day6 = new Solution(puzzle);

            // Act
            string closestRegion = day6.SolvePart2();

            // Assert
            closestRegion.ShouldBe($"Part 2: {expectedSize}");
        }

        private static IEnumerable<object[]> Part2Cases()
        {
            yield return new object[] { "", 0 };
        }
    }
}