using System.Collections.Generic;

namespace AdventOfCode.Year2018.Day8
{
    public class Node
    {
        public List<Node> ChildNodes { get; } = new();
        public List<int> Metadata { get; } = new();
    }
}