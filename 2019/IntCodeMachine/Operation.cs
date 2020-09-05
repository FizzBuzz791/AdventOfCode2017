using System;
using System.Numerics;

namespace IntCodeMachine
{
    public class Operation
    {
        public OpCode OpCode { get; }
        public Mode FirstParameterMode { get; }
        public Mode SecondParameterMode { get; }
        public Mode ThirdParameterMode { get; }

        public Operation(BigInteger operation)
        {
            var operationParts = operation.ToString().ToCharArray();
            char partE = operationParts.Length - 1 >= 0 ? operationParts[^1] : '0';
            char partD = operationParts.Length - 2 >= 0 ? operationParts[^2] : '0';
            char partC = operationParts.Length - 3 >= 0 ? operationParts[^3] : '0';
            char partB = operationParts.Length - 4 >= 0 ? operationParts[^4] : '0';
            char partA = operationParts.Length - 5 >= 0 ? operationParts[^5] : '0';

            OpCode = (OpCode)(int.Parse(partD.ToString()) * 10 + int.Parse(partE.ToString()));

            FirstParameterMode = Enum.Parse<Mode>(partC.ToString());
            SecondParameterMode = Enum.Parse<Mode>(partB.ToString());
            ThirdParameterMode = operationParts.Length >= 5 ? Enum.Parse<Mode>(partA.ToString()) : Mode.Position;
        }
    }
}