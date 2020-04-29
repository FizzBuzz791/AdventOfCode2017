using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace Day6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("e27fdcf7-d252-4000-a18d-986c51dbd22f"));
            var puzzle = new Puzzle(user, 2019, 6);
            var input = puzzle.GetInputAsync().Result;

            var orbits = input.Split("\n");

            var orbitMap = new OrbitMap();
            orbitMap.Build(orbits);

            Console.WriteLine($"Total orbits: {orbitMap.OrbitChecksum}");
        }
    }

    public class OrbitMap
    {
        public OrbitingObject CenterOfMass { get; private set; }
        public int OrbitChecksum { get; private set; }

        public void Build(string[] orbits)
        {
            CenterOfMass = new OrbitingObject("COM", 0);
            CenterOfMass.OrbitingObjects.AddRange(GetOrbitingObjects(orbits, CenterOfMass.Name, 0));

            OrbitChecksum += CenterOfMass.OrbitingObjects.Sum(o => o.OrbitLevel);
        }

        private List<OrbitingObject> GetOrbitingObjects(string[] orbits, string name, int currentOrbitLevel)
        {
            var orbitingObjects = new List<OrbitingObject>();

            // Need to add the ')' or C and COM both match and an infinite loop occurs.
            foreach (var orbit in orbits.Where(o => o.StartsWith(name + ')')))
            {
                var orbitingObject = new OrbitingObject(orbit.Substring(orbit.IndexOf(')') + 1), currentOrbitLevel + 1);

                if (orbits.Any(o => o.StartsWith(orbitingObject.Name)))
                    orbitingObject.OrbitingObjects.AddRange(GetOrbitingObjects(orbits, orbitingObject.Name, currentOrbitLevel + 1));

                orbitingObjects.Add(orbitingObject);
                OrbitChecksum += orbitingObject.OrbitingObjects.Sum(o => o.OrbitLevel);
            }

            return orbitingObjects;
        }
    }

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
