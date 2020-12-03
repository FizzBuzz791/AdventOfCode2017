namespace AdventOfCode.Year2019.Day13
{
    public record Instruction
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int TileId { get; init; }
    }
}