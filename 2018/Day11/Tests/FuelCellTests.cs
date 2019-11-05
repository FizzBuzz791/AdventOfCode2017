using System.Drawing;
using Day11;
using NUnit.Framework;

namespace Day11Tests
{
    public class FuelCellTests
    {
        [TestCase(2, 4, 8, 4)]
        [TestCase(121, 78, 57, -5)]
        [TestCase(216, 195, 39, 0)]
        [TestCase(100, 152, 71, 4)]
        public void CalculatesPowerLevel(int x, int y, int gridSerial, int expectedPowerLevel)
        {
            // Arrange
            var coordinate = new Point(x, y);

            // Act
            var fuelCell = new FuelCell(coordinate, gridSerial);

            // Assert
            Assert.That(fuelCell.PowerLevel, Is.EqualTo(expectedPowerLevel));
        }
    }
}