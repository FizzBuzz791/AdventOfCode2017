using Day7;
using NUnit.Framework;
using Shouldly;

namespace Day7Tests
{
    public class OperationTests
    {
        [TestCase(1002, OpCode.Multiply, Mode.Position, Mode.Immediate, Mode.Position)]
        public void CanSuccessfullyConstructOperationFromInteger(int input, OpCode expectedOpCode, Mode expectedMode1, Mode expectedMode2, Mode expectedMode3)
        {
            // Arrange

            // Act
            var operation = new Operation(input);

            // Assert
            operation.OpCode.ShouldBe(expectedOpCode);
        }
    }
}