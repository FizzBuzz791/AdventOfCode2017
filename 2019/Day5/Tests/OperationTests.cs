using Day5;
using NUnit.Framework;
using Shouldly;

namespace Day5Tests
{
    public class OperationTests
    {
        [TestCase(1002, 2, Mode.Position, Mode.Immediate, Mode.Position)]
        public void CanSuccessfullyConstructOperationFromInteger(int input, int expectedOpCode, Mode expectedMode1, Mode expectedMode2, Mode expectedMode3)
        {
            // Arrange

            // Act
            var operation = new Operation(input);

            // Assert
            operation.OpCode.ShouldBe(expectedOpCode);
        }
    }
}