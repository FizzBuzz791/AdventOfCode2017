using System;
using System.Linq;

namespace Day5
{
    public class IntCodeMachine
    {
        public int[] Memory { get; }
        public int InstructionPointer { get; private set; } = 0;

        public IntCodeMachine(int[] initialState)
        {
            Memory = initialState.ToArray(); // Use .ToArray so we get a copy instead of a reference.
        }

        public void Execute()
        {
            var operation = new Operation(Memory[InstructionPointer]);
            while (operation.OpCode != OpCode.Halt)
            {
                switch (operation.OpCode)
                {
                    case OpCode.Add:
                        Add(operation, InstructionPointer);
                        break;
                    case OpCode.Multiply:
                        Multiply(operation, InstructionPointer);
                        break;
                    case OpCode.Input:
                        Input(operation, InstructionPointer);
                        break;
                    case OpCode.Output:
                        Output(operation, InstructionPointer);
                        break;
                }

                operation = new Operation(Memory[InstructionPointer]);
            }
            Console.WriteLine("Halt");
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

            IncrementInstructionPointer(4);
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

            IncrementInstructionPointer(4);
        }

        private void Input(Operation operation, int instructionAddress)
        {
            // Input operation's first param is an address, no point in checking the mode.
            int inputAddress = Memory[instructionAddress + 1];

            Memory[inputAddress] = 1; // This'll work for Day 5...

            IncrementInstructionPointer(2);
        }

        private void Output(Operation operation, int instructionAddress)
        {
            // Output operation's first param is an address, no point in checking the mode.
            int output;
            if (operation.FirstParameterMode == Mode.Immediate)
                output = Memory[instructionAddress + 1];
            else
                output = Memory[Memory[instructionAddress + 1]];

            Console.WriteLine(output);

            IncrementInstructionPointer(2);
        }

        private void IncrementInstructionPointer(int increment)
        {
            InstructionPointer += increment;
        }
    }
}