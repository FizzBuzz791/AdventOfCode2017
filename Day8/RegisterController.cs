using System.Collections.Generic;
using System.Linq;

namespace Day8
{
	public class RegisterController
	{
		public Dictionary<string, int> Registers { get; set; }

		public RegisterController()
		{
			Registers = new Dictionary<string, int>();
		}

		public void InitialiseRegisters(List<Instruction> instructions)
		{
			foreach (IGrouping<string, Instruction> register in instructions.GroupBy(i => i.Register))
			{
				Registers.Add(register.Key, 0);
			}
		}

		public void ProcessInstruction(Instruction instruction)
		{
			string targetRegister = instruction.Condition[0];
			string conditional = instruction.Condition[1];
			int operand = int.Parse(instruction.Condition[2]);

			bool modifyRegister = false;

			switch (conditional)
			{
				case ">":
					modifyRegister = Registers[targetRegister] > operand;
					break;
				case "<":
					modifyRegister = Registers[targetRegister] < operand;
					break;
				case ">=":
					modifyRegister = Registers[targetRegister] >= operand;
					break;
				case "<=":
					modifyRegister = Registers[targetRegister] <= operand;
					break;
				case "!=":
					modifyRegister = Registers[targetRegister] != operand;
					break;
				case "==":
					modifyRegister = Registers[targetRegister] == operand;
					break;
			}

			if (modifyRegister)
			{
				Registers[instruction.Register] = instruction.Increment
					? Registers[instruction.Register] + instruction.Step
					: Registers[instruction.Register] - instruction.Step;
			}
		}
	}
}