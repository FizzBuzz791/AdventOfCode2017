using Day6;
using NUnit.Framework;
using Shouldly;

namespace Day6Tests
{
    public class Part2Tests
    {
        [Test]
        public void Part2FindsJumpCountCorrectly()
        {
            // Arrange
            var orbits = new string[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN" };
            var orbitMap = new OrbitMap();
            orbitMap.Build(orbits);

            // Act
            var jumpCount = Program.Part2(orbitMap);

            // Assert
            jumpCount.ShouldBe(4);
        }
    }
}