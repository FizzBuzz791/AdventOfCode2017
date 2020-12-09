using System.Collections.Generic;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day3;

namespace Tests.Day3Tests
{
    public class Day3Tests
    {
        [TestCaseSource(nameof(Part1Cases))]
        public void FindsOverlaps(string input, int expectedResult)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(input);
            
            var day3 = new Solution(puzzle);

            // Act
            string overlap = day3.SolvePart1();

            // Assert
            overlap.ShouldBe($"Part 1: {expectedResult}");
        }

        private static IEnumerable<object[]> Part1Cases()
        {
            yield return new object[] { "#1 @ 1,3: 4x4\n#2 @ 3,1: 4x4\n#3 @ 5,5: 2x2\n", 4 };
        }

        [TestCaseSource(nameof(Part2Cases))]
        public void FindsIntactClaim(string input, int expectedResult)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(input);
            
            var day3 = new Solution(puzzle);

            // Act
            string intactClaim = day3.SolvePart2();

            // Assert
            intactClaim.ShouldBe($"Part 2: {expectedResult}");
        }
        
        private static IEnumerable<object[]> Part2Cases()
        {
            yield return new object[] { "#1 @ 1,3: 4x4\n#2 @ 3,1: 4x4\n#3 @ 5,5: 2x2\n", 3 };
        }
    }
}