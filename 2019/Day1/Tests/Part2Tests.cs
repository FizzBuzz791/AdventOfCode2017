using NUnit.Framework;
using Day1;
using Shouldly;

namespace Day1Tests
{
    public class Part2Tests
    {
        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void CalculateFuelReturnsExpectedResult(int moduleWeight, int expectedFuel)
        {
            // Arrange

            // Act
            int calculatedfuel = Program.CalculateFuel(moduleWeight, true);

            // Assert
            calculatedfuel.ShouldBe(expectedFuel);
        }
    }
}