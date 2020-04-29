using System.Linq;

namespace Day5
{
    public class IntCodeMachine
    {
        public int[] Memory { get; }
        public int InstructionPointer { get; private set; } = 0;

        private const int AddOpCode = 1;
        private const int MultiplyOpCode = 2;
        private const int HaltOpCode = 99;

        public IntCodeMachine(int[] initialState)
        {
            Memory = initialState.ToArray(); // Use .ToArray so we get a copy instead of a reference.
        }

        public void Execute()
        {
            var operation = new Operation(Memory[InstructionPointer]);
            while (operation.OpCode != HaltOpCode)
            {
                switch (operation.OpCode)
                {
                    case AddOpCode:
                        Add(operation, InstructionPointer);
                        break;
                    case MultiplyOpCode:
                        Multiply(operation, InstructionPointer);
                        break;
                }

                operation = new Operation(Memory[InstructionPointer]);
            }
        }

        private void Add(Operation operation, int instructionAddress)
        {
            int firstParam;
            if (operation.FirstParameterMode == Mode.Immediate)
                firstParam = Memory[instructionAddress + 1]; // Use the value directly.
            else
                firstParam = Memory[Memory[instructionAddress + 1]]; // Find the value at the given address.

            int secondParam;
            if (operation.SecondParameterMode == Mode.Immediate)
                secondParam = Memory[instructionAddress + 2]; // Use the value directly
            else
                secondParam = Memory[Memory[instructionAddress + 2]]; // Find the value at the given address.

            // Doesn't make sense for the result to be Mode.Immediate, assume Mode.Position
            Memory[Memory[instructionAddress + 3]] = firstParam + secondParam;

            IncrementInstructionPointer();
        }

        private void Multiply(Operation operation, int instructionAddress)
        {
            int firstParam;
            if (operation.FirstParameterMode == Mode.Immediate)
                firstParam = Memory[instructionAddress + 1]; // Use the value directly.
            else
                firstParam = Memory[Memory[instructionAddress + 1]]; // Find the value at the given address.

            int secondParam;
            if (operation.SecondParameterMode == Mode.Immediate)
                secondParam = Memory[instructionAddress + 2]; // Use the value directly
            else
                secondParam = Memory[Memory[instructionAddress + 2]]; // Find the value at the given address.

            // Doesn't make sense for the result to be Mode.Immediate, assume Mode.Position
            Memory[Memory[instructionAddress + 3]] = firstParam * secondParam;

            IncrementInstructionPointer();
        }

        private void IncrementInstructionPointer(int increment = 4)
        {
            InstructionPointer += increment;
        }
    }
}