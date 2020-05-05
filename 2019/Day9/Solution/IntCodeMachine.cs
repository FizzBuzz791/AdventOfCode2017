using System;
using System.Collections.Generic;
using System.Numerics;

namespace Day9
{
    public class IntCodeMachine
    {
        public BigInteger[] Memory { get; } = new BigInteger[1000000];
        public int InstructionPointer { get; private set; } = 0;
        public Queue<int> InputValues { get; }
        public List<string> Outputs { get; } = new List<string>();
        public MachineState State { get; private set; }
        public int RelativeBase { get; set; } = 0;

        public IntCodeMachine(BigInteger[] initialState)
        {
            initialState.CopyTo(Memory, 0);

            State = MachineState.Paused;
        }

        public IntCodeMachine(BigInteger[] initialState, int[] inputs)
        {
            initialState.CopyTo(Memory, 0);

            InputValues = new Queue<int>(inputs);
            State = MachineState.Paused;
        }

        public void Execute(bool printOutput = true)
        {
            State = MachineState.Running;
            var operation = new Operation(Memory[InstructionPointer]);
            while (State == MachineState.Running)
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
                    case OpCode.JumpIfTrue:
                        JumpIfTrue(operation, InstructionPointer);
                        break;
                    case OpCode.JumpIfFalse:
                        JumpIfFalse(operation, InstructionPointer);
                        break;
                    case OpCode.LessThan:
                        LessThan(operation, InstructionPointer);
                        break;
                    case OpCode.Equals:
                        Equals(operation, InstructionPointer);
                        break;
                    case OpCode.AdjustRelativeBase:
                        AdjustRelativeBase(operation, InstructionPointer);
                        break;
                    case OpCode.Halt:
                        State = MachineState.Paused;
                        Outputs.Add("Halt");
                        break;
                    default:
                        State = MachineState.Paused;
                        Outputs.Add($"Encountered unknown operation: {operation.OpCode}");
                        break;
                }

                operation = new Operation(Memory[InstructionPointer]);
            }

            if (printOutput)
            {
                foreach (var output in Outputs)
                {
                    Console.WriteLine(output);
                }
            }
        }

        private void Add(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            // Doesn't make sense for the result to be Mode.Immediate, assume Mode.Position
            Memory[(int)Memory[instructionAddress + 3]] = firstParam + secondParam;

            IncrementInstructionPointer(4);
        }

        private void Multiply(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            // Doesn't make sense for the result to be Mode.Immediate, assume Mode.Position
            Memory[(int)Memory[instructionAddress + 3]] = firstParam * secondParam;

            IncrementInstructionPointer(4);
        }

        private void Input(Operation operation, int instructionAddress)
        {
            // Input operation's first param is an address, no point in checking the mode.
            int inputAddress = (int)Memory[instructionAddress + 1];

            if (InputValues.Count > 0)
            {
                Memory[inputAddress] = InputValues.Dequeue();

                IncrementInstructionPointer(2);
            }
            else
            {
                State = MachineState.Paused;
            }
        }

        private void Output(Operation operation, int instructionAddress)
        {
            BigInteger output = GetParameter(operation.FirstParameterMode, instructionAddress + 1);

            Outputs.Add(output.ToString());

            IncrementInstructionPointer(2);
        }

        private void JumpIfTrue(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            if (firstParam != 0)
                SetInstructionPointer((int)secondParam);
            else
                IncrementInstructionPointer(3);
        }

        private void JumpIfFalse(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            if (firstParam == 0)
                SetInstructionPointer((int)secondParam);
            else
                IncrementInstructionPointer(3);
        }

        private void LessThan(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            // Doesn't make sense for the result to be Mode.Immediate, assume Mode.Position
            Memory[(int)Memory[instructionAddress + 3]] = firstParam < secondParam ? 1 : 0;

            IncrementInstructionPointer(4);
        }

        private void Equals(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            // Doesn't make sense for the result to be Mode.Immediate, assume Mode.Position
            Memory[(int)Memory[instructionAddress + 3]] = firstParam == secondParam ? 1 : 0;

            IncrementInstructionPointer(4);
        }

        private void AdjustRelativeBase(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);

            RelativeBase += (int)firstParam;

            IncrementInstructionPointer(2);
        }

        private BigInteger GetParameter(Mode parameterMode, int instructionAddress)
        {
            switch (parameterMode)
            {
                case Mode.Immediate:
                    return Memory[instructionAddress];
                case Mode.Position:
                    return Memory[(int)Memory[instructionAddress]];
                case Mode.Relative:
                    return Memory[(int)Memory[instructionAddress] + RelativeBase];
                default:
                    throw new ArgumentException(nameof(parameterMode), $"Unknown parameter mode: {parameterMode}");
            }
        }

        private void IncrementInstructionPointer(int increment)
        {
            InstructionPointer += increment;
        }

        private void SetInstructionPointer(int value)
        {
            InstructionPointer = value;
        }
    }
}