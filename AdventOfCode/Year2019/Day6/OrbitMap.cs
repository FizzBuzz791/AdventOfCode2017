using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Day6
{
    public class OrbitMap
    {
        public OrbitingObject? CenterOfMass { get; private set; }
        public int OrbitChecksum { get; private set; }

        public void Build(string[] orbits)
        {
            CenterOfMass = new OrbitingObject { Name = "COM", OrbitLevel = 0 };
            CenterOfMass.OrbitingObjects.AddRange(GetOrbitingObjects(orbits, CenterOfMass.Name, 0));

            OrbitChecksum += CenterOfMass.OrbitingObjects.Sum(o => o.OrbitLevel);
        }

        private IEnumerable<OrbitingObject> GetOrbitingObjects(string[] orbits, string name, int currentOrbitLevel)
        {
            var orbitingObjects = new List<OrbitingObject>();

            // Need to add the ')' or C and COM both match and an infinite loop occurs.
            foreach (var orbit in orbits.Where(o => o.StartsWith($"{name})")))
            {
                var orbitingObject = new OrbitingObject
                    { Name = orbit.Substring(orbit.IndexOf(')') + 1), OrbitLevel = currentOrbitLevel + 1 };

                if (orbits.Any(o => o.StartsWith(orbitingObject.Name)))
                    orbitingObject.OrbitingObjects.AddRange(
                        GetOrbitingObjects(orbits, orbitingObject.Name, currentOrbitLevel + 1));

                orbitingObjects.Add(orbitingObject);
                OrbitChecksum += orbitingObject.OrbitingObjects.Sum(o => o.OrbitLevel);
            }

            return orbitingObjects;
        }
    }
}