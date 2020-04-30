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

            var jumpCount = Part2(orbitMap);
            Console.WriteLine($"It took {jumpCount} orbital jumps to reach SAN");
        }

        public static int Part2(OrbitMap orbitMap)
        {
            // 1. Find what you're orbiting
            var yourOrbit = FindOrbitingObject(orbitMap.CenterOfMass, "YOU");
            Console.WriteLine($"YOU are orbiting {yourOrbit.Name}");

            // 2. Find what Santa's orbiting
            var santasOrbit = FindOrbitingObject(orbitMap.CenterOfMass, "SAN");
            Console.WriteLine($"SAN is orbiting {santasOrbit.Name}");

            // 3. Figure out how to get from whatever YOU are orbiting to whatever SAN is orbiting
            var path1 = yourOrbit;
            var visitedByYou = new Dictionary<string, int>() { { path1.Name, 0 } };
            var yourJumpCount = 0;
            var path2 = santasOrbit;
            var visitedBySanta = new Dictionary<string, int>() { { path2.Name, 0 } };
            var santasJumpCount = 0;
            string commonObjectName = string.Empty;

            while (string.IsNullOrWhiteSpace(commonObjectName))
            {
                if (path1 != null && string.IsNullOrWhiteSpace(commonObjectName))
                {
                    path1 = FindOrbitingObject(orbitMap.CenterOfMass, path1.Name);
                    if (path1 != null)
                    {
                        yourJumpCount++;
                        visitedByYou.Add(path1.Name, yourJumpCount);

                        // Exit condition
                        commonObjectName = visitedBySanta.ContainsKey(path1.Name) ? path1.Name : string.Empty;
                    }
                }

                if (path2 != null && string.IsNullOrWhiteSpace(commonObjectName))
                {
                    path2 = FindOrbitingObject(orbitMap.CenterOfMass, path2.Name);
                    if (path2 != null)
                    {
                        santasJumpCount++;
                        visitedBySanta.Add(path2.Name, santasJumpCount);

                        // Exit condition
                        commonObjectName = visitedByYou.ContainsKey(path2.Name) ? path2.Name : string.Empty;
                    }
                }
            }

            return visitedByYou[commonObjectName] + visitedBySanta[commonObjectName];
        }

        private static OrbitingObject FindOrbitingObject(OrbitingObject orbitingObject, string nameToFind)
        {
            if (orbitingObject.OrbitingObjects.Any(o => o.Name == nameToFind))
            {
                return orbitingObject;
            }
            else if (orbitingObject.OrbitingObjects.Count > 0)
            {
                return orbitingObject.OrbitingObjects.Select(oo => FindOrbitingObject(oo, nameToFind)).SingleOrDefault(oo => oo != null);
            }
            else
            {
                return null;
            }
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
