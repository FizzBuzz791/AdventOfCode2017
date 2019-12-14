using NUnit.Framework;
using Day1;
using Shouldly;

namespace Day1Tests
{
    public class Part1Tests
    {
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void CalculateFuelReturnsExpectedResult(int moduleWeight, int expectedFuel)
        {
            // Arrange

            // Act
            int calculatedfuel = Program.CalculateFuel(moduleWeight);

            // Assert
            calculatedfuel.ShouldBe(expectedFuel);
        }
    }
}