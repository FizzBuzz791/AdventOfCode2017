using Day8;
using NUnit.Framework;
using Shouldly;

namespace Day8Tests
{
    public class Tests
    {
        [TestCase("003006012012", 4)]
        public void FindsExpectedChecksum(string input, int expectedOutput)
        {
            // Arrange

            // Act
            var checksum = Program.Part1(input, 3, 2);

            // Assert
            checksum.ShouldBe(expectedOutput);
        }
    }
}