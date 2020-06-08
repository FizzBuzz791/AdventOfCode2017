using System.Collections.Generic;
using System.Linq;
using Day16;
using NUnit.Framework;
using Shouldly;

namespace Day16Tests
{
    public class Tests
    {
        [TestCaseSource(nameof(Part1Cases))]
        public void GeneratesOutputCorrectly(string input, int phases, List<int> expectedOutput)
        {
            // Arrange

            // Act
            var result = Program.Part1(input, phases);

            // Assert
            result.Take(8).ShouldBe(expectedOutput);
        }

        private static IEnumerable<object[]> Part1Cases()
        {
            yield return new object[] { "12345678", 4, new List<int> { 0, 1, 0, 2, 9, 4, 9, 8 } };
            yield return new object[] { "80871224585914546619083218645595", 100, new List<int> { 2, 4, 1, 7, 6, 1, 7, 6 } };
            yield return new object[] { "19617804207202209144916044189917", 100, new List<int> { 7, 3, 7, 4, 5, 4, 1, 8 } };
            yield return new object[] { "69317163492948606335995924319873", 100, new List<int> { 5, 2, 4, 3, 2, 1, 3, 3 } };
        }
    }
}