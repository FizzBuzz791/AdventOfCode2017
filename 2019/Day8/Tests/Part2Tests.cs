using Day8;
using NUnit.Framework;
using Shouldly;

namespace Day8Tests
{
    public class Part2Tests
    {
        [TestCase("0222112222120000", " #\r\n# \r\n")]
        public void FindsExpectedChecksum(string input, string expectedResult)
        {
            // Arrange
            var layers = Program.CalculateLayers(input, 2, 2);

            // Act
            var output = Program.Part2(layers, 2, 2);

            // Assert
            output.ShouldBe(expectedResult);
        }
    }
}