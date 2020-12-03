using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2019.Day6
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split("\n"))
        {
        }

        public string SolvePart1()
		{
			var orbitMap = new OrbitMap();
			orbitMap.Build(Input);

			return $"Part 1: {orbitMap.OrbitChecksum}";
		}

		public string SolvePart2()
		{
			var orbitMap = new OrbitMap();
			orbitMap.Build(Input);
			
			if (orbitMap.CenterOfMass == null)
				throw new Exception("Something went wrong building the Orbit Map");

			// 1. Find what you're orbiting
			OrbitingObject? yourOrbit = FindOrbitingObject(orbitMap.CenterOfMass, "YOU");

			// 2. Find what Santa's orbiting
			OrbitingObject? santasOrbit = FindOrbitingObject(orbitMap.CenterOfMass, "SAN");

			var visitedByYou = new Dictionary<string, int>();
			var visitedBySanta = new Dictionary<string, int>();
			string commonObjectName = string.Empty;
			if (yourOrbit != null && santasOrbit != null)
			{
				// 3. Figure out how to get from whatever YOU are orbiting to whatever SAN is orbiting
				OrbitingObject? path1 = yourOrbit;
				visitedByYou.Add(path1.Name, 0);
				var yourJumpCount = 0;
				OrbitingObject? path2 = santasOrbit;
				visitedBySanta.Add(path2.Name, 0);
				var santasJumpCount = 0;

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
			}

			return visitedByYou.ContainsKey(commonObjectName) && visitedBySanta.ContainsKey(commonObjectName)
				? $"Part 2: {visitedByYou[commonObjectName] + visitedBySanta[commonObjectName]}"
				: "Part 2: Unknown";
		}

		private static OrbitingObject? FindOrbitingObject(OrbitingObject orbitingObject, string nameToFind)
		{
			OrbitingObject? result = null;

			if (orbitingObject.OrbitingObjects.Any(o => o.Name == nameToFind))
			{
				result = orbitingObject;
			}
			else if (orbitingObject.OrbitingObjects.Count > 0)
			{
				result = orbitingObject.OrbitingObjects.Select(oo => FindOrbitingObject(oo, nameToFind))
				                       .SingleOrDefault(oo => oo != null);
			}

			return result;
		}
    }
}