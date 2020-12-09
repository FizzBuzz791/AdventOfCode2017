using NAoCHelper;

namespace AdventOfCode.Year2018.Day8
{
    public class Solution : BaseSolution<string>, ISolvable
    {
        private readonly Tree _tree;
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x)
        {
            _tree = new Tree();
            _tree.Build(Input);
        }

        public string SolvePart1() => $"Part 1: {Tree.GetMetadataTotal(_tree.RootNode!)}";

        public string SolvePart2() => $"Part 2: {Tree.GetNodeValue(_tree.RootNode!)}";
    }
}