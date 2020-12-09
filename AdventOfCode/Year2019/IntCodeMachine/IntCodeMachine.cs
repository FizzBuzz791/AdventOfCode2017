﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode.Year2019.IntCodeMachine
{
    public class IntCodeMachine
    {
        public BigInteger[] Memory { get; } = new BigInteger[1000000];
        private int InstructionPointer { get; set; }
        public Queue<int> InputValues { get; }
        public List<string> Outputs { get; } = new();
        public MachineState State { get; private set; } = MachineState.Paused;
        private int RelativeBase { get; set; }

        public IntCodeMachine(BigInteger[] initialState)
        {
            initialState.CopyTo(Memory, 0);

            InputValues = new Queue<int>();
        }

        public IntCodeMachine(BigInteger[] initialState, IEnumerable<int> inputs)
        {
            initialState.CopyTo(Memory, 0);

            InputValues = new Queue<int>(inputs);
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
                foreach (var output in Outputs)
                    Console.WriteLine(output);
        }

        private void Add(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            SetParameter(operation.ThirdParameterMode, instructionAddress + 3, firstParam + secondParam);

            IncrementInstructionPointer(4);
        }

        private void Multiply(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            SetParameter(operation.ThirdParameterMode, instructionAddress + 3, firstParam * secondParam);

            IncrementInstructionPointer(4);
        }

        private void Input(Operation operation, int instructionAddress)
        {
            if (InputValues.Count > 0)
            {
                SetParameter(operation.FirstParameterMode, instructionAddress + 1, InputValues.Dequeue());

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

            SetParameter(operation.ThirdParameterMode, instructionAddress + 3, firstParam < secondParam ? 1 : 0);

            IncrementInstructionPointer(4);
        }

        private void Equals(Operation operation, int instructionAddress)
        {
            BigInteger firstParam = GetParameter(operation.FirstParameterMode, instructionAddress + 1);
            BigInteger secondParam = GetParameter(operation.SecondParameterMode, instructionAddress + 2);

            SetParameter(operation.ThirdParameterMode, instructionAddress + 3, firstParam == secondParam ? 1 : 0);

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
            int parameterAddress = parameterMode switch
            {
                Mode.Position => (int)Memory[instructionAddress],
                Mode.Immediate => instructionAddress,
                Mode.Relative => (int)Memory[instructionAddress] + RelativeBase,
                _ => throw new ArgumentException($"Unknown parameter mode: {parameterMode}", nameof(parameterMode)),
            };
            return Memory[parameterAddress];
        }

        private void SetParameter(Mode parameterMode, int instructionAddress, BigInteger value)
        {
            var parameterAddress = (int)Memory[instructionAddress];

            if (parameterMode == Mode.Relative)
                parameterAddress += RelativeBase;

            Memory[parameterAddress] = value;
        }

        private void IncrementInstructionPointer(int increment) => InstructionPointer += increment;

        private void SetInstructionPointer(int value) => InstructionPointer = value;
    }
}