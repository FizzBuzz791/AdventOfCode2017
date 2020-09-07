using System.Collections.Generic;

namespace Solutions.Day6
{
	public class OrbitingObject
	{
		public string Name { get; }
		public int OrbitLevel { get; }
		public List<OrbitingObject> OrbitingObjects { get; } = new List<OrbitingObject>();

		public OrbitingObject(string name, int orbitLevel)
		{
			Name = name;
			OrbitLevel = orbitLevel;
		}
	}
}