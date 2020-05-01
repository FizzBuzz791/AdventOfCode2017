namespace Day7
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
        Halt = 99
    }

    public enum Mode
    {
        Position = 0,
        Immediate = 1
    }
}