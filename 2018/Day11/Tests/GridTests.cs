using System.Drawing;
using Day11;
using NUnit.Framework;

namespace Day11Tests
{
    public class GridTests
    {
        [TestCase(18, 29, 33, 45)]
        [TestCase(42, 30, 21, 61)]
        [TestCase(2694, 30, 243, 38)] // Actual
        public void GetsTotalPowerLevel(int gridSerialNumber, int expectedPowerLevel, int expectedXCoordinate, int expectedYCoordinate)
        {
            // Arrange
            var grid = new FuelGrid(new Size(300, 300), gridSerialNumber);

            // Act
            (Point coordinate, int powerLevel) = grid.GetHighestPowerSquare();

            // Assert
            Assert.That(powerLevel, Is.EqualTo(expectedPowerLevel));
            Assert.That(coordinate.X, Is.EqualTo(expectedXCoordinate));
            Assert.That(coordinate.Y, Is.EqualTo(expectedYCoordinate));
        }
    }
}