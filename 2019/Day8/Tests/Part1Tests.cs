using Day8;
using NUnit.Framework;
using Shouldly;

namespace Day8Tests
{
    public class Part1Tests
    {
        [TestCase("003006012012", 4)]
        public void FindsExpectedChecksum(string input, int expectedOutput)
        {
            // Arrange
            var layers = Program.CalculateLayers(input, 3, 2);

            // Act
            var checksum = Program.Part1(layers);

            // Assert
            checksum.ShouldBe(expectedOutput);
        }
    }
}