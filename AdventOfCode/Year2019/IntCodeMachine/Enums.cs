namespace AdventOfCode.Year2019.IntCodeMachine
{
    public enum OpCode
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        AdjustRelativeBase = 9,
        Halt = 99
    }

    public enum Mode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2
    }

    public enum MachineState
    {
        Running,
        Paused
    }
}