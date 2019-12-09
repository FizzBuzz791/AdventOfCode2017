using NUnit.Framework;
using Day1;
using Shouldly;

namespace Day1Tests
{
    public class Tests
    {
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void Test1(int moduleWeight, int expectedFuel)
        {
            // Arrange

            // Act
            int calculatedfuel = Program.CalculateFuelForModule(moduleWeight);

            // Assert
            calculatedfuel.ShouldBe(expectedFuel);
        }
    }
}