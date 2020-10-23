using System.Collections.Generic;
using System.Drawing;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day1;

namespace Day1Tests
{
    public class Tests
    {
        [TestCaseSource(nameof(Part1Cases))]
        public void FindsFinalFrequency(string input, int expectedResult)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(input);

            var day1 = new Solution(puzzle);

            // Act
            string finalFrequency = day1.SolvePart1();

            // Assert
            finalFrequency.ShouldBe($"Part 1: {expectedResult}");
        }

        private static IEnumerable<object[]> Part1Cases()
        {
            yield return new object[] { "+1\n-2\n+3\n+1", 3 };
            yield return new object[] { "+1\n+1\n+1", 3 };
            yield return new object[] { "+1\n+1\n-2", 0 };
            yield return new object[] { "-1\n-2\n-3", -6 };
        }

        [TestCaseSource(nameof(Part2Cases))]
        public void FindsFirstDuplicate(string input, int expectedDuplicate)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(input);

            var day1 = new Solution(puzzle);

            // Act
            string duplicateFrequency = day1.SolvePart2();

            // Assert
            duplicateFrequency.ShouldBe($"Part 2: {expectedDuplicate}");
        }

        private static IEnumerable<object[]> Part2Cases()
        {
            yield return new object[] { "+1\n-1", 0 };
            yield return new object[] { "+3\n+3\n+4\n-2\n-4", 10 };
            yield return new object[] { "-6\n+3\n+8\n+5\n-6", 5 };
            yield return new object[] { "+7\n+7\n-2\n-7\n-4", 14 };
        }
    }
}