using NUnit.Framework;
using Shouldly;
using Solutions.Day6;

namespace Tests.Day6Tests
{
	public class OrbitMapTests
	{
		[Test]
		public void OrbitMapBuildsCorrectly()
		{
			// Arrange
			var orbits = new[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" };
			var orbitMap = new OrbitMap();

			// Act
			orbitMap.Build(orbits);

			// Assert
			orbitMap.CenterOfMass.ShouldNotBeNull();
			orbitMap.CenterOfMass.Name.ShouldBe("COM");
			orbitMap.OrbitChecksum.ShouldBe(42);
		}
	}
}