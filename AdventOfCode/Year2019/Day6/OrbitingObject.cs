using System.Collections.Generic;

namespace AdventOfCode.Year2019.Day6
{
    public record OrbitingObject
    {
        public string? Name { get; init; }
        public int OrbitLevel { get; init; }
        public List<OrbitingObject> OrbitingObjects { get; } = new();
    }
}