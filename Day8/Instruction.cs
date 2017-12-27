using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day8
{
	// ReSharper disable once UseNameofExpression
	[DebuggerDisplay("Register: {Register} | Increment: {Increment} | Step: {Step}")]
	public class Instruction
	{
		public string Register { get; }
		public bool Increment { get; }
		public int Step { get; }
		public string[] Condition { get; }

		public Instruction(IList<string> instruction)
		{
			Register = instruction[0];
			Increment = instruction[1] == "inc";
			Step = int.Parse(instruction[2]);
			// Skip 4 to ignore the "if"
			Condition = instruction.Skip(4).ToArray();
		}
	}
}