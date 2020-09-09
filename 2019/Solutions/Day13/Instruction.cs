using System.Collections.Generic;

namespace Solutions.Day13
{
	public class Instruction
	{
		public int X { get; }
		public int Y { get; }
		public int TileId { get; }

		public Instruction(IReadOnlyList<string> instruction)
		{
			X = int.Parse(instruction[0]);
			Y = int.Parse(instruction[1]);
			TileId = int.Parse(instruction[2]);
		}
	}
}