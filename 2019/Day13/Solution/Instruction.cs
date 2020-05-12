using System;
using System.Collections.Generic;

namespace Day13
{
    public class Instruction
    {
        public int X { get; }
        public int Y { get; }
        public int TileId { get; }

        public Instruction(List<string> instruction)
        {
            X = Int32.Parse(instruction[0]);
            Y = Int32.Parse(instruction[1]);
            TileId = Int32.Parse(instruction[2]);
        }
    }
}