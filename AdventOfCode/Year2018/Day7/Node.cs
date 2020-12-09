using System.Diagnostics;

namespace AdventOfCode.Year2018.Day7
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public record Node
    {
        public char Name { get; init; }
        public int Time => 60 + Name % 32;
    }
}