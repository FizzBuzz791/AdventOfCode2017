using System.Linq;

namespace Day2
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
            int operation = Memory[InstructionPointer];
            while (Memory[InstructionPointer] != HaltOpCode)
            {
                switch (operation)
                {
                    case AddOpCode:
                        Add(InstructionPointer);
                        break;
                    case MultiplyOpCode:
                        Multiply(InstructionPointer);
                        break;
                }

                operation = Memory[InstructionPointer];
            }
        }

        private void Add(int instructionAddress)
        {
            int firstParamAddress = Memory[instructionAddress + 1];
            int secondParamAddress = Memory[instructionAddress + 2];
            int resultAddress = Memory[instructionAddress + 3];

            int firstParam = Memory[firstParamAddress];
            int secondParam = Memory[secondParamAddress];

            int result = firstParam + secondParam;
            Memory[resultAddress] = result;

            IncrementInstructionPointer();
        }

        private void Multiply(int instructionAddress)
        {
            int firstParamAddress = Memory[instructionAddress + 1];
            int secondTermAddress = Memory[instructionAddress + 2];
            int resultAddress = Memory[instructionAddress + 3];

            int firstParam = Memory[firstParamAddress];
            int secondParam = Memory[secondTermAddress];

            int result = firstParam * secondParam;
            Memory[resultAddress] = result;

            IncrementInstructionPointer();
        }

        private void IncrementInstructionPointer(int increment = 4)
        {
            InstructionPointer += increment;
        }
    }
}