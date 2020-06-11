using System.Collections.Generic;
using Day16;
using NUnit.Framework;
using Shouldly;

namespace Day16Tests
{
    public class Part2Tests
    {
        [TestCaseSource(nameof(Part2Cases))]
        public void DecodesInputCorrectly(string input, string expectedMessage)
        {
            // Arrange

            // Act
            var result = Program.DecodeSignal(input);

            // Assert
            result.ShouldBe(expectedMessage);
        }

        private static IEnumerable<object[]> Part2Cases()
        {
            yield return new object[] { "03036732577212944063491565474664", "84462026" };
            yield return new object[] { "02935109699940807407585447034323", "78725270" };
            yield return new object[] { "03081770884921959731165446850517", "53553731" };
        }
    }
}